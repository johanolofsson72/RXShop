<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GalleryBox2.ascx.cs" Inherits="Modules_Boxes_GalleryBox2_GalleryBox2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div id="GalleryBox2_holder" runat="server">
    <div id="GalleryBox2_admin" runat="server" visible="false">
        <script type="text/javascript">
            //<![CDATA[
            
            function showAdminGalleryBox2(SitId,PagId,ModId,Var)
            {
                var oWnd = $find("<%=RadWindow1_GalleryBox2.ClientID%>");
                var d = new Date();oWnd.SetUrl("http://<% Response.Write(Request.Url.Authority + Request.ApplicationPath); %>/Modules/Boxes/GalleryBox2/GalleryBox2_Admin.aspx?SitId=1&PagId=" + PagId + "&ModId=" + ModId + "&Var=" + Var + "&UID=" + d.getMilliseconds());
                oWnd.show();  
            }

        </script>

        <telerik:RadWindow
          ID ="RadWindow1_GalleryBox2" 
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
    <div id="GalleryBox2">    
    		<script type="text/javascript">
			document.write("<style type='text/css'>div.navigation{width:300px;float: left;}div.content{display:block;}</style>");
		</script>
    
            <div id="Gallery" class="content" style="display:block; float: left;">
				<div id="loading" class="loader"></div>
				<div id="slideshow" class="slideshow"></div>
				<div id="caption" class="embox"></div>
			</div>

        <div id="Gallery_navigation" class="navigation" style="width:100%;float: left;">
            <asp:Literal id="ltrGallery2" runat="server" />   
            </div>
            
		<script type="text/javascript">
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
		</script>
        

    
   </div></div>