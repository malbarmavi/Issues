using System;
using System.ComponentModel.DataAnnotations;

namespace Issues.ViewModels
{
  public class UserDetailsViewModel
  {
    public string Id { get; set; }

    public string Email { get; set; }

    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Display(Name = "Last name")]
    public string LastName { get; set; }

    public string Address { get; set; }

    public bool Gender { get; set; }

    public string Job { get; set; }

    [Display(Name = "Date Of create")]
    public DateTime DateOfCreate { get; set; }

    [Display(Name = "Last update")]
    public DateTime DateOfUpdate { get; set; }

    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }
  }
}