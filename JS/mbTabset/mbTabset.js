/*
 *        mbTabset plug in
 * 				developed by Matteo Bicocchi on JQuery
 *        � 2002-2009 Open Lab srl, Matteo Bicocchi
 *			    www.open-lab.com - info@open-lab.com
 *       	version 1.8
 *       	tested on: 	Explorer, FireFox and Chrome for PC
 *                  	FireFox and Safari for Mac Os X
 *                  	FireFox for Linux
 *         MIT (MIT-LICENSE.txt) licenses.
 */



(function($) {

  $.mbTabset={
    mbTabsetArray:[],
    options:{
      container:"",
      item:"a",
      axis:"x",
      sortable:true,
      position:"left",
      start:function(){},
      stop:function(){}
    },
    build: function(opt){
      this.each(function(){
        $(this).addClass("mbTabset");
        var mbTabsetOptions = {};
        $.extend (mbTabsetOptions, $.mbTabset.options);
        var el={el: $(this)};
        $.extend (mbTabsetOptions, el);
        $.extend (mbTabsetOptions, opt);
        $(this).addClass(mbTabsetOptions.position);
        var tabsetContainerID=$(this).attr("id")+"_container";

        $(this).after("<div class='mbTabsetContainer' id='"+tabsetContainerID+"'></div>");
        var tabsetContainer=$("#"+tabsetContainerID);
        var tabs= $(this).find(mbTabsetOptions.item);
        this.opt=mbTabsetOptions;
        this.opt.tabsetContainer=tabsetContainer;
        this.opt.tabs=tabs;
        var hasSel= $(tabs).is(".sel");
        if (!hasSel) $(this).find(mbTabsetOptions.item+":first").addClass("sel");
        if ($.metadata) $.metadata.setType("class");
        $(tabs).each(function(){
          $(this).setAsMbTab(mbTabsetOptions);

        });
        if (mbTabsetOptions.sortable){
          $(this).setSortableMbTabset(mbTabsetOptions);
        }
      });
    },
    setAsTab:function(opt){
      if ($.metadata){
        $.metadata.setType("class");
          if ($(this).metadata().content) $(this).attr("content",$(this).metadata().content);
          if ($(this).metadata().ajaxContent) $(this).attr("ajaxContent",$(this).metadata().ajaxContent);
          if ($(this).metadata().ajaxData) $(this).attr("ajaxData",$(this).metadata().ajaxData);
      }
     if ($(this).hasClass("sel")) $(this).mb_drawAjaxMbTabContent(opt.tabsetContainer);
      $(this).addClass("tab");
      $(this).addClass("mbTab");
      $(this).wrapInner("<span></span>");
      var myContainer=$("#"+$(this).attr("content"));
      myContainer.addClass("tabContent");
      opt.tabsetContainer.append(myContainer);
      myContainer.hide();
      if ($(this).hasClass("sel")) myContainer.show();

      $(this).bind("click",function(){
        $(this).mb_drawAjaxMbTabContent(opt.tabsetContainer);
        if ($(this).is(".disabled , .sel")) return;
        var choosenTab=$(this);
        var actualCont="";
        $(opt.tabs).each(function(){
          if ($(this).is(".sel")){
            actualCont=$(this).attr("content");
            $(this).removeClass("sel");
          }
        });
        $("#"+actualCont).hide();
        choosenTab.addClass("sel");
        $("#"+choosenTab.attr("content")).show();
        
      });
    },
    addTab:function(taboptions){
      var opt = $(this)[0].opt;
      var tabOpt={
        id:"tab_"+$(this).find(opt.item).length+1,
        title:"newTab",
        ajaxContent:"newAjaxContent",
        ajaxData:""
      };
      $.extend (tabOpt, taboptions);
      
      $(this).append("<a id='"+tabOpt.id+"'>"+tabOpt.title+"</a>");
      var tab=$(this).find("#"+tabOpt.id);
      tab.attr("ajaxContent", tabOpt.ajaxContent);
      tab.attr("content", "cont_"+tabOpt.id);
      tab.attr("ajaxData", tabOpt.ajaxData);
      opt.tabs= $(this).find(opt.item);
      tab.setAsMbTab(opt);
      if (opt.sortable)
      $(this).setSortableMbTabset(opt);
    },
    drawAjaxTabContent:function(tabsetContainer){
      if ($(this).attr("ajaxContent")){
        if ($("#"+$(this).attr("content")).html()==null) {
          tabsetContainer.append("<div id='"+$(this).attr("content")+"'> </div>");
        }
        var where=$("#"+$(this).attr("content"));
        $.ajax({
          type: "POST",
          url: $(this).attr("ajaxContent"),
          async: true,
          data: (!$(this).attr("ajaxData"))?"":$(this).attr("ajaxData"),
          success: function(html){
            where.html(html);
          }
        });
      }
    },
    setAjaxTabContent:function(contentUrl, contentData){
      $(this).attr({ajaxContent:contentUrl, ajaxData:contentData});
    },
    toArray:function(el){
      return $(el).sortable("toArray");
    },
    select: function(){
    },
    setSortable:function(opt){
      if (!opt) opt = $(this)[0].opt;
      var tabs= $(this).find(opt.item).not(".block");
      $(tabs).each(function(){
        if($(this).find("i").size()==0){
        $(this).find("span").prepend("<i>&nbsp;</i>").addClass("sortable");
        $(this).find("i").bind("click",function(e){e.preventDefault();return false;});
        };
      });
      $(this).sortable({
        item:opt.item,
        handle:"i",
        cursor:"move",
        revert:false,
        axis:opt.axis,
        opacity:.7,
        forcePlaceholderSize:true,
        start: function(){
          $(this).find(".tab").addClass("floatEl");
          if (opt.start) opt.start();
        },
        stop: function(){
          $(this).find(".tab").removeClass("floatEl");
          $.mbTabset.mbTabsetArray= $.mbTabset.toArray($(this));
          if (opt.stop) opt.stop();
        }
      });
    },
    clearSortable:function(opt){
      if (!opt) opt = $(this)[0].opt;
      var tabs= $(this).find(opt.item);
      $(tabs).each(function(){
        $(this).find("span").removeClass("sortable");
        $(this).find("i").remove();
      });
      $(this).sortable("destroy");
    },
    selectMbTab:function(){
      $(this).click();
    }
  };
  $.fn.setAsMbTab = $.mbTabset.setAsTab;
  $.fn.addMbTab = $.mbTabset.addTab;
  $.fn.setSortableMbTabset = $.mbTabset.setSortable;
  $.fn.mb_drawAjaxMbTabContent = $.mbTabset.drawAjaxTabContent;
  $.fn.mb_setAjaxMbTabContent = $.mbTabset.setAjaxTabContent;
  $.fn.clearSortableMbTabset = $.mbTabset.clearSortable;
  $.fn.buildMbTabset = $.mbTabset.build;
  $.fn.serializeMbTabset = $.mbTabset.serialize;
  $.fn.selectMbTab = $.mbTabset.selectMbTab;
})(jQuery);