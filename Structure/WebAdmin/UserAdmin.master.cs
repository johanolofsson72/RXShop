﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Telerik.Web.UI;

public partial class Structure_UserAdmin_UserAdmin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RXServer.Auth.AuthorizedUser.Identity.Authenticated)
        {
            RXServer.Web.Redirect.To("~/Default.aspx");
        }

    }
}
