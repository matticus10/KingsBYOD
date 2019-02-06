using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;

namespace Kings_BYOD_Helpdesk
{
    public partial class RegisterDevice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Deactivate button after sucessful click
            btnRegister.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnRegister, null) + ";");
            string schoolUserName = Session["userName"] as string;
            string schoolPassword = Session["password"] as string;
            loginForm.Visible = true;
            registered.Visible = false;

            //Force loguout if session variables are not found
            if (String.IsNullOrEmpty(schoolUserName))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("/Account/Login.aspx");
            }
            else if (String.IsNullOrEmpty(schoolPassword))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("/Account/Login.aspx");
            }
            else
            {
                SchoolUserName.Text = schoolUserName;
            }

            //Fill dropdown lists from Database Query
            if (!this.IsPostBack)
            {
                string SQLConnection = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(SQLConnection))
                {
                    //Get hardwareType for dropdown list
                    using (SqlCommand cmd = new SqlCommand("SELECT hardwareType FROM hardwareType ORDER BY CASE WHEN hardwareType = 'Other' THEN 1 ELSE 0 END, hardwareType"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        DeviceType.DataSource = cmd.ExecuteReader();
                        DeviceType.DataTextField = "hardwareType";
                        DeviceType.DataValueField = "hardwareType";
                        DeviceType.DataBind();
                        con.Close();
                    }

                    //Get OSType for dropdown list
                    using (SqlCommand cmd = new SqlCommand("SELECT OSType FROM OSType ORDER BY CASE WHEN OSType = 'Other' THEN 1 ELSE 0 END, OSType"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        OSType.DataSource = cmd.ExecuteReader();
                        OSType.DataTextField = "OSType";
                        OSType.DataValueField = "OSType";
                        OSType.DataBind();
                        con.Close();
                    }

                    //Get OSType for dropdown list
                    using (SqlCommand cmd = new SqlCommand("SELECT house FROM houses ORDER BY CASE WHEN house = 'N/A (Staff)' THEN 1 ELSE 0 END, house"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        House.DataSource = cmd.ExecuteReader();
                        House.DataTextField = "house";
                        House.DataValueField = "house";
                        House.DataBind();
                        con.Close();
                    }
                }
                //Set dropdown defaults
                DeviceType.Items.Insert(0, new ListItem("--Select Hardware Type--", "0"));
                OSType.Items.Insert(0, new ListItem("--Select OS Type--", "0"));
                House.Items.Insert(0, new ListItem("--Select House--", "0"));
            }

        }

        //Turn off device username validation if 'No Username' is checked
        protected void DeviceUsernameNA_CheckedChanged(object sender, EventArgs e)
        {
            if (DeviceUsernameNA.Checked)
            {
                DeviceUsernameValidate.Enabled = false;
            }
            else
            {
                DeviceUsernameValidate.Enabled = true;
            }
        }

        //Turn off device password validation if 'No Password' is checked
        protected void DevicePasswordNA_CheckedChanged(object sender, EventArgs e)
        {
            if (DevicePasswordNA.Checked)
            {
                DevicePasswordValidate.Enabled = false;
            }
            else
            {
                DevicePasswordValidate.Enabled = true;
            }
        }

        //IT Policy checkbox custom validation
        protected void CheckBoxRequired_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = MyCheckBox.Checked;
        }

        //If all requirements are met, upload data to the database
        protected void RegisterDevice_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Get form details as varialbles
                string deviceType = DeviceType.SelectedValue;
                string osType = OSType.SelectedValue;
                string deviceUsername;
                string devicePassword;
                string house = House.SelectedValue;
                string problemDetails = ProblemDetails.Text;
                bool ITPolicy;
                DateTime dateLogged = DateTime.Now;
                int systemStatus = 1;

                if (DeviceUsernameNA.Checked)
                {
                    deviceUsername = "N/A";
                }
                else
                {
                    deviceUsername = DeviceUsername.Text;
                }

                if (DevicePasswordNA.Checked)
                {
                    devicePassword = "N/A";
                }
                else
                {
                    devicePassword = DevicePassword.Text;
                }

                if (MyCheckBox.Checked)
                {
                    ITPolicy = true;
                }
                else 
                {
                    ITPolicy = false;
                }

                //Insert data into database

                string SQLConnection = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(SQLConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO deviceRegistration (schoolUsername, schoolPassword, deviceType, OSType, deviceUsername, devicePassword, house, problemDetails1, ITPolicyAgreement, dateLogged, ticketStatus) VALUES (@schoolUsername, @schoolPassword, @deviceType, @OSType, @deviceUsername, @devicePassword, @house, @problemDetails1, @ITPolicyAgreement, @dateLogged, @ticketStatus)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@schoolUsername", Session["userName"]);
                        cmd.Parameters.AddWithValue("@schoolPassword", Session["password"]);
                        cmd.Parameters.AddWithValue("@deviceType", deviceType);
                        cmd.Parameters.AddWithValue("@OSType", osType);
                        cmd.Parameters.AddWithValue("@deviceUsername", deviceUsername);
                        cmd.Parameters.AddWithValue("@devicePassword", devicePassword);
                        cmd.Parameters.AddWithValue("@house", house);
                        cmd.Parameters.AddWithValue("@problemDetails1", problemDetails);
                        cmd.Parameters.AddWithValue("@ITPolicyAgreement", ITPolicy);
                        cmd.Parameters.AddWithValue("@dateLogged", dateLogged);
                        cmd.Parameters.AddWithValue("@ticketStatus", systemStatus);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                string textDateLogged = dateLogged.ToString("MM'/'dd'/'yyyy HH':'mm':'ss.fff");
                SendEmail(GetID(textDateLogged));
   
                //Redirect once complete
                Response.Redirect("DeviceRegistered.aspx");
                loginForm.Visible = false;
                registered.Visible = true;
            }
        }
        protected string GetID(string dateLogged)
        {
            string error = "###";
            string ticketID;
            string SQLConnection = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(SQLConnection))
            {
                //Get ID of ticket
                using (SqlCommand cmd = new SqlCommand("SELECT ticketID FROM deviceRegistration WHERE (schoolUsername = '" + Session["userName"] + "') AND (dateLogged = '" + dateLogged + "')"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    var sqlQuery = cmd.ExecuteScalar();

                    if (sqlQuery != null)
                    {
                        ticketID = sqlQuery.ToString();
                        return ticketID;
                    };
                    con.Close();
                }
                return error;
            }
        }
        protected void SendEmail(string ticketID)
        {
            string schoolUserName = Session["userName"] as string;
            string schoolPassword = Session["password"] as string;
            string email = schoolUserName + "@kings-school.co.uk";
            string formatedDetails = ProblemDetails.Text.Replace("\r\n", "<br />");

            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential(email, schoolPassword);
            //smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;

            //Setting From , To and CC
            mail.From = new MailAddress(email);
            mail.To.Add(new MailAddress("helpdesk@kings-school.co.uk"));
            //mail.To.Add(new MailAddress("mnr@kings-school.co.uk"));
            mail.Subject = "BYOD Registration";
            mail.Body = "USERNAME: " + schoolUserName + "<br />HOUSE: " + House.SelectedValue + "<br />DEVICE TYPE: " + DeviceType.SelectedValue + "<br />OS TYPE: " + OSType.SelectedValue + "<br /><br />TICKET URL: http://kingsbyod/Ticket?ticketid=" + ticketID + "<br /><br />DETAILS OF PROBLEM:<br />" + formatedDetails;

            //attatchments
            if (FileUpload1.HasFiles)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                Attachment myAttachment = new Attachment(FileUpload1.FileContent, fileName);
                mail.Attachments.Add(myAttachment); 
            }

            if (FileUpload2.HasFiles)
            {
                string fileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
                Attachment myAttachment = new Attachment(FileUpload2.FileContent, fileName);
                mail.Attachments.Add(myAttachment);
            }

            if (FileUpload3.HasFiles)
            {
                string fileName = Path.GetFileName(FileUpload3.PostedFile.FileName);
                Attachment myAttachment = new Attachment(FileUpload3.FileContent, fileName);
                mail.Attachments.Add(myAttachment);
            }
            
            smtpClient.Send(mail);
        }
    } 
}
