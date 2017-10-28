namespace SendGrid.Tests
{
	using System;
	using System.IO;
	using System.Linq;
	using Xunit;

    public class LicenseTests
    {
        [Fact]
        public void ShouldHaveCurrentYearInLicense()
        {
            var line = File.ReadLines(@"..\..\..\..\..\LICENSE.txt").Skip(2).Take(1).First();
            Assert.Contains(DateTime.Now.Year.ToString(), line);
        }
    }
}