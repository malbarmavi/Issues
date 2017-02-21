using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Issues.ViewModels
{
  public class NewProjectViewModel
  {
      [Required]
      [MaxLength(100)]
      public string Name { get; set; }

      [Required]
      [MaxLength(250)]
      public string Description { get; set; }

      [Timestamp]
      public byte[] Version { get; set; }
    }
  }
