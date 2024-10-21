using System.Text.Json.Serialization;
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
            var actual = msg.Serialize();
            var expected = "{\"personalizations\":[{\"dynamic_template_data\":{\"myCamelCaseProperty\":\"camelCase\",\"my-kebab-case-property\":\"kebab-case\",\"MyPascalCaseProperty\":\"PascalCase\",\"my_snake_case_property\":\"snake_case\"}}]}";
            Assert.Equal(expected, actual);
        }

        private class TestTemplateData
        {
            [JsonPropertyName("myCamelCaseProperty")]
            public string MyCamelCaseProperty { get; set; }

            [JsonPropertyName("my-kebab-case-property")]
            public string MyKebabCaseProperty { get; set; }

            public string MyPascalCaseProperty { get; set; }

            [JsonPropertyName("my_snake_case_property")]
            public string MySnakeCaseProperty { get; set; }
        }
    }
}
