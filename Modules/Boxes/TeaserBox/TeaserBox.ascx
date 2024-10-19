<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TeaserBox.ascx.cs" Inherits="Modules_Boxes_TeaserBox_TeaserBox" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div id="TeaserBox_holder" runat="server">
    <div id="TeaserBox_admin" runat="server" visible="false">
        <script type="text/javascript">
            //<![CDATA[
            
            function showAdminTeaserBox(SitId,PagId,ModId,Var)
            {
                var oWnd = $find("<%=RadWindow1_TeaserBox.ClientID%>");
                var d = new Date();
			    oWnd.SetUrl("http://<% Response.Write(Request.Url.Authority + Request.ApplicationPath); %>/Modules/Boxes/TeaserBox/TeaserBox_Admin.aspx?SitId=1&PagId=" + PagId + "&ModId=" + ModId + "&Var=" + Var + "&UID=" + d.getMilliseconds());
                oWnd.show();  
            }

        </script>
        
        <telerik:RadWindow
          ID ="RadWindow1_TeaserBox" 
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
    <div id="TeaserBox" class="TeaserBox" style="text-align:left;	font-size:1px; padding-bottom:20px;" runat="server">
        <script type="text/javascript">
            //<![CDATA[
            
            function showArticleBox(PagId,ModId,Var)
            {
                var oWnd = $find("<%=RadWindow2_TeaserBox.ClientID%>");
			    oWnd.SetUrl("http://<% Response.Write(Request.Url.Authority + Request.ApplicationPath); %>/Modules/Boxes/TeaserBox/TeaserBox_Article.aspx?SitId=1&PagId=" + PagId + "&ModId=" + ModId + "&Var=" + Var);
                oWnd.show();  
            }

        </script>
        <telerik:RadWindow
          ID ="RadWindow2_TeaserBox" 
          VisibleTitlebar="True" 
          VisibleStatusbar="false"
          ReloadOnShow="true"          
          Behaviors="Close, Move"
          Modal="true"
          Skin="Telerik"
          Height="260"
          Width="450" 
          AutoSize="true"
          Runat = "server">
        </telerik:RadWindow >
        
        <div id="tb_top">&nbsp;</div>
        <div id="tb_main">
            <asp:Literal ID="ltrMedia" runat="server"/>
            <h2><asp:Label ID="lblHeader" runat="server" /></h2>
            <p><asp:Label ID="lblText" runat="server" /></p>
            <div style="width:100%; text-align:right; padding-bottom: 10px;"><asp:Literal ID="ltrReadMore" runat="server" /></div>
        </div>
        <div id="tb_bottom">&nbsp;</div>
    </div>
</div>