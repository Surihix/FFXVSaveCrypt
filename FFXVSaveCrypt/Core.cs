using FFXVSaveCrypt.Crypto;
using System;
using System.IO;

namespace FFXVSaveCrypt
{
    internal class Core
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");

            if (args.Length < 2)
            {
                var actionSwitchesMsgArray = new string[]
                {
                    "Action Switches:",
                    "-d = To Decrypt",
                    "-e = To Encrypt", ""
                };

                var filesSupported = new string[]
                {
                    "Supported files:",
                    "avatar0.save",
                    "avatar0_mod.save",
                    "gameplay0.save",
                    "gameplay_mission_temporary0.save",
                    "live_config_data.save",
                    "snapshotlink.sl",
                    "system_data.save",
                    "universal_data.save", ""
                };

                var exampleMsgArray = new string[]
                {
                    "Examples:",
                    "To decrypt a file: FFXVSaveCrypt.exe -d \"gameplay0.save\"",
                    "To encrypt a file: FFXVSaveCrypt.exe -e \"gameplay0.save\""
                };

                ErrorExit($"Enough arguments not specified\n\n{string.Join("\n", actionSwitchesMsgArray)}\n\n{string.Join("\n", filesSupported)}\n\n{string.Join("\n", exampleMsgArray)}");
            }

            if (!File.Exists(args[1]))
            {
                ErrorExit("File specified in the argument is missing");
            }

            try
            {
                switch (args[0])
                {
                    case "-d":
                        Decrypt.BeginDecryption(args[1]);
                        break;

                    case "-e":
                        Encrypt.BeginEncryption(args[1]);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception has occured");
                ErrorExit($"Exception: {ex}");
            }
        }

        private static void ErrorExit(string errorMsg)
        {
            Console.WriteLine($"Error: {errorMsg}");
            Console.WriteLine("");
            Console.ReadLine();

            Environment.Exit(0);
        }
    }
}