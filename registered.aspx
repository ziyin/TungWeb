<%@ Page Title="" Language="C#" MasterPageFile="~/HeadRole.Master" AutoEventWireup="true" CodeBehind="registered.aspx.cs" Inherits="WebTung.registered" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div style="padding-top: 7%"></div>
        <table class="form-table">
            <tr>
                <td><i>
                    <asp:Label Text="User name：" runat="server" /></i>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtAccount" AUTOCOMPLETE="OFF" />
                </td>
            </tr>
            <tr>
                <td><i>
                    <asp:Label Text="Last name：" runat="server" /></i>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtLastName" AUTOCOMPLETE="OFF" />
                </td>
            </tr>
            <tr>
                <td><i>
                    <asp:Label Text="First name：" runat="server" /></i>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtFirstName" AUTOCOMPLETE="OFF" />
                </td>
            </tr>
            <tr>
                <td><i>
                    <asp:Label Text="Email：" runat="server" /></i>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEmail" AUTOCOMPLETE="OFF" />
                </td>
            </tr>
            <tr>
                <td><i>
                    <asp:Label Text="Confirm email：" runat="server" /></i>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtReEmail" AUTOCOMPLETE="OFF" />
                </td>
            </tr>
            <tr>
                <td><i>
                    <asp:Label Text="Password：" runat="server" /></i>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtPass" TextMode="Password" />
                </td>
            </tr>
            <tr>
                <td><i>
                    <asp:Label Text="Confirm password：" runat="server" /></i>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtRePass" TextMode="Password" />
                </td>
            </tr>
            <tr>
                <td><i>
                    <asp:Label Text="Code livre：" runat="server" /></i>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCodeLiver"></asp:TextBox>
                </td>
            </tr>    
        </table>
        <div class="Center_DIV">     
            <asp:CheckBox Text="Accepter les" runat="server" ID="ckPrivacy"/>
            <a runat="server" id="ReadPrivacy" onserverclick="ReadPrivacy_ServerClick">conditions d'utlisation</a>
            <br />
            <asp:TextBox runat="server" ReadOnly="true" Width="304px" Height="400px" TextMode="MultiLine" CssClass="Privacy_text" Text="" ID="txtPrivacy" Visible="false"/>
            <br />
         <asp:Button Text="Register" runat="server" ID="btnRegister" OnClick="btnRegister_Click" />
            <br /><br />
        <asp:Label Text="" runat="server" ForeColor="Red" ID="lblError" />
            </div>
    </section>
</asp:Content>
