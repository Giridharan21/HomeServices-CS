using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class reviewmodel
    {
        public int OrderIdFK { get; set; }
        [Required(ErrorMessage ="Custom Required Field")]
        [Range(1.0,5.0,ErrorMessage ="Enter the number between 1 to 5\n")]
        public decimal Stars { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "comments cannot be longer than 256 charaters\n")]
        public string Comment { get; set; }


    }
}
