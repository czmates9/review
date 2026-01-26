<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Fask.MST_W_Server.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Prihlaseni</title>
    <link rel="stylesheet" href="Grafika/CSS/bootstrap.css" type="text/css" />
    <link rel="stylesheet" href="Grafika/CSS/Login.css" type="text/css" />
    <link rel="icon" type="image/png" sizes="32x32" href="Grafika/Image/favicon-32x32.png" />
    <link rel="icon" type="image/png" sizes="16x16" href="Grafika/Image/favicon-16x16.png" />
</head>
<body >

<div class="sidenav">
    <div class="login-main-text">
        <h2>
            <asp:label ID="TextNadpis" runat="server" meta:resourcekey="TextNadpis"></asp:label>
        </h2>
        <asp:label ID="TextUvitaci" runat="server" meta:resourcekey="TextUvitaci"></asp:label>
    </div>
    <div>
     <img id="ImageCode" src="Grafika/Image/qr-code_Transparent.png" alt="QR Code FASK" /> 
    </div>
</div>

<div class="main">
    <div class="col-md-6 col-sm-12">
        <div class="login-form">
            <form id="form1" runat="server">
                <asp:Login ID="Login1" runat="server" DisplayRememberMe="False" EnableTheming="True" OnAuthenticate="Login1_Authenticate" Width="100%">
                <LayoutTemplate>
                    <div class="form-group">
                        <asp:label ID="TextUserID" runat="server" meta:resourcekey="TextUserID" class="label "></asp:label>
                        <asp:TextBox ID="UserName" runat="server" meta:resourcekey="UserName" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" meta:resourcekey="UserNameRequired" runat="server" ControlToValidate="UserName" ValidationGroup="Login1"></asp:RequiredFieldValidator>
                    </div>
                    
                    <div class="form-group">
                        <asp:label   ID="TextPassword" runat="server" meta:resourcekey="TextPassword" class="label"></asp:label>
                        <asp:TextBox ID="Password"  runat  ="server"   meta:resourcekey="Password" class="form-control"  TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" meta:resourcekey="PasswordRequired" runat="server" ControlToValidate="Password"  ValidationGroup="Login1"></asp:RequiredFieldValidator>
                    </div>
                    
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                    <asp:Button ID="LoginButton" meta:resourcekey="LoginButton" class="btn btn-black" runat="server" CommandName="Login"  ValidationGroup="Login1" />
                    
                </LayoutTemplate>
                </asp:Login>
            </form>
        </div>
    </div>
    <div id="LabelVerze" >
            <asp:label ID="LabelV" style="opacity: 0;"  runat="server" Text="XXX"></asp:label>
        <br />
        <asp:label  ID="Label_Provider" style="opacity: 0;" runat="server" Text="XXX"></asp:label>
        <br />
        <asp:label  ID="Label_Databaze" style="opacity: 0;" runat="server" Text="XXX"></asp:label>
    </div>
</div>

<script src="Grafika/JS/jquery.js" type="text/javascript"></script>
<script src="Grafika/JS/bootstrap.js" type="text/javascript"></script>
<script src="Grafika/JS/Login.js" type="text/javascript"></script>

</body>
</html>
