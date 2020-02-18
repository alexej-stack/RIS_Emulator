using RIS.Core;
using System.IO;
using System.Xml.Serialization;

namespace RIS.Console
{
	// это первоначальное представоение, потом методы будут переделвыаться по считывание с консоли,
	// вероятнее всего просто всяких считываний, просто с входными портами
	public class ConsoleRISConfigrtionMPPS : IRISConfigurationMPPS
	{
		private string mppsLocalAeTitle= "DVTK_MPPS_SCP";
		private string mppsRemoteAeTitle= "DVTK_MPPS_SCU";
		private string mppsLocalPort="108";

		public string MppsLocalAeTitle
		{
			get { return mppsLocalAeTitle; }
			set { mppsLocalAeTitle = value; }
		}

		public string MppsLocalPort
		{
			get { return mppsLocalPort; }
			set { mppsLocalPort = value; }
		}

		public string MppsRemoteAeTitle
		{
			get { return mppsRemoteAeTitle; }
			set { mppsRemoteAeTitle = value; }
		}
		public bool TSELESupport { get; set; }
		public string ConfigFullFileName { get; set; }

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

		public void Serialize(string LocalAeTitle, string LocalPort, string RemoteAeTitle)
		{
			if (LocalAeTitle != null || LocalPort != null || RemoteAeTitle != null)
			{
				mppsLocalAeTitle = LocalAeTitle;
				mppsLocalPort = LocalPort;
				mppsRemoteAeTitle = RemoteAeTitle;
			}
		}
	}
}
