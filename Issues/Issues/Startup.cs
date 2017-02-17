using Issues.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Issues.Startup))]

namespace Issues
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
      CreateRols();
    }

    private void CreateRols()
    {
      ApplicationDbContext db = new ApplicationDbContext();
      var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

      if (!roleManager.RoleExists(nameof(Roles.Manager)))
      {
        var role = new IdentityRole();
        role.Name = nameof(Roles.Manager);
        roleManager.Create(role);
      }

      if (!roleManager.RoleExists(nameof(Roles.Admin)))
      {
        var role = new IdentityRole();
        role.Name = nameof(Roles.Admin);
        roleManager.Create(role);
      }

      if (!roleManager.RoleExists(nameof(Roles.Staff)))
      {
        var role = new IdentityRole();
        role.Name = nameof(Roles.Staff);
        roleManager.Create(role);
      }
    }
  }
}