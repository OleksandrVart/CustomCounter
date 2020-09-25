using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCounter
{
    class Logging
    {
        public static void write(string text)
        {
            string filename = @"\counter_logs.txt";
            string path = Directory.GetCurrentDirectory() + filename;
            DateTime curDate = DateTime.Now;

            FileStream file = new FileStream(path, FileMode.Append);
            StreamWriter fnew = new StreamWriter(file, Encoding.GetEncoding(1251));

            fnew.WriteLine(curDate.ToLocalTime() + " - " + text);
            fnew.Close();
        }
    }
}
