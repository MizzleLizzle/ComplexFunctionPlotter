# ComplexFunctionPlotter
Plots complex valued functions

# Usage
Change ``FunctionPlotter.Function()`` to your desired function or pass it as a Delegate to ``FunctionPlotter.Plot()``. Filepath,  codec, resolution and the Area that is to be plotted can be set in Program.cs. ``FunctionPlotter.MagnitudeLimit`` can be thought of as the aperture the image is taken with. Make sure that nuGet Packages ``System.Drawing`` and ``System.Numerics`` are installed.


# Example
This is a plot of ``f(z)=sin(x^-1)`` on the unitm square I quickly generated.
![test](https://user-images.githubusercontent.com/35309553/132891127-46c003ad-2542-4b40-ae3d-db50b287f498.png)
