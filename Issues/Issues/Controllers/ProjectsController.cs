using Issues.Models;
using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Issues.Controllers
{
  public class ProjectsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Projects
    public async Task<ActionResult> Index()
    {
      var projects = db.Projects.Include(p => p.Company);
      return View(await projects.ToListAsync());
    }

    // GET: Projects/Details/5
    public async Task<ActionResult> Details(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Project project = await db.Projects.FindAsync(id);
      if (project == null)
      {
        return HttpNotFound();
      }
      return View(project);
    }

    // GET: Projects/Create
    public ActionResult Create()
    {
      ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");
      return View();
    }

    // POST: Projects/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Version,DateOfCreate,DateOfUpdate,CompanyId")] Project project)
    {
      if (ModelState.IsValid)
      {
        db.Projects.Add(project);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }

      ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", project.CompanyId);
      return View(project);
    }

    // GET: Projects/Edit/5
    public async Task<ActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Project project = await db.Projects.FindAsync(id);
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
    public async Task<ActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Project project = await db.Projects.FindAsync(id);
      if (project == null)
      {
        return HttpNotFound();
      }
      return View(project);
    }

    // POST: Projects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int? id)
    {
      //TODO Add null check
      Project project = await db.Projects.FindAsync(id);
      db.Projects.Remove(project);
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