<%@ Page Title="PupilReport" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PupilReport.aspx.cs" Inherits="Kings_BYOD_Helpdesk.PupilReport" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
    </p>
    
    <asp:PlaceHolder ID = "PlaceHolder1" runat="server" />
    <asp:PlaceHolder ID = "PlaceHolder2" runat="server" />
    <asp:PlaceHolder ID = "PlaceHolder3" runat="server" />
    <asp:PlaceHolder ID = "PlaceHolder4" runat="server" />

</asp:Content>