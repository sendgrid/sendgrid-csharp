using EllipticCurve;

namespace SendGrid.Helpers.EventWebbook
{
    /// <summary>
    /// This class allows you to use the Event Webhook feature. Read the docs for
    /// more details: https://sendgrid.com/docs/for-developers/tracking-events/event
    /// </summary>
    public class RequestValidator
    {
        /// <summary>
        /// Signature verification HTTP header name for the signature being sent.
        /// </summary>
        public const string SIGNATURE_HEADER = "X-Twilio-Email-Event-Webhook-Signature";

        /// <summary>
        /// Timestamp http header name for timestamp.
        /// </summary>
        public const string TIMESTAMP_HEADER = "X-Twilio-Email-Event-Webhook-Timestamp";

        /**
        * Convert the public key string to a ECPublicKey.
        *
        * @param string $publicKey verification key under Mail Settings
        * @return PublicKey public key using the ECDSA algorithm
        */
        /// <summary>
        /// Convert the public key string to a <see cref="PublicKey"/>ECPublicKey.
        /// </summary>
        /// <param name="publicKey">verification key under Mail Settings</param>
        /// <returns>public key using the ECDSA algorithm</returns>
        public PublicKey ConvertPublicKeyToECDSA(string publicKey)
        {
            return PublicKey.fromPem(publicKey);
        }

        /// <summary>
        /// Verify signed event webhook requests.
        /// </summary>
        /// <param name="publicKey">elliptic curve public key</param>
        /// <param name="payload">event payload in the request body</param>
        /// <param name="signature">value obtained from the 'X-Twilio-Email-Event-Webhook-Signature' header</param>
        /// <param name="timestamp">value obtained from the 'X-Twilio-Email-Event-Webhook-Timestamp' header</param>
        /// <returns>true or false if signature is valid</returns>
        public bool VerifySignature(PublicKey publicKey, string payload, string signature, string timestamp)
        {
            var timestampedPayload = timestamp + payload;
            var decodedSignature = Signature.fromBase64(signature);

            return Ecdsa.verify(timestampedPayload, decodedSignature, publicKey);
        }
    }
}
