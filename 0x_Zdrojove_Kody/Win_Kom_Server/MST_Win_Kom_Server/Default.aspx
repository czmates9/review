<%@ Page Title="" Language="C#" MasterPageFile="~/MP_Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MST_Win_Konfig_Server.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Grafika/CSS/bootstrap.css" type="text/css">
    <link rel="stylesheet" href="Grafika/CSS/Default.css" type="text/css">
    <link rel="stylesheet" href="Grafika/CSS/MP_Main.css" type="text/css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="LabelHlavicka" runat="server" meta:resourcekey="LabelHlavicka"></asp:Label>
    <asp:Label ID="LabelText" runat="server" meta:resourcekey="LabelText"></asp:Label>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="ScriptyJS" runat="server">
    <script src="Grafika/JS/jquery.js" type="text/javascript"></script>
    <script src="Grafika/JS/bootstrap.js" type="text/javascript"></script>
    <script src="Grafika/JS/MP_Main.js" type="text/javascript"></script>
</asp:Content>
