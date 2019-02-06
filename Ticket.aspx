<%@ Page Title="Ticket" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ticket.aspx.cs" Inherits="Kings_BYOD_Helpdesk.Ticket" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>

    <asp:PlaceHolder ID = "PlaceHolder1" runat="server" />
    <asp:PlaceHolder ID = "PlaceHolder2" runat="server" />
    <asp:PlaceHolder ID = "PlaceHolder3" runat="server" />
    <asp:PlaceHolder ID = "PlaceHolder4" runat="server" />

</asp:Content>