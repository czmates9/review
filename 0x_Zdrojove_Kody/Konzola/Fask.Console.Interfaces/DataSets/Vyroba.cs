namespace Fask.Interfaces.DataSets
{
    public partial class Vyroba
    {
        partial class VPH_PotrebaMaterialuDataTable
        {
        }

        partial class PV_ZaplanovaniDataTable
        {
        }

        partial class FASK_Vyroba_TPDataTable
        {
        }

        partial class Production_SourcesDataTable
        {
        }

        partial class CZPRO_VPHDataTable
        {
        }

        partial class Production_KonzolaDataTable
        {
        }

        //public partial class LoginsRow
        //{
        //    public override string ToString()
        //    {
        //        //return base.ToString();
        //        return this.surname.Trim() + " " + this.firstname.Trim() + " (" + this.id.Trim() + ")";
        //    }
        //}

        public partial class CZPRO_VPHRow
        {
            public override string ToString()
            {
                return
                    this.SOPNUMBE.Trim() +
                    ((this.IsSOPDESCNull() || string.IsNullOrEmpty(this.SOPDESC)) ? string.Empty : (" (" + this.SOPDESC.Trim() + ")"));
            }
        }

        public partial class CZPRO_VPPRow
        {
            public override string ToString()
            {
                return
                    this.BarcodeP.Trim() +
                    ((this.IsITEMDESCNull() || string.IsNullOrEmpty(this.ITEMDESC)) ? string.Empty : (" : " + this.ITEMDESC.Trim())) +
                    ((this.IsVNDITNUMNull() || string.IsNullOrEmpty(this.VNDITNUM)) ? string.Empty : (" (" + this.VNDITNUM.Trim() + ") "));

            }
        }
        //partial class LoginsDataTable
        //{
        //}

        public partial class GroupsRow
        {
            public override string ToString()
            {
                return this.name + (string.IsNullOrEmpty(this.description) ? string.Empty : " (" + this.description.Trim() + ")");
            }
        }

        public partial class OperationsRow
        {
            public override string ToString()
            {
                return this.name + (string.IsNullOrEmpty(this.description) ? string.Empty : " (" + this.description.Trim() + ")");
            }
        }

        public partial class MachinesRow
        {
            public override string ToString()
            {
                return this.name + (string.IsNullOrEmpty(this.description) ? string.Empty : " (" + this.description.Trim() + ")");
            }
        }

        public partial class CZMST093Row
        {
            public override string ToString()
            {
                return this.skl_id.Trim() + " : " + this.skl_desc.Trim();
            }
        }

        public partial class CZMST094Row
        {
            public override string ToString()
            {
                return this.Barcode.Trim() + " (" + this.SKL_ID.Trim() + ") : " + this.Description.Trim();
            }
        }



        public partial class FASK_Vyroba_TPRow
        {
            public override string ToString()
            {

                return this.alter.Trim();
            }

        }

    }
}
