using Issues.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Issues.Controllers
{
  public class ProfileController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    [HttpGet]
    public async Task<ActionResult> Index()
    {
      string currentUserId = User.Identity.GetUserId();
      Profile currentUserProfile = await db.Profile.FindAsync(currentUserId);

      return View(currentUserProfile);
    }

    [HttpGet]
    public async Task<ActionResult> Edit()
    {
      string currentUserId = User.Identity.GetUserId();
      Profile currentUserProfile = await db.Profile.FindAsync(currentUserId);

      return View(currentUserProfile);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(Profile model)
    {
      if (ModelState.IsValid)
      {
        string currentUserId = User.Identity.GetUserId();
        Profile currentUserProfile = await db.Profile.FindAsync(currentUserId);

        currentUserProfile.DateOfUpdate = DateTime.Now;
        currentUserProfile.Address = model.Address;
        currentUserProfile.FirstName = model.FirstName;
        currentUserProfile.LastName = model.LastName;
        currentUserProfile.PhoneNumber = model.PhoneNumber;
        currentUserProfile.Job = model.Job;
        currentUserProfile.Gender = model.Gender;

        db.Entry(currentUserProfile).State = EntityState.Modified;
        await db.SaveChangesAsync();

        return RedirectToAction("Index");
      }

      return View(model);
    }
  }
}