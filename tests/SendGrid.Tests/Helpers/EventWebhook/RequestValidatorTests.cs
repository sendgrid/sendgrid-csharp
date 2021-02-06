using Xunit;
using Newtonsoft.Json;
using SendGrid.Helpers.EventWebhook;

namespace SendGrid.Tests.Helpers.EventWebhook
{
    public class RequestValidatorTests
    {
        public class EventClass
        {
            [JsonProperty("email")]
            public string Email;

            [JsonProperty("event")]
            public string Event;

            [JsonProperty("reason")]
            public string Reason;

            [JsonProperty("sg_event_id")]
            public string SgEventId;

            [JsonProperty("sg_message_id")]
            public string SgMessageId;

            [JsonProperty("smtp-id")]
            public string SmtpId;

            [JsonProperty("timestamp")]
            public long Timestamp;
        }

        private const string PUBLIC_KEY = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE83T4O/n84iotIvIW4mdBgQ/7dAfSmpqIM8kF9mN1flpVKS3GRqe62gw+2fNNRaINXvVpiglSI8eNEc6wEA3F+g==";
        private const string SIGNATURE = "MEUCIGHQVtGj+Y3LkG9fLcxf3qfI10QysgDWmMOVmxG0u6ZUAiEAyBiXDWzM+uOe5W0JuG+luQAbPIqHh89M15TluLtEZtM=";
        private const string TIMESTAMP = "1600112502";
        private string PAYLOAD = JsonConvert.SerializeObject(new[]{
                new EventClass {
                    Email = "hello@world.com",
                    Event = "dropped",
                    Reason = "Bounced Address",
                    SgEventId = "ZHJvcC0xMDk5NDkxOS1MUnpYbF9OSFN0T0doUTRrb2ZTbV9BLTA",
                    SgMessageId = "LRzXl_NHStOGhQ4kofSm_A.filterdrecv-p3mdw1-756b745b58-kmzbl-18-5F5FC76C-9.0",
                    SmtpId = "<LRzXl_NHStOGhQ4kofSm_A@ismtpd0039p1iad1.sendgrid.net>",
                    Timestamp = 1600112492,
                }
        }) + "\r\n"; // Be sure to include the trailing carriage return and newline!

        [Fact]
        public void TestVerifySignature()
        {
            var isValidSignature = Verify(
                PUBLIC_KEY,
                PAYLOAD,
                SIGNATURE,
                TIMESTAMP
            );

            Assert.True(isValidSignature);
        }

        [Fact]
        public void TestBadKey()
        {
            var isValidSignature = Verify(
                "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEqTxd43gyp8IOEto2LdIfjRQrIbsd4SXZkLW6jDutdhXSJCWHw8REntlo7aNDthvj+y7GjUuFDb/R1NGe1OPzpA==",
                PAYLOAD,
                SIGNATURE,
                TIMESTAMP
            );

            Assert.False(isValidSignature);
        }

        [Fact]
        public void TestBadPayload()
        {
            var isValidSignature = Verify(
                PUBLIC_KEY,
                "payload",
                SIGNATURE,
                TIMESTAMP
            );

            Assert.False(isValidSignature);
        }

        [Fact]
        public void TestBadSignature()
        {
            var isValidSignature = Verify(
                PUBLIC_KEY,
                PAYLOAD,
                "MEQCIB3bJQOarffIdM7+MEee+kYAdoViz6RUoScOASwMcXQxAiAcrus/j853JUlVm5qIRfbKBJwJq89znqOTedy3RetXLQ==",
                TIMESTAMP
            );

            Assert.False(isValidSignature);
        }

        [Fact]
        public void TestBadTimestamp()
        {
            var isValidSignature = Verify(
                PUBLIC_KEY,
                PAYLOAD,
                SIGNATURE,
                "timestamp"
            );

            Assert.False(isValidSignature);
        }

        private bool Verify(string publicKey, string payload, string signature, string timestamp)
        {
            var validator = new RequestValidator();
            var ecPublicKey = validator.ConvertPublicKeyToECDSA(publicKey);
            return validator.VerifySignature(ecPublicKey, payload, signature, timestamp);
        }
    }
}
