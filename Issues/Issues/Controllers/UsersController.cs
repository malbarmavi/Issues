using Issues.Models;
using Issues.Models.Attributes;
using Issues.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Issues.Controllers
{
  [AuthorizeRoles(Roles.Admin, Roles.Manager)]
  public class UsersController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    public async Task<ActionResult> Index()
    {
      var applicationUsers = db.Users.Include(u => u.Tasks).Include(u => u.Profile);
      return View(await applicationUsers.ToListAsync());
    }

    // GET: Users/Details/5
    public async Task<ActionResult> Details(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ApplicationUser applicationUser = await db.Users.Include(u => u.Profile).SingleAsync(u => u.Id == id);

      if (applicationUser == null)
      {
        return HttpNotFound();
      }

      UserDetailsViewModel user = new UserDetailsViewModel
      {
        Id = applicationUser.Id,
        Email = applicationUser.Email,
        FirstName = applicationUser.Profile.FirstName,
        LastName = applicationUser.Profile.LastName,
        Address = applicationUser.Profile.Address,
        Gender = applicationUser.Profile.Gender,
        Job = applicationUser.Profile.Job,
        PhoneNumber = applicationUser.Profile.PhoneNumber,
        DateOfCreate = applicationUser.Profile.DateOfCreate,
        DateOfUpdate = applicationUser.Profile.DateOfUpdate
      };
      return View(user);
    }

    // GET: Users/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Users/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(NewUserViewModel model)
    {
      if (ModelState.IsValid)
      {
        string currentUserId = User.Identity.GetUserId();
        int companyId = db.Users.Single(a => a.Id == currentUserId).CompanyId; //TODO Save Campny id in session when during t he login
        var user = new ApplicationUser
        {
          UserName = model.Email,
          Email = model.Email,
          CompanyId = companyId,
          Profile = new Profile()
          {
            DateOfCreate = DateTime.Now,
            DateOfUpdate = DateTime.Now,
            Job = model.Job,
            Address = "Not Set",
            FirstName = model.FirstName,
            LastName = model.LastName,
            Gender = model.Gender,
            PhoneNumber = "Not Set"
          }
        };
        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          userManager.AddToRole(user.Id, Roles.Staff);
          return RedirectToAction("Index");
        }
      }

      return View(model);
    }

    // GET: Users/Edit/5
    public async Task<ActionResult> Edit(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ApplicationUser applicationUser = await db.Users.Include(u => u.Profile).FirstAsync(u => u.Id == id);

      if (applicationUser == null)
      {
        return HttpNotFound();
      }

      EditUserViewModel user = new EditUserViewModel
      {
        Id = applicationUser.Id,
        Email = applicationUser.Email,
        FirstName = applicationUser.Profile.FirstName,
        LastName = applicationUser.Profile.LastName,
        Gender = applicationUser.Profile.Gender,
        Job = applicationUser.Profile.Job
      };

      return View(user);
    }

    // POST: Users/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(EditUserViewModel model, string id)
    {
      if (ModelState.IsValid)
      {
        if (model.Id != id)
        {
          Response.StatusCode = (int)HttpStatusCode.BadRequest;
          return Content(nameof(HttpStatusCode.BadRequest));
        }
        ApplicationUser user = await db.Users.Include(u => u.Profile).SingleAsync(u => u.Id == id);

        user.Email = model.Email;
        user.UserName = model.Email;
        user.Profile.FirstName = model.FirstName;
        user.Profile.LastName = model.LastName;
        user.Profile.Job = model.Job;
        user.Profile.Gender = model.Gender;

        db.Entry(user).Property(u => u.Email).IsModified = true;

        db.Entry(user.Profile).Property(u => u.FirstName).IsModified = true;
        db.Entry(user.Profile).Property(u => u.LastName).IsModified = true;
        db.Entry(user.Profile).Property(u => u.Job).IsModified = true;
        db.Entry(user.Profile).Property(u => u.Gender).IsModified = true;

        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }

      return View(model);
    }

    // GET: Users/Delete/5
    public async Task<ActionResult> Delete(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ApplicationUser applicationUser = await db.Users.Include(u => u.Profile).SingleAsync(u => u.Id == id);
      if (applicationUser == null)
      {
        return HttpNotFound();
      }

      UserDetailsViewModel user = new UserDetailsViewModel
      {
        Email = applicationUser.Email,
        FirstName = applicationUser.Profile.FirstName,
        LastName = applicationUser.Profile.LastName,
        Address = applicationUser.Profile.Address,
        Gender = applicationUser.Profile.Gender,
        Job = applicationUser.Profile.Job,
        PhoneNumber = applicationUser.Profile.PhoneNumber,
        DateOfCreate = applicationUser.Profile.DateOfCreate,
        DateOfUpdate = applicationUser.Profile.DateOfUpdate
      };

      return View(user);
    }

    // POST: Users/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(string id)
    {
      ApplicationUser applicationUser = await db.Users.Include(u => u.Profile).SingleAsync(u => u.Id == id);
      db.Users.Remove(applicationUser);
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