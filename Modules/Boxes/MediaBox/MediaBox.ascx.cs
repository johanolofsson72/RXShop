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

public partial class Modules_Boxes_MediaBox_MediaBox : RXServer.Lib.RXBaseModule
{
    String class_name = "Modules_Boxes_MediaBox_MediaBox";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            if (RXServer.Auth.IsInRole("Admin"))
            {


                this.MediaBox_admin.Visible = true;

                if (RXMali.GetLastUrl(this.Page.MasterPageFile) == "Template1.master")
                {
                    this.img_AdminEdit.OnClientClick = "javascript:showAdminMediaBox(1," + this.PagId + "," + this.ModId + ",'4'); return false;";
                }
                else if (RXMali.GetLastUrl(this.Page.MasterPageFile) == "Template2.master")
                {
                    this.img_AdminEdit.OnClientClick = "javascript:showAdminMediaBox(1," + this.PagId + "," + this.ModId + ",'3'); return false;";
                }
                else if (RXMali.GetLastUrl(this.Page.MasterPageFile) == "Template3.master")
                {
                    if (GetConPa() == "ContentPane1")
                    {
                        this.img_AdminEdit.OnClientClick = "javascript:showAdminMediaBox(1," + this.PagId + "," + this.ModId + ",'2'); return false;";
                    }
                    else
                    {
                        this.img_AdminEdit.OnClientClick = "javascript:showAdminMediaBox(1," + this.PagId + "," + this.ModId + ",'1'); return false;";
                    }
                }
                else if (RXMali.GetLastUrl(this.Page.MasterPageFile) == "Template4.master")
                {
                    if (GetConPa() == "ContentPane1")
                    {
                        this.img_AdminEdit.OnClientClick = "javascript:showAdminMediaBox(1," + this.PagId + "," + this.ModId + ",'1'); return false;";
                    }
                    else
                    {
                        this.img_AdminEdit.OnClientClick = "javascript:showAdminMediaBox(1," + this.PagId + "," + this.ModId + ",'3'); return false;";
                    }
                }
                else
                {
                    this.img_AdminEdit.OnClientClick = "javascript:showAdminMediaBox(1," + this.PagId + "," + this.ModId + ",'3'); return false;";
                }

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
            String showMedia = "";
            String social_img = "";
            RXServer.Modules.MediaModule mm = new RXServer.Modules.MediaModule(this.SitId, this.PagId, this.ModId);

            if (mm.Visible == "true" || RXServer.Auth.IsInRole("Admin"))
            {

                this.MediaBox_holder.Visible = true;

                if (mm.Media != "" && mm.MediaVisible == "true")
                {

                    // Sets Module Width

                    Int32 ModelId = 0;
                    Int32.TryParse(mm.ModelId, out ModelId);
                    Int32 _width = RXMali.GetModelSize(ModelId.ToString());

                    String _style = "";
                    _style = "position: relative; float: " + mm.Float + "; width: " + _width + "px; ";

                    this.MediaBox_admin.Attributes.Add("style", _style);
                    this.MediaBox_holder.Attributes.Add("style", _style);


                    String mediafile = RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Upload/Pages/" + this.PagId + "/" + this.ModId + "/" + mm.Media;
                    String mediafile2 = "Upload/Pages/" + this.PagId + "/" + this.ModId + "/" + mm.Media;

                    if (mm.MediaType == ".flv")
                    {
                        String FlashId = mm.Media;
                        FlashId = FlashId.Replace(".flv", "");
                        showMedia += RXMali.GetFLVCode(RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "mediaplayer.swf", mediafile2, FlashId, _width, RXMali.GetMediaHeight(mm.ModelId));
                    }
                    else if(mm.MediaType == ".swf")
                    {
                        String FlashId = mm.Media;
                        FlashId = FlashId.Replace(".swf", "");
                        showMedia += RXMali.GetFlashCode(mediafile, FlashId, _width, RXMali.GetMediaHeight(mm.ModelId));
                    }
                    else if(mm.MediaType == ".gif" || mm.MediaType == ".jpeg" || mm.MediaType == ".jpg" || mm.MediaType == ".png")
                    {
                        Int32 link = 0;
                        Int32.TryParse(mm.ReadMoreLinkId, out link);
                        if (link > 0)
                        {
                            String path = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
                            showMedia += "<a href='" + path + "Default.aspx?PagId=" + link + "'>";
                        }

                        showMedia += "<img src='" + mediafile + "' style='width: " + _width + "px;' title='" + mm.MediaToolTip + "' alt='" + mm.MediaToolTip + "'/>";
                        social_img = RXMali.GetDomainUrl(Request.Url.ToString()) + mediafile;
                        
                        if (link > 0)
                        {
                            showMedia += "</a>";
                        }

                    }

                    this.ltrPuffImg.Text = showMedia;
                }

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
                this.MediaBox_holder.Visible = false;
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

            RXServer.Web.Redirect.To(RXMali.GetLastUrl(Request.Url.ToString()));
        }
        catch(Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private String GetConPa()
    {
        String function_name = "GetConPa";
        try
        {
            RXServer.Modules.MediaModule mm = new RXServer.Modules.MediaModule(this.SitId, this.PagId, this.ModId);
            return mm.ContentPane;
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
            return "";
        }
    }

}
