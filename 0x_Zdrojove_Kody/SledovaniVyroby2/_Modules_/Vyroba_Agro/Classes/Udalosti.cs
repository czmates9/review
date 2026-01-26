using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    // rizeni hlidani poctu udalosti 11

    public class Udalosti
    {
        public enum Event11Stav { ADD = 2, RESET, GET_MAX, SET_MAX, GET_TIME, SET_TIME, COUNT, STATE };
        public enum Event36Stav { ADD = 2, RESET, GET_MAX, SET_MAX, GET_TIME, SET_TIME, COUNT, STATE };


        private static List<long> event11list = new List<long>();
        private static int event11_max = AgroConfig.config.Agro[0].MaxPocetUdalosti11;  //maximalni pocet udalosti 11, po kterych vybehne hlaska
        private static int event11_time = AgroConfig.config.Agro[0].CasovyIntervalUdalosti11;  //15; //cas v minutach
        private static object lockObjectE11 = new object();

        public static int event11(Event11Stav command, int value)
        {
            lock (lockObjectE11)
            {
                if (command == Event11Stav.ADD)
                {
                    event11list.Add(DateTime.Now.Ticks);
                    return event11list.Count;
                }
                else if (command == Event11Stav.RESET)
                {
                    event11list.Clear();
                    return 0;
                }
                else if (command == Event11Stav.SET_MAX)
                {
                    event11_max = value;
                    return event11_max;
                }
                else if (command == Event11Stav.GET_MAX)
                    return event11_max;
                else if (command == Event11Stav.GET_TIME)
                    return event11_time;
                else if (command == Event11Stav.SET_TIME)
                {
                    event11_time = value;
                    return event11_time;
                }
                else if (command == Event11Stav.COUNT)
                    return event11list.Count;
                else if (command == Event11Stav.STATE)
                {
                    while (true)
                    {
                        long acttm = DateTime.Now.Ticks;

                        if (event11list.Count <= 0)
                            return 0;

                        long frstm = event11list[0];
                        if (frstm == 0)
                            break;

                        double diftm = acttm - frstm;
                        if (diftm > (60 * event11_time) * TimeSpan.TicksPerSecond) //min
                        {
                            event11list.RemoveAt(0);
                        }
                        else
                        {
                            break;
                        }
                    }

                }
                int event11_cnt = event11list.Count;
                return event11_cnt >= event11_max ? 1 : 0;
            }
        }

        private static List<long> event36list = new List<long>();
        private static int event36_max =  AgroConfig.config.Agro[0].MaxPocetUdalosti36 ;
        private static int event36_time =  AgroConfig.config.Agro[0].CasovyIntervalUdalosti36;  //15; //cas v minutach
        private static object lockObjectE36 = new object();

        public static int event36(Event36Stav command, int value)
        {
            lock (lockObjectE36)
            {
                if (command == Event36Stav.ADD)
                {
                    event36list.Add(DateTime.Now.Ticks);
                    return event36list.Count;
                }
                else if (command == Event36Stav.RESET)
                {
                    event36list.Clear();
                    return 0;
                }
                else if (command == Event36Stav.SET_MAX)
                {
                    event36_max = value;
                    return event36_max;
                }
                else if (command == Event36Stav.GET_MAX)
                    return event36_max;
                else if (command == Event36Stav.GET_TIME)
                    return event36_time;
                else if (command == Event36Stav.SET_TIME)
                {
                    event36_time = value;
                    return event36_time;
                }
                else if (command == Event36Stav.COUNT)
                    return event36list.Count;
                else if (command == Event36Stav.STATE)
                {
                    while (true)
                    {
                        long acttm = DateTime.Now.Ticks;

                        if (event36list.Count <= 0)
                            return 0;

                        long frstm = event36list[0];
                        if (frstm == 0)
                            break;

                        double diftm = acttm - frstm;
                        if (diftm > (60 * event36_time) * TimeSpan.TicksPerSecond) //min
                        {
                            event36list.RemoveAt(0);
                        }
                        else
                        {
                            break;
                        }
                    }

                }

                int event36_cnt = event36list.Count;
                return event36_cnt >= event36_max ? 1 : 0;
            }
        }
    }
}
