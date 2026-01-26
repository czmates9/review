using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MES_Android.Async_Metody
{
    public class TEST_Asynchronne_Okno
    {

        public async Task<bool> DisplayMessage(AppCompatActivity parent, string titile, string content)
        {
            var objDialog = new Android.App.AlertDialog.Builder(parent)
               .SetTitle(titile)
               .SetMessage(content)
               .SetCancelable(false)
               .Create();

            bool result = false;

            await Task.Run(() =>
            {

                var waitHandle = new AutoResetEvent(false);
                objDialog.SetButton((int)(DialogButtonType.Positive), "yes", (sender, e) =>
                {
                    result = true;
                    waitHandle.Set();
                });

                objDialog.SetButton((int)DialogButtonType.Negative, "no", (sender, e) =>
                {
                    result = false;
                    waitHandle.Set();
                });

                parent.RunOnUiThread(() =>
                {
                    objDialog.Show();
                });

                waitHandle.WaitOne();

            });

            objDialog.Dispose();

            return result;
        }
    }
}