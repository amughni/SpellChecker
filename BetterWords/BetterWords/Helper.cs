using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BetterWords
{
    public class Helper
    {
        private static Dictionary<String, int> _dictionary = new Dictionary<String, int>();

        private static Regex _wordRegex = new Regex("[a-z]+", RegexOptions.Compiled);

        public static string LoadData()
        {
            try
            {
                string fileContent = File.ReadAllText("C:\\temp\\words.txt");
                List<string> wordList = fileContent.Split('\n').ToList();

                foreach (var word in wordList)
                {
                    string trimmedWord = word.Trim().ToLower();
                    if (_wordRegex.IsMatch(trimmedWord))
                    {
                        if (_dictionary.ContainsKey(trimmedWord))
                            _dictionary[trimmedWord]++;
                        else
                            _dictionary.Add(trimmedWord, 1);
                    }
                }
                return "success";
            }
            catch (Exception ee)
            {
                return "Loading failure. Exception: " + ee.Message;
            }
        }

        public static string Correct(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            word = word.ToLower();

            if (_dictionary.ContainsKey(word))
                return word;

            List<String> list = Edits(word);
            Dictionary<string, int> candidates = new Dictionary<string, int>();

            FindCandidates(list, ref candidates);

            if (candidates.Count > 0)
                return candidates.OrderByDescending(x => x.Value).First().Key;

            foreach (string item in list)
            {
                FindCandidates(Edits(item), ref candidates);
            }

            return (candidates.Count > 0) ? candidates.OrderByDescending(x => x.Value).First().Key : word;
        }

        private static void FindCandidates(List<String> list, ref Dictionary<string, int> candidates)
        {
            foreach (string wordVariation in list)
            {
                if (_dictionary.ContainsKey(wordVariation) && !candidates.ContainsKey(wordVariation))
                    candidates.Add(wordVariation, _dictionary[wordVariation]);
            }
        }

        private static List<string> Edits(string word)
        {
            var splits = new List<Tuple<string, string>>();
            var transposes = new List<string>();
            var deletes = new List<string>();
            var replaces = new List<string>();
            var inserts = new List<string>();

            // Splits
            for (int i = 0; i < word.Length; i++)
            {
                //Example: "name"
                //<n,n>
                //<na,a>
                //<nam,m>
                //<name, e>
                var tuple = new Tuple<string, string>(word.Substring(0, i), word.Substring(i));
                splits.Add(tuple);
            }

            for (int i = 0; i < splits.Count; i++)
            {
                string a = splits[i].Item1;
                string b = splits[i].Item2;

                if (!string.IsNullOrEmpty(b))
                {
                    // Deletes
                    deletes.Add(a + b.Substring(1));

                    //Replaces
                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        replaces.Add(a + c + b.Substring(1));
                    }
                }

                // Transposes
                if (b.Length > 1)
                {
                    transposes.Add(a + b[1] + b[0] + b.Substring(2));
                }

                //Inserts
                for (char c = 'a'; c <= 'z'; c++)
                {
                    inserts.Add(a + c + b);
                }
            }
            return deletes.Union(transposes).Union(replaces).Union(inserts).ToList();
        }

    }
}
