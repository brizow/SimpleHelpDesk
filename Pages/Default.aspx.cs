using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Configuration;
using System.Web.Security;

namespace SimpleHelpDesk
{
    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string user_name = string.Empty;
                try
                {
                    //Active Directory calls to get user information
                    if (System.Web.HttpContext.Current != null &&
                        System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        System.Web.Security.MembershipUser usr = Membership.GetUser();
                        if (usr != null)
                        {
                            user_name = usr.UserName;
                        }
                    }

                    //string user_name = System.Web.HttpContext.Current.User.Identity.Name;
                    //string user_name = Thread.CurrentPrincipal.Identity.Name;
                    string domain = user_name.Substring(0, user_name.IndexOf(@"\"));
                    string strWork = string.Empty;

                    PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, domain);
                    UserPrincipal user = UserPrincipal.FindByIdentity(domainContext, user_name);

                    // Get the user's email address and set the label
                    string email_address = string.Empty;
                    try
                    {
                        email_address = user.EmailAddress.ToString();
                        lblEmail.Text = email_address;
                    }
                    catch
                    {
                    }

                    //Get the users phone number and set the label
                    string phone_number = string.Empty;
                    try
                    {
                        phone_number = user.VoiceTelephoneNumber.ToString();
                        lblPhoneNumber.Text = phone_number;
                    }
                    catch
                    {
                    }

                    //Get the user's full name and set the label
                    string full_name = string.Empty;
                    try
                    {
                        full_name = user.Name.ToString();
                        lblName.Text = full_name;
                    }
                    catch
                    {
                    }

                    //Get the userid name and set the label
                    string userid = string.Empty;
                    try
                    {
                        userid = user_name;
                        lblWindowsLogin.Text = userid;
                    }
                    catch
                    {
                    }

                    //Get the user's OU
                    string user_OU = string.Empty;

                    DirectoryEntry directoryEntry = (user.GetUnderlyingObject() as DirectoryEntry);

                    //if the directoryEntry is not null
                    if (directoryEntry != null)
                    {
                        //Get the directory entry path and split it with the "," character
                        string[] directoryEntryPath = directoryEntry.Path.Split(',');

                        user_OU = directoryEntryPath[2];
                        user_OU = user_OU.Substring(user_OU.IndexOf("=") + 1);
                    }

                    try
                    {
                        //Get the users PC name
                        string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });

                        String ecn = System.Environment.MachineName;
                        lblComputerName.Text = computer_name[0].ToString();
                    }
                    catch (Exception)
                    {
                    }

                    //set the support group for routing tickets in the CA system to the appropriate team in North America or Europe
                    string routingDomain = domain.ToUpper();

                    switch (routingDomain)
                    {
                        case "LocalDomain":
                            lblSupportGroup.Text = "Local IT Support";
                            break;

                        case "Other Domain":
                            lblSupportGroup.Text = "OTHER SUPPORT NAME";
                            break;

                        default:
                            lblSupportGroup.Text = "Local IT Support";
                            break;
                    }

                    //This section controls what the users see.  If the user is in the USPHC show all values in the dropdown.  UKPHC users do not see everything.
                    if (routingDomain == "OTHER SUPPORT NAME")
                    {
                        //hide dropdown items on page load
                        ddlRequestType.Items.Remove(ddlRequestType.Items.FindByValue("Programming"));
                    }

                    //hide the New Employee update panel until it is needed
                    updPanelNewEmployees.Visible = false;
                    ddlRequestType.Focus();

                    //add the Submit button disable actions after the user clicks it
                    string var = ClientScript.GetPostBackEventReference(btnSubmit, "").ToString();
                    btnSubmit.Attributes.Add("onClick", "javascript :if ( Page_ClientValidate() ){this.disabled=true; this.value='Please Wait...';" + var + "};");
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

        }
        //set the drop down values
        protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ReqType = ddlRequestType.SelectedValue.ToString();

            switch (ReqType)
            {
                case "Select":
                    lblInformation.Text = "";
                    updPanelInformation.Update();
                    ddlRequestType.Focus();
                    updPanelNewEmployees.Visible = false;
                    updPanelNewEmployees.Update();
                    break;
                    //hide/show the panels of information
                case "PC/Printer/Hardware":
                    lblInformation.Text = "Please select this option for all issues with Laptop's, PC's, Printers, or Hardware.";
                    updPanelInformation.Update();
                    txbRequestDescription.Focus();
                    updPanelNewEmployees.Visible = false;
                    updPanelNewEmployees.Update();
                    break;
                case "Desktop OS/Software":
                    lblInformation.Text = "Please select this option for all issues with software like Windows, Email, Adobe, and Microsoft Office.";
                    updPanelInformation.Update();
                    txbRequestDescription.Focus();
                    updPanelNewEmployees.Visible = false;
                    updPanelNewEmployees.Update();
                    break;
                case "Security/Access":
                    lblInformation.Text = "Please select this option for all requests relating to security access (guest wifi, network folders, and local system access).";
                    updPanelInformation.Update();
                    txbRequestDescription.Focus();
                    updPanelNewEmployees.Visible = false;
                    updPanelNewEmployees.Update();
                    break;
                case "New/Terminated Employees":
                    lblInformation.Text = "Please select this option for processing New or Terminated Employees.";
                    updPanelInformation.Update();
                    updPanelNewEmployees.Visible = true;
                    rblUserAction.Focus();
                    updPanelNewEmployees.Update();
                    break;
                case "Programming":
                    lblInformation.Text = "Please select this option for all Programming requests or new projects.";
                    updPanelInformation.Update();
                    updPanelNewEmployees.Visible = false;
                    updPanelNewEmployees.Update();
                    break;
                case "Other":
                    lblInformation.Text = "Please select this option for any issues not covered in the Request Type dropdown.";
                    updPanelInformation.Update();
                    txbRequestDescription.Focus();
                    updPanelNewEmployees.Visible = false;
                    updPanelNewEmployees.Update();
                    break;
            }
        }
        //fire that click event
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int ErrorCondition = 0;
            string EmailBody = "";

            try
            {
                //build a new field that has all of the form data concatenated together.  This will all go into the body of the email and Help desk ticket
                string ReqType = ddlRequestType.SelectedValue.ToString();

                switch (ReqType)
                {
                    case "New/Terminated Employees":

                        // We perform a for loop to check if each checkbox is selected then we get the value 
                        string AccessList = "";
                        foreach (ListItem objItem in cblUserAccessList.Items)
                        {
                            if (objItem.Selected)
                            {
                                AccessList += objItem.Value + ", ";
                            }
                        }
                        //put the email body together
                        EmailBody = "\n\n"
                            + "Action: " + rblUserAction.SelectedValue.ToString() + "\n"
                            + "Access Requirements: " + AccessList + "\n"
                            + "Manager: " + txbEmployeeManager.Text.ToString() + "\n\n"
                            + "Description: " + txbRequestDescription.Text.ToString() + "\n\n";

                        break;
                    default:
                        EmailBody = "\n\n"
                            + "Description: \n" + txbRequestDescription.Text.ToString() + "\n\n";
                        break;
                }

                //Put it all together for the final email
                EmailBody = EmailBody
                           + "Request Type: " + ReqType + "\n"
                           + "Name: " + lblName.Text.ToString() + "\n"
                           + "Phone Number: " + lblPhoneNumber.Text.ToString() + "\n"
                           + "Email: " + lblEmail.Text.ToString() + "\n"
                           + "Userid: " + lblWindowsLogin.Text.ToString() + "\n"
                           + "PC Name: " + lblComputerName.Text.ToString();

                //emailIT string is found in web.config. Apply settings there.
                string emailIT = System.Configuration.ConfigurationManager.AppSettings["EmailIT"].ToString();
                string host = System.Configuration.ConfigurationManager.AppSettings["Host"].ToString();
                int port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Port"].ToString());

                SmtpClient ob = new SmtpClient();
                MailMessage obMsg = new MailMessage();

                //email addressing
                obMsg.To.Add(emailIT);
                //obMsg.To.Add(new MailAddress("test.email@yourprovider.com"));

                obMsg.From = new MailAddress(lblEmail.Text);
                obMsg.Subject = lblSupportGroup.Text;
                obMsg.Body = EmailBody;
                obMsg.IsBodyHtml = false;

                //check to make sure the attachment has been uploaded
                if (txbAttachment.HasFile)
                {
                    //If we have an attachment, upload it
                    try
                    {
                        string strFileName = System.IO.Path.GetFileName(txbAttachment.PostedFile.FileName);
                        Attachment attachFile = new Attachment(txbAttachment.PostedFile.InputStream, strFileName);

                        obMsg.Attachments.Add(attachFile);
                    }
                    //catch any errors and populate the error message
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                        ErrorCondition = 1;
                    }
                }

                //send the email to the MailAddress
                ob.Send(obMsg);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ErrorCondition = 1;
            }

            //All done tranfer user to the complete thank you page
            if (ErrorCondition == 0)
            {
                Response.Redirect("Complete.aspx");
            }
        }
    }
}
