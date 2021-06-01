<%@ Page Title="" Language="C#" MasterPageFile="~/HeadRole.Master" AutoEventWireup="true" CodeBehind="manager.aspx.cs" Inherits="WebTung.manager" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="Center_DIV">
            <asp:Button Text="Edit roller photos" runat="server" ID="btnroller" CssClass="ManagerBtn" OnClick="btnroller_Click" />
            <asp:Button Text="New article" runat="server" CssClass="ManagerBtn" ID="btnNewArtricle" OnClick="btnNewArtricle_Click" />
            <asp:Button Text="Edit kind" runat="server" CssClass="ManagerBtn" ID="btnKind" OnClick="btnKind_Click" />
        </div>
        <hr />
        <div id="divRoller" runat="server" class="Center_DIV" visible="false">
            <asp:Table ID="tbRoller" runat="server" CssClass="RollerTable">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" CssClass="Centertd"><asp:label text="Photo 1" runat="server" /></asp:TableCell><asp:TableCell runat="server" CssClass="Centertd"><asp:label text="Photo 2" runat="server" /></asp:TableCell><asp:TableCell runat="server" CssClass="Centertd"><asp:label text="Photo 3" runat="server" /></asp:TableCell><asp:TableCell runat="server" CssClass="Centertd"><asp:label text="Photo 4" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" CssClass="Centertd"><asp:image imageurl="image/Carousel_1.jpg" runat="server" /></asp:TableCell>
                    <asp:TableCell runat="server" CssClass="Centertd"><asp:image imageurl="image/Carousel_2.jpg" runat="server" /></asp:TableCell>
                    <asp:TableCell runat="server" CssClass="Centertd"><asp:image imageurl="image/Carousel_3.jpg" runat="server" /></asp:TableCell>
                    <asp:TableCell runat="server" CssClass="Centertd"><asp:image imageurl="image/Carousel_4.jpg" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" CssClass="Centertd">
                        <button onclick="File_Div(1)" type="button">Change</button>
                    </asp:TableCell><asp:TableCell runat="server" CssClass="Centertd">
                        <button onclick="File_Div(2)" type="button">Change</button>
                    </asp:TableCell><asp:TableCell runat="server" CssClass="Centertd">
                        <button onclick="File_Div(3)" type="button">Change</button>
                    </asp:TableCell><asp:TableCell runat="server" CssClass="Centertd">
                        <button onclick="File_Div(4)" type="button">Change</button>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" CssClass="Centertd">
                        <div id="divFile_1" style="display: none; text-align: center">
                            <asp:FileUpload ID="fileUpload1" runat="server" accept="image/jpeg" />
                            <br />
                            <asp:Label Text="URL：" runat="server" /><asp:TextBox runat="server" ID="txtImg1" AUTOCOMPLETE="OFF" />
                            <br />
                            <asp:Button Text="Upload" runat="server" ID="btnPhoto_1" OnClick="ChangePhoto_Click" />
                        </div>
                    </asp:TableCell><asp:TableCell runat="server" CssClass="Centertd">
                        <div id="divFile_2" style="display: none;">
                            <asp:FileUpload ID="fileUpload2" runat="server" accept="image/jpeg" />
                            <br />
                            <asp:Label Text="URL：" runat="server" /><asp:TextBox runat="server" ID="txtImg2" AUTOCOMPLETE="OFF" />
                            <br />
                            <asp:Button Text="Upload" runat="server" ID="btnPhoto_2" OnClick="ChangePhoto_Click" />
                        </div>
                    </asp:TableCell><asp:TableCell runat="server" CssClass="Centertd">
                        <div id="divFile_3" style="display: none;">
                            <asp:FileUpload ID="fileUpload3" runat="server" accept="image/jpeg" />
                            <br />
                            <asp:Label Text="URL：" runat="server" /><asp:TextBox runat="server" ID="txtImg3" AUTOCOMPLETE="OFF" />
                            <br />
                            <asp:Button Text="Upload" runat="server" ID="btnPhoto_3" OnClick="ChangePhoto_Click" />
                        </div>
                    </asp:TableCell><asp:TableCell runat="server" CssClass="Centertd">
                        <div id="divFile_4" style="display: none;">
                            <asp:FileUpload ID="fileUpload4" runat="server" accept="image/jpeg" />
                            <br />
                            <asp:Label Text="URL：" runat="server" /><asp:TextBox runat="server" ID="txtImg4" AUTOCOMPLETE="OFF" />
                            <br />
                            <asp:Button Text="Upload" runat="server" ID="btnPhoto_4" OnClick="ChangePhoto_Click" />
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div id="divNewArtricle" runat="server" visible="false" style="padding-top: 10px;">
            <div id="divInfo" style="width: 50%; float: left;">
                <table class="form-table">
                    <tr>
                        <td><i>
                            <asp:Label Text="Title：" runat="server" />
                        </i>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTitle" TextMode="SingleLine" AUTOCOMPLETE="OFF" />
                        </td>
                    </tr>
                    <tr>
                        <td><i>
                            <asp:Label Text="Introduct：" runat="server" />
                        </i>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtIntroduct" Width="100%" TextMode="MultiLine" AUTOCOMPLETE="OFF" />
                        </td>
                    </tr>
                    <tr>
                        <td><i>
                            <asp:Label Text="Kind：" runat="server" />
                        </i>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="dropBigKind" OnSelectedIndexChanged="dropBigKind_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:DropDownList runat="server" ID="dropSmallKind">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td><i>
                            <asp:Label Text="Loggin to read：" runat="server" />
                        </i>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="radioLogin" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button Text="Next" runat="server" ID="btnNext" OnClick="btnNext_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divFile" runat="server" style="float: right; width: 50%">
                <table class="form-table">
                    <tr>
                        <td><i>
                            <asp:Label Text="Image：" runat="server" />
                        </i>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtimage" Width="50%" Enabled="false" />
                            <br />
                            <asp:FileUpload ID="fileNewImg" runat="server" accept="image/jpeg" />
                            <asp:Button Text="Upload" runat="server" ID="btnNewImg" OnClick="btnNewImg_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td><i>
                            <asp:Label Text="Video：" runat="server" />
                        </i>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNewVideo" Width="50%" Enabled="false" />
                            <br />
                            <asp:FileUpload ID="fileNewVideo" runat="server" accept="video/mp4" />
                            <asp:Button Text="Upload" runat="server" ID="btnNewVideo" OnClick="btnNewVideo_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td><i>
                            <asp:Label Text="PDF：" runat="server" />
                        </i>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPDF" Width="50%" Enabled="false" />
                            <br />
                            <asp:FileUpload ID="fileNewPDF" runat="server" accept="application/pdf" />
                            <asp:Button Text="Upload" runat="server" ID="btnNewPDF" OnClick="btnNewPDF_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button Text="Last" runat="server" ID="btnLast" OnClick="btnLast_Click" /></td>
                        <td>
                            <asp:Button Text="Insert" runat="server" ID="btnInsert" OnClick="btnInsert_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div runat="server" id="divKind" visible="false">
            <div class="KingPage_DIV">
                <a runat="server" onserverclick="BigCategory_ServerClick">Big category</a> &emsp; <a runat="server" onserverclick="SmallCategory_ServerClick1">Small category</a>
            </div>
            <div runat="server" id="divEditKind">
                <div style="width: 50%; float: left; text-align: center; padding-top: 5%;" id="divInsertKind" runat="server" visible="false">
                    <asp:Label Text="Insert" runat="server" CssClass="title_font" />
                    <br />
                    <br />
                    <div runat="server" id="divBigKind">
                        <asp:Label Text="Big category：" runat="server" />
                        <asp:TextBox runat="server" ID="txtNewBigKind" AUTOCOMPLETE="OFF" />
                    </div>
                    <div runat="server" id="divSmallKind">
                        <table class="form-table">
                            <tr>
                                <td><i>
                                    <asp:Label Text="Big category：" runat="server" />
                                </i>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList runat="server" ID="dropFamilyKind" OnSelectedIndexChanged="dropFamilyKind_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td><i>
                                    <asp:Label Text="Small category：" runat="server" />
                                </i>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox runat="server" ID="txtNewSmallKind" AUTOCOMPLETE="OFF" />
                                </td>
                            </tr>
                            <tr>
                                <td><i>
                                    <asp:Label Text="Category image：" runat="server" />
                                </i>
                                </td>
                                <td style="text-align: left">
                                    <asp:FileUpload ID="fileCategoryImg" runat="server" accept="image/jpeg" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <br />
                    <asp:Button Text="Insert" runat="server" ID="btnKindInsert" OnClick="btnKindInsert_Click" CssClass="ManagerBtn" />
                    <br />
                    <asp:Label Text="" runat="server" ForeColor="Red" ID="lblError" />
                </div>
                <div style="width: 50%; float: right; padding-top: 2%;">
                    <asp:GridView runat="server" ID="gvBigKind" PageSize="10" AllowPaging="true"
                        OnPageIndexChanged="gvBigKind_PageIndexChanged" OnPageIndexChanging="gvBigKind_PageIndexChanging" OnRowCommand="gvBigKind_RowCommand"
                        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                        <Columns>
                            <asp:ButtonField ControlStyle-CssClass="delete-btn" ButtonType="Button" CommandName="DeleteItem" Text="Delete" ItemStyle-Width="50px" />
                        </Columns>

                    </asp:GridView>

                    <asp:GridView runat="server" ID="gvSmallKind" OnPageIndexChanged="gvSmallKind_PageIndexChanged" OnPageIndexChanging="gvSmallKind_PageIndexChanging" OnRowCommand="gvSmallKind_RowCommand"
                        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                        <Columns>
                            <asp:ButtonField ControlStyle-CssClass="delete-btn" ButtonType="Button" CommandName="DeleteItem" Text="Delete" ItemStyle-Width="50px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </section>
    <script>
        function File_Div(photo_no) {
            document.getElementById("divFile_1").style.display = "none";
            document.getElementById("divFile_2").style.display = "none";
            document.getElementById("divFile_3").style.display = "none";
            document.getElementById("divFile_4").style.display = "none";
            switch (photo_no) {
                case 1:
                    document.getElementById("divFile_1").style.display = "";
                    break;
                case 2:
                    document.getElementById("divFile_2").style.display = "";
                    break;
                case 3:
                    document.getElementById("divFile_3").style.display = "";
                    break;
                case 4:
                    document.getElementById("divFile_4").style.display = "";
                    break;
            }
        }
    </script>
</asp:Content>
