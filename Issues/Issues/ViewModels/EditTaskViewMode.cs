using Issues.Models;
using Issues.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Issues.ViewModels
{
    public class EditTaskViewMode
    {
        [Required]
        public Guid Id { get; set; }

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

        public string[] UsersId { get; set; }

        public SelectList UserList{ get; set; }

    }
}