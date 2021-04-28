using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseCore.Data.Models
{
    public class User : BaseModel
    {

        [Column(TypeName="varchar(50)")]
        public string Username { get; set; }
        [Column(TypeName="varchar(11)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName="varchar(500)")]
        public string Token { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Password { get; set; }
    }
}
