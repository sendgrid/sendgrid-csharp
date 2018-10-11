using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using Xunit;

namespace SendGrid.Tests
{
    [CollectionDefinition(nameof(TemplateDataSerialisationTests), DisableParallelization = true)]
    public class TemplateDataSerialisationTests
    {
        /// <summary>
        /// Tests the conditions in issue #469.
        /// JSON sent to SendGrid should never include reference handling ($id & $ref)
        /// </summary>
        /// <returns></returns>
        [Theory]
        [InlineData("$id")]
        [InlineData("$ref")]
        public void TestJsonNetReferenceHandling(string referenceHandlingProperty)
        {
            /* ****************************************************************************************
             * Enable JSON.Net reference handling globally
             * **************************************************************************************** */
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            };

            /* ****************************************************************************************
             * Create message with all possible properties
             * **************************************************************************************** */
            var msg = new SendGridMessage();

            msg.SetBccSetting(true, "test@example.com");
            msg.SetBypassListManagement(false);
            msg.SetClickTracking(true, true);
            msg.SetFooterSetting(true, "<strong>footer</strong>", "footer");
            msg.SetFrom(new EmailAddress("test@example.com"));
            msg.SetGlobalSendAt(0);
            msg.SetGlobalSubject("globalSubject");
            msg.SetGoogleAnalytics(true);
            msg.SetIpPoolName("ipPoolName");
            msg.SetOpenTracking(true, "substituteTag");
            msg.SetReplyTo(new EmailAddress("test@example.com"));
            msg.SetSandBoxMode(true);
            msg.SetSendAt(0);
            msg.SetSpamCheck(true);
            msg.SetSubject("Hello World from the SendGrid CSharp Library");
            msg.SetSubscriptionTracking(true);
            msg.SetTemplateId("templateID");

            msg.AddAttachment("balance_001.pdf",
                              "TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBwdW12",
                              "application/pdf",
                              "attachment",
                              "Balance Sheet");
            msg.AddBcc("test@example.com");
            msg.AddCategory("category");
            msg.AddCc("test@example.com");
            msg.AddContent(MimeType.Html, "HTML content");
            msg.AddCustomArg("customArgKey", "customArgValue");
            msg.AddGlobalCustomArg("globalCustomArgKey", "globalCustomValue");
            msg.AddGlobalHeader("globalHeaderKey", "globalHeaderValue");
            msg.AddHeader("headerKey", "headerValue");
            msg.AddSection("sectionKey", "sectionValue");
            msg.AddSubstitution("substitutionKey", "substitutionValue");
            msg.AddTo(new EmailAddress("test@example.com"));

            /* ****************************************************************************************
             * Serialize & check
             * **************************************************************************************** */
            string serializedMessage = msg.Serialize();
            bool containsReferenceHandlingProperty = serializedMessage.Contains(referenceHandlingProperty);
            Assert.False(containsReferenceHandlingProperty);
        }

        [Fact]
        public void TestSetTemplateDataWorksWithSpecifiedJsonPropertyNames()
        {
            var msg = new SendGridMessage();

            var dynamicTemplateData = new TestTemplateData
            {
                MyCamelCaseProperty = "camelCase",
                MyKebabCaseProperty = "kebab-case",
                MyPascalCaseProperty = "PascalCase",
                MySnakeCaseProperty = "snake_case",
            };

            msg.SetTemplateData(dynamicTemplateData);
            Assert.Equal("{\"personalizations\":[{\"dynamic_template_data\":{\"myCamelCaseProperty\":\"camelCase\",\"my-kebab-case-property\":\"kebab-case\",\"MyPascalCaseProperty\":\"PascalCase\",\"my_snake_case_property\":\"snake_case\"}}]}", msg.Serialize());
        }

        private class TestTemplateData
        {
            [JsonProperty("myCamelCaseProperty")]
            public string MyCamelCaseProperty { get; set; }

            [JsonProperty("my-kebab-case-property")]
            public string MyKebabCaseProperty { get; set; }

            public string MyPascalCaseProperty { get; set; }

            [JsonProperty("my_snake_case_property")]
            public string MySnakeCaseProperty { get; set; }
        }
    }
}
