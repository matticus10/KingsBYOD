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
    public partial class PupilReport : System.Web.UI.Page
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
                    string query;

                    //query = "SELECT b.Expr1, a.schoolUsername, a.dateLogged FROM deviceRegistration AS a INNER JOIN (SELECT COUNT(*) AS Expr1, schoolUsername FROM deviceRegistration WHERE (schoolUsername = '"+ Session["pupilSearch"] +"') GROUP BY schoolUsername) AS b ON a.schoolUsername = b.schoolUsername";
                    query = "SELECT ticketID, schoolUsername, deviceType, OSType, dateLogged FROM deviceRegistration WHERE (schoolUsername LIKE '" + Session["pupilSearch"] + "%') ORDER BY dateLogged DESC";

                    if (Session["pupilSearch"] != null)
                    {
                        StringBuilder status = new StringBuilder();
                        status.Append("<h3>Total Tickets: " + recordCount(query) + "</h3>");
                        PlaceHolder1.Controls.Add(new Literal { Text = status.ToString() });
                        if (recordCount(query) > 0)
                        {
                            PlaceHolder1.Controls.Add(new Literal { Text = pupilSearchTable(query) });
                        }
                        Session["pupilSearch"] = null;
                    }
                }
            }
        }
        //Row count
        private int recordCount(string query)
        {
            DataTable dt = this.GetDataPS(query);

            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                i++;
            }
            return i;
        }
        //Create each table based on passed query
        private string pupilSearchTable(string query)
        {
            //Populating a DataTable from database.
            DataTable dt = this.GetDataPS(query);

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table border = '1' cellpadding='5'>");

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
                string ticketID = row["ticketID"].ToString();
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td><a href='Ticket?ticketid=" + ticketID + "'>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</a></td>");
                }
                html.Append("</tr>");
            }

            //Table end.
            html.Append("</table>");

            return html.ToString();
        }
        private DataTable GetDataPS(string query)
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["pupilSearch"] = TextBox1.Text;
            Response.Redirect(Request.RawUrl);
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