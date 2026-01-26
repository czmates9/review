#region License
// Advanced DataGridView
//
// Copyright (c), 2014 Davide Gironi <davide.gironi@gmail.com>
// Original work Copyright (c), 2013 Zuby <zuby@me.com>
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Zuby.ADGV
{
    internal partial class FormCustomFilter : Form
    {

        #region class properties

        private enum FilterType
        {
            Unknown,
            DateTime,
            TimeSpan,
            String,
            Float,
            Integer
        }

        private FilterType _filterType = FilterType.Unknown;
        private Control _valControl1 = null;
        private Control _valControl2 = null;

        private bool _filterDateAndTimeEnabled = true;

        private string _filterString = null;
        private string _filterStringDescription = null;

        //private Hashtable _textStrings = new Hashtable();

        #endregion


        #region constructors

        /// <summary>
        /// Main constructor
        /// </summary>
        private FormCustomFilter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form constructor
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="filterDateAndTimeEnabled"></param>
        public FormCustomFilter(Type dataType, bool filterDateAndTimeEnabled)
            : this()
        {
            //set localization strings
            //_textStrings.Add("EQUALS", "equals");
            //_textStrings.Add("DOES_NOT_EQUAL", "does not equal");
            //_textStrings.Add("EARLIER_THAN", "earlier than");
            //_textStrings.Add("LATER_THAN", "later than");
            //_textStrings.Add("BETWEEN", "between");
            //_textStrings.Add("GREATER_THAN", "greater than");
            //_textStrings.Add("GREATER_THAN_OR_EQUAL_TO", "greater than or equal to");
            //_textStrings.Add("LESS_THAN", "less than");
            //_textStrings.Add("LESS_THAN_OR_EQUAL_TO", "less than or equal to");
            //_textStrings.Add("BEGINS_WITH", "begins with");
            //_textStrings.Add("DOES_NOT_BEGIN_WITH", "does not begin with");
            //_textStrings.Add("ENDS_WITH", "ends with");
            //_textStrings.Add("DOES_NOT_END_WITH", "does not end with");
            //_textStrings.Add("CONTAINS", "contains");
            //_textStrings.Add("DOES_NOT_CONTAIN", "does not contain");
            //_textStrings.Add("INVALID_VALUE", "Invalid Value");
            //_textStrings.Add("FILTER_STRING_DESCRIPTION", "Show rows where value {0} \"{1}\"");
            //_textStrings.Add("FORM_TITLE", "Custom Filter");
            //_textStrings.Add("LABEL_COLUMNNAMETEXT", "Show rows where value");
            //_textStrings.Add("LABEL_AND", "And");
            //_textStrings.Add("BUTTON_OK", "OK");
            //_textStrings.Add("BUTTON_CANCEL", "Cancel");


            this.Text =  Properties.Settings.Default.FormCustomFilter_FORM_TITLE;
            label_columnName.Text = Properties.Settings.Default.FormCustomFilter_LABEL_COLUMNNAMETEXT ;
            label_and.Text = Properties.Settings.Default.FormCustomFilter_LABEL_AND ;
            button_ok.Text = Properties.Settings.Default.FormCustomFilter_BUTTON_OK ;
            button_cancel.Text = Properties.Settings.Default.FormCustomFilter_BUTTON_CANCEL ;

            if (dataType == typeof(DateTime))
                _filterType = FilterType.DateTime;
            else if (dataType == typeof(TimeSpan))
                _filterType = FilterType.TimeSpan;
            else if (dataType == typeof(Int32) || dataType == typeof(Int64) || dataType == typeof(Int16) ||
                    dataType == typeof(UInt32) || dataType == typeof(UInt64) || dataType == typeof(UInt16) ||
                    dataType == typeof(Byte) || dataType == typeof(SByte))
                _filterType = FilterType.Integer;
            else if (dataType == typeof(Single) || dataType == typeof(Double) || dataType == typeof(Decimal))
                _filterType = FilterType.Float;
            else if (dataType == typeof(String))
                _filterType = FilterType.String;
            else
                _filterType = FilterType.Unknown;

            _filterDateAndTimeEnabled = filterDateAndTimeEnabled;

            switch (_filterType)
            {
                case FilterType.DateTime:
                    _valControl1 = new DateTimePicker();
                    _valControl2 = new DateTimePicker();
                    if (_filterDateAndTimeEnabled)
                    {
                        System.Globalization.DateTimeFormatInfo dt = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat;

                        (_valControl1 as DateTimePicker).CustomFormat = dt.ShortDatePattern + " " + "HH:mm";
                        (_valControl2 as DateTimePicker).CustomFormat = dt.ShortDatePattern + " " + "HH:mm";
                        (_valControl1 as DateTimePicker).Format = DateTimePickerFormat.Custom;
                        (_valControl2 as DateTimePicker).Format = DateTimePickerFormat.Custom;
                    }
                    else
                    {
                        (_valControl1 as DateTimePicker).Format = DateTimePickerFormat.Short;
                        (_valControl2 as DateTimePicker).Format = DateTimePickerFormat.Short;
                    }

                    comboBox_filterType.Items.AddRange(new string[] {
                        Properties.Settings.Default.FormCustomFilter_EQUALS ,
                        Properties.Settings.Default.FormCustomFilter_DOES_NOT_EQUAL ,
                        Properties.Settings.Default.FormCustomFilter_EARLIER_THAN ,
                        Properties.Settings.Default.FormCustomFilter_LATER_THAN ,
                        Properties.Settings.Default.FormCustomFilter_BETWEEN 
                    });
                    break;

                case FilterType.TimeSpan:
                    _valControl1 = new TextBox();
                    _valControl2 = new TextBox();
                    comboBox_filterType.Items.AddRange(new string[] {
                        Properties.Settings.Default.FormCustomFilter_CONTAINS ,
                        Properties.Settings.Default.FormCustomFilter_DOES_NOT_CONTAIN 
                    });
                    break;

                case FilterType.Integer:
                case FilterType.Float:
                    _valControl1 = new TextBox();
                    _valControl2 = new TextBox();
                    _valControl1.TextChanged += valControl_TextChanged;
                    _valControl2.TextChanged += valControl_TextChanged;
                    comboBox_filterType.Items.AddRange(new string[] {
                        Properties.Settings.Default.FormCustomFilter_EQUALS ,
                        Properties.Settings.Default.FormCustomFilter_DOES_NOT_EQUAL ,
                        Properties.Settings.Default.FormCustomFilter_GREATER_THAN ,
                        Properties.Settings.Default.FormCustomFilter_GREATER_THAN_OR_EQUAL_TO ,
                        Properties.Settings.Default.FormCustomFilter_LESS_THAN ,
                        Properties.Settings.Default.FormCustomFilter_LESS_THAN_OR_EQUAL_TO ,
                        Properties.Settings.Default.FormCustomFilter_BETWEEN 
                    });
                    _valControl1.Tag = true;
                    _valControl2.Tag = true;
                    button_ok.Enabled = false;
                    break;

                default:
                    _valControl1 = new TextBox();
                    _valControl2 = new TextBox();
                    comboBox_filterType.Items.AddRange(new string[] {
                        Properties.Settings.Default.FormCustomFilter_EQUALS ,
                        Properties.Settings.Default.FormCustomFilter_DOES_NOT_EQUAL ,
                        Properties.Settings.Default.FormCustomFilter_BEGINS_WITH ,
                        Properties.Settings.Default.FormCustomFilter_DOES_NOT_BEGIN_WITH ,
                        Properties.Settings.Default.FormCustomFilter_ENDS_WITH ,
                        Properties.Settings.Default.FormCustomFilter_DOES_NOT_END_WITH ,
                        Properties.Settings.Default.FormCustomFilter_CONTAINS ,
                        Properties.Settings.Default.FormCustomFilter_DOES_NOT_CONTAIN 
                    });
                    break;
            }
            comboBox_filterType.SelectedIndex = 0;

            _valControl1.Name = "valControl1";
            _valControl1.Location = new System.Drawing.Point(30, 66);
            _valControl1.Size = new System.Drawing.Size(166, 20);
            _valControl1.TabIndex = 4;
            _valControl1.Visible = true;
            _valControl1.KeyDown += valControl_KeyDown;

            _valControl2.Name = "valControl2";
            _valControl2.Location = new System.Drawing.Point(30, 108);
            _valControl2.Size = new System.Drawing.Size(166, 20);
            _valControl2.TabIndex = 5;
            _valControl2.Visible = false;
            _valControl2.VisibleChanged += new EventHandler(valControl2_VisibleChanged);
            _valControl2.KeyDown += valControl_KeyDown;

            Controls.Add(_valControl1);
            Controls.Add(_valControl2);

            errorProvider.SetIconAlignment(_valControl1, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(_valControl1, -18);
            errorProvider.SetIconAlignment(_valControl2, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(_valControl2, -18);
        }

        /// <summary>
        /// Form loaders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCustomFilter_Load(object sender, EventArgs e)
        { }

        #endregion


        #region public filter methods

        /// <summary>
        /// Get the Filter string
        /// </summary>
        public string FilterString
        {
            get
            {
                return _filterString;
            }
        }

        /// <summary>
        /// Get the Filter string description
        /// </summary>
        public string FilterStringDescription
        {
            get
            {
                return _filterStringDescription;
            }
        }

        #endregion


        #region filter builder

        /// <summary>
        /// Build a Filter string
        /// </summary>
        /// <param name="filterType"></param>
        /// <param name="filterDateAndTimeEnabled"></param>
        /// <param name="filterTypeConditionText"></param>
        /// <param name="control1"></param>
        /// <param name="control2"></param>
        /// <returns></returns>
        private string BuildCustomFilter(FilterType filterType, bool filterDateAndTimeEnabled, string filterTypeConditionText, Control control1, Control control2)
        {
            string filterString = "";

            string column = "[{0}] ";

            if (filterType == FilterType.Unknown)
                column = "Convert([{0}], 'System.String') ";

            filterString = column;

            switch (filterType)
            {
                case FilterType.DateTime:
                    DateTime dt = ((DateTimePicker)control1).Value;
                    dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);

                    if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_EQUALS )
                    {
                        filterString = "Convert([{0}], 'System.String') LIKE '%" + Convert.ToString((filterDateAndTimeEnabled ? dt : dt.Date), CultureInfo.CurrentCulture) + "%'";
                    }
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_EARLIER_THAN )
                    {
                        filterString += "< '" + Convert.ToString((filterDateAndTimeEnabled ? dt : dt.Date), CultureInfo.CurrentCulture) + "'";
                    }
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_LATER_THAN )
                    {
                        filterString += "> '" + Convert.ToString((filterDateAndTimeEnabled ? dt : dt.Date), CultureInfo.CurrentCulture) + "'";
                    }
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_BETWEEN )
                    {
                        DateTime dt1 = ((DateTimePicker)control2).Value;
                        dt1 = new DateTime(dt1.Year, dt1.Month, dt1.Day, dt1.Hour, dt1.Minute, 0);
                        filterString += ">= '" + Convert.ToString((filterDateAndTimeEnabled ? dt : dt.Date), CultureInfo.CurrentCulture) + "'";
                        filterString += " AND " + column + "<= '" + Convert.ToString((filterDateAndTimeEnabled ? dt1 : dt1.Date), CultureInfo.CurrentCulture) + "'";
                    }
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_DOES_NOT_EQUAL )
                    {
                        filterString = "Convert([{0}], 'System.String') NOT LIKE '%" + Convert.ToString((filterDateAndTimeEnabled ? dt : dt.Date), CultureInfo.CurrentCulture) + "%'";
                    }
                    break;

                case FilterType.TimeSpan:
                    try
                    {
                        TimeSpan ts = TimeSpan.Parse(control1.Text);

                        if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_CONTAINS )
                        {
                            filterString = "(Convert([{0}], 'System.String') LIKE '%P" + ((int)ts.Days > 0 ? (int)ts.Days + "D" : "") + (ts.TotalHours > 0 ? "T" : "") + ((int)ts.Hours > 0 ? (int)ts.Hours + "H" : "") + ((int)ts.Minutes > 0 ? (int)ts.Minutes + "M" : "") + ((int)ts.Seconds > 0 ? (int)ts.Seconds + "S" : "") + "%')";
                        }
                        else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_DOES_NOT_CONTAIN )
                        {
                            filterString = "(Convert([{0}], 'System.String') NOT LIKE '%P" + ((int)ts.Days > 0 ? (int)ts.Days + "D" : "") + (ts.TotalHours > 0 ? "T" : "") + ((int)ts.Hours > 0 ? (int)ts.Hours + "H" : "") + ((int)ts.Minutes > 0 ? (int)ts.Minutes + "M" : "") + ((int)ts.Seconds > 0 ? (int)ts.Seconds + "S" : "") + "%')";
                        }
                    }
                    catch (FormatException)
                    {
                        filterString = null;
                    }
                    break;

                case FilterType.Integer:
                case FilterType.Float:

                    string num = control1.Text;

                    if (filterType == FilterType.Float)
                        num = num.Replace(",", ".");

                    if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_EQUALS )
                        filterString += "= " + num;
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_BETWEEN )
                        filterString += ">= " + num + " AND " + column + "<= " + (filterType == FilterType.Float ? control2.Text.Replace(",", ".") : control2.Text);
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_DOES_NOT_EQUAL )
                        filterString += "<> " + num;
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_GREATER_THAN )
                        filterString += "> " + num;
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_GREATER_THAN_OR_EQUAL_TO )
                        filterString += ">= " + num;
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_LESS_THAN )
                        filterString += "< " + num;
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_LESS_THAN_OR_EQUAL_TO )
                        filterString += "<= " + num;
                    break;

                default:
                    string txt = FormatFilterString(control1.Text);
                    if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_EQUALS )
                        filterString += "LIKE '" + txt + "'";
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_DOES_NOT_EQUAL )
                        filterString += "NOT LIKE '" + txt + "'";
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_BEGINS_WITH )
                        filterString += "LIKE '" + txt + "%'";
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_ENDS_WITH )
                        filterString += "LIKE '%" + txt + "'";
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_DOES_NOT_BEGIN_WITH )
                        filterString += "NOT LIKE '" + txt + "%'";
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_DOES_NOT_END_WITH )
                        filterString += "NOT LIKE '%" + txt + "'";
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_CONTAINS )
                        filterString += "LIKE '%" + txt + "%'";
                    else if (filterTypeConditionText == Properties.Settings.Default.FormCustomFilter_DOES_NOT_CONTAIN )
                        filterString += "NOT LIKE '%" + txt + "%'";
                    break;
            }

            return filterString;
        }

        /// <summary>
        /// Format a text Filter string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string FormatFilterString(string text)
        {
            string result = "";
            string s;
            string[] replace = { "%", "[", "]", "*", "\"", "\\" };

            for (int i = 0; i < text.Length; i++)
            {
                s = text[i].ToString();
                if (replace.Contains(s))
                    result += "[" + s + "]";
                else
                    result += s;
            }

            return result.Replace("'", "''");
        }


        #endregion


        #region buttons events

        /// <summary>
        /// Button Cancel Clieck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_cancel_Click(object sender, EventArgs e)
        {
            _filterStringDescription = null;
            _filterString = null;
            Close();
        }

        /// <summary>
        /// Button OK Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ok_Click(object sender, EventArgs e)
        {
            if ((_valControl1.Visible && _valControl1.Tag != null && ((bool)_valControl1.Tag)) ||
                (_valControl2.Visible && _valControl2.Tag != null && ((bool)_valControl2.Tag)))
            {
                button_ok.Enabled = false;
                return;
            }

            string filterString = BuildCustomFilter(_filterType, _filterDateAndTimeEnabled, comboBox_filterType.Text, _valControl1, _valControl2);

            if (!String.IsNullOrEmpty(filterString))
            {
                _filterString = filterString;
                _filterStringDescription = String.Format(Properties.Settings.Default.FormCustomFilter_FILTER_STRING_DESCRIPTION , comboBox_filterType.Text, _valControl1.Text);
                if (_valControl2.Visible)
                    _filterStringDescription += " " + label_and.Text + " \"" + _valControl2.Text + "\"";
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                _filterString = null;
                _filterStringDescription = null;
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }

            Close();
        }

        #endregion


        #region changed status events

        /// <summary>
        /// Changed condition type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_filterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _valControl2.Visible = comboBox_filterType.Text == Properties.Settings.Default.FormCustomFilter_BETWEEN ;
            button_ok.Enabled = !(_valControl1.Visible && _valControl1.Tag != null && ((bool)_valControl1.Tag)) ||
                (_valControl2.Visible && _valControl2.Tag != null && ((bool)_valControl2.Tag));
        }

        /// <summary>
        /// Changed control2 visibility
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void valControl2_VisibleChanged(object sender, EventArgs e)
        {
            label_and.Visible = _valControl2.Visible;
        }

        /// <summary>
        /// Changed a control Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void valControl_TextChanged(object sender, EventArgs e)
        {
            bool hasErrors = false;
            switch (_filterType)
            {
                case FilterType.Integer:
                    Int64 val;
                    hasErrors = !(Int64.TryParse((sender as TextBox).Text, out val));
                    break;

                case FilterType.Float:
                    Double val1;
                    hasErrors = !(Double.TryParse((sender as TextBox).Text, out val1));
                    break;
            }

            (sender as Control).Tag = hasErrors || (sender as TextBox).Text.Length == 0;

            if (hasErrors && (sender as TextBox).Text.Length > 0)
                errorProvider.SetError((sender as Control), Properties.Settings.Default.FormCustomFilter_INVALID_VALUE );
            else
                errorProvider.SetError((sender as Control), "");

            button_ok.Enabled = !(_valControl1.Visible && _valControl1.Tag != null && ((bool)_valControl1.Tag)) ||
                (_valControl2.Visible && _valControl2.Tag != null && ((bool)_valControl2.Tag));
        }

        /// <summary>
        /// KeyDown on a control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void valControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == _valControl1)
                {
                    if (_valControl2.Visible)
                        _valControl2.Focus();
                    else
                        button_ok_Click(button_ok, new EventArgs());
                }
                else
                {
                    button_ok_Click(button_ok, new EventArgs());
                }

                e.SuppressKeyPress = false;
                e.Handled = true;
            }
        }

        #endregion

    }
}