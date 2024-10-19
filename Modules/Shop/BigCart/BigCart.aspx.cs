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

public partial class Modules_Shop_BigCart_BigCart : System.Web.UI.Page
{
    String class_name = "Modules_Shop_BigCart_BigCart";

    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
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
        ltrlBigCartTitle.Text = RXMali.GetXMLNode("Modules/BigCart/title");
        lblTitle.Text = RXMali.GetXMLNode("Modules/BigCart/title");

        BindProducts();
    }

    /*
     * Populates the repeater with the products added to the cart.
     */
    private void BindProducts()
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

    protected void rptrCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName.Equals("Delete"))
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
                Response.Redirect("bigCart.aspx");
            }
        }
        else if (e.CommandName.Equals("Update"))
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
            Response.Redirect("bigCart.aspx");
        }
        else if (e.CommandName.Equals("Empty"))
        {
            CartModel.Current.EmptyCart();
            ReturnToParent();
        }
    }

    private void ReturnToParent()
    {
        lblJSCloseWindow.Text = "<script type=\"text/javascript\">returnToParent();</Script>";
        lblJSCloseWindow.Visible = true;
    }

    protected void lnkBtnCloseCart_Click(object sender, EventArgs e)
    {
        ReturnToParent();
    }
}
