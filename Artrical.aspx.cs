using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTung
{
    public partial class Artrical : System.Web.UI.Page
    {
        public string VideoURL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Login();
            Show_Artricle();
            More_Artricle();
        }

        #region 權限管理
        private void Login()
        {
            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();
            string title = Context.Request.QueryString["title"];

            string sqlstr = "select * FROM [Artricle] WHERE title='" + title + "'";
            OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows && dr.Read())
            {
                if (Convert.ToInt32(dr["login"].ToString()) == 1 && Request.Cookies["account"] == null)
                {
                    Response.Write("<script> alert('This article needs to log in to see the full article and play video') </script>");
                    btnPdf.Enabled = false;
                    videoControl.Attributes.Remove("controls");
                }
            }

            dr.Close();
            cmd.Cancel();
            cmd.Clone();
            Conn.Close();
        }
        #endregion

        #region 文章處理
        private void Show_Artricle()
        {
            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();

            string title = Context.Request.QueryString["title"];

            string sqlstr = "select * FROM [Artricle] WHERE title='" + title + "'";
            OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {

                lblTime.Text = dr["period"].ToString();
                lblTitle.Text = dr["title"].ToString();
                lblintroduct.Text = dr["introduct"].ToString();

                if (dr["imageUrl"].ToString() != "")
                {
                    imgImage.Visible = true;
                    imgImage.ImageUrl = "image/Article_Image/" + dr["imageUrl"].ToString() + ".jpg";
                }

                if (dr["videoUrl"].ToString() != "")
                {
                    divVideo.Visible = true;
                    VideoURL = "video/" + dr["videoUrl"].ToString() + ".mp4";
                }

            }
            dr.Close();
            cmd.Cancel();
            cmd.Clone();
            Conn.Close();

        }

        #endregion

        #region 下載PDF
        protected void btnPdf_Click(object sender, EventArgs e)
        {
            string path = Request.PhysicalApplicationPath + "\\pdf\\" + lblTitle.Text + "_pdf.pdf";
            Response.AppendHeader("content-disposition", "attachment; filename=" + Path.GetFileName(path));
            Response.WriteFile(path);
            Response.End();
        }
        #endregion

        #region 更多文章
        private void More_Artricle()
        {
            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();

            string sqlstr = "select * FROM [Artricle] WHERE title='" + lblTitle.Text + "'";
            OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
            OleDbDataReader dr = cmd.ExecuteReader();

            string kind = "";
            if (dr.Read())
                kind = dr["kind"].ToString();

            dr.Close();
            cmd.Cancel();
            cmd.Clone();

            sqlstr = "select top 6 * FROM [Artricle] WHERE kind='" + kind + "' order by period DESC";
            cmd = new OleDbCommand(sqlstr, Conn);
            dr = cmd.ExecuteReader();

            int count = 0;

            while (dr.Read() && count < 5)
            {
                if (dr["title"].ToString() != lblTitle.Text)
                {
                    TableRow row = new TableRow();
                    TableCell cell = new TableCell();

                    row = new TableRow();
                    cell = new TableCell();
                    Label label = new Label();
                    label.Text = dr["period"].ToString();
                    cell.Controls.Add(label);
                    row.Cells.Add(cell);
                    tbArticle.Rows.Add(row);

                    row = new TableRow();
                    cell = new TableCell();
                    LinkButton title = new LinkButton();
                    title.Text = dr["title"].ToString();
                    title.Click += new EventHandler(See_Artrical);
                    cell.Controls.Add(title);
                    row.Cells.Add(cell);
                    tbArticle.Rows.Add(row);

                    row = new TableRow();
                    cell = new TableCell();
                    row.Cells.Add(cell);
                    tbArticle.Rows.Add(row);
                    row = new TableRow();
                    cell = new TableCell();
                    row.Cells.Add(cell);
                    tbArticle.Rows.Add(row);
                    count++;
                }
            }


            dr.Close();
            cmd.Cancel();
            cmd.Clone();
            Conn.Close();
        }

        protected void See_Artrical(object sender, EventArgs e)
        {
            LinkButton Artrical = (LinkButton)sender;
            Response.Redirect("Artrical.aspx?title=" + Artrical.Text);
        }
        #endregion
    }
}