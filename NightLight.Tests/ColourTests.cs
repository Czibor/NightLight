using NightLight.Models;
using Xunit;

namespace NightLight.Tests
{
    public class ColourTests
    {
        [Theory]
        [InlineData(1000, 255, 51, 0)]
        [InlineData(3500, 255, 196, 137)]
        [InlineData(6500, 255, 249, 253)]
        public void CanConvertColourTemperature(int colourTemperature, int expectedRed, int expectedGreen, int expectedBlue)
        {
            Colour colour = new Colour(colourTemperature);
            Assert.Equal(expectedRed, colour.Red);
            Assert.Equal(expectedGreen, colour.Green);
            Assert.Equal(expectedBlue, colour.Blue);
        }

        [Theory]
        [InlineData(1000, 255, 51, 0)]
        [InlineData(3500, 255, 196, 137)]
        [InlineData(6500, 255, 249, 253)]
        public void CanConvertColourTemperatureNinetyPercent(int colourTemperature, int expectedRed, int expectedGreen, int expectedBlue)
        {
            Colour colour = new Colour(colourTemperature);
            Assert.InRange(colour.Red, expectedRed * 0.9, expectedRed * 1.1);
            Assert.InRange(colour.Green, expectedGreen * 0.9, expectedGreen * 1.1);
            Assert.InRange(colour.Blue, expectedBlue * 0.9, expectedBlue * 1.1);
        }

        [Theory]
        [InlineData(1000, 255, 51, 0)]
        [InlineData(3500, 255, 196, 137)]
        [InlineData(6500, 255, 249, 253)]
        public void CanConvertColourTemperatureEightyPercent(int colourTemperature, int expectedRed, int expectedGreen, int expectedBlue)
        {
            Colour colour = new Colour(colourTemperature);
            Assert.InRange(colour.Red, expectedRed * 0.8, expectedRed * 1.2);
            Assert.InRange(colour.Green, expectedGreen * 0.8, expectedGreen * 1.2);
            Assert.InRange(colour.Blue, expectedBlue * 0.8, expectedBlue * 1.2);
        }

        [Theory]
        [InlineData(1000, 255, 51, 0)]
        [InlineData(3500, 255, 196, 137)]
        [InlineData(6500, 255, 249, 253)]
        public void CanConvertColourTemperatureSeventyPercent(int colourTemperature, int expectedRed, int expectedGreen, int expectedBlue)
        {
            Colour colour = new Colour(colourTemperature);
            Assert.InRange(colour.Red, expectedRed * 0.7, expectedRed * 1.3);
            Assert.InRange(colour.Green, expectedGreen * 0.7, expectedGreen * 1.3);
            Assert.InRange(colour.Blue, expectedBlue * 0.7, expectedBlue * 1.3);
        }

        [Theory]
        [InlineData(1000, 255, 51, 0)]
        [InlineData(3500, 255, 196, 137)]
        [InlineData(6500, 255, 249, 253)]
        public void CanConvertColourTemperatureSixtyPercent(int colourTemperature, int expectedRed, int expectedGreen, int expectedBlue)
        {
            Colour colour = new Colour(colourTemperature);
            Assert.InRange(colour.Red, expectedRed * 0.6, expectedRed * 1.4);
            Assert.InRange(colour.Green, expectedGreen * 0.6, expectedGreen * 1.4);
            Assert.InRange(colour.Blue, expectedBlue * 0.6, expectedBlue * 1.4);
        }
    }
}