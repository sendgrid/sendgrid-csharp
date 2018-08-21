using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using Xunit;

namespace SendGrid.Tests
{
    public class DynamicTemplateDataSerialisationTests
    {
        [Fact]
        public void TestSetDynamicTemplateDataWorksWithSpecifiedJsonPropertyNames()
        {
            var msg = new SendGridMessage();

            var dynamicTemplateData = new TestDynamicTemplateData
            {
                MyCamelCaseProperty = "camelCase",
                MyKebabCaseProperty = "kebab-case",
                MyPascalCaseProperty = "PascalCase",
                MySnakeCaseProperty = "snake_case",
            };

            msg.SetDynamicTemplateData(dynamicTemplateData);
            Assert.Equal("{\"personalizations\":[{\"dynamic_template_data\":{\"myCamelCaseProperty\":\"camelCase\",\"my-kebab-case-property\":\"kebab-case\",\"MyPascalCaseProperty\":\"PascalCase\",\"my_snake_case_property\":\"snake_case\"}}]}", msg.Serialize());
        }

        private class TestDynamicTemplateData
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
