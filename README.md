[SendGridPlus](http://github.com/AdvancedREI/SendGrid-csharp)
=================

SendGridPlus is a client library for interacting with SendGrid's Mail and Event APIs. It is a refactored and enhanced version of the SendGrid package, and is being released because SendGrid has not kept the NuGet package in sync with their repository.

AdvancedREI has refactored this library to logically support multiple APIs, and enhanced it with Event processing, to let you easily leverage the Event API in your own code.


Quick start
-----------

Install the NuGet package: `Install-Package SendGridPlus`, clone the repo: `git clone git://github.com/advancedrei/sendgrid-csharp.git`, or  [download the latest release](https://github.com/advancedrei/sendgrid-csharp/zipball/master).

The major differences between this and the official library is that the default namespace is now `SendGrid` instead of `SendGridMail`, and you get a new instance by calling `Mail.GetInstance();` instead of  `SendGrid.GenerateInstance();`

You can also easily get single or batched Events from an MVC controller with the following example:

    public class SendGridController : Controller
    {
        public ActionResult Index()
        {
            var events = Events.GetEvents(Request.InputStream);
            events.ForEach(c => DoSomething(c));
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }


Bug tracker
-----------

Have a bug? Please create an issue here on GitHub that conforms with [necolas's guidelines](https://github.com/necolas/issue-guidelines).

https://github.com/AdvancedREI/sendgrid-csharp/issues



Twitter account
---------------

Keep up to date on announcements and more by following AdvancedREI on Twitter, [@AdvancedREI](http://twitter.com/AdvancedREI).



Blog
----

Read more detailed announcements, discussions, and more on [The AdvancedREI Dev Blog](http://advancedrei.com/blogs/development).


Author
-------

**Robert McLaws**

+ http://twitter.com/robertmclaws
+ http://github.com/advancedrei


Copyright and license
---------------------

Copyright 2012 AdvancedREI, LLC, and SendGrid, Inc.

Licensed under the MIT License. See the MIT.LICENSE file for more information.