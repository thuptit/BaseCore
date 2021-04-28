using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Common.Models.Users
{
    public class UserDetailOutputModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedDate { get; set; }
        public int IsActive { get; set; }
    }
}
