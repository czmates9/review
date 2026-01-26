using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace Zuby.Properties
{
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

    }
}
