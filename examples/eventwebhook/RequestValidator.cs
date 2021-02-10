using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.EventWebhook;
using System.IO;

public bool IsValidSignature(HttpRequest request)
{
    var publicKey = "base64-encoded public key";
    string requestBody;

    using (var reader = new StreamReader(request.Body))
    {
        requestBody = reader.ReadToEnd();
    }

    var validator = new RequestValidator();
    var ecPublicKey = validator.ConvertPublicKeyToECDSA(publicKey);

    return validator.VerifySignature(
        ecPublicKey,
        requestBody,
        request.Headers[RequestValidator.SIGNATURE_HEADER],
        request.Headers[RequestValidator.TIMESTAMP_HEADER]
    );
}
