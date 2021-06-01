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
    public partial class Category_Artrical : System.Web.UI.Page
    {
        String SmallKind = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Kind_show();
            Artrical_show();
        }

        #region 種類處理
        private void Kind_show()
        {
            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();

            string sqlstr = "select item FROM [BigKind] ORDER BY item";
            OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LinkButton kind = new LinkButton();
                kind.Text = dr["item"].ToString();
                kind.Click += new EventHandler(Kind_page);
                kind.CssClass = "circle_a";
                divKind.Controls.Add(kind);
            }
            dr.Close();
            cmd.Clone();
            Conn.Close();
        }

        protected void Kind_page(object sender, EventArgs e)
        {
            LinkButton kind = (LinkButton)sender;
            Response.Redirect("Category.aspx?BigKind=" + kind.Text);
        }
        #endregion

        #region 文章篩選
        private void Artrical_show()
        {
            tbArticle.Rows.Clear();

            SmallKind = Context.Request.QueryString["SmallKind"];

            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();
            string sqlstr = "select count(*) as total FROM [Artricle] WHERE kind = '" + SmallKind + "'";
            OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
            OleDbDataReader dr = cmd.ExecuteReader();

            int total_number = 0;
            if (dr.Read())
                total_number = Convert.ToInt32(dr["total"].ToString());

            dr.Close();
            cmd.Clone();

            sqlstr = "select * FROM [Artricle]  WHERE kind = '" + SmallKind + "' ORDER by period DESC";
            cmd = new OleDbCommand(sqlstr, Conn);
            dr = cmd.ExecuteReader();

            int count = 0;

            if (Context.Request.QueryString["page"] != null)
            {
                int rowcont = (Convert.ToInt32(Context.Request.QueryString["page"]) - 1) * 5;
                for (int i = 0; i < rowcont; i++)
                {
                    dr.Read();
                }
                while (dr.Read() && count < 5)
                {
                    Create_Article(dr);
                    count++;
                }
            }
            else
            {
                while (dr.Read() && count < 5)
                {
                    Create_Article(dr);
                    count++;
                }
            }
            int page = 0;
            if (Convert.ToInt32(total_number % 5) != 0)
                page = Convert.ToInt32(total_number / 5) + 1;
            else
                page = Convert.ToInt32(total_number / 5);

            for (int i = 0; i < page; i++)
            {

                LinkButton page_no = new LinkButton();
                page_no.Text = (i + 1).ToString();
                page_no.Click += new EventHandler(Cahnge_page);
                divPage.Controls.Add(page_no);
                Label label = new Label();
                label.Text = "&emsp;";
                divPage.Controls.Add(label);
            }
            Conn.Close();

        }

        #endregion

        #region 文章產生
        private void Create_Article(OleDbDataReader dr)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            row = new TableRow();
            cell = new TableCell();

            if (!string.IsNullOrEmpty(dr["imageUrl"].ToString()))
            {
                Image image = new Image();
                image.ImageUrl = "image/Article_Image/" + dr["imageUrl"].ToString() + ".jpg";
                cell.Controls.Add(image);
            }
            row.Cells.Add(cell);
            cell = new TableCell();
            Label label = new Label();
            label.Text = dr["period"].ToString() + "</br>";
            cell.Controls.Add(label);
            LinkButton title = new LinkButton();
            title.Text = dr["title"].ToString();
            title.Click += new EventHandler(See_Artrical);
            cell.Controls.Add(title);
            Label label1 = new Label();
            label1.Text = "<br/>" + dr["introduct"].ToString();
            cell.Controls.Add(label1);
            row.Cells.Add(cell);
            tbArticle.Rows.Add(row);

            row = new TableRow();
            cell = new TableCell();
            row.Cells.Add(cell);
            tbArticle.Rows.Add(row);

        }
        #endregion

        protected void Cahnge_page(object sender, EventArgs e)
        {
            LinkButton page = (LinkButton)sender;
            Response.Redirect("Category_Artrical.aspx?SmallKind=" + SmallKind + "&page=" + page.Text); ;
        }
        protected void See_Artrical(object sender, EventArgs e)
        {
            LinkButton Artrical = (LinkButton)sender;
            Response.Redirect("Artrical.aspx?title=" + Artrical.Text);
        }

    }
}