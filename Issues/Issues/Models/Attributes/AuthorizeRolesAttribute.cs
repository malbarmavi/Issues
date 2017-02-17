using System.Web.Mvc;

namespace Issues.Models.Attributes
{
  public class AuthorizeRolesAttribute : AuthorizeAttribute
  {
    public AuthorizeRolesAttribute(params string[] roles) : base()
    {
      Roles = string.Join(",", roles);
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
      if (filterContext.HttpContext.User.Identity.IsAuthenticated)
      {
        filterContext.Result = new RedirectResult("/Error/AccessDenied");
      }
      else
      {
        base.HandleUnauthorizedRequest(filterContext);
      }
    }
  }
}