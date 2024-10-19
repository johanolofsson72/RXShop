﻿using System;
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

public partial class Modules_Boxes_TextBox_TextBox : RXServer.Lib.RXBaseModule
{
    String class_name = "Modules_Boxes_TextBox_TextBox";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            if (RXServer.Auth.IsInRole("Admin"))
            {
                this.TextBox_admin.Visible = true;
                if (RXMali.GetLastUrl(this.Page.MasterPageFile) == "Template1.master")
                {
                    this.img_AdminEdit.OnClientClick = "javascript:showAdminTextBox(1," + this.PagId + "," + this.ModId + ",'4'); return false;";
                }
                else if (RXMali.GetLastUrl(this.Page.MasterPageFile) == "Template2.master")
                {
                    this.img_AdminEdit.OnClientClick = "javascript:showAdminTextBox(1," + this.PagId + "," + this.ModId + ",'3'); return false;";
                }
                else if (RXMali.GetLastUrl(this.Page.MasterPageFile) == "Template3.master")
                {
                    if (GetConPa() == "ContentPane1")
                    {
                        this.img_AdminEdit.OnClientClick = "javascript:showAdminTextBox(1," + this.PagId + "," + this.ModId + ",'2'); return false;";
                    }
                    else
                    {
                        this.img_AdminEdit.OnClientClick = "javascript:showAdminTextBox(1," + this.PagId + "," + this.ModId + ",'1'); return false;";
                    }
                }
                else if (RXMali.GetLastUrl(this.Page.MasterPageFile) == "Template4.master")
                {
                    if (GetConPa() == "ContentPane1")
                    {
                        this.img_AdminEdit.OnClientClick = "javascript:showAdminTextBox(1," + this.PagId + "," + this.ModId + ",'1'); return false;";
                    }
                    else
                    {
                        this.img_AdminEdit.OnClientClick = "javascript:showAdminTextBox(1," + this.PagId + "," + this.ModId + ",'3'); return false;";
                    }
                }
                else
                {
                    this.img_AdminEdit.OnClientClick = "javascript:showAdminTextBox(1," + this.PagId + "," + this.ModId + ",'3'); return false;";
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
            RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(this.SitId, this.PagId, this.ModId);

            if (tm.Visible == "true" || RXServer.Auth.IsInRole("Admin"))
            {
                // Sets Module Width

                Int32 ModelId = 0;
                Int32.TryParse(tm.ModelId, out ModelId);
                Int32 _width = RXMali.GetModelSize(ModelId.ToString());
                String _float = "";

                if (tm.Float == "")
                {
                    _float = "left";
                }
                else
                {
                    _float = tm.Float;
                }

                String _style = "";
                String _style2 = "";

                _style = "position: relative; float: " + _float + "; width: " + _width + "px; padding-bottom: 20px;";
                _style2 = "position: relative; float: " + _float + "; width: " + _width + "px;";

                this.TextBox_admin.Attributes.Add("style", _style2);
                this.TextBox_holder.Attributes.Add("style", _style);

                String fontsize_1 = "";
                String fontsize_2 = "";

                // ONLY FOR NEWSITEM -------------------------

                if (tm.Text10 == "NewsItem")
                {
                    this.img_AdminDelete.Visible = false;
                }

                // -------------------------------------------

                if (tm.ExtraFont == "true")
                {
                    this.text_size.Visible = true;
                    //this.ltrFontLoader.Visible = true;
                    #region fontsize

                    this.imbLargeText.ImageUrl = "~/Images/Modules/Boxes/fs_large.gif";
                    this.imbMediumText.ImageUrl = "~/Images/Modules/Boxes/fs_medium.gif";
                    this.imbSmallText.ImageUrl = "~/Images/Modules/Boxes/fs_small.gif";

                    //this.imbSmallText.PostBackUrl = RXServer.Lib.Common.Dynamic.GetFriendlyUrl(this.PagId);
                    //this.imbMediumText.PostBackUrl = RXServer.Lib.Common.Dynamic.GetFriendlyUrl(this.PagId);
                    //this.imbLargeText.PostBackUrl = RXServer.Lib.Common.Dynamic.GetFriendlyUrl(this.PagId);
                                        
                    if (Session != null)
                    {
                        if (Session["font_" + this.ModId] != null)
                        {
                            switch (Session["font_" + this.ModId].ToString())
                            {

                                case "0":

                                    fontsize_1 = "";
                                    fontsize_2 = "";

                                    break;

                                case "1":

                                    fontsize_1 = "<big>";
                                    fontsize_2 = "</big>";

                                    break;

                                case "2":

                                    fontsize_1 = "<big><big>";
                                    fontsize_2 = "</big></big>";

                                    break;

                                default:

                                    fontsize_1 = "";
                                    fontsize_2 = "";

                                    break;
                            }
                        }
                    }

                    #endregion
                    
                //    string font_html = "";


                //    font_html += "<a style=\"cursor:pointer\" onclick=\"SetFontSize('small');ChangeFontSize('" + this.lblHeader.ClientID + ";" + this.lblIngress.ClientID + ";" + this.lblText.ClientID + "');\"><img src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/fs_small.gif' alt='Normal size' title='Normal size' /></a>";
                //    font_html += "<a style=\"cursor:pointer\" onclick=\"SetFontSize('medium');ChangeFontSize('" + this.lblHeader.ClientID + ";" + this.lblIngress.ClientID + ";" + this.lblText.ClientID + "');\"><img src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/fs_medium.gif' alt='Large size' title='Large size' /></a>";
                //    font_html += "<a style=\"cursor:pointer\" onclick=\"SetFontSize('large');ChangeFontSize('" + this.lblHeader.ClientID + ";" + this.lblIngress.ClientID + ";" + this.lblText.ClientID + "');\"><img src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/fs_large.gif' alt='Extra Large size' title='Extra Large size' /></a>";

                //    this.ltrFontResizer.Text = font_html;
                //    this.ltrFontLoader.Text += "<script language='javascript' type='text/javascript'>ChangeFontSize('" + this.lblHeader.ClientID + ";" + this.lblIngress.ClientID + ";" + this.lblText.ClientID + "');</script>";
                }

                if (tm.Text3 == "")
                {
                    this.IngressText.Visible = false;
                }
                else
                {
                    this.IngressText.Visible = true;
                }

                this.TextBox_holder.Visible = true;



                this.lblHeader.Text = fontsize_1 + Server.HtmlDecode(tm.Text2).Replace("`", "'") + fontsize_2;
                this.lblIngress.Text = fontsize_1 + Server.HtmlDecode(tm.Text3).Replace("`", "'") + fontsize_2;
                this.lblText.Text = fontsize_1 + EE.EncodeEmails(Server.HtmlDecode(tm.Text4)).Replace("`", "'") + fontsize_2;
                                
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


                if (tm.ExtraSocial == "true")
                {
                    this.TextBox_social.Visible = true;
                    this.imbAdminSocial.ImageUrl = "~/App_Themes/WebAdmin/Images/adminbar_share_on.gif";
                    this.imbAdminSocial.ToolTip = "Disable Social Links"; 

                    #region SpecialBox

                        String html = "";

                        html += "<table cellpadding='0' cellspacing='0'><tr>";
                        html += "<td>";
                        html += "<a class='social_link' style='cursor:pointer;' onclick=\"javascript:TellaFriendClick('" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "','" + this.PagId + "'); return false; \" >";
                        html += "<img name='tellfriend_img" + this.ModId + "' src=\"" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/special_tellfriend_off.gif\" onmouseover=\"this.src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/special_tellfriend_on.gif'\" onmouseout=\"this.src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/special_tellfriend_off.gif'\" class=\"img_noborder\" alt=\"" + RXMali.GetXMLNode("Modules/PopUp/Tellafriend/link") + "\"  title=\"" + RXMali.GetXMLNode("Modules/PopUp/Tellafriend/link") + "\" /></a>";
                        html += "</td>";
                        html += "<td style='font-size:1px; width:6px;'>&nbsp;</td>";
                        html += "<td>";
                        html += "<a class='social_link' style='cursor:pointer;' onclick=\"javascript:TellaFriendClick('" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "','" + this.PagId + "'); return false; \" onmouseover=\"document['tellfriend_img" + this.ModId + "'].src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/special_tellfriend_on.gif'\" onmouseout=\"document['tellfriend_img" + this.ModId + "'].src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/special_tellfriend_off.gif'\" >" + RXMali.GetXMLNode("Modules/PopUp/Tellafriend/link") + "</a>";
                        html += "</td>";
                        html += "<td style='font-size:1px; width:10px;'>&nbsp;</td>";
                        html += "<td>";
                        html += "<a class='social_link' style='cursor:pointer;' onclick=\"javascript:PrintClick('" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "','TextBox','" + this.ModId + "','" + this.PagId + "'); return false;\" >";
                        html += "<img name='print_img" + this.ModId + "' src=\"" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/special_print_off.gif\" onmouseover=\"this.src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/special_print_on.gif'\" onmouseout=\"this.src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/special_print_off.gif'\" class=\"img_noborder\" alt=\"" + RXMali.GetXMLNode("Modules/Social/Tooltip/print") + "\"  title=\"" + RXMali.GetXMLNode("Modules/Social/Tooltip/print") + "\" /></a>";
                        html += "</td>";
                        html += "<td style='font-size:1px; width:6px;'>&nbsp;</td>";
                        html += "<td>";
                        html += "<a class='social_link' style='cursor:pointer;' onclick=\"javascript:PrintClick('" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "','TextBox','" + this.ModId + "','" + this.PagId + "'); return false;\" onmouseover=\"document['print_img" + this.ModId + "'].src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/special_print_on.gif'\" onmouseout=\"document['print_img" + this.ModId + "'].src='" + RXServer.Lib.Common.Dynamic.CreateUrlPrefix() + "Images/Modules/Boxes/special_print_off.gif'\" >" + RXMali.GetXMLNode("Common/print") + "</a>";
                        html += "</td>";

                        html += "</tr></table>";

                        this.ltrSpecial.Text = html;

                        #endregion;

                }
                else
                {
                    this.TextBox_social.Visible = false;
                    this.imbAdminSocial.ImageUrl = "~/App_Themes/WebAdmin/Images/adminbar_share_off.gif";
                    this.imbAdminSocial.ToolTip = "Enable Social Links";
                }

            }
            else
            {
                this.TextBox_holder.Visible = false;
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
            RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(this.SitId, this.PagId, this.ModId);
            tm.ChangeOrderDown();
            tm.Save();

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
            RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(this.SitId, this.PagId, this.ModId);
            tm.ChangeOrderUp();
            tm.Save();

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
            RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(this.SitId, this.PagId, this.ModId);
            if (tm.Visible == "true")
            {
                tm.Visible = "false";
            }
            else
            {
                tm.Visible = "true";
            }
            tm.Save();

            RXServer.Web.Redirect.To("~/Default.aspx?PagId=" + this.PagId);
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

            RXServer.Web.Redirect.To("~/Default.aspx?PagId=" + this.PagId);
        }
        catch(Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void imbMediumText_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbMediumText_Click";
        try
        {
            Session["font_" + this.ModId] = "1";
            bindData();
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void imbSmallText_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbSmallText_Click";
        try
        {
            Session["font_" + this.ModId] = "0";
            bindData();
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void imbLargeText_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbLargeText_Click";
        try
        {
            Session["font_" + this.ModId] = "2";
            bindData();
        }
        catch (Exception ex)
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
