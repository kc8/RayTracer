using System; 
using System.Drawing; 
using System.Drawing.Imaging;

using RayTracer.Colors;


namespace RayTracer
{
    public interface IImage 
    {
        int PixelHeight { get; }
        int PixelWidth { get; }

        void SetPixel(int x, int y, Colors.Color color);
        Colors.Color GetPixel(int x, int y); 
        ///<summary> 
        /// We want to commit this frame to the saving/displaying method
        ///<summary> 
        void Commit(); 
    }

    ///<summary>
    /// Think of this as a canvas
    /// Our image will write to the view, window or image file. 
    /// The image will take care of converting from our RayTarcer world space 
    /// to the views specific type. For instance if your Height and Width are in 
    /// the range of 255 X 255 pixels, we can call SetPixel at (-1.0, 0.9) and 
    /// the image will convert to the correct coorindate system
    ///</summary>
    /// URGENT TODO: I don't want to use System.Drawing as there is  alot of windows 
    /// specific things
    public class Image : IImage
    {
        private readonly int _Height;
        public int PixelHeight 
        {
            get => _Height; 
        }

        private readonly int _Width;
        public int PixelWidth
        {
            get => _Width; 
        }

        //https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bitmap?view=windowsdesktop-5.0
        private Bitmap _Bitmap;

        ///<summary>
        /// Create a basic image with a width and a height
        /// The image will just create a BMP image in the directory
        ///</summary>
        public Image(int pixelWidth, int pixelHeight) 
        {
            _Height = pixelHeight; 
            _Width = pixelWidth; 
            _Bitmap = new Bitmap(_Width, _Height, PixelFormat.Format32bppArgb); 
        }

        public void SetPixel(int x, int y, Colors.Color color) 
        {
            Colors.Color sRGBColor = color.Linear1ToSRGB255(); 
            System.Drawing.Color convertedColor =  RayTracerColorToSystemDrawingColor(sRGBColor); 
            _Bitmap.SetPixel(x, y, convertedColor);
        }

        public Colors.Color GetPixel(int x, int y) 
        {
            throw new NotImplementedException("GET PIXEL NEEDS TO CONVERT FROM 0,255 to 0f-1f"); 
        }

        public void Commit() 
        {
            _Bitmap.Save("test.bmp", ImageFormat.Bmp); 
        }

        ///<summary>
        /// I would like to keep our platform C# code from the drawing library 
        /// sepreate from the rest of our code base incase we decided to 
        /// use OpenGL or do something more fancy in the future
        ///<summary>
        public System.Drawing.Color RayTracerColorToSystemDrawingColor(Colors.Color c)
        {
            //TODO we will need to test this cast at somepoint
            System.Drawing.Color result = System.Drawing.Color.FromArgb(
                    (int)c.A, 
                    (int)c.R, 
                    (int)c.G, 
                    (int)c.B);
            return result; 
        }
    }
}
