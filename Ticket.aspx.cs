using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kings_BYOD_Helpdesk
{
    public partial class Ticket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string schoolUserName = Session["userName"] as string;

            //Force loguout if session variables are not found
            if (String.IsNullOrEmpty(schoolUserName))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("/Account/Login.aspx");
            }
            else if (techLookup() != "1")
            {
                FormsAuthentication.SignOut();
                Response.Redirect("/Account/Login.aspx");
            }
            else
            {
                if (!this.IsPostBack)
                {
                    string ticketID = Request.QueryString["ticketid"]; //read phone from querystring.

                    string query;

                    query = "SELECT schoolUsername AS [School Username], schoolPassword AS [School Password], deviceUsername AS [Device Username], devicePassword AS [Device Password], problemDetails1 AS [Problem Details], house AS [House], dateLogged AS [Date Logged] FROM deviceRegistration WHERE ticketID = " + ticketID;

                    PlaceHolder1.Controls.Add(new Literal { Text = statusTable(query) });
                }
            }
        }
        //Row count
        private int recordCount(string query)
        {
            DataTable dt = this.GetData(query);

            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                i++;
            }
            return i;
        }
        //Create each table based on passed query
        private string statusTable(string query)
        {
            //Populating a DataTable from database.
            DataTable dt = this.GetData(query);

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table border = '1' cellpadding='5'>");
            html.Append("<col width='150'>");
            html.Append("<col width='130'>");
            html.Append("<col width='130'>");
            html.Append("<col width='130'>");
            html.Append("<col width='300'>");
            html.Append("<col width='130'>");
            html.Append("<col width='150'>");

            //Building the Header row.
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");

            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }

            //Table end.
            html.Append("</table>");

            return html.ToString();
        }
        private DataTable GetData(string query)
        {
            string SQLConnection = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(SQLConnection))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        private string techLookup()
        {
            string schoolUserName = Session["userName"] as string;
            string SQLConnection = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(SQLConnection))
            {
                //Get hardwareType for dropdown list
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM technician WHERE (technician LIKE '" + schoolUserName + "')"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    var sqlQuery = cmd.ExecuteScalar();

                    if (sqlQuery != null)
                    {
                        string count = sqlQuery.ToString();
                        return count;
                    };
                    con.Close();
                }
                return "error";
            }
        }
    }
}