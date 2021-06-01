using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTung
{
    public partial class manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
                OleDbConnection Conn = new OleDbConnection(strConn);
                Conn.Open();
                string sqlstr = "select item FROM [BigKind] ORDER BY item";
                OleDbDataAdapter dr = new OleDbDataAdapter(sqlstr, Conn);
                DataSet ds = new DataSet();
                dr.Fill(ds, "kind");
                dropBigKind.DataValueField = "item";
                dropBigKind.DataTextField = "item";
                dropBigKind.DataSource = ds.Tables["kind"].DefaultView;
                dropBigKind.DataBind();
                Conn.Close();
            }
        }

        #region 顯示畫面
        protected void btnroller_Click(object sender, EventArgs e)
        {
            divRoller.Visible = true;
            divNewArtricle.Visible = divKind.Visible = false;
        }

        protected void btnNewArtricle_Click(object sender, EventArgs e)
        {
            init_new();
        }

        protected void btnKind_Click(object sender, EventArgs e)
        {
            lblError.Text = String.Empty;
            divKind.Visible = true;
            divNewArtricle.Visible = divRoller.Visible = divInsertKind .Visible=gvSmallKind.Visible=gvBigKind.Visible= false;
        }

        private void init_new()
        {
            ClearControlValue(new List<Control> { txtTitle, txtIntroduct, dropBigKind, radioLogin, txtimage, txtNewVideo, txtPDF, radioLogin });
            Bind_Kind();
            txtTitle.Enabled = true;
            divRoller.Visible = divKind.Visible = false;
            divNewArtricle.Visible = true;
            divFile.Visible = false;
        }

        private void ClearControlValue(List<Control> controls)  //清除多項控制項的值
        {
            foreach (Control control in controls)
            {
                if (control is TextBox)
                    ((TextBox)control).Text = string.Empty;
                else if (control is DropDownList)
                {
                    for (int i = 0; i < ((DropDownList)control).Items.Count; i++)
                    {
                        ((DropDownList)control).Items[i].Selected = false;
                    }
                    ((DropDownList)control).Items[0].Selected = true;
                }
                else if (control is RadioButtonList)
                {
                    for (int i = 0; i < ((RadioButtonList)control).Items.Count; i++)
                    {
                        ((RadioButtonList)control).Items[i].Selected = false;
                    }
                    ((RadioButtonList)control).Items[0].Selected = true;
                }
            }
        }

        #endregion

        #region 修改圖片
        protected void ChangePhoto_Click(object sender, EventArgs e)
        {
            Button choose = (Button)sender;
            int PhotoNo = Convert.ToInt32((choose.ID).Split('_')[1]);
            string photoname = "Carousel_" + PhotoNo;

            string path = Request.PhysicalApplicationPath + "\\image\\" + photoname + ".jpg";
            #region 刪除檔案
            if (File.Exists(path))
                File.Delete(path);

            #endregion

            switch (PhotoNo)
            {
                case 1:
                    fileUpload1.SaveAs(path);
                    break;
                case 2:
                    fileUpload2.SaveAs(path);
                    break;
                case 3:
                    fileUpload3.SaveAs(path);
                    break;
                case 4:
                    fileUpload4.SaveAs(path);
                    break;
                default:
                    break;
            }

            Response.Redirect("manager.aspx");

        }

        private void checkurl(int node, string photoname)
        {
            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();
            string sqlstr = "";
            if (node == 1 && !String.IsNullOrEmpty(txtImg1.Text))
            {
                sqlstr = "UPDATE [ScollImg] SET titleurl = '" + txtImg1.Text.Trim() + "'WHERE img = '" + photoname + "'";

                OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
                cmd.ExecuteNonQuery();
                cmd.Clone();
            }

            else if (node == 2 && !String.IsNullOrEmpty(txtImg2.Text))
            {
                sqlstr = "UPDATE [ScollImg] SET titleurl = '" + txtImg2.Text.Trim() + "'WHERE img = '" + photoname + "'";

                OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
                cmd.ExecuteNonQuery();
                cmd.Clone();
            }
            else if (node == 3 && !String.IsNullOrEmpty(txtImg3.Text))
            {
                sqlstr = "UPDATE [ScollImg] SET titleurl = '" + txtImg3.Text.Trim() + "'WHERE img = '" + photoname + "'";

                OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
                cmd.ExecuteNonQuery();
                cmd.Clone();
            }
            else if (node == 4 && !String.IsNullOrEmpty(txtImg4.Text))
            {
                sqlstr = "UPDATE [ScollImg] SET titleurl = '" + txtImg4.Text.Trim() + "'WHERE img = '" + photoname + "'";

                OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
                cmd.ExecuteNonQuery();
                cmd.Clone();
            }

            Conn.Close();
        }
        #endregion

        #region 上下步驟
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTitle.Text))
                txtTitle.Focus();
            else if (String.IsNullOrEmpty(txtIntroduct.Text))
                txtIntroduct.Focus();
            else
            {
                Control_Enable(new List<Control> { }, new List<Control> { txtTitle, txtIntroduct, dropBigKind, dropSmallKind, radioLogin });
                divFile.Visible = true;
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            Control_Enable(new List<Control> { txtIntroduct, dropBigKind, dropSmallKind, radioLogin }, new List<Control> { });
            divFile.Visible = false;
        }

        private void Control_Enable(List<Control> truelist, List<Control> falselist)
        {
            foreach (Control control in truelist)
            {
                if (control is TextBox)
                    ((TextBox)control).Enabled = true;
                else if (control is DropDownList)
                    ((DropDownList)control).Enabled = true;
                else if (control is RadioButtonList)
                {
                    for (int i = 0; i < ((RadioButtonList)control).Items.Count; i++)
                    {
                        ((RadioButtonList)control).Items[i].Enabled = true;
                    }
                }
            }

            foreach (Control control in falselist)
            {
                if (control is TextBox)
                    ((TextBox)control).Enabled = false;
                else if (control is DropDownList)
                    ((DropDownList)control).Enabled = false;
                else if (control is RadioButtonList)
                {
                    for (int i = 0; i < ((RadioButtonList)control).Items.Count; i++)
                    {
                        ((RadioButtonList)control).Items[i].Enabled = false;
                    }
                }
            }
        }
        #endregion

        #region 檔案處理
        protected void btnNewImg_Click(object sender, EventArgs e)
        {
            string photoname = txtTitle.Text + "_image";
            string path = Request.PhysicalApplicationPath + "\\image\\Article_Image\\" + photoname + ".jpg";
            #region 刪除檔案
            if (File.Exists(path))
                File.Delete(path);
            #endregion
            fileNewImg.SaveAs(path);

            txtimage.Text = photoname;
        }

        protected void btnNewVideo_Click(object sender, EventArgs e)
        {
            string videoname = txtTitle.Text + "_video";
            string path = Request.PhysicalApplicationPath + "\\video\\" + videoname + ".mp4";
            #region 刪除檔案
            if (File.Exists(path))
                File.Delete(path);
            #endregion
            fileNewVideo.SaveAs(path);

            txtNewVideo.Text = videoname;
        }

        protected void btnNewPDF_Click(object sender, EventArgs e)
        {
            string padname = txtTitle.Text + "_pdf";
            string path = Request.PhysicalApplicationPath + "\\pdf\\" + padname + ".pdf";
            #region 刪除檔案
            if (File.Exists(path))
                File.Delete(path);
            #endregion
            fileNewPDF.SaveAs(path);

            txtPDF.Text = padname;
        }
        #endregion

        #region 新增文章
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPDF.Text))
                txtPDF.Focus();
            else
            {
                string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
                OleDbConnection Conn = new OleDbConnection(strConn);
                Conn.Open();
                string sqlstr = "INSERT INTO [Artricle] (title, introduct, imageUrl, videoUrl, kind, period, login )VALUES(";
                sqlstr += "'" + txtTitle.Text + "', ";
                sqlstr += "'" + txtIntroduct.Text.Replace("'", "’") + "', ";
                sqlstr += "'" + txtimage.Text + "', ";
                sqlstr += "'" + txtNewVideo.Text + "', ";
                for (int i = 0; i < radioLogin.Items.Count; i++)
                {
                    if (dropSmallKind.Items[i].Selected)
                        sqlstr += "'" + dropSmallKind.Items[i].Text + "', ";
                }
                sqlstr += "'" + DateTime.Now.ToString("yyyy/MM/dd") + "', ";

                for (int i = 0; i < radioLogin.Items.Count; i++)
                {
                    if (radioLogin.Items[i].Selected)
                        sqlstr += Convert.ToInt32(radioLogin.Items[i].Value);
                }
                sqlstr += ")";
                OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
                cmd.ExecuteNonQuery();
                cmd.Clone();
                Conn.Close();

                Response.Write("<Script language='JavaScript'>alert('Sucess！');</Script>");
                init_new();
            }
        }

        #region 種類轉換
        protected void dropBigKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_Kind();
        }

        private void Bind_Kind()
        {
            String BigKind = dropBigKind.SelectedValue;

            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();

            string sqlstr = "select SmallItem FROM [SmallKind] WHERE BigItem = '" + BigKind + "' ORDER BY SmallItem";
            OleDbDataAdapter dr = new OleDbDataAdapter(sqlstr, Conn);
            DataSet ds = new DataSet();
            dr.Fill(ds, "kind");
            dropSmallKind.DataValueField = "SmallItem";
            dropSmallKind.DataTextField = "SmallItem";
            dropSmallKind.DataSource = ds.Tables["kind"].DefaultView;
            dropSmallKind.DataBind();
            Conn.Close();
        }

        #endregion
        #endregion

        #region 欲修改種類選擇
        protected void BigCategory_ServerClick(object sender, EventArgs e)
        {
            divInsertKind.Visible = true;
            divBigKind.Visible = true;
            divSmallKind.Visible = false;
            gvSmallKind.Visible = false;
            gvBigKind.Visible = true;
            Load_BigGridView();
        }

        private void Load_BigGridView()
        {
            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();
            string sqlstr = "select item FROM [BigKind] ORDER BY item";
            OleDbDataAdapter dr = new OleDbDataAdapter(sqlstr, Conn);
            DataTable dt = new DataTable("List");
            dr.Fill(dt);
            Conn.Close();

            dt.Columns["item"].ColumnName = "Big Category";

            showGridview(dt, gvBigKind);
        }

        protected void SmallCategory_ServerClick1(object sender, EventArgs e)
        {
            divInsertKind.Visible = true;
            divBigKind.Visible = false;
            divSmallKind.Visible = true;
            gvSmallKind.Visible = true;
            gvBigKind.Visible = false;
            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();
            string sqlstr = "select item FROM [BigKind] ORDER BY item";
            OleDbDataAdapter dr = new OleDbDataAdapter(sqlstr, Conn);
            DataSet ds = new DataSet();
            dr.Fill(ds, "kind");
            dropFamilyKind.DataValueField = "item";
            dropFamilyKind.DataTextField = "item";
            dropFamilyKind.DataSource = ds.Tables["kind"].DefaultView;
            dropFamilyKind.DataBind();
            Conn.Close();

            ClearControlValue(new List<Control> { dropFamilyKind });
            Load_SmallGridView();
        }

        protected void dropFamilyKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_SmallGridView();
        }

        private void Load_SmallGridView()
        {
            string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();
            string sqlstr = "select SmallItem FROM [SmallKind] WHERE BigItem = '"+dropFamilyKind.SelectedValue+"' ORDER BY SmallItem";
            OleDbDataAdapter dr = new OleDbDataAdapter(sqlstr, Conn);
            DataTable dt = new DataTable("List");
            dr.Fill(dt);
            Conn.Close();

            dt.Columns["SmallItem"].ColumnName = "Small Category";

            showGridview(dt, gvSmallKind);
        }
        #endregion

        #region 大類別GridView
        protected void gvBigKind_PageIndexChanged(object sender, EventArgs e)
        {
            gvBigKind.DataSource = ViewState["gvBigKind"];
            gvBigKind.DataBind();
        }

        protected void gvBigKind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBigKind.PageIndex = e.NewPageIndex;
        }

        protected void gvBigKind_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt16(e.CommandArgument);

                string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
                OleDbConnection Conn = new OleDbConnection(strConn);
                Conn.Open();

                string sqlstr = "DELETE FROM [BigKind] WHERE item ='" + gvBigKind.Rows[index].Cells[1].Text + "'";
                OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
                cmd.ExecuteNonQuery();
                cmd.Clone();
                Conn.Close();

                Load_BigGridView();
            }
        }

        #endregion

        #region 小類別GridView
        protected void gvSmallKind_PageIndexChanged(object sender, EventArgs e)
        {
            gvSmallKind.DataSource = ViewState["gvSmallKind"];
            gvSmallKind.DataBind();
        }

        protected void gvSmallKind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSmallKind.PageIndex = e.NewPageIndex;
        }

        protected void gvSmallKind_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt16(e.CommandArgument);

                string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
                OleDbConnection Conn = new OleDbConnection(strConn);
                Conn.Open();

                string sqlstr = "DELETE FROM [SmallKind] WHERE BigItem ='" + dropFamilyKind.SelectedValue + "' AND SmallItem = '"+ gvSmallKind.Rows[index].Cells[1].Text + "'";
                OleDbCommand cmd = new OleDbCommand(sqlstr, Conn);
                cmd.ExecuteNonQuery();
                cmd.Clone();
                Conn.Close();
                Load_SmallGridView();
            }
        }
        #endregion

        public void showGridview(DataTable dt, GridView gd)
        {
            ViewState[gd.ID] = dt;
            gd.DataSource = dt;
            gd.DataBind();
        }

        #region 新增種類

        protected void btnKindInsert_Click(object sender, EventArgs e)
        {
            if (fileCategoryImg.HasFile)
            {
                string strConn = ConfigurationManager.ConnectionStrings["TungConnectionString"].ConnectionString;
                OleDbConnection Conn = new OleDbConnection(strConn);
                Conn.Open();
                string sqlstr = "";
                OleDbCommand cmd = new OleDbCommand();
                if (divBigKind.Visible)
                {
                    if (!String.IsNullOrEmpty(txtNewBigKind.Text.Trim()))
                    {
                        sqlstr = "SELECT * FROM BigKind WHERE item = '" + txtNewBigKind.Text.Trim() + "'";
                        cmd = new OleDbCommand(sqlstr, Conn);
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            lblError.Text = "This category already exists.";
                        }
                        else
                        {
                            cmd.Cancel();
                            sqlstr = "INSERT INTO [BigKind] (item )VALUES( '" + txtNewBigKind.Text.Trim() + "')";
                            cmd = new OleDbCommand(sqlstr, Conn);
                            cmd.ExecuteNonQuery();

                            lblError.Text = txtNewBigKind.Text = String.Empty;
                        }
                        dr.Close();
                    }
                    else
                        lblError.Text = "Please input the category.";
                    cmd.Cancel();
                    Conn.Close();
                    Load_BigGridView();
                }
                else
                {
                    sqlstr = "SELECT * FROM SmallKind WHERE BigItem = '" + dropFamilyKind.SelectedValue + "' AND SmallItem ='" + txtNewSmallKind.Text.Trim() + "'";
                    cmd = new OleDbCommand(sqlstr, Conn);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (!String.IsNullOrEmpty(txtNewSmallKind.Text.Trim()))
                    {
                        if (dr.HasRows)
                        {
                            lblError.Text = "This category already exists.";
                        }
                        else
                        {                       
                            string filename = dropFamilyKind.SelectedValue + "_" + txtNewSmallKind.Text.Trim() + ".jpg";
                            string path = Request.PhysicalApplicationPath + "\\image\\Category_Image\\" + filename;
                            fileCategoryImg.SaveAs(path);

                            cmd.Cancel();
                            sqlstr = "INSERT INTO [SmallKind] ( BigItem, SmallItem, imgURL )VALUES( '" + dropFamilyKind.SelectedValue + "',  '" + txtNewSmallKind.Text.Trim() + "', '"+filename+ "')";
                            cmd = new OleDbCommand(sqlstr, Conn);
                            cmd.ExecuteNonQuery();

                            lblError.Text = txtNewSmallKind.Text = String.Empty;
                        }

                        dr.Close();
                    }
                    else
                        lblError.Text = "Please input the category.";
                    cmd.Cancel();
                    Conn.Close();
                    Load_SmallGridView();
                }
            }
            else
                lblError.Text = "Please upload the image.";
        }
        #endregion
    }
}