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

public partial class Modules_Modules_DeleteModule_DeleteModule_Admin : System.Web.UI.Page
{
    String class_name = "Modules_Modules_DeleteModule_DeleteModule_Admin";

    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            this.SubMenu.Visible = true;
            this.lblHeaderPage1.Text = "Delete Module";
            this.Page_1.Visible = true;
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }

    protected void btnDeleteModule_Click(object sender, EventArgs e)
    {
        String function_name = "btnDeleteModule_Click";
        try
        {
            LiquidCore.Module m = new LiquidCore.Module(RXServer.Web.RequestValues.ModId);
            m.Delete();

            FileStream fs = new FileStream(ConfigurationManager.AppSettings["Shop.ProductsFile"].ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            XmlDocument document = new XmlDocument();
            document.Load(fs);
            fs.Close();
            for (int n = 0; n < document.DocumentElement.ChildNodes.Count; n++)
            {
                if (document.DocumentElement.ChildNodes[n].Attributes[1].Value.Equals(RXServer.Web.RequestValues.ModId.ToString()))
                {
                    document.DocumentElement.RemoveChild(document.DocumentElement.ChildNodes[n]);
                    break;
                }
            }
            document.Save(ConfigurationManager.AppSettings["Shop.ProductsFile"].ToString());

            this.lblScript.Text = "<script language='javascript'>returnToParent();</script>";
        }
        catch (Exception ex)
        {
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }

    }
}
