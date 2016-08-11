<%@ Page Title="IT Help Desk Request" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SimpleHelpDesk._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div>
            <h4>Welcome to the IT Help Desk request form.
            </h4>
            <p>
                Please enter all of the required information and click the Submit button. Your request will be routed to 
        Your Company's IT team.
            </p>
        </div>
        <div class="well-lg">
            * = Required Field
        <table class="table-condensed">
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Name:</b>
                </td>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Phone Number:</b>
                </td>
                <td>
                    <asp:Label ID="lblPhoneNumber" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Email:</b>
                </td>
                <td>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Windows Login:
                </b>
                </td>
                <td>
                    <asp:Label ID="lblWindowsLogin" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Computer Name:
                </b>
                </td>
                <td>
                    <asp:Label ID="lblComputerName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Support Group:
                </b>
                </td>
                <td>
                    <asp:Label ID="lblSupportGroup" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        <div class="well-lg">
            <table class="table-condensed">
                <tr>
                    <td>
                        <asp:Label ID="lblRequestType" runat="server" Text="Request Type:*" Style="font-weight: 700"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRequestType" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlRequestType_SelectedIndexChanged" TabIndex="1" CssClass="dropdown">
                            <asp:ListItem Selected="True" Value="Select">Please Select...</asp:ListItem>
                            <asp:ListItem Value="PC/Printer/Hardware">PC/Printer/Hardware</asp:ListItem>
                            <asp:ListItem Value="Desktop OS/Software">Desktop Software</asp:ListItem>
                            <asp:ListItem Value="Security/Access">Security/Access</asp:ListItem>
                            <asp:ListItem Value="New/Terminated Employees">New or Terminated Employees</asp:ListItem>
                            <asp:ListItem Value="Programming">Programming Changes</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRequestType" runat="server" ControlToValidate="ddlRequestType" InitialValue="Select" ErrorMessage="Please select a value."
                            Font-Bold="True" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:UpdatePanel ID="updPanelInformation" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <br />
                                <asp:Label ID="lblInformation" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <asp:UpdatePanel ID="updPanelNewEmployees" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="lblUserAction" runat="server" Text="Action:*"></asp:Label></b>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblUserAction" runat="server" TabIndex="2">
                                    <asp:ListItem Text="Add New Employee" Value="New Employee"></asp:ListItem>
                                    <asp:ListItem Text="Add New Temp Employee" Value="New Temp Employee"></asp:ListItem>
                                    <asp:ListItem Text="Terminate Existing Employee" Value="Terminate Employee"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvUserAction" runat="server" ErrorMessage="Please select the Action that IT needs to take."
                                    Font-Bold="True" ForeColor="Red" ControlToValidate="rblUserAction" SetFocusOnError="True"
                                    ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="lblUserAccessList" runat="server" Text="Access Requirements:*"></asp:Label></b>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="cblUserAccessList" runat="server" TabIndex="3">
                                    <asp:ListItem Text="Network Login" Value="Network Login"></asp:ListItem>
                                    <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
                                    <asp:ListItem Text="Operating System Issue" Value="Operating System Issue"></asp:ListItem>
                                    <asp:ListItem Text="Software needed" Value="Software needed"></asp:ListItem>
                                    <asp:ListItem Text="PC" Value="PC"></asp:ListItem>
                                    <asp:ListItem Text="Laptop" Value="Laptop"></asp:ListItem>
                                    <asp:ListItem Text="VPN access" Value="VPN access"></asp:ListItem>
                                </asp:CheckBoxList>
                                <asp:CustomValidator ID="cvUserAccessList" runat="server"
                                    ErrorMessage="Please select at least one option from the choices"
                                    ClientValidationFunction="ValidateUserAccessList" Font-Bold="True" ForeColor="Red"
                                    ValidationGroup="vgSubmit"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <b>
                                    <asp:Label ID="lblEmployeeManager" runat="server" Text="Employee's Manager:*"></asp:Label></b>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvEmployeeManager" runat="server" ErrorMessage="Please enter the employee's manager name."
                                    Font-Bold="True" ForeColor="Red" ControlToValidate="txbEmployeeManager" SetFocusOnError="True"
                                    ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                <br />
                                <asp:TextBox ID="txbEmployeeManager" runat="server" MaxLength="50" TabIndex="4"
                                    Width="194px" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <tr>
                    <td>
                        <asp:Label ID="lblRequestDescription" runat="server" Text="Request Description:*" Style="font-weight: 700"></asp:Label>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvRequestDescription" runat="server" ErrorMessage="Please enter the Request Description."
                            Font-Bold="True" ForeColor="Red" ControlToValidate="txbRequestDescription" SetFocusOnError="True"
                            ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txbRequestDescription" runat="server" TextMode="MultiLine" Height="100" Width="350" TabIndex="5" AutoCompleteType="Disabled" MaxLength="5000" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAttachment" runat="server" Text="Attachment:" Style="font-weight: 700"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="txbAttachment" runat="server" TabIndex="6" CssClass="" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <div style="margin-left: 165px">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="vgSubmit" TabIndex="7" CssClass="label-success" Height="39px" />
            </div>
        </div>
    </div>
</asp:Content>
