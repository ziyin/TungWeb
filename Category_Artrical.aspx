<%@ Page Title="" Language="C#" MasterPageFile="~/HeadRole.Master" AutoEventWireup="true" CodeBehind="Category_Artrical.aspx.cs" Inherits="WebTung.Category_Artrical" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <section>
        <div runat="server" id="divKind" class="KingPage_DIV">
        </div>
        <hr />
        <div runat="server" id="divArticle">
            <asp:Table ID="tbArticle" runat="server" CssClass="KindTable"></asp:Table>
            <div class="Center_DIV" id="divPage" runat="server">
            </div>
        </div>
    </section>
</asp:Content>
