using Issues.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Issues.Models
{
  public class Tasks
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Statement { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }

    [Required]
    public TaskState State { get; set; }

    public DateTime DateOfCreate { get; set; }

    public DateTime DateOfUpdate { get; set; }

    public List<ApplicationUser> Users { get; set; }
  }
}