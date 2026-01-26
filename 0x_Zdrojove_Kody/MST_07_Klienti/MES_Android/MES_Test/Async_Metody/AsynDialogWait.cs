using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace MES_Android
{
    public class AsynDialogWait
    {
        //await AlertAsync(this, "My Title", "My Message", "Yes", "No");

        public static TaskCompletionSource<bool> tcs;

        public static Task<bool> AlertAsync(Context context, string title, string message, string positiveButton, string negativeButton, string neutralButton)
        {
            tcs = new TaskCompletionSource<bool>();

            using (var db = new AlertDialog.Builder(context))
            {
                db.SetTitle(title);
                db.SetMessage(message);

                //db.SetPositiveButton(positiveButton, (sender, args) => { tcs.TrySetResult(true); });
                //db.SetNegativeButton(negativeButton, (sender, args) => { tcs.TrySetResult(false); });

                db.SetPositiveButton(positiveButton, PosButton);
                db.SetNegativeButton(negativeButton, NegButton);

                db.SetCancelable(false);
                //db.SetOnCancelListener += new Func<IDialogInterfaceOnCancelListener?, AlertDialog.Builder?>((a, b) => { });
                db.SetIcon(Resource.Drawable.Vyroba);
                db.SetNeutralButton(neutralButton, ErrButton);
                db.Show();
            }

            return tcs.Task;
        }

        private static void PosButton(object sender, DialogClickEventArgs e)
        {
            tcs.TrySetResult(true);
        }

        private static void NegButton(object sender, DialogClickEventArgs e)
        {
            tcs.TrySetResult(false);
        }

        private static void ErrButton(object sender, DialogClickEventArgs e)
        {
            tcs.TrySetException(new Exception("krles"));
        }

    }
}