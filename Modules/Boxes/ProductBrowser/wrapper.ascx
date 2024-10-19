<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wrapper.ascx.cs" Inherits="Modules_Boxes_ProductBrowser_wrapper" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<telerik:RadAjaxPanel ID="RadAjaxPanel1" EnableAJAX="true" runat="server">
    <asp:ImageButton ID="imgBtnAddToCart" runat="server"  
        ImageUrl="~/Images/Modules/Boxes/productbrowser_add_to_cart.png" 
        onclick="imgBtnAddToCart_Click" />
</telerik:RadAjaxPanel>
    
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server" >
</telerik:RadAjaxManagerProxy>