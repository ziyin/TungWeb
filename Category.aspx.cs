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
    public partial class Category : System.Web.UI.Page
    {

        String BigKind = "";
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        protected void Page_Load(object sender, EventArgs e)
        {
                Kind_show();
                Sub_Kind();
        }

        #region 大種類選擇
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

        #region 子種類呈現

        private void Sub_Kind()
        {
            tbCategory.Rows.Clear();

            BigKind = Context.Request.QueryString["BigKind"];

            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();
            string sqlstr = "select count(*) as total FROM [SmallKind] WHERE BigItem = '" + BigKind + "'";
            OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
            OleDbDataReader dr = cmd.ExecuteReader();

            int total_number = 0;
            if (dr.Read())
                total_number = Convert.ToInt32(dr["total"].ToString());

            dr.Close();
            cmd.Clone();

            sqlstr = "select * FROM [SmallKind]  WHERE BigItem = '" + BigKind + "' ORDER by SmallItem";
            cmd = new OleDbCommand(sqlstr, Conn);
            dr = cmd.ExecuteReader();

            int count = 0;

            int rowcont = 0;
            if (Context.Request.QueryString["page"] != null)
            {

                rowcont = (Convert.ToInt32(Context.Request.QueryString["page"]) - 1) * 9;
                for (int i = 0; i < rowcont; i++)
                {
                    dr.Read();
                }
                while (dr.Read() && count < 9)
                {
                    Create_Sub(dr, count);
                    count++;
                }
            }
            else
            {
                while (dr.Read() && count < 9)
                {
                    Create_Sub(dr, count);
                    count++;
                }
            }
            if ((total_number - rowcont) % 9 != 0)
                tbCategory.Rows.Add(row);

            int page = 0;
            if (Convert.ToInt32(total_number % 9) != 0)
                page = Convert.ToInt32(total_number / 9) + 1;
            else
                page = Convert.ToInt32(total_number / 9);

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

        protected void Cahnge_page(object sender, EventArgs e)
        {
            LinkButton page = (LinkButton)sender;
            Response.Redirect("Category.aspx?BigKind=" + BigKind + "&page=" + page.Text); ;
        }

        #endregion

        #region 子種類產生
        private void Create_Sub(OleDbDataReader dr, int count)
        {
            if (count % 3 == 0)
                row = new TableRow();

            cell = new TableCell();

            String d = dr["imgURL"].ToString();
            ImageButton img = new ImageButton();
            img.ImageUrl = "image/Category_Image/"+ dr["imgURL"].ToString();
            img.ID= dr["SmallItem"].ToString();
            img.Click += new ImageClickEventHandler(See_Sub);
            img.CssClass = "KindImg";
            cell.Controls.Add(img);

            row.Cells.Add(cell);

            if (count % 3 == 2 )
                tbCategory.Rows.Add(row);

        }
        #endregion

        protected void See_Sub(object sender, ImageClickEventArgs e)
        {
            ImageButton Sub = (ImageButton)sender;
            Response.Redirect("Category_Artrical.aspx?SmallKind=" + Sub.ID);
        }
    }
}