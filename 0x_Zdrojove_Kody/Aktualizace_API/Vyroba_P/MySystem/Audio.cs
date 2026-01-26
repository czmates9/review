using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Aktualizace_API.MySystem
{
    public class Audio
    {
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
