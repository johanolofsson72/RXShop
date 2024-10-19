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

public partial class Modules__System_AdminTopMenu_AdminTopMenu_Admin : System.Web.UI.Page
{
    String class_name = "Modules__System_AdminTopMenu_AdminTopMenu_Admin";

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

            switch (RXServer.Web.RequestValues.SubPage)
            {

                case "Add":
                    this.Page_2.Visible = true;
                    this.lblHeaderPage2.Text = "Add Page";
                    //this.btnAddPag.Text = "Add Page";
                    this.ltrHelpText.Text = RXMali.GetXMLHelpNode("Menu/Add");
                    break;

                case "Edit":
                    this.Page_2.Visible = true;
                    if (RXServer.Web.RequestValues.ViewId != "")
                    {
                        if (!Page.IsPostBack)
                        {
                            BindEditPageData(RXServer.Web.RequestValues.ViewId);
                        }

                    }
                    this.ltrHelpText.Text = RXMali.GetXMLHelpNode("Menu/Edit");
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
                    BindPageData();
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

    public void BindPageData()
    {
        String function_name = "BindPageData";
        try
        {
            Int32 parentId = 0;

            this.ltrHelpText.Text = RXMali.GetXMLHelpNode("Menu/List");

            parentId = RXServer.Web.RequestValues.PagId;

            RXServer.Modules.Menu.Item mi = new RXServer.Modules.Menu.Item(parentId);

            if (mi.Level == 2)
            {
                parentId = mi.ParentId;
            }            
            
            if (parentId > 0)
            {
                RXServer.Modules.Menu.Item mItem2 = new RXServer.Modules.Menu.Item(parentId);
                this.lblHeaderPage1.Text = mItem2.Title + " » Subpages";
            }
            else
            {
                this.lblHeaderPage1.Text = "Rootpages";
            }            

            RXServer.Modules.Menu m = new RXServer.Modules.Menu(1, parentId,1);           

            String list = "";

            list += "<table cellspacing='0' cellpadding='5' style='width: 100%; border: solid 1px #CCCCCC;'>";

            if (m.Count > 0)
            {
                list += "<tr style='background-color: #666666' class='Text11_white'>";
                list += "<td valign='top'>Id</td>";
                list += "<td valign='top'>Title</td>";
                //list += "<td valign='top'>Friendly Url</td>";
                list += "<td valign='top' align='center'>Visible</td>";
                list += "<td valign='top' align='center'>Move Up</td>";
                list += "<td valign='top' align='center'>Move Down</td>";
                list += "<td valign='top' align='center'>Delete</td>";
                list += "<td valign='top'>Edit</td>";
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

                    list += "<td valign='top'>" + mItem.Id.ToString() + "</td>";
                    list += "<td valign='top' style='width: 200px;'>" + mItem.Title + "</td>";
                    //list += "<td valign='top'>" + mItem.Alias + "</td>";
                    if (mItem.Hidden)
                    {
                        list += "<td valign='top' align='center'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&SubPage=Hidden&ViewId=" + mItem.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_off.gif' class='img_noborder' /></a></td><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&SubPage=Hidden&ViewId=" + mItem.Id.ToString() + "'>&nbsp;Hidden</td></tr></table></td>";
                    }
                    else
                    {
                        list += "<td valign='top' align='center'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&SubPage=Hidden&ViewId=" + mItem.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_visible_on.gif' class='img_noborder' /></a></td><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&SubPage=Hidden&ViewId=" + mItem.Id.ToString() + "'>&nbsp Visible</td></tr></table></td>";
                    }
                    list += "<td valign='top'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&SubPage=MoveUp&ViewId=" + mItem.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_up.gif' class='img_noborder' /></a></td><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&SubPage=MoveUp&ViewId=" + mItem.Id.ToString() + "'>&nbsp;Move Up</a></td></tr></table></td>";
                    list += "<td valign='top'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&SubPage=MoveDown&ViewId=" + mItem.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_arrow_down.gif' class='img_noborder' /></a></td><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&SubPage=MoveDown&ViewId=" + mItem.Id.ToString() + "'>&nbsp;Move Down</a></td></tr></table></td>";
                    if (mItem.Id == 1)
                    {
                        list += "<td>&nbsp;</td>";
                    }
                    else
                    {
                        list += "<td valign='top' align='center'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&DelId=" + mItem.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this page( " + mItem.Title + " )?')\"><img src='../../../App_Themes/WebAdmin/Images/icon_bullet_delete.gif' class='img_noborder' /></a></td><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&DelId=" + mItem.Id.ToString() + "' onclick=\"return confirm('Are you sure you want to remove this page( " + mItem.Title + " )?')\">Delete</a></td></tr></table></td>";
                    }
                    list += "<td valign='top'><table cellpadding='0' cellspacing='0'><tr><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&SubPage=Edit&ViewId=" + mItem.Id.ToString() + "'><img src='../../../App_Themes/WebAdmin/Images/icon_edit.gif' class='img_noborder' /></a></td><td><a href='" + RXMali.GetLastUrl(Request.Url.ToString()) + "&SubPage=Edit&ViewId=" + mItem.Id.ToString() + "'>&nbsp;Edit</a></td></tr></table></td>";
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

    public void BindEditPageData(String editId)
    {
        String function_name = "BindEditPageData";
        try
        {
            Int32 pId = 0;

            Int32.TryParse(editId, out pId);

            this.lblHeaderPage2.Text = "Edit Page";
            //this.btnAddPag.Text = "Edit Page";

            if (pId > 0)
            {
                RXServer.Modules.Menu.Item mItem = new RXServer.Modules.Menu.Item(pId);
                this.txtPageName.Text = Server.HtmlDecode(mItem.Title).Replace("`", "'");
                //this.txtFriendlyUrl.Text = mItem.Alias;
            }
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }

    }

    protected void btnAddPag_Click(object sender, EventArgs e)
    {
        String function_name = "btnAddPag_Click";
        try
        {
            
            if (RXServer.Web.RequestValues.SubPage == "Add")
            {           
                if (AddPage(RXServer.Web.RequestValues.ViewId))
                {
                    String Url = "";
                    Url = RXMali.GetLastUrl(Request.Url.ToString());
                    Url = Url.Replace("&SubPage=Add&ViewId=" + RXServer.Web.RequestValues.ViewId, "");

                    RXServer.Web.Redirect.To(Url);
                }
            }
            if (RXServer.Web.RequestValues.SubPage == "Edit")
            {
                if (EditPage(RXServer.Web.RequestValues.ViewId))
                {
                    String Url = "";
                    Url = RXMali.GetLastUrl(Request.Url.ToString());
                    Url = Url.Replace("&SubPage=Edit&ViewId=" + RXServer.Web.RequestValues.ViewId, "");

                    RXServer.Web.Redirect.To(Url);
                }
            }

           
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
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
            mItem.Template = "Template0.master";
            mItem.Title = Server.HtmlEncode(this.txtPageName.Text).Replace("'","`");
            mItem.ModelId = 1;
            mItem.Hidden = true;

            if (this.txtPageName.Text == "")
            {
                ErrorList += " - You must fill in a pagename<br />";
                this.imgError2_1.Visible = true;
                valid = false;
            }

            //if (RXMali.TrimURL(this.txtFriendlyUrl.Text) == "")
            //{
            //    mItem.Alias = RXMali.TrimURL(this.txtPageName.Text);
            //}
            //else
            //{
            //    mItem.Alias = RXMali.TrimURL(this.txtFriendlyUrl.Text);
            //}


            if (valid)
            {
                mItem.Save();
                CreatePageFolder(mItem.Id);

                //RXServer.Modules.Menu.Item n = new RXServer.Modules.Menu.Item(mItem.Id);
                //n.Alias = RXMali.GetFriendlyUrl(Server.HtmlEncode(this.txtPageName.Text).Replace("'", "`"), this.txtFriendlyUrl.Text, n.Level, n.Id);
                //n.Save();
                return true;
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
    private Boolean EditPage(String PagId)
    {
        String function_name = "EditPage";
        try
        {
            Int32 pId = 0;
            String ErrorList = "";
            Boolean valid = true;

            Int32.TryParse(PagId, out pId);

            if (pId > 0)
            {
                RXServer.Modules.Menu.Item mItem = new RXServer.Modules.Menu.Item(pId);
                mItem.Title = Server.HtmlEncode(this.txtPageName.Text).Replace("'", "`");
                //mItem.Alias = RXMali.GetFriendlyUrl(Server.HtmlEncode(this.txtPageName.Text).Replace("'", "`"), this.txtFriendlyUrl.Text, mItem.Level, mItem.Id);

                if (this.txtPageName.Text == "")
                {
                    ErrorList += " - You must fill in a pagename<br />";
                    this.imgError2_1.Visible = true;
                    valid = false;
                }
                
                if (valid)
                {
                    mItem.Save();
                    return true;
                }
                else
                {
                    this.ltrErrors.Text = ErrorList;
                    this.ErrorBox.Visible = true;
                    return false;
                }
            }
            else
            {
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
                Session["rx_del"] = "true";
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
                Url = Url.Replace("&SubPage=MoveUp&ViewId=" + pId, "");

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
                Url = Url.Replace("&SubPage=MoveDown&ViewId=" + pId, "");

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
                Url = Url.Replace("&SubPage=Hidden&ViewId=" + pId, "");

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
            Url = Url.Replace("&SubPage=Add&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
            Url = Url.Replace("&SubPage=Edit&ViewId=" + RXServer.Web.RequestValues.ViewId, "");

            String list = "";

            list += "<table cellspacing='0' cellpadding='0' style='border:0px;'>";
            list += "<tr><td style='width:10px;'></td>";
            if (RXServer.Web.RequestValues.SubPage == "")
            {
                list += "<td style='height:34px;'><a href='" + Url + "' class='submenu_on'>Pages</a></td>";
            }
            else
            {
                list += "<td style='height:34px;'><a href='" + Url + "' class='submenu_off'>Pages</a></td>";
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

            RXServer.Modules.Menu.Item mi = new RXServer.Modules.Menu.Item(parentId);

            if (mi.Level == 2)
            {
                parentId = mi.ParentId;
            }  
           
            RXServer.Modules.Menu.Item mItem = new RXServer.Modules.Menu.Item(parentId);

            if (mItem.Level < 2)
            {               

                string Url = "";

                Url = RXMali.GetLastUrl(Request.Url.ToString());
                Url = Url.Replace("&SubPage=Add&ViewId=" + RXServer.Web.RequestValues.ViewId, "");
                Url = Url.Replace("&SubPage=Edit&ViewId=" + RXServer.Web.RequestValues.ViewId, "");

                String list = "";

                list += "<table cellspacing='0' cellpadding='0' style='border:0px;'>";
                list += "<tr>";
                list += "<td style='width:10px;'><td><img src='../../../App_Themes/WebAdmin/Images/submenu_divider.gif' class='img_noborder' /></td><td style='width:10px;'>";

                if (RXServer.Web.RequestValues.SubPage == "Add")
                {
                    list += "<td style='height:34px;'><a href='" + Url + "&SubPage=Add&ViewId=" + parentId + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='" + Url + "&SubPage=Add&ViewId=" + parentId + "' class='submenu_on'>Add Page</a></td>";
                }
                else
                {
                    list += "<td style='height:34px;'><a href='" + Url + "&SubPage=Add&ViewId=" + parentId + "'><img src='../../../App_Themes/WebAdmin/Images/icon_add.gif' class='img_noborder' /></a></td><td style='width:6px;'></td><td><a href='" + Url + "&SubPage=Add&ViewId=" + parentId + "' class='submenu_off'>Add Page</a></td>";
                }
                list += "<td style='width:20px;'></td></tr>";
                list += "</table>";


                this.ltrAdminSubMenuList2.Text = list;
            }
            
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
            // Edit Page
            this.imgError2_1.Visible = false;
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
}
