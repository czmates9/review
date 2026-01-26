using System;
using System.Runtime.InteropServices;
using System.Resources;
using System.IO;

namespace Fask.Vyroba_W.MySystem
{
    /// <summary>
    /// Trida, ktera implementuje volani funkci prehravajicich zvuky (winmm.dll)
    /// </summary>
    public class Audio
    {
        private Audio()
        { }

        //public const UInt32 SND_ASYNC = 1;
        //public const UInt32 SND_MEMORY = 4;

        ///// <summary>
        ///// Prehraje .wav soubor nacteny v poli bytu
        ///// </summary>
        ///// <param name="data">Data .wav souboru</param>
        ///// <param name="hMod">Mod prehravani</param>
        ///// <param name="dwFlags">Prepinace</param>
        ///// <returns></returns>
        //[DllImport("winmm.dll")]
        //public static extern bool PlaySound(byte[] data, IntPtr hMod, UInt32 dwFlags);

        ///// <summary>
        ///// Prehraje soubor .wav
        ///// </summary>
        ///// <param name="wavFileName">jmeno .wav souboru</param>
        //public static void PlaySound(string wavFileName)
        //{
        //    // get the namespace
        //    string strNameSpace=
        //        System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();

        //    // get the resource into a stream
        //    Stream str =
        //        System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream( strNameSpace +"."+ wavFileName );
        //    if ( str == null )
        //        return;
        //    // bring stream into a byte array
        //    byte[] bStr = new Byte[str.Length];
        //    str.Read(bStr, 0, (int)str.Length);
        //    // play the resource
        //    PlaySound(bStr, IntPtr.Zero, SND_ASYNC | SND_MEMORY);
        //}

        ///// <summary>
        ///// Prehraje soubor .wav
        ///// </summary>
        ///// <param name="wavFileName">jmeno .wav souboru</param>
        //public static void PlaySound(byte[] wavdata)
        //{
        //    // play the resource
        //    string data = System.Text.Encoding.ASCII.GetString(wavdata, 0, wavdata.Length);

        //    PlaySound(data, IntPtr.Zero, PlaySoundFlags.SND_ASYNC | PlaySoundFlags.SND_MEMORY);
        //}


        [Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000, /* play synchronously (default) */
            SND_ASYNC = 0x0001, /* play asynchronously */
            SND_NODEFAULT = 0x0002, /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004, /* pszSound points to a memory file */
            SND_LOOP = 0x0008, /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010, /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004 /* name is resource name or atom */
        }

        [DllImport("coredll")]
        private static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);

        public static void PlaySound(string strFileName)
        {
            PlaySound(strFileName, IntPtr.Zero,
                PlaySoundFlags.SND_FILENAME | PlaySoundFlags.SND_ASYNC);
        }

        //[DllImport("Kernel32.dll")]
        //private static extern bool Beep(uint dwFreq, uint dwDuration);

        //public static bool PlayBeep(uint dwFreq, uint dwDuration)
        //{
        //    try
        //    {
        //        return Beep(dwFreq, dwDuration);
        //    }
        //    catch (Exception ex)
        //    {
		//        Logging.Log.Write(ex.Message, "PlayBeep");
        //        return false;
        //    }
        //}
    }

}
