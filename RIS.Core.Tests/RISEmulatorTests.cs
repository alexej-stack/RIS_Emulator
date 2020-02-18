using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIS.Console;
using System.IO;

namespace RIS.Core.Tests
{
    [TestClass]
    public class RISEmulatorTests
    {
        [TestMethod]
        public void ConsoleConfigMPPSTest()
        {
            var risEmulator = new RISEmulator();
            risEmulator.wlConfig = new ConsoleRISConfigurationWL();

            risEmulator.mppsConfig = new ConsoleRISConfigrtionMPPS();

            Assert.IsTrue(risEmulator.mppsConfig.MppsLocalPort == "108");
            Assert.IsTrue(risEmulator.wlConfig.WorklistLocalPort == "107");
        }
        [TestMethod]
        public void InizializeTest()
        {
            var risEmulator = new RISEmulator();
            risEmulator.wlConfig = new ConsoleRISConfigurationWL();
            risEmulator.mppsConfig = new ConsoleRISConfigrtionMPPS();
            risEmulator.dataDirectory = @"C:\Users\Alexej Drosodv\Documents\GitHub\DVTk\RIS_Emulator\Resources\Data\Worklist";

            risEmulator.StartAction();

            Assert.AreEqual(risEmulator.isRunning, true);

            risEmulator.StopAction();
        }

        [TestMethod]
        public void OverviewThreadTest()
        {
            var risEmulator = new RISEmulator();
            risEmulator.wlConfig = new ConsoleRISConfigurationWL();
            risEmulator.mppsConfig = new ConsoleRISConfigrtionMPPS();
            risEmulator.dataDirectory = @"C:\Users\Alexej Drosodv\Documents\GitHub\DVTk\RIS_Emulator\Resources\Data\Worklist";

            risEmulator.StartAction();

            Assert.IsFalse(risEmulator.overviewThread == null);

            risEmulator.StopAction();
        }

        [TestMethod]
        public void StartWithFirstFileFromDataTest()
        {
            var risEmulator = new RISEmulator();
            risEmulator.wlConfig = new ConsoleRISConfigurationWL();
            risEmulator.mppsConfig = new ConsoleRISConfigrtionMPPS();
            risEmulator.dataDirectory = @"C:\Users\Alexej Drosodv\Documents\GitHub\DVTk\RIS_Emulator\Resources\Data\Worklist";

            risEmulator.StartAction();

            Assert.AreEqual(risEmulator.isRunning, true);
            Assert.AreEqual(risEmulator.overviewThread.HasBeenStarted, true);
            Assert.AreEqual(risEmulator.overviewThread.IsStopCalled, false);

            risEmulator.StopAction();
        }

        [TestMethod]
        public void StopTest()
        {
            var risEmulator = new RISEmulator();
            risEmulator.wlConfig = new ConsoleRISConfigurationWL();
            risEmulator.mppsConfig = new ConsoleRISConfigrtionMPPS();
            risEmulator.dataDirectory = @"C:\Users\Alexej Drosodv\Documents\GitHub\DVTk\RIS_Emulator\Resources\Data\Worklist";

            risEmulator.StartAction();
     

            risEmulator.StopAction();

            Assert.AreEqual(risEmulator.overviewThread.IsStopCalled, true);
        }



        [TestMethod]
        public void GetDicomFile()
        {
            var DICOMFILE = "";
            var risEmulator = new RISEmulator();
            var dataDirectory = @"C:\Users\Alexej Drosodv\Documents\GitHub\DVTk\RIS_Emulator\Resources\Data\Worklist";

            var directoryDataRepository = new RIS.Data.DirectoryDataRepository();

            directoryDataRepository.dataRepositoryName = dataDirectory;
            directoryDataRepository.Initialize(dataDirectory);
            var dicomFiles = directoryDataRepository.GetData(directoryDataRepository.GetDirectories(dataDirectory), @"*.dcm", SearchOption.AllDirectories);
            foreach (var file in dicomFiles)
            {
                DICOMFILE = file.FullName;
            }
            Assert.AreNotEqual(DICOMFILE, "");
        }
    }

}

