using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Issues.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        public DateTime DateOfCreate { get; set; }

        public DateTime DateOfUpdate { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public List<ApplicationUser> Users { get; set; }
    }
}