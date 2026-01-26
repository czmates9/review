using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Fask.Module.ABRA.CarpServise.Properties
{
	/// <summary>
	/// Metoda která upravuje načítavání položek z časti settings projektu
	/// </summary>
    public sealed partial class Settings
    {
        private List<ConfigurationElement> list;

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            this.OpenAndStoreConfiguration();
        }

        /// <summary>
        /// Opens the dll.config file and reads its sections into a private List of ConfigurationElement.
        /// </summary>
        private void OpenAndStoreConfiguration()
        {
            string codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            Uri p = new Uri(codebase);
            string localPath = p.LocalPath;
            string executingFilename = System.IO.Path.GetFileNameWithoutExtension(localPath);
            string sectionGroupName = "applicationSettings";
            string sectionName = executingFilename + ".Properties.Settings";
            string configName = localPath + ".config";
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configName;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            list = new List<ConfigurationElement>();

            // read section of properties
            var sectionGroup = config.GetSectionGroup(sectionGroupName);
            if (sectionGroup != null)
            {
                var settingsSection = (ClientSettingsSection)sectionGroup.Sections[sectionName];
                if (settingsSection != null)
                    list.AddRange(settingsSection.Settings.OfType<ConfigurationElement>().ToList());
            }

            // read section of Connectionstrings
            var sections = config.Sections.OfType<ConfigurationSection>();
            if (sections != null)
            {
                var connSection = (from section in sections
                                   where section.GetType() == typeof(ConnectionStringsSection)
                                   select section).FirstOrDefault() as ConnectionStringsSection;
                if (connSection != null)
                    list.AddRange(connSection.ConnectionStrings.Cast<ConfigurationElement>());
            }

            
            // Naplnim znovu propertyValues ... 
            //this.PropertyValues.Clear();
            foreach (var result in list)
            {
                SettingsPropertyValue spv = null;

                if (result.ElementInformation.Type == typeof(ConnectionStringSettings))
                {
                    ConnectionStringSettings css = (ConnectionStringSettings)result;
                    string[] cssNameSpace = css.Name.Split('.');
                    string cssName = cssNameSpace[cssNameSpace.Length - 1];
                    if (this.Properties[cssName] == null)
                        continue;
                    spv = this.PropertyValues[cssName];
                    if (spv != null)
                        this.PropertyValues.Remove(cssName);
                    spv = new SettingsPropertyValue(this.Properties[cssName]);
                    spv.SerializedValue = css.ConnectionString;
                    spv.IsDirty = false;
                    this.PropertyValues.Add(spv);
                }
                else if (result.ElementInformation.Type == typeof(SettingElement))
                {
                    SettingElement se = (SettingElement)result;
                    if (this.Properties[se.Name] == null)
                        continue;
                    spv = this.PropertyValues[se.Name];
                    if (spv != null)
                        this.PropertyValues.Remove(se.Name);
                    spv = new SettingsPropertyValue(this.Properties[se.Name]);
                    spv.SerializedValue = se.Value.ValueXml.InnerText;
                    spv.IsDirty = false;
                    this.PropertyValues.Add(spv);
                }
            }

        }

        ///// <summary>
        ///// Gets or sets the <see cref="System.Object"/> with the specified property name.
        ///// </summary>
        ///// <value></value>
        //public override object this[string propertyName]
        ////public object this[string propertyName]
        //{
        //    get
        //    {
        //        var result = (from item in list
        //                      where Convert.ToString(item.ElementInformation.Properties["name"].Value).Contains(propertyName)
        //                      select item).FirstOrDefault();
        //        if (result != null)
        //        {
        //            if (result.ElementInformation.Type == typeof(ConnectionStringSettings))
        //            {
        //                ConnectionStringSettings css = (ConnectionStringSettings)result;
        //                return css.ConnectionString;
        //            }
        //            else if (result.ElementInformation.Type == typeof(SettingElement))
        //            {
        //                SettingElement se = (SettingElement)result;
        //                //return se.Value.ValueXml.InnerText + "/" + se.SerializeAs.ToString();
        //                //this.Properties[propertyName].PropertyType.
        //                if (this.PropertyValues[propertyName] == null)
        //                {
        //                    SettingsPropertyValue sv = new SettingsPropertyValue(this.Properties[propertyName]);
        //                    sv.SerializedValue = se.Value.ValueXml.InnerText;
        //                    sv.IsDirty = false;
        //                    this.PropertyValues.Add(sv);
        //                }
        //                return this.PropertyValues[propertyName].PropertyValue;
        //            }
        //        }
        //        return null;
        //    }
        //    // ignore
        //    set
        //    {
        //        base[propertyName] = value;
        //    }
        //}
    }
}
