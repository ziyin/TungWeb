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
    public partial class registered : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
                OleDbConnection Conn = new OleDbConnection(strConn);
                Conn.Open();
                string sqlstr = "INSERT INTO [User] ([UserName], [Pass], [FirstName], [LastName], [Email], [identity], [CodeLiver] )VALUES(";
                sqlstr += "'" + txtAccount.Text.Trim() + "', ";
                sqlstr += "'" + txtPass.Text.Trim() + "', ";
                sqlstr += "'" + txtFirstName.Text.Trim() + "', ";
                sqlstr += "'" + txtLastName.Text.Trim() + "', ";
                sqlstr += "'" + txtEmail.Text.Trim() + "', ";
                sqlstr += 0 + ", ";
                txtCodeLiver.Text=txtCodeLiver.Text.Trim();
                if (String.IsNullOrEmpty(txtCodeLiver.Text))
                    txtCodeLiver.Text = " ";
                sqlstr += "'" + txtCodeLiver.Text + "'";
                sqlstr += ")";

                OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
                cmd.ExecuteNonQuery();
                cmd.Clone();
                Conn.Close();

                Response.Cookies["account"].Value = txtAccount.Text;
                Response.Cookies["identity"].Value = "0" ;
                Conn.Close();
                Response.Redirect("index.aspx");
            }
        }

        private bool Check()
        {
            if (String.IsNullOrEmpty(txtAccount.Text))
            {
                lblError.Text = "Please enter your user name.";
                txtAccount.Focus();
                return false;
            }
            else if (String.IsNullOrEmpty(txtLastName.Text))
            {
                lblError.Text = "Please enter your last name.";
                txtLastName.Focus();
                return false;
            }
            else if (String.IsNullOrEmpty(txtFirstName.Text))
            {
                lblError.Text = "Please enter your first name.";
                txtFirstName.Focus();
                return false;
            }
            else if (String.IsNullOrEmpty(txtEmail.Text))
            {
                lblError.Text = "Please enter your email.";
                txtEmail.Focus();
                return false;
            }
            else if (txtEmail.Text != txtReEmail.Text)
            {
                lblError.Text = "The two emails entered are not the same.";
                txtReEmail.Focus();
                return false;
            }
            else if (String.IsNullOrEmpty(txtPass.Text))
            {
                lblError.Text = "Please enter your password.";
                txtPass.Focus();
                return false;
            }
            else if (txtPass.Text != txtRePass.Text)
            {
                lblError.Text = "The two passwords entered are not the same.";
                txtRePass.Focus();
                return false;
            }
            else if (!ckPrivacy.Checked)
            {
                lblError.Text = "Please accepter les conditions d'utlisation.";
                ckPrivacy.Focus();
                return false;
            }
            else
            {
                string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
                OleDbConnection Conn = new OleDbConnection(strConn);
                Conn.Open();

                string sqlstr = "select * FROM [User] WHERE UserName='" + txtAccount.Text.Trim() + "'";
                OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    lblError.Text = "Account already exists.";
                    txtAccount.Focus();
                    return false;
                }
                dr.Close();
                cmd.Clone();
                cmd.Clone();
                Conn.Close();
            }
            return true;
        }

        protected void ReadPrivacy_ServerClick(object sender, EventArgs e)
        {
            if (txtPrivacy.Visible)
                txtPrivacy.Visible = false;
            else
                txtPrivacy.Visible = true;
        }
    }
}