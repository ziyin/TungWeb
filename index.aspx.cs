using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;

namespace WebTung
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Timer1.Interval = 5000;
                Timer1.Enabled = true;
            }
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
        
        #region 文章處理
        private void Artrical_show()
        {
            tbArticle.Rows.Clear();

            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();
            string sqlstr = "select count(*) as total FROM [Artricle]";
            OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
            OleDbDataReader dr = cmd.ExecuteReader();

            int total_number = 0;
            if (dr.Read())
                total_number = Convert.ToInt32(dr["total"].ToString());

            dr.Close();
            cmd.Clone();

            sqlstr = "select * FROM [Artricle] ORDER by period DESC";
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
                Label space = new Label();
                space.Text = "&emsp;";
                divPage.Controls.Add(space);
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

            row = new TableRow();
            cell = new TableCell();
            LinkButton title = new LinkButton();
            title.Text = dr["title"].ToString();
            title.Click += new EventHandler(See_Artrical);
            cell.Controls.Add(title);
            cell.ColumnSpan = 3;
            row.Cells.Add(cell);
            tbArticle.Rows.Add(row);

            row = new TableRow();
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.CssClass = "Centertd";
            if (!String.IsNullOrEmpty(dr["imageUrl"].ToString()))
            {
                Image image = new Image();
                image.ImageUrl = "image/Article_Image/" + dr["imageUrl"].ToString() + ".jpg";
                cell.Controls.Add(image);
            }
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            tbArticle.Rows.Add(row);

            String content = dr["introduct"].ToString();

            row = new TableRow();
            cell = new TableCell();
            cell.Text = content;
            row.Cells.Add(cell);
            cell.ColumnSpan = 3;
            tbArticle.Rows.Add(row);

            row = new TableRow();
            cell = new TableCell();
            cell.Text = dr["period"].ToString();
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            tbArticle.Rows.Add(row);
        }
        #endregion
        
        protected void Cahnge_page(object sender, EventArgs e)
        {
            LinkButton page = (LinkButton)sender;
            Response.Redirect("index.aspx?page=" + page.Text);
        }

        protected void See_Artrical(object sender, EventArgs e)
        {
            LinkButton Artrical = (LinkButton)sender;
            Response.Redirect("Artrical.aspx?title=" + Artrical.Text);
        }

        #region 切換圖片
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            int node = Convert.ToInt32((imgScoll.ImageUrl.ToString().Split('_')[1]).Split('.')[0]);
            if (node == 4)
                node = 1;
            else
                node++;

            imgScoll.ImageUrl= "image/Carousel_" + node + ".jpg";
            imgScoll.Click +=scoll_url;
        }

        protected void scoll_url(object sender, ImageClickEventArgs e)
        {
            ImageButton resi = (ImageButton)sender;
            string img = (imgScoll.ImageUrl.ToString().Split('/')[1]).Split('.')[0];

            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();

            string sqlstr = "select titleurl FROM [ScollImg] where img = '"+img+"'";
            OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Redirect(dr["titleurl"].ToString());
            }

            dr.Close();
            cmd.Clone();
            Conn.Close();
        }
        #endregion

        protected void imgLast_Click(object sender, EventArgs e)
        {
            Timer1.Enabled = false;

            int node = Convert.ToInt32((imgScoll.ImageUrl.ToString().Split('_')[1]).Split('.')[0]);
            if (node == 1)
                node = 4;
            else
                node--;

            imgScoll.ImageUrl = "image/Carousel_" + node + ".jpg";
            imgScoll.Click += scoll_url;

            Timer1.Interval = 5000;
            Timer1.Enabled = true;
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            Timer1.Enabled = false;

            int node = Convert.ToInt32((imgScoll.ImageUrl.ToString().Split('_')[1]).Split('.')[0]);
            if (node == 4)
                node = 1;
            else
                node++;

            imgScoll.ImageUrl = "image/Carousel_" + node + ".jpg";
            imgScoll.Click += scoll_url;

            Timer1.Interval = 5000;
            Timer1.Enabled = true;
        }
    }
}