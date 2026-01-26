namespace Fask.Module.Zbozi.DataSets {
    
    
    public partial class Configuration {
        public bool OnlineHmotnost
        {
            get
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("OnlineHmotnost");
                    if (parRow == null)
                        return false;
                    else
                        return bool.Parse(parRow.Value);

                }
                catch 
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("OnlineHmotnost");
                    if (parRow == null)
                    {
                        parRow = this.Parameters.AddParametersRow("OnlineHmotnost", value.ToString(), value.GetType().ToString());
                    }
                    else
                    {
                        parRow.Value = value.ToString();
                        parRow.Type = value.GetType().ToString();
                    }
                }
                catch
                {                    
                }
            }
        }

        public bool OnlineLokace
        {
            get
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("OnlineLokace");
                    if (parRow == null)
                        return false;
                    else
                        return bool.Parse(parRow.Value);

                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("OnlineLokace");
                    if (parRow == null)
                    {
                        parRow = this.Parameters.AddParametersRow("OnlineLokace", value.ToString(), value.GetType().ToString());
                    }
                    else
                    {
                        parRow.Value = value.ToString();
                        parRow.Type = value.GetType().ToString();
                    }
                }
                catch
                {
                }
            }
        }

    }
}
