using Issues.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Issues.ViewModels
{
  public class NewTaskViewMode
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [Required]
    public string Statement { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }

    [Required]
    public TaskState State { get; set; }

    [Display(Name = "Users")]
    public string[] UsersId { get; set; }

    public SelectList UserList { get; set; }
  }
}