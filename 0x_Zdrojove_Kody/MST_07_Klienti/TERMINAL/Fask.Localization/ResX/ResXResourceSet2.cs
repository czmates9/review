//------------------------------------------------------------------------------
// <copyright file="ResXResourceSet.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------
 
#if !SYSTEM_WEB
 
namespace System.Resources {
 
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
 
    using System;
    using System.Windows.Forms;
    using System.Reflection;
    using Microsoft.Win32;
    using System.Drawing;
    using System.IO;
    using System.ComponentModel;
    using System.Collections;
    using System.Resources;
    using Fask.Localization.ResX;
 
    /// <include file='doc\ResXResourceSet.uex' path='docs/doc[@for="ResXResourceSet"]/*' />
    /// <devdoc>
    ///     ResX resource set.
    /// </devdoc>
    //[System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.InheritanceDemand, Name="FullTrust")]
    //[System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Name="FullTrust")]
    public class ResXResourceSet2 : ResourceSet {
 
        /// <include file='doc\ResXResourceSet.uex' path='docs/doc[@for="ResXResourceSet.ResXResourceSet"]/*' />
        /// <devdoc>
        ///     Creates a resource set for the specified file.
        /// </devdoc>
        [
            SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")  // Shipped like this in Everett.
        ]
        public ResXResourceSet2(String fileName) {
            this.Reader = new ResXResourceReader2(fileName);
            this.Table = new Hashtable();
            ReadResources();
        }
 
        /// <include file='doc\ResXResourceSet.uex' path='docs/doc[@for="ResXResourceSet.ResXResourceSet1"]/*' />
        /// <devdoc>
        ///     Creates a resource set for the specified stream.
        /// </devdoc>
        [
            SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")  // Shipped like this in Everett.
        ]
        public ResXResourceSet2(Stream stream) {
            this.Reader = new ResXResourceReader2(stream);
            this.Table = new Hashtable();
            ReadResources();
        }
 
        /// <include file='doc\ResXResourceSet.uex' path='docs/doc[@for="ResXResourceSet.GetDefaultReader"]/*' />
        /// <devdoc>
        ///     Gets the default reader type associated with this set.
        /// </devdoc>
        public override Type GetDefaultReader() {
            return typeof(ResXResourceReader2);
        }
       
    }
}
 
#endif // !SYSTEM_WEB