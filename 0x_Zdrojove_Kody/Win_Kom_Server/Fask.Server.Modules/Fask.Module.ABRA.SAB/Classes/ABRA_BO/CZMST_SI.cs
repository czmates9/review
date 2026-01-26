using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.ABRA_BO
{
    public class CZMST_SI
    {
		public CZMST_SI_row[] rows { get; set; }
	}

    public class CZMST_SI_row
    {
        public System.Data.DataRowState RowState { get; set; }

        // --- SQL: [CountEntries] [int] NOT NULL
        public int CountEntries { get; set; }

        // --- SQL:  NOT NULL
        public string SOPNUMBE { get; set; }

        // --- SQL:  NULL
        public string ITEMNMBR { get; set; }

        // --- SQL: [ORD] [int] NOT NULL
        public int ORD { get; set; }

        // --- SQL:  NULL
        public string VNDDOCNM { get; set; }

        // --- SQL:  NULL
        public string VNDITNUM { get; set; }

        // --- SQL:  NULL
        public string CZ_CarKod { get; set; }

        // --- SQL:  NULL
        public string SKL_ID { get; set; }

        // --- SQL:  NULL
        public string LOCNCODE { get; set; }

        // --- SQL:  NOT NULL
        public string MJ { get; set; }

        // --- SQL: [QTYSHPPD] [numeric](19, 5) NOT NULL
        public decimal QTYSHPPD { get; set; }

        // --- SQL: [QTYPACK] [numeric](19, 5) NOT NULL
        public decimal QTYPACK { get; set; }

        // --- SQL: [QTYSHPPDMJ] [numeric](19, 5) NULL
        public decimal? QTYSHPPDMJ { get; set; }

        // --- SQL:  NOT NULL
        public string SERLTNUM { get; set; }

        // --- SQL:  NULL
        public string KOD_SW { get; set; }

        // --- SQL:  NULL
        // v DB je nvarchar(11) – držím string (např. "2025-10-01")
        public string DAT_VYROBY { get; set; }

        // --- SQL:  NULL
        public string REZ_1 { get; set; }

        // --- SQL:  NULL
        public string REZ_2 { get; set; }

        // --- SQL:  NULL
        public string ODBER_ID { get; set; }

        // --- SQL:  NULL
        // často bývá "yyyyMMdd" nebo "ddMMyyyy" – proto string
        public string DATEDONE { get; set; }

        // --- SQL:  NULL
        // často bývá "HHmmss" – proto string
        public string TIMEDONE { get; set; }

        // --- SQL: [USER_ID] [int] NOT NULL
        public int USER_ID { get; set; }

        // --- SQL:  NULL
        public string TYPEPAL { get; set; }

        // --- SQL:  NULL
        public string NMBRPAL { get; set; }

        // --- SQL: [PRINTED] [tinyint] NULL + default(0)
        // tinyint v SQL -> byte? (nebo bool?), nechávám byte? kvůli věrnosti DB
        public byte? PRINTED { get; set; } = 0;

        // --- SQL: [DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
        // Obvykle neplní klient. Pokud chceš číst z DB, ponech public int DEX_ROW_ID { get; set; }
        // Pokud nechceš nastavovat, klidně vynechám. Dávám sem jako nullable pro bezpečí.
        public int? DEX_ROW_ID { get; set; }

        // --- SQL: [GUID] [uniqueidentifier] NULL
        public Guid? GUID { get; set; }

        // --- SQL: [INPUT_MODE] [tinyint] NOT NULL
        public byte INPUT_MODE { get; set; } = 1;

        // --- SQL: [ID_TERMINAL] [int] NOT NULL
        public int ID_TERMINAL { get; set; }

        // --- SQL:  NULL
        public string ITEMCODE { get; set; }

        // --- SQL: [WEIGHT] [numeric](19, 5) NULL
        public decimal? WEIGHT { get; set; }

        // --- SQL: [Expirace] [datetime] NULL
        public DateTime? Expirace { get; set; }
    }


}
