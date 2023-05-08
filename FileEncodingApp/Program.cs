using FileEncodingApp.Utility;
using System.Text;

namespace FileEncodingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("============检查文件编码，以及转换文件编码==============");
            string filepath3 = @"D:\参考程序\杂项\FileEncodingApp\FileEncodingApp\files\gb2312001.txt";
            string filepath3_bak = @"D:\参考程序\杂项\FileEncodingApp\FileEncodingApp\files\gb2312001_bak.txt";
            //var tc2 = ConsoleAppDome.Common.FileEncodingUtility.GetEncoding(filepath3_bak);
            var tc = FileEncodingUtility.GetEncoding(filepath3);
            //转换文件编码格式可以传编码字符串,比如"gb2312 ,utf-8" 或者 Encoding.UTF8
            //FileEncodingUtility.ConvertFileEncoding(filepath3, filepath3_bak);
            FileEncodingUtility.ConvertFileEncoding(filepath3, filepath3_bak, tc, Encoding.UTF8);
            var tc2 = FileEncodingUtility.GetEncoding(filepath3_bak);

           // var en=  Encoding.GetEncoding("gb13080");
            Console.ReadLine();
            Console.ReadKey();

        }
    }
}