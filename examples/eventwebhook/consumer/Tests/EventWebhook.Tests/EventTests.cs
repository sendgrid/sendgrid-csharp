using EventWebhook.Models;
using EventWebhook.Parser;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EventWebhook.Tests
{
    public class EventTests
    {
        [Fact]
        public async Task AllEvents()
        {
            var jsonStream = new MemoryStream(File.ReadAllBytes("TestData/events.json"));
            
            IEnumerable<Event> events = await EventParser.ParseAsync(jsonStream);

            events.Count().ShouldBe(11);
        }

        [Fact]
        public async Task ProcessedEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'processed',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id'
                      }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var processedEvent = events.Single();
            processedEvent.Email.ShouldBe("example@test.com");
            processedEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            processedEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            processedEvent.EventType.ShouldBe(EventType.Processed);
            processedEvent.Category.Value[0].ShouldBe("cat facts");
            processedEvent.SendGridEventId.ShouldBe("sg_event_id");
            processedEvent.SendGridMessageId.ShouldBe("sg_message_id");
        }

        [Fact]
        public async Task DefferedEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'deferred',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id',
                        'response': '400 try again later',
                        'attempt': '5'
                    }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var defferedEvent = (DeferredEvent)events.Single();
            defferedEvent.Email.ShouldBe("example@test.com");
            defferedEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            defferedEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            defferedEvent.EventType.ShouldBe(EventType.Deferred);
            defferedEvent.Category.Value[0].ShouldBe("cat facts");
            defferedEvent.SendGridEventId.ShouldBe("sg_event_id");
            defferedEvent.SendGridMessageId.ShouldBe("sg_message_id");
            defferedEvent.Response.ShouldBe("400 try again later");
            defferedEvent.Attempt.ShouldBe(5);
        }

        [Fact]
        public async Task DeleveredEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'delivered',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id',
                        'response': '200 OK'
                    }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var defferedEvent = (DeliveredEvent)events.Single();
            defferedEvent.Email.ShouldBe("example@test.com");
            defferedEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            defferedEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            defferedEvent.EventType.ShouldBe(EventType.Delivered);
            defferedEvent.Category.Value[0].ShouldBe("cat facts");
            defferedEvent.SendGridEventId.ShouldBe("sg_event_id");
            defferedEvent.SendGridMessageId.ShouldBe("sg_message_id");
            defferedEvent.Response.ShouldBe("200 OK");
        }

        [Fact]
        public async Task OpenEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'open',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id',
                        'useragent': 'Mozilla/4.0 (compatible; MSIE 6.1; Windows XP; .NET CLR 1.1.4322; .NET CLR 2.0.50727)',
                        'ip': '255.255.255.255'
                    }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var openEvent = (OpenEvent)events.Single();
            openEvent.Email.ShouldBe("example@test.com");
            openEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            openEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            openEvent.EventType.ShouldBe(EventType.Open);
            openEvent.Category.Value[0].ShouldBe("cat facts");
            openEvent.SendGridEventId.ShouldBe("sg_event_id");
            openEvent.SendGridMessageId.ShouldBe("sg_message_id");
            openEvent.UserAgent.ShouldBe("Mozilla/4.0 (compatible; MSIE 6.1; Windows XP; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
            openEvent.IP.ShouldBe("255.255.255.255");

        }

        [Fact]
        public async Task ClickEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'click',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id',
                        'useragent': 'Mozilla/4.0 (compatible; MSIE 6.1; Windows XP; .NET CLR 1.1.4322; .NET CLR 2.0.50727)',
                        'ip': '255.255.255.255',
                        'url': 'http://www.sendgrid.com/'
                    }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var clickEvent = (ClickEvent)events.Single();
            clickEvent.Email.ShouldBe("example@test.com");
            clickEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            clickEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            clickEvent.EventType.ShouldBe(EventType.Click);
            clickEvent.Category.Value[0].ShouldBe("cat facts");
            clickEvent.SendGridEventId.ShouldBe("sg_event_id");
            clickEvent.SendGridMessageId.ShouldBe("sg_message_id");
            clickEvent.UserAgent.ShouldBe("Mozilla/4.0 (compatible; MSIE 6.1; Windows XP; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
            clickEvent.IP.ShouldBe("255.255.255.255");
            clickEvent.Url.ToString().ShouldBe("http://www.sendgrid.com/");
            

        }

        [Fact]
        public async Task BounceEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'bounce',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id',
                        'reason': '500 unknown recipient',
                        'status': '5.0.0'
                      }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var bounceEvent = (BounceEvent)events.Single();
            bounceEvent.Email.ShouldBe("example@test.com");
            bounceEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            bounceEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            bounceEvent.EventType.ShouldBe(EventType.Bounce);
            bounceEvent.Category.Value[0].ShouldBe("cat facts");
            bounceEvent.SendGridEventId.ShouldBe("sg_event_id");
            bounceEvent.SendGridMessageId.ShouldBe("sg_message_id");
            bounceEvent.Reason.ShouldBe("500 unknown recipient");
            bounceEvent.Status.ShouldBe("5.0.0");
        }

        [Fact]
        public async Task DroppedEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'dropped',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id',
                        'reason': 'Bounced Address',
                        'status': '5.0.0'
                      }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var droppedEvent = (DroppedEvent)events.Single();
            droppedEvent.Email.ShouldBe("example@test.com");
            droppedEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            droppedEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            droppedEvent.EventType.ShouldBe(EventType.Dropped);
            droppedEvent.Category.Value[0].ShouldBe("cat facts");
            droppedEvent.SendGridEventId.ShouldBe("sg_event_id");
            droppedEvent.SendGridMessageId.ShouldBe("sg_message_id");
            droppedEvent.Reason.ShouldBe("Bounced Address");
            droppedEvent.Status.ShouldBe("5.0.0");
        }

        [Fact]
        public async Task SpamReportEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'spamreport',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id'
                      }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var spamReportEvent = (SpamReportEvent)events.Single();
            spamReportEvent.Email.ShouldBe("example@test.com");
            spamReportEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            spamReportEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            spamReportEvent.EventType.ShouldBe(EventType.SpamReport);
            spamReportEvent.Category.Value[0].ShouldBe("cat facts");
            spamReportEvent.SendGridEventId.ShouldBe("sg_event_id");
            spamReportEvent.SendGridMessageId.ShouldBe("sg_message_id");
        }

        [Fact]
        public async Task UnsubscribeEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'unsubscribe',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id'
                      }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var spamReportEvent = (UnsubscribeEvent)events.Single();
            spamReportEvent.Email.ShouldBe("example@test.com");
            spamReportEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            spamReportEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            spamReportEvent.EventType.ShouldBe(EventType.Unsubscribe);
            spamReportEvent.Category.Value[0].ShouldBe("cat facts");
            spamReportEvent.SendGridEventId.ShouldBe("sg_event_id");
            spamReportEvent.SendGridMessageId.ShouldBe("sg_message_id");
        }

        [Fact]
        public async Task GroupUnsubscribeEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'group_unsubscribe',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id',
                        'useragent': 'Mozilla/4.0 (compatible; MSIE 6.1; Windows XP; .NET CLR 1.1.4322; .NET CLR 2.0.50727)',
                        'ip': '255.255.255.255',
                        'url': 'http://www.sendgrid.com/',
                        'asm_group_id': 10
                      }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var groupUnSubscribeEvent = (GroupUnsubscribeEvent)events.Single();
            groupUnSubscribeEvent.Email.ShouldBe("example@test.com");
            groupUnSubscribeEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            groupUnSubscribeEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            groupUnSubscribeEvent.EventType.ShouldBe(EventType.GroupUnsubscribe);
            groupUnSubscribeEvent.Category.Value[0].ShouldBe("cat facts");
            groupUnSubscribeEvent.SendGridEventId.ShouldBe("sg_event_id");
            groupUnSubscribeEvent.SendGridMessageId.ShouldBe("sg_message_id");
            groupUnSubscribeEvent.UserAgent.ShouldBe("Mozilla/4.0 (compatible; MSIE 6.1; Windows XP; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
            groupUnSubscribeEvent.IP.ShouldBe("255.255.255.255");
            groupUnSubscribeEvent.Url.ToString().ShouldBe("http://www.sendgrid.com/");
            groupUnSubscribeEvent.AsmGroupId.ShouldBe(10);
        }

        [Fact]
        public async Task GroupResubscribeEventTest()
        {
            var json = @"
                [
                    {
                        'email': 'example@test.com',
                        'timestamp': 1513299569,
                        'smtp-id': '<14c5d75ce93.dfd.64b469@ismtpd-555>',
                        'event': 'group_resubscribe',
                        'category': 'cat facts',
                        'sg_event_id': 'sg_event_id',
                        'sg_message_id': 'sg_message_id',
                        'useragent': 'Mozilla/4.0 (compatible; MSIE 6.1; Windows XP; .NET CLR 1.1.4322; .NET CLR 2.0.50727)',
                        'ip': '255.255.255.255',
                        'url': 'http://www.sendgrid.com/',
                        'asm_group_id': 10
                      }
                ]
            ";
            IEnumerable<Event> events = await EventParser.ParseAsync(json);
            events.Count().ShouldBe(1);
            var groupUnSubscribeEvent = (GroupResubscribeEvent)events.Single();
            groupUnSubscribeEvent.Email.ShouldBe("example@test.com");
            groupUnSubscribeEvent.Timestamp.ShouldBe(DateTime.UnixEpoch.AddSeconds(1513299569));
            groupUnSubscribeEvent.SmtpId.ShouldBe("<14c5d75ce93.dfd.64b469@ismtpd-555>");
            groupUnSubscribeEvent.EventType.ShouldBe(EventType.GroupResubscribe);
            groupUnSubscribeEvent.Category.Value[0].ShouldBe("cat facts");
            groupUnSubscribeEvent.SendGridEventId.ShouldBe("sg_event_id");
            groupUnSubscribeEvent.SendGridMessageId.ShouldBe("sg_message_id");
            groupUnSubscribeEvent.UserAgent.ShouldBe("Mozilla/4.0 (compatible; MSIE 6.1; Windows XP; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
            groupUnSubscribeEvent.IP.ShouldBe("255.255.255.255");
            groupUnSubscribeEvent.Url.ToString().ShouldBe("http://www.sendgrid.com/");
            groupUnSubscribeEvent.AsmGroupId.ShouldBe(10);
        }
    }
}
