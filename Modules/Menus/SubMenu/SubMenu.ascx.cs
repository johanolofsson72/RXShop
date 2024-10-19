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
using System.Text;
using System.IO;
using Telerik;
using Telerik.RadMenuUtils;
using Telerik.Web.UI;

public partial class Modules_Menus_SubMenu_SubMenu : RXServer.Lib.RXBaseModule
{
    String class_name = "Modules_Menus_SubMenu_SubMenu";

    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            if (!Page.IsPostBack)
            {
                bindMenu();
            }
            
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void bindMenu()
    {
        String function_name = "bindMenu";
        try
        {
            Int32 pagId = 1;

            if (RXServer.Web.SelectedPages.Level1 > 0)
            {
                pagId = RXServer.Web.SelectedPages.Level1;
            }
            RXServer.Modules.Menu m = new RXServer.Modules.Menu(1, pagId, 1);

            String menu = "";



            if (m.Count > 0)
            {
                menu += "<table cellspacing='0' cellpadding='0'>";
                menu += "<tr>";
                foreach (LiquidCore.Menu.Item mi in m)
                {
                    if (!mi.Hidden || RXServer.Auth.AuthorizedUser.Identity.Authenticated)
                    {
                        if (mi.Id.Equals(RXServer.Web.CurrentValues.PagId) || RXServer.Web.SelectedPages.IsSelected(mi.Id) || mi.Id.Equals(RXServer.Web.SelectedPages.Level2))
                        {
                            menu += "<td class='menu2_left_on'></td>";
                            //menu += "<td class='menu2_middle_on'><a href='" + RXServer.Lib.Common.Dynamic.GetFriendlyUrl(mi.Id) + "' class='menu2_on'>" + mi.Title.ToUpper() + "</a></td>";
                            String path = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
                            menu += "<td class='menu2_middle_on'><a href='" + path + "Default.aspx?PagId=" + mi.Id + "' class='menu2_on'>" + mi.Title.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("sv-SE")) + "</a></td>";
                            menu += "<td class='menu2_right_on'></td>";
                        }
                        else
                        {
                            menu += "<td class='menu2_left_off'></td>";
                            //menu += "<td class='menu2_middle_off'><a href='" + RXServer.Lib.Common.Dynamic.GetFriendlyUrl(mi.Id) + "' class='menu2_off'>" + mi.Title.ToUpper() + "</a></td>";
                            String path = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
                            menu += "<td class='menu2_middle_off'><a href='" + path + "Default.aspx?PagId=" + mi.Id + "' class='menu2_off'>" + mi.Title.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("sv-SE")) + "</a></td>";
                            menu += "<td class='menu2_left_off'></td>";
                        }

                        menu += "<td class='menu2_divider'></td>";
                    }
                }
                menu += "</tr>";
                menu += "</table>";
            }
           
            //menu += "<tr>";
            //menu += "<td style='height: 7px; font-size:1px;'></td>";

            //if (m.Count > 0)
            //{
            //    foreach (LiquidCore.Menu.Item mi in m)
            //    {
            //        if (mi.Id.Equals(RXServer.Web.CurrentValues.PagId) || RXServer.Web.SelectedPages.IsSelected(mi.Id) || mi.Id.Equals(RXServer.Web.SelectedPages.Level2))
            //        {
            //            menu += "<td align='center' valign='bottom'><img src='Images/Site/menu/submenu_arrow.gif' alt='' /></td>";
            //        }
            //        else
            //        {
            //            menu += "<td></td>";
            //        }

            //        menu += "<td></td>";
            //    }
            //}
            //menu += "</tr>";
            


            this.ltrSubMenu.Text = menu;

        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }


}
