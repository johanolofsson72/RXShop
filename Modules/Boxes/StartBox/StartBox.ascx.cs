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

public partial class Modules_Boxes_StartBox_StartBox : RXServer.Lib.RXBaseModule
{
    String class_name = "Modules_Boxes_StartBox_StartBox";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            if (RXServer.Auth.IsInRole("Admin"))
            {
                this.StartBox_admin.Visible = true;
                if (RXMali.GetLastUrl(this.Page.MasterPageFile) == "Template3.master")
                {
                    this.img_AdminEdit.OnClientClick = "javascript:showAdminStartBox(1," + this.PagId + "," + this.ModId + ",'2'); return false;";
                }
                else
                {
                    this.img_AdminEdit.OnClientClick = "javascript:showAdminStartBox(1," + this.PagId + "," + this.ModId + ",'4'); return false;";
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
            RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(this.SitId, this.PagId, this.ModId);

            if (sm.Visible == "true" || RXServer.Auth.IsInRole("Admin"))
            {

                this.StartBox_holder.Visible = true;

                // Sets Module Width

                Int32 ModelId = 0;
                Int32.TryParse(sm.ModelId, out ModelId);
                Int32 _width = RXMali.GetModelSize(ModelId.ToString());

                String _style = "";
                String _float = "";

                if (sm.Float == "")
                {
                    _float = "left";
                }
                else
                {
                    _float = sm.Float;
                }

                if (ModelId == 0)
                {
                    _width = 900;
                }

                _style = "position: relative; float: " + _float + "; width: " + _width + "px;";

                this.StartBox_admin.Attributes.Add("style", _style);
                this.StartBox_holder.Attributes.Add("style", _style);

                BindStartData();

                // CONTENT

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

            }
            else
            {
                this.StartBox_holder.Visible = false;
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

    private void BindStartData()
    {
        String function_name = "BindStartData";
        try
        {


            RXServer.Modules.Base.List t = new RXServer.Modules.Base.List("StartBox_" + this.ModId);          

            Int32 count = 0;

            foreach (RXServer.Modules.Base.List.Item item in t)
            {
                count++;

                Int32 link = 0;
                Int32.TryParse(item.Value17, out link);

                // Media

                String showMedia = "";
                //String mediafile = RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Upload/Pages/" + this.PagId + "/" + this.ModId + "/" + item.Value10;
                String mediafile = "Upload/Pages/" + this.PagId + "/" + this.ModId + "/" + item.Value10;
                String mediafile2 = "Upload/Pages/" + this.PagId + "/" + this.ModId + "/" + item.Value10;

                if (item.Value11 == ".flv")
                {
                    String FlashId = item.Value10;
                    FlashId = FlashId.Replace(".flv", "");
                    showMedia += RXMali.GetFLVCode(RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "mediaplayer.swf", mediafile2, FlashId, 205, 205);
                }
                else if (item.Value11 == ".swf")
                {
                    String FlashId = item.Value10;
                    FlashId = FlashId.Replace(".swf", "");
                    showMedia += RXMali.GetFlashCode(mediafile, FlashId, 205, 205);
                }
                else if (item.Value11 == ".gif" || item.Value11 == ".jpeg" || item.Value11 == ".jpg" || item.Value11 == ".png")
                {

                    if (link > 0)
                    {
                        String path = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
                        showMedia += "<a href='" + path + "Default.aspx?PagId=" + link + "'>";
                    }

                    showMedia += "<img src='" + mediafile + "' style='width: " + 203 + "px;' title='" + item.Value13 + "' alt='" + item.Value13 + "'/>";

                    if (link > 0)
                    {
                        showMedia += "</a>";
                    }

                }

                if (count == 1)
                {
                    this.lblHeader1.Text = Server.HtmlDecode(item.Value2).Replace("`", "'");
                    this.lblText1.Text = EE.EncodeEmails(RXMali.Substring(Server.HtmlDecode(item.Value4), 90)).Replace("`", "'");
                    //this.hplReadMore1.NavigateUrl = RXServer.Lib.Common.Dynamic.GetFriendlyUrl(link);
                    this.hplReadMore1.NavigateUrl = "~/Default.aspx?PagId=" + link;
                    this.hplReadMore1.Text = Server.HtmlDecode(item.Value16).Replace("`", "'");
                    
                   this.ltrMedia1.Text = showMedia;

                }
                if (count == 2)
                {
                    this.lblHeader2.Text = Server.HtmlDecode(item.Value2).Replace("`", "'");
                    this.lblText2.Text = EE.EncodeEmails(RXMali.Substring(Server.HtmlDecode(item.Value4), 90)).Replace("`", "'");

                    this.hplReadMore2.NavigateUrl = "~/Default.aspx?PagId=" + link;
                    this.hplReadMore2.Text = Server.HtmlDecode(item.Value16).Replace("`", "'");

                    this.ltrMedia2.Text = showMedia;
                }
                if (count == 3)
                {
                    this.lblHeader3.Text = Server.HtmlDecode(item.Value2).Replace("`", "'");
                    this.lblText3.Text = EE.EncodeEmails(RXMali.Substring(Server.HtmlDecode(item.Value4), 90)).Replace("`", "'");
                    this.hplReadMore3.NavigateUrl = "~/Default.aspx?PagId=" + link;
                    this.hplReadMore3.Text = Server.HtmlDecode(item.Value16).Replace("`", "'");

                    this.ltrMedia3.Text = showMedia;
                }
                if (count == 4)
                {
                    this.lblHeader4.Text = Server.HtmlDecode(item.Value2).Replace("`", "'");
                    this.lblText4.Text = EE.EncodeEmails(RXMali.Substring(Server.HtmlDecode(item.Value4), 90)).Replace("`", "'");
                    this.hplReadMore4.NavigateUrl = "~/Default.aspx?PagId=" + link;
                    this.hplReadMore4.Text = Server.HtmlDecode(item.Value16).Replace("`", "'");


                    this.ltrMedia4.Text = showMedia;
                }
            }
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
  
}
