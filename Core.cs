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

            var exampleMsgArray = new string[]
            {
                "Examples:",
                "To decrypt a save file: FFXVSaveCrypt.exe -d \"gameplay0.save\"",
                "To encrypt a save file: FFXVSaveCrypt.exe -e \"gameplay0.save\"", ""
            };

            var actionSwitchesMsgArray = new string[]
            {
                "Action Switches:", "-d = To Decrypt", "-e = To Encrypt"
            };

            if (args.Length < 2)
            {
                ErrorExit($"Enough arguments not specified\n\n{string.Join("\n", actionSwitchesMsgArray)}\n\n{string.Join("\n", exampleMsgArray)}");
            }

            if (Enum.TryParse(args[0].Replace("-", ""), false, out CryptActions actionSwitch) == false)
            {
                ErrorExit($"Invalid or no action switch specified\n\n{string.Join("\n", actionSwitchesMsgArray)}");
            }

            if (!File.Exists(args[1]))
            {
                ErrorExit("Specified file is missing");
            }

            try
            {
                switch (actionSwitch)
                {
                    case CryptActions.d:
                        Decrypt.BeginDecryption(args[1]);
                        break;

                    case CryptActions.e:
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

        private enum CryptActions
        {
            d,
            e
        }
    }
}