using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SendGrid.ASPSamples.Models
{
    public class EmailContract
    {
        [Required]
        [Display(Name = "From Email Address")]
        [EmailAddress]
        public string FromEmailAddress { get; set; }

        public string Alias { get; set; }

        [Required]
        [Display(Name = "To Email Address")]
        [EmailAddress]
        public string ToEmailAddress { get; set; }
        
        [Display(Name = "Cc Email Address")]
        [EmailAddress]
        public string CcEmailAddress { get; set; }
        
        [Display(Name = "Bcc Email Address")]
        [EmailAddress]
        public string BccEmailAddress { get; set; }
        
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        
        [Required]
        [Display(Name = "Body")]
        public string Body { get; set; }
    }
}
