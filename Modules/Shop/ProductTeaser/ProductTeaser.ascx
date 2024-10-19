<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductTeaser.ascx.cs" Inherits="Modules_Boxes_ProductTeaser_ProductTeaser" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div id="ProductTeaser_holder" runat="server">
    <div id="ProductTeaser" class="ProductTeaser" style="text-align:left;	font-size:1px; padding-bottom:20px;" runat="server">       
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