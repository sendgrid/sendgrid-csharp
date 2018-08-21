namespace SendGrid.Tests.Helpers.Mail
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SendGrid.Helpers.Mail;
    using Xunit;

    public class SendGridMessageTests
    {
        #region AddAttachment tests

        [Theory]
        [InlineData(null, "foo")]
        [InlineData("", "foo")]
        [InlineData("    ", "foo")]
        [InlineData("foo", null)]
        [InlineData("foo", "")]
        [InlineData("foo", "    ")]
        public void SendGridMessage_AddAttachment_Doesnt_Add_If_Filename_Or_Content_Are_Missing(string filename, string content)
        {
            // Arrange
            var sut = new SendGridMessage();

            // Act
            sut.AddAttachment(filename, content);

            // Assert
            Assert.Null(sut.Attachments);
        }

        [Theory]
        [InlineData("filename", "content", null, null, null)]
        [InlineData("filename", "content", "type", null, null)]
        [InlineData("filename", "content", "type", "disposition", null)]
        [InlineData("filename", "content", "type", "disposition", "contentId")]
        public void SendGridMessage_AddAttachment_Should_Add_Single_Attachment_To_Attachments(string filename, string content, string type, string disposition, string contentId)
        {
            // Arrange
            var sut = new SendGridMessage();

            // Act
            sut.AddAttachment(filename, content, type, disposition, contentId);

            // Assert
            Assert.Equal(1, sut.Attachments.Count);

            var attachment = sut.Attachments.First();

            Assert.Equal(filename, attachment.Filename);
            Assert.Equal(content, attachment.Content);
            Assert.Equal(type, attachment.Type);
            Assert.Equal(disposition, attachment.Disposition);
            Assert.Equal(contentId, attachment.ContentId);
        }

        public void SendGridMessage_AddAttachment_Doesnt_Touch_Attachment_Passed_In()
        {
            // Arrange
            var sut = new SendGridMessage();

            var content = "content";
            var contentId = "contentId";
            var disposition = "disposition";
            var filename = "filename";
            var type = "type";

            var attachment = new Attachment
            {
                Content = content,
                ContentId = contentId,
                Disposition = disposition,
                Filename = filename,
                Type = type
            };

            // Act
            sut.AddAttachment(attachment);

            // Assert
            Assert.Equal(1, sut.Attachments.Count);

            var addedAttachment = sut.Attachments.First();

            Assert.Same(attachment, addedAttachment);
            Assert.Equal(attachment.Content, addedAttachment.Content);
            Assert.Equal(attachment.ContentId, addedAttachment.ContentId);
            Assert.Equal(attachment.Disposition, addedAttachment.Disposition);
            Assert.Equal(attachment.Filename, addedAttachment.Filename);
            Assert.Equal(attachment.Type, addedAttachment.Type);
        }

        #endregion

        #region AddAttachments tests

        [Fact]
        public void SendGridMessage_AddAttachments_Adds_All_Attachments()
        {
            // Arrange
            var sut = new SendGridMessage();

            var attachments = new[]
            {
                new Attachment(),
                new Attachment()
            };

            // Act
            sut.AddAttachments(attachments);

            // Assert
            Assert.Equal(attachments.Length, sut.Attachments.Count);
        }

        [Fact]
        public void SendGridMessage_AddAttachments_Doesnt_Use_Provided_List_As_Property()
        {
            // Arrange
            var sut = new SendGridMessage();

            var attachments = new List<Attachment>
            {
                new Attachment(),
                new Attachment()
            };

            // Act
            sut.AddAttachments(attachments);

            // Assert
            Assert.Equal(attachments.Count(), sut.Attachments.Count);
            Assert.NotSame(attachments, sut.Attachments);
        }

        [Fact]
        public void SendGridMessage_AddAttachments_Doesnt_Touch_Attachments_Passed_In()
        {
            // Arrange
            var sut = new SendGridMessage();

            var content = "content";
            var contentId = "contentId";
            var disposition = "disposition";
            var filename = "filename";
            var type = "type";

            var attachment = new Attachment
            {
                Content = content,
                ContentId = contentId,
                Disposition = disposition,
                Filename = filename,
                Type = type
            };

            var attachments = new[] { attachment };

            // Act
            sut.AddAttachments(attachments);

            // Assert
            Assert.Equal(1, sut.Attachments.Count);

            var addedAttachment = sut.Attachments.First();

            Assert.Same(attachment, addedAttachment);
            Assert.Equal(attachment.Content, addedAttachment.Content);
            Assert.Equal(attachment.ContentId, addedAttachment.ContentId);
            Assert.Equal(attachment.Disposition, addedAttachment.Disposition);
            Assert.Equal(attachment.Filename, addedAttachment.Filename);
            Assert.Equal(attachment.Type, addedAttachment.Type);
        }

        #endregion

        #region AddAttachmentAsync tests

        [Fact]
        public async Task SendGridMessage_AddAttachmentAsync_Doesnt_Read_Non_Readable_Streams()
        {
            // Arrange 
            var sut = new SendGridMessage();

            var stream = new NonReadableStream();

            // Act
            await sut.AddAttachmentAsync(null, stream);

            // Assert
            Assert.Null(sut.Attachments);
        }

        [Fact]
        public async Task SendGridMessage_AddAttachmentAsync_Adds_Base64_Content_Of_Stream()
        {
            // Arrange
            var sut = new SendGridMessage();

            var content = "hello world";
            var contentBytes = Encoding.UTF8.GetBytes(content);
            var base64EncodedContent = Convert.ToBase64String(contentBytes);
            var stream = new MemoryStream(contentBytes);

            // Act
            await sut.AddAttachmentAsync("filename", stream);

            // Assert
            Assert.Equal(1, sut.Attachments.Count);

            var attachment = sut.Attachments.First();

            Assert.Equal(attachment.Content, base64EncodedContent);
        }

        [Theory]
        [InlineData(null, "foo")]
        [InlineData("", "foo")]
        [InlineData("    ", "foo")]
        public async Task SendGridMessage_AddAttachmentAsync_Doesnt_Add_If_Filename_Is_Missing(string filename, string content)
        {
            // Arrange
            var sut = new SendGridMessage();

            var contentBytes = Encoding.UTF8.GetBytes(content);
            var base64EncodedContent = Convert.ToBase64String(contentBytes);
            var contentStream = new MemoryStream(contentBytes);

            // Act
            await sut.AddAttachmentAsync(filename, contentStream);

            // Assert
            Assert.Null(sut.Attachments);
        }

        [Fact]
        public async Task SendGridMessage_AddAttachmentAsync_Doesnt_Add_If_Content_Stream_Is_Missing()
        {
            // Arrange
            var sut = new SendGridMessage();

            Stream contentStream = null;

            // Act
            await sut.AddAttachmentAsync("filename", contentStream);

            // Assert
            Assert.Null(sut.Attachments);
        }

        [Theory]
        [InlineData("filename", "content", null, null, null)]
        [InlineData("filename", "content", "type", null, null)]
        [InlineData("filename", "content", "type", "disposition", null)]
        [InlineData("filename", "content", "type", "disposition", "contentId")]
        public async Task SendGridMessage_AddAttachmentAsync_Should_Add_Single_Attachment_To_Attachments(string filename, string content, string type, string disposition, string contentId)
        {
            // Arrange
            var sut = new SendGridMessage();

            var contentBytes = Encoding.UTF8.GetBytes(content);
            var base64EncodedContent = Convert.ToBase64String(contentBytes);
            var contentStream = new MemoryStream(contentBytes);

            // Act
            await sut.AddAttachmentAsync(filename, contentStream, type, disposition, contentId);

            // Assert
            Assert.Equal(1, sut.Attachments.Count);

            var addedAttachment = sut.Attachments.First();

            Assert.Equal(base64EncodedContent, addedAttachment.Content);
            Assert.Equal(contentId, addedAttachment.ContentId);
            Assert.Equal(disposition, addedAttachment.Disposition);
            Assert.Equal(filename, addedAttachment.Filename);
            Assert.Equal(type, addedAttachment.Type);
        }

        #endregion
    }
}
