using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using System.Xml;

namespace Fask.Graphic
{
    public partial class DataGrid : System.Windows.Forms.DataGrid, ISupportInitialize
    {
        public event EventHandler CurrentRowIndexChanged;

        public DataGrid()
            : base()
        {
            InitializeComponent();
        }

        private Point souradniceUpXY = Point.Empty;
        private Point souradniceDownXY = Point.Empty;
        private Point souradniceClickXY = Point.Empty;
        private Point souradniceMoveXY = Point.Empty;
        private Point souradniceMultiSelCellChngXY = Point.Empty;
        private int rowIndexMultiSelCellChng = -1;
        private int rowIndexClick = -1;
        private HScrollBar hscroll = null;
        private int sloupec1 = -1;
        private int sloupec2 = -1;
        private int firstVisColumn = 0;
        private DataGridTableStyle vsechySloupce = null;
        ArrayList selectedRows = new ArrayList();
        //private int indexRomRemove = -1;
        //private bool rowChange = false;
        
        private Keys _keyScrollUp = Keys.D2;

        public Keys KeyScrollUp
        {
            get { return _keyScrollUp; }
            set { _keyScrollUp = value; }
        }

        private Keys _keyScrollDown = Keys.D5;

        public Keys KeyScrollDown
        {
            get { return _keyScrollDown; }
            set { _keyScrollDown = value; }
        }

        private bool _defaultScroll = false;

        public bool DefaultScroll
        {
            get { return _defaultScroll; }
            set { _defaultScroll = value; }
        }

        private bool _fullRowSelect = false;

        public bool FullRowSelect
        {
            get { return _fullRowSelect; }
            set { _fullRowSelect = value; }
        }

        private bool _multiSelect = true;
        public bool MultiSelect
        {
            get { return _multiSelect; }
            set { _multiSelect = value; }
        }

        private List<int> vybraneRadkyCislo = new List<int>();

        [System.ComponentModel.DefaultValue(23)]
        public int DefaultRowHeight
        {
            get
            {
                try
                {
                    FieldInfo fi = this.GetType().GetField("m_cyRow",
                        BindingFlags.NonPublic |
                        BindingFlags.Static |
                        BindingFlags.Instance);
                    return (int)fi.GetValue(this);

                }
                catch 
                {
                    return 23;
                }
            }
            set
            {
                try
                {
                    FieldInfo fi = this.GetType().GetField("m_cyRow",
                        BindingFlags.NonPublic |
                        BindingFlags.Static |
                        BindingFlags.Instance);
                    fi.SetValue(this, value);
                    this.GetType().GetMethod("_DataRebind",
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance).Invoke(this, new object[] { });
                    this.Invalidate();

                }
                catch
                {                    
                }
            }
        }

        public void SetRowHeight(int nRow, int cy)
        {
            ArrayList arrRows = (ArrayList)this.GetType().GetField("m_rlrow",
                         BindingFlags.NonPublic |
                         BindingFlags.Static |
                         BindingFlags.Instance).GetValue(this);
            object row = arrRows[nRow];
            row.GetType().GetField("m_cy",
                         BindingFlags.NonPublic |
                         BindingFlags.Static |
                         BindingFlags.Instance).SetValue(row, cy);
        }

        public int GetRowHeight(int nRow)
        {
            ArrayList arrRows = (ArrayList)this.GetType().GetField("m_rlrow",
                         BindingFlags.NonPublic |
                         BindingFlags.Static |
                         BindingFlags.Instance).GetValue(this);
            object row = arrRows[nRow];
            return (int)(row.GetType().GetField("m_cy",
                         BindingFlags.NonPublic |
                         BindingFlags.Static |
                         BindingFlags.Instance).GetValue(row));
        }

        protected override void OnMouseUp(MouseEventArgs mea)
        {
            if (!_multiSelect)
            {
                base.OnMouseUp(mea);

                souradniceUpXY = new Point(mea.X, mea.Y);
                try
                {
                    System.Windows.Forms.DataGrid.HitTestInfo hittestinfo = this.HitTest(mea.X, mea.Y);
                    if (hittestinfo.Type == DataGrid.HitTestType.RowResize)
                    {
                        int rowH = this.GetRowHeight(hittestinfo.Row);
                        this.DefaultRowHeight = rowH;
                    }
                    else if (hittestinfo.Type == HitTestType.ColumnResize || hittestinfo.Type == HitTestType.ColumnHeader)
                    {
                        if (this.TableStyles[0].GridColumnStyles[hittestinfo.Column].Width < 20)
                            TableStyles[0].GridColumnStyles[hittestinfo.Column].Width = -1;
                    }
                    /*
                    if (this._multiSelect)
                    {
                        for (int i = 0; i < vybraneRadkyCislo.Count; i++)
                        {
                            this.Select(vybraneRadkyCislo[i]);
                        }

                        if (indexRomRemove != -1)
                        {
                            this.UnSelect(indexRomRemove);
                            indexRomRemove = -1;
                        }

                        if (!rowChange)
                        {
                            if(this.IsSelected(this.CurrentRowIndex))
                                this.UnSelect(this.CurrentRowIndex);

                            rowChange = false;
                        }
                   
                    }
                     */
                }
                catch { }
            }
            else
            {
                try
                {
                    /****** PRIDANO ******/
                    base.OnMouseUp(mea);

                    souradniceUpXY = new Point(mea.X, mea.Y);

                    System.Windows.Forms.DataGrid.HitTestInfo hittestinfo2 = this.HitTest(mea.X, mea.Y);
                    if (hittestinfo2.Type == DataGrid.HitTestType.RowResize)
                    {
                        int rowH = this.GetRowHeight(hittestinfo2.Row);
                        this.DefaultRowHeight = rowH;
                    }
                    else if (hittestinfo2.Type == HitTestType.ColumnResize || hittestinfo2.Type == HitTestType.ColumnHeader)
                    {
                        if (this.TableStyles[0].GridColumnStyles[hittestinfo2.Column].Width < 20)
                            TableStyles[0].GridColumnStyles[hittestinfo2.Column].Width = -1;
                    }

                    /********************/

                    System.Windows.Forms.DataGrid.HitTestInfo hittestinfo = this.HitTest(DataGrid.MousePosition.X, DataGrid.MousePosition.Y);
                    if (hittestinfo.Type == HitTestType.RowHeader || DataGrid.MousePosition.X <= 20)
                    {
                        //base.OnMouseUp(mea);

                       /* if ((rowIndexClick != -1) && (rowIndexClick != (hittestinfo.Row-1)))
                        {
                            for (int i = rowIndexClick; i < hittestinfo.Row; i++)
                            {
                                this.Select(int.Parse(selectedRows[i].ToString()));
                                selectedRows.Add(i);
                            }
                            rowIndexClick = -1;
                        }
                        else
                        {
                        */
                            int c = this.CurrentRowIndex;
                            if (selectedRows.Contains(c))
                            {
                                this.UnSelect(c);
                                selectedRows.Remove(c);
                            }
                            else
                            {
                                this.Select(c);
                                selectedRows.Add(c);
                            }
                            for (int i = 0; i < selectedRows.Count; i++)
                            {
                                this.Select(int.Parse(selectedRows[i].ToString()));
                            }
                       // }
                    }
                    else if (hittestinfo.Type == HitTestType.Cell || hittestinfo.Type == HitTestType.None)
                    {
                        //base.OnMouseUp(mea);

                        for (int i = 0; i < selectedRows.Count; i++)
                        {
                            this.UnSelect(int.Parse(selectedRows[i].ToString()));
                        }
                        selectedRows.Clear();

                        int lastRow = hittestinfo.Type == HitTestType.Cell ? (hittestinfo.Row - 1) : (this.BindingContext[this.DataSource].Count - 1);
                        for (int i = rowIndexMultiSelCellChng; i <= lastRow; i++)
                        {
                            this.Select(i);
                            selectedRows.Add(i);
                        }
                    }
                }
                catch
                { }
            }
        }

        protected override void OnMouseMove(MouseEventArgs mea)
        {
            base.OnMouseMove(mea);
            souradniceMoveXY = new Point(mea.X, mea.Y);
            try
            {
                System.Windows.Forms.DataGrid.HitTestInfo hittestinfo = this.HitTest(mea.X, mea.Y);
                if (hittestinfo.Type == HitTestType.ColumnResize || hittestinfo.Type == HitTestType.ColumnHeader)
                {
                    if (this.TableStyles[0].GridColumnStyles[hittestinfo.Column].Width < 20)
                        TableStyles[0].GridColumnStyles[hittestinfo.Column].Width = -1;
                }
            }
            catch { }

        }

        protected override void OnMouseDown(MouseEventArgs mea)
        {
            //rowChange = true;

            //if(this.IsSelected(this.CurrentRowIndex))
            //    rowChange = false;

            base.OnMouseDown(mea);
            souradniceDownXY = new Point(mea.X, mea.Y);
           
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            try
            {   // obnoveni vsech "schovanych" sloupcu...
                System.Windows.Forms.DataGrid.HitTestInfo hittestinfo = this.HitTest(DataGrid.MousePosition.X, DataGrid.MousePosition.Y);
                if (hittestinfo.Type == HitTestType.RowHeader)
                {
                    for (int i = 0; i < this.TableStyles[0].GridColumnStyles.Count; i++)
                    {
                        if (this.TableStyles[0].GridColumnStyles[i].Width < 0)
                            TableStyles[0].GridColumnStyles[i].Width = 40;
                    }
                }
            }
            catch
            {
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (!_multiSelect)
            {
                base.OnClick(e);
                try
                {
                    souradniceClickXY = new Point(DataGrid.MousePosition.X, DataGrid.MousePosition.Y);

                    int diff = 35;
                    Rectangle r = new Rectangle(souradniceDownXY.X - 15, souradniceDownXY.Y - 15, diff, diff);
                    //if (souradniceDownXY != souradniceUpXY)
                    //if (!r.Contains(souradniceUpXY))

                    System.Windows.Forms.DataGrid.HitTestInfo hittestinfo = this.HitTest(souradniceClickXY.X, souradniceClickXY.Y);
                    if (hittestinfo.Type == HitTestType.ColumnResize || hittestinfo.Type == HitTestType.ColumnHeader)
                    {
                        for (int i = 0; i < this.TableStyles[0].GridColumnStyles.Count; i++)
                        {
                            if (this.TableStyles[0].GridColumnStyles[i].Width < 20)
                            {
                                if (TableStyles[0].GridColumnStyles[i].Width != -1)
                                {
                                    TableStyles[0].GridColumnStyles[i].Width = -1;
                                    if (i == this.TableStyles[0].GridColumnStyles.Count - 1)
                                        return;
                                }
                                //return;
                            }
                        }
                    }

                    if (!r.Contains(souradniceClickXY.X, souradniceDownXY.Y))
                    {
                        return;
                    }

                    try
                    {
                        Point pdg = this.PointToClient(DataGrid.MousePosition);
                        DataGrid.HitTestInfo hti = this.HitTest(pdg.X, pdg.Y);
                        firstVisColumn = this.FirstVisibleColumn;

                        if (sloupec1 == hti.Column)
                            return;

                        Point dgCell = new Point(this.CurrentCell.RowNumber, this.CurrentCell.ColumnNumber);

                        if (hti.Type == DataGrid.HitTestType.ColumnHeader)
                        {            //DataGridTableStyle vsechySloupce = null;
                            if (sloupec1 < 0)
                            {
                                sloupec1 = hti.Column;
                            }
                            else
                            {
                                int pocetSloupcu = this.TableStyles[0].GridColumnStyles.Count;
                                vsechySloupce = new DataGridTableStyle();
                                for (int i = 0; i < pocetSloupcu; i++)
                                {
                                    vsechySloupce.GridColumnStyles.Add(this.TableStyles[0].GridColumnStyles[i]);
                                }

                                sloupec2 = hti.Column;
                                this.TableStyles[0].GridColumnStyles.Clear();

                                for (int i = 0; i < pocetSloupcu; i++)
                                {
                                    if (i == sloupec1)
                                        this.TableStyles[0].GridColumnStyles.Add(vsechySloupce.GridColumnStyles[sloupec2]);
                                    else if (i == sloupec2)
                                        this.TableStyles[0].GridColumnStyles.Add(vsechySloupce.GridColumnStyles[sloupec1]);
                                    else
                                        this.TableStyles[0].GridColumnStyles.Add(vsechySloupce.GridColumnStyles[i]);
                                }

                                this.CurrentCell = new DataGridCell(dgCell.X, dgCell.Y);

                                foreach (Control prvekDataGridu in this.Controls)
                                {
                                    if (prvekDataGridu is HScrollBar)
                                    {
                                        ((HScrollBar)prvekDataGridu).Value += (((HScrollBar)prvekDataGridu).SmallChange) * firstVisColumn * (2);
                                        if (((HScrollBar)prvekDataGridu).Value + (((HScrollBar)prvekDataGridu).Width + 25) >= ((HScrollBar)prvekDataGridu).Maximum)
                                            ((HScrollBar)prvekDataGridu).Value -= ((HScrollBar)prvekDataGridu).SmallChange;
                                    }
                                }

                                //this.FirstVisibleColumn = firstVisColumn;
                                sloupec1 = -1;
                                sloupec2 = -1;
                            }
                        }
                        else
                        {
                            sloupec1 = -1;
                            sloupec1 = -1;
                        }
                    }
                    catch
                    {
                        throw;
                    }

                }
                catch
                {
                }
            }
            else
            {
                base.OnClick(e);

                #region zmena poradi sloupcu

                Point pdg = this.PointToClient(DataGrid.MousePosition);
                DataGrid.HitTestInfo hti = this.HitTest(pdg.X, pdg.Y);
                firstVisColumn = this.FirstVisibleColumn;

                if (hti.Type == DataGrid.HitTestType.ColumnHeader)
                {
                    if (sloupec1 == hti.Column)
                    {
                        sloupec1 = -1;
                        return; //kliknul znova na stejny sloupec
                    }

                    Point dgCell = new Point(this.CurrentCell.RowNumber, this.CurrentCell.ColumnNumber);

                    // if (hti.Type == DataGrid.HitTestType.ColumnHeader)
                    // {
                    if (sloupec1 < 0) //teprve kliknul na prvni sloupec
                    {
                        sloupec1 = hti.Column;
                    }
                    else
                    { //kliknul na druhy sloupec
                        int pocetSloupcu = this.TableStyles[0].GridColumnStyles.Count;
                        vsechySloupce = new DataGridTableStyle();
                        for (int i = 0; i < pocetSloupcu; i++)
                        {
                            vsechySloupce.GridColumnStyles.Add(this.TableStyles[0].GridColumnStyles[i]);
                        }

                        sloupec2 = hti.Column;
                        this.TableStyles[0].GridColumnStyles.Clear();

                        for (int i = 0; i < pocetSloupcu; i++)
                        {
                            if (i == sloupec1)
                                this.TableStyles[0].GridColumnStyles.Add(vsechySloupce.GridColumnStyles[sloupec2]);
                            else if (i == sloupec2)
                                this.TableStyles[0].GridColumnStyles.Add(vsechySloupce.GridColumnStyles[sloupec1]);
                            else
                                this.TableStyles[0].GridColumnStyles.Add(vsechySloupce.GridColumnStyles[i]);
                        }

                        this.CurrentCell = new DataGridCell(dgCell.X, dgCell.Y);

                        foreach (Control prvekDataGridu in this.Controls)
                        {
                            if (prvekDataGridu is HScrollBar)
                            {
                                ((HScrollBar)prvekDataGridu).Value += (((HScrollBar)prvekDataGridu).SmallChange) * firstVisColumn * (2);
                                if (((HScrollBar)prvekDataGridu).Value + (((HScrollBar)prvekDataGridu).Width + 25) >= ((HScrollBar)prvekDataGridu).Maximum)
                                    ((HScrollBar)prvekDataGridu).Value -= ((HScrollBar)prvekDataGridu).SmallChange;
                            }
                        }

                        sloupec1 = -1;
                        sloupec2 = -1;
                    }
                }
                else
                {
                    sloupec1 = -1;
                    sloupec1 = -1;
                }

                #endregion

                System.Windows.Forms.DataGrid.HitTestInfo hittestinfo = this.HitTest(DataGrid.MousePosition.X, DataGrid.MousePosition.Y);
                if (hittestinfo.Type == HitTestType.RowHeader || DataGrid.MousePosition.X <= 20)
                {
                   // base.OnClick(e);
                    int c = this.CurrentRowIndex;
                    rowIndexClick = c;
                    this.Select(c);
                }
                else if (hittestinfo.Type == HitTestType.Cell || hittestinfo.Type == HitTestType.None)
                {
                  //  base.OnClick(e);
                    //souradniceMultiSelCellChngXY.X = DataGrid.MousePosition.X;
                    //souradniceMultiSelCellChngXY.Y = DataGrid.MousePosition.Y;
                    // int c = this.CurrentRowIndex;
                    // this.Select(c);
                }
            }
        }

        /// <summary>
        /// Pocet polozek po kterych se bude scrollovat gridem
        /// </summary>
        [System.ComponentModel.DefaultValue(8)]
        public int ScrollItemCount
        {
            get
            {
                int vrc = this.VisibleRowCount;
                return vrc > 1 ? vrc - 1 : vrc; //posledni polozka, muze byt napul viditelna ...
            }
        }

        public void Save(string file)
        {
            System.Xml.XmlWriter xmlwrite = null;
            try
            {
                xmlwrite = System.Xml.XmlWriter.Create(file);

                xmlwrite.WriteStartElement("DataGrid");

                for (int i = 0; i < this.TableStyles[0].GridColumnStyles.Count; i++)
                {
                    xmlwrite.WriteElementString("Header", this.TableStyles[0].GridColumnStyles[i].HeaderText);
                    xmlwrite.WriteElementString("MappingName", this.TableStyles[0].GridColumnStyles[i].MappingName.ToString());
                    xmlwrite.WriteElementString("Width", this.TableStyles[0].GridColumnStyles[i].Width.ToString());
                }
                xmlwrite.WriteElementString("Row", this.DefaultRowHeight.ToString());

                xmlwrite.WriteEndElement();
                xmlwrite.Close();
                xmlwrite = null;
            }
            catch
            { xmlwrite.Close(); }
        }

        public void Load(string file)
        {

            if (!System.IO.File.Exists(file))
                return;

            System.Xml.XmlDocument xmldoc = null;
            try
            {
                xmldoc = new System.Xml.XmlDocument();
                //int i = 0;
                xmldoc.Load(file);
                XmlNode node = xmldoc.SelectSingleNode("DataGrid");
                 
                DataGridTableStyle myGridTableStyle = new DataGridTableStyle();
                DataGridTextBoxColumn textColumn = null;


                myGridTableStyle.MappingName = this.TableStyles[0].MappingName;
                this.TableStyles.Clear(); 

                if (node != null)
                {
                    foreach (XmlNode chnode in node.ChildNodes)
                    {
                        if (textColumn == null)
                            textColumn = new DataGridTextBoxColumn();

                        switch (chnode.LocalName)
                        {
                            case "Header": textColumn.HeaderText = (chnode.InnerText); break;
                            case "MappingName":
                                textColumn.MappingName = (chnode.InnerText);
                                break;
                            case "Width": 
                                textColumn.Width = Int32.Parse(chnode.InnerText);
                                myGridTableStyle.GridColumnStyles.Add(textColumn);
                                textColumn = null;
                                break;
                            case "Row": this.DefaultRowHeight = Int32.Parse(chnode.InnerText); break;
                            default: break;

                            /*case "Header": this.TableStyles[0].GridColumnStyles[i].HeaderText = (chnode.InnerText); break;
                            case "MappingName":
                                this.TableStyles[0].GridColumnStyles[i].MappingName = (chnode.InnerText);
                                break;
                            case "Width": this.TableStyles[0].GridColumnStyles[i].Width = Int32.Parse(chnode.InnerText); i++; break;
                            case "Row": this.DefaultRowHeight = Int32.Parse(chnode.InnerText); break;
                            default: break;*/
                        }
                    }
                }

                this.TableStyles.Add(myGridTableStyle);
            }
            catch (Exception ex)
            {}
        }

        protected override void OnKeyDown(KeyEventArgs kea)
        {
            if (_defaultScroll)
            {
                base.OnKeyDown(kea);
                return;
            }

            if (!kea.Alt && !kea.Control && !kea.Shift)
            {
                //if (kea.KeyValue == _keyScrollUp + (int)Keys.D0)
                if (kea.KeyCode == _keyScrollUp)
                {
                    this.ScrollUp();
                }
                //else if (kea.KeyValue == _keyScrollDown + (int)Keys.D0)
                else if (kea.KeyCode == _keyScrollDown)
                {
                    this.ScrollDown();
                }
                else
                {
                    base.OnKeyDown(kea);
                    return;
                }
            }
            else
            {
                base.OnKeyDown(kea);
                return;
            }

            kea.Handled = true;
        }

        /// <summary>
        /// Listovani gridem dolu po ScrollItemCount
        /// </summary>
        public void ScrollDown()
        {
            //listovani datagridem nahoru
            int _celkemPolozek = this.BindingContext[this.DataSource].Count;
            try
            {
                if (this.CurrentRowIndex < 0)
                {
                    return;
                }
                //else if (this.CurrentRowIndex + this.VisibleRowCount >= _celkemPolozek)
                else if (this.CurrentRowIndex + this.ScrollItemCount >= _celkemPolozek)
                {
                    this.CurrentRowIndex = _celkemPolozek - 1;
                }
                else
                {
                    //this.CurrentRowIndex += this.VisibleRowCount;
                    this.CurrentRowIndex += this.ScrollItemCount;
                }
            //    if (this.CurrentRowIndex < 0)
            //    {
            //        return;
            //    }
            //    else if (this.CurrentRowIndex + _scrollItemCount >= _celkemPolozek)
            //    {
            //        this.CurrentRowIndex = _celkemPolozek - 1;
            //    }
            //    else
            //    {
            //        this.CurrentRowIndex += _scrollItemCount;
            //    }
            }
            catch { }
        }

        /// <summary>
        /// Listovani gridem nahoru po ScrollItemCount
        /// </summary>
        public void ScrollUp()
        {
            //listovani datagridem nahoru
            try
            {
                if (this.CurrentRowIndex < 0)
                {
                    return;
                }
                //else if (this.CurrentRowIndex - this.VisibleRowCount < 0)
                else if (this.CurrentRowIndex - this.ScrollItemCount < 0)
                {
                    this.CurrentRowIndex = 0;
                }
                else
                {
                    //this.CurrentRowIndex -= this.VisibleRowCount;
                    this.CurrentRowIndex -= this.ScrollItemCount;
                }
                //if (this.CurrentRowIndex < 0)
                //{
                //    return;
                //}
                //else if (this.CurrentRowIndex - _scrollItemCount < 0)
                //{
                //    this.CurrentRowIndex = 0;
                //}
                //else
                //{
                //    this.CurrentRowIndex -= _scrollItemCount;
                //}

            }
            catch { }
        }

        public new int CurrentRowIndex
        {
            get { return base.CurrentRowIndex; }
            set
            {
                try
                {
                    if (!this._multiSelect)
                        this.UnSelect(this.CurrentRowIndex);

                    base.CurrentRowIndex = value;
                    if (_fullRowSelect)
                    {
                        if (this._multiSelect)
                        {
                            if (this.IsSelected(CurrentRowIndex))
                                this.UnSelect(this.CurrentRowIndex);
                            else
                                this.Select(this.CurrentRowIndex);
                        }
                        else
                            this.Select(this.CurrentRowIndex);
                    }
                    this.OnCurrentRowIndexChanged();
                }
                catch
                {
                
                }
            }
        }

        protected override void OnCurrentCellChanged(EventArgs e)
        {
            /* stare...
            if (!this._multiSelect)
            {
                this.UnSelect(this.CurrentRowIndex);
            }

                base.OnCurrentCellChanged(e);

                if (_fullRowSelect)
                {
                    if (this._multiSelect)
                    {
                        if (this.IsSelected(CurrentRowIndex))
                            this.UnSelect(this.CurrentRowIndex);
                        else
                            this.Select(this.CurrentRowIndex);
                    }
                    else
                        this.Select(this.CurrentRowIndex);
                }
            */

            if (!this._multiSelect)
            {
                this.UnSelect(this.CurrentRowIndex);


                base.OnCurrentCellChanged(e);

                if (_fullRowSelect)
                {
                    if (this._multiSelect)
                    {
                        if (this.IsSelected(CurrentRowIndex))
                            this.UnSelect(this.CurrentRowIndex);
                        else
                            this.Select(this.CurrentRowIndex);
                    }
                    else
                        this.Select(this.CurrentRowIndex);
                }
            }
            else
            {
                
                System.Windows.Forms.DataGrid.HitTestInfo hittestinfo = this.HitTest( DataGrid.MousePosition.X,  DataGrid.MousePosition.Y);
                if (hittestinfo.Type == HitTestType.RowHeader || DataGrid.MousePosition.X <= 20)
                {
                    base.OnCurrentCellChanged(e);
                    int c = this.CurrentRowIndex;
                    this.Select(c);
                }
                else if (hittestinfo.Type == HitTestType.Cell || hittestinfo.Type == HitTestType.None)
                {
                    base.OnCurrentCellChanged(e);
                    souradniceMultiSelCellChngXY.X = DataGrid.MousePosition.X;
                    souradniceMultiSelCellChngXY.Y = DataGrid.MousePosition.Y;
                    rowIndexMultiSelCellChng = this.CurrentRowIndex;
                   // this.Select(c);
                }
                //this.OnClick(e);
            }
        }

        protected virtual void OnCurrentRowIndexChanged()
        {
            if (CurrentRowIndexChanged != null)
            {
                this.CurrentRowIndexChanged(this, new EventArgs());
            }
        }



        #region ISupportInitialize Members

        void ISupportInitialize.BeginInit()
        {
            //throw new NotImplementedException();
        }

        void ISupportInitialize.EndInit()
        {
            //throw new NotImplementedException();
        }

        #endregion

        public List<DataRow> SelectedRows
        {
            get
            {
                List<DataRow> selectedRows = new List<DataRow>();
                try
                {
                    CurrencyManager cm = (CurrencyManager)this.BindingContext[this.DataSource];
                    DataView dv = ((BindingSource)cm.List).List as DataView;

                    for (int i = 0; i < dv.Count; i++)
                    {
                        if (this.IsSelected(i))
                            selectedRows.Add(dv[i].Row);
                    }

                }
                catch { }
                return selectedRows;
            }
        }

        public List<int> SelectedRowsIndex
        {
            get
            {
                List<int> selectedRowsIndex = new List<int>();
                try
                {
                    CurrencyManager cm = (CurrencyManager)this.BindingContext[this.DataSource];
                    DataView dv = ((BindingSource)cm.List).List as DataView;

                    for (int i = 0; i < dv.Count; i++)
                    {
                        if (this.IsSelected(i))
                            selectedRowsIndex.Add(i);
                    }

                }
                catch { }
                return selectedRowsIndex;
            }
        }
    }
}
