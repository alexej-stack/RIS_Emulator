using RIS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RIS.Console
{
    public class ConsoleRISConfigurationWL : IRISConfigurationWL
    {
		private string worklistLocalAeTitle = "DVTK_MW_SCP";
		private string	worklistLocalPort = "107";
		private string	worklistRemoteAeTitle = "DVTK_MW_SCU";


		public bool TSELESupport { get ; set; }
		public string WorklistLocalAeTitle
		{
			get { return worklistLocalAeTitle; }
			set { worklistLocalAeTitle = value; }
		}
		public string WorklistLocalPort
		{
			get { return worklistLocalPort; }
			set { worklistLocalPort = value; }
		}
		public string WorklistRemoteAeTitle
		{
			get { return worklistRemoteAeTitle; }
			set { worklistRemoteAeTitle = value; }
		}
		public string ConfigFullFileName { get;  set; }

		public void Deserialize(string configFile)
		{
			FileStream myFileStream = null;

			XmlSerializer mySerializer = new XmlSerializer(typeof(ConsoleRISConfigurationWL));

			ConsoleRISConfigurationWL config = null;

			try
			{
				// To read the file, creates a FileStream.
				myFileStream = new FileStream(configFile, FileMode.Open);

				// Calls the Deserialize method and casts to the object type.
				config = (ConsoleRISConfigurationWL)mySerializer.Deserialize(myFileStream);
			}
			catch
			{
				// If deserializing the config file fails, use the default settings.
				config = new ConsoleRISConfigurationWL();
			}
			finally
			{
				if (myFileStream != null)
				{
					myFileStream.Close();
				}
			}

			
		}

        public void Serialize(string LocalAeTitle,string LocalPort, string RemoteAeTitle)
        {
			if (LocalAeTitle != null || LocalPort != null || RemoteAeTitle != null)
			{
				WorklistLocalAeTitle = LocalAeTitle;
				WorklistLocalPort = LocalPort;
				WorklistRemoteAeTitle = RemoteAeTitle;
			}

		}
    }
}
