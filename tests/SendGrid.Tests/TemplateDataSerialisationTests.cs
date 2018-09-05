using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using Xunit;

namespace SendGrid.Tests
{
    public class TemplateDataSerialisationTests
    {
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
