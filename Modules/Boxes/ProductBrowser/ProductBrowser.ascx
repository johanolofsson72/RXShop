<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductBrowser.ascx.cs" Inherits="Modules_Boxes_ProductBrowser_ProductBrowser" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register src="wrapper.ascx" tagname="wrapper" tagprefix="uc1" %>

<div id="ProductBrowser_holder" runat="server">
    <div id="ProductBrowser_admin" runat="server" visible="false">

        <script type="text/javascript">
            //<![CDATA[
            
            function showAdminProductBrowser(SitId,PagId,ModId,Var)
            {
                var oWnd = $find("<%=RadWindow1_ProductBrowser.ClientID%>");
                var d = new Date();
                oWnd.SetUrl("Modules/Boxes/ProductBrowser/ProductBrowser_Admin.aspx?SitId=1&PagId=" + PagId + "&ModId=" + ModId + "&Var=" + Var + "&UID=" + d.getMilliseconds());
                oWnd.show();  
            }
            

        </script>

        <telerik:RadWindow
          ID ="RadWindow1_ProductBrowser" 
          VisibleTitlebar="True" 
          VisibleStatusbar="false"
          ReloadOnShow="true"
          IconUrl="~/App_Themes/WebAdmin/Images/window_logo.gif"
         Behaviors="Maximize, Close, Move"
          Skin="Telerik"
          Modal="true"
          Height="600"
          Width="900" 
          Runat = "server">
        </telerik:RadWindow >

        

        <div id="adminbar" style="position:relative; float:left; height:35px;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background-color: #666666; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="img_AdminEdit" ToolTip="Edit" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_edit.gif" runat="server" /></td>
                    <td style="width:1px; font-size:1px;">&nbsp;</td>
                    <td style="background-color: #674343; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="img_AdminDelete" ToolTip="Delete" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_delete_red.gif" runat="server" /></td>
                    <td style="width:1px; font-size:1px;">&nbsp;</td>
                    <td style="background-color: #666666; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="img_AdminMoveUp" ToolTip="MoveUp"
                            ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_moveup.gif" runat="server" onclick="img_AdminMoveUp_Click" /></td>
                    <td style="width:3px; font-size:1px;"><asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_divider.gif" /></td>
                    <td style="background-color: #666666; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="img_AdminMoveDown" ToolTip="MoveDown" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_movedown.gif" runat="server" onclick="img_AdminMoveDown_Click" /></td>
                    <td style="width:1px; font-size:1px;">&nbsp;</td>
                    <td style="background-color: #666666; text-align: center; height:25px; width:24px;">
                        <asp:ImageButton ID="imbAdminVisible" runat="server" onclick="img_AdminVisible_Click" /></td>
                <td style="width:1px; font-size:1px;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="font-size:1px; height:5px; text-align:center;"><asp:Image ID="Image0" runat="server" ImageUrl="~/App_Themes/WebAdmin/Images/adminbar_arrow_gray.gif" /></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
    <div id="ProductBrowser" style="border: none; float:left; width:708px;">
        <h2><asp:Label ID="lblTitle" runat="server" Text="lblTitle" /></h2>   
        <script type="text/javascript">
			//document.write("<style type='text/css'>div.navigation{width:429px; float: left;}div.content{display:block;}</style>");
		</script>
		
		<table cellpadding="0" cellspacing="0" style="border: none;">
		    <tr>
		        <td><img alt="" src='../Images/pixel_trans.gif' border="0" style="width: 429px; height: 0px; border: none 0;" /></td>
		        <td><img alt="" src='../Images/pixel_trans.gif' border="0" style="width: 237px; height: 0px; border: none 0;" /></td>
		    </tr>
		    <tr valign="top">
		        <td><div id="loading" class="loader"></div><div id="slideshow" class="slideshow"></div></td>
		        <td rowspan="4">
		            <table cellpadding="0" cellspacing="0">
		                <tr>
		                    <td colspan="2" style="width: 260px;">
		                        <div id="ProductBrowser_SpecialPrice" visible="false" runat="server" class="ProductBrowser_SpecialPrice">
		                        </div>
		                        <div id="ProductBrowser_OldPrice" visible="false" runat="server" class="ProductBrowser_OldPrice">
		                            <asp:Label ID="lblOldPrice" runat="server">1595:- / st</asp:Label>
		                        </div>
		                        <div id="ProductBrowser_Price">
		                            <asp:Label ID="lblPrice" runat="server">1298:- / st</asp:Label>
		                        </div>
		                        <div id="ProductBrowser_Text">
		                            <asp:Label ID="lblText" runat="server">HJULBULTAR OCH CENTRUMKÅPA INGÅR I PRISET</asp:Label>
		                        </div>
		                    </td>
		                    <td></td>
		                </tr>
		                <tr>
	                        <td valign="top" style="width: 20px; height: 49px; border: none; background-image: url('Images/Modules/Boxes/productbrowser_arrow_middle.gif'); background-repeat: no-repeat;"></td>
	                        <td valign="top" rowspan="4">
	                            <table id="tblSelectionTitle" runat="server" visible="true" cellpadding="0" cellspacing="0" border="0" style="border: none; height: 49px; background-image: url('Images/Modules/Boxes/productbrowser_gray_top.gif'); background-repeat: no-repeat;">
	                                <tr valign="top">
	                                    <td style="padding-left: 5px; padding-top: 15px; padding-bottom: 5px; width: 217px;"><asp:Label ID="lblSelectionsTitle" CssClass="ProductBrowser_SelectionsTitle" runat="server" Text="lblSelectionsTitle" /></td>
	                                </tr>
	                                <tr valign="top">
	                                    <td><img src='../images/pixel_trans.gif' style="width: 1px; height: 5px; border: none;" /></td>
	                                </tr>
	                            </table>
	                            <table cellpadding="0" cellspacing="0" style="background-image: url('<%= tblSelectionTitle.Visible ? "Images/Modules/Boxes/productbrowser_gray_middle.gif" : "Images/Modules/Boxes/productbrowser_gray_without_choices.gif" %>'); background-repeat: repeat-y" width="217px">
	                                <tr>
	                                    <td>
	                                        <div id="ProductBrowser_ChoicesList" runat="server" class="ProductBrowser_ChoicesList">
	                                            <asp:RadioButtonList ID="rdBtnLstChoices" runat="server" Visible="true" />
	                                        </div>
	                                        <div id="ProductBrowser_Quantity">
                                                <asp:Label ID="lblQuantity" runat="server" Text="lblQuantity">ANTAL:</asp:Label>
                                                &nbsp;
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="20px">1</asp:TextBox>
                                            </div>
	                                    </td>
	                                </tr>
	                            </table>
	                            <table cellpadding="0" cellspacing="0">
	                                <tr valign="top">
	                                    <td style="">
	                                        <uc1:wrapper ID="wrapper1" runat="server" />
	                                    </td>
	                                </tr>
	                            </table>
            	                <p>
                                    &nbsp;
                                </p>
	                        </td>
		                </tr>
		            </table>
		        </td>
		    </tr>
		    <tr valign="top">
		        <td valign="top"><div id="Gallery_navigation" class="navigation" style="width: 428px;"><asp:Literal id="ltrGallery2" runat="server" /></div></td>
		    </tr>
		    <tr style="height: 14px; width: 217px;">
		        <td><img src='../images/pixel_trans.gif' style="width: 0px; height: 14px; border: none;" /></td>
		    </tr>
		    <tr valign="top">
		        <td valign="top" style="width: 428px;"><div id="Gallery_tabs" class="navigation" style="width: 428px;"><asp:Literal id="ltrTabs" runat="server" /></div></td>
		    </tr>
		    <tr>
		        <td colspan="2"><img src='../images/pixel_trans.gif' style="width: 0px; height: 20px; border: none;" /></td>
		    </tr>
		</table>
		

		<script type="text/javascript">
		    //TABSET
            $(function()
            {
			    $("#tabset1").buildMbTabset(
			    {
				    stop:function()
				    {
				        if ($("#array").is(":checked")) alert($.mbTabset.mbTabsetArray)
				    },
				    sortable:true
			    });
		    });
		
			// Initially set opacity on thumbs and add
			// additional styling for hover effect on thumbs
			var onMouseOutOpacity = 0.67;
			$('#Gallery_navigation ul.thumbs li').css('opacity', onMouseOutOpacity)
				.hover(
					function () {
						$(this).not('.selected').fadeTo('fast', 1.0);
					}, 
					function () {
						$(this).not('.selected').fadeTo('fast', onMouseOutOpacity);
					}
				);

			$(document).ready(function() {
				// Initialize Advanced Galleriffic Gallery
				var galleryAdv = $('#Gallery').galleriffic('#Gallery_navigation', {
					delay:                  2000,
					numThumbs:              12,
					preloadAhead:           10,
					enableTopPager:         false,
					enableBottomPager:      true,
					imageContainerSel:      '#slideshow',
					controlsContainerSel:   '#controls',
					captionContainerSel:    '#caption',
					loadingContainerSel:    '#loading',
					renderNavControls:      false,
					playLinkText:           'Play Slideshow',
					pauseLinkText:          'Pause Slideshow',
					prevLinkText:           '&lsaquo; Previous Photo',
					nextLinkText:           'Next Photo &rsaquo;',
					nextPageLinkText:       'Next &rsaquo;',
					prevPageLinkText:       '&lsaquo; Prev',
					enableHistory:          false,
					autoStart:              false,
					onChange:               function(prevIndex, nextIndex) {
						$('#Gallery_navigation ul.thumbs').children()
							.eq(prevIndex).fadeTo('fast', onMouseOutOpacity).end()
							.eq(nextIndex).fadeTo('fast', 1.0);
					},
					onTransitionOut:        function(callback) {
						$('#slideshow, #caption').fadeOut('fast', callback);
					},
					onTransitionIn:         function() {
						$('#slideshow, #caption').fadeIn('fast');
					},
					onPageTransitionOut:    function(callback) {
						$('#Gallery_navigation ul.thumbs').fadeOut('fast', callback);
					},
					onPageTransitionIn:     function() {
						$('#Gallery_navigation ul.thumbs').fadeIn('fast');
					}
				});

			});
			
			function getPosition(obj){
                var topValue= 0,leftValue= 0;
                while(obj){
	                leftValue += obj.offsetLeft;
	                topValue += obj.offsetTop;
	                obj = obj.offsetParent;
                }
                finalvalue = leftValue + "," + topValue;
                return finalvalue;
            }
		</script>
        
   </div>
</div>
