<%@ Page Title="" Language="C#" MasterPageFile="~/MP_Main.Master" AutoEventWireup="true" CodeBehind="Konfigurace_ProviderPohodaXML.aspx.cs" Inherits="Fask.MST_W_Server.Konfigurace.Stranky.Konfigurace_ProviderPohodaXML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Grafika/CSS/bootstrap.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/icheck-bootstrap.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/Konfigurace_ProviderPohodaXML.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/MP_Main.css" type="text/css">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <nav class="col-sm-3" id="myScrollspy">
        <ul class="nav nav-pills nav-stacked">
            <li><a href="#section1">Pohoda Info</a></li>
            <li><a href="#section2">Connection Strings</a></li>
            <li><a href="#section3">Sdilené</a></li>
            <li><a href="#section4">Inventura 1</a></li>
            <li><a href="#section5">Výdej</a></li>
            <li><a href="#section6">Příjem</a></li>
            <li><a href="#section7">Prodej</a></li>
            <li><a href="#section8">Exporty zasoby</a></li>
            <li><a href="#section9">Com. Driver Map.</a></li>
            <li><a href="#section10">Ukolovani</a></li>
            <li><a href="#section10">LokMech</a></li>
        </ul>
    </nav>

    <div class="col-sm-9">
        <div id="section1">
            <h1>Pohoda Info</h1>
            <asp:Table ID="PohodaInfo" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section2">
            <h1>Connection Strings</h1>
            <asp:Table ID="ConnectionStrings" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section3">
            <h1>Sdilené</h1>
            <asp:Table ID="Sdilene" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section4">
            <h1>Inventura 1</h1>

            <asp:Table ID="Inventura1" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section5">
            <h1>Výdej</h1>
            <asp:Table ID="Vydej" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section6">
            <h1>Příjem</h1>
            <asp:Table ID="Prijem" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section7">
            <h1>Prodej</h1>
            <asp:Table ID="Prodej" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section8">
            <h1>Exporty zasoby</h1>
            <asp:Table ID="ExportZasoby" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section9">
            <h1>Com. Driver Map.</h1>
            <asp:Table ID="ComunicatorDriveMapping" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section10">
            <h1>Ukolovani</h1>
            <asp:Table ID="Ukolovani" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section11">
            <h1>LokMech</h1>
            <asp:Table ID="LokMech" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div class="col-sm-12" style="height: 150px">
        </div>

    </div>

    <asp:Button class="BTN" ID="Button_Save" runat="server" OnClick="Button_Save_Click" meta:resourcekey="Button_Save" />

    <a href="#" class="back-to-top"></a>


</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptyJS" runat="server">
    <script src="../../Grafika/JS/jquery.js" type="text/javascript"></script>
    <script src="../../Grafika/JS/bootstrap.js" type="text/javascript"></script>
    <script src="../../Grafika/JS/Scrollspy.js" type="text/javascript"></script>
    <script src="../../Grafika/JS/Konfigurace_ProviderPohodaXML.js" type="text/javascript"></script>
    <script src="../../Grafika/JS/MP_Main.js" type="text/javascript"></script>
</asp:Content>
