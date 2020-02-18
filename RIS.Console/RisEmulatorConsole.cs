using CommandLine;
using RIS.Core;
using System;

namespace RIS.Console
{
    public static class RisEmulatorConsole
    {
        /// <summary>
        ///Depending on the progress, either a signal of successful execution of the program or an error code is returned.
        /// </summary>

        static int Main(string[] args)
        {
            var resOfParsing = Parser.Default.ParseArguments<ProgramArguments>(args).WithParsed(arguments =>
            {
                try
                {
                    var worklistLocalAeTitle = arguments.WorklistLocalAeTitle;
                    var worklistLocalPort = arguments.WorklistLocalPort;
                    var worklistRemoteAeTitle = arguments.WorklistRemoteAeTitle;

                    var dataDirectory = arguments.DataDirectoryName;

                    var mppsLocalAeTitle = arguments.MppsLocalAeTitle;
                    var mppsLocalPort = arguments.MppsLocalPort;
                    var mppsRemoteAeTitle = arguments.MppsRemoteAeTitle;

                }
                catch (Exception exeption)
                {
                    System.Console.WriteLine(exeption);
                }
            });

            var parsedResult = ((Parsed<ProgramArguments>)resOfParsing).Value;
            try
            {
                var risEmulator = new RISEmulator();
                risEmulator.wlConfig = new ConsoleRISConfigurationWL();
                risEmulator.wlConfig.Serialize(parsedResult.WorklistLocalAeTitle, parsedResult.WorklistLocalPort, parsedResult.WorklistRemoteAeTitle);

                risEmulator.mppsConfig = new ConsoleRISConfigrtionMPPS();
                risEmulator.mppsConfig.Serialize(parsedResult.MppsLocalAeTitle, parsedResult.MppsLocalPort, parsedResult.MppsRemoteAeTitle);

                risEmulator.dataDirectory = parsedResult.DataDirectoryName;

                risEmulator.StartAction();

                if (risEmulator.isRunning == true)
                {
                    return -1;
                }
                else
                {
                    return 101010;
                }
            }
            catch (Exception exeption)
            {
                System.Console.WriteLine(exeption);
            }
           
            return 777;
            
           
              
            
        }
    }
}
