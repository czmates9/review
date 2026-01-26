
namespace FASK.SledovaniVyroby.ChipScannersIfc
{
    /// <summary>
    /// Rozhrani pro pripojeni chipovych sceneru.
    /// </summary>
    public interface IChipScannersConnector
    {
        /// <summary>
        /// Vrati instrukci na pozadavek na identifikaci scanneru
        /// </summary>
        string Idenfication { get; }

        /// <summary>
        /// Vrati instrukci na pozadavek na kod cipu ze scanneru
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Vrati instrukci na pozadavek na zablokovani cipovych scanneru
        /// </summary>
        string Block { get; }

        /// <summary>
        /// Vrati instrukci na pozadavek na odblokovani cipovych scanneru
        /// </summary>
        string Unblock { get; }

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
