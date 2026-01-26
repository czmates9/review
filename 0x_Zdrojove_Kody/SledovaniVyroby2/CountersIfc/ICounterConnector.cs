
namespace FASK.SledovaniVyroby.CountersIfc
{
    /// <summary>
    /// Rozhrani pro pripojeni citacu.
    /// </summary>
    public interface ICounterConnector
    {
        /// <summary>
        /// Vrati instrukci na pozadavek o aktualni hodnotu
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Vrati instrukci na pozadavek na vynulovani dosavadni hodnoty citace
        /// </summary>
        string Reset { get; }

        ///// <summary>
        ///// Vrati instrukci na pozadavek na precteni soucasneho modu citace
        ///// </summary>
        //string GetBasicOperationMode { get; }

        ///// <summary>
        ///// Vrati instrukci na pozadavek na nastaveni zakladniho modu citace
        ///// </summary>
        //string SetBasicOperationMode { get; }

        ///// <summary>
        ///// Vrati instrukci na pozadavek na precteni soucasneho submodu citace
        ///// </summary>
        ///// <param name="mode">Soucasny mod.</param>
        ///// <param name="instruction">Vystupni isntrukce.</param>
        ///// <returns>Uspech || neuspech</returns>
        //bool GetBasicOperationSubMode(string mode, out string instruction);

        ///// <summary>
        ///// Vrati instrukci na pozadavek na nastaveni submodu citace
        ///// </summary>
        ///// <param name="mode">Soucasny mod.</param>
        ///// <param name="instruction">Vystupni isntrukce.</param>
        ///// <returns>Uspech || neuspech</returns>
        //bool SetBasicOperationSubMode(string mode, out string instruction);

        ///// <summary>
        ///// Vrati instrukci na pozadavek na cteni predvolby (predvoleb ? - 717) citace
        ///// </summary>
        //string GetPreset { get; }

        ///// <summary>
        ///// Vrati instrukci na pozadavek na nastaveni hodnoty predvolby citace
        ///// </summary>
        //string SetPreset { get; }

        /// <summary>
        /// Zpracovani - rozparsovani odpovedi, ktera prisla na nejakou instrukci
        /// </summary>
        /// <param name="answer">Odpoved ke zpracovani</param>
        /// <param name="instruction">Instrukce, na kterou odpoved prisla</param>
        /// <param name="note">Poznamka k vykonavani instrukce (napriklad preteceni), nebo string.Empty</param>
        /// <param name="data">Odpoved na instrukci, nebo string.Empty, pokud instrukce nevyzaduje datovou (napr hodnota citace) odpoved</param>
        /// <returns>Uspech ci neuspech</returns>
        bool processAnswer(string answer, string instruction, ref string note, ref string data);
    }
}
