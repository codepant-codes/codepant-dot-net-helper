using CodePant.DotNet.Helper.String;
using Xunit;

namespace CodePant.DotNet.Helper.Test.String
{
    public class StringExtensionTest
    {
        [InlineData("PasCalCase", "pas_cal_case")]
        [InlineData("CPUPower", "cpu_power")]
        [Theory]
        public void ToSnakeCaseTest(string input, string output)
        {
            Assert.Equal(input.ToSnakeCase(), output);
        }
    }
}