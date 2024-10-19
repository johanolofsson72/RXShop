﻿using System;
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
using System.IO;
using Telerik.Web.UI;
using System.Drawing;
using System.Xml;

public partial class Modules_Boxes_StartBox_StartBox_Admin : System.Web.UI.Page
{
    String class_name = "Modules_Boxes_StartBox_StartBox_Admin";
    String ErrorList = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            this.SubMenu.Visible = true;
            HideErrors();
            BindMenuData();
            //BindMenuData2();

            this.CustomValidator1.ErrorMessage = "- " + RXMali.GetXMLNode("Error/upload_valid");

            switch (RXServer.Web.RequestValues.Page)
            {
                case "Module":                    
                    if (!Page.IsPostBack)
                    {
                        PopulateModels();
                        BindTextData();
                        this.Page_2.Visible = true;

                    }
                break;

                case "EditTab":
                if (!Page.IsPostBack)
                {
                    this.Page_3.Visible = true;
                    this.ltrHelpText.Text = RXMali.GetXMLHelpNode("StartBox/Add");

                    if (RXServer.Web.RequestValues.ViewId != "")
                    {
                        BindTabData(RXServer.Web.RequestValues.ViewId);
                    }
                }
                break;
                default:

                    if (!Page.IsPostBack)
                    {
                        PopulateModels();
                        BindTextData();
                        this.Page_2.Visible = true;
                    }

                break;

            }
            
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    public void BindTextData()
    {
        String function_name = "BindTextData";
        try
        {
            this.ltrHelpText.Text = RXMali.GetXMLHelpNode("StartBox/Text");
            RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);

            Int32 ModelId = 0;  // Default value
            Int32.TryParse(sm.ModelId, out ModelId);

            this.ddlModels.SelectedValue = ModelId.ToString();

            if (sm.Float == "right")
            {
                this.rbFloatRight.Checked = true;
            }
            else
            {
                this.rbFloatLeft.Checked = true;
            }
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    public void BindTabData(String eId)
    {
        String function_name = "BindTabData";
        try
        {
            Int32 obd_id = 0;
            Int32.TryParse(eId, out obd_id);

            RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item(obd_id);

            InternalLink.LinkId = i.Value17;

            this.txtHeader.Text = Server.HtmlDecode(i.Value2).Replace("`", "'");
            this.RadEditor1.Content = Server.HtmlDecode(i.Value4).Replace("`", "'");
            this.txtLinkName.Text = Server.HtmlDecode(i.Value16).Replace("`", "'");
            this.txtToolTip.Text = Server.HtmlDecode(i.Value13).Replace("`", "'");

            #region loadImage
            if (i.Value10 != "")
            {
                if (i.Value11 == ".gif" || i.Value11 == ".jpg" || i.Value11 == ".jpeg" || i.Value11 == ".png")
                {
                    String imgfile = Server.MapPath("~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + i.Value10);

                    if (File.Exists(imgfile))
                    {
                        // Retrieve the image.
                        Bitmap image1;

                        image1 = new Bitmap(imgfile, true);

                        Int32 width, height, newheight, newwidth;

                        width = 200;
                        height = 200;

                        newwidth = width;

                        if (image1.Width <= newwidth)
                        {
                            newwidth = image1.Width;
                        }

                        newheight = Convert.ToInt32((image1.Height * newwidth) / image1.Width);

                        if (newheight > image1.Height)
                        {
                            newwidth = Convert.ToInt32((image1.Width * height) / image1.Height);
                            newheight = height;
                        }

                        if (newheight > height)
                        {
                            float temp, temph, tempnh;
                            temph = height;
                            tempnh = newheight;
                            temp = temph / tempnh;
                            newwidth = Convert.ToInt32(newwidth * temp);
                            newheight = height;
                        }

                        this.imgContent.Visible = true;
                        this.imgContent.ImageUrl = "~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + i.Value10;
                        this.imgContent.Width = newwidth;
                        this.imgContent.Height = newheight;
                        this.hplZoomImg.Visible = true;
                        this.hplZoomImg.NavigateUrl = "~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + i.Value10;
                    }
                }
                else if (i.Value11 == ".swf")
                {
                    this.lblImgText.Text = "FlashFile (" + i.Value10 + ")";
                }
                else if (i.Value11 == ".flv")
                {
                    this.lblImgText.Text = "FlashVideo (" + i.Value10 + ")";
                }
                else
                {
                    this.lblImgText.CssClass = "Text11_red";
                    this.lblImgText.Text = "Invalid MediaFile! (" + i.Value10 + ")";
                }
            }

            #endregion

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    public void BindListData()
    {
        String function_name = "BindListData";
        try
        {
            this.ltrHelpText.Text = RXMali.GetXMLHelpNode("StartBox/List");
            String list = "";
            
            String Url = "";
            Int32 limit = 10;
            Int32 total = 0;
            Int32 index = RXServer.Web.RequestValues.Index;
            Int32 page = (index / limit) + 1;
            Int32 pages = 0;

            Url = RXMali.GetLastUrl(Request.Url.ToString());

            RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("TabText_" + RXServer.Web.RequestValues.ModId, LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, index, limit);
            RXServer.Modules.Base.List t = new RXServer.Modules.Base.List("TabText_" + RXServer.Web.RequestValues.ModId);
           

            total = t.Count();

            list += "<table cellspacing='0' cellpadding='5' style='width: 100%; border: solid 1px #CCCCCC;'>";

            if (total > 0)
            {
                list += "<tr style='background-color: #666666' class='Text11_white'>";
                list += "<td valign='top'>Header</td>";
                list += "<td valign='top' align='center'>Order</td>";
                list += "<td valign='top'>Delete</td>";
                list += "<td valign='top'>Edit</td>";

                list += "</tr>";

                Int32 counter = 0;

                foreach (RXServer.Modules.Base.List.Item item in cl)
                {
                    if ((counter % 2) == 0)
                    {
                        list += "<tr style='background-color: white; vertical-align: middle;'  class='Text11_gray'>";
                    }
                    else
                    {
                        list += "<tr style='background-color: #EFEFEF; vertical-align: middle;'  class='Text11_gray'>";
                    }
                    list += "<td valign='middle'><a href='StartBox_Admin.aspx?Page=EditTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "&amp;ViewId=" + item.Id.ToString() + "' class='underline'>" + item.Value13 + "</a></td>";
                    if (item.Value6 == "true")
                    {
                        list += "<td valign='middle' align='center'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_on.gif' title='Active' class='img_noborder' /></td>";
                    }
                    else
                    {
                        list += "<td valign='middle' align='center'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_off.gif' title='Unactive' class='img_noborder' /></td>";
                    }

                    list += "<td valign='middle' align='center'><a href='" + Request.Url.ToString() + "&Up=" + item.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_up.gif' alt='Move Up' class='img_noborder' /></a> / <a href='" + Request.Url.ToString() + "&Up=" + item.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_down.gif' alt='Move Down' class='img_noborder' /></a></td>";
                    list += "<td valign='middle'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&DelId=" + item.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this event( " + item.Value1 + " )?')\"><img src='../../../App_Themes/WebAdmin/Images/icon_bullet_delete.gif' class='img_noborder' /></a></td><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&DelId=" + item.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this item( " + item.Value1 + " )?')\">Delete</a></td></tr></table></td>";
                    list += "<td valign='middle'><table cellpadding='0' cellspacing='0'><tr><td><a href='StartBox_Admin.aspx?Page=EditTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "&amp;ViewId=" + item.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_edit.gif' class='img_noborder' /></a></td><td><a href='StartBox_Admin.aspx?Page=EditTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "&amp;ViewId=" + item.Id.ToString() + "'>&nbsp;Edit</a></td></tr></table></td>";
                    
                    list += "</tr>";
                    counter++;
                }
            }
            else
            {
                list += "<tr><td class='Text11_gray'>No Records was found.</td></tr>";
            }

            list += "</table>";

            #region Paging

            if (total > 0)
            {
                double t1, t2;
                t1 = total;
                t2 = limit;
                pages = (int)Math.Ceiling(t1 / t2);
                Url = Url.Replace("&Index=" + index, "");

                if (RXServer.Web.RequestValues.PagId > 0)
                {
                    Url = Url + "&PagId=" + RXServer.Web.RequestValues.PagId;
                }

                list += "<table cellspacing='0' cellpadding='0' style='width: 100%;'><tr>";
                list += "<td colspan='10' style='text-align: right' class='Text11_gray'><div style='position:relative; padding: 0px; margin: 0px; float: right'><table cellspacing='0' cellpadding='5'><tr>";
                list += "<td>Page " + page + "  of " + pages + "</td>";
                
                if (page > 1)
                {
                    list += "<td style='width:5px;'><a href='" + Url + "&Index=" + (index - limit) + "' class='paging_on'>«</a></td>";
                }
                else
                {
                    list += "<td style='width:5px;'>&nbsp;</td>";
                }

                if ((page) > 3)
                {
                    list += "<td><a href='" + Url + "&Index=" + 0 + "' class='paging_on'>1</a></td><td>...</td>";
                }

                for (int i = 0; i < pages; i++)
                {
                    if (i < page + 2 && i > (page - 4))
                    {
                        Int32 ix = i * limit;
                        if ((i + 1) == page)
                        {
                            list += "<td style='background-color: #CCCCCC;'>" + (i + 1) + "</td>";
                        }
                        else
                        {
                            list += "<td><a href='" + Url + "&Index=" + ix + "' class='paging_on'>" + (i + 1) + "</a></td>";
                        }
                    }
                }
                if ((page + 2) < pages)
                {
                    Int32 ix = (pages-1) * limit;
                    list += "<td>...</td><td><a href='" + Url + "&Index=" + ix + "' class='paging_on'>" + (pages) + "</a></td>";
                }


                if (page < pages)
                {
                    list += "<td style='width:5px;'><a href='" + Url + "&Index=" + (index + limit) + "' class='paging_on'>»</a></td>";
                }
                else
                {
                    list += "<td style='width:5px;'>&nbsp;</td>";
                }

                list += "<tr></table></div></td></tr></table>";
            }

            #endregion

            this.ltrEventsList.Text = list;

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    public void BindMenuData()
    {
        String function_name = "BindMenuData";
        try
        {
            String list = "";

            list += "<table cellspacing='0' cellpadding='0' style='border:0px;'>";
            list += "<tr>";
            list += "<tr><td style='width:10px; height:34px;'></td>";
            if (RXServer.Web.RequestValues.Page == "Module")
            {
                list += "<td><a href='StartBox_Admin.aspx?Page=Module&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_on'>Module</a></td>";
            }
            else
            {
                list += "<td><a href='StartBox_Admin.aspx?Page=Module&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_off'>Module</a></td>";
            }
            list += "<td style='width:20px;'></td>";


            RXServer.Modules.Base.List t = new RXServer.Modules.Base.List("StartBox_" + RXServer.Web.RequestValues.ModId);          

            Int32 count = 0;

            if (t.Count() > 0)
            {
                foreach (RXServer.Modules.Base.List.Item item in t)
                {
                    count++;
                    if (RXServer.Web.RequestValues.ViewId == item.Id.ToString())
                    {
                        list += "<td><a href='StartBox_Admin.aspx?Page=EditTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "&ViewId=" + item.Id.ToString() + "' class='submenu_on'>";
                        list += "Column " + count;  
                        list += "</a></td>";
                    }
                    else
                    {
                        list += "<td><a href='StartBox_Admin.aspx?Page=EditTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "&ViewId=" + item.Id.ToString() + "' class='submenu_off'>";
                        list += "Column " + count;  
                        list += "</a></td>";
                    }
                    list += "<td style='width:20px;'></td>";
                }
            }


            list += "</tr>";
            list += "</table>";

            this.ltrSubMenuList1.Text = list;
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    public void BindMenuData2()
    {
        String function_name = "BindMenuData2";
        try
        {
            String list = "";

            list += "<table cellspacing='0' cellpadding='0' style='border:0px;'>";
            list += "<tr>";
            list += "<td style='width:10px;'><td><img src='../../../App_Themes/WebAdmin/Images/submenu_divider.gif' class='img_noborder' /></td><td style='width:10px;'>";
            if (RXServer.Web.RequestValues.Page == "AddTab")
            {
                list += "<td><a href='StartBox_Admin.aspx?Page=AddTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='StartBox_Admin.aspx?Page=AddTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_on'>Add Tab</a></td>";
            }
            else
            {
                list += "<td><a href='StartBox_Admin.aspx?Page=AddTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='StartBox_Admin.aspx?Page=AddTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_off'>Add Tab</a></td>";
            }
            list += "<td style='width:20px; height:34px;'></td>";
            list += "</tr>";
            list += "</table>";

            this.ltrSubMenuList2.Text = list;
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
       
    protected void btnSaveData_Click(object sender, EventArgs e)
    {
        String function_name = "btnSaveData_Click";
        try
        {
            Boolean valid = true;
            String Errors = "";

            RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);

            if (this.rbFloatRight.Checked)
            {
                sm.Float = "right";
            }
            else
            {
                sm.Float = "left";
            }

            if (this.ddlModels.SelectedItem.Value == "0")
            {
                valid = false;
                Errors += " - Please select a size.";
                this.imgError1_2.Visible = true;
            }
            else
            {
                sm.ModelId = this.ddlModels.SelectedItem.Value;
            }

            sm.Save();

            if (valid)
            {
                this.lblScript.Text = "<script language='javascript'>returnToParent();</script>";
            }
            else
            {
                this.ErrorBox.Visible = true;
                this.ltrErrors.Text = Errors;
            }


        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    public void DeleteTab(String Id)
    {
        String function_name = "DeleteTab";
        try
        {
            Int32 obdId = 0;
            Int32.TryParse(Id, out obdId);
            if (obdId > 0)
            {
                RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item(obdId);
                i.Delete();
            }

            String Url;
            Url = RXMali.GetLastUrl(Request.Url.ToString());
            Url = Url.Replace("&DelId=" + Id, "");

            RXServer.Web.Redirect.To(Url);
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    public void HideErrors()
    {
        String function_name = "HideErrors";
        try
        {
            this.imgError1_1.Visible = false;
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }

    }

    protected void btnEditTab_Click(object sender, EventArgs e)
    {
        String function_name = "btnEditTab_Click";
        try
        {
            Boolean valid = true;
            string Url = "";

            Int32 obd_id = 0;

            Int32.TryParse(RXServer.Web.RequestValues.ViewId, out obd_id);

            if (obd_id > 0)
            {
                RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item(obd_id);

                i.Value2 = Server.HtmlEncode(this.txtHeader.Text).Replace("'", "`");
                i.Value4 = Server.HtmlEncode(this.RadEditor1.Content).Replace("'", "`");
                i.Value16 = Server.HtmlEncode(this.txtLinkName.Text).Replace("'", "`");
                i.Value17 = GetReadMoreLink().ToString();
                i.Value13 = Server.HtmlEncode(this.txtToolTip.Text).Replace("'", "`");

                #region UploadImage

                // Sets Module Width
                
                Int32 img_width = 203;


                String strFileName = "";
                String strFileName2 = "";
                String strFileExt = "";

                if (RadUpload1.InvalidFiles.Count > 0)
                {
                    this.ErrorBox.Visible = true;
                    ErrorList += " - " + RXMali.GetXMLNode("Error/media");
                    this.imgError1_1.Visible = true;
                    valid = false;
                }
                else
                {
                    if (this.RadUpload1.UploadedFiles.Count > 0)
                    {
                        String activeDir = "";
                        try
                        {
                            activeDir = Server.MapPath(@"../../../Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/");
                        }
                        catch (Exception ex)
                        {
                            this.ErrorBox.Visible = true;
                            ErrorList += ex;
                        }

                        if (!System.IO.Directory.Exists(activeDir))
                        {
                            System.IO.Directory.CreateDirectory(activeDir);
                        }
                        this.RadUpload1.TargetFolder = "~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId;

                        foreach (UploadedFile f1 in RadUpload1.UploadedFiles)
                        {
                            strFileName = f1.GetNameWithoutExtension().Trim() + "_" + DateTime.Today.Ticks.ToString();
                            strFileExt = f1.GetExtension().ToLower();
                            strFileName2 = strFileName + strFileExt;

                            f1.SaveAs(activeDir + strFileName2);

                            if (strFileExt == ".jpg" || strFileExt == ".gif" || strFileExt == ".jpeg" || strFileExt == ".png")
                            {
                                // Scale Image

                                System.Drawing.Image scaleImg = System.Drawing.Image.FromFile(activeDir + strFileName2);

                                if (scaleImg.Width != img_width)
                                {
                                    System.Drawing.Image newImg = null;
                                    newImg = RXMali.ScaleFixedWidthImage(scaleImg, img_width);
                                    newImg.Save(activeDir + "s_" + strFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                    newImg.Dispose();

                                    i.Value10 = "s_" + strFileName + ".jpg";
                                    i.Value11 = strFileExt;
                                }
                                else
                                {
                                    i.Value10 = strFileName2;
                                    i.Value11 = strFileExt;
                                }
                            }
                            else
                            {
                                i.Value10 = strFileName2;
                                i.Value11 = strFileExt;
                            }
                        }
                    }
                }

                #endregion


                i.Save();

                if (valid)
                {
                    Url = RXMali.GetLastUrl(Request.Url.ToString());

                    Url = Url.Replace("&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
                    Url = Url.Replace("EditTab", "Tab");

                    RXServer.Web.Redirect.To(Url);
                }
                else
                {
                    this.ltrErrors.Text = ErrorList;
                    this.ErrorBox.Visible = true;
                    BindTabData(RXServer.Web.RequestValues.ViewId);
                }
            }
            else
            {
                Url = RXMali.GetLastUrl(Request.Url.ToString());

                Url = Url.Replace("&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
                Url = Url.Replace("EditTab", "Tab");

                RXServer.Web.Redirect.To(Url);
            }
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    public void MoveUpItem(string id)
    {
        String function_name = "MoveUpItem";
        try
        {
            Int32 obd_id = 0;
            Int32.TryParse(id, out obd_id);

            RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item(obd_id);
            i.ChangeOrderDown();

            String Url = "";
            Url = RXMali.GetLastUrl(Request.Url.ToString());
            Url = Url.Replace("&Up=" + Request["Up"], "");

            RXServer.Web.Redirect.To(Url);
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    public void MoveDownItem(string id)
    {
        String function_name = "MoveDownItem";
        try
        {
            Int32 obd_id = 0;
            Int32.TryParse(id, out obd_id);

            RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item(obd_id);
            i.ChangeOrderUp();

            String Url = "";
            Url = RXMali.GetLastUrl(Request.Url.ToString());
            Url = Url.Replace("&Down=" + Request["Down"], "");

            RXServer.Web.Redirect.To(Url);
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void PopulateModels()
    {
        String function_name = "PopulateModels";
        try
        {
            Int32 modelId = 0;
            Int32.TryParse(RXServer.Web.RequestValues.Var, out modelId);

            this.ddlModels.Items.Add(new ListItem("Select a size", "0"));
            ////this.ddlModels.Items.Add(new ListItem("Small", "1"));
            //if (modelId > 1)
            //{
            //    this.ddlModels.Items.Add(new ListItem("Medium", "2"));
            //}
            //if (modelId > 2)
            //{
            //    this.ddlModels.Items.Add(new ListItem("Large", "3"));
            //}
            //if (modelId > 3)
            //{
                this.ddlModels.Items.Add(new ListItem("XLarge", "4"));
            //}
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private int GetReadMoreLink()
    {
        String function_name = "GetReadMoreLink";
        try
        {
            int returnValue = 0;
            DropDownList ddl1 = (DropDownList)RXServer.Lib.Common.FindControlRecursive(this, "ddlLevel1");
            DropDownList ddl2 = (DropDownList)RXServer.Lib.Common.FindControlRecursive(this, "ddlLevel2");
            DropDownList ddl3 = (DropDownList)RXServer.Lib.Common.FindControlRecursive(this, "ddlLevel3");
            DropDownList ddl4 = (DropDownList)RXServer.Lib.Common.FindControlRecursive(this, "ddlLevel4");
            DropDownList ddl5 = (DropDownList)RXServer.Lib.Common.FindControlRecursive(this, "ddlLevel5");

            if (ddl1 != null)
            {
                if (ddl1.SelectedItem.Value == "0")
                { return 0; }
                else if (ddl1.SelectedItem.Value != null)
                { returnValue = Convert.ToInt32(ddl1.SelectedItem.Value); }
            }
            if (ddl2 != null)
            {
                if (ddl2.SelectedItem.Value != "0")
                { returnValue = Convert.ToInt32(ddl2.SelectedItem.Value); }
            }
            if (ddl3 != null)
            {
                if (ddl3.SelectedItem.Value.ToString() != "0")
                {
                    if (ddl3.SelectedItem.Value != null)
                    { returnValue = Convert.ToInt32(ddl3.SelectedItem.Value); }
                }
            }
            if (ddl4 != null)
            {
                if (ddl4.SelectedItem.Value.ToString() != "0")
                {
                    if (ddl4.SelectedItem.Value != null)
                    { returnValue = Convert.ToInt32(ddl4.SelectedItem.Value); }
                }
            }
            if (ddl5 != null)
            {
                if (ddl5.SelectedItem.Value.ToString() != "0")
                {
                    if (ddl5.SelectedItem.Value != null)
                    { returnValue = Convert.ToInt32(ddl5.SelectedItem.Value); }
                }
            }
            return returnValue;
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
            return 0;
        }
    }

}


