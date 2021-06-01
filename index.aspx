<%@ Page Title="" Language="C#" MasterPageFile="HeadRole.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebTung.index" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="Logo_Div">
            <img src="image/logo.bmp" alt="" />
        </div>
        <div class="Scoll_div">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:LinkButton Text="<" runat="server" ID="imgLast" OnClick="imgLast_Click"/>
                    &emsp;
                    <asp:ImageButton ImageUrl="image/Carousel_1.jpg" runat="server" CssClass="scoll_img" ID="imgScoll" OnClick="scoll_url"/>
                    &ensp;
                    <asp:LinkButton Text=">" runat="server" ID="imgNext" OnClick="imgNext_Click"/>
                    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick"></asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <div class="Kind_DIV" runat="server" id="divKind">
        </div>
        <div class="Article_Div">
            <asp:Table ID="tbArticle" runat="server" CssClass="ArticleTable"></asp:Table>
            <div class="Center_DIV" id="divPage" runat="server">
            </div>
        </div>
        <div class="Other_Div">
            <br />
            <div class="fb-page" data-href="https://www.facebook.com/Technique-dacupuncture-de-la-famille-Tung-112089350962242" data-tabs="timeline" data-width="300" data-height="" data-small-header="false" data-adapt-container-width="true" data-hide-cover="false" data-show-facepile="true">
                <blockquote cite="https://www.facebook.com/Technique-dacupuncture-de-la-famille-Tung-112089350962242" class="fb-xfbml-parse-ignore"><a href="https://www.facebook.com/Technique-dacupuncture-de-la-famille-Tung-112089350962242">Technique d&#039;acupuncture de la famille Tung</a></blockquote>
            </div>
        </div>
    </section>
    <div id="fb-root"></div>
    <script async defer crossorigin="anonymous" src="https://connect.facebook.net/zh_TW/sdk.js#xfbml=1&version=v10.0&appId=3960056657413038&autoLogAppEvents=1" nonce="xCn8vsMi"></script>
</asp:Content>
