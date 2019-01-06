using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PM.Bazaar.Services.WebApi.Extensions
{
    public static class ImageUtilExtensions
    {
        public static Image Resize(this Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth/image.Width;
            var ratioY = (double)maxHeight/image.Height;

            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        public static Image Resize(this Image image, int maxHeight)
        {
            var ratioY = (double)maxHeight / image.Height;

            var newWidth = (int)(image.Width * ratioY);
            var newHeight = (int)(image.Height * ratioY);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        public static byte[] ToArray(this Image image)
        {
            var ms = new MemoryStream();

            image.Save(ms, ImageFormat.Jpeg);

            return ms.ToArray();
        }

        public static Image ToImage(this byte[] buffer)
        {
            return Image.FromStream(new MemoryStream(buffer));
        }
    }
}