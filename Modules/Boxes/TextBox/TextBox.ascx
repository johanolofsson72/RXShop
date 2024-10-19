<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TextBox.ascx.cs" Inherits="Modules_Boxes_TextBox_TextBox" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div id="TextBox_holder" runat="server">
    <div id="TextBox_admin" runat="server" visible="false">

        <script type="text/javascript">
            //<![CDATA[
            
            function showAdminTextBox(SitId,PagId,ModId,Var)
            {
                var oWnd = $find("<%=RadWindow1_TextBox.ClientID%>");
                var d = new Date();oWnd.SetUrl("http://<% Response.Write(Request.Url.Authority + Request.ApplicationPath); %>/Modules/Boxes/TextBox/TextBox_Admin.aspx?SitId=1&PagId=" + PagId + "&ModId=" + ModId + "&Var=" + Var + "&UID=" + d.getMilliseconds());
                oWnd.show();  
            }

        </script>

        <telerik:RadWindow
          ID ="RadWindow1_TextBox" 
          VisibleTitlebar="True" 
          VisibleStatusbar="false"
          ReloadOnShow="true"
          IconUrl="~/App_Themes/WebAdmin/Images/window_logo.gif"
         Behaviors="Maximize, Close, Move"
          Skin="Telerik"
          Modal="true"
          Height="600"
          Width="900" 
          Runat = "server">
        </telerik:RadWindow >

        <div id="adminbar" style="position:relative; float:left; height:35px;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background-color: #666666; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="img_AdminEdit" ToolTip="Edit" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_edit.gif" runat="server" /></td>
                    <td style="width:1px; font-size:1px;">&nbsp;</td>
                    <td style="background-color: #674343; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="img_AdminDelete" ToolTip="Delete" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_delete_red.gif" runat="server" /></td>
                    <td style="width:1px; font-size:1px;">&nbsp;</td>
                    <td style="background-color: #666666; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="img_AdminMoveUp" ToolTip="MoveUp"
                            ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_moveup.gif" runat="server" onclick="img_AdminMoveUp_Click" /></td>
                    <td style="width:3px; font-size:1px;"><asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_divider.gif" /></td>
                    <td style="background-color: #666666; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="img_AdminMoveDown" ToolTip="MoveDown" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_movedown.gif" runat="server" onclick="img_AdminMoveDown_Click" /></td>
                    <td style="width:1px; font-size:1px;">&nbsp;</td>
                    <td style="background-color: #666666; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="imbAdminVisible" runat="server" onclick="img_AdminVisible_Click" /></td>
                    <td style="width:1px; font-size:1px;">&nbsp;</td>
                    <td style="background-color: #666666; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="imbAdminSocial" onclick="img_AdminSocial_Click" runat="server" /></td>
                </tr>
                <tr>
                    <td style="font-size:1px; height:5px; text-align:center;"><asp:Image ID="Image0" runat="server" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_arrow_gray.gif" /></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
    <div id="TextBox">
        <telerik:RadAjaxPanel ID="RAP_LiteBox" runat="server">
            <div id="text_size" visible="false" runat="server" style="position: relative; float: left; padding-top: 10px; height: 15px; width: 100%; margin-bottom: 10px; border-bottom: solid 1px #E5E5E5; display:inline;">
                <div id="text_right" style="position: relative; float: right; text-align: right;">
                   <asp:ImageButton id="imbSmallText" runat="server" onclick="imbSmallText_Click" /><asp:ImageButton id="imbMediumText" runat="server" onclick="imbMediumText_Click" /><asp:ImageButton id="imbLargeText" runat="server" onclick="imbLargeText_Click" />
                   <%-- <asp:Literal ID="ltrFontResizer" runat="server" />--%>
                </div>
             </div>
            <div class="tb_main">
                <h2><asp:Label ID="lblHeader" runat="server" /></h2>
                <div id="IngressText" visible="false" runat="server"><p class="Ingress"><asp:Label ID="lblIngress" runat="server" /></p></div>
                <p><asp:Label ID="lblText" runat="server" /></p>
            </div>
            <%--<asp:Literal ID="ltrFontLoader" Visible="true" runat="server" />--%>
        </telerik:RadAjaxPanel>
    </div>
    <div id="TextBox_social" runat="server" visible="false" class="social_holder">        
        <div class="icons">
            <div class="social_left">
                <asp:Literal ID="ltrSocial" runat="server"></asp:Literal>
            </div>
            <div class="social_right">
                <asp:Literal ID="ltrSpecial" runat="server"></asp:Literal>
            </div>
       </div>
    </div>
</div>