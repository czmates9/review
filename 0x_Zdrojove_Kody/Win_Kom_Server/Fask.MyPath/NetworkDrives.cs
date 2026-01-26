using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.IO;

namespace Fask.MyPath.Network
{
    public class Drives
    {
        //private void MapNetworkDrive()
        //{
        //    System.Diagnostics.Process.Start("net.exe", @"use K: \\Server\URI\path\here");
        //}

        // source : https://www.lifewire.com/net-use-command-2618096    
        //net use [{devicename | *}] [\\computername\sharename[\volume] [{password | *}]] [/user:[domainname\]username] [/user:[dotteddomainname\]username] [/user:[username@dotteddomainname] [/home {devicename | *} [{password | *}]] [/persistent:{yes | no}] [/smartcard] [/savecred] [/delete] [/help] [/?]
        //net use	Execute the net use command alone to show detailed information about currently mapped drives and devices.
        //devicename	Use this option to specify the drive letter or printer port you want to map the network resource to. For a shared folder on the network, specify a drive letter from D: through Z:, and for a shared printer, LPT1: through LPT3:. Use * instead of specifying devicename to automatically assign the next available drive letter, starting with Z: and moving backward, for a mapped drive.
        //\\computername\sharename	This specifies the name of the computer, computername, and the shared resource, sharename, like a shared folder or a shared printer connected to computername. If there are spaces anywhere here, be sure to put the entire path, slashes included, in quotes.
        //volume	Use this option to specify the volume when connecting to a NetWare server.
        //password	This is the password needed to access the shared resource on computername. You can choose to enter the password during the execution of the net use command by typing * instead of the actual password.
        ///user	Use this net command option to specify a username to connect to the resource with. If you don't use /user, net use will attempt to connect to the network share or printer with your current username.
        //domainname	Specify a different domain than the one you're on, assuming you're on one, with this option. Skip domainname if you're not on a domain or you want net use to use the one you're already on.
        //username	Use this option with /user to specify the username to use to connect to the shared resource.
        //dotteddomainname	This option specifies the fully qualified domain name where username exists.
        ///home	This net use command option maps the current user's home directory to either the devicename drive letter or the next available drive letter with *.
        ///persistent:{yes | no}	Use this option to control the persistence of connections created with the net use command. Choose yes to automatically restore created connections at the next login or choose no to limit the life of this connection to this session. You can shorten this switch to /p if you like.
        ///smartcard	This switch tells the net use command to use the credentials present on the available smart card.
        ///savecred	This option stores the password and user information for use next time you connect in this session or in all future sessions when used with /persistent:yes.
        ///delete	This net use command is used to cancel a network connection. Use /delete with devicename to remove a specified connection or with * to remove all mapped drives and devices. This option can be shortened to /d.
        ///help	Use this option, or the shortened /h, to display detailed help information for the net use command. Using this switch is the same as using the net help command with net use: net help use.
        ///?	The standard help switch also works with the net use command but only displays the command syntax, not any detailed information about the command's options.

        // source2 : http://www.blackwasp.co.uk/MapDriveLetter_2.aspx

        public static void MapDrive(string driveLetter, string UNCPath, string domainname, string username, string password)
        {
            try
            {
                // pokud neni nastaveno pismeno jednotky, tak se nebude mapovat ...
                if (String.IsNullOrEmpty(driveLetter))
                    return;

                string credentials = @"/user:";
                if (!String.IsNullOrEmpty(domainname))
                    credentials += domainname + @"\";
                credentials += username;

                ProcessStartInfo processStartInfo =
                    new ProcessStartInfo(
                        "net.exe",
                        string.Format(@"use {0}: {1} {2} {3}", driveLetter, UNCPath, password, credentials)
                        );
                processStartInfo.UseShellExecute = false;
                Process process = Process.Start(processStartInfo);
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    ;
                }

            }
            catch 
            {
            }
        }
        public static void UnMapDrive(string driveLetter)
        {
            try
            {
                // pokud neni nastaveno pismeno jednotky, tak se nebude mapovat ...
                if (String.IsNullOrEmpty(driveLetter))
                    return;

                ProcessStartInfo processStartInfo =
                    new ProcessStartInfo(
                        "net.exe",
                        string.Format(@"use {0}: /delete", driveLetter)
                        );
                processStartInfo.UseShellExecute = false;
                Process process = Process.Start(processStartInfo);
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    ;
                }

            }
            catch
            {
            }
        }
        public static bool ExistsDrive(string driveLetter)
        {
            // pokud neni nastaveno pismeno jednotky, tak se vraci jako true...
            if (String.IsNullOrEmpty(driveLetter))
                return true;

            var driveInfos = DriveInfo.GetDrives();
            foreach (var driveInfo in driveInfos)
            {
                if (driveInfo.Name.StartsWith(driveLetter, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

    }
}
