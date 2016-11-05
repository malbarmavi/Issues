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


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = await db.Tasks.FindAsync(id);
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
          

           

            return View(new NewTaskViewMode() { UserList = new SelectList(users, "Id", "Name")});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewTaskViewMode model)
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
                    Users = users
                };

                db.Tasks.Add(task);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            var usersList = db.Users
              .Include(u => u.Profile).ToList()
              .Select(u => new { Id = u.Id, Name = $"{u.Profile.FirstName} {u.Profile.LastName}" })
              .ToList();

            model.UserList = new SelectList(usersList, "Id", "Name",model.UsersId);

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
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

            EditTaskViewMode model = new EditTaskViewMode()
            {
                Id = task.Id,
                Name = task.Name,
                State = task.State,
                Statement = task.Statement,
                UsersId =taskUsersId,
                UserList = new SelectList(users, "Id", "Name", taskUsersId)
            };


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditTaskViewMode model)
        {
            if (ModelState.IsValid)
            {
                Tasks task = await db.Tasks.Include(testc => testc.Users).SingleAsync(t => t.Id == model.Id);

                if (task == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                task.Name = model.Name;
                task.State = model.State;
                task.Statement = model.Statement;
                task.Users = db.Users.Where(u => model.UsersId.Contains(u.Id)).ToList();
                task.DateOfUpdate = DateTime.Now;

                db.Entry(task).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
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
        public async Task<ActionResult> DeleteConfirmed(Guid? id)
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
