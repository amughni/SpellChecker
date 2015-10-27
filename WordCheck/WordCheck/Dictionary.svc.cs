using DawgSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WordCheck.Helper;

namespace WordCheck
{

    public class Dictionary : IDictionary
    {
        public String Insert(String word)
        {
            try
            {

                if (DawgHelper.InsertWord(word))
                    return "success";

                return "fail";
            }
            catch (Exception ee)
            {
                return "Fail to insert the word. Exception: " + ee.Message;
            }
        }


        public String GetSuggestion(String word)
        {
            //Todo
            return word;
        }
    }
}
