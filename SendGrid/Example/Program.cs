using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Example.Commands;

namespace Example
{
    class Program
    {
        private static String _userName = "sgrid_username";
        private static String _password = "sgrid_password";
        private static List<IConsoleCommand> _commands = null;

        static void Main(string[] args)
        {
            Console.WriteLine("This is the SendGrid examples test console.");
            InitializeCommands();

            RunGetCredentials();

            while (true)
            {
                try
                {
                    Console.Write("Pick a command to test (? for help):");
                    String cmdText = Console.ReadLine().Trim();
                    if (cmdText == "?")
                    {
                        RunHelp();
                    }
                    else if (cmdText == "exit")
                    {
                        break;
                    }
                    else
                    {
                        var cmd = _commands.Find(x => x.Name == cmdText);
                        if (cmd != null)
                        {
                            cmd.Credential = new NetworkCredential(_userName, _password);
                            cmd.Run();
                            cmd.Credential = null;
                        }
                        else
                        {
                            Console.WriteLine("Unrecognized command");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("");
                }
            }
        }

        static void InitializeCommands()
        {
            _commands = new List<IConsoleCommand>();
            _commands.Add(new Example.Commands.Blocks.BlockListConsoleCommand());
            _commands.Add(new Example.Commands.Blocks.BlockDeleteConsoleCommand());
            _commands.Add(new Example.Commands.Bounces.BounceListConsoleCommand());
            _commands.Add(new Example.Commands.Bounces.BounceDeleteConsoleCommand());
            _commands.Add(new Example.Commands.InvalidEmails.InvalidEmailListConsoleCommand());
            _commands.Add(new Example.Commands.InvalidEmails.InvalidEmailDeleteConsoleCommand());
            _commands.Add(new Example.Commands.Delivery.SendEmailConsoleCommand());
            _commands.Add(new Example.Commands.SpamReports.SpamReportListConsoleCommand());
            _commands.Add(new Example.Commands.SpamReports.SpamReportDeleteConsoleCommand());
            _commands.Add(new Example.Commands.Unsubscribes.UnsubscribeListConsoleCommand());
            _commands.Add(new Example.Commands.Unsubscribes.UnsubscribeAddConsoleCommand());
            _commands.Add(new Example.Commands.Unsubscribes.UnsubscribeDeleteConsoleCommand());
        }

        static void RunGetCredentials()
        {
            Console.WriteLine("What are your SendGrid credentials to use for this session?");
            _userName = ConsoleCommandParser.PromptString("User Name:", _userName);
            _password = ConsoleCommandParser.PromptString("Password:", _password);
            Console.WriteLine("");
        }

        static void RunHelp()
        {
            Console.WriteLine("Command List");
            Console.WriteLine("? - this help screen");
            Console.WriteLine("exit - ends the program");
            foreach (var cmd in _commands)
            {
                Console.WriteLine(String.Format("{0} - {1}", cmd.Name, cmd.HelpText));
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}