using System.Text;
using Ude;

namespace FileEncodingApp.Utility
{

    /// <summary>
    /// 文件编码操作
    /// unget包：Ude.NET:https://github.com/chrisquirk/ude
    /// unget包：Ude.NetStandard  https://github.com/yinyue200/ude
    /// 测试这两个包都可以
    /// 最终我使用了Ude.NetStandard
    /// </summary>
    public class FileEncodingUtility
    {
        /// <summary>
        /// 使用这个来判断
        /// utf-8 带dom和不带dom都是utf-8
        /// 经过测试，notepad++,vscode 简体中文gb2312 
        /// 获取的字符串都是gb18030 就用这个字符串去判断
        /// 别再转换成Encoding.GetEncoding(charset) 如果是gb18030就会转换失败
        /// 转换失败加入; Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        /// 但已经是目前能找到的最好办法
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetCharset(string filename)
        {


            using (var fs = File.OpenRead(filename))
            {
                ICharsetDetector cdet = new CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();
                if (cdet.Charset != null)
                {
                    return cdet.Charset;

                }
                return "未知";



            }
        }

        /// <summary>
        /// 这个是得到Charset 转换成Encoding
        /// 但是测试可能存在转换不成功，所以就没必要调用这个了
        /// 但是加入;Encoding.RegisterProvider(CodePagesEncodingProvider.Instance) 转换成功
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        public static Encoding GetEncoding(string filename)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var charset = GetCharset(filename);
                return Encoding.GetEncoding(charset);
            }
            catch (Exception ex)
            {
                return Encoding.Default;
            }
        }

        /// <summary>
        /// 文件转换编码
        /// 由于历史原因gb2312可能存在问题
        /// </summary>
        /// <param name="sourceFile">源文件路径</param>
        /// <param name="targetFile">目标路径</param>
        /// <param name="sourceEncoding">源编码</param>
        /// <param name="targetEncoding">目标编码</param>
        public static void ConvertFileEncoding(string sourceFile, string targetFile, string sourceEncoding = "gb18030", string targetEncoding = "utf-8")
        {
            //gb2312 以及gb18030不支持
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (StreamReader gb2312Reader = new StreamReader(sourceFile, Encoding.GetEncoding(sourceEncoding)))
            using (StreamWriter utf8Writer = new StreamWriter(targetFile, false, Encoding.GetEncoding(targetEncoding)))
            {
                while (!gb2312Reader.EndOfStream)
                {
                    // 逐行读取 GB2312 文本，并按 UTF-8 编码方式写入到输出文件中
                    string line = gb2312Reader.ReadLine();
                    utf8Writer.WriteLine(line);
                }
            }
        }


        /// <summary>
        /// 文件转换编码
        /// 由于历史原因gb2312可能存在问题
        /// </summary>
        /// <param name="sourceFile">源文件路径</param>
        /// <param name="targetFile">目标路径</param>
        /// <param name="sourceEncoding">源编码</param>
        /// <param name="targetEncoding">目标编码</param>
        public static void ConvertFileEncoding(string sourceFile, string targetFile, Encoding sourceEncoding, Encoding targetEncoding)
        {
            //gb2312 以及gb...不支持。所以注册类型
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (StreamReader gb2312Reader = new StreamReader(sourceFile, sourceEncoding))
            using (StreamWriter utf8Writer = new StreamWriter(targetFile, false, targetEncoding))
            {
                while (!gb2312Reader.EndOfStream)
                {
                    // 逐行读取 GB2312 文本，并按 UTF-8 编码方式写入到输出文件中
                    string line = gb2312Reader.ReadLine();
                    utf8Writer.WriteLine(line);
                }
            }
        }

    }
}
