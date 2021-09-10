using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace ComplexFunctionPlotter
{
    class Program
    {
        static void Main(string[] args)
        {
            Complex Topleft = new Complex(-1, 1);
            Complex BottomRight = new Complex(1, -1);
            FunctionPlotter plotter = new FunctionPlotter(Topleft, BottomRight, 1000, 1000, 200);
            Bitmap Bmap = plotter.Plot();
            Bmap.Save("Plot.png", ImageFormat.Png);
        }
    }
}
