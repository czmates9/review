using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes
{
    class Prijem
    {
		internal static void KontrolaVykriti_A_Smazani(Fask.DataSets.Prijem dsP,
			out List<Polozky_Prijem> polEdit,
			out List<Polozky_Prijem> polDelete,
			out List<string> listOV)
		{
			try
			{
				polEdit = new List<Polozky_Prijem>();
				polDelete = new List<Polozky_Prijem>();
				listOV = new List<string>();

				foreach (var rowPE in dsP.CZMST_PE.Where(x => x.QTYPACK == 0))
				{
					//var rows = dsV.CZMST_SI.ToList().Where(x => x.ITEMNMBR == rowSE.ITEMNMBR && x.ORD == rowSE.ORD);
					var rows = dsP.CZMST_PI.ToList().Where(
						x =>
							x.ITEMNMBR == rowPE.ITEMNMBR &&
							x.PONUMBER == rowPE.PONUMBER &&
							x.ORD == rowPE.ORD &&
							x.SKL_ID == rowPE.SKL_ID
					);

					if (rows == null || rows.Count() == 0)
					{
						polDelete.Add(new Polozky_Prijem()
						{
							ITEMNMBR = rowPE.ITEMNMBR,
							ORD = rowPE.ORD,
							SKL_ID = rowPE.SKL_ID,
							PONUMBER = rowPE.PONUMBER
						});

						listOV = Get_List_ID_OV(listOV, rowPE);
					}
					else
					{

						decimal qtyPI_Sum = rows.Sum(x => x.QTYSHPPD);

						decimal qty = rowPE.QTYSHPPD - qtyPI_Sum;

						if (qty != 0)
						{
							polEdit.Add(new Polozky_Prijem()
							{
								QTY = qtyPI_Sum,
								ITEMNMBR = rows.ToArray()[0].ITEMNMBR,
								ORD = rows.ToArray()[0].ORD,
								SKL_ID = rows.ToArray()[0].SKL_ID,
								PONUMBER = rows.ToArray()[0].PONUMBER,
								SELTNUM = null,
								Expirace = null
							});

							listOV = Get_List_ID_OV(listOV, rowPE);
						}

						if (rowPE.CZ_SerNum_Track == 2 || rowPE.CZ_SerNum_Track == 1)
						{
							var rows_group = rows.GroupBy(x => new { x.SERLTNUM }); // !!! serltnum != null ...
							foreach (var item in rows_group)
							{
								if (!string.IsNullOrEmpty(item.Key.SERLTNUM))
								{
									DateTime? expirace = null;
									if (rowPE.CZ_Expirace_Track > 0)
									{
										expirace = item.First().IsExpiraceNull() ? (DateTime?)null : item.First().Expirace;
										if ((expirace.HasValue) && !item.All(x => x.Expirace == expirace.Value))
											throw new Exception("Různé exspirace u jedné šarže !!!!");
									}

									polEdit.Add(new Polozky_Prijem()
									{
										QTY = null,
										ITEMNMBR = rowPE.ITEMNMBR.Trim(),
										ORD = rowPE.ORD,
										SKL_ID = rowPE.SKL_ID.Trim(),
										PONUMBER = rowPE.PONUMBER.Trim(),
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

		internal static void KontrolaVykriti_A_Smazani(Fask.DataSets.Prijem dsP,
			out List<Polozky_Prijem> polEdit,
			out List<Polozky_Prijem> polDelete)
		{
			try
			{
				polEdit = new List<Polozky_Prijem>();
				polDelete = new List<Polozky_Prijem>();

				foreach (var rowPE in dsP.CZMST_PE.Where(x => x.QTYPACK == 0))
				{
					//var rows = dsV.CZMST_SI.ToList().Where(x => x.ITEMNMBR == rowSE.ITEMNMBR && x.ORD == rowSE.ORD);
					var rows = dsP.CZMST_PI.ToList().Where(
						x =>
							x.ITEMNMBR == rowPE.ITEMNMBR &&
							x.PONUMBER == rowPE.PONUMBER &&
							x.ORD == rowPE.ORD &&
							x.SKL_ID == rowPE.SKL_ID
					);

					if (rows == null || rows.Count() == 0)
					{
						polDelete.Add(new Polozky_Prijem()
						{
							ITEMNMBR = rowPE.ITEMNMBR,
							ORD = rowPE.ORD,
							SKL_ID = rowPE.SKL_ID,
							PONUMBER = rowPE.PONUMBER
						});
					}
					else
					{

						decimal qtyPI_Sum = rows.Sum(x => x.QTYSHPPD);

						decimal qty = rowPE.QTYSHPPD - qtyPI_Sum;

						if (qty != 0)
						{
							polEdit.Add(new Polozky_Prijem()
							{
								QTY = qtyPI_Sum,
								ITEMNMBR = rows.ToArray()[0].ITEMNMBR,
								ORD = rows.ToArray()[0].ORD,
								SKL_ID = rows.ToArray()[0].SKL_ID,
								PONUMBER = rows.ToArray()[0].PONUMBER,
								SELTNUM = null,
								Expirace = null
							});
						}

						if (rowPE.CZ_SerNum_Track == 2 || rowPE.CZ_SerNum_Track == 1)
						{
							var rows_group = rows.GroupBy(x => new { x.SERLTNUM }); // !!! serltnum != null ...
							foreach (var item in rows_group)
							{
								if (!string.IsNullOrEmpty(item.Key.SERLTNUM))
								{
									DateTime? expirace = null;
									if (rowPE.CZ_Expirace_Track > 0)
									{
										expirace = item.First().IsExpiraceNull() ? (DateTime?)null : item.First().Expirace;
										if ((expirace.HasValue) && !item.All(x => x.Expirace == expirace.Value))
											throw new Exception("Různé exspirace u jedné šarže !!!!");
									}

									polEdit.Add(new Polozky_Prijem()
									{
										QTY = null,
										ITEMNMBR = rowPE.ITEMNMBR.Trim(),
										ORD = rowPE.ORD,
										SKL_ID = rowPE.SKL_ID.Trim(),
										PONUMBER = rowPE.PONUMBER.Trim(),
										SELTNUM = item.Key.SERLTNUM.Trim(),
										QTY_SELTNUM = item.Sum(x => x.QTYSHPPD),
										Expirace = expirace,
										CZ_SerNumTrack = rowPE.CZ_SerNum_Track
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


		private static List<string> Get_List_ID_OV(List<string> listOV, DataSets.Prijem.CZMST_PERow rowPE)
		{
			string ID_OV = Database.ABRA.Get_ID_OV(new Polozky_Prijem()
			{
				ITEMNMBR = rowPE.ITEMNMBR.Trim(),
				PONUMBER = rowPE.PONUMBER.Trim(),
				SKL_ID = rowPE.SKL_ID.Trim()
			});

			if (!string.IsNullOrEmpty(ID_OV))
			{
				if (!listOV.Contains(ID_OV.Trim()))
				{
					listOV.Add(ID_OV.Trim());
				}
			}

			return listOV;
		}

	}

    /// <summary>
    /// Pomocna třida ktera nese inforace o položce, misto objetu DataRow anebo DataTable....
    /// </summary>
    public class Polozky_Prijem
    {
        public decimal? QTY;
        public string ITEMNMBR;
        public int? ORD;
        public string SKL_ID;
        public string PONUMBER;
        public string SELTNUM;
        public decimal? QTY_SELTNUM;
		public DateTime? Expirace;
		public byte? CZ_SerNumTrack;
	}
}
