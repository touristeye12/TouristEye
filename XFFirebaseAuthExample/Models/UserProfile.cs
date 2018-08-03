using System;
using System.Collections.Generic;
using System.Text;

namespace TouristEye.Models
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string UpdatedAt { get; set; }
        public string CreatedAt { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
