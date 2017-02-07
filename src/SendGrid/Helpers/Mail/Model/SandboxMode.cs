using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// This allows you to send a test email to ensure that your request body is valid and formatted correctly. For more information, please see our Classroom.
    /// https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/sandbox_mode.html
    /// </summary>
    public class SandboxMode
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }
    }
}