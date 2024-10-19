using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Modules_Shop_SmallCart_SmallCart : RXServer.Lib.RXBaseModule
{
    String class_name = "Modules_Boxes_Shop_SmallCart_SmallCart";

    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            imgBtnCart.OnClientClick = "javascript:showBigCart(1, 1, 1, '1'); return false;";
            lnkBtnCheckout.OnClientClick = "javascript:showCheckout(1, 1, 1, '1'); return false;";
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void RadAjaxManager_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {

        SmallCartx.Attributes["style"] += "background-color: #F39531;";
        BindData();
        lblCartText.Text += "-load";
        apa.Controls.Add(new LiteralControl("<script>XXX('" + lblCartText.ClientID.Replace("_", "$") + "');</script>"));
    }

    protected void imgBtnCart_Load(object sender, EventArgs e)
    {
        String function_name = "imgBtnCart_Load";
        try
        {
            BindData();
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void BindData()
    {
        String function_name = "BindData";
        try
        {
            //System.Diagnostics.Debug.WriteLine("RequestValues.Page: " + RXServer.Web.RequestValues.Page);
            BindProducts();
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    /*
     * Populates the repeater with the products added to the cart.
     */
    private void BindProducts()
    {
        CartModel cart = CartModel.Current;

        ltrCartIcon.Text = "<img src=\"http://" + Request.Url.Authority + Request.ApplicationPath + "/Images/Modules/Shop/small_cart_icon.png\" alt=\"Ikon föreställande en kundkorg.\" />";

        if (cart.Count() > 0)
        {
            //SmallCart.Attributes["style"] += "background-color: #F39531;";
            String text = cart.Count().ToString();
            if (cart.Count() == 1)
            {
                text += RXMali.GetXMLNode("Modules/SmallCart/product");
            }
            else
            {
                text += RXMali.GetXMLNode("Modules/SmallCart/products");
            }

            //Calculate price w/ or w/o VAT.
            decimal total = 0;
            bool showVat = Convert.ToBoolean(ConfigurationManager.AppSettings["Shop.ShowPriceWithVat"].ToString());
            String vatSymbol = showVat ? RXMali.GetXMLNode("Modules/SmallCart/withvatsymbol") : RXMali.GetXMLNode("Modules/SmallCart/withoutvatsymbol");
            for (int n = 0; n < cart.Count(); n++)
            {
                if (showVat)
                {
                    total += (decimal)cart[n].Quantity * (cart[n].Price * ((decimal)1.0 + cart[n].Vat));
                }
                else
                {
                    total += (decimal)cart[n].Quantity * cart[n].Price;
                }
            }

            //Handle fractions presentation
            String totalStringWhole = Decimal.Truncate(total).ToString();
            String totalStringFractions = (total - Decimal.Truncate(total)).ToString();
            if (totalStringFractions.Equals("0"))
            {
                totalStringFractions = ",00";
            }
            else if (totalStringFractions.Length < 4)
            {
                totalStringFractions = totalStringFractions.Substring(1);
                totalStringFractions += "0";
            }
            else
            {
                totalStringFractions = totalStringFractions.Substring(1, 3);
            }
 
            String currencySymbol = RXMali.GetXMLNode("Modules/SmallCart/currencysymbol");
            text += totalStringWhole + totalStringFractions + currencySymbol;// +" (" + vatSymbol + ")";


            lblCartText.Text = text;
            //open_cart_button.Visible = true;
            //to_checkout_button.Visible = true;
            lnkBtnCheckout.Text = RXMali.GetXMLNode("Modules/SmallCart/checkout");
        }
        else
        {
            lblCartText.Text = RXMali.GetXMLNode("Modules/SmallCart/emptytext");
            //SmallCart.Attributes["style"] += "background-color: #3B6A74;";
            //open_cart_button.Visible = false;
            //to_checkout_button.Visible = false;
        }

        //lnkEditCart.NavigateUrl = "javascript:showBigCart(1," + this.PagId + "," + this.ModId + ",'1');";

        if (!ConfigurationManager.AppSettings["Shop.OrderEmail"].ToString().Equals(""))
        {
            //lnkCheckout.NavigateUrl = "javascript:showCheckout(1," + this.PagId + "," + this.ModId + ",'1');";
        }
        else
        {
            //lnkCheckout.NavigateUrl = "javascript:alert('No order email in the configuration. Please contact site admin.');";
        }
    }
}
