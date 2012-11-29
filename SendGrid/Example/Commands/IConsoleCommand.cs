using System;
using System.Linq;
using System.Net;

namespace Example.Commands
{
    /// <summary>
    /// Describes a command run within a console application.
    /// </summary>
    public interface IConsoleCommand
    {
        /// <summary>
        /// Gets or sets the credential to use during invokation of this command.
        /// </summary>
        NetworkCredential Credential { get; set; }
        /// <summary>
        /// Gets the name of the command used to begin execution.
        /// </summary>
        String Name { get; }
        /// <summary>
        /// Gets the helptext to describe the command.
        /// </summary>
        String HelpText { get; }
        /// <summary>
        /// Begins running the command on the console by collecting additional inputs and calling Execute.
        /// </summary>
        void Run();
        /// <summary>
        /// Takes the supplied inputs and generates the result.
        /// </summary>
        void Execute();
    }
}