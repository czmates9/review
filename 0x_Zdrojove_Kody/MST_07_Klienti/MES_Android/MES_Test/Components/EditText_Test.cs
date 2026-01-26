using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Android.Widget
{
    [DesignTimeVisible(true)]
    [Register("android/widget/EditText_Test", DoNotGenerateAcw = true)]
    public class EditText_Test : EditText
    {
        
        [Register(".ctor", "(Landroid/content/Context;)V", "")]
        public EditText_Test(Context? context) : base(context) { HideKeyboard(); }
        
        
        [Register(".ctor", "(Landroid/content/Context;Landroid/util/AttributeSet;)V", "")]
        public EditText_Test(Context? context, IAttributeSet? attrs) : base(context, attrs) { HideKeyboard(); }
                
        
        [Register(".ctor", "(Landroid/content/Context;Landroid/util/AttributeSet;I)V", "")]
        public EditText_Test(Context? context, IAttributeSet? attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { HideKeyboard(); }



        [Register(".ctor", "(Landroid/content/Context;Landroid/util/AttributeSet;II)V", "")]
        public EditText_Test(Context? context, IAttributeSet? attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes) { HideKeyboard(); }
        
        
        protected EditText_Test(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { HideKeyboard(); }

        public void ShowKeyboard()
        {
            this.RequestFocus();

            var inputMethodManager = this.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.ShowSoftInput(this, ShowFlags.Forced);
            inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
        }

        public void HideKeyboard()
        {
            this.RequestFocus();
            var inputMethodManager = this.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.HideSoftInputFromWindow(this.WindowToken, HideSoftInputFlags.None); // this probably needs to be set to ToogleSoftInput, forced.
        }

        protected override void OnTextChanged(ICharSequence text, int start, int lengthBefore, int lengthAfter)
        {
            this.HideKeyboard();
            base.OnTextChanged(text, start, lengthBefore, lengthAfter);
        }
    }
}