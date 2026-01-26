using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fask.PhotoProvider
{
    public interface IPhotoProvider
    {
        // TODO: formulare pro konfiguraci kvality fotek, ...

        /// <summary>
        /// Scanner, nutny pro vypnuti pri foceni na zarizenich MC s 2D imagerem.
        /// </summary>
        Fask.ScannerProvider.IScannerProvider Scanner { get; set; }

        /// <summary>
        /// Potreba kvuli foceni pomoci ES400.
        /// </summary>
        /// <param name="topLevelForm"></param>
        void SetForm(System.Windows.Forms.Form topLevelForm);

        /// <summary>
        /// Nazev nove vytvoreneho souboru bez cesty k souboru (napr. Fotka.jpg).
        /// </summary>
        string ImageFilename { get; set; }
        
        /// <summary>
        /// Adresar pro ukladani fotografii
        /// </summary>
        string ImagesDirectory { get; set; }

        /// <summary>
        /// Foceni.
        /// </summary>
        /// <returns></returns>
        DialogResult CaptureImage();

        /// <summary>
        /// Foceni s zadanym nazvem souboru.
        /// </summary>
        /// <param name="filename">Nazev fotky, jak se ma jmenovat (napr. 'Fotka').</param>
        /// <returns></returns>
        DialogResult CaptureImage(string filename);

        /// <summary>
        /// Foceni s zadanym nazvem souboru.
        /// </summary>
        /// <param name="filename">Nazev fotky, jak se ma jmenovat (napr. 'Fotka').</param>
        /// <param name="statusBarText">Text ve statusbaru.</param>
        /// <returns></returns>
        DialogResult CaptureImage(string filename, string statusBarText);
        
        /// <summary>
        /// Foceni v sernivsnim modulu (kvuli kroku zpet).
        /// </summary>
        /// <returns></returns>
        DialogResult CaptureServisImage();

        /// <summary>
        /// Foceni s zadanym nazvem souboru v sernivsnim modulu (kvuli kroku zpet).
        /// </summary>
        /// <param name="filename">Nazev fotky, jak se ma jmenovat (napr. 'Fotka').</param>
        /// <returns></returns>
        DialogResult CaptureServisImage(string filename);

        /// <summary>
        /// Foceni s zadanym nazvem souboru v sernivsnim modulu (kvuli kroku zpet).
        /// </summary>
        /// <param name="filename">Nazev fotky, jak se ma jmenovat (napr. 'Fotka').</param>
        /// <param name="statusBarText">Text ve statusbaru.</param>
        /// <returns>OK - vse v poradku, Cancel - Storno, Retry - Krok zpet</returns>
        DialogResult CaptureServisImage(string filename, string statusBarText);
    }
}
