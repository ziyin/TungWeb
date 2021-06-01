using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebTung
{
    public partial class HeadRole : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "No-Cache");

            if (Request.Cookies["account"] != null)
            {
                this.LoginCal.Style.Add("display", "none");
                this.LogoutCal.Style.Add("display", "");

                if (Request.Cookies["identity"].Value.ToString() == "1")
                    this.ManageCal.Style.Add("display", "");
                else
                    this.ManageCal.Style.Add("display", "none");
            }
            else
            {
                this.LoginCal.Style.Add("display", "");
                this.ManageCal.Style.Add("display", "none");
                this.LogoutCal.Style.Add("display", "none");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Server.Transfer("registered.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["account"];
            if (cookie != null)
                Response.Cookies["account"].Expires = DateTime.Now.AddDays(-1);
            
            cookie = Request.Cookies["identity"];
            if (cookie != null)
                Response.Cookies["identity"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("index.aspx");
        }

        protected void LoginCal_ServerClick(object sender, EventArgs e)
        {
            Response.Cookies["lasturl"].Value = System.IO.Path.GetFileName(Request.PhysicalPath)+ Request.Url.Query;
            Response.Redirect("Loggin.aspx");
        }
    }
}