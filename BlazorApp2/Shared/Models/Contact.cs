using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApp2.Shared.Models
{
    public class Contact
    {
        [JsonIgnore]
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        public string FirstName { set; get; }

        [Required(ErrorMessage = "{0} is Required")]
        public string LastName { set; get; }

        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { set; get; }

        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage ="{0} should be 10 numbers")]
        public string PhoneNumber { set; get; }
    }
}
