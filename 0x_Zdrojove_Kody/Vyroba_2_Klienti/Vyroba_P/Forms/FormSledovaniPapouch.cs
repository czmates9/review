using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization; // přidej navrch souboru

namespace Fask.Vyroba_P.Forms
{
    public partial class FormSledovaniPapouch : Form
    {
        public FormSledovaniPapouch()
        {
            InitializeComponent();
        }

        private void btn_vycist_Click(object sender, EventArgs e)
        {
            Mereni();
        }




        #region mereni Papouch 15.4.2025 OLD
        //private void Mereni()
        //{
        //    try
        //    {
        //        string url = "http://192.168.1.254/fresh.xml";

        //        using (HttpClient client = new HttpClient())
        //        {
        //            string xmlContent = client.GetStringAsync(url).Result;

        //            XDocument xmlDoc = XDocument.Parse(xmlContent);


        //            List<InputValue> inputs = new List<InputValue>();
        //            foreach (var input in xmlDoc.Descendants("input"))
        //            {
        //                inputs.Add(new InputValue
        //                {
        //                    Id = int.Parse(input.Attribute("id")?.Value ?? "0"),
        //                    Name = input.Attribute("name")?.Value?.Trim(),
        //                    Unit = input.Attribute("unit")?.Value?.Trim(),
        //                    Value = float.TryParse(input.Attribute("val")?.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float v) ? v : 0,
        //                    Status = int.Parse(input.Attribute("stat")?.Value ?? "0")
        //                });
        //            }


        //            float hodnota = 0;

        //            // Vyber konkrétní vstup – například ID 1
        //            var kanal1 = inputs.Find(x => x.Id == 1);
        //            var kanal2 = inputs.Find(x => x.Id == 2);
        //            var kanal3 = inputs.Find(x => x.Id == 3);
        //            var kanal4 = inputs.Find(x => x.Id == 4);





        //            if (kanal1 != null)
        //            {
        //                MessageBox.Show($"Kanál {kanal1.Id} - {kanal1.Name}: {kanal1.Value} {kanal1.Unit}",
        //                                "Výsledek", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Kanál 1 nebyl nalezen.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }


        //            if (kanal2 != null)
        //            {
        //                MessageBox.Show($"Kanál {kanal2.Id} - {kanal2.Name}: {kanal2.Value} {kanal2.Unit}",
        //                                "Výsledek", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Kanál 2 nebyl nalezen.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }

        //            if (kanal1 != null)
        //            {
        //                MessageBox.Show($"Kanál {kanal3.Id} - {kanal3.Name}: {kanal3.Value} {kanal3.Unit}",
        //                                "Výsledek", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Kanál 3 nebyl nalezen.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }

        //            if (kanal4 != null)
        //            {
        //                MessageBox.Show($"Kanál {kanal4.Id} - {kanal4.Name}: {kanal4.Value} {kanal4.Unit}",
        //                                "Výsledek", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Kanál 4 nebyl nalezen.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Chyba při načítání: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //} 
        #endregion


        private void Mereni()
        {
            try
            {
                //string url = "http://192.168.1.254/fresh.xml";
                string url = Settings.PapouchURL;
                

                using (HttpClient client = new HttpClient())
                {

                    string xmlContent = string.Empty;

                    try
                    {
                       xmlContent = client.GetStringAsync(url).Result;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Chyba při měření, špatně zadaná URL adresa Papouch: ", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }




                    XDocument xmlDoc = XDocument.Parse(xmlContent);

                    List<InputValue> inputs = new List<InputValue>();
                    foreach (var input in xmlDoc.Descendants("input"))
                    {
                        inputs.Add(new InputValue
                        {
                            Id = int.Parse(input.Attribute("id")?.Value ?? "0"),
                            Name = input.Attribute("name")?.Value?.Trim(),
                            Unit = input.Attribute("unit")?.Value?.Trim(),
                            Value = float.TryParse(input.Attribute("val")?.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float v) ? v : 0,
                            Status = int.Parse(input.Attribute("stat")?.Value ?? "0")
                        });
                    }

                    string vystup = "";
                    for (int i = 1; i <= 4; i++)
                    {
                        var kanal = inputs.Find(x => x.Id == i);
                        if (kanal != null)
                        {
                            vystup += $"Kanál {kanal.Id} - {kanal.Name.Trim()}: {kanal.Value} {kanal.Unit}\n";
                        }
                        else
                        {
                            vystup += $"Kanál {i} nebyl nalezen.\n";
                        }
                    }

                    MessageBox.Show(vystup, "Výsledky měření", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při načítání: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }


    public class InputValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public float Value { get; set; }
        public int Status { get; set; }
    }
}
