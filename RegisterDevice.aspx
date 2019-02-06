<%@ Page Title="Register Device" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterDevice.aspx.cs" Inherits="Kings_BYOD_Helpdesk.RegisterDevice" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2><%: Title %>.</h2>

     <div class="row">
        <div class="col-md-8">
            <div id="registered" runat="server">
                <p>Thankyou for registering your device. Please bring your device to the IT Department in the Mint Yard above the ICT classrooms.</p>
            </div>
            <section id="loginForm" runat="server">
                <div class="form-horizontal">
                    <h4>Please provide us with the following details about your device.</h4>
                    <hr />
                      <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="SchoolUsername" CssClass="col-md-3 control-label">School Username</asp:Label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="SchoolUserName" ReadOnly=true CssClass="form-control" />
                            <br />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="SchoolPassword" CssClass="col-md-3 control-label">School Password</asp:Label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="SchoolPassword" ReadOnly=true CssClass="form-control" >••••••••••</asp:TextBox>
                            <br />
                    </div>
                    </div>
                        <!--Dropdown items generated from SQL hardwareType.dbo-->
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="DeviceType" CssClass="col-md-3 control-label">Device Type</asp:Label>
                        <div class="col-md-9">
                            <asp:DropDownList ID = "DeviceType" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DeviceType"
                                CssClass="text-danger" InitialValue="0" ErrorMessage="Please specify device type." />
                        </div>
                    </div>
                        <!--Dropdown items generated from SQL OSType.dbo-->
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="OSType" CssClass="col-md-3 control-label">OS Type</asp:Label>
                        <div class="col-md-9">
                            <asp:DropDownList ID = "OSType" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="OSType"
                                CssClass="text-danger" InitialValue="0" ErrorMessage="Please specify OS type." />
                        </div>
                    </div>
                        <!--Validated by RegistrationValidation.js-->
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="DeviceUsername" CssClass="col-md-3 control-label">Device Username</asp:Label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="DeviceUsername" CssClass="form-control" />
                            <asp:Label runat="server" class="checkbox-inline">
                            <asp:CheckBox ID="DeviceUsernameNA" runat="server"  Text="No Username" OnCheckedChanged="DeviceUsernameNA_CheckedChanged" AutoPostBack=true Checked="false" />
                                </asp:Label><br />
                            <asp:RequiredFieldValidator ID="DeviceUsernameValidate" runat="server" ControlToValidate="DeviceUsername"
                                CssClass="text-danger" ErrorMessage="Please enter device username or tick the 'No Username' box" />
                        </div>
                    </div>
                        <!--Validated by RegistrationValidation.js-->
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="DevicePassword" CssClass="col-md-3 control-label">Device Password</asp:Label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="DevicePassword" TextMode="Password" CssClass="form-control" />
                            <asp:Label runat="server" class="checkbox-inline">
                            <asp:CheckBox ID="DevicePasswordNA" runat="server" Text="No Password" OnCheckedChanged="DevicePasswordNA_CheckedChanged" AutoPostBack=true Checked="false" />
                                </asp:Label><br />
                            <asp:RequiredFieldValidator ID="DevicePasswordValidate" runat="server" ControlToValidate="DevicePassword" 
                                CssClass="text-danger" ErrorMessage="Please enter device password or tick the 'No Password' box" />
                        </div>
                    </div>
                        <!--Dropdown items generated from SQL houses.dbo-->
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="House" CssClass="col-md-3 control-label">House</asp:Label>
                        <div class="col-md-9">
                            <asp:DropDownList ID = "House" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="House"
                                CssClass="text-danger" InitialValue="0" ErrorMessage="Please specify your house." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ProblemDetails" CssClass="col-md-3 control-label">Details of Problem</asp:Label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="ProblemDetails" TextMode="multiline" rows="10" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ProblemDetails"
                                CssClass="text-danger" ErrorMessage="Please specify the details of the problem." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="FileUpload1" CssClass="col-md-3 control-label">Screenshots/Pictures</asp:Label>
                        <div class="col-md-9">
                            <p><b>(Maximum of 3 images)</b></p><asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"/>
                            <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control"/>
                            <asp:FileUpload ID="FileUpload3" runat="server" CssClass="form-control"/>
                        </div>
                    </div>
                    <!--Custom validation for IT Policy-->
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="MyCheckBox" CssClass="col-md-3 control-label">IT Policy Agreement</asp:Label>
                        <div class="col-md-9">
                            <asp:Label runat="server" class="checkbox-inline">
                                <asp:CheckBox runat="server" ID="MyCheckBox" CssClass="nowrap_list" text="I confirm that I have read and agree to the terms of the school IT policy document"/>
                            </asp:Label><br />
                                <span>The King's School IT Policy document can be found <a href="http://vle.kings-school.co.uk/itsupport/pupil-acceptable-use-of-it-policy" target="_blank">here</a></span><br />
                            <asp:CustomValidator runat="server" ID="CheckBoxRequired" OnServerValidate="CheckBoxRequired_ServerValidate"
                                ClientValidationFunction="CheckBoxRequired_ServerValidate" CssClass="text-danger">You must tick this box to proceed.</asp:CustomValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-9">
                            <asp:Button runat="server" id="btnRegister" Text="Register Device" CssClass="btn btn-default" OnClick="RegisterDevice_Click" />
                        </div>
                    </div>
                </div>
            </section>
        </div>


    </div>

</asp:Content>