﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Structure/WebAdmin/WebAdmin.master" CodeFile="GalleryBox2_Admin.aspx.cs" Inherits="Modules_Boxes_Gallery_Gallery_Admin" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content" ContentPlaceHolderId="MainAdminContent" runat="server">

  <telerik:RadProgressManager ID="RadProgressManager2" runat="server" />

    <!-- Script Handler for WebAdmin -->
    <asp:Label ID="lblScript" runat="server"></asp:Label>

    <div id="SubMenu" runat="server" visible="false" class="SubMenu_div">
        <div style="position: relative; float: left;"><asp:Literal ID="ltrSubMenuList1" runat="server" /></div>
        <div style="position: relative; float: right;"><asp:Literal ID="ltrSubMenuList2" runat="server" /></div>
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


    <%--Page 1 :: List all Users--%>
    
    <div id="Page_1" runat="server" visible="false" class="Page_div">
        <asp:Literal ID="ltrEventsList" runat="server" />            
    </div>

 
    <!--  Page2 :: Edit Data --->
    <div id="Page_2" runat="server" visible="false" class="Page_div">
        <span class='Text18_bold_gray'>Edit Gallery</span>
        <br />
        <hr class='line' />
        <br />        
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="Text12_gray">Size:&nbsp;<asp:Image id="imgError1_2" runat="server" Visible="false" ImageUrl="~/App_Themes/WebAdmin/Images/icon_error_small.gif" ToolTip="Error"/></td>
            </tr>
            <tr>
                <td><asp:DropDownList ID="ddlModels" class="form_dropdown_200" runat="server" /></td>
            </tr>
            <tr>
                <td style="height:10px; font-size:1px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="Text12_gray">Align:</td>
            </tr>
            <tr>
                <td class="Text12_gray"><asp:RadioButton ID="rbFloatLeft" runat="server" GroupName="float" />&nbsp;Left&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbFloatRight" runat="server" GroupName="float" />&nbsp;Right</td>
            </tr>
            <tr>
                <td style="height:10px; font-size:1px;">&nbsp;</td>
            </tr>
        </table>
        <br />
        <hr class='line' />    
        <br />
        
        <div style="position:relative; float: right;">
        <asp:Button ID="btnSaveData" runat="server" Text="Save & Close" class="form_button" 
            onclick="btnSaveData_Click" />
        </div>
            
    </div> 
    
    <!--  Page3 :: Edit Image --->
    <div id="Page_3" runat="server" visible="false" class="Page_div">
        <span class='Text18_bold_gray'>Edit Image</span>
        <br />
        <hr class='line' />
        <br />        
        <br />      
        <table cellpadding="0" cellspacing="0" style="">
            <tr>
                <td class="Text12_gray"><asp:CheckBox id="cbShowImage" Checked="true" runat="server" />&nbsp;&nbsp;Visible</td>
            </tr>
            <tr>
                <td style="height:10px; font-size:1px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="Text12_gray">Current Mediafile:</td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td rowspan="8" style="width:200px;" align="right">
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
                                    Allowedfileextensions=".jpeg,.jpg,.gif,.png,.GIF,.JPG,.PNG,.JPEG"
                                    OverwriteExistingFiles="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height:10px; font-size:1px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height:25px;"></td>
                            <td class="Text11_gray"  valign="top">Image Name:</td>
                        </tr>
                        <tr>
                            <td style="height:25px;"></td>
                            <td><asp:TextBox ID="txtImageName" class="form_textbox_200" runat="server" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="Text12_gray">Information</td>
            </tr>
            <tr>
                <td style="height:10px; font-size:1px;">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <telerik:radeditor runat="server" 
                        ID="RadEditor2"
                        Skin="Telerik"
                        ToolsFile="~/App_Themes/RXSK/RadControls/Editor/ToolsFile_Profile.xml"
                        Height="200px"
                        Width="600px"
                        ShowHtmlMode="False" 
                        ShowPreviewMode="False"
                        EditModes="Design"
                        ContentAreaCssFile="~/App_Themes/WebAdmin/radeditor.css">
                        </telerik:radeditor>
                </td>
            </tr>

        </table>
        
        <br />
        <hr class='line' />    
        <br />
        <telerik:RadProgressArea ID="RadProgressArea2" runat="server" skin="Telerik" />
        
        <div id="Error2" runat="server" class="ErrorBox_div" style="visibility:hidden;">
             <table>
                <tr>
                    <td style="height: 20px;" valign="top"><img src="../../../App_Themes/WebAdmin/Images/icon_error_big.gif" title="Errors" /></td>
                    <td style="width: 10px;"></td>
                    <td class="Text12_bold_orange" valign="middle">Oops!</td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td class="Text12_gray"><asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validateRadUpload" CssClass="Text12_gray" /></td>
                </tr>
            </table>
        </div>
        
        
        <div style="position:relative; float: right; margin:10px;">
        <asp:Button ID="btnEditImage" runat="server" Text="Save" class="form_button" onclick="btnEditImage_Click" />
        </div>
        
        <script type="text/javascript">
            function validateRadUpload(source, e) 
            { 
//                e.IsValid = false; 
//                 
//                var upload = $find("<%= RadUpload1.ClientID %>"); 
//                var inputs = upload.getFileInputs(); 
//                document.getElementById("<%=Error2.ClientID%>").style.visibility="hidden";
//                for (var i = 0; i < inputs.length; i++) 
//                { 
//                    //check for empty string or invalid extension 
//                    if (inputs[i].value == "" || !upload.isExtensionValid(inputs[i].value)) 
//                    { 
//                        document.getElementById("<%=Error2.ClientID%>").style.visibility="visible";
//                        return; 
//                    }             
//                } 
                e.IsValid = true; 
            } 
        </script>
            
    </div>
    
     <!--  Page3 :: Add Image --->
    <div id="Page_4" runat="server" visible="false" class="Page_div">
        <span class='Text18_bold_gray'>Add Image(s)</span>
        <br />
        <hr class='line' />
        <br />        
        <br />      
        <table cellpadding="0" cellspacing="0" style="">
            <tr>
                <td style="width:380px; height:25px;" class="Text11_gray"  valign="top">Choose Media:&nbsp;<asp:Image id="Image1" runat="server" Visible="false" ImageUrl="~/App_Themes/WebAdmin/Images/icon_error_small.gif" ToolTip="Error"/></td>
            </tr>
            <tr>
                <td style="height:10px; font-size:1px;">&nbsp;</td>
            </tr>
            <tr>
                <td valign="top">
                       <telerik:RadUpload
                        ID="RadUpload2" runat="server" 
                        Skin="Telerik"
                        MaxFileInputsCount="10" 
                        Allowedfileextensions=".jpeg,.jpg,.gif,.png,.JPG,.GIF,.JPEG,.PNG"
                        OverwriteExistingFiles="true" />
                </td>
            </tr>
            <tr>
                <td style="height:10px; font-size:1px;">&nbsp;</td>
            </tr>
        </table>
        
        <br />
        <hr class='line' />    
        <br />
        
        <telerik:RadProgressArea ID="RadProgressArea1" runat="server" skin="Telerik" />
        
        <div id="Error3" runat="server" class="ErrorBox_div" style="visibility:hidden;">
             <table>
                <tr>
                    <td style="height: 20px;" valign="top"><img src="../../../App_Themes/WebAdmin/Images/icon_error_big.gif" title="Errors" /></td>
                    <td style="width: 10px;"></td>
                    <td class="Text12_bold_orange" valign="middle">Oops!</td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td class="Text12_gray"><asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="validateRadUpload2" CssClass="Text12_gray" /></td>
                </tr>
            </table>
        </div>
                
        <div style="position:relative; float: right; margin-top:10px;">
        <asp:Button ID="btnSaveImage" runat="server" Text="Save" 
            class="form_button" onclick="btnSaveImage_Click" />
        </div>
        
        <script type="text/javascript">
            function validateRadUpload2(source, e) 
            { 
                e.IsValid = false; 
                 
                var upload = $find("<%= RadUpload2.ClientID %>"); 
                var inputs = upload.getFileInputs(); 
                document.getElementById("<%=Error3.ClientID%>").style.visibility="hidden";
                for (var i = 0; i < inputs.length; i++) 
                { 
                    //check for empty string or invalid extension 
                    if (!upload.isExtensionValid(inputs[i].value)) 
                    { 
                        document.getElementById("<%=Error3.ClientID%>").style.visibility="visible";
                        return; 
                    }             
                } 
                e.IsValid = true; 
            } 
        </script>
            
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