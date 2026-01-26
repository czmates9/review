<%@ Page Title="" Language="C#" MasterPageFile="~/MP_Main.Master" AutoEventWireup="true" CodeBehind="Konfigurace_Agendy.aspx.cs" Inherits="Fask.MST_W_Server.Konfigurace.Stranky.Konfigurace_Agendy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Grafika/CSS/bootstrap.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/icheck-bootstrap.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/Konfigurace_Agendy.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/MP_Main.css" type="text/css">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <nav class="col-sm-3" id="myScrollspy">
        <ul class="nav nav-pills nav-stacked">
            <li><a href="#section1">Inventura 1</a></li>
            <li><a href="#section2">Inventura 2</a></li>
            <li><a href="#section3">Příjem</a></li>
            <li><a href="#section4">Výdej</a></li>
            <li><a href="#section5">Servis</a></li>
        </ul>
    </nav>

    <div class="col-sm-9">
        <div id="section1">
            <h1>Inventura 1</h1>
            <asp:Table ID="Inventura1Parametry" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section2">
            <h1>Inventura 2</h1>
            <asp:Table ID="Inventura2Parametry" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section3">
            <h1>Příjem</h1>
            <asp:Table ID="PrijemParametry" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section4">
            <h1>Výdej</h1>

            <asp:Table ID="VydejParametry" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section5">
            <h1>Servis</h1>
            <asp:Table ID="ServisParametry" runat="server" class="Tabulka">

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
    <script src="../../Grafika/JS/Konfigurace_Agendy.js" type="text/javascript"></script>
    <script src="../../Grafika/JS/MP_Main.js" type="text/javascript"></script>
</asp:Content>
