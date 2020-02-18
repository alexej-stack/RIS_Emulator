using DvtkApplicationLayer.StoredFiles;
using DvtkHighLevelInterface.Common.Threads;
using DvtkHighLevelInterface.Dicom.Threads;
using RIS.Data;
using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace RIS.Core
{
    public class RISEmulator
    {
        #region Inizialize

     
        private ThreadManager.ThreadsStateChangeEventHandler threadsStateChangeEventHandler = null;
      
        private bool hasUnsavedChanges = false;

        private IteratorClass worklistDicomThread = null;
        private DicomThreadOptions worklistOptions;
        private DicomThreadOptions mppsOptions;
        public ThreadManager threadManager;

        public IRISConfigurationMPPS mppsConfig;
        public IRISConfigurationWL wlConfig;

        public bool isRunning = false;
        private String rootPath = "";
        private ArrayList selectedTS = new ArrayList();

        public static string dataDirectoryForTempFiles = "";
        public static bool isCurrentSPSD = true;

        private int nrOfRsps = 1;
        private bool modeOfRsp = true;
        private FileGroups fileGroups = null;
        private ValidationResultsFileGroup validationResultsFileGroup = null;
        public OverviewThread overviewThread;

        public string fileMask = "*.dcm";
        public SearchOption searchOption = SearchOption.AllDirectories;
        /// <summary>
        /// This field implement data directory name from (not only) console input 
        /// </summary>
        public string dataDirectory = "";
        private String ConfigurationsDirectory
        {
            get
            {
                return (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"DVTk\RIS Emulator\Configurations"));
            }
        }


        #endregion Inizialize

        public RISEmulator()
        {
            //
            // Stored files options.
            //
            fileGroups = new FileGroups("RIS Emulator");

            validationResultsFileGroup = new ValidationResultsFileGroup();
            this.validationResultsFileGroup.DefaultFolder = "Results";
            this.fileGroups.Add(validationResultsFileGroup);

            this.fileGroups.CreateDirectories();

            Initialize();

            // Save the config so next time no attempt will be made to again try to load the same settings
            // wlConfig.Serialize();

            dataDirectory = Directory.GetCurrentDirectory();

            modeOfRsp = true;
        }

        private void Initialize()
        {
            //
            // Construct the MPPS DicomOptions implicitly by constructing a DicomThread.
            //
            IteratorClass mppsDicomThread = new IteratorClass(rootPath, validationResultsFileGroup.Directory,/* "MPPS_SCP.ses", */"MPPS");
            this.mppsOptions = mppsDicomThread.Options;

            //
            // Construct the Worklist DicomOptions implicitly by constructing a DicomThread.
            //
            worklistDicomThread = new IteratorClass(rootPath, validationResultsFileGroup.Directory, /*"WLM_SCP.ses",*/ "Worklist");
            this.worklistOptions = worklistDicomThread.Options;
        }

        public void StartAction()
        {
            // wlConfig.Serialize();
            wlConfig.TSELESupport = true;


            // mppsConfig.Serialize();
            mppsConfig.TSELESupport = true;

            // Set the correct settings for the overview DicomThread.
            //
            String resultsFileBaseName = "RIS_Emulator" + System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);

            threadManager = new ThreadManager();
            threadManager.ThreadsStateChangeEvent += threadsStateChangeEventHandler;
            Initialize();

            var dataRepository = new DirectoryDataRepository();
            dataRepository.Initialize(dataDirectory);
            var directories = dataRepository.GetDirectories(dataRepository.dataRepositoryName);
            // на данном этапе я пока подключил LINQ. В дальнейшем возможно надо будет его убрать. 
            var startDicomFile = dataRepository.GetData(directories, fileMask, searchOption).ToList().First();

            overviewThread = new OverviewThread(mppsOptions, worklistDicomThread, selectedTS,
                modeOfRsp, startDicomFile.FullName, nrOfRsps);
            overviewThread.Initialize(threadManager);
            overviewThread.Options.ResultsDirectory = validationResultsFileGroup.Directory;
            overviewThread.Options.Identifier = resultsFileBaseName;
            overviewThread.Options.AttachChildsToUserInterfaces = true;
            overviewThread.Options.LogThreadStartingAndStoppingInParent = false;
            overviewThread.Options.LogWaitingForCompletionChildThreads = false;


            //
            // Start the DicomThread.
            //
            overviewThread.Start();

            isRunning = true;
        }

        public void StopAction()
        {
            threadManager.Stop();
            overviewThread.Stop();
       
        }
    }
}
