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
using System.Threading;

public partial class Modules_Boxes_ProductBrowser_wrapper : System.Web.UI.UserControl
{
    private Thread trd;
    private Int32 _SitId = 0;
    private Int32 _PagId = 0;
    private Int32 _ModId = 0;
    public Int32 SitId { get { return _SitId; } set { _SitId = value; } }
    public Int32 PagId { get { return _PagId; } set { _PagId = value; } }
    public Int32 ModId { get { return _ModId; } set { _ModId = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void imgBtnAddToCart_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imgbAddToCart_Click";
        try
        {
            int quantity;
            bool valid = Int32.TryParse(((TextBox)RXServer.Lib.Common.FindControlRecursive(Page, "txtQuantity")).Text, out quantity);
            if (valid && quantity > 0)
            {
                RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(_SitId, _PagId, _ModId);
                String productId = Server.HtmlDecode(tm.Text9);
                String name = Server.HtmlDecode(tm.Text4);
                decimal price = Convert.ToDecimal(Server.HtmlDecode(tm.Text5));
                decimal vat = Convert.ToDecimal(Server.HtmlDecode(tm.Text6));
                bool inStock = Convert.ToInt32(Server.HtmlDecode(tm.Text7)) > 0;
                String choice = "";
                if (((RadioButtonList)RXServer.Lib.Common.FindControlRecursive(Page, "rdBtnLstChoices")).SelectedItem != null)
                {
                    choice = ((RadioButtonList)RXServer.Lib.Common.FindControlRecursive(Page, "rdBtnLstChoices")).SelectedItem.Text;
                }
                CartModel.Product.ProductModuleData moduleData = new CartModel.Product.ProductModuleData(_SitId, _PagId, _ModId);
                CartModel.Current.Add(productId, name, price, vat, quantity, inStock, choice, moduleData);

                Control c = RXServer.Lib.Common.FindControlRecursive(Page, "RadAjaxPanel_SmallCart");
                if (c != null)
                {
                    Telerik.Web.UI.RadAjaxPanel p = (Telerik.Web.UI.RadAjaxPanel)c;
                    String call1 = String.Format("$find('{0}').ajaxRequest('load');", p.ClientID);
                    RadAjaxPanel1.ResponseScripts.Add(call1);
                }
            }
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, ":" + function_name, String.Empty);
        }
    }

}
