using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Collections;
using System.Xml;
using System.IO;

namespace Fask.Graphic
{
    public class DataGrid2 : DataGrid, ISupportInitialize
    {
        /// <summary>
        /// Udalost pro zachyceni zmeny aktualniho vybraneho radku(indexu)
        /// </summary>
        public event EventHandler CurrentRowIndexChanged;
        /// <summary>
        /// Udalost pri zmene razeni zdroje, pri dvojkliku na hlavicku, pokud je razeni dvojklikem povoleno ...
        /// </summary>
        public event EventHandler SortChanged;

        Point pMouseDown = new Point(0, 0);
        Point pMouseUp = new Point(0, 0);
        Point pMouseMove = new Point(0, 0);
        Brush bMouseInfo = new System.Drawing.SolidBrush(Color.Black);
        StringFormat sfMouseInfo = new StringFormat();
        Pen penSortTriangle = null;
        Brush bSortTriangle = null;

        public DataGrid2()
            : base()
        {
            //sfMouseInfo.Alignment = StringAlignment.Center;
            sfMouseInfo.LineAlignment = StringAlignment.Center;
            RefreshForeColorObjects();
        }

        /// <summary>
        /// Barva textu popredi
        /// </summary>
        new Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                RefreshForeColorObjects();
            }
        }

        /// <summary>
        /// Interni zmena barvy textu popredi
        /// </summary>
        private void RefreshForeColorObjects()
        {
            penSortTriangle = new Pen(this.ForeColor);
            bSortTriangle = new SolidBrush(this.ForeColor);
        }

        private bool _sortByHeaderDoubleClick = true;
        /// <summary>
        /// Urcuje, zda bude mozne seradit data dvojklikem na sloupec hlavicky gridu
        /// </summary>
        public bool SortByHeaderDoubleClick
        {
            get { return _sortByHeaderDoubleClick; }
            set { _sortByHeaderDoubleClick = value; }
        }

        /// <summary>
        /// Aktualizace razeni
        /// </summary>
        /// <param name="columnIndex">cislo sloupce pro razeni</param>
        private void SortUpdate(int columnIndex)
        {
            string columnName = string.Empty;
            BindingSource dataBindigSource = null;
            DataView dataDataView = null;
            DataTable dataDataTable = null;

            try
            {
                dataBindigSource = this.DataSource as BindingSource;
                dataDataView = this.DataSource as DataView;
                dataDataTable = this.DataSource as DataTable;

                // Get the name of the column that was clicked.
                if (this.TableStyles.Count != 0)
                {
                    if ((dataBindigSource != null) && (!String.IsNullOrEmpty(dataBindigSource.DataMember)))
                        columnName = this.TableStyles[dataBindigSource.DataMember].GridColumnStyles[columnIndex].MappingName;
                    else
                        columnName = this.TableStyles[0].GridColumnStyles[columnIndex].MappingName;
                }

                if (dataBindigSource != null)
                {
                    if (dataBindigSource.Sort == columnName)
                        dataBindigSource.Sort = columnName + " DESC";
                    else if ((dataBindigSource.Sort ?? string.Empty).Trim().Length > 0)
                        dataBindigSource.Sort = string.Empty;
                    else
                        dataBindigSource.Sort = columnName;
                }
                else if (dataDataView != null)
                {
                    if (dataDataView.Sort == columnName)
                        dataDataView.Sort = columnName + " DESC";
                    else if ((dataDataView.Sort ?? string.Empty).Trim().Length > 0)
                        dataDataView.Sort = string.Empty;
                    else
                        dataDataView.Sort = columnName;
                }
            }
            //catch (Exception ex)
            //{
            //    ;
            //} 
            catch { }

            this.OnSortChanged();
        }

        /// <summary>
        /// Fires event "SortChanged" if columns sort order has changed...
        /// </summary>
        protected virtual void OnSortChanged()
        {
            if (this.SortChanged != null)
            {
                this.SortChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Nastavuje sloupce pro razeni gridu
        /// </summary>
        public string Sort
        {
            get
            {
                BindingSource dataBindigSource = null;
                DataView dataDataView = null;
                DataTable dataDataTable = null;

                try
                {
                    dataBindigSource = this.DataSource as BindingSource;
                    dataDataView = this.DataSource as DataView;
                    dataDataTable = this.DataSource as DataTable;

                    if (dataBindigSource != null)
                        return dataBindigSource.Sort ?? string.Empty;
                    else if (dataDataView != null)
                        return dataDataView.Sort ?? string.Empty;
                    else
                        return string.Empty;
                }
                catch { return string.Empty; }                            
            }
            set
            {
                BindingSource dataBindigSource = null;
                DataView dataDataView = null;
                DataTable dataDataTable = null;

                try
                {
                    dataBindigSource = this.DataSource as BindingSource;
                    dataDataView = this.DataSource as DataView;
                    dataDataTable = this.DataSource as DataTable;

                    if (dataBindigSource != null)
                    {
                        try
                        {
                            dataBindigSource.Sort = value;
                        }
                        catch
                        {
                            dataBindigSource.Sort = string.Empty;
                        }
                    }
                    else if (dataDataView != null)
                    {
                        try
                        {
                            dataDataView.Sort = value;
                        }
                        catch 
                        {
                            dataDataView.Sort = string.Empty;
                        }
                    }
                    //else
                    //    ;
                }
                catch 
                {
                }
            }
        }

        private Keys _keyScrollUp = Keys.D2;
        /// <summary>
        /// Klavesa, ktera bude fungovat pro scrolovani o stranku nahoru
        /// </summary>
        public Keys KeyScrollUp
        {
            get { return _keyScrollUp; }
            set { _keyScrollUp = value; }
        }

        private Keys _keyScrollDown = Keys.D5;
        /// <summary>
        /// Klavesa, ktera bude fungovat pro scrolovani o stranku dolu
        /// </summary>
        public Keys KeyScrollDown
        {
            get { return _keyScrollDown; }
            set { _keyScrollDown = value; }
        }

        /// <summary>
        /// Vyska radku
        /// </summary>
        [System.ComponentModel.DefaultValue(23)]
        public int RowHeightDefault
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

        /// <summary>
        /// Nastaveni vysky radku
        /// </summary>
        /// <param name="nRow"></param>
        /// <param name="cy"></param>
        public void RowHeightSet(int nRow, int cy)
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

        /// <summary>
        /// Nastaveni vysky hlavicky
        /// </summary>
        [System.ComponentModel.EditorBrowsable( EditorBrowsableState.Never)]
        public int HeaderColumnHeight
        {
            get
            {
                try
                {
                    return (int)this.GetType().GetField("m_cyColumnHeader",
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance).GetValue(this);
                }
                catch { return 20; }
            }
            //set
            //{
            //    try
            //    {
            //        this.GetType().GetField("m_cyColumnHeader",
            //             BindingFlags.NonPublic |
            //             BindingFlags.Static |
            //             BindingFlags.Instance).SetValue(this, value);

            //    }
            //    catch { }
            //}
        }
         
        /// <summary>
        /// Nastaveni sirky spojovacich car(vizualne)
        /// </summary>
        public int DefaultBorderWidth
        {
            get
            {
                try
                {
                    return (int)this.GetType().BaseType.GetField("v_cxyDefaultBorderWidth",
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static | 
                                 BindingFlags.Instance).GetValue(this);
                }
                catch { return 0; }
            }
        }

        /// <summary>
        /// Minimalni sirka sloupcu/radku
        /// </summary>
        public int MinRowColWidth
        {
            get
            {
                try
                {
                    return (int)this.GetType().BaseType.GetField("v_cxyMinRowColWidth",
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static | 
                                 BindingFlags.Instance).GetValue(this);
                }
                catch { return 0; }
            }
        }

        /// <summary>
        /// Defaultni zanoreni textu v bunkach gridu
        /// </summary>
        public int DefaultTextInset
        {
            get
            {
                try
                {
                    return (int)this.GetType().BaseType.GetField("v_nDefaultTextInset",
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static | 
                                 BindingFlags.Instance).GetValue(this);
                }
                catch { return 0; }
            }
        }

        /// <summary>
        /// Sirka radku hlavicky
        /// </summary>
        public int HeaderRowWidth
        {
            get
            {
                try
                {
                    return (int)this.GetType().BaseType.GetField("v_cxDefaultRowHdrWidth",
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static | 
                                 BindingFlags.Instance).GetValue(this);
                }
                catch { return 0; }
            }
        }

        /// <summary>
        /// Zda je zobrazen horizontalni scrollbar
        /// </summary>
        public HScrollBar ScrollBarHorizontal
        {
            get
            {
                try
                {
                    return (HScrollBar)this.GetType().GetField("m_sbHorz",
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance).GetValue(this);
                }
                catch { return null; }
            }
        }

        /// <summary>
        /// Zda je zobrazen vertikalni scrollbar
        /// </summary>
        public VScrollBar ScrollBarVertical
        {
            get
            {
                try
                {
                    return (VScrollBar)this.GetType().GetField("m_sbVert",
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance).GetValue(this);
                }
                catch { return null; }
            }
        }

        /// <summary>
        /// Vraci vysku radku
        /// </summary>
        /// <param name="nRow"></param>
        /// <returns></returns>
        public int RowHeightGet(int nRow)
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

        /// <summary>
        /// Urcuje, zda bude pouzit "Multiselect"
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool MultiSelect { get; set; }

        /// <summary>
        /// Oznacovani celeho radku pomoci Select
        /// </summary>
        /// <remarks>Pri vlozeni/smazani radku z datasource, je momentalne nutne provest UnSelectAll(pri zmene v bindigu neni udalost pro tuto zmenu a grid si s tim neporadi korektne ...)</remarks>
        new public int CurrentRowIndex
        {
            get { return base.CurrentRowIndex; }
            set
            {
                try
                {
                    int row = base.CurrentRowIndex;
                    if (value < 0) value = 0;
                    base.CurrentRowIndex = value;
                    if (!MultiSelect)
                    {
                        this.UnSelect(row);
                    }
                    this.Select(base.CurrentRowIndex);
                    this.OnCurrentRowIndexChanged();
                }
                catch { }
            }
        }

        /// <summary>
        /// Nastaveni/Zjisteni aktualniho vybraneho radku
        /// </summary>
        public DataRow CurrentRow
        {
            get
            {
                try
                {
                    
                    CurrencyManager cm = (CurrencyManager)this.BindingContext[this.DataSource];
                    DataView dv = null;
                    try
                    {
                        dv = ((BindingSource)cm.List).List as DataView;
                    }
                    catch
                    {
                        try { dv = cm.List as DataView; }
                        catch { }
                    }

                    return dv[this.CurrentRowIndex].Row;

                }
                catch { return null; }
            }
            set
            {
                try
                {                    
                    CurrencyManager cm = (CurrencyManager)this.BindingContext[this.DataSource];
                    DataView dv = null;
                    try
                    {
                        dv = ((BindingSource)cm.List).List as DataView;
                    }
                    catch
                    {
                        try { dv = cm.List as DataView; }
                        catch { }
                    }

                    for (int i = 0; i < dv.Count; i++)
                    {
                        if (value == dv[i].Row)
                        {
                            this.CurrentRowIndex = i;
                            break;
                        }
                    }
                }
                catch { }
            }
        }

        protected virtual void OnCurrentRowIndexChanged()
        {
            if (CurrentRowIndexChanged != null)
            {
                this.CurrentRowIndexChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Slouzi snad jen pro testovaci ucely pro zjisteni informaci o internich promennych pro DLLInvokes...
        /// </summary>
        /// <returns></returns>
        public string GetGridProperties()
        {
            string fields = string.Empty;
            System.Reflection.FieldInfo[] fieldInfos =
             this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            foreach (System.Reflection.FieldInfo fieldInfo in fieldInfos)
            {
                fields += fieldInfo.Name + "=" + fieldInfo.FieldType.ToString() + "\r\n";
            }

            return fields;
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
                else if (this.CurrentRowIndex + this.ScrollItemCount >= _celkemPolozek)
                {
                    this.CurrentCell = new DataGridCell(_celkemPolozek - 1, this.CurrentCell.ColumnNumber);
                }
                else
                {
                    this.CurrentCell = new DataGridCell(this.CurrentCell.RowNumber + this.ScrollItemCount, this.CurrentCell.ColumnNumber);
                }
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
                else if (this.CurrentRowIndex - this.ScrollItemCount < 0)
                {
                    this.CurrentCell = new DataGridCell(0, this.CurrentCell.ColumnNumber);
                }
                else
                {
                    this.CurrentCell = new DataGridCell(this.CurrentCell.RowNumber - this.ScrollItemCount, this.CurrentCell.ColumnNumber);
                }
            }
            catch { }
        }

        /// <summary>
        /// Odznaci vsechny oznacene radky
        /// </summary>
        public void UnSelectAll()
        {
            try
            {
                CurrencyManager cm = (CurrencyManager)this.BindingContext[this.DataSource];
                DataView dv = null;
                try
                {
                    dv = ((BindingSource)cm.List).List as DataView;
                }
                catch
                {
                    try { dv = cm.List as DataView; }
                    catch { }
                }

                for (int i = 0; i < dv.Count; i++)
                {
                    this.UnSelect(i);
                }
            }
            catch { }
        }

        /// <summary>
        /// Seznam vsech oznacenych radku
        /// </summary>
        public List<DataRow> SelectedRowsGet()
        {
            List<DataRow> selectedRowsIndex = new List<DataRow>();
            try
            {
                CurrencyManager cm = (CurrencyManager)this.BindingContext[this.DataSource];
                DataView dv = null;
                try
                {
                    dv = ((BindingSource)cm.List).List as DataView;
                }
                catch
                {
                    try { dv = cm.List as DataView; }
                    catch { }
                }

                for (int i = 0; i < dv.Count; i++)
                {
                    if (this.IsSelected(i))
                        selectedRowsIndex.Add(dv[i].Row);
                }

            }
            catch { }
            return selectedRowsIndex;
        }

        public void SelectedRowsSet(List<DataRow> value)
        {
            try
            {
                CurrencyManager cm = (CurrencyManager)this.BindingContext[this.DataSource];
                DataView dv = null;
                try
                {
                    dv = ((BindingSource)cm.List).List as DataView;
                }
                catch
                {
                    try { dv = cm.List as DataView; }
                    catch { }
                }

                for (int i = 0; i < dv.Count; i++)
                {
                    if (value.Contains(dv[i].Row))
                    {
                        this.Select(i);
                        value.Remove(dv[i].Row);
                    }
                    if (value.Count == 0)
                        break;
                }
            }
            catch { }
        }

        public List<int> SelectedIndicesGet()
        {
            return this.SelectedIndices;
        }
        public void SelectedIndicesSet(List<int> list)
        {
            this.SelectedIndices = list;
        }

        /// <summary>
        /// Seznam vsech oznacenych radku (jejich indexu v datasource)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal List<int> SelectedIndices
        {
            get
            {
                List<int> selectedRowsIndex = new List<int>();
                try
                {
                    CurrencyManager cm = (CurrencyManager)this.BindingContext[this.DataSource];
                    DataView dv = null;
                    try
                    {
                        dv = ((BindingSource)cm.List).List as DataView;
                    }
                    catch
                    {
                        try { dv = cm.List as DataView; }
                        catch { }
                    }

                    for (int i = 0; i < dv.Count; i++)
                    {
                        if (this.IsSelected(i))
                            selectedRowsIndex.Add(i);
                    }

                }
                catch { }
                return selectedRowsIndex;
            }
            set
            {
                try
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        this.Select(value[i]);
                    }

                }
                catch { }
            }
        }


        #region ISupportInitialize Members

        public void BeginInit()
        {
            //throw new NotImplementedException();
        }

        public void EndInit()
        {
            //throw new NotImplementedException();
        }

        #endregion

        HitTestInfo hittestMouseDown = HitTestInfo.Nowhere;
        protected override void OnMouseDown(MouseEventArgs mea)
        {
            pMouseDown = new Point(mea.X, mea.Y);

            if (!this.Focused)
                this.Focus();

            HitTestInfo hitinfo = this.HitTest(pMouseDown.X, pMouseDown.Y);
            hittestMouseDown = hitinfo;
            if (hitinfo.Type == HitTestType.Cell)
            {
                return;
            }

            base.OnMouseDown(mea);

        }

        private bool columnMove = false;
        private int columnMoveIndex = -1;
        protected override void OnMouseUp(MouseEventArgs mea)
        {
            if (hittestMouseDown == HitTestInfo.Nowhere)
                return;

            moveFirstHitTestType = HitTestType.None;
            pMouseUp = new Point(mea.X, mea.Y);

            if (hittestMouseDown.Type != HitTestType.ColumnResize && hittestMouseDown.Type != HitTestType.RowResize)
            {
                HitTestInfo hitinfo = this.HitTest(pMouseUp.X, pMouseUp.Y);
                if (hitinfo.Type == HitTestType.ColumnHeader)
                {
                    if (!columnMove)
                    {
                        columnMove = true;
                        columnMoveIndex = hitinfo.Column; //Column vraci ne index ale poradi !!! ???
                    }
                    else
                    {
                        if (columnMoveIndex != hitinfo.Column)
                        {
                            ColumnsChange(columnMoveIndex, hitinfo.Column);
                            columnMove = false;
                            columnMoveIndex = -1;
                        }
                        else
                        {
                            columnMove = false;
                            columnMoveIndex = -1;
                        }
                    }
                    this.Invalidate();
                    return;
                }


                columnMove = false;
                columnMoveIndex = -1;

                if (hitinfo.Type == HitTestType.Cell)
                {
                    if (MultiSelect)
                    {
                        //region
                        Point pLeftTop = new Point();
                        Point pRigthBottom = new Point();
                        pLeftTop.X = Math.Min(pMouseDown.X, pMouseUp.X);
                        pLeftTop.Y = Math.Min(pMouseDown.Y, pMouseUp.Y);
                        pRigthBottom.X = Math.Max(pMouseDown.X, pMouseUp.X);
                        pRigthBottom.Y = Math.Max(pMouseDown.Y, pMouseUp.Y);

                        HitTestInfo hitinfoMin = this.HitTest(pLeftTop.X, pLeftTop.Y);
                        HitTestInfo hitinfoMax = this.HitTest(pRigthBottom.X, pRigthBottom.Y);
                        if (hitinfoMin.Type == HitTestType.Cell && hitinfoMax.Type == HitTestType.Cell)
                        {
                            for (int i = hitinfoMin.Row; i <= hitinfoMax.Row; i++)
                            {
                                int row = i;
                                bool selected = this.IsSelected(row);
                                if (selected)
                                    this.UnSelect(row);
                                else
                                    this.Select(row);
                            }

                            this.CurrentCell = new DataGridCell(hitinfo.Row, hitinfo.Column);
                            this.Invalidate();
                            return;
                        }
                    }
                    else
                    {
                        this.UnSelect(CurrentRowIndex);
                        this.Select(hitinfo.Row);
                        this.CurrentCell = new DataGridCell(hitinfo.Row, hitinfo.Column);
                        this.Invalidate();
                        return;
                    }
                }
            }

            base.OnMouseUp(mea);

            if (hittestMouseDown.Type == HitTestType.RowResize)
            {
                int rowH = this.RowHeightGet(hittestMouseDown.Row);
                this.RowHeightDefault = rowH;
            }
            else if (hittestMouseDown.Type == HitTestType.ColumnResize)
                ColumnsHide();

        }

        private void ColumnsChange(int fromColumn, int toColumn)
        {
            if (fromColumn == toColumn)
                return;

            try
            {
                int fvcol = this.FirstVisibleColumn;
                List<int> selectedRows = this.SelectedIndices;
                int hsval = this.ScrollBarHorizontal == null ? 0 : this.ScrollBarHorizontal.Value;

                DataGridTableStyle oldTS = TableStyles[0];
                DataGridTableStyle newTS = new DataGridTableStyle();
                newTS.MappingName = oldTS.MappingName;  // Table Name

                //CopyTableStyle(oldTS, newTS);
                // Copy the old TableStyle to new TableStyle

                for (int i = 0; i < oldTS.GridColumnStyles.Count; i++)
                {
                    if (i != fromColumn && fromColumn < toColumn)
                        newTS.GridColumnStyles.Add(oldTS.GridColumnStyles[i]);
                    if (i == toColumn)
                        newTS.GridColumnStyles.Add(oldTS.GridColumnStyles[fromColumn]);
                    if (i != fromColumn && fromColumn > toColumn)
                        newTS.GridColumnStyles.Add(oldTS.GridColumnStyles[i]);
                }
                TableStyles.Remove(oldTS);
                TableStyles.Add(newTS);

                if (this.ScrollBarHorizontal != null)
                    this.ScrollBarHorizontal.Value = hsval;

                this.SelectedIndices = selectedRows;
            }
            catch //(Exception ex)
            {
            }
        }

        private HitTestType moveFirstHitTestType = HitTestType.None;
        protected override void OnMouseMove(MouseEventArgs mea)
        {
            pMouseMove = new Point(mea.X, mea.Y);
            
            this.Invalidate();

            HitTestInfo hitinfo = this.HitTest(pMouseMove.X, pMouseMove.Y);
            if (moveFirstHitTestType == HitTestType.None)
                moveFirstHitTestType = hitinfo.Type;

            if (!MultiSelect)
            {
                if (moveFirstHitTestType == HitTestType.RowHeader)
                    return;
            }

            //if
            //{
                //this.Invalidate();
                //return;
            //}

            base.OnMouseMove(mea);
            this.Invalidate();
        }

        private void ColumnsHide()
        {
            //test na zmenu velikosti datagridcolumstyles
            DataGridColumnStyle dgs = null;
            for (int i = 0; i < this.TableStyles.Count; i++)
            {
                for (int j = 0; j < this.TableStyles[i].GridColumnStyles.Count; j++)
                {
                    dgs = this.TableStyles[i].GridColumnStyles[j];
                    if (dgs != null && dgs.Width <= MinRowColWidth)
                        dgs.Width = -1; //timto se skryje
                }
            }
        }

        private void ColumnsShow()
        {
            //test na zmenu velikosti datagridcolumstyles
            DataGridColumnStyle dgs = null;
            for (int i = 0; i < this.TableStyles.Count; i++)
            {
                for (int j = 0; j < this.TableStyles[i].GridColumnStyles.Count; j++)
                {
                    dgs = this.TableStyles[i].GridColumnStyles[j];
                    if (dgs != null && dgs.Width <= MinRowColWidth)
                        dgs.Width = 10 * MinRowColWidth; //timto se zobrazi 
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            HitTestInfo hitinfo = this.HitTest(pMouseUp.X, pMouseUp.Y);
            if (hitinfo.Type == HitTestType.None && pMouseUp.X <= this.HeaderRowWidth && pMouseUp.Y <= this.HeaderColumnHeight)
            {
                ColumnsShow();
                return;
            }
            else if (hitinfo.Type == HitTestType.ColumnHeader)
            {
                if (_sortByHeaderDoubleClick)
                    this.SortUpdate(hitinfo.Column);
                return;
            }
            else if (hitinfo.Type == HitTestType.ColumnResize)
            {
                // TODO : nastavit sirku daneho sloupce na maximalni sirku textu hodnoty sloupce ze vsech radku...
            }
            else if (hitinfo.Type == HitTestType.RowResize)
            {
                // TODO : nastavit velikost radku na jakou hodnotu? => defaultni velikost???
            }
            else if (hitinfo.Type == HitTestType.Cell)
            {
                base.OnDoubleClick(e);
            }
        }

        protected override void OnCurrentCellChanged(EventArgs e)
        {
            base.OnCurrentCellChanged(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //base.OnKeyDown(e);

            e.Handled = true;

            int row = 0;
            int col = 0;
            try
            {
                row = this.CurrentCell.RowNumber;
                col = this.CurrentCell.ColumnNumber;

                if (!e.Alt && !e.Control && !e.Shift)
                {
                    #region Multiselect options
                    if (MultiSelect)
                    {
                        if (e.KeyCode == Keys.Space)
                        {
                            if (this.IsSelected(row))
                                this.UnSelect(row);
                            else
                                this.Select(row);
                            return;
                        }
                    }
                    #endregion


                    int rownew = row;
                    int colnew = col;

                    if (e.KeyCode == Keys.Up)
                    {
                        rownew = row - 1; colnew = col;
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        rownew = row + 1; colnew = col;
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        rownew = row; colnew = col - 1;
                    }
                    else if (e.KeyCode == Keys.Right)
                    {
                        rownew = row; colnew = col + 1;
                    }
                    else if (e.KeyCode == _keyScrollUp)
                    {
                        this.ScrollUp();
                        rownew = this.CurrentCell.RowNumber;
                        colnew = this.CurrentCell.ColumnNumber;
                    }
                    else if (e.KeyCode == _keyScrollDown)
                    {
                        this.ScrollDown();
                        rownew = this.CurrentCell.RowNumber;
                        colnew = this.CurrentCell.ColumnNumber;
                    }
                    else
                    {
                        e.Handled = false;
                        return;
                    }

                    this.CurrentCell = new DataGridCell(rownew, colnew);
                    if (!MultiSelect)
                    {
                        this.UnSelect(row);
                        this.Select(rownew);
                    }
                    
                    if (row != rownew)
                    {
                        this.OnCurrentRowIndexChanged();
                    }
                }
                else
                {
                    e.Handled = false;
                }

            }
            catch { }
        }
        
        protected override void OnKeyUp(KeyEventArgs e)
        {
            //base.OnKeyUp(e);
        }
        
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            base.OnPaint(pea);
            //pea.Graphics.DrawString(
            //    "D:" + pMouseDown.X + "," + pMouseDown.Y + "\n\rU:" + pMouseUp.X + "," + pMouseUp.Y + "\n\rM:" + pMouseMove.X + "," + pMouseMove.Y,
            //    this.Font,
            //    bMouseInfo,
            //    pea.ClipRectangle,
            //    sfMouseInfo
            //    );
            //pea.Graphics.DrawString(
            //    moveHitTestType.ToString(),
            //    this.Font,
            //    bMouseInfo,
            //    pea.ClipRectangle,
            //    sfMouseInfo
            //    );
            //Pen pMove = new Pen(Color.Brown, 2);
            //pMove.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //pea.Graphics.DrawLine(pMove, pMouseDown.X, pMouseDown.Y, pMouseMove.X, pMouseMove.Y);

            try
            {
                if (columnMove)
                {
                    int leftpos = (this.RowHeadersVisible ? this.HeaderRowWidth : 0) + this.DefaultBorderWidth;
                    for (int i = 0; i < columnMoveIndex; i++)
                    {
                        leftpos += TableStyles[0].GridColumnStyles[i].Width + this.DefaultBorderWidth;
                    }
                    leftpos += this.DefaultBorderWidth;
                    HScrollBar hbar = this.ScrollBarHorizontal;
                    leftpos -= (hbar == null) ? 0 : hbar.Value;
                    //int toppos = this.Top + this.DefaultBorderWidth;
                    int toppos = this.DefaultBorderWidth;
                    int width = this.TableStyles[0].GridColumnStyles[columnMoveIndex].Width;
                    string text = this.TableStyles[0].GridColumnStyles[columnMoveIndex].HeaderText;
                    Font dFont = new Font(this.Font.Name, this.Font.Size, FontStyle.Bold);
                    int height = this.HeaderColumnHeight;// -this.DefaultBorderWidth;
                    //Brush bforeColor = new SolidBrush(this.ForeColor);
                    Rectangle rect = new Rectangle(leftpos, toppos, width, height);
                    //pea.Graphics.FillRectangle(bcolumnMove, rect);
                    //pea.Graphics.DrawRectangle(new Pen(Color.Red), rect);
                    bool clipleftposition = (leftpos < (this.RowHeadersVisible ? this.HeaderRowWidth : 0));
                    if (clipleftposition)
                        pea.Graphics.Clip = new Region(new Rectangle(
                            (this.RowHeadersVisible ? this.HeaderRowWidth : 0),
                            toppos,
                            width - (this.RowHeadersVisible ? this.HeaderRowWidth : 0),
                            height));
                    pea.Graphics.FillRectangle(new SolidBrush(this.HeaderBackColorSelected), rect);
                    rect.Inflate(-this.DefaultTextInset, -this.DefaultTextInset);
                    //pea.Graphics.FillRectangle(new SolidBrush(this.HeaderBackColorSelected), rect);
                    pea.Graphics.DrawString(text, dFont, new SolidBrush(this.HeaderForeColorSelected), (RectangleF)rect, sfMouseInfo);
                    if (clipleftposition)
                        pea.Graphics.ResetClip();
                }

            }
            catch (Exception ex)
            {
				Fask.Logging.Log.Write(ex);
				//Logging.Log.Write(ex);
            }

            try
            {
                string sort = this.Sort;
                if (sort != null && sort != string.Empty)
                {
                    string[] sorts = sort.Split(new char[] { ',' });

                    for (int j = 0; j < sorts.Length; j++)
                    {
                        int leftpos = (this.RowHeadersVisible ? this.HeaderRowWidth : 0) + this.DefaultBorderWidth;
                        int rightpos = 0;
                        int toppos = 0;
                        int bottompos = 0;
                        bool desc = false;
                        desc = sorts[j].Trim().EndsWith(" DESC", StringComparison.CurrentCultureIgnoreCase);
                        string columname = sorts[j].Split(new char[] { ' ' })[0].Trim();
                        bool founded = false;
                        bool visible = false;
                        for (int i = 0; i < this.TableStyles[0].GridColumnStyles.Count; i++)
                        {
                            if (String.Compare(TableStyles[0].GridColumnStyles[i].MappingName, columname, true) == 0)
                            {
                                founded = true;
                                visible = TableStyles[0].GridColumnStyles[i].Width >= MinRowColWidth;
                                rightpos = leftpos + TableStyles[0].GridColumnStyles[i].Width;
                                toppos = this.DefaultBorderWidth;
                                bottompos = this.HeaderColumnHeight;
                                break;
                            }
                            leftpos += TableStyles[0].GridColumnStyles[i].Width + this.DefaultBorderWidth;
                        }
                        if (founded && visible)
                        {
                            HScrollBar hbar = this.ScrollBarHorizontal;
                            rightpos -= (hbar == null) ? 0 : hbar.Value;
                            DrawSortingTriangle(pea, leftpos, rightpos, toppos, bottompos, desc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.Log.Write(ex);
            }

        }

        private const int ctriangle = 3;
        private void DrawSortingTriangle(PaintEventArgs pea, int leftpos, int rightpos, int toppos, int bottompos, bool desc)
        {
            Point center = new Point(rightpos - (bottompos - toppos) / 2, (bottompos - toppos) / 2);
            Point[] points = new Point[3];
            if (!desc)
            {
                points[0] = new Point(center.X, center.Y - ctriangle);
                points[1] = new Point(center.X - ctriangle, center.Y + ctriangle);
                points[2] = new Point(center.X + ctriangle, center.Y + ctriangle);
            }
            else
            {
                points[0] = new Point(center.X, center.Y + ctriangle);
                points[1] = new Point(center.X - ctriangle, center.Y - ctriangle);
                points[2] = new Point(center.X + ctriangle, center.Y - ctriangle);
            }
            pea.Graphics.FillPolygon(bSortTriangle, points);
            //pea.Graphics.DrawLines(penSortTriangle, points);
        }

        private Color _headerBackColorSelected = Color.Green;
        public Color HeaderBackColorSelected
        {
            get { return _headerBackColorSelected; }
            set { _headerBackColorSelected = value; this.Invalidate(); }
        }

        private Color _headerForeColorSelected = Color.Brown;
        public Color HeaderForeColorSelected
        {
            get{ return _headerForeColorSelected; }
            set { _headerForeColorSelected = value; this.Invalidate(); }
        }

        private Color _alternatingBackColor = Color.Gold;
        public Color BackColorAlternating
        {
            get { return _alternatingBackColor; }
            set { _alternatingBackColor = value; this.Invalidate(); }
        }
        //private Color _alternatingForeColor = Color.DarkGreen;
        //public Color ForeColorAlternating
        //{
        //    get { return _alternatingForeColor; }
        //    set { _alternatingForeColor = value; this.Invalidate(); }
        //}


        protected override void OnPaintBackground(PaintEventArgs pea)
        {
            base.OnPaintBackground(pea);
        }

        /// <summary>
        /// Ulozi konfiguraci rozlozeni gridu do souboru
        /// </summary>
        /// <param name="file">plna cesta souboru do ktereho se ulozi(pokud neexistuje, vytvori se)</param>
        public void Save(string file)
        {
            System.Xml.XmlTextWriter xmlwrite = null;
            try
            {
                string directory = Path.GetDirectoryName(file);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                xmlwrite = new System.Xml.XmlTextWriter(file, null);
                xmlwrite.Formatting = Formatting.Indented;

                xmlwrite.WriteStartElement("DataGrid");
                xmlwrite.WriteAttributeString("Sort", (this.Sort ?? string.Empty).ToString());

                if (this.TableStyles.Count > 0)
                {
                    xmlwrite.WriteStartElement("TableStyle");
                    xmlwrite.WriteAttributeString("MappingName", this.TableStyles[0].MappingName);
                    xmlwrite.WriteEndElement();

                    for (int i = 0; i < this.TableStyles[0].GridColumnStyles.Count; i++)
                    {
                        xmlwrite.WriteStartElement("Column");
                        xmlwrite.WriteAttributeString("MappingName", this.TableStyles[0].GridColumnStyles[i].MappingName);
                        xmlwrite.WriteAttributeString("Width", this.TableStyles[0].GridColumnStyles[i].Width.ToString());
                        xmlwrite.WriteAttributeString("HeaderText", this.TableStyles[0].GridColumnStyles[i].HeaderText);
                        xmlwrite.WriteEndElement();
                    }
                }
                xmlwrite.WriteStartElement("Row");
                xmlwrite.WriteAttributeString("Height", this.RowHeightDefault.ToString());
                xmlwrite.WriteEndElement();

                xmlwrite.WriteEndElement();
                xmlwrite.Close();
                xmlwrite = null;
            }
            catch (Exception ex)
            {
                xmlwrite.Close();
                MessageBox.Show(ex.Message, this.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Nacteni konfigurace rozlozeni gridu ze souboru
        /// </summary>
        /// <param name="file">plna cesta k souboru s konfiguraci</param>
        public void Load(string file)
        {

            if (!System.IO.File.Exists(file))
                return;

            System.Xml.XmlDocument xmldoc = null;
            try
            {
                xmldoc = new System.Xml.XmlDocument();
                xmldoc.Load(file);

                DataGridTableStyle oldTS = TableStyles.Count > 0 ? TableStyles[0] : new DataGridTableStyle();
                DataGridTableStyle newTS = new DataGridTableStyle();
                
                //newTS.MappingName = oldTS.MappingName;  // Table Name

                List<int> indices = this.SelectedIndices;

                XmlNode nodeGrid = xmldoc.SelectSingleNode("DataGrid");
                if (nodeGrid != null)
                {
                    if (nodeGrid.Attributes["Sort"] != null)
                    {
                        this.Sort = nodeGrid.Attributes["Sort"].InnerText;
                    }
                }

                XmlNode nodeTS = xmldoc.SelectSingleNode("DataGrid/TableStyle");
                if (nodeTS != null && nodeTS.Attributes["MappingName"] != null)
                {
                    newTS.MappingName = nodeTS.Attributes["MappingName"].InnerText;
                }

                XmlNodeList nodes = xmldoc.SelectNodes("DataGrid/Column");
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes != null)
                        {
                            string mpname = node.Attributes["MappingName"].InnerText;
                            int width = int.Parse(node.Attributes["Width"].InnerText);

                            if (oldTS.GridColumnStyles.Contains(mpname))
                            {
                                DataGridColumnStyle dgs = oldTS.GridColumnStyles[mpname];
                                dgs.Width = width;

                                string htext = string.Empty;
                                if (node.Attributes["HeaderText"] != null)
                                {
                                    htext = node.Attributes["HeaderText"].InnerText;
                                    dgs.HeaderText = htext;
                                }

                                newTS.GridColumnStyles.Add(dgs);
                                oldTS.GridColumnStyles.Remove(dgs);
                            }
                            else // TODO : Pridat novy ...
                            {
                                Fask.Graphic.DataGrid2TextBoxColumn c = new Fask.Graphic.DataGrid2TextBoxColumn();
                                c.Grid = this;
                                //c.SelectionShow = true;
                                c.HeaderText = node.Attributes["HeaderText"] != null ? node.Attributes["HeaderText"].InnerText : string.Empty;
                                c.MappingName = mpname;
                                c.NullText = "-";
                                c.Width = width;
                                newTS.GridColumnStyles.Add(c);
                            }
                        }
                    }

                    //nakonec dotahnout zbyvajici, ktere nemusi byt zobrazeny?? - nove upravy apod...
                    for (int i = 0; i < oldTS.GridColumnStyles.Count; i++)
                    {
                        newTS.GridColumnStyles.Add(oldTS.GridColumnStyles[i]);
                    }
                }

                int rowheight = this.RowHeightDefault;
                XmlNode noderow = xmldoc.SelectSingleNode("DataGrid/Row");
                if (noderow != null)
                {
                    if (noderow.Attributes != null)
                    {
                        rowheight = int.Parse(noderow.Attributes["Height"].InnerText);
                    }
                    this.RowHeightDefault = rowheight;
                }

                if (TableStyles.Contains(oldTS))
                    TableStyles.Remove(oldTS);
                TableStyles.Add(newTS);

                this.SelectedIndices = indices;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Inicializuje styly gridu
        /// </summary>
        /// <param name="numberFormat">format ciselnych sloupcu</param>
        public void InitializeTableGridColumnStyles(string numberFormat)
        {
            this.NumberFormat = numberFormat;
            this.InitializeTableGridColumnStyles();
        }

        /// Inicializuje styly gridu
        public void InitializeTableGridColumnStyles()
        {
            for (int i = 0; i < this.TableStyles.Count; i++)
            {
                for (int j = 0; j < this.TableStyles[i].GridColumnStyles.Count; j++)
                {
                    object dgs = this.TableStyles[i].GridColumnStyles[j];
                    //??? je toto vubec potreba???
                    //((DataGridTextBoxColumn)dgs).FormatInfo = System.Globalization.CultureInfo.CurrentCulture;
                    //((DataGridTextBoxColumn)dgs).FormatInfo = System.Globalization.NumberFormatInfo.CurrentInfo;
                    //((DataGridTextBoxColumn)dgs).FormatInfo = _numberFormat;
                    if (dgs is Fask.Graphic.DataGrid2TextBoxColumn)
                    {
                        Fask.Graphic.DataGrid2TextBoxColumn o = (Fask.Graphic.DataGrid2TextBoxColumn)dgs;
                        o.Grid = this;
                    }
                    else if (dgs is Fask.Graphic.DataGrid2NumberBoxColumn)
                    {
                        Fask.Graphic.DataGrid2NumberBoxColumn o = (Fask.Graphic.DataGrid2NumberBoxColumn)dgs;
                        o.Grid = this;
                        o.Format = _numberFormat;
                    }
                }
            }
        }

        /// <summary>
        /// Vytvori column styles, pokud neexistuje tablestyle...
        /// </summary>
        /// <param name="dt"></param>
        public void CreateTableGridColumnStyles(DataTable dt)
        {           
            if (this.TableStyles.Count == 0)
            {
                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = dt.TableName;
                foreach (DataColumn dc in dt.Columns)
                {
                    Fask.Graphic.DataGrid2TextBoxColumn c = new Fask.Graphic.DataGrid2TextBoxColumn();
                    c.Grid = this;
                    c.HeaderText = dc.ColumnName;
                    c.MappingName = dc.ColumnName;
                    c.NullText = "-";
                    //c.SelectionShow = true;
                    ts.GridColumnStyles.Add(c);
                }
                this.TableStyles.Add(ts);
            }
        }

        private string _numberFormat = "N";
        /// <summary>
        /// Ciselny format pro zobrazeni cisel
        /// </summary>
        public string NumberFormat
        {
            get { return _numberFormat; }
            set { _numberFormat = value; }
        }

        private bool focused = false;
        protected override void OnGotFocus(EventArgs e)
        {
            if (focused)
            {
                return;
            }
            base.OnGotFocus(e);
            this.focused = this.Focused;
        }
    }
}
