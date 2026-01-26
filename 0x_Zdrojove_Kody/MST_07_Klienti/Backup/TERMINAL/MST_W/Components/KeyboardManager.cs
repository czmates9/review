using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Components
{
    public class KeyboardManager
    {
        public enum KeyboardType
        {
            Unknown,
            Symbol_QWERTY,
            Symbol_NoQWERTY,
            Honeywell_NoQWERTY
        }

        public static KeyboardType keyboardtype = Settings.KeyboardType;

        /// <summary>
        /// Uchovani puvodniho modu klavesnice pri nacateni formulare.
        /// </summary>
        public static KeyboardMode defaultKeyboardMode = KeyboardMode.Unknown;

        public enum KeyboardMode
        {
            Unknown, 
            Numeric,
            Alpha
        };

        /// <summary>
        /// Ulozeni puvodniho stavu klavesnice (umistit do konstruktoru formulare).
        /// </summary>
        public static void SaveDefaultKeyboardMode()
        {
            try
            {
                switch (keyboardtype)
                {
                    case KeyboardType.Symbol_QWERTY:
                        Symbol.Keyboard.KeyPad k = null;
                        try
                        {
                            k = new Symbol.Keyboard.KeyPad();
                            int keyStateAct = k.GetKeyStateEx();
                            switch (keyStateAct)
                            {
                                case Symbol.Keyboard.KeyStates.KEYSTATE_NUMERIC_LOCK:
                                case Symbol.Keyboard.KeyStates.KEYSTATE_NUMLOCK:
                                    defaultKeyboardMode = KeyboardMode.Numeric;
                                    break;
                                default:
                                    defaultKeyboardMode = KeyboardMode.Alpha;
                                    break;
                            }
                        }
                        catch { }
                        finally
                        {
                            try
                            {
                                if (k != null)
                                    k.Dispose();
                            }
                            catch { }
                        }
                        break;
                    case KeyboardType.Symbol_NoQWERTY:
                        Symbol.Keyboard.KeyPad k2 = null;
                        try
                        {
                            k2 = new Symbol.Keyboard.KeyPad();
                            defaultKeyboardMode = k2.AlphaMode ? KeyboardMode.Alpha : KeyboardMode.Numeric;
                        }
                        catch { }
                        finally
                        {
                            try
                            {
                                if (k2 != null)
                                    k2.Dispose();
                            }
                            catch { }
                        }
                        break;
                    case KeyboardType.Honeywell_NoQWERTY:
                        defaultKeyboardMode = HSM.Embedded.Utility.SystemNotification.GetNumlockKeyState() ? KeyboardMode.Numeric : KeyboardMode.Alpha;
                        break;
                    case KeyboardType.Unknown:
                    default:
                        break;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Nacteni puvodniho stavu klavesnice
        /// </summary>
        public static void LoadDefaultKeyboardMode()
        {
            // pokud neni defaultni mod inicializovan (Unknown), pokusi se nastavit puvodni hodnoty
            Switch(defaultKeyboardMode);
        }

        public static void SetDefault()
        {            
            switch (keyboardtype)
            {
                case KeyboardType.Symbol_QWERTY:
                    Switch(KeyboardMode.Alpha);
                    break;
                case KeyboardType.Symbol_NoQWERTY:
                    Switch(KeyboardMode.Numeric);
                    break;
                case KeyboardType.Honeywell_NoQWERTY:
                    Switch(KeyboardMode.Numeric);
                    break;
                case KeyboardType.Unknown:
                default:
                    break;
            }
        }

        public static void Switch(KeyboardMode keyboardmode)
        {
            switch (keyboardtype)
            {
                case KeyboardType.Symbol_QWERTY:
                    Symbol.Keyboard.KeyPad k = null;
                    try
                    {                        
                        k = new Symbol.Keyboard.KeyPad();
                        switch (keyboardmode)
                        {
                            case KeyboardMode.Numeric:
                                k.SetKeyState(Symbol.Keyboard.KeyStates.KEYSTATE_NUMERIC_LOCK, 0, false);
                                break;
                            case KeyboardMode.Alpha:
                                k.SetKeyState(Symbol.Keyboard.KeyStates.KEYSTATE_UNSHIFT, 0, false);
                                break;
                            default:  // Unknown                              
                                break;
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        try
                        {
                            if (k != null)
                                k.Dispose();
                        }
                        catch { }
                    }
                    break;
                case KeyboardType.Symbol_NoQWERTY:
                    Symbol.Keyboard.KeyPad k2 = null;
                    try
                    {
                        k2 = new Symbol.Keyboard.KeyPad();
                        switch (keyboardmode)
                        {
                            case KeyboardMode.Numeric:
                                k2.AlphaMode = false;
                                break;
                            case KeyboardMode.Alpha:
                                k2.AlphaMode = true;
                                break;
                            default:  // Unknown                                
                                break;
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        try
                        {
                            if (k2 != null)
                                k2.Dispose();
                        }
                        catch { }
                    }
                    break;
                case KeyboardType.Honeywell_NoQWERTY:
                    try
                    {
                        switch (keyboardmode)
                        {
                            case KeyboardMode.Numeric:
                                HSM.Embedded.Utility.SystemNotification.SetNumlockKeyState(true);
                                break;
                            case KeyboardMode.Alpha:
                                HSM.Embedded.Utility.SystemNotification.SetNumlockKeyState(false);
                                break;
                            default:  // Unknown
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex, "Switch Honeywell keyboard");
                    }
                    break;
                case KeyboardType.Unknown:
                default:
                    break;
            }
        }
    }
}
