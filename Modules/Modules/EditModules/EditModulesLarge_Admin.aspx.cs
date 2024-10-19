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
using System.Xml;
using System.Xml.Linq;
using System.IO;

public partial class Modules_Modules_EditModules_EditModulesLarge_Admin : System.Web.UI.Page
{
    String class_name = "Modules_Modules_EditModules_EditModulesLarge_Admin";

    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            this.ltrHelpText.Text = RXMali.GetXMLHelpNode("EditModules/Text");
            this.SubMenu.Visible = true;
            this.lblHeaderPage1.Text = "Add Boxes";
            this.Page_1.Visible = true;
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void imbAddModule_1_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbAddModule_1_Click";
        try
        {
            Int32 ConPaId, PagId;

            Int32.TryParse(Request["PagId"], out PagId);
            Int32.TryParse(Request["ConPa"], out ConPaId);

            if (PagId > 0 && ConPaId > 0)
            {
                String conPa = "";

                if (ConPaId == 1)
                {
                    conPa = "ContentPane1";
                }
                else if (ConPaId == 2)
                {
                    conPa = "ContentPane2";
                }
                else
                {
                    conPa = "[ SET CONTENTPANE ]";
                }

                RXServer.Modules.TextModule.Create(1, PagId, 71, conPa, 1, 1, false, false);

                this.MessageBox.Visible = true;
                this.ltrMessage.Text = " - Text Module was added to your page.";
            }
            else
            {
                this.ErrorBox.Visible = true;
                this.ltrErrors.Text = " - Text Module wasn't added to your page. Please contact your admin";
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void imbAddModule_2_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbAddModule_2_Click";
        try
        {
            Int32 ConPaId, PagId;

            Int32.TryParse(Request["PagId"], out PagId);
            Int32.TryParse(Request["ConPa"], out ConPaId);

            if (PagId > 0 && ConPaId > 0)
            {
                String conPa = "";

                if (ConPaId == 1)
                {
                    conPa = "ContentPane1";
                }
                else if (ConPaId == 2)
                {
                    conPa = "ContentPane2";
                }
                else
                {
                    conPa = "[ SET CONTENTPANE ]";
                }

                RXServer.Modules.MediaModule.Create(1, PagId, 89, conPa, 1, 1, false, false);

                this.MessageBox.Visible = true;
                this.ltrMessage.Text = " - Media Module was added to your page.";
            }
            else
            {
                this.ErrorBox.Visible = true;
                this.ltrErrors.Text = " - Media Module wasn't added to your page. Please contact your admin";
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void imbAddModule_3_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbAddModule_3_Click";
        try
        {
            Int32 ConPaId, PagId;

            Int32.TryParse(Request["PagId"], out PagId);
            Int32.TryParse(Request["ConPa"], out ConPaId);

            if (PagId > 0 && ConPaId > 0)
            {
                String conPa = "";

                if (ConPaId == 1)
                {
                    conPa = "ContentPane1";
                }
                else if (ConPaId == 2)
                {
                    conPa = "ContentPane2";
                }
                else
                {
                    conPa = "[ SET CONTENTPANE ]";
                }

                RXServer.Modules.MediaModule.Create(1, PagId, 90, conPa, 1, 1, false, false);

                this.MessageBox.Visible = true;
                this.ltrMessage.Text = " -  Image SlideShow Module was added to your page.";
            }
            else
            {
                this.ErrorBox.Visible = true;
                this.ltrErrors.Text = " - Image SlideShow Module wasn't added to your page. Please contact your admin";
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void imbAddModule_4_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbAddModule_4_Click";
        try
        {
            Int32 ConPaId, PagId;

            Int32.TryParse(Request["PagId"], out PagId);
            Int32.TryParse(Request["ConPa"], out ConPaId);

            if (PagId > 0 && ConPaId > 0)
            {
                String conPa = "";

                if (ConPaId == 1)
                {
                    conPa = "ContentPane1";
                }
                else if (ConPaId == 2)
                {
                    conPa = "ContentPane2";
                }
                else
                {
                    conPa = "[ SET CONTENTPANE ]";
                }

                RXServer.Modules.MediaModule.Create(1, PagId, 91, conPa, 1, 1, false, false);

                this.MessageBox.Visible = true;
                this.ltrMessage.Text = " - Image Gallery Module was added to your page.";
            }
            else
            {
                this.ErrorBox.Visible = true;
                this.ltrErrors.Text = " - Image Gallery Module wasn't added to your page. Please contact your admin";
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void imbAddModule_5_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbAddModule_5_Click";
        try
        {
            Int32 ConPaId, PagId;

            Int32.TryParse(Request["PagId"], out PagId);
            Int32.TryParse(Request["ConPa"], out ConPaId);

            if (PagId > 0 && ConPaId > 0)
            {
                String conPa = "";

                if (ConPaId == 1)
                {
                    conPa = "ContentPane1";
                }
                else if (ConPaId == 2)
                {
                    conPa = "ContentPane2";
                }
                else
                {
                    conPa = "[ SET CONTENTPANE ]";
                }

                RXServer.Modules.MediaModule.Create(1, PagId, 96, conPa, 1, 1, false, false);

                this.MessageBox.Visible = true;
                this.ltrMessage.Text = " - Teaser Module was added to your page.";
            }
            else
            {
                this.ErrorBox.Visible = true;
                this.ltrErrors.Text = " - Teaser Module wasn't added to your page. Please contact your admin";
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void imbAddModule_6_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbAddModule_6_Click";
        try
        {
            Int32 ConPaId, PagId;

            Int32.TryParse(Request["PagId"], out PagId);
            Int32.TryParse(Request["ConPa"], out ConPaId);

            if (PagId > 0 && ConPaId > 0)
            {
                String conPa = "";

                if (ConPaId == 1)
                {
                    conPa = "ContentPane1";
                }
                else if (ConPaId == 2)
                {
                    conPa = "ContentPane2";
                }
                else
                {
                    conPa = "[ SET CONTENTPANE ]";
                }

                Int32 newModId = RXServer.Modules.MediaModule.Create(1, PagId, 99, conPa, 1, 1, false, false);


                this.MessageBox.Visible = true;
                this.ltrMessage.Text = " - Contact Us Module was added to your page.";
            }
            else
            {
                this.ErrorBox.Visible = true;
                this.ltrErrors.Text = " - Contact Module wasn't added to your page. Please contact your admin";
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void imbAddModule_7_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbAddModule_7_Click";
        try
        {
            Int32 ConPaId, PagId;

            Int32.TryParse(Request["PagId"], out PagId);
            Int32.TryParse(Request["ConPa"], out ConPaId);

            if (PagId > 0 && ConPaId > 0)
            {
                String conPa = "";

                if (ConPaId == 1)
                {
                    conPa = "ContentPane1";
                }
                else if (ConPaId == 2)
                {
                    conPa = "ContentPane2";
                }
                else
                {
                    conPa = "[ SET CONTENTPANE ]";
                }

                Int32 newModId = RXServer.Modules.MediaModule.Create(1, PagId, 100, conPa, 1, 1, false, false);

                this.MessageBox.Visible = true;
                this.ltrMessage.Text = " - ProductBrowser was added to your page.";
            }
            else
            {
                this.ErrorBox.Visible = true;
                this.ltrErrors.Text = " - ProductBrowser wasn't added to your page. Please contact your admin";
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void imbAddModule_8_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbAddModule_8_Click";
        try
        {
            Int32 ConPaId, PagId;

            Int32.TryParse(Request["PagId"], out PagId);
            Int32.TryParse(Request["ConPa"], out ConPaId);

            if (PagId > 0 && ConPaId > 0)
            {
                String conPa = "";

                if (ConPaId == 1)
                {
                    conPa = "ContentPane1";
                }
                else if (ConPaId == 2)
                {
                    conPa = "ContentPane2";
                }
                else
                {
                    conPa = "[ SET CONTENTPANE ]";
                }

                Int32 newModId = RXServer.Modules.Puffer.Create(1, PagId, 102, conPa, 1, 1, false, false);


                this.MessageBox.Visible = true;
                this.ltrMessage.Text = " - Puffer Module was added to your page.";
            }
            else
            {
                this.ErrorBox.Visible = true;
                this.ltrErrors.Text = " - Puffer wasn't added to your page. Please contact your admin";
            }

        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
    protected void imbAddModule_9_Click(object sender, ImageClickEventArgs e)
    {
        String function_name = "imbAddModule_9_Click";
        try
        {
            Int32 ConPaId, PagId;

            Int32.TryParse(Request["PagId"], out PagId);
            Int32.TryParse(Request["ConPa"], out ConPaId);

            if (PagId > 0 && ConPaId > 0)
            {
                String conPa = "";

                if (ConPaId == 1)
                {
                    conPa = "ContentPane1";
                }
                else if (ConPaId == 2)
                {
                    conPa = "ContentPane2";
                }
                else
                {
                    conPa = "[ SET CONTENTPANE ]";
                }

                Int32 newModId = RXServer.Modules.Puffer.Create(1, PagId, 103, conPa, 1, 1, false, false);


                this.MessageBox.Visible = true;
                this.ltrMessage.Text = " - Category Module was added to your page.";
            }
            else
            {
                this.ErrorBox.Visible = true;
                this.ltrErrors.Text = " - Category wasn't added to your page. Please contact your admin";
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
