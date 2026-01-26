/*!
 * hoverIntent r7 // 2013.03.11 // jQuery 1.9.1+
 * http://cherne.net/brian/resources/jquery.hoverIntent.html
 *
 * You may use hoverIntent under the terms of the MIT license. Basically that
 * means you are free to use hoverIntent as long as this header is left intact.
 * Copyright 2007, 2013 Brian Cherne
 */
 
/* hoverIntent is similar to jQuery's built-in "hover" method except that
 * instead of firing the handlerIn function immediately, hoverIntent checks
 * to see if the user's mouse has slowed down (beneath the sensitivity
 * threshold) before firing the event. The handlerOut function is only
 * called after a matching handlerIn.
 *
 * // basic usage ... just like .hover()
 * .hoverIntent( handlerIn, handlerOut )
 * .hoverIntent( handlerInOut )
 *
 * // basic usage ... with event delegation!
 * .hoverIntent( handlerIn, handlerOut, selector )
 * .hoverIntent( handlerInOut, selector )
 *
 * // using a basic configuration object
 * .hoverIntent( config )
 *
 * @param  handlerIn   function OR configuration object
 * @param  handlerOut  function OR selector for delegation OR undefined
 * @param  selector    selector OR undefined
 * @author Brian Cherne <brian(at)cherne(dot)net>
 */
(function($) {
    $.fn.hoverIntent = function(handlerIn,handlerOut,selector) {

        // default configuration values
        var cfg = {
            interval: 100,
            sensitivity: 7,
            timeout: 500
        };

        if ( typeof handlerIn === "object" ) {
            cfg = $.extend(cfg, handlerIn );
        } else if ($.isFunction(handlerOut)) {
            cfg = $.extend(cfg, { over: handlerIn, out: handlerOut, selector: selector } );
        } else {
            cfg = $.extend(cfg, { over: handlerIn, out: handlerIn, selector: handlerOut } );
        }

        // instantiate variables
        // cX, cY = current X and Y position of mouse, updated by mousemove event
        // pX, pY = previous X and Y position of mouse, set by mouseover and polling interval
        var cX, cY, pX, pY;

        // A private function for getting mouse position
        var track = function(ev) {
            cX = ev.pageX;
            cY = ev.pageY;
        };

        // A private function for comparing current and previous mouse position
        var compare = function(ev,ob) {
            ob.hoverIntent_t = clearTimeout(ob.hoverIntent_t);
            // compare mouse positions to see if they've crossed the threshold
            if ( ( Math.abs(pX-cX) + Math.abs(pY-cY) ) < cfg.sensitivity ) {
                $(ob).off("mousemove.hoverIntent",track);
                // set hoverIntent state to true (so mouseOut can be called)
                ob.hoverIntent_s = 1;
                return cfg.over.apply(ob,[ev]);
            } else {
                // set previous coordinates for next time
                pX = cX; pY = cY;
                // use self-calling timeout, guarantees intervals are spaced out properly (avoids JavaScript timer bugs)
                ob.hoverIntent_t = setTimeout( function(){compare(ev, ob);} , cfg.interval );
            }
        };

        // A private function for delaying the mouseOut function
        var delay = function(ev,ob) {
            ob.hoverIntent_t = clearTimeout(ob.hoverIntent_t);
            ob.hoverIntent_s = 0;
            return cfg.out.apply(ob,[ev]);
        };

        // A private function for handling mouse 'hovering'
        var handleHover = function(e) {
            // copy objects to be passed into t (required for event object to be passed in IE)
            var ev = jQuery.extend({},e);
            var ob = this;

            // cancel hoverIntent timer if it exists
            if (ob.hoverIntent_t) { ob.hoverIntent_t = clearTimeout(ob.hoverIntent_t); }

            // if e.type == "mouseenter"
            if (e.type == "mouseenter") {
                // set "previous" X and Y position based on initial entry point
                pX = ev.pageX; pY = ev.pageY;
                // update "current" X and Y position based on mousemove
                $(ob).on("mousemove.hoverIntent",track);
                // start polling interval (self-calling timeout) to compare mouse coordinates over time
                if (ob.hoverIntent_s != 1) { ob.hoverIntent_t = setTimeout( function(){compare(ev,ob);} , cfg.interval );}

                // else e.type == "mouseleave"
            } else {
                // unbind expensive mousemove event
                $(ob).off("mousemove.hoverIntent",track);
                // if hoverIntent state is true, then call the mouseOut function after the specified delay
                if (ob.hoverIntent_s == 1) { ob.hoverIntent_t = setTimeout( function(){delay(ev,ob);} , cfg.timeout );}
            }
        };

        // listen for mouseenter and mouseleave
        return this.on({'mouseenter.hoverIntent':handleHover,'mouseleave.hoverIntent':handleHover}, cfg.selector);
    };
})(jQuery);

;

/*function computeVisibleHeight ($t) {
        var top = $t.position().top;
        var windowHeight = $j(window).height();
        var scrollTop = $j(window).scrollTop();
        var height = $t.height();

        if (top < scrollTop && height - scrollTop >= windowHeight) {
            // first case: the top and the bottom of the element is outside of the window
            return windowHeight;
        } else if (top < scrollTop) {
            // second: the top is outside of the viewport but the bottom is visible
            return height - (scrollTop - top);
        } else if (top > scrollTop && top + height < windowHeight) {
            // the whole element is visible
            return height;
        } else {
            // the top is visible but the bottom is outside of the viewport
            return windowHeight - (top - scrollTop);
        }
    }
*/

/*
 * jquery.tocible.js v1.1.1, Tocible
 *
 * Copyright 2014 Mark Serbol.   
 * Use, reproduction, distribution, and modification of this code is subject to the terms and 
 * conditions of the MIT license, available at http://www.opensource.org/licenses/MIT.
 *
 * A lightweight jQuery plugin for creating table of contents navigation menu
 * https://github.com/markserbol/tocible
 *
 */
 
;(function($){
  var defaults = {
		heading:'h2',
		subheading:'h3',
		navigation:'nav',
		title:'',
		hash:false,
		offset:50,
		speed:800,
		collapsible:false
  };
		
  $.fn.tocible = function(options){
		var opts = $.extend({}, defaults, options);

		createId = function(idString) {
			idString = idString.replace(/[^\w\s]/gi, ''); // Keep only letters
			idString = idString.replace(/  /gi, ' '); // replace double spaces
			idString = idString.replace(/ /gi, '-'); // add in dashes
			idString = idString.toLowerCase();

			return idString;
		}

		return this.each(function(){
			var wrapper = $(this), nav, heading, subheading, left, oleft; 
			
			nav = $(opts.navigation);

			left = nav.offset().left;
			oleft = left - wrapper.offset().left;
			
			nav.addClass('tocible').html('<ul/>');
			
			wrapper.css({'position':'relative'});
			
			if(opts.title){
				var title = $(opts.title).length ? $(opts.title).text() : opts.title;
				var head = $('<h4/>', {
					'class':'tocible_header', 
					html:'<span/>'+title 
				});
				
				head.prependTo(nav).click(function() {
					$(this).siblings('ul').slideToggle({
					duration:'slow',
					step:contain
					});

					$(this).find('span').toggleClass('toc_open'); 		
				});	
			}
										
			heading = wrapper.find(opts.heading);
			subheading = wrapper.find(opts.subheading);
	
			heading.add(subheading).each(function() {
				var el = $(this), href, title, type, anchor, list;

				el.attr('id', createId( el.text() ) );
				
				href = el.attr('id') ? '#'+el.attr('id'): '#';
				title = el.text();
				
				if(el.is(heading)) {
					type = 'heading';
				} else if(el.is(subheading)) {
					type = 'subheading';
				}
				
				anchor = $('<a/>', {text:title, href:href});				
				list = $('<li/>', {'class':'tocible_'+type});			
				list.append(anchor).appendTo('.tocible > ul');
							
				anchor.click(function(e) {
					e.preventDefault();
					
					var offset = el.offset();
			
					if(opts.hash){
						var winTop = $(window).scrollTop();
			
						if(history.pushState){
							history.pushState({}, document.title, href);
						}else{
							window.location.hash = href;
							$(window).scrollTop(winTop);
						}
					}		  
					$('html, body').stop(true).animate({scrollTop:offset.top - 10}, opts.speed);
				});
				
			});
			
			contain = function(){
				var winTop = $(window).scrollTop(), wrapTop = wrapper.offset().top;
					
				nav.css({'top':opts.offset, 'bottom':'auto'});
				
				if(wrapTop + wrapper.outerHeight() <= winTop + nav.height() + opts.offset){
					nav.css({'position':'absolute', 'bottom':0, 'top':'auto'});
				}else if(winTop >= wrapTop){
					nav.addClass('fixed');
					nav.css({'position':'fixed', 'bottom':'auto', 'top':opts.offset});
				}else{
					nav.removeClass('fixed');
					nav.css({'position':'absolute'});
				}		
			};
			
			onScroll = function(){
				if(opts.collapsible){ $('.tocible li.tocible_subheading').hide(); }
							
				heading.add(subheading).each(function(index) {
					var el = $(this), elTop = el.offset().top, 
					target = $('.tocible li').eq(index),
					winTop = $(window).scrollTop();
			
					if(winTop >= elTop - 20){
						target.addClass('toc_scrolled').siblings().removeClass('toc_scrolled');
						if(opts.collapsible){
							target.siblings().filter('.tocible_subheading').hide();
							if(target.is('.tocible_subheading')){
								target.prevAll('.tocible_heading:first').nextUntil('.tocible_heading').show();
							}else if(target.is('.tocible_heading')){
								target.nextUntil('.tocible_heading').show();
							}
						}
					}else{
						target.removeClass('toc_scrolled');
					}
				});
			};

			$(window).scroll(function() {
				contain();
				onScroll();
			}).trigger('scroll');
					
		});				
  };

$docContent = $('#jive-body-main').find('.jive-content');
if( 
		( $('.jive-rendered-content .tableofcontents').length > 0 ) && 
		( $docContent.find('h2, h3').length > 0 ) 
	) {
	$('body').addClass('has-table-of-contents');

	$sidebar    = $('#jive-body-sidebarcol-container');
	$sidebar.prepend('<div id="tableofcontents-wrapper"><nav id="tableofcontents"></nav></div>');

	$docContent.tocible({
		title:'Inside this Document...',
		navigation:'nav#tableofcontents', 
	    hash: true, //boolean, setting true will enable URL hashing on click
	    offset: 0, //number, top spacing/margin for the navigation
	    speed: 300, //number or string ('slow' & 'fast'), animation speed when anchoring
	    collapsible: true //boolean, auto collapsing sub level heading
	});

	var toc_height = $('#tableofcontents').height();
	//var toc_visible = computeVisibleHeight($('#tableofcontents'));
	//console.log(toc_visible);
	//console.log(toc_height);
	
	//if(toc_height > toc_visible){
	//	toc_height = toc_visible - 80;
	//}
	$('#tableofcontents-wrapper').height( toc_height );
	$('#tableofcontents-wrapper ul').height( toc_height );
	//$('#tableofcontents-wrapper ul').css("max-height", toc_visible + 'px');

	$('nav#tableofcontents').find('.tocible_header').click( function() {
		if ( $(this).find('span').hasClass('toc_open') ) {
			$('#tableofcontents-wrapper').animate({
				height: 28
			}, 600, function() {
				$(this).addClass('closed');	
			});
		} else {
			$('#tableofcontents-wrapper').animate({
				height: toc_height
			}, 600).removeClass('closed');
		}
	});

}


})(jQuery);


;
jive.namespace("sevenSummits");

jive.sevenSummits.modal = jive.oo.Class.extend(function(protect) {
  var $ = $j;
  var defaults = {
		  dataConfig: "modalconfig",
		  dataHandle: ".modalOverlay"
      };
  
  
  protect.init = function(options) {
    var main = this;
    
    this.options = $.extend( {}, defaults, options );
    this._defaults = defaults;
    
    $(this.options.dataHandle).click(function(){
      
      // get data object can either be an object on the element written with javascript or
      // a JSON string written inline in the html. That is the reason for the two checks.
      var data = $.parseJSON($(this).data(main.options.dataConfig));
      if(!data){
        data = $(this).data(main.options.dataConfig);
      }
      
      var type = data.type;
      if("iframe" == type){
        main.iframeOverlay(this, data);
      } else if ("html" == type){
        main.htmlOverlay(this, data)
      } 
    
      return false;
    });
  };
  
  protect.iframeOverlay = function(el, data) {
    var id      = "iframeModal";
    var header  = data.header || "Watch Video";
    var width   = data.width || "auto";
    var height  = data.height || "auto";
    var classes = data.classes || "";
    var body    = '<iframe src="' + (data.url || "") + '" width="' + width + '" height="' + height + '" frameborder="0"></iframe>';
  
    // create/display the modal
    this.modal_me(id, header, body, width, height, classes);
  };
  
  protect.htmlOverlay = function(el, data) {
    var id      = "htmlModal";
    var header  = data.header || "";
    var width   = data.width || "auto";
    var height  = data.height || "auto";
    var classes = data.classes || "";
    var body    = $(data.contentHtml).html() || "";
  
    // create/display the modal
    this.modal_me(id, header, body, width, height, classes);
  };
  
  protect.modal_me = function(id, header, body, width, height, classes, onLoad, onClose) {
    var html = '<div id="' + (id || "") + '" class="jive-modal j-modal ' + (classes || "") + '">';
    html    +=   '<header style="margin-right: 70px;"><h2>' + (header || "") + '</h2></header><a class="j-modal-close-top close" href="#">Close  <span class="j-close-icon j-ui-elem"></span></a>';
    html    +=   '<section class="jive-modal-content clearfix" style="width:' + (width || "auto") + ';height:' + (height || "auto") + ';">' + (body || "") + '</section>';
    html    += '</div>';
    
    $("body").append(html);
    
    $(("#" + id)).lightbox_me({
      closeSelector: ".jive-modal-close, .close",
      centered: true,
      destroyOnClose: true,
      showOverlay: true,
      onLoad: (onLoad && typeof(onLoad) == 'function' ) ? onLoad(this) : function(){},
      onClose: (onClose && typeof(onClose) == 'function' ) ? onClose(this) : function(){}
    });
  };
  
});
;
/**
 * JQuery plugin to display recent videos from a youtube channel. Onclick
 * the videos will be played in jive's default modal window. The integration
 * to the modal window is provided by the jive.sevensummits.modal JS which 
 * is a required include for this plugin to work.
 * 
 * requires jive.sevensummits.modal
 */

;(function ( $, window, document, undefined ) {

    // undefined is used here as the undefined global variable in ECMAScript 3 is
    // mutable (ie. it can be changed by someone else). undefined isn't really being
    // passed in so we can ensure the value of it is truly undefined. In ES5, undefined
    // can no longer be modified.

    // window and document are passed through as local variable rather than global
    // as this (slightly) quickens the resolution process and can be more efficiently
    // minified (especially when both are regularly referenced in your plugin).

    // Create the defaults once
    var pluginName = "youtube",
        defaults = {
            apiVersion: "2",
            maxResults: "5",
            alt: "jsonc",
            isSSL: false,
            url: "http://gdata.youtube.com/feeds/api/users/7summitsagency/uploads",
            urlEmbed: "http://youtube.com/embed/",
            loaderPath: "/themes/generated_advanced_skin_global/images/ajax-loader.gif",
            modalHeaderText: "",
            playerHeight: "400px",
            playerWidth: "500px",
            thumbnail: "sm"
        };

    // The actual plugin constructor
    function Plugin( element, options ) {
        this.element = element;

        // jQuery has an extend method which merges the contents of two or
        // more objects, storing the result in the first object. The first object
        // is generally empty as we don't want to alter the default options for
        // future instances of the plugin
        this.options = $.extend( {}, defaults, options );

        this._defaults = defaults;
        this._name = pluginName;

        this.init();
    }

    Plugin.prototype = {
    	
    	/**
    	 * @function init
    	 * @description make the initial AJAX call to get the recent videos
    	 */
        init: function() {
        	var self = this;
        	
        	// Add the loading class to the container as well as create the loader div 
        	// append it to the container element
        	$(this.element).addClass("loading").html("<div class='loader'>Loading</div>");
        	
        	// Deferred structure to handle the AJAX call that happens
        	// in the getData function
        	$.when(this.getData()).then(function(data){
        		$(self.element).removeClass("loading")
        		self.writeData(data);
        		
        		// initialize the modal window the video will be played in
        		jive.sevenSummits.modal({
        			dataHandle: ".youtubeOverlay"
        		});
        	}, function(){
        		// handle AJAX failed
        		if(window.console){
        		  console.log("AJAX call failed.");
        		}
        	});
        },
        
        /**
         * @function getData
         * @description make the AJAX call to the youtube api to get the recent videos
         * @return jquery deferred
         */
        getData: function() {
        	var d = $.Deferred();
            $.ajax({
              url: ((this.options.isSSL) ? (this.options.url).replace("http://", "https://") : this.options.url) + "?v=" + this.options.apiVersion + "&max-results=" + this.options.maxResults + "&alt=" + this.options.alt,
              dataType: "jsonp"
            }).done(function(data){
              d.resolve(data);
            }).fail(function(){
              d.reject();
            });
            return d.promise();
        },
        
        /**
         * @function writeData
         * @description write the recent video items to the container
         * @param data - returned data from the AJAX youtube api call
         */
        writeData: function(data) {
        	var items = data.data.items;
        	var wrapper = $("<ul class='youtube-list'>"); 
        	for(var x=0, len=items.length; x < len; x++){
        		var item = this.getItemHtml(items[x]);
        		wrapper.append(item);
        	}
        	$(this.element).html(wrapper);
        },
        
        /**
         * @function getItemHtml
         * @description helper function to create the html for a single video item. 
         * @param data - single video item from the AJAX youtube api call
         */
        getItemHtml: function(data){
        	var item = $("<li>"); 
        	
        	// create the anchor tag that will be used by the jive.sevensummits.modal JS
        	// to display the video in Jive's default modal. Note the modalConfig data attr
        	// is what the modal JS uses to display the video.
        	var a = $("<a href='#' class='youtubeOverlay'>"); 
        	var modalConfig = '{"type":"iframe",'
        		+ '"url":"' + (((this.options.isSSL) ? (this.options.urlEmbed).replace("http://", "https://") : this.options.urlEmbed) + data.id) + '"'
        		+ ',"width":"' + this.options.playerWidth + '"'
        		+ ',"height":"' + this.options.playerHeight + '"' 
        		+ ',"header":"' + this.options.modalHeaderText + '"}';
        	a.data("modalconfig", modalConfig);
        	
        	var thumbnail = $("<img>"); 
        	
        	if(this.options.thumbnail == "lg") {
        		thumbnail.attr("src", ((this.options.isSSL) ? (data.thumbnail.hqDefault).replace("http://", "https://") : data.thumbnail.hqDefault));
        		thumbnail.attr("class", "lgThumb")
        	} else {
        		thumbnail.attr("src", ((this.options.isSSL) ? (data.thumbnail.sqDefault).replace("http://", "https://") : data.thumbnail.sqDefault));
        	}
        	
        	var title = $("<p>");
        	title.html(data.title);
        	
        	a.append(thumbnail);
        	a.append(title);
        	
        	item.html(a);
        	return item;
        }
        
    };

    // A really lightweight plugin wrapper around the constructor,
    // preventing against multiple instantiations
    $.fn[pluginName] = function ( options ) {
        return this.each(function () {
            if (!$.data(this, "plugin_" + pluginName)) {
                $.data(this, "plugin_" + pluginName, new Plugin( this, options ));
            }
        });
    };

})( $j, window, document );
;
jive.namespace("sevenSummits");

/**
 * Displays a menu using a dark popover on hover.
 *
 * depends path=/scripts/external/hoverIntent.js
 * depends path=/resources/scripts/jquery/jquery.popover.js
 */

jive.sevenSummits.hoverMenu = jive.oo.Class.extend(function(protect) {
  var $ = $j;
  var defaults = {
    darkpopover: true
  };
  
  
  protect.init = function(hoverTargetHandle, hoverDisplayHandle, options) {
    var main = this;
    var $hoverTargetHandle = $(hoverTargetHandle);
    var $hoverDisplayHandle = $(hoverDisplayHandle);
    
    
    this.options = $.extend( {}, defaults, options );
    this._defaults = defaults;
    
    // set hover intent
    main.initTargetHover($hoverTargetHandle, $hoverDisplayHandle);
    main.initDisplayHover($hoverTargetHandle, $hoverDisplayHandle);
  };
  
  protect.initTargetHover = function($hoverTargetHandle, $hoverDisplayHandle){
    var main = this;
    $hoverTargetHandle.hoverIntent(function(){
      main.onHoverIn($hoverTargetHandle, $hoverDisplayHandle);
    }, function(){
      main.onHoverOut($hoverTargetHandle, $hoverDisplayHandle);
    });
  };
  
  protect.initDisplayHover = function($hoverTargetHandle, $hoverDisplayHandle){
    var main = this;
    $hoverDisplayHandle.hoverIntent(function(){
      main.onHoverIn($hoverTargetHandle, $hoverDisplayHandle);
    }, function(){
      main.onHoverOut($hoverTargetHandle, $hoverDisplayHandle);
    });
  };
  
  protect.onHoverIn = function($hoverTargetHandle, $hoverDisplayHandle){
    var main = this;
    if (!$hoverTargetHandle.data('menu')) {
      $hoverDisplayHandle.popover($.extend({
          context: $hoverTargetHandle,
          onClose: function() {
            $hoverTargetHandle.removeData('menu');
          }
      }, main.options || {}));
      $hoverTargetHandle.data('menu', $hoverDisplayHandle);
    }
  };
  
  protect.onHoverOut = function($hoverTargetHandle, $hoverDisplayHandle){
    var main = this;
    if(!$hoverTargetHandle.is(":hover") && !$hoverDisplayHandle.is(":hover")){
      if($hoverTargetHandle.data('menu')){
        $hoverTargetHandle.data('menu').trigger('close');
        main.initDisplayHover($hoverTargetHandle, $hoverDisplayHandle);
      }
    } else {
      // used to prevent a quick roll through where hoverIntent doesn't catch
      setTimeout(function(){
        if(!$hoverTargetHandle.is(":hover") && !$hoverDisplayHandle.is(":hover")){
          if($hoverTargetHandle.data('menu')){
            $hoverTargetHandle.data('menu').trigger('close');
            main.initDisplayHover($hoverTargetHandle, $hoverDisplayHandle);
          }
        }
      }, 500);
      
    }
  };
  
  
});
;
