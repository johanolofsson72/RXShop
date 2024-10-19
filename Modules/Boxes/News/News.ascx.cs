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
using Telerik.Web.UI;

public partial class Modules_Boxes_News_News : RXServer.Lib.RXBaseModule
{
    String class_name = "Modules_Boxes_News_News";
    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            if (RXServer.Auth.IsInRole("Admin"))
            {
                this.News_admin.Visible = true;
                this.img_AdminEdit.OnClientClick = "javascript:showAdminNews(1," + this.PagId + "," + this.ModId + "); return false;";
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
            RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(this.SitId, this.PagId, this.ModId);

            if (sm.Visible == "true" || RXServer.Auth.IsInRole("Admin"))
            {
 
                this.News_holder.Visible = true;
                this.lblTitle.Text = Server.HtmlDecode(sm.Text1);
                //this.hplRSS.NavigateUrl = "~/RSS/rss.aspx?Var=News&PagId=" + this.PagId + "&Id=" + this.ModId;
                //this.lbnArchive.Text = "» " + RXMali.GetXMLNode("Modules/News/newsarchive");
                //this.lbnHide.Text = RXMali.GetXMLNode("Modules/News/hide_newsarchive");


                BindListData();

                // Sets Module Width

                Int32 ModelId = 0;
                Int32.TryParse(sm.ModelId, out ModelId);
                Int32 _width = RXMali.GetModelSize(ModelId.ToString());

                String _style = "";
                _style = "position: relative; float: " + sm.Float + "; width: " + _width + "px;";

                this.News_admin.Attributes.Add("style", _style);
                this.News_holder.Attributes.Add("style", _style);

                if (sm.Visible == "true")
                {
                    this.imbAdminVisible.ImageUrl = "~/App_Themes/WebAdmin/Images/adminbar_vis_true.gif";
                    this.imbAdminVisible.ToolTip = "Hide Content";
                }
                else
                {
                    this.imbAdminVisible.ImageUrl = "~/App_Themes/WebAdmin/Images/adminbar_vis_false.gif";
                    this.imbAdminVisible.ToolTip = "Publish Content";
                }

                //if (sm.ExtraSocial == "true")
                //{
                //    this.imbAdminSocial.ImageUrl = "~/App_Themes/WebAdmin/Images/adminbar_share_on.gif";
                //    this.imbAdminSocial.ToolTip = "Disable Social Links";
                //}
                //else
                //{
                //    this.imbAdminSocial.ImageUrl = "~/App_Themes/WebAdmin/Images/adminbar_share_off.gif";
                //    this.imbAdminSocial.ToolTip = "Enable Social Links"; 
                //}


            }
        }
        catch(Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void BindListData()
    {
        String function_name = "BindListData";
        try
        {
            String html = "";

            RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("News_" + this.ModId, LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, 0, 1000);

            html += "<div id=\"NewsItem\">";

            Int32 link = 0;

            foreach (RXServer.Modules.Base.List.Item item in cl)
            {
                Int32.TryParse(item.Value26, out link);
                html += "<h2>" + Server.HtmlDecode(item.Value2).Replace("`", "'") + "</h2>";
                html += "<em class='Date'>" + item.Value25.Replace('-', '/') + "</em>";
                html += "<p class='Introduction'>" + EE.EncodeEmails(Server.HtmlDecode(item.Value5)).Replace("`", "'") + "</p>";
                //html += "<a href='" + RXServer.Lib.Common.Dynamic.GetFriendlyUrl(link) + "'>" + RXMali.GetXMLNode("Modules/News/readmore_link") + "</a><br /><br /><br />";
                String path = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
                html += "<a href='" + path + "Default.aspx?PagId=" + link + "'>" + RXMali.GetXMLNode("Modules/News/readmore_link") + "</a><br /><br /><br />";
            }

            html += "</div>";

            this.ltrNewsList.Text = html;
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void img_AdminMoveDown_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "img_AdminMoveDown_Click";
        try
        {
            RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(this.SitId, this.PagId, this.ModId);
            sm.ChangeOrderDown();
            sm.Save();

            RXServer.Web.Redirect.To("~/Default.aspx?PagId=" + this.PagId);
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
            RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(this.SitId, this.PagId, this.ModId);
            sm.ChangeOrderUp();
            sm.Save();

            RXServer.Web.Redirect.To("~/Default.aspx?PagId=" + this.PagId);
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
            RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(this.SitId, this.PagId, this.ModId);
            if (sm.Visible == "true")
            {
                sm.Visible = "false";
            }
            else
            {
                sm.Visible = "true";
            }
            sm.Save();

            RXServer.Web.Redirect.To("~/Default.aspx?PagId=" + this.PagId);
        }
        catch(Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    //protected void img_AdminSocial_Click(object sender, ImageClickEventArgs e)
    //{
    //    String function_name = "img_AdminSocial_Click";
    //    try
    //    {
    //        RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(this.SitId, this.PagId, this.ModId);
    //        if (sm.ExtraSocial == "true")
    //        {
    //            sm.ExtraSocial = "false";
    //        }
    //        else
    //        {
    //            sm.ExtraSocial = "true";
    //        }
    //        sm.Save();

    //        RXServer.Web.Redirect.To("Default.aspx?PagId=" + this.PagId);
    //    }
    //    catch(Exception ex)
    //    {
    //        RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
    //    }
    //}

    //protected void imbSmallText_Click(object sender, ImageClickEventArgs e)
    //{
    //    String function_name = "imbSmallText_Click";
    //    try
    //    {
    //        Int32 id = 0;
    //        Int32.TryParse(((ImageButton)sender).CommandArgument, out id);
    //        Session["font_" + id] = "0";
    //    }
    //    catch(Exception ex)
    //    {
    //        RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
    //    }
    //}
    //protected void imbMediumText_Click(object sender, ImageClickEventArgs e)
    //{
    //    String function_name = "imbMediumText_Click";
    //    try
    //    {
    //        Int32 id = 0;
    //        Int32.TryParse(((ImageButton)sender).CommandArgument, out id);
    //        Session["font_" + id] = "1";
    //    }
    //    catch (Exception ex)
    //    {
    //        RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
    //    }
    //}
    //protected void imbLargeText_Click(object sender, ImageClickEventArgs e)
    //{
    //    String function_name = "imbLargeText_Click";
    //    try
    //    {
    //        Int32 id = 0;
    //        Int32.TryParse(((ImageButton)sender).CommandArgument, out id);
    //        Session["font_" + id] = "2";
    //    }
    //    catch(Exception ex)
    //    {
    //        RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
    //    }
    //}
}
