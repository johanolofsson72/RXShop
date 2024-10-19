﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Structure/WebAdmin/WebAdmin.master" CodeFile="ArticleBox_Admin.aspx.cs" Inherits="Modules_Boxes_ArticleBox_ArticleBox_Admin" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/Modules/Links/InternalLink/InternalLink.ascx" TagName="InternalLink" TagPrefix="uc1" %>

<asp:Content ID="Content" ContentPlaceHolderId="MainAdminContent" runat="server">



    <!-- Script Handler for WebAdmin -->
    <asp:Label ID="lblScript" runat="server"></asp:Label>

    <div id="SubMenu" runat="server" visible="false" class="SubMenu_div">
        <asp:Literal ID="ltrSubMenuList" runat="server" />
    </div>
    
     <%--Error Box :: List all Errors--%>
    
     <div id="ErrorBox" runat="server" visible="false" class="ErrorBox_div">
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
     
      <%--Success Box :: List all success--%>
    
     <div id="MessageBox" runat="server" visible="false" class="MessageBox_div">
        <table>
            <tr>
                <td style="height: 20px;" valign="top"><img src="../../../App_Themes/WebAdmin/Images/icon_success_small.gif" title="Success" /></td>
                <td style="width: 10px;"></td>
                <td class="Text12_bold_orange" valign="middle">Success!</td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td class="Text12_gray"><asp:Literal ID="ltrMessage" runat="server"/></td>
            </tr>
        </table>
     </div>


 
    <!--  Page1 :: Edit Data --->
    <div id="Page_1" runat="server" visible="false" class="Page_div">
    <telerik:RadAjaxPanel ID="RAP_Admin" runat="server">
        <span class='Text18_bold_gray'>Edit Text</span>
        <br />
        <hr class='line' />
        <br />        
        <table cellpadding="0" cellspacing="0">
           <%-- <tr>
                <td class="Text12_gray"><asp:CheckBox id="cbExtraBar" runat="server" />&nbsp;&nbsp;Show Articlebar</td>
            </tr>
            <tr>
                <td style="height:10px; font-size:1px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="Text12_gray">Title:&nbsp;<asp:Image id="imgError1_2" runat="server" Visible="false" ImageUrl="~/App_Themes/WebAdmin/Images/icon_error_small.gif" ToolTip="Error"/></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtTitle" class="form_textbox_bold_600" runat="server" /></td>
            </tr>
            <tr>
                <td style="height:10px; font-size:1px;">&nbsp;</td>
            </tr>--%>
            <tr>
                <td class="Text12_gray">Header:</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtHeader" class="form_textbox_bold_600" runat="server" /></td>
            </tr>
            <tr>
                <td style="height:10px; font-size:1px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="Text12_gray">Text:</td>
            </tr>
            <tr>
                <td>
                 <telerik:radeditor runat="server" 
                    ID="RadEditor1"
                    Skin="Telerik"
                    ToolsFile="~/App_Themes/equipnor/RadControls/Editor/ToolsFile_Normal.xml"
                    Height="200px"
                    Width="600px"
                    ShowHtmlMode="False" 
                    ShowPreviewMode="False"
                    EditModes="Design"
                    ContentAreaCssFile="~/App_Themes/WebAdmin/radeditor.css" 
                    Allowedfileextensions=".zip"
                    >
                    <ImageManager ViewPaths="~/Upload/ImageManager" UploadPaths="~/Upload/ImageManager" DeletePaths="~/Upload/ImageManager" MaxUploadFileSize="5120000" />
                    <MediaManager ViewPaths="~/Upload/MediaManager" UploadPaths="~/Upload/MediaManager" DeletePaths="~/Upload/MediaManager" MaxUploadFileSize="51200000"/>
                    <FlashManager ViewPaths="~/Upload/FlashManager" UploadPaths="~/Upload/FlashManager" DeletePaths="~/Upload/FlashManager" MaxUploadFileSize="51200000" />
                    <DocumentManager ViewPaths="~/Upload/DocumentManager" UploadPaths="~/Upload/DocumentManager" DeletePaths="~/Upload/DocumentManager" MaxUploadFileSize="10240000"/>
                    </telerik:radeditor>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <span class='Text18_bold_gray'>Edit Image</span>
        <br />
        <hr class='line' />
        <br />        
        <table cellpadding="0" cellspacing="0" style="">
            <tr>
            <tr>
                <td class="Text12_gray">Current Mediafile:</td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td rowspan="9" style="width:200px;" align="right">
                                <table cellpadding="0" cellspacing="0" style="border: solid 1px #CCCCCC;">
                                    <tr>
                                        <td valign="middle" align="center" style="height:200px; width: 200px;">
                                            <asp:Image ID="imgContent" Visible="false" runat="server" />
                                            <asp:Label ID="lblImgText" CssClass="Text11_gray" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:HyperLink ID="hplZoomImg" Visible="false" Target="_blank" runat="server"><img src="../../../App_Themes/WebAdmin/Images/icon_zoom.gif" class="img_noborder" style="margin: 3px;" title="Zoom" /></asp:HyperLink></a>
                            </td>
                            <td style="width:20px;">&nbsp;</td>
                            <td style="width:380px; height:25px;" class="Text11_gray"  valign="top">Choose Media:&nbsp;<asp:Image id="imgError1_1" runat="server" Visible="false" ImageUrl="~/App_Themes/WebAdmin/Images/icon_error_small.gif" ToolTip="Error"/></td>
                        </tr>
                        <tr>
                            <td style="height:25px;"></td>
                            <td valign="top">
                                   <telerik:RadUpload
                                    ID="RadUpload1" runat="server" 
                                    Skin="Telerik"
                                    ControlObjectsVisibility="none"
                                    OverwriteExistingFiles="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height:10px; font-size:1px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height:25px;"></td>
                            <td class="Text11_gray"  valign="top">Media Tooltip: (Only works for images)</td>
                        </tr>
                        <tr>
                            <td style="height:25px;"></td>
                            <td><asp:TextBox ID="txtToolTip" class="form_textbox_200" runat="server" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="Text11_gray"  valign="top"><asp:CheckBox id="cbShowImage" Checked="true" runat="server" />&nbsp;&nbsp;Media visible on page</td>
                        </tr>
                         <tr>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <hr class='line' />    
        <br />
        </telerik:RadAjaxPanel> 
        <div style="position:relative; float: right;">
        <asp:Button ID="btnSaveData" runat="server" Text="Save & Close" class="form_button" 
            onclick="btnSaveData_Click" />
        </div>    
    </div> 


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderId="MainAdminTextContent" runat="server">

    <%--Help Box :: Help Texts--%>
    
     <div id="Help" runat="server" class="Help_div">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="height: 5px; font-size:1px;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 10px;"></td>
                    <td style="height: 20px;"><img src="../../../App_Themes/WebAdmin/Images/icon_help.gif" title="Help" /></td>
                    <td style="width: 10px;"></td>
                    <td class="Text13_bold_gray">Help</td>
                </tr>
            </table>
     </div>
     <div id="Help_Text" runat="server" class="Help_Text_div">
         <div id="Help_Text_main" runat="server" class="Help_Text_main_div">
            <asp:Literal ID="ltrHelpText" runat="server"></asp:Literal>
         </div>
     </div>

</asp:Content>