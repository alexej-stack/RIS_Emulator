using CommandLine;
using System;

namespace RIS.Console
{
    // на данный момент сделал ввод всех входных данных в одну строку
  public  class ProgramArguments
    {
        #region Worklist
        [Option('l',Default = "DVTK_MW_SCP", MetaValue  ="WorklistLocalAETitle", HelpText = "WorklistLocalAETitle value")]
        public string WorklistLocalAeTitle { get; set; }

        [Option('p',Default ="107",MetaValue = "WorklistLocalPort",HelpText = "WorklistLocalPort vlaue")]
        public string WorklistLocalPort { get; set; }

        [Option('r',Default = "DVTK_MW_SCU", MetaValue = "WorklistRemoteAeTitle",HelpText = "WorklistRemoteAeTitle")]
        public string WorklistRemoteAeTitle { get; set; }

        #endregion
        #region MPPS
        [Option('m', Default = "DVTK_MPPS_SCP", MetaValue = "MppsLocalAETitle", HelpText = "MPPSLocalAETitle value")]
        public string MppsLocalAeTitle { get; set; }

        [Option('y', Default = "108", MetaValue = "MppsLocalPort", HelpText = "MPPSLocalPort vlaue")]
        public string MppsLocalPort { get; set; }

        [Option('o', Default = "DVTK_MPPS_SCU", MetaValue = "MppsRemoteAeTitle", HelpText = "MPPSRemoteAeTitle value")]
        public string MppsRemoteAeTitle { get; set; }
        #endregion

        [Option('d',  MetaValue = "Data Directory", HelpText = "Data Directory name value")]
        public string DataDirectoryName { get; set; }
    }
}
