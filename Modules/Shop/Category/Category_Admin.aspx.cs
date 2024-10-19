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
using System.IO;
using Telerik.Web.UI;
using System.Drawing;

public partial class Modules_Menus_Category_Category_Admin : System.Web.UI.Page
{
    String class_name = "Modules_Menus_Category_Category_Admin";

    String ErrorList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            this.AdminSubMenu.Visible = true;
            this.ErrorBox.Visible = false;

            BindMenuData();
            BindMenuData2();

            switch (RXServer.Web.RequestValues.Page)
            {
                case "Header":
                    this.Page_2.Visible = true;
                    if (!Page.IsPostBack)
                    {
                        BindHeaderData();
                    }
                    break;
                case "Add":
                    if (AddPage(RXServer.Web.RequestValues.ViewId))
                    {
                        String Url = "";
                        Url = RXMali.GetLastUrl(Request.Url.ToString());
                        Url = Url.Replace("&Page=Add&ViewId=" + RXServer.Web.RequestValues.ViewId, "");

                        RXServer.Web.Redirect.To(Url);
                    }
                    break;

                case "MoveUp":
                    this.Page_1.Visible = true;
                    if (RXServer.Web.RequestValues.ViewId != "")
                    {
                        MovePageUp(RXServer.Web.RequestValues.ViewId);
                    }
                    break;

                case "MoveDown":
                    this.Page_1.Visible = true;
                    if (RXServer.Web.RequestValues.ViewId != "")
                    {
                        MovePageDown(RXServer.Web.RequestValues.ViewId);
                    }
                    break;

                case "Hidden":
                    this.Page_1.Visible = true;
                    if (RXServer.Web.RequestValues.ViewId != "")
                    {
                        SetHidden(RXServer.Web.RequestValues.ViewId);
                    }
                    break;


                default:
                    this.Page_1.Visible = true;
                    if (RXServer.Web.RequestValues.DelId != "")
                    {
                        DeletePage(RXServer.Web.RequestValues.DelId);
                    }
                    BindProductsData();
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

    public void BindProductsData()
    {
        String function_name = "BindPageData";
        try
        {
            Int32 parentId = 0;

            this.ltrHelpText.Text = RXMali.GetXMLHelpNode("Menu/List2");

            parentId = RXServer.Web.RequestValues.PagId;

            RXServer.Modules.Menu.Item mi = new RXServer.Modules.Menu.Item(parentId);
           
            RXServer.Modules.Menu.Item mItem2 = new RXServer.Modules.Menu.Item(parentId);
            this.lblHeaderPage1.Text = "Products";

            RXServer.Modules.Menu m = new RXServer.Modules.Menu(1, parentId,1);           

            String list = "";

            list += "<table cellspacing='0' cellpadding='5' style='width: 100%; border: solid 1px #CCCCCC;'>";

            if (m.Count > 0)
            {
                list += "<tr style='background-color: #666666' class='Text11_white'>";
                list += "<td valign='top'>Id</td>";
                list += "<td valign='top'>Title</td>";
                list += "<td valign='top' align='center'>Visible</td>";
                list += "<td valign='top' align='center'>Move Up</td>";
                list += "<td valign='top' align='center'>Move Down</td>";
                list += "<td valign='top' align='center'>Delete</td>";
                list += "</tr>";

                Int32 counter = 0;
                foreach (LiquidCore.Menu.Item mItem in m)
                {
                    if ((counter % 2) == 0)
                    {
                        list += "<tr style='background-color: white;'  class='Text11_gray'>";
                    }
                    else
                    {
                        list += "<tr style='background-color: #EFEFEF;'  class='Text11_gray'>";
                    }

                    string Url = "";

                    Url = RXMali.GetLastUrl(Request.Url.ToString());
                    Url = Url.Replace("&Page=Products", "");

                    list += "<td valign='top'>" + mItem.Id.ToString() + "</td>";
                    list += "<td valign='top' style='width: 200px;'>" + FetchProductTitle(mItem.Id) + "</td>";
                    if (mItem.Hidden)
                    {
                        list += "<td valign='top' align='center'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + Url + "&Page=Hidden&ViewId=" + mItem.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_off.gif' class='img_noborder' /></a></td><td><a href='" + Url + "&Page=Hidden&ViewId=" + mItem.Id.ToString() + "'>&nbsp;Hidden</td></tr></table></td>";
                    }
                    else
                    {
                        list += "<td valign='top' align='center'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + Url + "&Page=Hidden&ViewId=" + mItem.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_on.gif' class='img_noborder' /></a></td><td><a href='" + Url + "&Page=Hidden&ViewId=" + mItem.Id.ToString() + "'>&nbsp Visible</td></tr></table></td>";
                    }
                    list += "<td valign='top'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + Url + "&Page=MoveUp&ViewId=" + mItem.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_up.gif' class='img_noborder' /></a></td><td><a href='" + Url + "&Page=MoveUp&ViewId=" + mItem.Id.ToString() + "'>&nbsp;Move Up</a></td></tr></table></td>";
                    list += "<td valign='top'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + Url + "&Page=MoveDown&ViewId=" + mItem.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_down.gif' class='img_noborder' /></a></td><td><a href='" + Url + "&Page=MoveDown&ViewId=" + mItem.Id.ToString() + "'>&nbsp;Move Down</a></td></tr></table></td>";
                    if (mItem.Id == 1)
                    {
                        list += "<td>&nbsp;</td>";
                    }
                    else
                    {
                        list += "<td valign='top' align='center'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + Url + "&DelId=" + mItem.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this page( " + mItem.Title + " )?')\"><img src='../../../App_Themes/WebAdmin/Images/icon_bullet_delete.gif' class='img_noborder' /></a></td><td><a href='" + Url + "&DelId=" + mItem.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this page( " + mItem.Title + " )?')\">Delete</a></td></tr></table></td>";
                    }
                    list += "</tr>";

                    counter++;
                }


            }
            else
            {
                list += "<tr><td class='Text11_gray'>No Records was found.</td></tr>";
            }

            list += "</table>";

            this.ltrPageList.Text = list;

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    public void BindHeaderData()
    {
        String function_name = "BindHeaderData";
        try
        {
            lblHeaderPage2.Text = "Header";
            this.ltrHelpText.Text = RXMali.GetXMLHelpNode("Menu/Header");
            RXServer.Modules.TextModule tm = new RXServer.Modules.TextModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);
            txtHeader.Text = tm.Text1;
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private String FetchProductTitle(int pagId)
    {
        String title = "";
        foreach(LiquidCore.Module md in new RXServer.Modules.Menu.Item(pagId).Modules)
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

    private Boolean AddPage(String PagId)
    {
        String function_name = "AddPage";
        try
        {
            Int32 pId = 0;
            String ErrorList = "";
            Boolean valid = true;

            Int32.TryParse(PagId, out pId);

            RXServer.Modules.Menu.Item mItem = new RXServer.Modules.Menu.Item();
            mItem.SitId = 1;
            mItem.Language = 1;
            mItem.Status = 1;
            mItem.ParentId = pId;
            mItem.Template = "Template2.master";
            mItem.ModelId = 1;
            mItem.Hidden = true;

            if (valid)
            {
                mItem.Save();
                CreatePageFolder(mItem.Id);

                RXServer.Modules.Menu.Item n = new RXServer.Modules.Menu.Item(mItem.Id);
                n.Save();

                int modId = RXServer.Modules.Base.Generic.Create(1, mItem.Id, 100, "ContentPane1", 1, 1, false, false);

                if (valid)
                {
                    mItem.Save();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                this.ltrErrors.Text = ErrorList;
                this.ErrorBox.Visible = true;
                return false;
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
            return false;
        }
        
    }

    private void DeletePage(String delId)
    {
        String function_name = "DeletePage";
        try
        {
            Int32 pId = 0;

            Int32.TryParse(delId, out pId);

            if (pId > 0)
            {
                RXServer.Modules.Menu.DeletePage(1, pId);

                string activeDir = Server.MapPath("~/Upload/Pages/" + pId + "/");

                if (System.IO.Directory.Exists(activeDir))
                {
                    foreach (string sFile in System.IO.Directory.GetFiles(activeDir))
                    {
                        System.IO.File.Delete(sFile);
                    }
                }

                string Url = "";

                Url = RXMali.GetLastUrl(Request.Url.ToString());
                Url = Url.Replace("&DelId=" + pId, "");

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
    private void MovePageUp(String pagId)
    {
        String function_name = "MovePageUp";
        try
        {
            Int32 pId = 0;

            Int32.TryParse(pagId, out pId);

            if (pId > 0)
            {
                RXServer.Modules.Menu.Item mItem = new RXServer.Modules.Menu.Item(pId);
                mItem.ChangeOrderUp();
                mItem.Save();

                String Url = "";

                Url = RXMali.GetLastUrl(Request.Url.ToString());
                Url = Url.Replace("&Page=MoveUp&ViewId=" + pId, "");

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
    private void MovePageDown(String pagId)
    {
        String function_name = "MovePageDown";
        try
        {
            Int32 pId = 0;

            Int32.TryParse(pagId, out pId);

            if (pId > 0)
            {
                RXServer.Modules.Menu.Item mItem = new RXServer.Modules.Menu.Item(pId);
                mItem.ChangeOrderDown();
                mItem.Save();

                String Url = "";

                Url = RXMali.GetLastUrl(Request.Url.ToString());
                Url = Url.Replace("&Page=MoveDown&ViewId=" + pId, "");

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

    private void SetHidden(String pagId)
    {
        String function_name = "SetHidden";
        try
        {
            Int32 pId = 0;

            Int32.TryParse(pagId, out pId);

            if (pId > 0)
            {
                RXServer.Modules.Menu.Item mItem = new RXServer.Modules.Menu.Item(pId);
                if (mItem.Hidden)
                {
                    mItem.Hidden = false;
                }
                else
                {
                    mItem.Hidden = true;
                }
                mItem.Save();

                String Url = "";

                Url = RXMali.GetLastUrl(Request.Url.ToString());
                Url = Url.Replace("&Page=Hidden&ViewId=" + pId, "");

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

    public void BindMenuData()
    {
        String function_name = "BindMenuData";
        try
        {
            string Url = "";

            Url = RXMali.GetLastUrl(Request.Url.ToString());
            Url = Url.Replace("&Page=Add&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
            Url = Url.Replace("&Page=Edit&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
            Url = Url.Replace("&Page=Products", "");
            Url = Url.Replace("&Page=Header", "");

            String list = "";

            list += "<table cellspacing='0' cellpadding='0' style='border:0px;'>";
            list += "<tr><td style='width:10px;'></td>";
            if (RXServer.Web.RequestValues.Page == "Products")
            {
                list += "<td style='height:34px;'><a href='" + Url + "&Page=Products' class='submenu_on'>Products</a></td>";
            }
            else
            {
                list += "<td style='height:34px;'><a href='" + Url + "&Page=Products' class='submenu_off'>Products</a></td>";
            }
            list += "<td style='width:20px;'></td>";
            if (RXServer.Web.RequestValues.Page == "Header")
            {
                list += "<td style='height:34px;'><a href='" + Url + "&Page=Header' class='submenu_on'>Header</a></td>";
            }
            else
            {
                list += "<td style='height:34px;'><a href='" + Url + "&Page=Header' class='submenu_off'>Header</a></td>";
            }
            list += "</tr>";
            list += "</table>";


            this.ltrAdminSubMenuList.Text = list;
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

            Int32 parentId = 0;

            parentId = RXServer.Web.RequestValues.PagId;           

            string Url = "";

            Url = RXMali.GetLastUrl(Request.Url.ToString());
            Url = Url.Replace("&Page=Add&ViewId=" + RXServer.Web.RequestValues.PagId, "");
            Url = Url.Replace("&Page=Products", "");
            Url = Url.Replace("&Page=Header", "");

            String list = "";

            list += "<table cellspacing='0' cellpadding='0' style='border:0px;'>";
            list += "<tr>";
            list += "<td style='width:10px;'><td><img src='../../../App_Themes/WebAdmin/Images/submenu_divider.gif' class='img_noborder' /></td><td style='width:10px;'>";

            RXServer.Modules.Menu.Item mi = new RXServer.Modules.Menu.Item(RXServer.Web.RequestValues.PagId);

            if (RXServer.Web.RequestValues.Page == "Add")
            {
                list += "<td style='height:34px;'><a href='" + Url + "&Page=Add&ViewId=" + parentId + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='" + Url + "&Page=Add&ViewId=" + parentId + "' class='submenu_on'>Add Product</a></td>";
            }
            else
            {
                list += "<td style='height:34px;'><a href='" + Url + "&Page=Add&ViewId=" + parentId + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='" + Url + "&Page=Add&ViewId=" + parentId + "' class='submenu_off'>Add Product</a></td>";
            }

            list += "<td style='width:20px;'></td></tr>";
            list += "</table>";

            this.ltrAdminSubMenuList2.Text = list;

            
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
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    private void CreatePageFolder(Int32 pagId)
    {
        String function_name = "CreatePageFolder";
        try
        {
            string activeDir = Server.MapPath("~/Upload/Pages/");
            string newPath = System.IO.Path.Combine(activeDir, pagId.ToString());

            System.IO.Directory.CreateDirectory(newPath);
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }

    }
    protected void btnSaveHeader_Click(object sender, EventArgs e)
    {
        String function_name = "btnSaveHeader_Click";
        try
        {
            Boolean valid = true;
            String Errors = "";
            
            RXServer.Modules.StandardModule sm = new RXServer.Modules.StandardModule(RXServer.Web.RequestValues.SitId, RXServer.Web.RequestValues.PagId, RXServer.Web.RequestValues.ModId);

            if (txtHeader.Text.Equals(""))
            {
                valid = false;
                Errors += " - Title missing.";
            }

            if (valid)
            {
                sm.Text1 = txtHeader.Text;
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
}
