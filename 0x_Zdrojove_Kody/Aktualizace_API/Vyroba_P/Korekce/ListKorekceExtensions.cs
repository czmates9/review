using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Aktualizace_API.Korekce
{
    public static class ListKorekceExtensions
    {
        public static TimeSpan SumKorekce(this List<Korekce> korekce)
        {
            TimeSpan lTSKorekceSuma = TimeSpan.Zero;
            if (korekce != null)
            {
                korekce.ForEach((Action<Korekce>)delegate(Korekce x)
                {
                    if (x.type.HasValue && x.type == 1)
                    {
                        lTSKorekceSuma = lTSKorekceSuma.Subtract(x.delka);
                    }
                    else
                    {
                        lTSKorekceSuma = lTSKorekceSuma.Add(x.delka);
                    }
                });
            }
            return lTSKorekceSuma;
        }

        public static TimeSpan SumKorekceZpozdeni(this List<Korekce> korekce)
        {
            TimeSpan lTSKorekceSuma = TimeSpan.Zero;
            if (korekce != null)
            {
                korekce.ForEach((Action<Korekce>)delegate(Korekce x)
                {
                    if (x.type.HasValue && x.type == 1)
                    {
                        //lTSKorekceSuma = lTSKorekceSuma.Subtract(x.delka);
                    }
                    else
                    {
                        lTSKorekceSuma = lTSKorekceSuma.Add(x.delka);
                    }
                });
            }
            return lTSKorekceSuma;
        }

        public static TimeSpan SumKorekceUspora(this List<Korekce> korekce)
        {
            TimeSpan lTSKorekceSuma = TimeSpan.Zero;
            if (korekce != null)
            {
                korekce.ForEach((Action<Korekce>)delegate(Korekce x)
                {
                    if (x.type.HasValue && x.type == 1)
                    {
                        //lTSKorekceSuma = lTSKorekceSuma.Subtract(x.delka);
                        lTSKorekceSuma = lTSKorekceSuma.Add(x.delka);
                    }
                    else
                    {
                        //lTSKorekceSuma = lTSKorekceSuma.Add(x.delka);
                    }
                });
            }
            return lTSKorekceSuma;
        }

    }
}
