using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Drawing.Printing;

public partial class Modules_Boxes_ProductBrowser_ProductBrowser : RXServer.Lib.RXBaseModule
{
    String class_name = "Modules_Boxes_ProductBrowser_ProductBrowser";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {        
            wrapper1.SitId = SitId;
            wrapper1.PagId = PagId;
            wrapper1.ModId = ModId;
            if (RXServer.Auth.IsInRole("Admin"))
            {
                this.ProductBrowser_admin.Visible = true;
                this.img_AdminEdit.OnClientClick = "javascript:showAdminProductBrowser(1," + this.PagId + "," + this.ModId + ",'2'); return false;";
                this.img_AdminDelete.OnClientClick = "javascript:showAdminDeleteModule(1," + this.PagId + "," + this.ModId + "); return false;";
            }

            if (!Page.IsPostBack)
            {
                bindData();
            }
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void bindData()
    {
        String function_name = "bindData";
        try
        {
            //System.Diagnostics.Debug.WriteLine("RequestValues.Page: " + RXServer.Web.RequestValues.Page);

            RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(this.SitId, this.PagId, this.ModId);

            if (tm.Visible == "true" || RXServer.Auth.IsInRole("Admin"))
            {

                this.ProductBrowser_holder.Visible = true;

                // Sets Module Width

                Int32 ModelId = 0;
                Int32.TryParse(tm.ModelId, out ModelId);
                Int32 _width = RXMali.GetModelSize(ModelId.ToString());
                
                if (ModelId == 0)
                {
                    _width = 430;
                }

                String _style = "";
                _style = "position: relative; float: " + tm.Float + "; width: " + _width + "px;";

                this.ProductBrowser_admin.Attributes.Add("style", _style);
                this.ProductBrowser_holder.Attributes.Add("style", _style);

                decimal price = tm.Text5.Equals("") ? 0 : Convert.ToDecimal(Server.HtmlDecode(tm.Text5));
                decimal oldPrice = tm.Text8.Equals("") ? 0 : Convert.ToDecimal(Server.HtmlDecode(tm.Text8));
                decimal vat = tm.Text6.Equals("") ? 0 : Convert.ToDecimal(Server.HtmlDecode(tm.Text6));

                lblPrice.Text = CreatePriceString(price, vat) + RXMali.GetXMLNode("Modules/ProductBrowser/currencysymbol") + " / " + RXMali.GetXMLNode("Modules/ProductBrowser/quantitysymbol");
                if (oldPrice > price)
                {
                    ProductBrowser_SpecialPrice.Visible = true;
                    ProductBrowser_OldPrice.Visible = true;
                    lblOldPrice.Text = CreatePriceString(oldPrice, vat) + RXMali.GetXMLNode("Modules/ProductBrowser/currencysymbol") + " / " + RXMali.GetXMLNode("Modules/ProductBrowser/quantitysymbol");
                }

                lblSelectionsTitle.Text = Server.HtmlDecode(tm.Text1);

                BindChoices();

                txtQuantity.Text = "1";

                String stock;
                if(!Server.HtmlDecode(tm.Text7).Equals("") && Convert.ToInt32(Server.HtmlDecode(tm.Text7)) < 1 || Server.HtmlDecode(tm.Text7).Equals(""))
                {
                    stock = RXMali.GetXMLNode("Modules/ProductBrowser/isnotinstock");
                }
                else
                {
                    stock = RXMali.GetXMLNode("Modules/ProductBrowser/isinstock");
                }

                lblTitle.Text = Server.HtmlDecode(tm.Text2);
                lblText.Text = Server.HtmlDecode(tm.Text3);

                BindGallery();
                BindTabs();

                // CONTENT

                if (tm.Visible == "true")
                {
                    this.imbAdminVisible.ImageUrl = "~/App_Themes/WebAdmin/Images/adminbar_vis_true.gif";
                    this.imbAdminVisible.ToolTip = "Hide Content";
                }
                else
                {
                    this.imbAdminVisible.ImageUrl = "~/App_Themes/WebAdmin/Images/adminbar_vis_false.gif";
                    this.imbAdminVisible.ToolTip = "Publish Content";
                }
            }
            else
            {
                this.ProductBrowser_holder.Visible = false;
            }
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private String CreatePriceString(decimal price, decimal vat)
    {
        decimal total;
        String vatSymbol;

        //Create VAT symbol
        if (ConfigurationManager.AppSettings["Shop.ShowPriceWithVat"].ToString().Equals("True"))
        {
            total = price * (1 + vat);
            vatSymbol = RXMali.GetXMLNode("Modules/ProductBrowser/withvatsymbol");
        }
        else
        {
            total = price;
            vatSymbol = RXMali.GetXMLNode("Modules/ProductBrowser/withoutvatsymbol");
        }

        //Handle fractions
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

        return totalStringWhole + totalStringFractions;
    }

    protected void img_AdminMoveDown_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "img_AdminMoveDown_Click";
        try
        {
            RXServer.Modules.MediaModule mm = new RXServer.Modules.MediaModule(this.SitId, this.PagId, this.ModId);
            mm.ChangeOrderDown();
            mm.Save();

            RXServer.Web.Redirect.To("Default.aspx?PagId=" + this.PagId);
        }
        catch(Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
        
    }
    protected void img_AdminMoveUp_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "img_AdminMoveUp_Click";
        try
        {
            RXServer.Modules.MediaModule mm = new RXServer.Modules.MediaModule(this.SitId, this.PagId, this.ModId);
            mm.ChangeOrderUp();
            mm.Save();

            RXServer.Web.Redirect.To("Default.aspx?PagId=" + this.PagId);
        }
        catch(Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void img_AdminVisible_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "img_AdminVisible_Click";
        try
        {
            RXServer.Modules.MediaModule mm = new RXServer.Modules.MediaModule(this.SitId, this.PagId, this.ModId);
            if (mm.Visible == "true")
            {
                mm.Visible = "false";
            }
            else
            {
                mm.Visible = "true";
            }
            mm.Save();

            RXServer.Web.Redirect.To("Default.aspx?PagId=" + this.PagId);
        }
        catch(Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void img_AdminSocial_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "img_AdminSocial_Click";
        try
        {
            RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(this.SitId, this.PagId, this.ModId);
            if (tm.ExtraSocial == "true")
            {
                tm.ExtraSocial = "false";
            }
            else
            {
                tm.ExtraSocial = "true";
            }
            tm.Save();

            RXServer.Web.Redirect.To(RXMali.GetLastUrl(Request.Url.ToString()));
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void BindGallery()
    {
        String function_name = "BindGallery";
        try
        {
            String html = "";

            RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("Gallery2_" + this.ModId, LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, 0, 1000);

            html += "<ul class=\"thumbs noscript\">";

            foreach (RXServer.Modules.Base.List.Item item in cl)
            {
                if (item.Value6 == "true")
                {
                    html += "<li><a class=\"thumb\" href=\""+ RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Upload/Pages/" + this.PagId + "/" + this.ModId + "/" + item.Value10 + "\" title=\"" + item.Value13 + "\">";
					html += "<img src=\"" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Upload/Pages/" + this.PagId + "/" + this.ModId + "/" + item.Value14 + "\" alt=\"" + item.Value13 + "\" /></a>";
                    //<a href=\"" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Upload/Pages/" + this.PagId + "/" + this.ModId + "/" + item.Value10 + "\" original=\"" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix()+ "Upload/Pages/" + this.PagId + "/" + this.ModId + "/" + item.Value10 + "\" title=\"" + item.Value13 + "\" description=\"" + item.Value13 + "\"><img src=\"" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Upload/Pages/" + this.PagId + "/" + this.ModId + "/" + item.Value14 + "\" alt=\"" + item.Value13 + "\" /></a>";
                    html += "<div class='caption'>";
                    html += "<div class='image-title'>" + Server.HtmlDecode(item.Value13).Replace("`", "'") + "</div>";
                    html += "<div class='image-desc'>" + Server.HtmlDecode(item.Value25).Replace("`", "'") + "</div>";
                    html += "</div>";
                    html += "</li>";
                }
            }

            html += "</ul>";

            this.ltrGallery2.Text = html;


        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void BindTabs()
    {
        String function_name = "BindTabs";
        try
        {
            String html = "<div class=\"tabset\" id=\"tabset1\">";
            Int32 counter = 1;

            RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("ProductBrowserTab_" + this.ModId, LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, 0, 1000);

            foreach (RXServer.Modules.Base.List.Item item in cl)
            {
                html += "<a id=\"a\" class=\"tab {content:'cont_" + counter.ToString() + "'}\">" + Server.HtmlDecode(item.Value1) + "</a>";
                counter++;
            }
            counter = 1;
            foreach (RXServer.Modules.Base.List.Item item in cl)
            {
                html += "<div id=\"cont_" + counter.ToString() + "\">";
                html += Server.HtmlDecode(item.Value2);
                html += "</div>";
                counter++;
            }
            html += "</div>";

            this.ltrTabs.Text = html;
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void BindChoices()
    {
        String function_name = "BindChoices";
        try
        {
            String html = "";
            Int32 counter = 1;

            RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("Choices_" + this.ModId, LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, 0, 1000);

            foreach (RXServer.Modules.Base.List.Item item in cl)
            {
                ListItem choice = new ListItem();
                choice.Text = Server.HtmlDecode(item.Value1);
                rdBtnLstChoices.Items.Add(choice);
                counter++;
            }

            rdBtnLstChoices.SelectedIndex = 0;

            if (counter == 1)
            {
                tblSelectionTitle.Visible = false;
            }

            //this.ltrRadioButtons.Text = html;
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void imgBtnAddToCart_Click(object sender, ImageClickEventArgs e)
    {
        // flyttad till wrapper...


        //String function_name = "imgbAddToCart_Click";
        //try
        //{
        //    int quantity;
        //    bool valid = Int32.TryParse(txtQuantity.Text, out quantity);
        //    if (valid && quantity > 0)
        //    {
        //        RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(this.SitId, this.PagId, this.ModId);
        //        String productId = Server.HtmlDecode(tm.Text9);
        //        String name = Server.HtmlDecode(tm.Text4);
        //        decimal price = Convert.ToDecimal(Server.HtmlDecode(tm.Text5));
        //        decimal vat = Convert.ToDecimal(Server.HtmlDecode(tm.Text6));
        //        bool inStock = Convert.ToInt32(Server.HtmlDecode(tm.Text7)) > 0;
        //        String choice = "";
        //        if (rdBtnLstChoices.SelectedItem != null)
        //        {
        //            choice = rdBtnLstChoices.SelectedItem.Text;
        //        }
        //        CartModel.Product.ProductModuleData moduleData = new CartModel.Product.ProductModuleData(this.SitId, this.PagId, this.ModId);
        //        CartModel.Current.Add(productId, name, price, vat, quantity, inStock, choice, moduleData);

                
        //        //RXServer.Web.Redirect.To("Default.aspx?PagId=" + this.PagId);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        //}    
    }
}
