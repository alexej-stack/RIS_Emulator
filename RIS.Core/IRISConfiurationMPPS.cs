using System;
using System.Collections.Generic;
using System.Text;

namespace RIS.Core
{
    public interface IRISConfigurationMPPS
    {
        string MppsLocalAeTitle { get; set; }
        string MppsLocalPort { get; set; }
        string MppsRemoteAeTitle { get; set; }

        bool TSELESupport { get; set; }

        void Serialize(string LocalAeTitle, string LocalPort, string RemoteAeTitle);

        void Deserialize(string configFile);
    }
}
