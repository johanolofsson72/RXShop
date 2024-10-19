using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public partial class Modules_Shop_Checkout_Checkout : System.Web.UI.Page
{
    String class_name = "Modules_Shop_Checkout_Checkout";

    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            if (Session["Paypal_redirect"] != null && (bool)Session["Paypal_redirect"])
            {
                HandlePayPalRedirect();
            }
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

    private void BindData()
    {
        String function_name = "BindData";
        try
        {
            ltrlCheckoutTitle.Text = RXMali.GetXMLNode("Modules/Checkout/title");
            BindProducts();
            BindPayment();
            BindAddress();
            BindTerms();
            btnConfirm.Text = RXMali.GetXMLNode("Modules/Checkout/confirm");
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
        String function_name = "BindProducts";
        try
        {
            lnkBtnCloseCart.Text = RXMali.GetXMLNode("Modules/BigCart/close");
            CartModel cart = CartModel.Current;

            if (cart.Count() > 0)
            {
                rptrCartItems.Visible = true;

                //Calculate price
                bool showVat = Convert.ToBoolean(ConfigurationManager.AppSettings["Shop.ShowPriceWithVat"].ToString());
                String vatSymbol = showVat ? RXMali.GetXMLNode("Modules/BigCart/withvatsymbol") : RXMali.GetXMLNode("Modules/BigCart/withoutvatsymbol");
                decimal total = 0;
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

                lblTotalCartPrice.Text = PriceToString(total) + RXMali.GetXMLNode("Modules/BigCart/currencysymbol");
                lblShiping.Text = "0,00" + RXMali.GetXMLNode("Modules/BigCart/currencysymbol");
                lblTotalWithShiping.Text = PriceToString(total) + RXMali.GetXMLNode("Modules/BigCart/currencysymbol");
                lblVatSymbol.Text = "(" + vatSymbol + ")";

                DataSet ds = new DataSet();
                DataTable dt = ds.Tables.Add("Products");
                dt.Columns.Add("PicturePath", Type.GetType("System.String"));
                dt.Columns.Add("ProductName", Type.GetType("System.String"));
                dt.Columns.Add("Description", Type.GetType("System.String"));
                dt.Columns.Add("Quantity", Type.GetType("System.String"));
                dt.Columns.Add("Price", Type.GetType("System.String"));
                dt.Columns.Add("TotalPrice", Type.GetType("System.String"));
                dt.Columns.Add("DeleteText", Type.GetType("System.String"));

                for (int n = 0; n < cart.Count(); n++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = FetchProductImageUrl(cart[n].ModuleData.SitId, cart[n].ModuleData.PagId, cart[n].ModuleData.ModId);

                    String choice = cart[n].Choice;
                    if (choice.Equals(""))
                    {
                        dr[1] = cart[n].Name;
                    }
                    else
                    {
                        dr[1] = cart[n].Name + " (" + choice + ")";
                    }
                    dr[2] = FetchProductDesc(cart[n].ModuleData.SitId, cart[n].ModuleData.PagId, cart[n].ModuleData.ModId);
                    dr[3] = cart[n].Quantity;

                    if (ConfigurationManager.AppSettings["Shop.ShowPriceWithVat"].ToString().Equals("True"))
                    {
                        decimal price = cart[n].Price * ((decimal)1.0 + cart[n].Vat);
                        dr[4] = PriceToString(price);

                        price = (cart[n].Price * ((decimal)1.0 + cart[n].Vat)) * cart[n].Quantity;

                        dr[5] = PriceToString(price);
                    }
                    else
                    {
                        decimal price = cart[n].Price;

                        dr[4] = PriceToString(price);

                        price = cart[n].Price * cart[n].Quantity;

                        dr[5] = PriceToString(price);
                    }

                    dr[6] = RXMali.GetXMLNode("Modules/BigCart/delete");
                    dt.Rows.Add(dr);
                }

                rptrCartItems.DataSource = ds;
                rptrCartItems.DataMember = "Products";
                rptrCartItems.DataBind();
            }
            else
            {
                rptrCartItems.Visible = false;
            }
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private String PriceToString(decimal price)
    {
        String totalStringWhole = Decimal.Truncate(price).ToString();
        String totalStringFractions = (price - Decimal.Truncate(price)).ToString();
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

        return totalStringWhole + totalStringFractions;
    }

    private String FetchProductImageUrl(Int32 sitId, Int32 pagId, Int32 modId)
    {
        RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("Gallery2_" + modId, LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, 0, 1000);

        String url = "";

        foreach (RXServer.Modules.Base.List.Item item in cl)
        {
            if (item.Value6 == "true")
            {
                url += RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Upload/Pages/" + pagId + "/" + modId + "/" + item.Value10;
                break;
            }
        }

        return url;
    }

    private String FetchProductDesc(Int32 sitId, Int32 pagId, Int32 modId)
    {
        RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(sitId, pagId, modId);
        String text = Server.HtmlDecode(tm.Text3);
        if (text.Length > 60)
        {
            text = text.Substring(0, 57);
            text = text.Trim();
            if (!text.EndsWith("."))
            {
                text += "...";
            }
        }

        return text;
    }

    private void BindPayment()
    {
        String function_name = "BindPayment";
        try
        {
            lblPaymentMethodsTxt.Text = RXMali.GetXMLNode("Modules/Checkout/paymentmethods");
            lblPaymentError.Text = RXMali.GetXMLNode("Modules/Checkout/paymenterror");
            if (ConfigurationManager.AppSettings["Shop.PayPalPaymentActive"].ToString().Equals("True"))
            {
                rblPaymentMethods.Items.Add(new ListItem(RXMali.GetXMLNode("Modules/Checkout/paypal")));
            }
            if (ConfigurationManager.AppSettings["Shop.SmsPaymentActive"].ToString().Equals("True"))
            {
                rblPaymentMethods.Items.Add(new ListItem(RXMali.GetXMLNode("Modules/Checkout/sms")));
            }
            if (ConfigurationManager.AppSettings["Shop.InvoicePaymentActive"].ToString().Equals("True"))
            {
                rblPaymentMethods.Items.Add(new ListItem(RXMali.GetXMLNode("Modules/Checkout/invoice")));
            }
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void BindAddress()
    {
        String function_name = "BindAddress";
        try
        {
            lblAddressTxt.Text = RXMali.GetXMLNode("Modules/Checkout/address");
            lblFirstNameTxt.Text = RXMali.GetXMLNode("Modules/Checkout/firstname");
            lblLastNameTxt.Text = RXMali.GetXMLNode("Modules/Checkout/lastname");
            lblStreetAddressTxt.Text = RXMali.GetXMLNode("Modules/Checkout/streetaddress");
            lblZipCodeTxt.Text = RXMali.GetXMLNode("Modules/Checkout/zipcode");
            lblCityTxt.Text = RXMali.GetXMLNode("Modules/Checkout/city");
            lblCountryTxt.Text = RXMali.GetXMLNode("Modules/Checkout/country");
            lblPhoneNumberTxt.Text = RXMali.GetXMLNode("Modules/Checkout/phonenumber");
            lblEmailTxt.Text = RXMali.GetXMLNode("Modules/Checkout/email");
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void BindTerms()
    {
        String function_name = "BindTerms";
        try
        {
            String path = ConfigurationManager.AppSettings["Shop.TermsOfAgreementFile"].ToString();
            lblTermsTxt.Text = RXMali.GetXMLNode("Modules/Checkout/terms");
            if (File.Exists(path))
            {
                TextReader tr = new StreamReader(path);
                ltrTerms.Text = tr.ReadToEnd();
                tr.Close();
            }

            chbxAgreeToTerms.Text = RXMali.GetXMLNode("Modules/Checkout/agreetoterms");
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void btnEmptyCart_Click(object sender, EventArgs e)
    {
        String function_name = "btnEmptyCart_Click";
        try
        {
            CartModel.Current.EmptyCart();
            ReturnToParent();
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void rptrCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String function_name = "rptrCartItems_ItemCommand";
        try
        {
            if (e.CommandName.Equals("Delete"))
            {
                /*System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Är du säker?", "Ta bort", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {*/

                int index = e.Item.ItemIndex;
                CartModel.Current.DeleteAt(index);

                //}

                if (CartModel.Current.Count() == 0)
                {
                    ReturnToParent();
                }
                else
                {
                    Response.Redirect("Checkout.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void btnUpdateCart_Click(object sender, EventArgs e)
    {
        String function_name = "btnUpdateCart_Click";
        try
        {
            CartModel cart = CartModel.Current;
            foreach (RepeaterItem item in rptrCartItems.Items)
            {
                TextBox box = (TextBox)item.FindControl("txtQuantity");
                int qty = Convert.ToInt32(box.Text);
                if (qty > 0)
                {
                    cart[item.ItemIndex].Quantity = qty;
                }
            }
            Response.Redirect("Checkout.aspx");
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void ReturnToParent()
    {
        String function_name = "ReturnToParent";
        try
        {
            lblJavaScript.Text = "<script type=\"text/javascript\">returnToParent();</Script>";
            lblJavaScript.Visible = true;
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        String function_name = "btnConfirm_Click";
        try
        {
            bool isPaymentSelected = (rblPaymentMethods.SelectedItem != null);
            bool hasFirstName = !txtFirstName.Text.Equals("");
            bool hasLastName = !txtLastName.Text.Equals("");
            bool hasStreetAddress = !txtStreetAddress.Text.Equals("");
            bool hasZipCode = !txtZipCode.Text.Equals("");
            bool hasCity = !txtCity.Text.Equals("");
            bool hasCountry = !txtCountry.Text.Equals("");
            bool hasPhoneNumber = !txtPhoneNumber.Text.Equals("");
            bool hasEmail = !txtEmail.Text.Equals("") && IsValidEmail(txtEmail.Text);
            bool hasAcceptedTerms = chbxAgreeToTerms.Checked;

            //Handle input error display
            if (!isPaymentSelected)
            {
                lblPaymentError.Visible = true;
            }
            else
            {
                lblPaymentError.Visible = false;
            }

            if (!hasFirstName)
            {
                lblFirstNameTxt.ForeColor = System.Drawing.Color.Red;
                lblFirstNameTxt.Font.Bold = true;
            }
            else
            {
                lblFirstNameTxt.ForeColor = System.Drawing.Color.Black;
                lblFirstNameTxt.Font.Bold = false;
            }

            if (!hasLastName)
            {
                lblLastNameTxt.ForeColor = System.Drawing.Color.Red;
                lblLastNameTxt.Font.Bold = true;
            }
            else
            {
                lblLastNameTxt.ForeColor = System.Drawing.Color.Black;
                lblLastNameTxt.Font.Bold = false;
            }

            if (!hasStreetAddress)
            {
                lblStreetAddressTxt.ForeColor = System.Drawing.Color.Red;
                lblStreetAddressTxt.Font.Bold = true;
            }
            else
            {
                lblStreetAddressTxt.ForeColor = System.Drawing.Color.Black;
                lblStreetAddressTxt.Font.Bold = false;
            }

            if (!hasZipCode)
            {
                lblZipCodeTxt.ForeColor = System.Drawing.Color.Red;
                lblZipCodeTxt.Font.Bold = true;
            }
            else
            {
                lblZipCodeTxt.ForeColor = System.Drawing.Color.Black;
                lblZipCodeTxt.Font.Bold = false;
            }

            if (!hasCity)
            {
                lblCityTxt.ForeColor = System.Drawing.Color.Red;
                lblCityTxt.Font.Bold = true;
            }
            else
            {
                lblCityTxt.ForeColor = System.Drawing.Color.Black;
                lblCityTxt.Font.Bold = false;
            }

            if (!hasCountry)
            {
                lblCountryTxt.ForeColor = System.Drawing.Color.Red;
                lblCountryTxt.Font.Bold = true;
            }
            else
            {
                lblCountryTxt.ForeColor = System.Drawing.Color.Black;
                lblCountryTxt.Font.Bold = false;
            }

            if (!hasPhoneNumber)
            {
                lblPhoneNumberTxt.ForeColor = System.Drawing.Color.Red;
                lblPhoneNumberTxt.Font.Bold = true;
            }
            else
            {
                lblPhoneNumberTxt.ForeColor = System.Drawing.Color.Black;
                lblPhoneNumberTxt.Font.Bold = false;
            }

            if (!hasEmail)
            {
                lblEmailTxt.ForeColor = System.Drawing.Color.Red;
                lblEmailTxt.Font.Bold = true;
            }
            else
            {
                lblEmailTxt.ForeColor = System.Drawing.Color.Black;
                lblEmailTxt.Font.Bold = false;
            }

            if (!hasAcceptedTerms)
            {
                chbxAgreeToTerms.ForeColor = System.Drawing.Color.Red;
                chbxAgreeToTerms.Font.Bold = true;
            }
            else
            {
                chbxAgreeToTerms.ForeColor = System.Drawing.Color.Black;
                chbxAgreeToTerms.Font.Bold = false;
            }

            if (isPaymentSelected && hasFirstName && hasLastName && hasStreetAddress && hasZipCode && hasCity && hasCountry && hasPhoneNumber && hasEmail && hasAcceptedTerms)
            {
                if (rblPaymentMethods.SelectedItem.Text.Equals(RXMali.GetXMLNode("Modules/Checkout/paypal")))
                {
                    HandlePayPalCheckout();
                }
                //Code for other payments
            }
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void HandlePayPalCheckout()
    {
        String function_name = "HandlePayPalCheckout";
        try
        {
            CartModel cart = CartModel.Current;
            bool showVat = Convert.ToBoolean(ConfigurationManager.AppSettings["Shop.ShowPriceWithVat"].ToString());
            decimal total = 0;
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

            String baseUrl;
            if (ConfigurationManager.AppSettings["Shop.PayPalUseSandbox"].ToString().Equals("True"))
            {
                baseUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick";
            }
            else
            {
                baseUrl = "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick";
            }

            String business = "&business=" + ConfigurationManager.AppSettings["Shop.PayPalRecieverEmail"].ToString();
            String lc = "&lc=US";
            String itemName = "&item_name=" + ConfigurationManager.AppSettings["Shop.PayPalItemName"].ToString();

            decimal totalDecimal = Convert.ToDecimal(total);
            decimal fractions = totalDecimal - Decimal.Truncate(totalDecimal);
            fractions = Convert.ToDecimal((fractions + (decimal)0.005).ToString().Substring(0, 4)); //Round the number to two places
            String amount = "&amount=" + (Decimal.Truncate(totalDecimal)).ToString() + "." + (fractions).ToString().Substring(2);

            String currencyCode = "&currency_code=" + ConfigurationManager.AppSettings["Shop.PayPalCurrencyCode"].ToString();
            String returnUrl = "&return=http://" + Request.Url.Authority + Request.Url.AbsolutePath + "?PayPal=return";
            String returnCancleUrl = "&cancel_return=http://" + Request.Url.Authority + Request.Url.AbsolutePath + "?PayPal=cancel";
            String staticEnd = "&button_subtype=products&tax_rate=0%2e000&shipping=0%2e00&bn=PP%2dBuyNowBF%3abtn_buynowCC_LG%2egif%3aNonHostedGuest";

            String finalUrl = baseUrl + business + lc + itemName + amount + currencyCode + returnUrl + returnCancleUrl + staticEnd;
            Session["Paypal_redirect"] = true;
            Session["Order_FirstName"] = txtFirstName.Text;
            Session["Order_LastName"] = txtLastName.Text;
            Session["Order_StreetAddress"] = txtStreetAddress.Text;
            Session["Order_ZipCode"] = txtZipCode.Text;
            Session["Order_City"] = txtCity.Text;
            Session["Order_Country"] = txtCountry.Text;
            Session["Order_PhoneNumber"] = txtPhoneNumber.Text;
            Session["Order_Email"] = txtEmail.Text;
            Response.Redirect(finalUrl);
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void HandlePayPalRedirect()
    {
        String function_name = "HandlePayPalRedirect";
        try
        {
            Session.Remove("Paypal_redirect");
            String result = Request.QueryString["PayPal"];
            if (result != null && result.Equals("cancel"))
            {
                HandlePayPalCancel();
            }
            else
            {
                HandlePayPalReturn();
            }
            ReturnToParent();
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void HandlePayPalCancel()
    {
        String function_name = "HandlePayPalCancel";
        try
        {
            Session.Remove("Order_FirstName");
            Session.Remove("Order_LastName");
            Session.Remove("Order_StreetAddress");
            Session.Remove("Order_ZipCode");
            Session.Remove("Order_City");
            Session.Remove("Order_Country");
            Session.Remove("Order_PhoneNumber");
            Session.Remove("Order_Email");
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void HandlePayPalReturn()
    {
        String function_name = "HandlePayPalReturn";
        try
        {
            //Mail order to shop.
            String mailFrom = ConfigurationManager.AppSettings["Shop.OrderEmailSender"].ToString();
            String mailFromName = ConfigurationManager.AppSettings["Shop.OrderEmailSenderName"].ToString();
            String mailTo = ConfigurationManager.AppSettings["Shop.OrderEmail"].ToString();
            String mailSubject = ConfigurationManager.AppSettings["Shop.OrderEmailSubject"].ToString();
            String mailServer = ConfigurationManager.AppSettings["MailServer"].ToString();
            String mailPort = ConfigurationManager.AppSettings["MailPort"].ToString();

            String mailBody = "<html><body>Order confirmed<br>";
            CartModel cart = CartModel.Current;
            mailBody += "<table border=\"1\"><tr><td>Name</td><td>quantity</td><td>price</td><td>vat</td></tr>";
            for (int n = 0; n < cart.Count(); n++)
            {
                mailBody += "<tr><td>" + cart[n].Name + "</td><td>" + cart[n].Quantity + "</td><td>" + cart[n].Price + "</td><td>" + cart[n].Vat + "</td></tr>";
            }
            mailBody += "</table><br>" + Session["Order_FirstName"].ToString() + " " + Session["Order_LastName"].ToString() + "<br>";
            mailBody += Session["Order_StreetAddress"].ToString() + ", " + Session["Order_ZipCode"].ToString() + ", " + Session["Order_City"].ToString() + "<br>";
            mailBody += Session["Order_Country"].ToString() + "<br>";
            mailBody += Session["Order_PhoneNumber"].ToString() + ", " + Session["Order_Email"].ToString() + "<br>";
            mailBody += "</body></html>";

            RXMali.SendMail(mailBody, mailSubject, mailTo, mailFrom, mailFromName, mailServer, mailPort);

            //Mail order to buyer.
            mailTo = Session["Order_Email"].ToString();

            mailBody = "<html><body>Order confirmed<br>";
            mailBody += "<table border=\"1\"><tr><td>Name</td><td>quantity</td><td>price</td><td>vat</td></tr>";
            for (int n = 0; n < cart.Count(); n++)
            {
                mailBody += "<tr><td>" + cart[n].Name + "</td><td>" + cart[n].Quantity + "</td><td>" + cart[n].Price + "</td><td>" + cart[n].Vat + "</td></tr>";
            }
            mailBody += "</table><br>If the details are incorrect please contact us as soon as possible.<br><br>";
            mailBody += ConfigurationManager.AppSettings["Shop.CompanyName"].ToString() + "<br>";
            mailBody += ConfigurationManager.AppSettings["Shop.StreetAddress"].ToString() + ", " + ConfigurationManager.AppSettings["Shop.ZipCode"].ToString() + ", " + ConfigurationManager.AppSettings["Shop.City"].ToString() + "<br>";
            mailBody += ConfigurationManager.AppSettings["Shop.Country"].ToString() + "<br>";
            mailBody += ConfigurationManager.AppSettings["Shop.PhoneNumber"].ToString() + ", " + ConfigurationManager.AppSettings["Shop.ContactEmail"].ToString() + "<br>";
            mailBody += "</body></html>";

            RXMali.SendMail(mailBody, mailSubject, mailTo, mailFrom, mailFromName, mailServer, mailPort);

            //Remove session data for buyer contact info.
            Session.Remove("Order_FirstName");
            Session.Remove("Order_LastName");
            Session.Remove("Order_StreetAddress");
            Session.Remove("Order_ZipCode");
            Session.Remove("Order_City");
            Session.Remove("Order_Country");
            Session.Remove("Order_PhoneNumber");
            Session.Remove("Order_Email");

            CountDownStock();

            CartModel.Current.EmptyCart();
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    
    public bool IsValidEmail(string email)
    {
        String function_name = "IsValidEmail";
        try
        {
            //regular expression pattern for valid email
            //addresses, allows for the following domains:
            //com,edu,info,gov,int,mil,net,org,biz,name,museum,coop,aero,pro,tv
            string pattern = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.
                (com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$";
            //Regular expression object
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            //boolean variable to return to calling method
            bool valid = false;

            //make sure an email address was provided
            if (string.IsNullOrEmpty(email))
            {
                valid = false;
            }
            else
            {
                //use IsMatch to validate the address
                valid = check.IsMatch(email);
            }
            //return the value to the calling method
            return valid;
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
            return false;
        }
    }

    private void CountDownStock()
    {
        String function_name = "CountDownStock";
        try
        {
            CartModel cart = CartModel.Current;
            for (int n = 0; n < cart.Count(); n++)
            {
                RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(cart[n].ModuleData.SitId, cart[n].ModuleData.PagId, cart[n].ModuleData.ModId);
                int stock = Convert.ToInt32(Server.HtmlDecode(tm.Text7));
                stock -= cart[n].Quantity;
                tm.Text7 = Server.HtmlEncode(stock.ToString());
                tm.Save();
            }
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void lnkBtnCloseCart_Click(object sender, EventArgs e)
    {
        ReturnToParent();
    }
}
