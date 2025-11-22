using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Yb.Utility.Security
{
    public class CaptchaHelper
    {
        /// <summary>
        /// 生成指定长度的数字验证码
        /// </summary>
        public string CreateValidateCode(int length)
        {
            var random = new Random();
            string code = "";
            for (int i = 0; i < length; i++)
            {
                code += random.Next(0, 10).ToString();
            }
            return code;
        }

        /// <summary>
        /// 根据验证码生成图片字节数组（JPEG）
        /// </summary>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            int width = (int)Math.Ceiling(validateCode.Length * 14.0);
            int height = 27;
            using Bitmap image = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(image);

            // 背景色
            g.Clear(Color.White);

            // 干扰线
            Random random = new Random();
            for (int i = 0; i < 25; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }

            // 验证码字体
            Font font = new Font("Arial", 14, FontStyle.Bold | FontStyle.Italic);
            LinearGradientBrush brush = new LinearGradientBrush(
                new Rectangle(0, 0, image.Width, image.Height),
                Color.Blue, Color.DarkRed, 1.2f, true);

            g.DrawString(validateCode, font, brush, 3, 2);

            // 前景干扰点
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            // 边框
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

            // 输出为 JPEG 字节数组
            using MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();
        }
    }
}