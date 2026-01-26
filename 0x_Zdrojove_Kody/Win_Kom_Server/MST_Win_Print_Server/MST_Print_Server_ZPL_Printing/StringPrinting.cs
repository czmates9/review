using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Fask.Logging;

namespace MST_Print_Server_ZPL_Printing
{
    public class StringPrinting
    {
        public User user = null;
        MST_Print_Server_ZPL_Printing.RAW_Printing raw_printing_object = null;

        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        public int PrintEncodingPage
        {
            get { return raw_printing_object.PrintEncodingPage; }
            set { raw_printing_object.PrintEncodingPage = value; }
        }

        public StringPrinting()
        {
            raw_printing_object = new RAW_Printing();
        }

        public StringPrinting(TiskParams tiskParams)
            : this()
        {
            raw_printing_object.PrintParams = tiskParams;
        }

        public TiskParams PrintParams
        {
            get { return raw_printing_object.PrintParams; }
            set { raw_printing_object.PrintParams = value; }
        }

        /// <summary>
        /// Vytlaci retazce -> posle ich do tlaciarne,
        /// automaticky orezava po kazdom listku
        /// </summary>
        /// <param name="messages">Zoznam retazcov na tlacenie (vysledny ZPL zdrojovy kod)</param>
        /// <param name="id_tiskarna">ID tlaciarne</param>
        /// <returns>Vrati pocet neuspesnych tlaceni, 0 = vsetko OK</returns>
        public int Print(List<string> messages, string jobName)
        {
            int count = 0;
            int cnt = 0;
            // kontrola uzivatele na empty a podle toho se rozhodne, jak tisknout
            if (user != null && !string.IsNullOrEmpty(user.Name) && impersonateValidUser(user))
            {
                //Insert your code that runs under the security context of a specific user here.
                foreach (string message in messages)
                {
                    if (!PrintString(message, jobName + "(" + (++cnt) + ")"))
                        count++;
                }
                undoImpersonation();
            }
            else
            {
                foreach (string message in messages)
                {
                    if (!PrintString(message, jobName + "(" + (++cnt) + ")"))
                        count++;
                }
            }

            return count;
        }

        public bool PrintString(string finalString, string jobName)
        {
            bool succed = false;

            try
            {
                raw_printing_object.JobName = jobName;

                if (raw_printing_object.PrintParams.CONFIG_NAME != string.Empty)
                {
                    succed = raw_printing_object.PrintName(finalString);
                }
                else if (raw_printing_object.PrintParams.CONFIG_IP != "")
                {
                    succed = raw_printing_object.PrintIP3(finalString);
                }
                else if (raw_printing_object.PrintParams.CONFIG_COM != "")
                {
                    succed = raw_printing_object.PrintCOM(finalString);
                }
                else
                {
                    throw new Exception("Printer parameters not configured correctly");
                }

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return succed;

        }

        private bool impersonateValidUser(User us)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(us.Name, us.DomainName, us.Password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        private void undoImpersonation()
        {
            impersonationContext.Undo();
        }
    }
}
