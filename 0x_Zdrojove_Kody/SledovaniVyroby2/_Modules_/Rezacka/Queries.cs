using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Rezacka
{
    /// <summary>
    /// Trida opbsahujici databazove dotazy.
    /// </summary>
    internal static class Queries
    {
        /// <summary>
        /// Ziskani vsech informaci k dane operaci dle jejiho ID a typu stroje.
        /// </summary>
        internal static string getInformationsAboutOperations = "SELECT * FROM fask_operations WHERE (ck = @ck) AND (machinetype = @machinetype)";

        /// <summary>
        /// Ziskani nasledujici operace k dane operaci
        /// </summary>
        internal static string getNextOperations = "SELECT * FROM fask_operations_next WHERE (ido = @ido) AND (machinetype = @machinetype)";

        /// <summary>
        /// Ziska posledni 1 radek stavove tabulky pro typ operace @operationtype pro stroj @machinetype = posledni stav
        /// </summary>
        internal static string restoreOperations = "SELECT TOP 1 * FROM fask_currentstate WHERE (machinetype = @machinetype) AND (operationtype = @operationtype) ORDER BY dateeve DESC";

        /// <summary>
        /// Ziska poslednich ulozeych $NUMBER$ radku (musi se zamenit za opravdove cislo)
        /// </summary>
        internal static string getHistoricalOperations = "SELECT TOP $NUMBER$ dateeve, ido, description, zakazka, material, scan1, scan2, scan3, sensor FROM fask_events ORDER BY dateeve DESC";
    }
}
