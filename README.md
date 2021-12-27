# Ray Tracer
This is a current work in progress with many TODOs. 
This project is inspired by the "The Ray Tracer Challenge" 
[here](https://pragprog.com/titles/jbtracer/the-ray-tracer-challenge/) book

# Here is a working example of the current Ray Tracing status: 

## Small 100 X 100 Sphere/Circle
![First Sphere](SampleImages\FirstSphere.bmp)

# How to run this
**These instructs are incomplete**
Double clicking the EXE will produce a bitmap in your current directory.
After building/running. These instructions are not complete, as this is not 
a complete RayTracer.

# How to Compile
This library will work on Windows (other OSes have not been tested yet) however 
this works with .NET5.0 which is cross platform. The one cavet to this is 
we are using some intrinsics for Vectors/Math which will cause issues on different 
platoforms. 

### However, here are the instructions to run 
1. Ensure you had .NET installed and on your path
1.1. run ```dotnet run``` to see if you do
2. cd into the RayTracer\RayTracer directory and type ```dotnet run```
2.2. Everything will be compiled and a image should be producded
3. To run test cd into RayTracer\Tets and use ```dotnet test``` to run 
the tests

# Libraries 

## Math 
I wanted to write my own, as I have done so in the past and 
find it interresting. I also feel that I do not do enough of this 
kind of math in my day to day, so reading about it and implementing it 
helps me keep it in my memory. I use some of the built in functions from 
the C# library like Round, Truncate, Sin but plan to implment a lot of the 
basics myself

## Current Issues
- Matrix shhould be SIMD
- I think it might be better to use Mono rather than .NET Core
- Heap allocations are going to cost us at some point
- I can forsee an issue with having only a Matrix 4x4 and Vector 4.

# References
[Setting up Unit Tests](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-nunit)

## Math
[3D Math Primer for Graphics](https://www.amazon.com/Math-Primer-Graphics-Game-Development/dp/1568817231) 
[Scratch Pixel](https://www.scratchapixel.com/index.php?redirect)
[Arcane Algorithm Archive](https://www.algorithm-archive.org/) 
