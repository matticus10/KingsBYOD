<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Kings_BYOD_Helpdesk._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>The King's School BYOD Helpdesk</h1>
        <p class="lead">Please use this site to register your device BEFORE taking it to the IT Department.</p>
        <p><a href="RegisterDevice" class="btn btn-primary btn-large">Get Started &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Step 1</h2>
            <p>
                Register device online giving a brief description of the problems you are experiencing. Once complete you can bring the device to the IT department.
            </p>
            <p>
                <a class="btn btn-default" href="RegisterDevice">Register Device &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Step 2</h2>
            <p>
                Bring the device to the IT Department in the Mint Yard above the ICT classrooms.
            </p>
        </div>
        <div class="col-md-4">
            <h2>Step 3</h2>
            <p>
                When the work is complete you will receive an email alerting you that your device is ready to collect from the IT Department.
            </p>
        </div>
    </div>

</asp:Content>
