using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fask.MST_W.Vydej_3.Online
{
    public class Material
    {
        public static Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow Online_Material_Get(string itemnmbr, string skl_id, string serltnum)
        {
            Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow vydej_online_item = null;
            if (MST_Global.Vydej_Items_Online)
            {
                var go = Vydej.vydejInstance.globalObject;
                var ds_items_online = go.service_vydej.Online_GetMaterial(itemnmbr, skl_id, serltnum);
                if (ds_items_online == null)
                    throw new Exception("Položky online nenalezeny");
                var dt_items_online = ds_items_online.Items;
                // TODO : dialog pro zobrazeni vysledku ... viz color
                if (dt_items_online.Count == 0)
                {
                    throw new Exception("Položky online nenalezeny");
                }
                else if (dt_items_online.Count == 1)
                {
                    vydej_online_item = dt_items_online[0];
                }
                else // vice zaznamu -> zobrazit dialog
                {
                    using (VydejVyberMaterialuList vvml = new VydejVyberMaterialuList(ds_items_online))
                    {
                        if (vvml.ShowDialog() == DialogResult.OK)
                        {
                            //ITEMNMBR = pvpl.ITEMNMBR;
                            vydej_online_item = vvml.SelectedItem;
                        }
                    }
                }
            }
            return vydej_online_item;
        }
    }
}
