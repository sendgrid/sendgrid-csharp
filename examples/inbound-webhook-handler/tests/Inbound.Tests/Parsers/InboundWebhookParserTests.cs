using Inbound.Parsers;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Inbound.Tests.Parsers
{
    public class InboundWebhookParserTests
    {
        [Fact]
        public async Task DefaultPayloadWithoutAttachments()
        {
            var stream = File.OpenRead("sample_data/default_data.txt");
            
            var inboundEmail = await InboundWebhookParser.ParseAsync(stream);

            inboundEmail.ShouldNotBeNull();

            inboundEmail.Headers.Except(new[] {
                new KeyValuePair<string, string>("MIME-Version","1.0"),
                new KeyValuePair<string, string>("Received","by 0.0.0.0 with HTTP; Wed, 10 Aug 2016 18:10:13 -0700 (PDT)"),
                new KeyValuePair<string, string>("From","Example User <test@example.com>"),
                new KeyValuePair<string, string>("Date","Wed, 10 Aug 2016 18:10:13 -0700"),
                new KeyValuePair<string, string>("Subject","Inbound Parse Test Data"),
                new KeyValuePair<string, string>("To","inbound@inbound.example.com"),
                new KeyValuePair<string, string>("Content-Type","multipart/alternative; boundary=001a113df448cad2d00539c16e89")
            }).Count().ShouldBe(0);

            inboundEmail.Dkim.ShouldBe("{@sendgrid.com : pass}");

            inboundEmail.To[0].Email.ShouldBe("inbound@inbound.example.com");
            inboundEmail.To[0].Name.ShouldBe(string.Empty);

            inboundEmail.Html.Trim().ShouldBe("<html><body><strong>Hello SendGrid!</body></html>");

            inboundEmail.Text.Trim().ShouldBe("Hello SendGrid!");

            inboundEmail.From.Email.ShouldBe("test@example.com");
            inboundEmail.From.Name.ShouldBe("Example User");

            inboundEmail.SenderIp.ShouldBe("0.0.0.0");

            inboundEmail.SpamReport.ShouldBeNull();

            inboundEmail.Envelope.From.ShouldBe("test@example.com");
            inboundEmail.Envelope.To.Length.ShouldBe(1);
            inboundEmail.Envelope.To.ShouldContain("inbound@inbound.example.com");

            inboundEmail.Attachments.Length.ShouldBe(0);

            inboundEmail.Subject.ShouldBe("Testing non-raw");

            inboundEmail.SpamScore.ShouldBeNull();

            inboundEmail.Charsets.Except(new[] {
                new KeyValuePair<string, Encoding>("to", Encoding.UTF8),
                new KeyValuePair<string, Encoding>("html", Encoding.UTF8),
                new KeyValuePair<string, Encoding>("subject", Encoding.UTF8),
                new KeyValuePair<string, Encoding>("from", Encoding.UTF8),
                new KeyValuePair<string, Encoding>("text", Encoding.UTF8)
            }).Count().ShouldBe(0);
            
            inboundEmail.Spf.ShouldBe("pass");
        }

        [Fact]
        public async Task RawPayloadWithAttachments()
        {
            var stream = File.OpenRead("sample_data/raw_data_with_attachments.txt");
            
            var inboundEmail = await InboundWebhookParser.ParseAsync(stream);

            inboundEmail.ShouldNotBeNull();

            inboundEmail.Dkim.ShouldBe("{@sendgrid.com : pass}");

            var rawEmailTestData = await File.ReadAllTextAsync("sample_data/raw_email_with_attachments.txt");
            inboundEmail.RawEmail.Trim().ShouldBe(rawEmailTestData);

            inboundEmail.To[0].Email.ShouldBe("inbound@inbound.example.com");
            inboundEmail.To[0].Name.ShouldBe(string.Empty);

            inboundEmail.Cc.Length.ShouldBe(0);

            inboundEmail.From.Email.ShouldBe("test@example.com");
            inboundEmail.From.Name.ShouldBe("Example User");

            inboundEmail.SenderIp.ShouldBe("0.0.0.0");

            inboundEmail.SpamReport.ShouldBeNull();

            inboundEmail.Envelope.From.ShouldBe("test@example.com");
            inboundEmail.Envelope.To.Length.ShouldBe(1);
            inboundEmail.Envelope.To.ShouldContain("inbound@inbound.example.com");

            inboundEmail.Subject.ShouldBe("Raw Payload");

            inboundEmail.SpamScore.ShouldBeNull();

            inboundEmail.Charsets.Except(new[] {
                new KeyValuePair<string, Encoding>("to", Encoding.UTF8),
                new KeyValuePair<string, Encoding>("subject", Encoding.UTF8),
                new KeyValuePair<string, Encoding>("from", Encoding.UTF8)
            }).Count().ShouldBe(0);

            inboundEmail.Spf.ShouldBe("pass");
        }
    }
}
