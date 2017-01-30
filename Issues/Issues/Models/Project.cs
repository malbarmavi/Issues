using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Issues.Models
{
  public class Project
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Description { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }

    public DateTime DateOfCreate { get; set; }

    public DateTime DateOfUpdate { get; set; }

    public List<Tasks> Tasks { get; set; }

    public Company Company { get; set; }

    public int CompanyId { get; set; }


  }
}