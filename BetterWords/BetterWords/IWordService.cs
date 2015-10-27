using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BetterWords
{
    [ServiceContract]
    public interface IWordService
    {
        [OperationContract]
        string LoadData();

        [OperationContract]
        string Correct(string word);
    }

}
