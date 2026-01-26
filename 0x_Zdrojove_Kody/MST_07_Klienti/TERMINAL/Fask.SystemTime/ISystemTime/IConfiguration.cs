using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.SystemTime
{
    /// <summary>
    /// Zdroj, odkud se ma synchronizovat cas
    /// </summary>
    public enum ConfigurationSource
    {
        /// <summary>
        /// Neni konfigurovano, nebude se provadet synchronizace, respektive cas zustane zachovan
        /// </summary>
        None,
        /// <summary>
        /// Dotazem do databaze
        /// </summary>
        Database,
        /// <summary>
        /// Dotazem na web service
        /// </summary>
        WebService,
        /// <summary>
        /// Dotazem na casovy server
        /// </summary>
        NNTP
    }

    public interface IConfiguration
    {
        ConfigurationSource Source { get; set; }
        string Connection { get; set; }
    }
}
