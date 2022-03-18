using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mid_Assignment_Project.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } = "123456789";
        public Boolean IsSuper { get; set; } = false;
        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}