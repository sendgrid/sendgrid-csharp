using Xunit;
using SendGrid.Helpers.EventWebbook;

namespace SendGrid.Tests.Helpers.EventWebhook
{
    public class RequestValidatorTests
    {
        private const string PUBLIC_KEY = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEEDr2LjtURuePQzplybdC+u4CwrqDqBaWjcMMsTbhdbcwHBcepxo7yAQGhHPTnlvFYPAZFceEu/1FwCM/QmGUhA==";
        private const string PAYLOAD = "{\"category\":\"example_payload\",\"event\":\"test_event\",\"message_id\":\"message_id\"}";
        private const string SIGNATURE = "MEUCIQCtIHJeH93Y+qpYeWrySphQgpNGNr/U+UyUlBkU6n7RAwIgJTz2C+8a8xonZGi6BpSzoQsbVRamr2nlxFDWYNH2j/0=";
        private const string TIMESTAMP = "1588788367";

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
                "signature",
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
