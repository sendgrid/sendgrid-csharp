using System;
using System.Linq;
using System.Net;

namespace Example.Commands.SpamReports
{
    public class SpamReportListConsoleCommand : IConsoleCommand
    {
        public NetworkCredential Credential { get; set; }

        public Boolean IncludeDate { get; set; }

        public Int32 Days { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Int32 Limit { get; set; }

        public Int32 Offset { get; set; }

        public String Email { get; set; }

        public string Name
        {
            get
            {
                return "spamReportList";
            }
        }

        public string HelpText
        {
            get
            {
                return "list addresses that reported email as spam";
            }
        }

        public void Run()
        {
            Console.WriteLine("Spam Report List");
            this.IncludeDate = ConsoleCommandParser.PromptBoolean("include date (y)?", true);
            if (this.Days <= 0)
            {
                this.StartDate = ConsoleCommandParser.PromptDate("(optional) Starting date:", null);
                this.EndDate = ConsoleCommandParser.PromptDate("(optional) Ending date:", null);
            }
            this.Limit = ConsoleCommandParser.PromptInt32("(optional) Max records to return:", 0);
            this.Offset = 0;
            if (this.Limit > 0)
            {
                this.Offset = ConsoleCommandParser.PromptInt32("(optional) starting record:", 0);
            }
            this.Email = ConsoleCommandParser.PromptString("(optional) Email filter:", null);

            this.Execute();
            Console.WriteLine();
        }

        public void Execute()
        {
            SendGridMail.WebApi.WebSpamReportApi api = new SendGridMail.WebApi.WebSpamReportApi(this.Credential);
            var items = api.GetSpamReports(this.IncludeDate, this.Days, this.StartDate, this.EndDate, this.Limit, this.Offset, this.Email);
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(String.Format("{0};  {1};  {2}", item.Created, item.IP, item.Email));
                    Console.WriteLine();
                }
                Console.WriteLine(String.Format("{0} items found.", items.Count));
            }
        }
    }
}
