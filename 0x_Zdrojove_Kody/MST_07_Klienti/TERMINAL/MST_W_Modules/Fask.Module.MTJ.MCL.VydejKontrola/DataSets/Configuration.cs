// Toto je prevzato z jimitore modulu ...
namespace Fask.Module.MTJ.MCL.VydejKontrola.DataSets
{      
    public partial class Configuration {
        partial class ParametersDataTable
        {
        }

        public string SQLConnectionString
        {
            get
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("SQLConnectionString");
                    if (parRow == null)
                    {
                        string def = @"Data Source=FASKCZ-CV005\SQL2008_2;Initial Catalog=Fask_06;Persist Security Info=True;User ID=sa;Password=sasa;Connect Timeout=1";
                        this.SQLConnectionString = def;   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
                        return def;
                    }
                    else
                        return parRow.Value.ToString();
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("SQLConnectionString");
                    if (parRow == null)
                    {
                        parRow = this.Parameters.AddParametersRow("SQLConnectionString", value.ToString(), value.GetType().ToString());
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

        public string ZvukShoda
        {
            get
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("ZvukShoda");
                    if (parRow == null)
                    {
                        string def = "necozbyva.wav";
                        this.ZvukShoda = def;   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
                        return def;
                    }
                    else
                        return parRow.Value.ToString();
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("ZvukShoda");
                    if (parRow == null)
                    {
                        parRow = this.Parameters.AddParametersRow("ZvukShoda", value.ToString(), value.GetType().ToString());
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

        public string ZvukNeShoda
        {
            get
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("ZvukNeShoda");
                    if (parRow == null)
                    {
                        string def = "8point1.wav";
                        this.ZvukNeShoda = def;   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
                        return def;
                    }
                    else
                        return parRow.Value.ToString();
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("ZvukNeShoda");
                    if (parRow == null)
                    {
                        parRow = this.Parameters.AddParametersRow("ZvukNeShoda", value.ToString(), value.GetType().ToString());
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

        //public bool DialogNeShoda
        //{
        //    get
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("DialogNeShoda");
        //            if (parRow == null)
        //            {
        //                bool def = false;
        //                this.DialogNeShoda = def;   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
        //                return def;
        //            }
        //            else
        //                return bool.Parse(parRow.Value);
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("DialogNeShoda");
        //            if (parRow == null)
        //            {
        //                parRow = this.Parameters.AddParametersRow("DialogNeShoda", value.ToString(), value.GetType().ToString());
        //            }
        //            else
        //            {
        //                parRow.Value = value.ToString();
        //                parRow.Type = value.GetType().ToString();
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        public int ShodaPrefixZnaku
        {
            get
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("ShodaPrefixZnaku");
                    if (parRow == null)
                    {
                        int def = 6;
                        this.ShodaPrefixZnaku = def;   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
                        return def;
                    }
                    else
                        return int.Parse(parRow.Value);
                }
                catch
                {
                    return 6;
                }
            }
            set
            {
                try
                {
                    ParametersRow parRow = this.Parameters.FindByName("ShodaPrefixZnaku");
                    if (parRow == null)
                    {
                        parRow = this.Parameters.AddParametersRow("ShodaPrefixZnaku", value.ToString(), value.GetType().ToString());
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


        // Toto je prevzato z jimitore modulu ...
        //public string PocetVytisku
        //{
        //    get
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("PocetVytisku");
        //            if (parRow == null)
        //            {
        //                this.PocetVytisku = string.Empty;   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
        //                return string.Empty;
        //            }
        //            else
        //                return parRow.Value.ToString();
        //        }
        //        catch
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("PocetVytisku");
        //            if (parRow == null)
        //            {
        //                parRow = this.Parameters.AddParametersRow("PocetVytisku", value.ToString(), value.GetType().ToString());
        //            }
        //            else
        //            {
        //                parRow.Value = value.ToString();
        //                parRow.Type = value.GetType().ToString();
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        //public bool PotvrzovaniPoctuVytisku
        //{
        //    get
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("PotvrzovaniPoctuVytisku");
        //            if (parRow == null)
        //            {
        //                PotvrzovaniPoctuVytisku = true;   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
        //                return true;
        //            }
        //            else
        //                return bool.Parse(parRow.Value);
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("PotvrzovaniPoctuVytisku");
        //            if (parRow == null)
        //            {
        //                parRow = this.Parameters.AddParametersRow("PotvrzovaniPoctuVytisku", value.ToString(), value.GetType().ToString());
        //            }
        //            else
        //            {
        //                parRow.Value = value.ToString();
        //                parRow.Type = value.GetType().ToString();
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        //public bool PovolitPodbarveniTlacitek
        //{
        //    get
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("PovolitPodbarveniTlacitek");
        //            if (parRow == null)
        //            {
        //                PovolitPodbarveniTlacitek = true;   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
        //                return true;
        //            }
        //            else
        //                return bool.Parse(parRow.Value);
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("PovolitPodbarveniTlacitek");
        //            if (parRow == null)
        //            {
        //                parRow = this.Parameters.AddParametersRow("PovolitPodbarveniTlacitek", value.ToString(), value.GetType().ToString());
        //            }
        //            else
        //            {
        //                parRow.Value = value.ToString();
        //                parRow.Type = value.GetType().ToString();
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        //public string ModuleBaleniColor
        //{
        //    // LimeGreen -> #32CD32
        //    get
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("ModuleBaleniColor");
        //            if (parRow == null)
        //            {
        //                this.ModuleBaleniColor = "#32CD32";   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
        //                return "#32CD32";
        //            }
        //            else
        //                return parRow.Value.ToString();
        //        }
        //        catch
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("ModuleBaleniColor");
        //            if (parRow == null)
        //            {
        //                parRow = this.Parameters.AddParametersRow("ModuleBaleniColor", value.ToString(), value.GetType().ToString());
        //            }
        //            else
        //            {
        //                parRow.Value = value.ToString();
        //                parRow.Type = value.GetType().ToString();
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        //public string ModuleVydejColor
        //{
        //    // Cyan -> #00ffff
        //    get
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("ModuleVydejColor");
        //            if (parRow == null)
        //            {
        //                this.ModuleVydejColor = "#00ffff";   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
        //                return "#00ffff";
        //            }
        //            else
        //                return parRow.Value.ToString();
        //        }
        //        catch
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("ModuleVydejColor");
        //            if (parRow == null)
        //            {
        //                parRow = this.Parameters.AddParametersRow("ModuleVydejColor", value.ToString(), value.GetType().ToString());
        //            }
        //            else
        //            {
        //                parRow.Value = value.ToString();
        //                parRow.Type = value.GetType().ToString();
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        //public string ModulePrijemZbytkuColor
        //{
        //    // LemonChiffon -> #FFFACD
        //    get
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("ModulePrijemZbytkuColor");
        //            if (parRow == null)
        //            {
        //                this.ModulePrijemZbytkuColor = "#FFFACD";   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
        //                return "#FFFACD";
        //            }
        //            else
        //                return parRow.Value.ToString();
        //        }
        //        catch
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("ModulePrijemZbytkuColor");
        //            if (parRow == null)
        //            {
        //                parRow = this.Parameters.AddParametersRow("ModulePrijemZbytkuColor", value.ToString(), value.GetType().ToString());
        //            }
        //            else
        //            {
        //                parRow.Value = value.ToString();
        //                parRow.Type = value.GetType().ToString();
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        //public string ModulePrijemDlePodkladuColor
        //{
        //    // 128; 192; 255 -> #80C0FF
        //    get
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("ModulePrijemDlePodkladuColor");
        //            if (parRow == null)
        //            {
        //                this.ModulePrijemDlePodkladuColor = "#80C0FF";   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
        //                return "#80C0FF";
        //            }
        //            else
        //                return parRow.Value.ToString();
        //        }
        //        catch
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("ModulePrijemDlePodkladuColor");
        //            if (parRow == null)
        //            {
        //                parRow = this.Parameters.AddParametersRow("ModulePrijemDlePodkladuColor", value.ToString(), value.GetType().ToString());
        //            }
        //            else
        //            {
        //                parRow.Value = value.ToString();
        //                parRow.Type = value.GetType().ToString();
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        //public string ModuleKontrolaColor
        //{
        //    // 255; 128; 255 -> #ff80e1
        //    get
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("ModuleKontrolaColor");
        //            if (parRow == null)
        //            {
        //                this.ModuleKontrolaColor = "#ff80e1";   // aby se zapsal do datasetu, pokud neni jeste nastaven ...
        //                return "#ff80e1";
        //            }
        //            else
        //                return parRow.Value.ToString();
        //        }
        //        catch
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            ParametersRow parRow = this.Parameters.FindByName("ModuleKontrolaColor");
        //            if (parRow == null)
        //            {
        //                parRow = this.Parameters.AddParametersRow("ModuleKontrolaColor", value.ToString(), value.GetType().ToString());
        //            }
        //            else
        //            {
        //                parRow.Value = value.ToString();
        //                parRow.Type = value.GetType().ToString();
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
    }
}
