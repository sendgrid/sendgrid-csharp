using System;
using System.Linq;

namespace SendGridMail
{
    public class Block
    {
        public String Email { get; set; }

        public String Status { get; set; }

        public String Reason { get; set; }

        public DateTime? Created { get; set; }
    }
}