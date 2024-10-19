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

public partial class Modules_Boxes_ProductBrowser_ProductBrowser_Admin : System.Web.UI.Page
{
    String class_name = "Modules_Boxes_ProductBrowser_ProductBrowser_Admin";
    String ErrorList = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            this.SubMenu.Visible = true;
            HideErrors();
            BindMenuData();
            BindMenuData2();

            this.CustomValidator1.ErrorMessage = "- " + RXMali.GetXMLNode("Error/upload_valid");
            this.CustomValidator2.ErrorMessage = "- " + RXMali.GetXMLNode("Error/upload_valid");

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

                case "AddImage":
                    if (!Page.IsPostBack)
                    {
                        this.Page_4.Visible = true;
                        this.ltrHelpText.Text = RXMali.GetXMLHelpNode("ProductBrowser/Add");
                    }
                break;

                case "EditImage":
                if (!Page.IsPostBack)
                {
                    this.Page_3.Visible = true;
                    this.ltrHelpText.Text = RXMali.GetXMLHelpNode("ProductBrowser/Edit");

                    if (RXServer.Web.RequestValues.ViewId != "")
                    {
                        BindGalleryData(RXServer.Web.RequestValues.ViewId);
                    }
                }
                break;

                case "Tabs":
                if (!Page.IsPostBack)
                {
                    this.Page_5.Visible = true;
                    //this.ltrHelpText.Text = RXMali.GetXMLHelpNode("ProductBrowser/Tabs");

                    if (RXServer.Web.RequestValues.DelId != "")
                    {
                        DeleteItem(RXServer.Web.RequestValues.DelId);
                    }
                    else
                    {
                        BindListDataTabs();
                    }
                    if (Request["Up"] != null)
                    {
                        MoveUpItem(Request["Up"]);
                    }
                    if (Request["Down"] != null)
                    {
                        MoveDownItem(Request["Down"]);
                    }
                }
                break;

                case "AddTab":
                if (!Page.IsPostBack)
                {
                    this.Page_6.Visible = true;
                    //this.ltrHelpText.Text = RXMali.GetXMLHelpNode("ProductBrowser/Tabs");

                    //if (RXServer.Web.RequestValues.ViewId != "")
                    //{
                    //    BindGalleryData(RXServer.Web.RequestValues.ViewId);
                    //}
                }
                break;

                case "EditTab":
                if (!Page.IsPostBack)
                {
                    this.Page_7.Visible = true;
                    //this.ltrHelpText.Text = RXMali.GetXMLHelpNode("ProductBrowser/Tabs");

                    if (RXServer.Web.RequestValues.ViewId != "")
                    {
                        BindTabData(RXServer.Web.RequestValues.ViewId);
                    }
                }
                break;


                case "AddChoice":
                if (!Page.IsPostBack)
                {
                    this.Page_8.Visible = true;
                }
                break;

                case "EditChoice":
                if (!Page.IsPostBack)
                {
                    this.Page_9.Visible = true;

                    if (RXServer.Web.RequestValues.ViewId != "")
                    {
                        BindChoiceData(RXServer.Web.RequestValues.ViewId);
                    }
                }
                break;

                case "Choices":
                if (!Page.IsPostBack)
                {
                    this.Page_10.Visible = true;

                    if (RXServer.Web.RequestValues.DelId != "")
                    {
                        DeleteItem(RXServer.Web.RequestValues.DelId);
                    }
                    else
                    {
                        BindChoicesData();
                    }
                    if (Request["Up"] != null)
                    {
                        MoveUpItem(Request["Up"]);
                    }
                    if (Request["Down"] != null)
                    {
                        MoveDownItem(Request["Down"]);
                    }
                }
                break;

                default:
                    if (!Page.IsPostBack)
                    {
                        this.Page_1.Visible = true;
                        if (RXServer.Web.RequestValues.DelId != "")
                        {
                            DeleteItem(RXServer.Web.RequestValues.DelId);
                        }
                        else
                        {
                            BindListData();
                        }
                    }
                    if (Request["Up"] != null)
                    {
                        MoveUpItem(Request["Up"]);
                    }
                    if (Request["Down"] != null)
                    {
                        MoveDownItem(Request["Down"]);
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
            this.ltrHelpText.Text = RXMali.GetXMLHelpNode("ProductBrowser/Text");
            RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);

            Int32 ModelId = 0;  // Default value
            Int32.TryParse(sm.ModelId, out ModelId);

            txtChoicesTitle.Text = Server.HtmlDecode(sm.Text1);
            txtHeader.Text = Server.HtmlDecode(sm.Text2);
            txtText.Content = Server.HtmlDecode(sm.Text3);
            txtProductName.Text = Server.HtmlDecode(sm.Text4);
            txtProductPrice.Text = Server.HtmlDecode(sm.Text5);
            txtProductVat.Text = Server.HtmlDecode(sm.Text6);
            txtProductStockAmount.Text = Server.HtmlDecode(sm.Text7);
            txtOldPrice.Text = Server.HtmlDecode(sm.Text8);
            txtProductId.Text = Server.HtmlDecode(sm.Text9);
            txtCategoryText.Content = Server.HtmlDecode(sm.Text10);

            //this.ddlModels.SelectedValue = ModelId.ToString();

            //if (sm.Float == "right")
            //{
            //    this.rbFloatRight.Checked = true;
            //}
            //else
            //{
            //    this.rbFloatLeft.Checked = true;
            //}
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    public void BindChoicesData()
    {
        String function_name = "BindChoicesData";
        try
        {
            this.ltrHelpText.Text = RXMali.GetXMLHelpNode("ProductBrowser/List");
            String list = "";

            String Url = "";
            Int32 limit = 10;
            Int32 total = 0;
            Int32 index = RXServer.Web.RequestValues.Index;
            Int32 page = (index / limit) + 1;
            Int32 pages = 0;

            Url = RXMali.GetLastUrl(Request.Url.ToString());

            RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("Choices_" + RXServer.Web.RequestValues.ModId, LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, index, limit);
            RXServer.Modules.Base.List t = new RXServer.Modules.Base.List("Choices_" + RXServer.Web.RequestValues.ModId);


            total = t.Count();

            list += "<table cellspacing='0' cellpadding='5' style='width: 100%; border: solid 1px #CCCCCC;'>";

            if (total > 0)
            {
                list += "<tr style='background-color: #666666' class='Text11_white'>";
                list += "<td valign='top'></td>";
                list += "<td valign='top'>Name</td>";
                list += "<td valign='top' align='center'>Visible</td>";
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
                    list += "<td valign='middle'>";
                    list += item.Value1;

                    list += "</td>";
                    list += "<td valign='middle'>" + item.Value13 + "</td>";
                    if (item.Value6 == "true")
                    {
                        list += "<td valign='middle' align='center'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_on.gif' title='Active' class='img_noborder' /></td>";
                    }
                    else
                    {
                        list += "<td valign='middle' align='center'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_off.gif' title='Unactive' class='img_noborder' /></td>";
                    }

                    list += "<td valign='middle' align='center'><a href='" + Request.Url.ToString() + "&Up=" + item.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_up.gif' alt='Move Up' class='img_noborder' /></a> / <a href='" + Request.Url.ToString() + "&Down=" + item.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_down.gif' alt='Move Down' class='img_noborder' /></a></td>";
                    list += "<td valign='middle'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&DelId=" + item.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this item( " + item.Value13 + " )?')\"><img src='../../../App_Themes/WebAdmin/Images/icon_bullet_delete.gif' class='img_noborder' /></a></td><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&DelId=" + item.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this item( " + item.Value13 + " )?')\">Delete</a></td></tr></table></td>";
                    list += "<td valign='middle'><table cellpadding='0' cellspacing='0'><tr><td><a href='ProductBrowser_Admin.aspx?Page=EditChoice&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;ViewId=" + item.Id.ToString() + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_edit.gif' class='img_noborder' /></a></td><td><a href='ProductBrowser_Admin.aspx?Page=EditChoice&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;ViewId=" + item.Id.ToString() + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'>&nbsp;Edit</a></td></tr></table></td>";

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

                //if (RXServer.Web.RequestValues.PagId > 0)
                //{
                //    Url = Url + "&PagId=" + RXServer.Web.RequestValues.PagId;
                //}

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
                    Int32 ix = (pages - 1) * limit;
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

            this.ltrChoices.Text = list;

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    public void BindGalleryData(String eId)
    {
        String function_name = "BindGalleryData";
        try
        {
            Int32 obd_id = 0;
            Int32.TryParse(eId, out obd_id);

            RXServer.Modules.Base.List.Item e = new LiquidCore.List.Item(obd_id);

            this.txtImageName.Text = Server.HtmlDecode(e.Value13).Replace("`", "'");
            this.RadEditor2.Content = Server.HtmlDecode(e.Value25).Replace("`", "'");

            if (e.Value6 == "false")
            {
                this.cbShowImage.Checked = false;
            }
            else
            {
                this.cbShowImage.Checked = true;
            }

            #region loadImage
            if (e.Value10 != "")
            {
                this.imgContent.Visible = true;

                if (e.Value11 == ".gif" || e.Value11 == ".jpg" || e.Value11 == ".jpeg" || e.Value11 == ".png" || e.Value11 == ".JPG" || e.Value11 == ".JPEG" || e.Value11 == ".PNG" || e.Value11 == ".GIF")
                {
                    String imgfile = Server.MapPath("~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + e.Value10);

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

                        this.imgContent.ImageUrl = "~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + e.Value10;
                        this.imgContent.Width = newwidth;
                        this.imgContent.Height = newheight;
                        this.hplZoomImg.Visible = true;
                        this.hplZoomImg.NavigateUrl = "~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + e.Value10;
                    }
                }
                else
                {
                    this.lblImgText.CssClass = "Text11_red";
                    this.lblImgText.Text = "Invalid MediaFile! (" + e.Value7 + ")";
                }
            }
            else
            {
                this.imgContent.Visible = false;
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
            this.ltrHelpText.Text = RXMali.GetXMLHelpNode("ProductBrowser/List");
            String list = "";
            
            String Url = "";
            Int32 limit = 10;
            Int32 total = 0;
            Int32 index = RXServer.Web.RequestValues.Index;
            Int32 page = (index / limit) + 1;
            Int32 pages = 0;

            Url = RXMali.GetLastUrl(Request.Url.ToString());

            //RXServer.Modules.CalendarList cl = new RXServer.Modules.CalendarList("Calendar_" + RXServer.Web.RequestValues.ModId.ToString());

            RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("Gallery2_" + RXServer.Web.RequestValues.ModId, LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, index, limit);
            RXServer.Modules.Base.List t = new RXServer.Modules.Base.List("Gallery2_" + RXServer.Web.RequestValues.ModId);
           

            total = t.Count();

            list += "<table cellspacing='0' cellpadding='5' style='width: 100%; border: solid 1px #CCCCCC;'>";

            if (total > 0)
            {
                list += "<tr style='background-color: #666666' class='Text11_white'>";
                list += "<td valign='top'>View</td>";
                list += "<td valign='top'>Image Name</td>";
                list += "<td valign='top' align='center'>Visible</td>";
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
                    list += "<td valign='middle'>";

                    #region loadImage
                    if (item.Value10 != "")
                    {
                        if (item.Value11 == ".gif" || item.Value11 == ".jpg" || item.Value11 == ".jpeg" || item.Value11 == ".png")
                        {
                            String imgfile = Server.MapPath("~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + item.Value10);

                            if (File.Exists(imgfile))
                            {
                                // Retrieve the image.
                                Bitmap image1;

                                image1 = new Bitmap(imgfile, true);

                                Int32 width, height, newheight, newwidth;

                                width = 30;
                                height = 30;

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

                                list += "<img src='" + "../../../Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + item.Value10 + "' style='width: " + newwidth + "px; height: " + newheight + "px; border: solid 1px black;' />";

                            }
                        }

                    }

                    #endregion

                    list += "</td>";
                    list += "<td valign='middle'>" + item.Value13 + "</td>";
                    if (item.Value6 == "true")
                    {
                        list += "<td valign='middle' align='center'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_on.gif' title='Active' class='img_noborder' /></td>";
                    }
                    else
                    {
                        list += "<td valign='middle' align='center'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_off.gif' title='Unactive' class='img_noborder' /></td>";
                    }

                    list += "<td valign='middle' align='center'><a href='" + Request.Url.ToString() + "&Up=" + item.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_up.gif' alt='Move Up' class='img_noborder' /></a> / <a href='" + Request.Url.ToString() + "&Down=" + item.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_down.gif' alt='Move Down' class='img_noborder' /></a></td>";
                    list += "<td valign='middle'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&DelId=" + item.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this event( " + item.Value13 + " )?')\"><img src='../../../App_Themes/WebAdmin/Images/icon_bullet_delete.gif' class='img_noborder' /></a></td><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&DelId=" + item.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this item( " + item.Value13 + " )?')\">Delete</a></td></tr></table></td>";
                    list += "<td valign='middle'><table cellpadding='0' cellspacing='0'><tr><td><a href='ProductBrowser_Admin.aspx?Page=EditImage&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;ViewId=" + item.Id.ToString() + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_edit.gif' class='img_noborder' /></a></td><td><a href='ProductBrowser_Admin.aspx?Page=EditImage&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;ViewId=" + item.Id.ToString() + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'>&nbsp;Edit</a></td></tr></table></td>";
                    
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

                //if (RXServer.Web.RequestValues.PagId > 0)
                //{
                //    Url = Url + "&PagId=" + RXServer.Web.RequestValues.PagId;
                //}

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
    public void BindListDataTabs()
    {
        String function_name = "BindListDataTabs";
        try
        {
            this.ltrHelpText.Text = RXMali.GetXMLHelpNode("ProductBrowser/List");
            String list = "";

            String Url = "";
            Int32 limit = 10;
            Int32 total = 0;
            Int32 index = RXServer.Web.RequestValues.Index;
            Int32 page = (index / limit) + 1;
            Int32 pages = 0;

            Url = RXMali.GetLastUrl(Request.Url.ToString());

            //RXServer.Modules.CalendarList cl = new RXServer.Modules.CalendarList("Calendar_" + RXServer.Web.RequestValues.ModId.ToString());

            //RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("Gallery2_" + RXServer.Web.RequestValues.ModId, LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, index, limit);
            //RXServer.Modules.Base.List t = new RXServer.Modules.Base.List("Gallery2_" + RXServer.Web.RequestValues.ModId);
            RXServer.Modules.Base.List cl = new RXServer.Modules.Base.List("ProductBrowserTab_" + RXServer.Web.RequestValues.ModId, LiquidCore.LiquidCore.Definition.ListDefinition.SortParamEnum.Order, LiquidCore.LiquidCore.Definition.ListDefinition.SortOrderEnum.Descending, index, limit);
            RXServer.Modules.Base.List t = new RXServer.Modules.Base.List("ProductBrowserTab_" + RXServer.Web.RequestValues.ModId);


            total = t.Count();

            list += "<table cellspacing='0' cellpadding='5' style='width: 100%; border: solid 1px #CCCCCC;'>";

            if (total > 0)
            {
                list += "<tr style='background-color: #666666' class='Text11_white'>";
                list += "<td valign='top'></td>";
                list += "<td valign='top'>Title</td>";
                list += "<td valign='top' align='center'>Visible</td>";
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
                    list += "<td valign='middle'>";
                    list += item.Value1;


                    #region loadImage
                    //if (item.Value10 != "")
                    //{
                    //    if (item.Value11 == ".gif" || item.Value11 == ".jpg" || item.Value11 == ".jpeg" || item.Value11 == ".png")
                    //    {
                    //        String imgfile = Server.MapPath("~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + item.Value10);

                    //        if (File.Exists(imgfile))
                    //        {
                    //            // Retrieve the image.
                    //            Bitmap image1;

                    //            image1 = new Bitmap(imgfile, true);

                    //            Int32 width, height, newheight, newwidth;

                    //            width = 30;
                    //            height = 30;

                    //            newwidth = width;

                    //            if (image1.Width <= newwidth)
                    //            {
                    //                newwidth = image1.Width;
                    //            }

                    //            newheight = Convert.ToInt32((image1.Height * newwidth) / image1.Width);

                    //            if (newheight > image1.Height)
                    //            {
                    //                newwidth = Convert.ToInt32((image1.Width * height) / image1.Height);
                    //                newheight = height;
                    //            }

                    //            if (newheight > height)
                    //            {
                    //                float temp, temph, tempnh;
                    //                temph = height;
                    //                tempnh = newheight;
                    //                temp = temph / tempnh;
                    //                newwidth = Convert.ToInt32(newwidth * temp);
                    //                newheight = height;
                    //            }

                    //            list += "<img src='" + "../../../Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + item.Value10 + "' style='width: " + newwidth + "px; height: " + newheight + "px; border: solid 1px black;' />";

                    //        }
                    //    }

                    //}

                    #endregion

                    list += "</td>";
                    list += "<td valign='middle'>" + item.Value13 + "</td>";
                    if (item.Value6 == "true")
                    {
                        list += "<td valign='middle' align='center'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_on.gif' title='Active' class='img_noborder' /></td>";
                    }
                    else
                    {
                        list += "<td valign='middle' align='center'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_off.gif' title='Unactive' class='img_noborder' /></td>";
                    }

                    list += "<td valign='middle' align='center'><a href='" + Request.Url.ToString() + "&Up=" + item.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_up.gif' alt='Move Up' class='img_noborder' /></a> / <a href='" + Request.Url.ToString() + "&Down=" + item.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_down.gif' alt='Move Down' class='img_noborder' /></a></td>";
                    list += "<td valign='middle'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&DelId=" + item.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this event( " + item.Value13 + " )?')\"><img src='../../../App_Themes/WebAdmin/Images/icon_bullet_delete.gif' class='img_noborder' /></a></td><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&DelId=" + item.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this item( " + item.Value13 + " )?')\">Delete</a></td></tr></table></td>";
                    list += "<td valign='middle'><table cellpadding='0' cellspacing='0'><tr><td><a href='ProductBrowser_Admin.aspx?Page=EditTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;ViewId=" + item.Id.ToString() + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_edit.gif' class='img_noborder' /></a></td><td><a href='ProductBrowser_Admin.aspx?Page=EditTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;ViewId=" + item.Id.ToString() + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'>&nbsp;Edit</a></td></tr></table></td>";

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

                //if (RXServer.Web.RequestValues.PagId > 0)
                //{
                //    Url = Url + "&PagId=" + RXServer.Web.RequestValues.PagId;
                //}

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
                    Int32 ix = (pages - 1) * limit;
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

            this.ltrTabs.Text = list;

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

            RXServer.Modules.Base.List.Item e = new LiquidCore.List.Item(obd_id);

            this.txtEditTab.Text = Server.HtmlDecode(e.Value1).Replace("`", "'");
            this.RadEditor1.Content = Server.HtmlDecode(e.Value2);
            //this.txtImageName.Text = Server.HtmlDecode(e.Value13).Replace("`", "'");
            //this.RadEditor2.Content = Server.HtmlDecode(e.Value25).Replace("`", "'");

            //if (e.Value6 == "false")
            //{
            //    this.cbShowImage.Checked = false;
            //}
            //else
            //{
            //    this.cbShowImage.Checked = true;
            //}

            #region loadImage
            //if (e.Value10 != "")
            //{
            //    this.imgContent.Visible = true;

            //    if (e.Value11 == ".gif" || e.Value11 == ".jpg" || e.Value11 == ".jpeg" || e.Value11 == ".png" || e.Value11 == ".JPG" || e.Value11 == ".JPEG" || e.Value11 == ".PNG" || e.Value11 == ".GIF")
            //    {
            //        String imgfile = Server.MapPath("~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + e.Value10);

            //        if (File.Exists(imgfile))
            //        {
            //            // Retrieve the image.
            //            Bitmap image1;

            //            image1 = new Bitmap(imgfile, true);

            //            Int32 width, height, newheight, newwidth;

            //            width = 200;
            //            height = 200;

            //            newwidth = width;

            //            if (image1.Width <= newwidth)
            //            {
            //                newwidth = image1.Width;
            //            }

            //            newheight = Convert.ToInt32((image1.Height * newwidth) / image1.Width);

            //            if (newheight > image1.Height)
            //            {
            //                newwidth = Convert.ToInt32((image1.Width * height) / image1.Height);
            //                newheight = height;
            //            }

            //            if (newheight > height)
            //            {
            //                float temp, temph, tempnh;
            //                temph = height;
            //                tempnh = newheight;
            //                temp = temph / tempnh;
            //                newwidth = Convert.ToInt32(newwidth * temp);
            //                newheight = height;
            //            }

            //            this.imgContent.ImageUrl = "~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + e.Value10;
            //            this.imgContent.Width = newwidth;
            //            this.imgContent.Height = newheight;
            //            this.hplZoomImg.Visible = true;
            //            this.hplZoomImg.NavigateUrl = "~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/" + e.Value10;
            //        }
            //    }
            //    else
            //    {
            //        this.lblImgText.CssClass = "Text11_red";
            //        this.lblImgText.Text = "Invalid MediaFile! (" + e.Value7 + ")";
            //    }
            //}
            //else
            //{
            //    this.imgContent.Visible = false;
            //}

            #endregion

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    public void BindChoiceData(String eId)
    {
        String function_name = "BindTabData";
        try
        {
            Int32 obd_id = 0;
            Int32.TryParse(eId, out obd_id);

            RXServer.Modules.Base.List.Item e = new LiquidCore.List.Item(obd_id);

            this.txtEditChoice.Text = Server.HtmlDecode(e.Value1).Replace("`", "'");
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
            if (RXServer.Web.RequestValues.Page == "Image" || RXServer.Web.RequestValues.Page == "")
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=Gallery&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_on'>Gallery</a></td>";
            }
            else
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=Gallery&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_off'>Gallery</a></td>";
            }
            list += "<td style='width:20px;'></td>";
            if (RXServer.Web.RequestValues.Page == "Content")
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=Module&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_on'>Content</a></td>";
            }
            else
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=Module&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_off'>Content</a></td>";
            }
            list += "<td style='width:20px;'></td>";
            if (RXServer.Web.RequestValues.Page == "Choices")
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=Choices&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_on'>Choices</a></td>";
            }
            else
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=Choices&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_off'>Choices</a></td>";
            }
            list += "<td style='width:20px;'></td>";
            if (RXServer.Web.RequestValues.Page == "Tabs")
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=Tabs&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_on'>Tabs</a></td>";
            }
            else
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=Tabs&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_off'>Tabs</a></td>";
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
            if (RXServer.Web.RequestValues.Page == "AddImage")
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=AddImage&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='ProductBrowser_Admin.aspx?Page=AddImage&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_on'>Add Image</a></td>";
            }
            else
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=AddImage&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='ProductBrowser_Admin.aspx?Page=AddImage&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_off'>Add Image</a></td>";
            }
            list += "<td style='width:10px;'><td><img src='../../../App_Themes/WebAdmin/Images/submenu_divider.gif' class='img_noborder' /></td><td style='width:10px;'>";
            if (RXServer.Web.RequestValues.Page == "AddTab")
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=AddTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='ProductBrowser_Admin.aspx?Page=AddTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_on'>Add Tab</a></td>";
            }
            else
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=AddTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='ProductBrowser_Admin.aspx?Page=AddTab&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_off'>Add Tab</a></td>";
            }
            list += "<td style='width:10px;'><td><img src='../../../App_Themes/WebAdmin/Images/submenu_divider.gif' class='img_noborder' /></td><td style='width:10px;'>";
            if (RXServer.Web.RequestValues.Page == "AddChoice")
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=AddChoice&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='ProductBrowser_Admin.aspx?Page=AddChoice&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_on'>Add Choice</a></td>";
            }
            else
            {
                list += "<td><a href='ProductBrowser_Admin.aspx?Page=AddChoice&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='ProductBrowser_Admin.aspx?Page=AddChoice&SitId=" + RXServer.Web.RequestValues.SitId + "&PagId=" + RXServer.Web.RequestValues.PagId + "&ModId=" + RXServer.Web.RequestValues.ModId + "&amp;Var=" + RXServer.Web.RequestValues.Var + "' class='submenu_off'>Add Choice</a></td>";
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

            double priceResult;
            double oldPriceResult;
            double vatResult;
            int stockResult;

            bool priceIsDouble = Double.TryParse(txtProductPrice.Text, out priceResult);
            bool oldPriceIsDouble = Double.TryParse(txtOldPrice.Text, out oldPriceResult);
            bool vatIsDouble = Double.TryParse(txtProductVat.Text, out vatResult);
            bool stockIsInt = Int32.TryParse(txtProductStockAmount.Text, out stockResult);

            RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);

            if (!priceIsDouble)
            {
                valid = false;
                Errors += " - Price has to be a numeric value.";
            }
            else if (priceResult < 0)
            {
                valid = false;
                Errors += " - Price can't be negative.";
            }

            if (!oldPriceIsDouble)
            {
                valid = false;
                Errors += " - Old price has to be a numeric value.";
            }
            else if (oldPriceResult < 0)
            {
                valid = false;
                Errors += " - Old price can't be negative.";
            }

            if (!vatIsDouble)
            {
                valid = false;
                Errors += " - Vat has to be a numeric value.";
            }
            else if (vatResult < 0)
            {
                valid = false;
                Errors += " - Vat can't be negative.";
            }

            if (!stockIsInt)
            {
                valid = false;
                Errors += " - Stock amount has to be a numeric value.";
            }
            else if (stockResult < 0)
            {
                valid = false;
                Errors += " - Stock amount can't be negative.";
            }

            if (valid)
            {
                sm.Float = "left";
                sm.ModelId = "2";
                sm.Text1 = Server.HtmlEncode(txtChoicesTitle.Text).Replace("'", "`");
                sm.Text2 = Server.HtmlEncode(txtHeader.Text).Replace("'", "`");
                sm.Text3 = Server.HtmlEncode(txtText.Content).Replace("'", "`");
                sm.Text4 = Server.HtmlEncode(txtProductName.Text).Replace("'", "`");
                sm.Text5 = Server.HtmlEncode(txtProductPrice.Text).Replace("'", "`");
                sm.Text6 = Server.HtmlEncode(txtProductVat.Text).Replace("'", "`");
                sm.Text7 = Server.HtmlEncode(txtProductStockAmount.Text).Replace("'", "`");
                sm.Text8 = Server.HtmlEncode(txtOldPrice.Text).Replace("'", "`");
                sm.Text9 = Server.HtmlEncode(txtProductId.Text).Replace("'", "`");
                sm.Text10 = Server.HtmlEncode(txtCategoryText.Content).Replace("'", "`");
                sm.Save();
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

    public void DeleteItem(String Id)
    {
        String function_name = "DeleteImage";
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

    protected void btnSaveImage_Click(object sender, EventArgs e)
    {
        String function_name = "btnSaveImage_Click";
        try
        {
            if (CheckImage())
            {
                String Errors = "";
                Boolean valid = true;

                RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);

                #region UploadImage

                // Sets Module Width

                Int32 ModelId = 0;
                Int32.TryParse(sm.ModelId, out ModelId);
                Int32 img_width = RXMali.GetModelSize(ModelId.ToString());
                Int32 img_big_width = 430;
                Int32 img_big_height = 323;
                Int32 img_thumb_width = 50;
                Int32 img_thumb_height = 50;


                String strFileName = "";
                String strFileName2 = "";
                String strFileExt = "";

                if (RadUpload2.InvalidFiles.Count > 0)
                {
                    this.ErrorBox.Visible = true;
                    Errors += " - " + RXMali.GetXMLNode("Error/media");
                    this.imgError1_1.Visible = true;
                    valid = false;
                }
                else
                {
                    if (this.RadUpload2.UploadedFiles.Count > 0)
                    {
                        String activeDir = "";
                        try
                        {
                            activeDir = Server.MapPath(@"../../../Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/");
                        }
                        catch (Exception ex)
                        {
                            this.ErrorBox.Visible = true;
                            Errors += ex;
                        }

                        if (!System.IO.Directory.Exists(activeDir))
                        {
                            System.IO.Directory.CreateDirectory(activeDir);
                        }
                        this.RadUpload2.TargetFolder = "~/Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId;

                        foreach (UploadedFile f1 in RadUpload2.UploadedFiles)
                        {
                            RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item();


                            i.Alias = "Gallery2_" + RXServer.Web.RequestValues.ModId;
                            i.Status = 1;
                            i.Language = 1;
                            i.SitId = 1;
                            i.PagId = RXServer.Web.RequestValues.PagId;
                            i.ModId = RXServer.Web.RequestValues.ModId;
                            i.Value6 = "true";
                            

                            //strFileName = f1.GetName().Trim();
                            //f1.SaveAs(activeDir + strFileName);


                            strFileName = f1.GetNameWithoutExtension().Trim() + "_" + DateTime.Today.Ticks.ToString();
                            strFileExt = f1.GetExtension().ToLower();
                            strFileName2 = strFileName + strFileExt;

                            f1.SaveAs(activeDir + strFileName2);

                            // Scale Large Image

                            System.Drawing.Image scaleImg = System.Drawing.Image.FromFile(activeDir + strFileName2);

                            if (scaleImg.Width == img_big_width && scaleImg.Height == img_big_height)
                            {
                                i.Value10 = strFileName2;
                                i.Value11 = strFileExt;
                            }
                            else
                            {
                                System.Drawing.Image newImg = null;
                                newImg = RXMali.ScaleFixedImage(scaleImg, img_big_width, img_big_height);
                                newImg.Save(activeDir + "s_" + f1.GetNameWithoutExtension() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                newImg.Dispose();

                                i.Value10 = "s_" + f1.GetNameWithoutExtension() + ".jpg";
                                i.Value11 = ".jpg";

                            }

                            // Scale Thumb Image

                            System.Drawing.Image newThumb = null;
                            newThumb = RXMali.ScaleFixedImage(scaleImg, img_thumb_width, img_thumb_height);
                            newThumb.Save(activeDir + "t_" + f1.GetNameWithoutExtension() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            newThumb.Dispose();

                            i.Value14 = "t_" + f1.GetNameWithoutExtension() + ".jpg";

                            i.Save();
                                               
                        }
                    }
                }

                #endregion

                if (valid)
                {
                    string Url = "";

                    Url = RXMali.GetLastUrl(Request.Url.ToString());

                    Url = Url.Replace("&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
                    Url = Url.Replace("AddImage", "Gallery");

                    RXServer.Web.Redirect.To(Url);
                }
                else
                {
                    this.ltrErrors.Text = Errors;
                    this.ErrorBox.Visible = true;
                }
            }
            else
            {
                this.ltrErrors.Text = ErrorList; 
                this.ErrorBox.Visible = true;
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void btnEditImage_Click(object sender, EventArgs e)
    {
        String function_name = "btnEditImage_Click";
        try
        {
            if (CheckImage())
            {
                String Errors = "";
                Boolean valid = true;
                string Url = "";

                Int32 obd_id = 0;

                Int32.TryParse(RXServer.Web.RequestValues.ViewId, out obd_id);

                if (obd_id > 0)
                {
                    RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);
                    RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item(obd_id);

                    i.Alias = "Gallery2_" + RXServer.Web.RequestValues.ModId;
                    i.Status = 1;
                    i.Language = 1;
                    i.SitId = 1;
                    i.PagId = RXServer.Web.RequestValues.PagId;
                    i.ModId = RXServer.Web.RequestValues.ModId;
                    i.Value13 = Server.HtmlEncode(this.txtImageName.Text).Replace("'", "`");
                    i.Value25 = Server.HtmlEncode(this.RadEditor2.Content).Replace("'", "`");

                    if (this.cbShowImage.Checked == true)
                    {
                        i.Value6 = "true";
                    }
                    else
                    {
                        i.Value6 = "false";
                    }

                    #region UploadImage

                    // Sets Module Width

                    Int32 ModelId = 0;
                    Int32.TryParse(sm.ModelId, out ModelId);
                    Int32 img_width = RXMali.GetModelSize(ModelId.ToString());
                    Int32 img_big_width = 428;
                    Int32 img_big_height = 321;
                    Int32 img_thumb_width = 50;
                    Int32 img_thumb_height = 50;
                    String strFileName = "";
                    String strFileName2 = "";
                    String strFileExt = "";

                    if (RadUpload1.InvalidFiles.Count > 0)
                    {
                        Errors += " - " + RXMali.GetXMLNode("Error/media");
                        this.imgError1_1.Visible = true;
                        valid = false;
                    }
                    else
                    {
                        if (this.RadUpload1.UploadedFiles.Count > 0)
                        {
                            String activeDir = "";
                            activeDir = Server.MapPath(@"../../../Upload/Pages/" + RXServer.Web.RequestValues.PagId + "/" + RXServer.Web.RequestValues.ModId + "/");

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


                                // Scale Image
                                System.Drawing.Image scaleImg = System.Drawing.Image.FromFile(activeDir + strFileName2);

                                if (scaleImg.Width == img_big_width && scaleImg.Height == img_big_height)
                                {
                                    i.Value10 = strFileName2;
                                    i.Value11 = strFileExt;
                                }
                                else
                                {
                                    System.Drawing.Image newImg = null;
                                    newImg = RXMali.ScaleFixedImage(scaleImg, img_big_width, img_big_height);
                                    newImg.Save(activeDir + "s_" + strFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                    newImg.Dispose();

                                    i.Value10 = "s_" + strFileName + ".jpg";
                                    i.Value11 = ".jpg";

                                }

                                // Scale Thumb Image

                                System.Drawing.Image newThumb = null;
                                newThumb = RXMali.ScaleFixedImage(scaleImg, img_thumb_width, img_thumb_height);
                                newThumb.Save(activeDir + "t_" + strFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                newThumb.Dispose();

                                i.Value14 = "t_" + strFileName + ".jpg";
                            }
                        }
                    }

                    #endregion

                    if (valid)
                    {
                        i.Save();

                        Url = RXMali.GetLastUrl(Request.Url.ToString());

                        Url = Url.Replace("&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
                        Url = Url.Replace("EditImage", "Gallery");

                        RXServer.Web.Redirect.To(Url);
                    }
                    else
                    {
                        this.ltrErrors.Text = Errors;
                        this.ErrorBox.Visible = true;
                    }

                }
                else
                {
                    Url = RXMali.GetLastUrl(Request.Url.ToString());

                    Url = Url.Replace("&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
                    Url = Url.Replace("EditImage", "Gallery");

                    RXServer.Web.Redirect.To(Url);
                }


            }
            else
            {
                this.ltrErrors.Text = ErrorList;
                this.ErrorBox.Visible = true;
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void btnSaveTab_Click(object sender, EventArgs e)
    {
        String function_name = "btnSaveTab_Click";
        try
        {
            if (txtTabTitle.Text != String.Empty)
            {
                String Errors = "";
                Boolean valid = true;

                RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);

                RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item();
                i.Alias = "ProductBrowserTab_" + RXServer.Web.RequestValues.ModId;
                i.Status = 1;
                i.Language = 1;
                i.SitId = 1;
                i.PagId = RXServer.Web.RequestValues.PagId;
                i.ModId = RXServer.Web.RequestValues.ModId;
                i.Value6 = "true";
                i.Value1 = Server.HtmlDecode(txtTabTitle.Text).Replace("'", "`");
                i.Save();

                if (valid)
                {
                    string Url = "";

                    Url = RXMali.GetLastUrl(Request.Url.ToString());

                    //Url = Url.Replace("&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
                    Url = Url.Replace("AddTab", "Tabs");

                    RXServer.Web.Redirect.To(Url);
                }
                else
                {
                    this.ltrErrors.Text = Errors;
                    this.ErrorBox.Visible = true;
                }
            }
            else
            {
                this.ltrErrors.Text = ErrorList;
                this.ErrorBox.Visible = true;
            }

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
            if (txtEditTab.Text != String.Empty)
            {
                String Errors = "";
                Boolean valid = true;

                RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);

                Int32 obd_id = 0;
                Int32.TryParse(RXServer.Web.RequestValues.ViewId, out obd_id);

                if (obd_id > 0)
                {

                    RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item(obd_id);
                    i.Alias = "ProductBrowserTab_" + RXServer.Web.RequestValues.ModId;
                    i.Status = 1;
                    i.Language = 1;
                    i.SitId = 1;
                    i.PagId = RXServer.Web.RequestValues.PagId;
                    i.ModId = RXServer.Web.RequestValues.ModId;
                    i.Value6 = "true";
                    i.Value1 = Server.HtmlDecode(txtEditTab.Text).Replace("'", "`");
                    i.Value2 = Server.HtmlDecode(RadEditor1.Content).Replace("'", "`");
                    i.Save();
                }

                if (valid)
                {
                    string Url = "";

                    Url = RXMali.GetLastUrl(Request.Url.ToString());

                    Url = Url.Replace("&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
                    Url = Url.Replace("EditTab", "Tabs");

                    RXServer.Web.Redirect.To(Url);
                }
                else
                {
                    this.ltrErrors.Text = Errors;
                    this.ErrorBox.Visible = true;
                }
            }
            else
            {
                this.ltrErrors.Text = ErrorList;
                this.ErrorBox.Visible = true;
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    public Boolean CheckImage()
    {
        String function_name = "CheckEvent";
        try
        {
            Boolean valid = true;
            return valid;

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
            return false;
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

            //this.ddlModels.Items.Add(new ListItem("Select a size", "0"));
            //this.ddlModels.Items.Add(new ListItem("Small", "1"));
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
            //    this.ddlModels.Items.Add(new ListItem("XLarge", "4"));
            //}
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void btnSaveChoice_Click(object sender, EventArgs e)
    {
        String function_name = "btnSaveChoice_Click";
        try
        {
            if (txtChoiceName.Text != String.Empty)
            {
                String Errors = "";
                Boolean valid = true;

                RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);

                RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item();
                i.Alias = "Choices_" + RXServer.Web.RequestValues.ModId;
                i.Status = 1;
                i.Language = 1;
                i.SitId = 1;
                i.PagId = RXServer.Web.RequestValues.PagId;
                i.ModId = RXServer.Web.RequestValues.ModId;
                i.Value6 = "true";
                i.Value1 = Server.HtmlDecode(txtChoiceName.Text).Replace("'", "`");
                i.Save();

                if (valid)
                {
                    string Url = "";

                    Url = RXMali.GetLastUrl(Request.Url.ToString());

                    //Url = Url.Replace("&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
                    Url = Url.Replace("AddChoice", "Choices");

                    RXServer.Web.Redirect.To(Url);
                }
                else
                {
                    this.ltrErrors.Text = Errors;
                    this.ErrorBox.Visible = true;
                }
            }
            else
            {
                this.ltrErrors.Text = ErrorList;
                this.ErrorBox.Visible = true;
            }
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void btnEditChoice_Click(object sender, EventArgs e)
    {
        String function_name = "btnEditChoice_Click";
        try
        {
            if (txtEditChoice.Text != String.Empty)
            {
                String Errors = "";
                Boolean valid = true;

                RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);

                Int32 obd_id = 0;
                Int32.TryParse(RXServer.Web.RequestValues.ViewId, out obd_id);

                if (obd_id > 0)
                {

                    RXServer.Modules.Base.List.Item i = new LiquidCore.List.Item(obd_id);
                    i.Alias = "Choices_" + RXServer.Web.RequestValues.ModId;
                    i.Status = 1;
                    i.Language = 1;
                    i.SitId = 1;
                    i.PagId = RXServer.Web.RequestValues.PagId;
                    i.ModId = RXServer.Web.RequestValues.ModId;
                    i.Value6 = "true";
                    i.Value1 = Server.HtmlDecode(txtEditChoice.Text).Replace("'", "`");
                    i.Save();
                }

                if (valid)
                {
                    string Url = "";

                    Url = RXMali.GetLastUrl(Request.Url.ToString());

                    Url = Url.Replace("&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
                    Url = Url.Replace("EditChoice", "Choices");

                    RXServer.Web.Redirect.To(Url);
                }
                else
                {
                    this.ltrErrors.Text = Errors;
                    this.ErrorBox.Visible = true;
                }
            }
            else
            {
                this.ltrErrors.Text = ErrorList;
                this.ErrorBox.Visible = true;
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
}


