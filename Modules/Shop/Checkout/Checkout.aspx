<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Modules_Shop_Checkout_Checkout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link href="../../../App_Themes/RXSK/modules.css" type="text/css" rel="stylesheet" id="stylesheet" />
        <title><asp:Literal ID="ltrlCheckoutTitle" Text="ltrlCheckoutTitle" runat="server"></asp:Literal></title>
        <style type="text/css">
            #Checkout
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
    <body id="Checkout_body" style="margin: 0px 0px 0px 0px;">
        <form id="Checkout_form" runat="server">
            <div id="Checkout_header" style="margin: 10px 20px 10px 20px;">
                <asp:Label runat="server" ID="lblTitle" Text="lblTitle" Font-Bold="True" 
                    Font-Size="18px" ForeColor="White" Font-Names="Arial" />
                <table border="0" cellpadding="0" cellspacing="0" height="23px" align="right">
                    <tr>
                        <td id="Checkout_close_button_left"></td>
                        <td id="Checkout_close_button_main" width="39px" align="center">
                            <asp:LinkButton ID="lnkBtnCloseCart" runat="server" Text="lnkBtnCloseCart" onclick="lnkBtnCloseCart_Click"/>
                        </td>
                        <td id="Checkout_close_button_right"></td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="Checkout_products_main">
                <asp:Repeater ID="rptrCartItems" runat="server" 
                    onitemcommand="rptrCartItems_ItemCommand">
                    <HeaderTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="880px">
                        <tr id="Items_header" height="21px">
                            <td style="padding-left: 20px;"><%# RXMali.GetXMLNode("Modules/Checkout/desc") %></td>
                            <td align="center"><%# RXMali.GetXMLNode("Modules/Checkout/quantity") %></td>
                            <td></td>
                            <td align="center" colspan="2"><%# RXMali.GetXMLNode("Modules/Checkout/price") %></td>
                            <td></td>
                            <td align="center" colspan="2"><%# RXMali.GetXMLNode("Modules/Checkout/totalprice")%></td>
                            <td></td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="Items_row">
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
                            <td align="left" width="1*"><%# RXMali.GetXMLNode("Modules/Checkout/currencysymbol")%></td>
                            <td width="100px"></td>
                            <td align="right" width="1*"><%# DataBinder.Eval(Container.DataItem, "TotalPrice") %>&nbsp;</td>
                            <td align="left" width="1*"><%# RXMali.GetXMLNode("Modules/Checkout/currencysymbol")%></td>
                            <td align="right" style="padding-right: 20px;">
                                <table border="0" cellpadding="0" cellspacing="0" height="25px">
                                    <tr>
                                        <td id="Checkout_delete_button_left"></td>
                                        <td id="Checkout_delete_button_main">
                                            <asp:LinkButton ID="lnkBtnDeleteItem" runat="server" CommandName="Delete">
                                                <%# DataBinder.Eval(Container.DataItem, "DeleteText") %></asp:LinkButton>
                                        </td>
                                        <td id="Checkout_delete_button_right"></td>
                                    </tr>
                                </table>
                            </td>               
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr id="Items_footer">
                    <td valign="top" style="padding-top: 14px; padding-left: 18px;">
                        <table>
                            <tr>
                                <td style="padding-right: 8px;">
                                    <table border="0" cellpadding="0" cellspacing="0" height="25px">
                                        <tr>
                                            <td id="Checkout_update_button_left"></td>
                                            <td id="Checkout_update_button_main">
                                                <asp:LinkButton ID="lnkBtnUpdateCart" runat="server" CommandName="Update">
                                                    <%= RXMali.GetXMLNode("Modules/Checkout/update")%></asp:LinkButton>
                                            </td>
                                            <td id="Checkout_update_button_right"></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" height="25px">
                                        <tr>
                                            <td id="Checkout_empty_button_left"></td>
                                            <td id="Checkout_empty_button_main">
                                                <asp:LinkButton ID="lnkBtnEmptyCart" runat="server" CommandName="Empty">
                                                    <%= RXMali.GetXMLNode("Modules/Checkout/empty")%></asp:LinkButton>
                                            </td>
                                            <td id="Checkout_empty_button_right"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                    <td colspan="3" align="right" valign="top" style="padding-top: 20px">
                        <div id="TopTwoPricesLables">
                            <%= RXMali.GetXMLNode("Modules/Checkout/totalcartprice")%>:<br />
                            <%= RXMali.GetXMLNode("Modules/Checkout/shiping")%>:
                        </div>
                        <b><%= RXMali.GetXMLNode("Modules/Checkout/totalcartpricewithshiping")%>:</b><br />
                    </td>
                    <td colspan="3" align="right" valign="top" style="padding-top: 20px">
                        <div id="TopTwoPrices">
                            <asp:Label ID="lblTotalCartPrice" runat="server" Text="lblTotalCartPrice" /><br />
                            <asp:Label ID="lblShiping" runat="server" Text="lblShiping" />
                        </div>
                        <asp:Label ID="lblTotalWithShiping" runat="server" Text="lblTotalWithShiping" CssClass="TotalWithShiping" /><br />
                        <asp:Label ID="lblVatSymbol" runat="server" Text="lblVatSymbol" />
                    </td>
                    <td></td>
                </tr>
                </table>
                <asp:Label ID="lblJSCloseWindow" runat="server" Text="lblJSCloseWindow" Visible="False" />
            </div>
            <div id="PaymentMethods">
                <asp:Label ID="lblPaymentMethodsTxt" runat="server" 
                Text="lblPaymentMethodsTxt" CssClass="PaymentMethodsText"></asp:Label>
                <hr noshade="noshade" size="1" />
                <asp:Label ID="lblPaymentError" runat="server" Font-Bold="True" 
                ForeColor="Red" Text="lblPaymentError" Visible="False"></asp:Label>
                <asp:RadioButtonList ID="rblPaymentMethods" runat="server">
                </asp:RadioButtonList>
                <hr noshade="noshade" size="1" />
            </div>
            <div id="DeliveryInfo">
                <asp:Label ID="lblAddressTxt" runat="server" Text="lblAddressTxt" CssClass="DeliveryInfoTitle"></asp:Label>
                <hr noshade="noshade" size="1" />
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblFirstNameTxt" runat="server" Text="lblFirstNameTxt"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblLastNameTxt" runat="server" Text="lblLastNameTxt"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblStreetAddressTxt" runat="server" Text="lblStreetAddressTxt"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtStreetAddress" runat="server"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblZipCodeTxt" runat="server" Text="lblZipCodeTxt"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCityTxt" runat="server" Text="lblCityTxt"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblCountryTxt" runat="server" Text="lblCountryTxt"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPhoneNumberTxt" runat="server" Text="lblPhoneNumberTxt"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblEmailTxt" runat="server" Text="lblEmailTxt"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <hr noshade="noshade" size="1" />
            </div>
            <asp:Label ID="lblTermsTxt" runat="server" Font-Bold="True" Text="lblTermsTxt"></asp:Label>
            <hr />
            <asp:Literal ID="ltrTerms" runat="server" Text="ltrTerms"></asp:Literal>
            <hr />
            <asp:CheckBox ID="chbxAgreeToTerms" runat="server" Text="chbxAgreeToTerms" />
            <br />
            <br />
            <asp:Button ID="btnConfirm" runat="server" onclick="btnConfirm_Click" 
                Text="btnConfirm" />
            <asp:Label ID="lblJavaScript" runat="server" Text="lblJavaScript" 
                    Visible="False" />
        </form>
    </body>
</html>
