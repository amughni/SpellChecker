using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace BetterWords
{
    public class WordService : IWordService
    {
        public string LoadData()
        {
            return Helper.LoadData();
        }

        public string Correct(string word)
        {
            return Helper.Correct(word);
        }

    }
}
