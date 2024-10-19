<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SmallCart.ascx.cs" Inherits="Modules_Shop_SmallCart_SmallCart" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div id="SmallCart_holder" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        
        function showBigCart(SitId,PagId,ModId,Var)
        {
            var oWnd = $find("<%=RadWindow1_BigCart.ClientID%>");
            oWnd.SetUrl("http://<% Response.Write(Request.Url.Authority + Request.ApplicationPath); %>/Modules/Shop/BigCart/BigCart.aspx?SitId=1&PagId=" + PagId + "&ModId=" + ModId + "&Var=" + Var);
            oWnd.show();  
        }
        
        function showCheckout(SitId,PagId,ModId,Var)
        {
            var oWnd = $find("<%=RadWindow1_Checkout.ClientID%>");
            oWnd.SetUrl("http://<% Response.Write(Request.Url.Authority + Request.ApplicationPath); %>/Modules/Shop/Checkout/Checkout.aspx?SitId=1&PagId=" + PagId + "&ModId=" + ModId + "&Var=" + Var);
            oWnd.show();  
        }

    </script>

    <telerik:RadWindow ID="RadWindow1_BigCart" VisibleTitlebar="false" VisibleStatusbar="false"
        ReloadOnShow="true" Behaviors="Close, Move" Modal="true" Skin="Simple" Height="600"
        Width="896" runat="server">
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindow1_Checkout" VisibleTitlebar="false" VisibleStatusbar="false"
        ReloadOnShow="true" Behaviors="Close, Move" Skin="Telerik" Modal="true" Height="600"
        Width="900" runat="server">
    </telerik:RadWindow>
</div>

<div style="background-color: #FFFF00">
    <telerik:RadAjaxPanel ID="RadAjaxPanel_SmallCart" EnableAJAX="true" OnAjaxRequest="RadAjaxManager_AjaxRequest"
        runat="server">
        <telerik:RadScriptBlock ID="apa" runat="server">
            <script type="text/javascript">
                function XXX(id)
                {
                    //alert(id);
                    var t = document.getElementById(id);
                    t.value = "satan";
                    //setTimeout("CCC("+id+")", 5000);
                }
                function CCC(obj)
                {
                    document.getElementById(id).value = "satan";
                }
            </script>
        </telerik:RadScriptBlock> 
        <div id="SmallCartx" runat="server">
            <div style="float: left;">
                <asp:Label ID="lblCartText" runat="server" Text="lblCartText" />
            </div>
            <div style="float: left">
                <asp:Literal ID="ltrCartIcon" runat="server" Text="ltrCartIcon" />
            </div>
            <div style="float: left">
                <asp:ImageButton ID="imgBtnCart" runat="server" ImageUrl="~/Images/Modules/Shop/small_cart_open_cart_button.png" />
            </div>
            <div style="float: left">
                <asp:LinkButton ID="lnkBtnCheckout" runat="server" Text="lnkBtnCheckout" />
            </div>
        </div>
    </telerik:RadAjaxPanel>
</div>
