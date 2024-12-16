
using Syntra.Frituurtje.Helpers;


namespace Syntra.Frituurtje.UnitTests
{
    public class CalculationTest
    {
        [Fact]
        public void CelciusTest()
        {
            // Arrange
            decimal farenheit = 32m;
            var calc = new Calculations();

            // Act
            decimal? celcius = calc.ToCelcius(farenheit);

            // Assert
            Assert.Equal(0, celcius);

        }
        [Fact]
        public void CelciusUnhappyTest()
        {
            decimal farenheit = 32.4m;
            var calc = new Calculations();

            decimal? celcius = calc.ToCelcius(farenheit);

            Assert.NotEqual(0, celcius);

        }
        [Fact]
        public void FarenheitUnhappyTest()
        {
            decimal celcius = 0.1m;
            var calc = new Calculations();

            decimal? farenheit = calc.ToFarenheit(celcius);

            Assert.NotEqual(32, farenheit);

        }
        [Fact]
        public void FarenheitTest()
        {
            decimal celcius = 0;
            var calc = new Calculations();

            decimal? farenheit = calc.ToFarenheit(celcius);

            Assert.Equal(32, farenheit);           

        }
        [Theory]
        [InlineData(0, 32)]
        [InlineData(100, 212)]
        [InlineData(36.5, 97.7)]
        public void CelciusToFarenheitShouldBe(decimal value, decimal expected)
        {
            expected = Math.Round(expected, 2);
            // Arrange
            var calc = new Calculations();
            // Act
            decimal? celcius = calc.ToFarenheit(value);
            // Assert
            Assert.Equal(expected, celcius);
        }
        [Theory]
        [MemberData(nameof(FarenheitTestData))]
        public void FarenheitToCelciusShouldBe(decimal value, decimal expected)
        {
            expected=Math.Round(expected,2);    
            // Arrange
            var calc = new Calculations();
            // Act
            decimal farenheit = calc.ToCelcius(value);
            // Assert
            Assert.Equal(expected, farenheit);
        }
        public static IEnumerable<object[]> FarenheitTestData()
        {
            yield return new object[] { 100, 37.7778 };
            yield return new object[] { 0, -17.7778 };
            yield return new object[] { 82.4, 28 };
        }
    }
}