using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTung
{
    public partial class Loggin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccount.Text))
            {
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
                    OleDbConnection Conn = new OleDbConnection(strConn);
                    Conn.Open();
                    string sqlstr = "select Pass, identity FROM [User] WHERE UserName = '" + txtAccount.Text + "'";
                    OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);

                    OleDbDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        if (dr["Pass"].ToString() == txtPassword.Text)
                        {
                            Response.Cookies["account"].Value = txtAccount.Text;
                            Response.Cookies["identity"].Value = dr["identity"].ToString(); ;
                            Conn.Close();
                            Response.Redirect(Request.Cookies["lasturl"].Value.ToString());
                        }
                        else
                        {
                            lblError.Text = "Password is error!";
                            txtPassword.Focus();
                        }

                    }
                    else
                        lblError.Text = "This account has not been registered!";

                    Conn.Close();
                }
                else
                {
                    lblError.Text = "Please enter password!";
                    txtPassword.Focus();
                }
            }
            else
            {
                lblError.Text = "Please enter account!";
                txtAccount.Focus();
            }
        }

        protected void btnresi_Click(object sender, EventArgs e)
        {
            Server.Transfer("registered.aspx");
        }

        protected void btnLoginFb_Click(object sender, EventArgs e)
        {

        }

        protected void btnLoginGmail_Click(object sender, EventArgs e)
        {

        }
    }
}