using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Konzola.Extensions;
using FirebirdSql.Data.FirebirdClient;
using System.Reflection;

namespace Konzola.Servis
{
    public enum TypeData
    {
        Unknow,
        BindingSource,
        DataTable,
        IEnumerable,
        Object,
        Typ
    }

    public partial class FormZdrojePohybReportViewer : Form
    {

        #region Predavane data

        /// <summary>
        /// Binding Source pro report
        /// </summary>
        private System.Windows.Forms.BindingSource _bindingsource = null;
        public System.Windows.Forms.BindingSource bindingsource
        {
            set { _bindingsource = value; }
            get { return _bindingsource; }

        }


        // Datatable je moc obecne, pro konkretne
        /// <summary>
        /// DataTable predavany do reportu
        /// </summary>
        private DataTable _DataTable = null;
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable DataTable
        {
            set
            {
                _DataTable = (DataTable)value;
            }

            get
            {
                return (Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable)_DataTable;
            }
        }

        /// <summary>
        /// IEnumerable predavane data
        /// </summary>
        private System.Collections.IEnumerable _ienumerable = null;
        public System.Collections.IEnumerable ienumerable
        {
            set { _ienumerable = value; }
            get { return _ienumerable; }

        }

        /// <summary>
        /// Objekt
        /// </summary>
        private object _Objekt = null;
        public object Objekt
        {
            set { _Objekt = value; }
            get { return _Objekt; }
        }

        /// <summary>
        /// Type
        /// </summary>
        private Type _typ = null;
        public Type typ
        {
            set { _typ = value; }
            get { return _typ; }
        }

        #endregion

        #region Predavane data o reportu

        private TypeData _typedata = TypeData.Unknow;
        public TypeData typedata
        {
            set { _typedata = value; }
            get { return _typedata; }
        }


        private string _Name = null;
        public string NazevDataTable
        {
            set { _Name = value; }
            get { return _Name; }
        }


        private string _Path = null;
        public string Path
        {
            set { _Path = value; }
            get { return _Path; }
        }

        private Microsoft.Reporting.WinForms.ReportParameter[] _Params = null;
        public Microsoft.Reporting.WinForms.ReportParameter[] Params
        {
            set { _Params = value; }
            get { return _Params; }
        }

        #endregion


        public FormZdrojePohybReportViewer()
        {
            InitializeComponent();

       }

        private void FormPohybyList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                string[] typy = new string[] { "Excel", "EXCEL", "WORD", "Word" };


                DisableUnwantedExportFormat(this.reportViewer1, typy);
                //EnableAllExportFormat(this.reportViewer1);



                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                this.reportViewer1.LocalReport.ReportPath = _Path;

                switch (_typedata)
                {
                    case TypeData.BindingSource:
                        this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(_Name, _bindingsource));
                        break;
                    case TypeData.DataTable:
                        this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(_Name, _DataTable));
                        break;
                    case TypeData.IEnumerable:
                        this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(_Name, _ienumerable));
                        break;
                    case TypeData.Object:
                        this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(_Name, _Objekt));
                        break;
                    case TypeData.Typ:
                        this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(_Name, _typ));
                        break;
                    default:
                        break;
                }

                if (_Params != null)
                {
                    this.reportViewer1.LocalReport.EnableExternalImages = true;
                    this.reportViewer1.LocalReport.SetParameters(_Params);
                }

               
                //this.reportViewer1
                
                this.reportViewer1.RefreshReport();

                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //this.reportViewer1.RefreshReport();
        }

        private void EnableAllExportFormat(Microsoft.Reporting.WinForms.ReportViewer reportViewer)
        {
            try
            {


                FieldInfo info;
                foreach (Microsoft.Reporting.WinForms.RenderingExtension extension in reportViewer.LocalReport.ListRenderingExtensions())
                {

                        info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                        info.SetValue(extension, true);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), this.Name, MessageBoxButtons.OK);
                throw;
            }
        }


        public void DisableUnwantedExportFormat(Microsoft.Reporting.WinForms.ReportViewer ReportViewerID, string[] typy)
        {
            try
            {
                

                FieldInfo info;
                foreach (Microsoft.Reporting.WinForms.RenderingExtension extension in ReportViewerID.LocalReport.ListRenderingExtensions())
                {
                    if ((extension.Name == typy[0]) || (extension.Name == typy[1]) || (extension.Name == typy[2]) || (extension.Name == typy[3]))
                    {
                        info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                        info.SetValue(extension, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), this.Name, MessageBoxButtons.OK);
                throw;
            }
        }

        private void FormPohybyList_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    //todo enter
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

    }
}
