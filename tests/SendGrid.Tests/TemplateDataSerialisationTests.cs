using System.Collections.Generic;
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
        public void TestSetTemplateData()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            var dynamicTemplateData1 = new
            {
                key12 = "Dynamic Template Data Value 12",
                key13 = "Dynamic Template Data Value 13"
            };
            msg.SetTemplateData(dynamicTemplateData1);
            Assert.Equal("{\"personalizations\":[{\"dynamic_template_data\":{\"key12\":\"Dynamic Template Data Value 12\",\"key13\":\"Dynamic Template Data Value 13\"}}]}", msg.Serialize());

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var dynamicTemplateData2 = new
            {
                key14 = "Dynamic Template Data Value 14",
                key15 = "Dynamic Template Data Value 15"
            };
            var personalization = new Personalization()
            {
                TemplateData = dynamicTemplateData2
            };
            var dynamicTemplateData3 = new
            {
                key16 = "Dynamic Template Data Value 16",
                key17 = "Dynamic Template Data Value 17"
            };
            msg.SetTemplateData(dynamicTemplateData3, 0, personalization);
            Assert.Equal("{\"personalizations\":[{\"dynamic_template_data\":{\"key16\":\"Dynamic Template Data Value 16\",\"key17\":\"Dynamic Template Data Value 17\"}}]}", msg.Serialize());

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            var dynamicTemplateData4 = new
            {
                key18 = "Dynamic Template Data Value 18",
                key19 = "Dynamic Template Data Value 19"
            };
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    TemplateData = dynamicTemplateData4
                }
            };
            var dynamicTemplateData5 = new
            {
                key20 = "Dynamic Template Data Value 20",
                key21 = "Dynamic Template Data Value 21"
            };
            personalization = new Personalization()
            {
                TemplateData = dynamicTemplateData5
            };
            var dynamicTemplateData6 = new
            {
                key22 = "Dynamic Template Data Value 22",
                key23 = "Dynamic Template Data Value 23"
            };
            msg.SetTemplateData(dynamicTemplateData6, 1, personalization);
            Assert.Equal("{\"personalizations\":[{\"dynamic_template_data\":{\"key18\":\"Dynamic Template Data Value 18\",\"key19\":\"Dynamic Template Data Value 19\"}},{\"dynamic_template_data\":{\"key22\":\"Dynamic Template Data Value 22\",\"key23\":\"Dynamic Template Data Value 23\"}}]}", msg.Serialize());

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            var dynamicTemplateData7 = new
            {
                key24 = "Dynamic Template Data Value 24",
                key25 = "Dynamic Template Data Value 25"
            };
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    TemplateData = dynamicTemplateData7
                }
            };
            var dynamicTemplateData8 = new
            {
                key26 = "Dynamic Template Data Value 26",
                key27 = "Dynamic Template Data Value 27"
            };
            msg.SetTemplateData(dynamicTemplateData8);
            Assert.Equal("{\"personalizations\":[{\"dynamic_template_data\":{\"key26\":\"Dynamic Template Data Value 26\",\"key27\":\"Dynamic Template Data Value 27\"}}]}", msg.Serialize());

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            var dynamicTemplateData9 = new
            {
                key28 = "Dynamic Template Data Value 28",
                key29 = "Dynamic Template Data Value 29"
            };
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    TemplateData = dynamicTemplateData9
                }
            };
            var dynamicTemplateData10 = new
            {
                key30 = "Dynamic Template Data Value 30",
                key31 = "Dynamic Template Data Value 31"
            };
            personalization = new Personalization()
            {
                TemplateData = dynamicTemplateData10
            };
            msg.Personalizations.Add(personalization);
            var dynamicTemplateData11 = new
            {
                key32 = "Dynamic Template Data Value 32",
                key33 = "Dynamic Template Data Value 33"
            };
            msg.SetTemplateData(dynamicTemplateData11);
            Assert.Equal("{\"personalizations\":[{\"dynamic_template_data\":{\"key32\":\"Dynamic Template Data Value 32\",\"key33\":\"Dynamic Template Data Value 33\"}},{\"dynamic_template_data\":{\"key30\":\"Dynamic Template Data Value 30\",\"key31\":\"Dynamic Template Data Value 31\"}}]}", msg.Serialize());

            // Complex dynamic template data
            msg = new SendGridMessage();
            var dynamicTemplateData12 = new
            {
                array = new List<string>
                {
                    "Dynamic Template Data Array Value 1",
                    "Dynamic Template Data Array Value 2"
                },
                innerObject = new
                {
                    innerObjectKey1 = "Dynamic Template Data Deep Object Value 1"
                }
            };
            msg.SetTemplateData(dynamicTemplateData12);
            Assert.Equal("{\"personalizations\":[{\"dynamic_template_data\":{\"array\":[\"Dynamic Template Data Array Value 1\",\"Dynamic Template Data Array Value 2\"],\"innerObject\":{\"innerObjectKey1\":\"Dynamic Template Data Deep Object Value 1\"}}}]}", msg.Serialize());
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
