<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BigCart.aspx.cs" Inherits="Modules_Shop_BigCart_BigCart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link href="../../../App_Themes/RXSK/modules.css" type="text/css" rel="stylesheet" id="stylesheet" />
        <title><asp:Literal ID="ltrlBigCartTitle" Text="ltrlBigCartTitle" runat="server"></asp:Literal></title>
        <style type="text/css">
            #BigCart
            {
                width: 800px;
            }
        </style>
        <script type="text/javascript">
            function GetRadWindow()
            {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }    
    
            function returnToParent()
            {
                var oArg = new Object();
                var oWnd = GetRadWindow();         
                var win = window.parent;
                win.location.reload(); 
                oWnd.close();
            }
        </script>
    </head>
    <body id="BigCart_body" style="margin: 0px 0px 0px 0px;">
        <form id="BigCart_form" runat="server">
            <div id="BigCart_header" style="margin: 10px 20px 10px 20px;">
                <asp:Label runat="server" ID="lblTitle" Text="lblTitle" Font-Bold="True" 
                    Font-Size="18px" ForeColor="White" Font-Names="Arial" />
                <table border="0" cellpadding="0" cellspacing="0" height="23px" align="right">
                    <tr>
                        <td id="BigCart_close_button_left"></td>
                        <td id="BigCart_close_button_main" width="39px" align="center">
                            <asp:LinkButton ID="lnkBtnCloseCart" runat="server" Text="lnkBtnCloseCart" 
                                onclick="lnkBtnCloseCart_Click" />
                        </td>
                        <td id="BigCart_close_button_right"></td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="BigCart_main">
                <asp:Repeater ID="rptrCartItems" runat="server" 
                    onitemcommand="rptrCartItems_ItemCommand">
                    <HeaderTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="880px">
                        <tr id="BigCartItems_header" height="21px">
                            <td style="padding-left: 20px;"><%# RXMali.GetXMLNode("Modules/BigCart/desc") %></td>
                            <td align="center"><%# RXMali.GetXMLNode("Modules/BigCart/quantity") %></td>
                            <td></td>
                            <td align="center" colspan="2"><%# RXMali.GetXMLNode("Modules/BigCart/price") %></td>
                            <td></td>
                            <td align="center" colspan="2"><%# RXMali.GetXMLNode("Modules/BigCart/totalprice") %></td>
                            <td></td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="BigCartItems_row">
                            <td width="335px" style="padding-left: 20px;">
                                <div style="float: left;">
                                    <asp:Image ID="imgProductPicture" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "PicturePath") %>' BorderWidth="1px" CssClass="ProductImage" ImageAlign="Middle" />
                                </div>
                                <div style="float: left;">
                                    <div id="ProductTitle"><%# DataBinder.Eval(Container.DataItem, "ProductName")%></div>
                                    <div id="ProductDesc"><%# DataBinder.Eval(Container.DataItem, "Description")%></div>
                                </div>
                            </td>
                            <td align="center" width="1*"><asp:TextBox ID="txtQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>' style="width: 30px; vertical-align: middle;" /></td>
                            <td width="100px"></td>
                            <td align="right" width="1*"><%# DataBinder.Eval(Container.DataItem, "Price") %>&nbsp;</td>
                            <td align="left" width="1*"><%# RXMali.GetXMLNode("Modules/BigCart/currencysymbol") %></td>
                            <td width="100px"></td>
                            <td align="right" width="1*"><%# DataBinder.Eval(Container.DataItem, "TotalPrice") %>&nbsp;</td>
                            <td align="left" width="1*"><%# RXMali.GetXMLNode("Modules/BigCart/currencysymbol") %></td>
                            <td align="right" style="padding-right: 20px;">
                                <table border="0" cellpadding="0" cellspacing="0" height="25px">
                                    <tr>
                                        <td id="BigCart_delete_button_left"></td>
                                        <td id="BigCart_delete_button_main">
                                            <asp:LinkButton ID="lnkBtnDeleteItem" runat="server" CommandName="Delete">
                                                <%# DataBinder.Eval(Container.DataItem, "DeleteText") %></asp:LinkButton>
                                        </td>
                                        <td id="BigCart_delete_button_right"></td>
                                    </tr>
                                </table>
                            </td>               
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr id="BigCartItems_footer">
                    <td valign="top" style="padding-top: 14px; padding-left: 18px;">
                        <table>
                            <tr>
                                <td style="padding-right: 8px;">
                                    <table border="0" cellpadding="0" cellspacing="0" height="25px">
                                        <tr>
                                            <td id="BigCart_update_button_left"></td>
                                            <td id="BigCart_update_button_main">
                                                <asp:LinkButton ID="lnkBtnUpdateCart" runat="server" CommandName="Update">
                                                    <%= RXMali.GetXMLNode("Modules/BigCart/update") %></asp:LinkButton>
                                            </td>
                                            <td id="BigCart_update_button_right"></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" height="25px">
                                        <tr>
                                            <td id="BigCart_empty_button_left"></td>
                                            <td id="BigCart_empty_button_main">
                                                <asp:LinkButton ID="lnkBtnEmptyCart" runat="server" CommandName="Empty">
                                                    <%= RXMali.GetXMLNode("Modules/BigCart/empty") %></asp:LinkButton>
                                            </td>
                                            <td id="BigCart_empty_button_right"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                    <td colspan="3" align="right" valign="top" style="padding-top: 20px">
                        <div id="TopTwoPricesLables">
                            <%= RXMali.GetXMLNode("Modules/BigCart/totalcartprice") %>:<br />
                            <%= RXMali.GetXMLNode("Modules/BigCart/shiping") %>:
                        </div>
                        <b><%= RXMali.GetXMLNode("Modules/BigCart/totalcartpricewithshiping") %>:</b><br />
                    </td>
                    <td colspan="3" align="right" valign="top" style="padding-top: 20px">
                        <div id="TopTwoPrices">
                            <asp:Label ID="lblTotalCartPrice" runat="server" Text="lblTotalCartPrice" /><br />
                            <asp:Label ID="lblShiping" runat="server" Text="lblShiping" />
                        </div>
                        <asp:Label ID="lblTotalWithShiping" runat="server" Text="lblTotalWithShiping" CssClass="TotalWithShiping" /><br />
                        <asp:Label ID="lblVatSymbol" runat="server" Text="lblVatSymbol"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                </table>
                <asp:Label ID="lblJSCloseWindow" runat="server" Text="lblJSCloseWindow" Visible="False" />
            </div>
        </form>
    </body>
</html>
