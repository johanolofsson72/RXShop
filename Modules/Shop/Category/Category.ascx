<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Category.ascx.cs" Inherits="Modules_Menus_Category_Category" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div id="Category_admin" style="position: relative; float: left; width: 100%;" runat="server" visible="false">
     <script type="text/javascript">
        //<![CDATA[
        
        function showAdminCategory(SitId,PagId,ModId)
        {
            var oWnd = $find("<%=RadWindow1_Category.ClientID%>");
            var d = new Date();oWnd.SetUrl("http://<% Response.Write(Request.Url.Authority + Request.ApplicationPath); %>/Modules/Shop/Category/Category_Admin.aspx?SitId=1&PagId=" + PagId + "&ModId=" + ModId + "&UID=" + d.getMilliseconds());
            oWnd.show();  
        }

    </script>

        <telerik:RadWindow
          ID ="RadWindow1_Category" 
          VisibleTitlebar="True" 
          VisibleStatusbar="false"
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
                <td style="background-color: #666666; text-align: center; height:25px; width:24px;">
                    <asp:ImageButton ID="img_AdminDelete" ToolTip="Delete" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_delete.gif" runat="server" /></td>
                <td style="width:1px; font-size:1px;">&nbsp;</td>
            </tr>
            <tr>
                <td style="font-size:1px; height:5px; text-align:center;"><asp:Image ID="Image0" runat="server" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_arrow_gray.gif" /></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
</div>
<div id="Category">
    <h2><asp:Literal ID="ltrTitle" runat="server" Text="ltrTitle" /></h2>
    <div style="position: relative; float: left; width:708px; margin-top: -1px;"><asp:Literal ID="ltrCategory" runat="server" /></div>
</div>
