<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="oops.aspx.cs" Inherits="Fask.MST_W_Server.ErrorPages.oops" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Error</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700,900" rel="stylesheet"/>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="notfound">
        <div class="notfound">
            <div class="notfound-404">
                <h1>Oops!</h1>
            </div>
            <h2>Chyba, ale nic si z toho nedělej, třeba máš zase jiný klady ...</h2>
            <p>Chyby jsou důkazem toho, že se o něco snažíš.</p>
            <asp:LinkButton ID="ButtonBack" runat="server" Text="Zpět na začátek..." OnClick="ButtonBack_Click"></asp:LinkButton>
        </div>
    </div>
    </form>
</body>
</html>
