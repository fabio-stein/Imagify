using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;

namespace Imagify
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.CreateDirectory("data");
            var data = File.ReadAllBytes("data/input.txt");

            var pixelsNeeded = (int)Math.Ceiling((double)data.Length / 3);
            var pixelCount = Util.getClosestPerfectSquare(pixelsNeeded);

            var emptyBytes = (pixelCount * 3) - data.Length;
            var temp = data.ToList();

            //We use byte 255 to specify a null item
            temp.AddRange(Enumerable.Repeat((byte)255, emptyBytes));
            data = temp.ToArray();

            var sqrt = (int)Math.Sqrt(pixelCount);

            var xSize = sqrt;
            var ySize = sqrt;

            using (Image<Rgb24> image = new Image<Rgb24>(xSize, ySize))
            {
                var x = 0;
                var y = 0;
                for (int i = 0; i < data.Length; i += 3)
                {
                    var r = (byte)data[i];
                    var g = (i + 1 < data.Length) ? data[i + 1] : (byte)255;
                    var b = (i + 2 < data.Length) ? data[i + 2] : (byte)255;

                    image[x, y] = new Rgb24(r, g, b);

                    if (x == (xSize - 1))
                    {
                        x = 0;
                        y++;
                    }
                    else
                        x++;
                }
                image.Save("data/output.png");
            }

            //READ THE IMAGE FILE

            using (Image<Rgb24> image = Image.Load<Rgb24>("data/output.png"))
            {
                byte[] decoded = new byte[image.Width * image.Height * 3];
                long pointer = 0;
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        var pixel = image[x, y];

                        decoded[pointer] = (byte)pixel.R;
                        pointer++;
                        decoded[pointer] = (byte)pixel.G;
                        pointer++;
                        decoded[pointer] = (byte)pixel.B;
                        pointer++;
                    }
                }

                //Clear empty bytes
                decoded = decoded.Where(c => c != (byte)255).ToArray();
                var text = Encoding.UTF8.GetString(decoded);
                Console.WriteLine(text);
            }
        }
    }
}
