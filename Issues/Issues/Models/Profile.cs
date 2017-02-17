using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Issues.Models
{
  public class Profile
  {
    [Key, ForeignKey("User")]
    public string Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(150)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(250)]
    public string Address { get; set; }

    [Required]
    public bool Gender { get; set; }

    [Required]
    [MaxLength(150)]
    public string Job { get; set; }

    public DateTime DateOfCreate { get; set; }

    public DateTime DateOfUpdate { get; set; }

    public string PhoneNumber { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }

    public ApplicationUser User { get; set; }
  }
}