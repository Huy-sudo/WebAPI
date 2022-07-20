using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Img { get; set; }
        public int Title { get; set; }
        public string? Gender { get; set; }
        public string Email { get; set; } = null!;
        public string Organization { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Status { get; set; }

        public virtual Title? TitleNavigation { get; set; } = null!;
    }
}
