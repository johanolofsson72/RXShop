<%@ Application Language="C#" %>

<script runat="server">
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

  
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started   
        string sessionId = Session.SessionID;
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    
    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        FriendlyUrl(); //Comment this to de-activate friendly url.
    }

    private void FriendlyUrl()
    {
        HttpContext incoming = HttpContext.Current;
        string path = incoming.Request.Path.ToLower();
        if (!path.ToLower().Contains("Default.aspx".ToLower()) && path.ToLower().EndsWith(".aspx".ToLower()))
        {
            if (!path.ToLower().EndsWith("admin.aspx".ToLower()) && !path.ToLower().EndsWith("bigcart.aspx".ToLower()) && !path.ToLower().EndsWith("checkout.aspx".ToLower()) && !path.ToLower().EndsWith("article.aspx".ToLower()) && !path.Contains("telerik"))
            {
                //Friendly url
                int pageId = FindPageIdFromFriendlyUrl(path);
                incoming.RewritePath("~/Default.aspx?PagId=" + pageId, false);
            }
            else
            {
                //Admin url
            }
        }
        else
        {
            //Normal request or Automatic request from browser
        }
    }
    
    private int FindPageIdFromFriendlyUrl(String path)
    {
        char[] delimitors = new char[1];
        delimitors[0] = '/';
        String[] pathComponents = path.Split(delimitors);
        int siteId = 1; //To whom it may concerne. Should be dynamic.
        int pageId = -1;

        for (int n = 0; n < pathComponents.Length; n++)
        {
            if (!pathComponents[n].Equals(""))
            {
                if (pathComponents[n].ToLower().Contains(".aspx"))
                {
                    pathComponents[n] = pathComponents[n].Remove(pathComponents[n].Length - 5);
                }

                LiquidCore.Pages pages;
                if (pageId == -1)
                {
                    pages = new LiquidCore.Pages();
                }
                else
                {
                    pages = new LiquidCore.Pages(siteId, pageId);
                }

                foreach (LiquidCore.Page page in pages)
                {
                    if (page.Alias.ToLower().Equals(pathComponents[n].ToLower()))
                    {
                        pageId = page.Id;
                        break;
                    }
                }
            }
        }

        return pageId;
    }
       
</script>
