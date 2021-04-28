using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseCore.Data.Models
{
    public class BaseModel
    {
        #region Properties
        [Key]
        [Required]
        public int Id { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "int")]
        public int IsActive { get; set; }
        #endregion
    }
}
