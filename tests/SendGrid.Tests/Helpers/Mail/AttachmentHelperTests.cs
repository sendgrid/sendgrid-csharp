namespace SendGrid.Tests.Mail
{
    using System.Threading.Tasks;
    using SendGrid.Helpers.Mail;
    using Xunit;
    using System.Text;

    public class AttachmentHelperTests
    {
        [Fact]
        public async void AddAttachmentThroughHelper()
        {
            var attachmentSource = new FakeAttachmentSource("testfile.txt", "text/plain", Encoding.ASCII.GetBytes("Hello"));
            var attachment = await AttachmentHelper.CreateAttachmentAsync(attachmentSource);

            Assert.True(attachment.Filename == "testfile.txt");
            Assert.True(attachment.Type == "text/plain");
            Assert.True(attachment.Content == "SGVsbG8=");
        }
    }

    public class FakeAttachmentSource : IAttachmentSource
    {
        private readonly AttachmentSource result;

        public FakeAttachmentSource(string fileName, string mimeType, byte[] data)
        {
            result = new AttachmentSource(fileName, mimeType, data);
        }

        public Task<AttachmentSource> GetAttachmentAsync()
        {
            return Task.FromResult(result);
        }
    }
}
