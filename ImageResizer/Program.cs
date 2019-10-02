using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output");

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            sw.Stop();
            var oriTime = sw.ElapsedMilliseconds;
            imageProcess.Clean(destinationPath);

            Console.WriteLine($"同步-花費時間: {oriTime} ms");

            sw.Restart();
            await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
            sw.Stop();
            var asyncTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"非同步-花費時間: {asyncTime} ms");

            var improve = ((float)(oriTime - asyncTime) / oriTime) * 100;

            Console.WriteLine($"{improve}%");

            Console.ReadKey();


        }
    }
}
