using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace SendGrid.Extensions.DependencyInjection.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void TestAddSendGridWithoutApiKey()
        {
            // Arrange
            var services = new ServiceCollection().AddSendGrid(options => { }).Services.BuildServiceProvider();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => services.GetRequiredService<ISendGridClient>());
        }

        [Fact]
        public void TestAddSendGridReturnHttpClientBuilder()
        {
            // Arrange
            var collection = new ServiceCollection();

            // Act
            var builder = collection.AddSendGrid(options => options.ApiKey = "FAKE_API_KEY");

            // Assert
            Assert.NotNull(builder);
            Assert.IsAssignableFrom<IHttpClientBuilder>(builder);
        }

        [Fact]
        public void TestAddSendGridRegisteredWithTransientLifeTime()
        {
            // Arrange
            var collection = new ServiceCollection();

            // Act
            var builder = collection.AddSendGrid(options => options.ApiKey = "FAKE_API_KEY");

            // Assert
            var serviceDescriptor = collection.FirstOrDefault(x => x.ServiceType == typeof(ISendGridClient));
            Assert.NotNull(serviceDescriptor);
            Assert.Equal(ServiceLifetime.Transient, serviceDescriptor.Lifetime);
        }

        [Fact]
        public void TestAddSendGridCanResolveSendGridClientOptions()
        {
            // Arrange
            var services = new ServiceCollection().AddSendGrid(options => options.ApiKey = "FAKE_API_KEY").Services.BuildServiceProvider();

            // Act
            var sendGridClientOptions = services.GetService<IOptions<SendGridClientOptions>>();

            // Assert
            Assert.NotNull(sendGridClientOptions);
        }

        [Fact]
        public void TestAddSendGridCanResolveSendGridClient()
        {
            // Arrange
            var services = new ServiceCollection().AddSendGrid(options => options.ApiKey = "FAKE_API_KEY").Services.BuildServiceProvider();

            // Act
            var sendGrid = services.GetService<ISendGridClient>();

            // Assert
            Assert.NotNull(sendGrid);
        }
    }
}
