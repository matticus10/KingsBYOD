<%@ Page Title="Device Registered" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeviceRegistered.aspx.cs" Inherits="Kings_BYOD_Helpdesk.fonts.DeviceRegistered" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2><%: Title %>.</h2>

     <div class="row">
        <div class="col-md-8">
            <div id="registered" runat="server">
                <p>Thankyou for registering your device. Please bring your device to the IT Department in the Mint Yard above the ICT classrooms.</p>
            </div>
            
        </div>
    </div>

</asp:Content>