using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WordCheck
{
    [ServiceContract]
    public interface IDictionary
    {
        [OperationContract]
        String GetSuggestion(String word);

        [OperationContract]
        String Insert(String word);

    }
}
