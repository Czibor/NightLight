using System;

namespace NightLight.Models
{
    public class Colour
    {
        public byte Alpha { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }

        public Colour()
        {
        }

        public Colour(byte alpha, byte red, byte green, byte blue)
        {
            Alpha = alpha;
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>
        /// Automatically sets RGB values based on colour temperature.
        /// </summary>
        public Colour(int colourTemperature)
        {
            SetColourByTemperature(colourTemperature);
        }
        
        public void SetColourByTemperature(int colourTemperature)
        {
            Colour colour = GetColourFromTemperature(colourTemperature);
            Red = colour.Red;
            Green = colour.Green;
            Blue = colour.Blue;
        }

        /// <summary>
        /// Colour temperature to colour converter.
        /// </summary>
        /// <param name="colourTemperature">Temperature value. Should be between 1000 and 40000.</param>
        public static Colour GetColourFromTemperature(int colourTemperature)
        {
            Colour colour = new Colour();
            int number = colourTemperature;
            
            if (number < 1000)
            {
                number = 1000;
            }
            else if (number > 40000)
            {
                number = 40000;
            }

            // All calculations require temperature / 100.
            number = number / 100;

            // Calculate colours.
            colour.Red = GetRedValue(number);
            colour.Green = GetGreenValue(number);
            colour.Blue = GetBlueValue(number);

            return colour;
        }

        private static byte GetRedValue(int number)
        {
            byte result = 0;

            if (number <= 66)
            {
                result = 255;
            }
            else
            {
                // The R-squared value for this approximation is .988.
                double red = number - 60;
                red = 329.698727446 * Math.Pow(red, -0.1332047592);

                if (red < 0)
                {
                    result = 0;
                }
                else if (red > 255)
                {
                    result = 255;
                }
                else
                {
                    result = (byte)red;
                }
            }

            return result;
        }

        private static byte GetGreenValue(int number)
        {
            byte result = 0;

            if (number <= 66)
            {
                // The R-squared value for this approximation is .996.
                double green = number;
                green = 99.4708025861 * Math.Log(green) - 161.1195681661;

                if (green < 0)
                {
                    result = 0;
                }
                else if (green > 255)
                {
                    result = 255;
                }
                else
                {
                    result = (byte)green;
                }
            }
            else
            {
                // The R-squared value for this approximation is .987.
                double green = number - 60;
                green = 288.1221695283 * Math.Pow(green, -0.0755148492);

                if (green < 0)
                {
                    result = 0;
                }
                else if (green > 255)
                {
                    result = 255;
                }
                else
                {
                    result = (byte)green;
                }
            }

            return result;
        }

        private static byte GetBlueValue(int number)
        {
            byte result = 0;

            if (number >= 66)
            {
                result = 255;
            }
            else if (number <= 19)
            {
                result = 0;
            }
            else
            {
                // The R-squared value for this approximation is .998.
                double blue = number - 10;
                blue = 138.5177312231 * Math.Log(blue) - 305.0447927307;

                if (blue < 0)
                {
                    result = 0;
                }
                else if (blue > 255)
                {
                    result = 255;
                }
                else
                {
                    result = (byte)blue;
                }
            }

            return result;
        }
    }
}