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
using System.Text;
using System.IO;
using Telerik;
using Telerik.Web.UI;

public partial class Modules_Menus_Category_Category : RXServer.Lib.RXBaseModule
{
    String class_name = "Modules_Menus_Category_Category";

    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            if (RXServer.Auth.IsInRole("Admin"))
            {
                this.Category_admin.Visible = true;
                this.img_AdminEdit.OnClientClick = "javascript:showAdminCategory(1," + RXServer.Web.RequestValues.PagId + "," + this.ModId + "); return false;";
                this.img_AdminDelete.OnClientClick = "javascript:showAdminDeleteModule(1," + this.PagId + "," + this.ModId + "); return false;";
            }

            bindData();
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
            Int32 pagId = RXServer.Web.RequestValues.PagId;
            RXServer.Modules.Menu m = new RXServer.Modules.Menu(1, pagId, 1);

            String menu = "";

            RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(1, RXServer.Web.RequestValues.PagId, this.ModId);
            ltrTitle.Text = tm.Text1;

            if (m.Count > 0)
            {
                foreach (LiquidCore.Menu.Item mi in m)
                {

                    if (!mi.Hidden || RXServer.Auth.AuthorizedUser.Identity.Authenticated)
                    {
                        int targetPagId = mi.Id;

                        menu += "<div id=\"ProductTeaser_holder\" runat=\"server\" style=\"float: left; width: 215px; margin-right: 10px;\">";
                        menu += "<div id=\"ProductTeaser\" class=\"ProductTeaser\" style=\"text-align:left;	font-size:1px; padding-bottom:20px;\" runat=\"server\">";
                        menu += "<div id=\"tb_top\">&nbsp;</div>";
                        menu += "<div id=\"tb_main\">";
                        menu += GetMediaCode(targetPagId);
                        menu += "<h3>" + FetchProductTitle(targetPagId) + "</h3>";
                        menu += "<p>" + FetchProductText(targetPagId) + "</p>";
                        menu += "<div style=\"width:100%; text-align:right; padding-bottom: 10px;\"><a href='Default.aspx?PagId=" + targetPagId + "'>" + RXMali.GetXMLNode("Modules/Category/readmore") + "</a></div>";
                        menu += "</div>";
                        menu += "<div id=\"tb_bottom\">&nbsp;</div>";
                        menu += "</div>";
                        menu += "</div>";
                    }

                }
            }

            this.ltrCategory.Text = menu;
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private String GetMediaCode(int pagId)
    {
        RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(1, pagId, FetchProductId(pagId));
        String showMedia = "";

        Int32 link = 0;
        Int32.TryParse(tm.ReadMoreLinkId, out link);

        if (link > 0)
        {
            showMedia += "<a href='Default.aspx?PagId=" + link + "'>";                           
        }

        showMedia += "<img src='" + FetchProductImageUrl(pagId) + "' style='width: " + 173 + "px;' title='" + tm.MediaToolTip + "'/>";

        if (link > 0)
        {
            showMedia += "</a>";
        }

        return showMedia;
    }

    private String FetchProductImageUrl(Int32 pagId)
    {
        RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("Gallery2_" + FetchProductId(pagId), LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, 0, 1000);

        String url = "";

        foreach (RXServer.Modules.Base.List.Item item in cl)
        {
            if (item.Value6 == "true")
            {
                url += RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Upload/Pages/" + pagId + "/" + FetchProductId(pagId) + "/" + item.Value10;
                break;
            }
        }

        return url;
    }

    private int FetchProductId(int pagId)
    {
        int id = -1;
        foreach(LiquidCore.Module md in new RXServer.Modules.Menu.Item(pagId).Modules)
        {
            if (md.Alias.Equals("ProductBrowser_Instance"))
            {
                id = md.Id;
                break;
            }
        }

        return id;
    }

    private String FetchProductTitle(int pagId)
    {
        String title = "";
        foreach (LiquidCore.Module md in new RXServer.Modules.Menu.Item(pagId).Modules)
        {
            if (md.Alias.Equals("ProductBrowser_Instance"))
            {
                RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(1, pagId, md.Id);
                title = Server.HtmlDecode(tm.Text2);
                break;
            }
        }

        if (title.Equals(""))
        {
            title = "[NotSet]";
        }
        return title;
    }

    private String FetchProductText(int pagId)
    {
        String text = "";
        foreach (LiquidCore.Module md in new RXServer.Modules.Menu.Item(pagId).Modules)
        {
            if (md.Alias.Equals("ProductBrowser_Instance"))
            {
                RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(1, pagId, md.Id);
                text = Server.HtmlDecode(tm.Text10);
                break;
            }
        }

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
}
