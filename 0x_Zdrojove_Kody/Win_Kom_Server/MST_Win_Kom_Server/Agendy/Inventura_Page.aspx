<%@ Page Title="" Language="C#" MasterPageFile="~/MP_Server.Master" AutoEventWireup="true" CodeBehind="Inventura_Page.aspx.cs" Inherits="Fask.MST_W_Server.Inventura_Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Grafika/CSS/bootstrap.css" type="text/css">
    <link rel="stylesheet" href="../Grafika/CSS/Inventura_Page.css" type="text/css">
    <link rel="stylesheet" href="../Grafika/CSS/MP_Server.css" type="text/css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="sidebar">

            <asp:TreeView ID="TreeMenu" runat="server" ImageSet="Arrows">
            <ParentNodeStyle />
            <HoverNodeStyle />
            <SelectedNodeStyle />
            <Nodes>
                <%--- Inventura1 --%>
                <asp:TreeNode Text="Inventura1" Value="Inventura1">
                    <asp:TreeNode Text="WebServices" NavigateUrl="~/Inventura1.asmx" Value="WebServices">
                    </asp:TreeNode>
                </asp:TreeNode>
            </Nodes>
            <RootNodeStyle/>
            <NodeStyle  />
        </asp:TreeView>
    </div>
    
    <div id="primary">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
    </div>


</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="ScriptyJS" runat="server">
    <script src="../Grafika/JS/jquery.js" type="text/javascript"></script>
    <script src="../Grafika/JS/bootstrap.js" type="text/javascript"></script>
    <script src="../Grafika/JS/MP_Server.js" type="text/javascript"></script>
</asp:Content>

