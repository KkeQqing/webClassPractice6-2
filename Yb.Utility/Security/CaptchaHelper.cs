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
        /// 生成验证码（随机数字字符串）
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns>验证码字符串</returns>
        public string CreateValidateCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";

            // 生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }

            // 生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }

            // 抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }

            // 组合成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }

            return validateNumberStr;
        }

        /// <summary>
        /// 生成验证码图片（返回字节数组）
        /// </summary>
        /// <param name="validateCode">验证码文本</param>
        /// <returns>byte[] 图片数据</returns>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 14.0), 27);
            Graphics g = Graphics.FromImage(image);

            try
            {
                Random random = new Random();

                // 清空背景
                g.Clear(Color.White);

                // 添加干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                // 设置字体
                Font font = new Font("Arial", 14, (FontStyle.Bold | FontStyle.Italic)); // ✅ 正确
                LinearGradientBrush brush = new LinearGradientBrush(
                    new Rectangle(0, 0, image.Width, image.Height),
                    Color.Blue,
                    Color.DarkRed,
                    1.2f,
                    true
                );

                // 绘制验证码文字
                g.DrawString(validateCode, font, brush, 3, 2);

                // 添加前景噪点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                // 边框
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                // 保存为字节数组
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 生成唯一分组编号（用于区分不同用户请求）
        /// </summary>
        /// <returns>long 类型的唯一编号</returns>
        public long CreateGroupNumber()
        {
            DateTime dateTime = DateTime.Now;
            return Convert.ToInt64(dateTime.Year.ToString() +
                                  dateTime.Month.ToString() +
                                  dateTime.Day.ToString() +
                                  dateTime.Minute.ToString() +
                                  RndNum(4));
        }

        /// <summary>
        /// 生成指定长度的随机数字符串
        /// </summary>
        /// <param name="VcodeNum">长度</param>
        /// <returns>随机数字串</returns>
        public string RndNum(int VcodeNum)
        {
            string Vchar = "0,1,2,3,4,5,6,7,8,9";
            string[] VcArray = Vchar.Split(',');
            string VNum = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(9); // 0-9
                if (temp != -1 && temp == t)
                {
                    return RndNum(VcodeNum); // 递归避免重复
                }

                temp = t;
                VNum += VcArray[t];
            }

            return VNum;
        }
    }
}