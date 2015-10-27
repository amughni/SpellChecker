using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WordCheck.Helper
{
    public class FileHelper
    {
        public static bool Save(String path, Stream stream)
        {
            try
            {
                if (String.IsNullOrEmpty(path))
                    return false;

                //using (var fileStream = new FileStream(path, GetFileMode(path), FileAccess.Write))
                //{
                //    stream.CopyTo(fileStream);
                //    return true;
                //}

                using (FileStream fileStream = GetFile(path, stream))
                {
                    // Initialize the bytes array with the stream length and then fill it with data
                    byte[] bytesInStream = new byte[stream.Length];
                    stream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                    return true;
                }
            }
            catch (Exception e)
            {
                //Log Exception
                throw (e);
            }
        }

        public static FileStream Open(String path)
        {
            return File.Open(path, FileMode.Open);
        }

        public static String Read(String path)
        {
            // Open the stream and read it back.
            using (FileStream fs = Open(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                StringBuilder text = new StringBuilder();
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    text.Append(temp.GetString(b));
                }
                return text.ToString();
            }
        }

        private static FileStream GetFile(String path, Stream stream)
        {
            if (File.Exists(path) && new FileInfo(path).Length > 0)
            {
                return new FileStream(path, FileMode.Append, FileAccess.Write);
            }
            else
            {
                return File.Create(path, (int)stream.Length);
            }
        }
    }
}