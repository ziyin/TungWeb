<%@ Page Title="" Language="C#" MasterPageFile="~/HeadRole.Master" AutoEventWireup="true" CodeBehind="Artrical.aspx.cs" Inherits="WebTung.Artrical" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="Content_Div">
            <asp:Label Text="" runat="server" ID="lblTime" CssClass="date_font" />
            <br />
            <asp:Label Text="" runat="server" ID="lblTitle" CssClass="title_font" />
            <hr class="sliderhr" />
            <asp:Image ImageUrl="" runat="server" Visible="false" ID="imgImage" CssClass="content_img" />
            <br />
            <br />
            <asp:Label Text="" runat="server" ID="lblintroduct" CssClass="content_font" />
            <div class="Center_DIV" runat="server" visible="false" id="divVideo">
                <hr class="sliderhr" />
                <asp:Label Text="Video：" runat="server"  CssClass="title_font" />
                <br /><br />
                <video controls="controls" runat="server" id="videoControl" style="width: 60%">
                    <source src="<%=VideoURL%>" type="video/mp4" />
                </video>
            </div>
        </div>
        <div class="Other_Div">
            <br />
            <br />
            <center>
            <asp:Button Text="DownLoad Full text" runat="server" ID="btnPdf" OnClick="btnPdf_Click"/>
                </center>
            <br />
            <asp:Label Text="More Articles" runat="server" ID="lblMore" />
            <asp:Table ID="tbArticle" runat="server" CssClass="tbArticle"></asp:Table>
        </div>
    </section>
</asp:Content>
