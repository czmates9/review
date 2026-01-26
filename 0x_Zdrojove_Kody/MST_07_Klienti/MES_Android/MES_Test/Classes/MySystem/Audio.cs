using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Android.Media;
using Android.Support.V7.App;
using Android.Net;
using System.Threading;
using MES_Android.Classes;
using System.IO;
using System.Threading.Tasks;

namespace MES_Android.MySystem
{
    public class Audio
    {

        //public static void PlaySound(AppCompatActivity _parent, string strFileName)
        //{
        //    //11.10.2019 upraveno na  PlaySoundFlags.SND_SYNC z duvodu HANIBAL na volnem pohybu

        //    //PlaySound(strFileName, IntPtr.Zero,
        //    //    PlaySoundFlags.SND_FILENAME | PlaySoundFlags.SND_SYNC);

        //    var confirmThread = new Thread(() => Play(_parent, strFileName));
        //    confirmThread.Start();

        //}

        public static async Task<bool> PlaySoundAsync(AppCompatActivity _parent, string strFileName)
        {
            //11.10.2019 upraveno na  PlaySoundFlags.SND_SYNC z duvodu HANIBAL na volnem pohybu

            //PlaySound(strFileName, IntPtr.Zero,
            //    PlaySoundFlags.SND_FILENAME | PlaySoundFlags.SND_SYNC);

            return await Task.Run(()=>{
                return Play(_parent, strFileName);
            });

            //var confirmThread = new Thread(() => Play(_parent, strFileName));
            //confirmThread.Start();

        }

        private static bool Play(AppCompatActivity _parent, string strFileName)
        {
            int ID = 0;

            if (strFileName == Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundExpedice))
                ID = Resource.Raw.expedice;
            if (strFileName == Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundSklad))
                ID = Resource.Raw.rozdelit;
            if (strFileName == Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundSkladExpedice))
                ID = Resource.Raw.sklad;
            if (strFileName == Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundUspesneVlozeni))
                ID = Resource.Raw.UspesneNacteni;
            if (strFileName == Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundDisponibility))
                ID = Resource.Raw.brownuv_sum;

            if (strFileName == Path.Combine(DataInfo_Static.SoundDir, DataInfo_Static.SoundDotaz))
                ID = Resource.Raw.dotaz;
            if (strFileName == Path.Combine(DataInfo_Static.SoundDir, DataInfo_Static.SoundChimes))
                ID = Resource.Raw.chimes;
            if (strFileName == Path.Combine(DataInfo_Static.SoundDir, DataInfo_Static.SoundChyba))
                ID = Resource.Raw.chyba;
            if (strFileName == Path.Combine(DataInfo_Static.SoundDir, DataInfo_Static.SoundInfo))
                ID = Resource.Raw.info;
            


            var p = MediaPlayer.Create(_parent, ID);
            p.Start();

            Thread.Sleep(1500);

            p.Release();
            p.Dispose();
            p = null;

            return true;
        }
    }
}