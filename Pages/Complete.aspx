<%@ Page Title="Request submission Complete" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Complete.aspx.cs" Inherits="SimpleHelpDesk.Complete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
     <h2>
        Thank you for submitting this request!
    </h2>
    <p>
        If this is an URGENT issue please use the following contact information:
    </p>
    <p>
        For all issues that need immediate attention please call your IT Support Team directly.
    </p>
    <table>
        <tr>
            <td>
                &nbsp;
            </td>
           
        </tr>
        <tr>
            <td class="">
                <asp:Button ID="btnHome" runat="server" Text="Create another Help Request" 
                    Width="412px"  PostBackUrl="Default.aspx" CssClass="btn-default"/>
            </td>
           
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
          
        </tr>
         </table>
        </div>




</asp:Content>
