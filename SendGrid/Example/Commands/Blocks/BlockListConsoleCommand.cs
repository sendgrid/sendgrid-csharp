using System;
using System.Linq;
using System.Net;

namespace Example.Commands.Blocks
{
    public class BlockListConsoleCommand : IConsoleCommand
    {
        public NetworkCredential Credential { get; set; }

        public Boolean IncludeDate { get; set; }

        public Int32 Days { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Name
        {
            get
            {
                return "blockList";
            }
        }

        public string HelpText
        {
            get
            {
                return "list blocks";
            }
        }

        public void Run()
        {
            Console.WriteLine("Block List");
            this.IncludeDate = ConsoleCommandParser.PromptBoolean("include date (y)?", true);
            if (this.Days <= 0)
            {
                this.StartDate = ConsoleCommandParser.PromptDate("(optional) Starting date:", null);
                this.EndDate = ConsoleCommandParser.PromptDate("(optional) Ending date:", null);
            }

            this.Execute();
            Console.WriteLine();
        }

        public void Execute()
        {
            SendGridMail.WebApi.WebBlockApi api = new SendGridMail.WebApi.WebBlockApi(this.Credential);
            var items = api.GetBlocks(this.IncludeDate, this.Days, this.StartDate, this.EndDate);
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(String.Format("{0};  {1};  {2}", item.Created, item.Status, item.Email));
                    Console.WriteLine(item.Reason);
                    Console.WriteLine();
                }
                Console.WriteLine(String.Format("{0} items found.", items.Count));
            }
        }
    }
}