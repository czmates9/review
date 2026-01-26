<%@ Page Title="" Language="C#" MasterPageFile="~/MP_Main.Master" AutoEventWireup="true" CodeBehind="Konfigurace_WebConfig.aspx.cs" Inherits="Fask.MST_W_Server.Konfigurace.Stranky.Konfigurace_WebConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Grafika/CSS/bootstrap.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/icheck-bootstrap.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/Konfigurace_WebConfig.css" type="text/css">
    <link rel="stylesheet" href="../../Grafika/CSS/MP_Main.css" type="text/css">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav class="col-sm-3" id="myScrollspy">
        <ul class="nav nav-pills nav-stacked">
            <li><a href="#section1">ConnectionString</a></li>
            <li><a href="#section2">Providers</a></li>
            <li><a href="#section3">Logs</a></li>
            <li><a href="#section4">Directories</a></li>
            <li><a href="#section5">PaletovyListek</a></li>
            <li><a href="#section6">Licenses</a></li>
            <li><a href="#section7">Image</a></li>
            <li><a href="#section8">Vyroba</a></li>
        </ul>
    </nav>

    <div class="col-sm-9">

        <div id="section1">
            <h1>ConnectionString</h1>
            <asp:Table ID="ConnectionString" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section2">
            <h1>Providers</h1>
            <asp:Table ID="Providers" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section3">
            <h1>Logs</h1>
            <asp:Table ID="Logs" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section4">
            <h1>Directories</h1>
            <asp:Table ID="Directories" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section5">
            <h1>PaletovyListek</h1>
            <asp:Table ID="PaletovyListek" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section6">
            <h1>Licenses</h1>
            <asp:Table ID="Licenses" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section7">
            <h1>Image</h1>
            <asp:Table ID="Image" runat="server" class="Tabulka">

                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Název parametru</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Hodnota parametru</asp:TableHeaderCell>
                </asp:TableHeaderRow>

            </asp:Table>
        </div>

        <div id="section8">
            <h1>Vyroba</h1>
            <asp:Table ID="Vyroba" runat="server" class="Tabulka">

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
    <script src="../../Grafika/JS/Konfigurace_WebConfig.js" type="text/javascript"></script>
    <script src="../../Grafika/JS/MP_Main.js" type="text/javascript"></script>
</asp:Content>
