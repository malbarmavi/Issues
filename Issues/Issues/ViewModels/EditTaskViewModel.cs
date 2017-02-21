using Issues.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Issues.ViewModels
{
  public class EditTaskViewModel
  {
    [Required]
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

    [Display(Name = "Projects")]
    public int ProjectId { get; set; }

    public SelectList ProjectsList { get; set; }

    public string[] UsersId { get; set; }

    public SelectList UserList { get; set; }

  }
}