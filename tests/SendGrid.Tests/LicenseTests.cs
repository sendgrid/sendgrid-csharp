namespace SendGrid.Tests
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using Xunit;

    public class LicenseTests
    {
        [Fact]
        public void ShouldHaveCurrentYearInLicense()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var line = File.ReadLines(Path.Combine(directoryInfo.Parent.Parent.Parent.Parent.Parent.FullName, "LICENSE")).Skip(2).Take(1).First();
            Assert.Contains(DateTime.Now.Year.ToString(), line);
        }
    }
}
