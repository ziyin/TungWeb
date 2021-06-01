<%@ Page Title="" Language="C#" MasterPageFile="~/HeadRole.Master" AutoEventWireup="true" CodeBehind="Loggin.aspx.cs" Inherits="WebTung.Loggin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div id="logreg-forms">
            <h1 class="h3 mb-3 font-weight-normal" style="text-align: center">Sign in</h1>
            <asp:TextBox runat="server" ID="txtAccount" TextMode="SingleLine" CssClass="form-control" placeholder="Account" AUTOCOMPLETE="OFF"/>
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" placeholder="Password" AUTOCOMPLETE="OFF"/>
            <asp:Label Text="" runat="server" ID="lblError" ForeColor="red"/>
            <br />
            <asp:Button Text="Sign in" runat="server" CssClass="btn btn-success btn-block" ID="btnlogin" OnClick="btnlogin_Click" />
            <hr>
            <asp:Button Text="Sign up New Account" runat="server" CssClass="btn btn-primary btn-block" ID="btnresi" OnClick="btnresi_Click" />
            <br />
        </div>
    </section>
</asp:Content>
