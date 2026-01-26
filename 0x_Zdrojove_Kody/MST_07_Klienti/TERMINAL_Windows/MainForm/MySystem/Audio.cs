using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.MST_WINDOWS
{
    public class Audio
    {
        public const string SoundChimes = "chimes.wav";
        public const string SoundDotaz = "dotaz.wav";
        public const string SoundChyba = "chyba.wav";
        public const string SoundInfo = "info.wav";

        private static System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        public static void PlaySound(string pathtosound)
        {
            try
            {
                sp.SoundLocation = pathtosound;
                sp.Play();

            }
            catch 
            {
            }
        }
    }
}
