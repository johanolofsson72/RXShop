<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StartBox.ascx.cs" Inherits="Modules_Boxes_StartBox_StartBox" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


    <div id="StartBox_admin" runat="server" visible="false">
        <script type="text/javascript">
            //<![CDATA[
            
            function showAdminStartBox(SitId,PagId,ModId,Var)
            {
                var oWnd = $find("<%=RadWindow1_StartBox.ClientID%>");
                var d = new Date();oWnd.SetUrl("http://<% Response.Write(Request.Url.Authority + Request.ApplicationPath); %>/Modules/Boxes/StartBox/StartBox_Admin.aspx?SitId=1&PagId=" + PagId + "&ModId=" + ModId + "&Var=" + Var + "&UID=" + d.getMilliseconds());
                oWnd.show();  
            }

        </script>

        <telerik:RadWindow
          ID ="RadWindow1_StartBox" 
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
    <div id="StartBox_holder" runat="server">
       <div id="sb_top">&nbsp;</div>
       <div id="StartBox">    
            <div class="column">
                <asp:Literal ID="ltrMedia1" runat="server"/>
                <h2><asp:Label ID="lblHeader1" runat="server" /></h2>
                <p><asp:Label ID="lblText1" runat="server" /></p>
                <div id="sb_divider">&nbsp;</div>
                <div style="position: relative; float: left; width:100%; text-align:right; padding-top:10px;"><asp:HyperLink ID="hplReadMore1" runat="server" /></div>
            </div>
            <div class="column">
                <asp:Literal ID="ltrMedia2" runat="server"/>
                <h2><asp:Label ID="lblHeader2" runat="server" /></h2>
                <p><asp:Label ID="lblText2" runat="server" /></p>
                <div id="sb_divider">&nbsp;</div>
                <div style="position: relative; float: left; width:100%; text-align:right; padding-top:10px;"><asp:HyperLink ID="hplReadMore2" runat="server" /></div>
            </div>
            <div class="column">
                <asp:Literal ID="ltrMedia3" runat="server"/>
                <h2><asp:Label ID="lblHeader3" runat="server" /></h2>
                <p><asp:Label ID="lblText3" runat="server" /></p>
                <div id="sb_divider">&nbsp;</div>
                <div style="position: relative; float: left; width:100%; text-align:right; padding-top:10px;"><asp:HyperLink ID="hplReadMore3" runat="server" /></div>
            </div>
            <div class="column">
                <asp:Literal ID="ltrMedia4" runat="server"/>
                <h2><asp:Label ID="lblHeader4" runat="server" /></h2>
                <p><asp:Label ID="lblText4" runat="server" /></p>
                <div id="sb_divider">&nbsp;</div>
                <div style="position: relative; float: left; width:100%; text-align:right; padding-top:10px;"><asp:HyperLink ID="hplReadMore4" runat="server" /></div>
            </div>
       </div>
       <div id="sb_bottom">&nbsp;</div>  
   </div>