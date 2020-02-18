using System;
using System.Collections.Generic;
using System.Text;

namespace RIS.Core
{
    public interface IRISConfigurationWL
    {
        bool TSELESupport { get; set; }
        string WorklistLocalAeTitle { get; set; }
        string WorklistLocalPort { get; set; }
        string WorklistRemoteAeTitle { get; set; }


        void Serialize(string LocalAeTitle, string LocalPort, string RemoteAeTitle);

        void Deserialize(string configFile);

    }
}
