using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Issues.Models;
using Issues.ViewModels;
using Microsoft.AspNet.Identity;

namespace Issues.Controllers
{
  public class ProjectsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Projects
    public async Task<ActionResult> Index()
    {
      var project = db.Project.Include(p => p.Company);
      return View(await project.ToListAsync());
    }

    // GET: Projects/Details/5
    public async Task<ActionResult> Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Project project = await db.Project.FindAsync(id);
      if (project == null)
      {
        return HttpNotFound();
      }
      return View(project);
    }

    // GET: Projects/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Projects/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(NewProjectViewModel model)
    {
      if (ModelState.IsValid)
      {
        string currentUserId = User.Identity.GetUserId();
        int companyId = db.Users.Single(a => a.Id == currentUserId).CompanyId; //TODO Save Campny id in session when during t he login

        Project project = new Project()
        {
          Name = model.Name,
          Description = model.Description,
          CompanyId = companyId,
          DateOfCreate = DateTime.Now,
          DateOfUpdate = DateTime.Now
        };
        db.Project.Add(project);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }

     
      return View(model);
    }

    // GET: Projects/Edit/5
    public async Task<ActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Project project = await db.Project.FindAsync(id);
      if (project == null)
      {
        return HttpNotFound();
      }
      ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", project.CompanyId);
      return View(project);
    }

    // POST: Projects/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Version,DateOfCreate,DateOfUpdate,CompanyId")] Project project)
    {
      if (ModelState.IsValid)
      {
        db.Entry(project).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", project.CompanyId);
      return View(project);
    }

    // GET: Projects/Delete/5
    public async Task<ActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Project project = await db.Project.FindAsync(id);
      if (project == null)
      {
        return HttpNotFound();
      }
      return View(project);
    }

    // POST: Projects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      Project project = await db.Project.FindAsync(id);
      db.Project.Remove(project);
      await db.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
