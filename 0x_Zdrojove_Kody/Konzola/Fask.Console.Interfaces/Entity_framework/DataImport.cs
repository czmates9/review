using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace DataImport
{
    public class FASK_ZASOBY_row
    {
        public string ITEMNMBR { get; set; }
        public string ITEMDESC { get; set; }
        public string ITEMCODE { get; set; }
        public string VNDITNUM { get; set; }
        public string CZ_CarKod { get; set; }
        public string LOCNCODE { get; set; }
        public string SKL_ID { get; set; }
        public decimal QTY { get; set; }
        public decimal? QTYPACK { get; set; }
        public string MJ { get; set; }
        public string DMJ { get; set; }
        public decimal? TAXRATE { get; set; }
        public decimal? PRICE0 { get; set; }
        public decimal? PRICE1 { get; set; }
        public decimal? PRICE2 { get; set; }
        public decimal? PRICE3 { get; set; }
        public decimal? PRICE4 { get; set; }
        public decimal? PRICE5 { get; set; }
        public byte CZ_SerNum_Track { get; set; }
        public short CZ_SerNum_Delka { get; set; }
        public byte CZ_Rez1_Track { get; set; }
        public byte CZ_Rez2_Track { get; set; }
        public byte CZ_Rez3_Track { get; set; }
        public byte CZ_Rez4_Track { get; set; }
        public string REZ1 { get; set; }
        public string REZ2 { get; set; }
        public string REZ3 { get; set; }
        public string REZ4 { get; set; }
        public string ODB_ID { get; set; }
        public string mena_ID { get; set; }
        public string SERLTNUM { get; set; }
        public decimal? WEIGHT { get; set; }
        public DateTime? TIMEFROM { get; set; }
        public DateTime? TIMETO { get; set; }
        public DateTime? LSTMod { get; set; }
        public string loginid { get; set; }
        public byte CZ_Expirace_Track { get; set; }
        public DateTime? EXPIRACE { get; set; }
    }

    class FASK_ZASOBY
    {
        public static void DB_insert_FASK_ZASOBY(string filePath, string connectionString)
        {
            filePath = "path_to_your_file.csv";
            var records = new List<FASK_ZASOBY_row>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                string xx = string.Empty;
                records = csv.GetRecords<FASK_ZASOBY_row>().ToList(); //zde mi to vyhazuje chybu typu: IEnumerable<FASK_ZASOBY_row> neobsahuje definici pro ToList..oprav to!



            }

             connectionString = "YourConnectionString";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var record in records)
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"INSERT INTO [dbo].[FASK_ZASOBY]
                            ([ITEMNMBR], [ITEMDESC], [ITEMCODE], [VNDITNUM], [CZ_CarKod], [LOCNCODE], [SKL_ID], [QTY], [QTYPACK], [MJ], [DMJ], 
                             [TAXRATE], [PRICE0], [PRICE1], [PRICE2], [PRICE3], [PRICE4], [PRICE5], [CZ_SerNum_Track], [CZ_SerNum_Delka], 
                             [CZ_Rez1_Track], [CZ_Rez2_Track], [CZ_Rez3_Track], [CZ_Rez4_Track], [REZ1], [REZ2], [REZ3], [REZ4], [ODB_ID], 
                             [mena_ID], [SERLTNUM], [WEIGHT], [TIMEFROM], [TIMETO], [LSTMod], [loginid], [CZ_Expirace_Track], [EXPIRACE])
                            VALUES (@ITEMNMBR, @ITEMDESC, @ITEMCODE, @VNDITNUM, @CZ_CarKod, @LOCNCODE, @SKL_ID, @QTY, @QTYPACK, @MJ, @DMJ, 
                                    @TAXRATE, @PRICE0, @PRICE1, @PRICE2, @PRICE3, @PRICE4, @PRICE5, @CZ_SerNum_Track, @CZ_SerNum_Delka, 
                                    @CZ_Rez1_Track, @CZ_Rez2_Track, @CZ_Rez3_Track, @CZ_Rez4_Track, @REZ1, @REZ2, @REZ3, @REZ4, @ODB_ID, 
                                    @mena_ID, @SERLTNUM, @WEIGHT, @TIMEFROM, @TIMETO, @LSTMod, @loginid, @CZ_Expirace_Track, @EXPIRACE)";

                        command.Parameters.AddWithValue("@ITEMNMBR", record.ITEMNMBR);
                        command.Parameters.AddWithValue("@ITEMDESC", record.ITEMDESC);
                        command.Parameters.AddWithValue("@ITEMCODE", record.ITEMCODE);
                        command.Parameters.AddWithValue("@VNDITNUM", record.VNDITNUM);
                        command.Parameters.AddWithValue("@CZ_CarKod", record.CZ_CarKod);
                        command.Parameters.AddWithValue("@LOCNCODE", record.LOCNCODE);
                        command.Parameters.AddWithValue("@SKL_ID", record.SKL_ID);
                        command.Parameters.AddWithValue("@QTY", record.QTY);
                        command.Parameters.AddWithValue("@QTYPACK", record.QTYPACK ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MJ", record.MJ);
                        command.Parameters.AddWithValue("@DMJ", record.DMJ);
                        command.Parameters.AddWithValue("@TAXRATE", record.TAXRATE ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PRICE0", record.PRICE0 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PRICE1", record.PRICE1 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PRICE2", record.PRICE2 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PRICE3", record.PRICE3 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PRICE4", record.PRICE4 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PRICE5", record.PRICE5 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CZ_SerNum_Track", record.CZ_SerNum_Track);
                        command.Parameters.AddWithValue("@CZ_SerNum_Delka", record.CZ_SerNum_Delka);
                        command.Parameters.AddWithValue("@CZ_Rez1_Track", record.CZ_Rez1_Track);
                        command.Parameters.AddWithValue("@CZ_Rez2_Track", record.CZ_Rez2_Track);
                        command.Parameters.AddWithValue("@CZ_Rez3_Track", record.CZ_Rez3_Track);
                        command.Parameters.AddWithValue("@CZ_Rez4_Track", record.CZ_Rez4_Track);
                        command.Parameters.AddWithValue("@REZ1", record.REZ1 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@REZ2", record.REZ2 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@REZ3", record.REZ3 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@REZ4", record.REZ4 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ODB_ID", record.ODB_ID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@mena_ID", record.mena_ID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SERLTNUM", record.SERLTNUM ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@WEIGHT", record.WEIGHT ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TIMEFROM", record.TIMEFROM ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TIMETO", record.TIMETO ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LSTMod", record.LSTMod ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@loginid", record.loginid ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CZ_Expirace_Track", record.CZ_Expirace_Track);
                        command.Parameters.AddWithValue("@EXPIRACE", record.EXPIRACE ?? (object)DBNull.Value);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
