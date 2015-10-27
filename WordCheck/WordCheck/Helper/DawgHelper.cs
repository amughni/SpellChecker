using DawgSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WordCheck.Helper
{
    public class DawgHelper
    {
        private const string _FILENAME = "DAWG.bin";
        private static string _BASEPATH = System.AppDomain.CurrentDomain.BaseDirectory;
        
        /// <summary>
        /// Inserts a word into the dictionary
        /// </summary>
        /// <param name="word"></param>
        /// <returns>TRUE if the words is saved</returns>
        public static bool InsertWord(String word)
        {
            var dawgBuilder = new DawgBuilder<bool>();

            dawgBuilder.Insert(word, true);

            var dawg = dawgBuilder.BuildDawg();

            using (Stream stream = new MemoryStream(100))
            {
                Action<BinaryWriter, bool> writePayload = null;
                dawg.SaveTo(stream, writePayload);

                //bool saved = FileHelper.Save(_BASEPATH + _FILENAME, stream);

                return WordExists(word, stream);
            }
        }

        /// <summary>
        /// Finds the word in the dictionary
        /// </summary>
        /// <param name="word"></param>
        /// <returns>TRUE if found</returns>
        public static bool WordExists(String word, Stream fs)
        {
            Func<BinaryReader, bool> readPayload = null;
            var dawgMap = Dawg<bool>.Load(fs, readPayload);

            if (dawgMap[word])
            {
                return true;
            }
            
            return false;
        }
    }
}