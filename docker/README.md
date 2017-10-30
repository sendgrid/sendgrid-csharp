You can use Docker to easily try out or test sendgrid-csharp.

# Quickstart

1. Install Docker on your machine.
2. Build the latest container with `docker build -t sendgrid/sendgrid-csharp docker`
3. Run `docker run -it sendgrid/sendgrid-csharp`.

# Info

This Docker image contains
 - `sendgrid-csharp`
 - Stoplight's Prism, which lets you try out the API without actually sending email

Run it in interactive mode with `-it`.

You can mount a repository in the `/mnt/sendgrid-csharp` directory to use it instead of the default SendGrid library.

# Testing
Testing is easy!  Run the container, then `dotnet test ./tests/SendGrid.Tests/SendGrid.Tests.csproj -c Release -f netcoreapp1.0`

# Usage examples

- Most recent version: docker run -it sendgrid/sendgrid-csharp.
- Your own fork:
  ```sh-session
  $ git clone https://github.com/you/cool-sendgrid-csharp.git
  $ realpath cool-sendgrid-csharp
  /path/to/cool-sendgrid-csharp
  $ docker run -it -v /path/to/cool-sendgrid-csharp:/mnt/sendgrid-csharp sendgrid/sendgrid-csharp
  ```