using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Xsl;

namespace PohodaImportVolitelneParametry.XML_Transformace
{
    public partial class Form_Test : Form
    {

        private static string LokalDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        private string CestaXML = Path.Combine(LokalDir, "XML_Transformace", "Kucharka.xml");
        private string CestaHTML = Path.Combine(LokalDir, "XML_Transformace", "Kucharka.html");
        private string CestaXSLT = Path.Combine(LokalDir, "XML_Transformace", "Kucharka.xslt");

        public Form_Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateHTML();
        }

        private void GenerateHTML()
        {
            System.Xml.Xsl.XslCompiledTransform xt = new System.Xml.Xsl.XslCompiledTransform();
            xt.Load(CestaXSLT);
            xt.Transform(CestaXML, CestaHTML);
        }
    }
}
