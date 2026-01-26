using Fask.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    public class OSK
    {
        //private static bool _showed = false;
        public static bool Showed
        {
            get
            {
                var klavesnice = System.Diagnostics.Process.GetProcessesByName(@"osk");
                return ((klavesnice != null) && (klavesnice.Length > 0));                    
            }
        }

        public static void Toggle()
        {
            if (!OSK.Showed)
            {
                OSK.Show();
            }
            else
            {
                OSK.Close();
            }
        }

        public static void Show()
        {
            //try
            //{
            //    var klavesnice = System.Diagnostics.Process.Start(@"c:\Program Files\Common Files\microsoft shared\ink\TabTip.exe");
            //    var uiHostNoLaungClass = new TabTipLib.UIHostNoLaunchClass();
            //    uiHostNoLaungClass.ToggleWnd();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            try
            {
                var klavesnice = System.Diagnostics.Process.Start(@"osk.exe");
            }
            catch (Exception ex)
            {
                //Exceptions.Handler.ErrorHandle(ex.Message, "OSK.Show()", false);
                ExceptionHandler2.Handle(ex.Message, "OSK.Show()", false);
            }
            finally
            {
            }
        }

        public static void Close()
        {
            try
            {
                if (Showed)
                {
                    var klavesnice = System.Diagnostics.Process.GetProcessesByName(@"osk");
                    if ((klavesnice != null) && (klavesnice.Length > 0))
                    {
                        klavesnice.ToList().ForEach(x => x.Kill());
                    }
                }
            }
            catch (Exception ex)
            {
                //Exceptions.Handler.ErrorHandle(ex.Message, "OSK.Close()", false);
                ExceptionHandler2.Handle(ex.Message, "OSK.Close()", false);
            }
            finally
            {
            }
        }
    }
}
