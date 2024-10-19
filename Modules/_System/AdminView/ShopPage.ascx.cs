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
using System.Numeric;

public partial class Modules__System_AdminView_ShopPage : System.Web.UI.UserControl
{
    String class_name = "Modules__System_AdminView_ShopPage";
    String ErrorList = "";
    String Links = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        String function_name = "Page_Load";
        try
        {
            ErrorList = "";
            this.ErrorBox.Visible = false;
            if (RXServer.Web.RequestValues.Page == "Shop")
            {
                this.ShopSubMenu.Visible = true;
                if (!Page.IsPostBack)
                {
                    BindData();
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

    private void BindData()
    {
        String function_name = "BindData";
        try
        {
            String path = ConfigurationManager.AppSettings["Shop.TermsOfAgreementFile"].ToString();
            if (!File.Exists(path))
            {
                StreamWriter sw = File.CreateText(path);
                sw.Close();
            }

            TextReader tr = new StreamReader(path);
            RadEditor1.Content = tr.ReadToEnd();
            tr.Close();
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
            String content = RadEditor1.Content;
            TextWriter tw = new StreamWriter(ConfigurationManager.AppSettings["Shop.TermsOfAgreementFile"].ToString());
            tw.Write(content);
            tw.Close();
        }
        catch (Exception ex)
        {
            this.ErrorBox.Visible = true;
            this.ltrErrors.Text = ex.ToString();
            RXServer.Lib.Error.Report(ex, class_name + ":" + function_name, String.Empty);
        }
    }
}
