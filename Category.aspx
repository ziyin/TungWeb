<%@ Page Title="" Language="C#" MasterPageFile="~/HeadRole.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="WebTung.Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div runat="server" id="divKind" class="KingPage_DIV">
        </div>
        <hr />
        <div>
            <asp:Table ID="tbCategory" runat="server" CssClass="Category-table">
            </asp:Table>
            <div class="Center_DIV" id="divPage" runat="server"></div>
        </div>
    </section>
</asp:Content>
