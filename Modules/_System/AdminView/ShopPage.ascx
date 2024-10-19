<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShopPage.ascx.cs" Inherits="Modules__System_AdminView_ShopPage" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Label ID="Script" runat="server"></asp:Label>

<div id="ShopPage" runat="server" style="position:relative; float: left; width:100%;">
 
    <div id="ShopSubMenu" runat="server" visible="false" class="SubMenu_div">
        &nbsp;
    </div>
    
     <%--Error Box :: List all Errors--%>
    
     <div id="ErrorBox" runat="server" class="ErrorBox_div">
        <table>
            <tr>
                <td style="height: 20px;" valign="top"><img src="../../../App_Themes/WebAdmin/Images/icon_error_big.gif" title="Errors" /></td>
                <td style="width: 10px;"></td>
                <td class="Text12_bold_orange" valign="middle">Oops!</td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td class="Text12_gray"><asp:Literal ID="ltrErrors" runat="server"/></td>
            </tr>
        </table>
     </div>
    
    <%--Terms of agreement--%>
    
    <div id="TermsOfAgreement" runat="server" visible="true" class="Page_div">
        <span class='Text18_bold_gray'><asp:Label ID="lblHeaderPage3" runat="server">Terms 
        of agreement</asp:Label></span>
        <br />
        <hr class='line' />
        <br />
        <telerik:radeditor runat="server" 
        ID="RadEditor1"
        Skin="Telerik"
        ToolsFile="~/App_Themes/RXSK/RadControls/Editor/ToolsFile_Basic.xml"
        Height="200px"
        Width="600px"
        ShowHtmlMode="False" 
        ShowPreviewMode="False"
        EditModes="Design"
        ContentAreaCssFile="~/App_Themes/WebAdmin/radeditor.css" 
        >
        </telerik:radeditor>
        <br />
        <hr class='line' />    
        <br />
        <div style="position:relative; float: right;">
            <asp:Button ID="btnSaveData" runat="server" Text="Save" class="form_button" 
                onclick="btnSaveData_Click" />
        </div>  
    </div>
</div>