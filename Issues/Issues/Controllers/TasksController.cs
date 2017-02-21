using Issues.Models;
using Issues.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Issues.Controllers
{
  [Authorize]
  public class TasksController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    public async Task<ActionResult> Index()
    {
      return View(await db.Tasks.Include(t => t.Users).ToListAsync());
    }

    // Get list of user for angular http.get
    public ActionResult TasksList()
    {
      return Json(db.Tasks.Include(t => t.Users)
          .Select(
          t => new
          {
            Id = t.Id,
            Name = t.Name,
            Statement = t.Statement,
            State = t.State.ToString().ToLower(),
            Users = t.Users.Count
          })
          , JsonRequestBehavior.AllowGet);
    }

    public async Task<ActionResult> Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Tasks tasks = await db.Tasks.Include(u => u.Users).SingleAsync(t => t.Id == id);
      if (tasks == null)
      {
        return HttpNotFound();
      }
      return View(tasks);
    }

    [HttpGet]
    public ActionResult Create()
    {
      var users = db.Users
          .Include(u => u.Profile).ToList()
          .Select(u => new { Id = u.Id, Name = $"{u.Profile.FirstName} {u.Profile.LastName}" })
          .ToList();

      string currentUserId = User.Identity.GetUserId();
      int companyId = db.Users.Single(a => a.Id == currentUserId).CompanyId;

      var projectsList = db.Project
        .Include(p => p.Company)
        .Where(p => p.CompanyId == companyId)
        .Select(p => new { Id = p.Id, Name = p.Name })
        .ToList();

      return View(new NewTaskViewModel() { UsersList = new SelectList(users, "Id", "Name"), ProjectsList = new SelectList(projectsList, "Id", "Name") });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(NewTaskViewModel model)
    {
      if (ModelState.IsValid)
      {
        List<ApplicationUser> users = model.UsersId.Select(i => db.Users.Single(u => u.Id == i)).ToList();

        Tasks task = new Tasks()
        {
          Name = model.Name,
          Statement = model.Statement,
          State = model.State,
          DateOfCreate = DateTime.Now,
          DateOfUpdate = DateTime.Now,
          Users = users,
          ProjectId = model.ProjectId
        };

        db.Tasks.Add(task);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }

      var usersList = db.Users
        .Include(u => u.Profile).ToList()
        .Select(u => new { Id = u.Id, Name = $"{u.Profile.FirstName} {u.Profile.LastName}" })
        .ToList();

      string currentUserId = User.Identity.GetUserId();
      int companyId = db.Users.Single(a => a.Id == currentUserId).CompanyId;

      var projectsList = db.Project
        .Include(p => p.Company)
        .Where(p => p.CompanyId == companyId)
        .Select(p => new { Id = p.Id, Name = p.Name })
        .ToList();

      model.ProjectsList = new SelectList(projectsList, "Id", "Name", model.ProjectId);
      model.UsersList = new SelectList(usersList, "Id", "Name", model.UsersId);

      return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Tasks task = await db.Tasks.Include(t => t.Users).SingleAsync(t => t.Id == id);
      if (task == null)
      {
        return HttpNotFound();
      }

      var users = db.Users
          .Include(u => u.Profile).ToList()
          .Select(u => new { Id = u.Id, Name = $"{u.Profile.FirstName} {u.Profile.LastName}" })
          .ToList();
      string[] taskUsersId = task.Users.Select(u => u.Id).ToArray();


      string currentUserId = User.Identity.GetUserId();
      int companyId = db.Users.Single(a => a.Id == currentUserId).CompanyId;

      var projectsList = db.Project
        .Include(p => p.Company)
        .Where(p => p.CompanyId == companyId)
        .Select(p => new { Id = p.Id, Name = p.Name })
        .ToList();


      EditTaskViewModel model = new EditTaskViewModel()
      {
        Id = task.Id,
        Name = task.Name,
        State = task.State,
        Statement = task.Statement,
        UsersId = taskUsersId,
        UserList = new SelectList(users, "Id", "Name", taskUsersId),
        Version = task.Version,
        ProjectId = task.ProjectId,
        ProjectsList =  new SelectList(projectsList, "Id", "Name")
    };

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(EditTaskViewModel model)
    {
      if (ModelState.IsValid)
      {
        Tasks task = await db.Tasks.Include(t => t.Users).SingleAsync(t => t.Id == model.Id);

        if (task == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        task.Name = model.Name;
        task.State = model.State;
        task.Statement = model.Statement;
        task.Users = db.Users.Where(u => model.UsersId.Contains(u.Id)).ToList();
        task.DateOfUpdate = DateTime.Now;
        task.ProjectId = model.ProjectId;

        db.Entry(task).State = EntityState.Modified;
        await db.SaveChangesAsync();

        return RedirectToAction("Index");
      }
      return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Tasks tasks = await db.Tasks.Include(t => t.Users).SingleAsync(t => t.Id == id);
      if (tasks == null)
      {
        return HttpNotFound();
      }
      return View(tasks);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Tasks tasks = await db.Tasks.Include(t => t.Users).SingleAsync(t => t.Id == id); db.Tasks.Remove(tasks);
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