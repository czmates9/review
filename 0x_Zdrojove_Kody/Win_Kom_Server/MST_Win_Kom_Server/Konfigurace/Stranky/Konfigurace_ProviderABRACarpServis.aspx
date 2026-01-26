<%@ Page Title="" Language="C#" MasterPageFile="~/MP_Main.Master" AutoEventWireup="true" CodeBehind="Konfigurace_ProviderABRACarpServis.aspx.cs" Inherits="Fask.MST_W_Server.Konfigurace.Stranky.Konfigurace_ProviderABRACarpServis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Grafika/CSS/bootstrap.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/icheck-bootstrap.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/Konfigurace_ProviderABRACarpServis.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/MP_Main.css" type="text/css">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <nav class="col-sm-3" id="myScrollspy">
        <ul class="nav nav-pills nav-stacked">
            <li><a href="#section1">Connection String</a></li>
            <li><a href="#section2">Inventura</a></li>
            <li><a href="#section3">Prijem</a></li>
            <li><a href="#section4">Vydej</a></li>
            <li><a href="#section5">Prodej</a></li>
            <li><a href="#section6">ExportZasoby</a></li>
        </ul>
    </nav>

    <div class="col-sm-9">

        <div id="section1">
            <h1>Connection String</h1>
            <asp:Table ID="ConnectionString" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section2">
            <h1>Inventura</h1>
            <asp:Table ID="Inventura" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section3">
            <h1>Prijem</h1>
            <asp:Table ID="Prijem" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section4">
            <h1>Vydej</h1>
            <asp:Table ID="Vydej" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section5">
            <h1>Prodej</h1>
            <asp:Table ID="Prodej" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

                <div id="section6">
            <h1>ExportZasoby</h1>
            <asp:Table ID="ExportZasoby" runat="server" class="Tabulka">

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
    <script src="../../Grafika/JS/Konfigurace_ProviderABRACarpServis.js" type="text/javascript"></script>
    <script src="../../Grafika/JS/MP_Main.js" type="text/javascript"></script>
</asp:Content>
