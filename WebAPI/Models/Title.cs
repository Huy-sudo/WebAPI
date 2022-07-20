using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Title
    {
        public Title()
        {
            Users = new HashSet<User>();
        }

        public int TitleId { get; set; }
        public string TitleName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
