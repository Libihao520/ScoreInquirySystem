using System.Drawing;

namespace Service.Utils;

public class CaptchaGenerator
{
    public static string GenerateCaptcha(int width, int height,string captchaText)
    {
        // 创建一个Bitmap对象
        using (Bitmap bitmap = new Bitmap(width, height))
        {
            // 使用Graphics对象在Bitmap上绘制图形和文本
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // 设置背景颜色
                graphics.Clear(Color.White);

                // 设置字体
                Font font = new Font("Arial", 18);

       

                // 绘制验证码文本
                graphics.DrawString(captchaText, font, Brushes.Black, new PointF((width - captchaText.Length * 15) / 2, (height - font.Height) / 2));

                // 将Bitmap对象转换为Base64编码的字符串
                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] bytes = ms.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }
    }
}