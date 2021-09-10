using System;
using System.Drawing;
using System.Numerics;

namespace ComplexFunctionPlotter
{
    class FunctionPlotter
    {
        private Bitmap Bmap;
        private Complex TopLeft;
        private Complex BottomRight;
        private double MagnitudeLimit;
        public FunctionPlotter(Complex TopLeft, Complex BottomRight, int width, int height, double MagnitudeLimit)
        {
            this.Bmap = new Bitmap(width, height);
            this.TopLeft = TopLeft;
            this.BottomRight = BottomRight;
            this.MagnitudeLimit = MagnitudeLimit;
        }

        private Color ComplexToColorRepresentation(Complex c)
        {
            Color Representation = AngleToColor(c.Phase);
            Representation = MagnitudeToLightness(Representation, c.Magnitude, MagnitudeLimit);
            return Representation;
        }
        public Bitmap Plot()
        {
            double AreaWidth = Math.Abs(BottomRight.Real - TopLeft.Real);
            double AreaHeight = Math.Abs(TopLeft.Imaginary - BottomRight.Imaginary);
            double AreaWidthStep = AreaWidth / Bmap.Width;
            double AreaHeightStep = AreaHeight / Bmap.Height;
            for (int x = 0; x < Bmap.Width; x++)
            {
                for(int y  = 0; y < Bmap.Height; y++)
                {
                    Complex Location = new Complex(TopLeft.Real + x * AreaWidthStep, BottomRight.Imaginary + y * AreaHeightStep);
                    Color newPixel = ComplexToColorRepresentation(Function(Location));
                    Bmap.SetPixel(x, y, newPixel);
                }
            }
            return Bmap;
        }

        public Bitmap Plot(Func<Complex,Complex> FunctionDelegate)
        {
            double AreaWidth = Math.Abs(BottomRight.Real - TopLeft.Real);
            double AreaHeight = Math.Abs(TopLeft.Imaginary - BottomRight.Imaginary);
            double AreaWidthStep = AreaWidth / Bmap.Width;
            double AreaHeightStep = AreaHeight / Bmap.Height;
            for (int x = 0; x < Bmap.Width; x++)
            {
                for (int y = 0; y < Bmap.Height; y++)
                {
                    Complex Location = new Complex(TopLeft.Real + x * AreaWidthStep, BottomRight.Imaginary + y * AreaHeightStep);
                    Color newPixel = ComplexToColorRepresentation(FunctionDelegate(Location));
                    Bmap.SetPixel(x, y, newPixel);
                }
            }
            return Bmap;
        }

        //Change to desired Function
        public static Complex Function(Complex x)
        {
            return Complex.Sin(Complex.Pow(x,new Complex(-1,0)));
        }

        public static Color AngleToColor(double Angle)
        {
            if (Angle < 0) Angle += 2.0 * Math.PI;

            if (Angle < Math.PI/3.0)
            {
                int Green = Convert.ToInt32(255.0 * (Angle / (Math.PI / 3.0)));

                return Color.FromArgb(255,Green, 0);
            }
            if(Angle < 2.0 * Math.PI / 3.0)
            {
                int Red = Convert.ToInt32(255.0 - (255.0 * (Angle - Math.PI / 3.0) / (Math.PI / 3.0)));

                return Color.FromArgb(Red, 255, 0);
            }
            if(Angle < Math.PI)
            {
                int Blue = Convert.ToInt32(255.0 * (Angle - 2.0 * Math.PI / 3.0) / (Math.PI / 3.0));

                return Color.FromArgb(0, 255, Blue);
            }
            if(Angle < 4.0 * Math.PI / 3.0)
            {
                int Green = Convert.ToInt32(255.0 - 255.0 * (Angle - Math.PI) / (Math.PI / 3.0));

                return Color.FromArgb(0, Green, 255);
            }
            if(Angle < 5.0 * Math.PI / 3.0)
            {
                int Red = Convert.ToInt32(255.0 * (Angle - 4.0 * Math.PI / 3.0) / (Math.PI / 3.0));

                return Color.FromArgb(Red, 0, 255);
            }
            else
            {
                int Blue = Convert.ToInt32(255.0 - 255.0 * (Angle - 5.0 * Math.PI / 3.0) / (Math.PI / 3.0));

                return Color.FromArgb(255, 0, Blue);
            }          
        }

        public static Color MagnitudeToLightness(Color Hue, double Magnitude, double UpperLimit)
        {
            int BlueDif = 255 - Hue.B;
            int RedDif = 255 - Hue.R;
            int GreenDif = 255 - Hue.G;
            if (Magnitude > UpperLimit) Magnitude = UpperLimit;
            if (Magnitude < UpperLimit / (2.0))
            {
                double Factor = Math.Sqrt(Math.Sqrt((Magnitude / (UpperLimit / 2.0))));
                return Color.FromArgb(Convert.ToInt32(Hue.R * Factor), Convert.ToInt32(Hue.G * Factor), Convert.ToInt32(Hue.B * Factor));
            }
            else
            {
                double Factor = Math.Sqrt(Math.Sqrt((Magnitude - (UpperLimit / 2.0)) / (UpperLimit / 2.0)));
                return Color.FromArgb(Hue.R + Convert.ToInt32(RedDif * Factor), Hue.G + Convert.ToInt32(GreenDif * Factor), Hue.B + Convert.ToInt32(BlueDif * Factor));
            }
        }
    }
}
