using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes
{

    public class Vydej
    {
        /// <summary>
		/// Nepouživa sa...
		/// </summary>
		/// <param name="dsV"></param>
		/// <param name="pol"></param>
		/// <returns></returns>
        internal static bool KontrolaVykriti(DataSets.Vydej dsV, out List<Polozky_Vydej> pol)
        {
            try
            {
                pol = new List<Polozky_Vydej>();

                foreach (var item in dsV.CZMST_SI)
                {
                    var rows =  dsV.CZMST_SE.ToList().Where(x => x.ITEMNMBR == item.ITEMNMBR && x.ORD == item.ORD );

                    if (rows.Count() != 1)
                        throw new Exception("Nalezeno jine množství položek než je jedna.");

                    decimal qty = rows.ToArray()[0].QTYSHPPD - item.QTYSHPPD;

                    if(qty != 0)
                    {
                        pol.Add(new Polozky_Vydej() { QTY = item.QTYSHPPD , ITEMNMBR = item.ITEMNMBR, SKL_ID = item.SKL_ID, SOPNUMBE = item.SOPNUMBE  });
                    }
                }

                if (pol.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw (ex);
            }

        }

		internal static void KontrolaVykriti_A_Smazani(Fask.DataSets.Vydej dsV,
		out List<Polozky_Vydej> polEdit,
		out List<Polozky_Vydej> polDelete,
		out List<string> listOP)
		{
			try
			{
				polEdit = new List<Polozky_Vydej>();
				polDelete = new List<Polozky_Vydej>();
				listOP = new List<string>();

				foreach (var rowSE in dsV.CZMST_SE.Where(x => x.QTYPACK == 0))
				{
					//var rows = dsV.CZMST_SI.ToList().Where(x => x.ITEMNMBR == rowSE.ITEMNMBR && x.ORD == rowSE.ORD);
					var rows = dsV.CZMST_SI.ToList().Where(
						x =>
							x.ITEMNMBR == rowSE.ITEMNMBR &&
							x.SOPNUMBE == rowSE.SOPNUMBE &&
							x.ORD == rowSE.ORD &&
							x.SKL_ID == rowSE.SKL_ID
					);


					if (rows == null || rows.Count() == 0)
					{
						polDelete.Add(new Polozky_Vydej() { 
							ITEMNMBR = rowSE.ITEMNMBR,
							ORD = rowSE.ORD,
							SKL_ID = rowSE.SKL_ID, 
							SOPNUMBE = rowSE.SOPNUMBE 
						});

						listOP = Get_List_ID_OP(listOP, rowSE);
					}
					else
					{

						decimal qtySI_Sum = rows.Sum(x => x.QTYSHPPD);

						decimal qty = rowSE.QTYSHPPD - qtySI_Sum;

						if(qty != 0)
						{
							polEdit.Add(new Polozky_Vydej()
							{
								QTY = qtySI_Sum,
								ITEMNMBR = rows.ToArray()[0].ITEMNMBR,
								ORD = rows.ToArray()[0].ORD,
								SKL_ID = rows.ToArray()[0].SKL_ID,
								SOPNUMBE = rows.ToArray()[0].SOPNUMBE,
								SELTNUM = null,
								Expirace = null
							});

							listOP = Get_List_ID_OP(listOP, rowSE);
						}

						if (rowSE.CZ_SerNum_Track == 2 || rowSE.CZ_SerNum_Track == 1)
						{
							var rows_group = rows.GroupBy(x => new { x.SERLTNUM }); // !!! serltnum != null ...
							foreach (var item in rows_group)
							{
								if (!string.IsNullOrEmpty(item.Key.SERLTNUM))
								{
									DateTime? expirace = null;
									if (rowSE.CZ_Expirace_Track > 0)
									{
										expirace = item.First().IsExpiraceNull() ? (DateTime?)null : item.First().Expirace;
										if ((expirace.HasValue) && !item.All(x => x.Expirace == expirace.Value))
											throw new Exception("Různé exspirace u jedné šarže !!!!");
									}

									polEdit.Add(new Polozky_Vydej()
									{
										QTY = null,
										ITEMNMBR = rowSE.ITEMNMBR.Trim(),
										ORD = rowSE.ORD,
										SKL_ID = rowSE.SKL_ID.Trim(),
										SOPNUMBE = rowSE.SOPNUMBE.Trim(),
										SELTNUM = item.Key.SERLTNUM.Trim(),
										QTY_SELTNUM = item.Sum(x => x.QTYSHPPD),
										Expirace = expirace
									}); ;
								}
							}
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw (ex);
			}

		}

		private static List<string> Get_List_ID_OP(List<string> listOP, DataSets.Vydej.CZMST_SERow rowSE)
		{
			string ID_OP = Database.ABRA.Get_ID_OP(new Polozky_Vydej()
			{
				ITEMNMBR = rowSE.ITEMNMBR.Trim(),
				SOPNUMBE = rowSE.SOPNUMBE.Trim(),
				SKL_ID = rowSE.SKL_ID.Trim()
			});

			if (!string.IsNullOrEmpty(ID_OP))
			{
				if (!listOP.Contains(ID_OP.Trim()))
				{
					listOP.Add(ID_OP.Trim());
				}
			}

			return listOP;
		}

		#region Prevodka

		internal static void KontrolaVykriti_A_Smazani_PRV(Fask.DataSets.Vydej dsV, out List<Polozky_Vydej> polEdit, out List<Polozky_Vydej> polDelete)
		{
			try
			{
				polEdit = new List<Polozky_Vydej>();
				polDelete = new List<Polozky_Vydej>();

				foreach (var rowSE in dsV.CZMST_SE.Where(x => x.QTYPACK == 0))
				{
					//var rows = dsV.CZMST_SI.ToList().Where(x => x.ITEMNMBR == rowSE.ITEMNMBR && x.ORD == rowSE.ORD);
					var rows = dsV.CZMST_SI.ToList().Where(
						x =>
							x.ITEMNMBR == rowSE.ITEMNMBR &&
							x.SOPNUMBE == rowSE.SOPNUMBE &&
							x.ORD == rowSE.ORD &&
							x.SKL_ID == rowSE.SKL_ID
					);


					if (rows == null || rows.Count() == 0)
					{
						polDelete.Add(new Polozky_Vydej()
						{
							ITEMNMBR = rowSE.ITEMNMBR,
							ORD = rowSE.ORD,
							SKL_ID = rowSE.SKL_ID,
							SOPNUMBE = rowSE.SOPNUMBE
						});

						//listOP = Get_List_ID_OP(listOP, rowSE);
					}
					else
					{

						decimal qtySI_Sum = rows.Sum(x => x.QTYSHPPD);

						decimal qty = rowSE.QTYSHPPD - qtySI_Sum;

						if (qty != 0)
						{
							polEdit.Add(new Polozky_Vydej()
							{
								QTY = qtySI_Sum,
								ITEMNMBR = rows.ToArray()[0].ITEMNMBR,
								ORD = rows.ToArray()[0].ORD,
								SKL_ID = rows.ToArray()[0].SKL_ID,
								SOPNUMBE = rows.ToArray()[0].SOPNUMBE,
								SELTNUM = null,
								Expirace = null
							});

							//listOP = Get_List_ID_OP(listOP, rowSE);
						}

						if (rowSE.CZ_SerNum_Track == 2 || rowSE.CZ_SerNum_Track == 1)
						{
							var rows_group = rows.GroupBy(x => new { x.SERLTNUM }); // !!! serltnum != null ...
							foreach (var item in rows_group)
							{
								if (!string.IsNullOrEmpty(item.Key.SERLTNUM))
								{
									DateTime? expirace = null;
									if (rowSE.CZ_Expirace_Track > 0)
									{
										expirace = item.First().IsExpiraceNull() ? (DateTime?)null : item.First().Expirace;
										if ((expirace.HasValue) && !item.All(x => x.Expirace == expirace.Value))
											throw new Exception("Různé exspirace u jedné šarže !!!!");
									}

									polEdit.Add(new Polozky_Vydej()
									{
										QTY = null,
										ITEMNMBR = rowSE.ITEMNMBR.Trim(),
										ORD = rowSE.ORD,
										SKL_ID = rowSE.SKL_ID.Trim(),
										SOPNUMBE = rowSE.SOPNUMBE.Trim(),
										SELTNUM = item.Key.SERLTNUM.Trim(),
										QTY_SELTNUM = item.Sum(x => x.QTYSHPPD),
										Expirace = expirace
									}); ;
								}
							}
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw (ex);
			}

		}


		#endregion

	}

	/// <summary>
	/// Pomocna třida ktera nese inforace o položce, misto objetu DataRow anebo DataTable....
	/// </summary>
	public class Polozky_Vydej
    {
        public decimal? QTY;
        public string ITEMNMBR;
		public int? ORD;
		public string SKL_ID;
        public string SOPNUMBE;
        public string SELTNUM;
		public decimal? QTY_SELTNUM;
		public DateTime? Expirace;
	}
}
