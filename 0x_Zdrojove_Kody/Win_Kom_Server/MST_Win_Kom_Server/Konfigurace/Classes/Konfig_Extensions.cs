using System;
using System.Web.UI.WebControls;

namespace Fask.MST_W_Server.Konfigurace.Classes
{
    public static class Konfig_Extensions
    {




        public static void CreateTableFromDTable(this System.Web.UI.WebControls.Table table, System.Data.DataColumn column, string Value, string Comment, System.Data.DataRow Radek)
        {

            try
            {
                string TableName = Radek.Table.TableName + "-";
                string Prefix_Label = "label-" + TableName;
                string Prefix_LabelComment = "labelComment-" + TableName;
                //string PostFix_LabelComment = "-Comment";
                string Prefix_ChechBox = "CheckBox-" + TableName;
                string Prefix_TestBox = "TextBox-" + TableName;
                string Prefix_TestBox32 = "TextBoxInt32-" + TableName;

                //string PlaceHolder = "ContentPlaceHolder1_";


                TableRow row = new TableRow();


                #region Column Label

                TableCell cell_Label = new TableCell();

                System.Web.UI.WebControls.Label lbl = new Label();
                lbl.Text = column.ColumnName;
                lbl.ID = Prefix_Label + column.ColumnName;

                if (!string.IsNullOrEmpty(Comment))
                {
                    lbl.CssClass = "ClickComment ClickCommentCursor";
                }



                if (!string.IsNullOrEmpty(Value))
                    lbl.Text = Value.Trim();

                cell_Label.Controls.Add(lbl);
                //cell_Label.CssClass = "ClickComment";

                //cell_Label.CssClass = "tdClass";
                row.Cells.Add(cell_Label);

                #endregion

                #region Column Value

                TableCell cell_Value = new TableCell();

                if (column.DataType == System.Type.GetType("System.Boolean"))
                {

                    Panel p = new Panel();
                    p.CssClass = "checkbox icheck-success";

                    System.Web.UI.WebControls.CheckBox tb = new CheckBox();
                    tb.ID = Prefix_ChechBox + column.ColumnName;
                    tb.Checked = (bool)Radek[column.ColumnName];
                    //tb.CssClass = "custom-switch-input";

                    Label l = new Label();
                    //l.CssClass = "custom-switch-btn";
                    l.AssociatedControlID = tb.ID;

                    p.Controls.Add(tb);
                    p.Controls.Add(l);


                    cell_Value.Controls.Add(p);
                }
                else if (column.DataType == System.Type.GetType("System.String"))
                {
                    System.Web.UI.WebControls.TextBox tb = new TextBox();
                    tb.ID = Prefix_TestBox + column.ColumnName;
                    

                    if (Classes.Sifrovani.Sifruj)
                    {
                        if (column.ColumnName.StartsWith(Classes.Sifrovani.Prefix))
                        {
                            tb.Text = Classes.Sifrovani.Sifruj_Do_Base64((string)Radek[column.ColumnName]);
                            tb.TextMode = TextBoxMode.Password;
                        }
                        else
                        {
                            tb.Text = (string)Radek[column.ColumnName];
                        }
                    }
                    else
                    {
                        tb.Text = (string)Radek[column.ColumnName];
                    }

                    cell_Value.Controls.Add(tb);
                }
                else if (column.DataType == System.Type.GetType("System.Int32"))
                {
                    System.Web.UI.WebControls.TextBox tb = new TextBox();
                    tb.ID = Prefix_TestBox32 + column.ColumnName;
                    tb.Text = ((System.Int32)Radek[column.ColumnName]).ToString();
                    cell_Value.Controls.Add(tb);
                }
                else
                {
                    System.Web.UI.WebControls.Label tb = new Label();
                    tb.ID = "ERROR";
                    tb.Text = "ERROR : " + column.DataType.ToString();
                    tb.BackColor = System.Drawing.Color.Red;
                    cell_Value.Controls.Add(tb);
                }

                //cell_Value.CssClass = "tdClass";
                row.Cells.Add(cell_Value);


                #endregion

                table.Rows.Add(row);

                if (!string.IsNullOrEmpty(Comment))
                {

                    TableRow rowComment = new TableRow();


                    TableCell cell_LabelComment = new TableCell();

                    System.Web.UI.WebControls.Label lblCom = new Label();
                    lblCom.Text = Comment;
                    lblCom.ID = Prefix_LabelComment + column.ColumnName;

                    cell_LabelComment.Controls.Add(lblCom);
                    cell_LabelComment.ColumnSpan = 2;
                    //cell_LabelComment.di tyle("display", "none");
                    //cell_LabelComment.CssClass = PlaceHolder + Prefix_Label + column.ColumnName + " Comment";
                    cell_LabelComment.CssClass = "Comment";
                    //cell_LabelComment.ID = PlaceHolder + Prefix_Label + column.ColumnName;
                    //cell_Label.CssClass = "tdClass";
                    rowComment.Cells.Add(cell_LabelComment);
                    //rowComment.CssClass = PlaceHolder + Prefix_Label + column.ColumnName + " Comment_Display";
                    //rowComment.Style.Add("display", "none");
                    //rowComment.ID = Prefix_Label + column.ColumnName;

                    table.Rows.Add(rowComment);


                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}