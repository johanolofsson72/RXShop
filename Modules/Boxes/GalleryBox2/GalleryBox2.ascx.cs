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

public partial class Modules_Boxes_GalleryBox2_GalleryBox2 : RXServer.Lib.RXBaseModule
{
    String class_name = "Modules_Boxes_GalleryBox2_GalleryBox2";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            if (RXServer.Auth.IsInRole("Admin"))
            {
                this.GalleryBox2_admin.Visible = true;
                this.img_AdminEdit.OnClientClick = "javascript:showAdminGalleryBox2(1," + this.PagId + "," + this.ModId + ",'2'); return false;";
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
            RXServer.Modules.MediaModule mm = new RXServer.Modules.MediaModule(this.SitId, this.PagId, this.ModId);

            if (mm.Visible == "true" || RXServer.Auth.IsInRole("Admin"))
            {

                this.GalleryBox2_holder.Visible = true;

                // Sets Module Width

                Int32 ModelId = 0;
                Int32.TryParse(mm.ModelId, out ModelId);
                Int32 _width = RXMali.GetModelSize(ModelId.ToString());
                
                if (ModelId == 0)
                {
                    _width = 430;
                }


                String _style = "";
                _style = "position: relative; float: " + mm.Float + "; width: " + _width + "px;";

                this.GalleryBox2_admin.Attributes.Add("style", _style);
                this.GalleryBox2_holder.Attributes.Add("style", _style);

                BindGallery();

                // CONTENT

                if (mm.Visible == "true")
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
                this.GalleryBox2_holder.Visible = false;
            }
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
            RXServer.Modules.MediaModule mm = new RXServer.Modules.MediaModule(this.SitId, this.PagId, this.ModId);
            mm.ChangeOrderDown();
            mm.Save();

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
            RXServer.Modules.MediaModule mm = new RXServer.Modules.MediaModule(this.SitId, this.PagId, this.ModId);
            mm.ChangeOrderUp();
            mm.Save();

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

            RXServer.Web.Redirect.To("~/Default.aspx?PagId=" + this.PagId);
        }
        catch(Exception ex)
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

}
