<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Register.ascx.cs" Inherits="Modules_Newsletter_Register_Register" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div id="Register_holder" runat="server">
    <div id="Register_admin" runat="server" visible="false">

        <script type="text/javascript">
            //<![CDATA[
            
            function showAdminRegister(SitId,PagId,ModId,Var)
            {
                var oWnd = $find("<%=RadWindow1_Register.ClientID%>");
                var d = new Date();oWnd.SetUrl("http://<% Response.Write(Request.Url.Authority + Request.ApplicationPath); %>/Modules/Newsletter/Register/Register_Admin.aspx?SitId=1&PagId=" + PagId + "&ModId=" + ModId + "&Var=" + Var + "&UID=" + d.getMilliseconds());
                oWnd.show();  
            }

        </script>

        <telerik:RadWindow
          ID ="RadWindow1_Register" 
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
     <div id="Register">
        <div id="r_top">&nbsp;</div>
        <div id="r_main">
           <div id="r_reload" runat="server">
                <asp:Literal ID="ltrMedia" runat="server"/>
                <h2><asp:Label ID="lblHeader" runat="server" /></h2>
                <p><asp:Label ID="lblText" runat="server" /></p>
                <table cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="padding-bottom: 10px;"><asp:DropDownList ID="ddlMailGroup" CssClass="form_dropdown" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="padding-bottom: 10px;"><asp:TextBox ID="txtMail" CssClass="form_textbox" runat="server" /><br /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; padding-bottom: 10px;"><div id="r_button">
                            <asp:LinkButton ID="lbnSignUp" runat="server" onclick="lbnSignUp_Click" /></div></td>
                    </tr>
                    <tr>
                        <td><em class='success'><asp:Label ID="lblSuccess" Visible="false" runat="server" /></em><em class='error'><asp:Label ID="lblError" Visible="false" runat="server" /></em></td>
                    </tr>
                    <tr>
                        <td style="padding: 10px 0px 0px 0px; text-align:center"><asp:HyperLink ID="hplUnregister" CssClass="remove" runat="server" /><br /></td>
                    </tr>
                </table>
             </div>
        </div>
        <div id="r_bottom">&nbsp;</div>
    </div>
    
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="lbnSignUp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="lblError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
</div>


