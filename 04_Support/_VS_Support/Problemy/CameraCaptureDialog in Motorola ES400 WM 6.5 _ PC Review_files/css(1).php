@charset "UTF-8";

/* --- bb_code.css --- */

/* .bbCodeX classes are designed to exist inside .baseHtml. ie: they have no CSS reset applied */

.bbCodeBlock
{
	margin: 1em 140px 1em 0;
border: 1px solid rgb(194, 219, 243);
-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;
overflow: auto;

}

	.bbCodeBlock .bbCodeBlock,
	.hasJs .bbCodeBlock .bbCodeSpoilerText,
	.messageList.withSidebar .bbCodeBlock
	{
		margin-right: 0;
	}

	/* mini CSS reset */
	.bbCodeBlock pre,
	.bbCodeBlock blockquote
	{
		margin: 0;
	}
	
	.bbCodeBlock img
	{
		border: none;
	}

.bbCodeBlock .type
{
	font-size: 11px;
font-family: 'open sans', sans-serif, Helvetica, Arial, sans-serif;
color: rgb(77, 149, 221);
background: rgb(194, 219, 243) url('styles/default/xenforo/gradients/form-button-white-25px.png') repeat-x top;
padding: 3px 8px;
border-bottom: 1px solid rgb(142, 188, 234);
-webkit-border-top-left-radius: 4px; -moz-border-radius-topleft: 4px; -khtml-border-top-left-radius: 4px; border-top-left-radius: 4px;
-webkit-border-top-right-radius: 4px; -moz-border-radius-topright: 4px; -khtml-border-top-right-radius: 4px; border-top-right-radius: 4px;

}

.bbCodeBlock pre,
.bbCodeBlock .code
{
	font-size: 10pt;
font-family: Consolas, 'Courier New', Courier, monospace;
background: rgb(219, 234, 249) url('styles/default/xenforo/gradients/category-23px-light.png') repeat-x top;
padding: 10px;
-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;
word-wrap: normal;
overflow: auto;
line-height: 1.24;
min-height: 30px;
max-height: 500px;
_width: 600px;
direction: ltr;

}

.bbCodeBlock .code
{
	white-space: nowrap;
}

.bbCodeQuote
{
	border-color: rgb(142, 188, 234);
overflow: auto;

}

.bbCodeQuote .attribution
{
	color: rgb(20,20,20);
background: rgb(142, 188, 234) url('styles/default/xenforo/gradients/form-button-white-25px.png') repeat-x top;
border-bottom: 1px solid rgb(77, 149, 221);

}

.bbCodeQuote .quoteContainer
{
	overflow: hidden;
	position: relative;
	
	font-style: italic;
font-size: 14px;
background: rgb(219, 234, 249) url('styles/default/xenforo/gradients/category-23px-light.png') repeat-x top;
padding: 10px;
-webkit-border-radius: 4px; -moz-border-radius: 4px; -khtml-border-radius: 4px; border-radius: 4px;

}


	.bbCodeQuote .quoteContainer .quote
	{
		max-height: 150px;
		overflow: hidden;
		padding-bottom: 1px;
	}
	
		.NoJs .bbCodeQuote .quoteContainer .quote
		{
			max-height: none;
		}

	.bbCodeQuote .quoteContainer .quoteExpand
	{		
		display: none;
		-webkit-box-sizing: border-box; -moz-box-sizing: border-box; -ms-box-sizing: border-box; box-sizing: border-box;
		position: absolute;
		height: 80px;
		top: 90px;
		left: 0;
		right: 0;
		
		font-size: 11px;
		line-height: 1;
		text-align: center;
		color: #f9bc6d;
		cursor: pointer;
		padding-top: 65px;
		background: -webkit-linear-gradient(top, rgba(219, 234, 249, 0) 0%, rgb(219, 234, 249) 80%);
		background: -moz-linear-gradient(top, rgba(219, 234, 249, 0) 0%, rgb(219, 234, 249) 80%);
		background: -o-linear-gradient(top, rgba(219, 234, 249, 0) 0%, rgb(219, 234, 249) 80%);
		background: linear-gradient(to bottom, rgba(219, 234, 249, 0) 0%, rgb(219, 234, 249) 80%);
		
		-webkit-border-bottom-left-radius: 4px; -moz-border-radius-bottomleft: 4px; -khtml-border-bottom-left-radius: 4px; border-bottom-left-radius: 4px;
		-webkit-border-bottom-right-radius: 4px; -moz-border-radius-bottomright: 4px; -khtml-border-bottom-right-radius: 4px; border-bottom-right-radius: 4px;
	}
	
	.bbCodeQuote .quoteContainer .quoteExpand.quoteCut
	{
		display: block;
	}
	
	.bbCodeQuote .quoteContainer.expanded .quote
	{
		max-height: none;
	}
	
	.bbCodeQuote .quoteContainer.expanded .quoteExpand
	{
		display: none;
	}


	.bbCodeQuote img
	{
		max-height: 150px;
	}
	
	.bbCodeQuote iframe,
	.bbCodeQuote .fb_iframe_widget,
	.bbCodeQuote object,
	.bbCodeQuote embed
	{
		max-width: 200px;
		max-height: 150px;
	}
	
	.bbCodeQuote iframe:-webkit-full-screen
	{
		max-width: none;
		max-height: none;
	}
	
	.bbCodeQuote iframe:-moz-full-screen
	{
		max-width: none;
		max-height: none;
	}
	
	.bbCodeQuote iframe:-ms-fullscreen
	{
		max-width: none;
		max-height: none;
	}
	
	.bbCodeQuote iframe:fullscreen
	{
		max-width: none;
		max-height: none;
	}
	
.bbCodeSpoilerButton
{
	margin: 5px 0;
	max-width: 99%;
}

	.bbCodeSpoilerButton > span
	{
		display: inline-block;
		max-width: 100%;
		white-space: nowrap;
		text-overflow: ellipsis;
		overflow: hidden;
	}
	
.hasJs .bbCodeSpoilerText
{
	display: none;
	background-color: rgb(219, 234, 249);
padding: 5px;
margin-top: 5px;
margin-right: 140px;
margin-bottom: 5px;
border: 1px solid rgb(194, 219, 243);
-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;
overflow: auto;

}

	.hasJs .bbCodeSpoilerText .bbCodeSpoilerText,
	.hasJs .bbCodeSpoilerText .bbCodeBlock,
	.hasJs .messageList.withSidebar .bbCodeSpoilerText
	{
		margin-right: 0;
	}
	
.NoJs .bbCodeSpoilerContainer
{
	background-color: rgb(20,20,20); /* fallback for browsers without currentColor */
	background-color: currentColor;
}

	.NoJs .bbCodeSpoilerContainer > .bbCodeSpoilerText
	{
		visibility: hidden;
	}

	.NoJs .bbCodeSpoilerContainer:hover
	{
		background-color: transparent;
	}
	
		.NoJs .bbCodeSpoilerContainer:hover > .bbCodeSpoilerText
		{
			visibility: visible;
		}


@media (max-width:980px)
{
	.Responsive .bbCodeBlock,
	.Responsive.hasJs .bbCodeSpoilerText
	{
		margin-right: 0;
	}
}


/* --- discussion_list.css --- */

.discussionList
{
	position: relative;
	zoom: 1;
}

/** column style and width **/

.discussionList .sectionHeaders,
.discussionListItem
{
	display: table;
	table-layout: fixed;
	width: 100%;
	word-wrap: normal;
}

	.discussionList .sectionHeaders dt,
	.discussionList .sectionHeaders dd,
	.discussionListItem .listBlock
	{
		display: table-cell;
		vertical-align: middle;
	}
	
		.discussionList .posterAvatar
		{
			width: 52px;
		}
		
		.discussionList .main
		{
			width: auto;
		}

			.discussionList .sectionHeaders .main .postDate
			{
				text-align: right;
			}
		
		.discussionList .stats
		{
			width: 140px;
		}
			
			.discussionList .sectionHeaders .stats .minor
			{
				text-align: right;
			}
		
		.discussionList .lastPost
		{
			width: 150px;
			text-align: right;
			overflow: hidden;
		}
		
		.discussionList .statsLastPost /* combined last two columns */
		{
			width: 290px;
		}
	

/* column headers */

.discussionList .sectionHeaders
{	
	font-weight: bold;
font-size: 14px;
font-family: 'Exo 2', sans-serif;
color: rgb(85, 85, 85);
padding: 5px 10px;
margin: 3px auto 0;
border-bottom: 3px solid rgb(31, 95, 161);

	
	padding: 0;
}

	.discussionList .sectionHeaders dt span
	{
		display: none !important;
	}
		
	.discussionList .sectionHeaders a
	{
		display: block;
		color: rgb(85, 85, 85);
		outline: none;
	}
	
	.discussionList .sectionHeaders a:hover
	{
		text-decoration: none;
	}
			
	.discussionList .sectionHeaders dd a[href]:hover
	{
		text-decoration: none;
background-color: #fff4e5;

	}
		
	.discussionList .sectionHeaders .main a,
	.discussionList .sectionHeaders .stats a
	{
		float: left;
		width: 50%;
		white-space: nowrap;
	}
	
		.discussionList .sectionHeaders a span
		{
			padding: 5px 10px;
			display: block;
		}
		
/** IE <8 **/
.discussionList .sectionHeaders,
.discussionListItem                { *display: block; _vertical-align: bottom; }
.discussionList .sectionHeaders dt,
.discussionList .sectionHeaders dd,
.discussionListItem .listBlock     { *display: block; *float: left; }
.discussionListItem .listBlock     { _height: 52px; *min-height: 52px; } /* todo: should be calculation */
.discussionList .posterAvatar      { *width: 6.98%; }
.discussionListItem .posterAvatar  { *font-size: 0; }	
.discussionList .main              { *width: 56.98%; }
.discussionList .stats             { *width: 15.97%; }	
.discussionList .lastPost          { *width: 19.97%; }
.discussionList .statsLastPost     { *width: 35.97%; }
.discussionList .sectionHeaders dt,
.discussionList .sectionHeaders dd { *padding: 5px 0; }
.discussionList .sectionHeaders a,
.discussionList .sectionHeaders a span { *display: inline !important; *float: none !important; }

/* items in thread list */

.discussionListItems
{
}



	
/* individual thread list item */
	
/** main **/

.discussionListItem
{
	background-color: rgb(252, 252, 255);
	
	border-bottom: 1px solid rgb(194, 219, 243);
}





	/* sections, section widths */
	
		.discussionListItem .posterAvatar,		
		.discussionListItem .stats
		{
			background-color: rgb(252, 252, 255);
		}
		
		.discussionListItem .main,
		.discussionListItem .lastPost
		{
		}
		
		
		
		
		
		
		
		
	/* avatar section */
	
	.discussionListItem .posterAvatar .avatarContainer
	{
		display: block;
		position: relative;
	}
	
	.discussionListItem .posterAvatar .avatar
	{
		padding: 5px;
display: block;
width: 36px;
height: 36px;

		
		width: auto;
		height: auto;
	}
	
		.discussionListItem .posterAvatar .avatar img
		{
			width: 36px;
			height: 36px;
			display: block;
		}
		
		.discussionListItem .posterAvatar .miniMe
		{
			padding: 0;
position: absolute;
bottom: 1px;
left: 29px;
z-index: 10;

		}
		
			.discussionListItem .posterAvatar .miniMe img
			{
				padding: 1px;
border: 1px none black;
-webkit-border-radius: 2px; -moz-border-radius: 2px; -khtml-border-radius: 2px; border-radius: 2px;
-webkit-box-shadow: 1px 1px 5px rgba(0,0,0, 0.5); -moz-box-shadow: 1px 1px 5px rgba(0,0,0, 0.5); -khtml-box-shadow: 1px 1px 5px rgba(0,0,0, 0.5); box-shadow: 1px 1px 5px rgba(0,0,0, 0.5);
width: 20px;
height: 20px;
				
			}		
		
		
	/* title, poster section */
	
	.discussionListItem .titleText
	{
		padding: 5px;
		overflow: hidden; zoom: 1;
		position: relative;
	}
	
		/* unread indicator */
		
		.LoggedIn .discussionListItem .titleText
		{			
			padding-left: 20px;
		}
		
		.LoggedIn .discussionListItem .unreadLink,
		.LoggedIn .discussionListItem .ReadToggle
		{
			display: block;
			width: 10px;
			height: 10px;
			
			position: absolute;
			left: 5px;
			top: 10px;
			
			background: url('styles/default/xenforo/xenforo-ui-sprite.png') no-repeat 10000px 0;
			
			white-space: nowrap;
			overflow: hidden;
			text-indent: 9999px;
		}
		
			.LoggedIn .discussionListItem .unreadLink,
			.LoggedIn .discussionListItem.unread .ReadToggle
			{
				background-position: -5px -42px;
			}
		
			.LoggedIn .discussionListItem .ReadToggle:hover
			{
				background-position: -25px -42px;
			}
	
		/* first row */
	
		.discussionListItem .title
		{
			font-size: 11pt;
line-height: 18px;

			max-width: 100%;
			word-wrap: break-word;
		}
		
			.discussionListItems .unread .title a
			{
				font-weight: bold;
/*padding-right: 18px;
				background: transparent url('styles/default/xenforo/widgets/discussion-new.png') no-repeat right center;*/

			}
			
		
		.discussionListItem .iconKey span
		{
			margin-left: 5px;
text-indent: -10000em;
float: right;
background: transparent url('styles/default/xenforo/xenforo-ui-sprite.png') no-repeat 10000px 0;
width: 16px;
height: 16px;

		}
			
			.discussionListItem .iconKey .sticky    { background-position:   0px -16px; }
			.discussionListItem .iconKey .starred   { background-position: -90px -32px; width: 18px; height: 18px; }
			.discussionListItem .iconKey .watched   { background-position: -144px -16px; width: 16px; height: 16px; }
			.discussionListItem .iconKey .locked    { background-position: -16px -16px; }
			.discussionListItem .iconKey .moderated { background-position: -32px -16px; }
			.discussionListItem .iconKey .redirect  { background-position: -48px -16px; }
			.discussionListItem .iconKey .new       { background-position: -64px -16px; }
		
		/* second row */
		
		.discussionListItem .secondRow
		{
			font-size: 11px;

			clear: both;
		}
		
			.discussionListItem .secondRow .controls
			{
				float: right;
				padding-left: 20px;
			}
				
				.discussionListItem.AjaxProgress .controls
				{
					background: transparent url('styles/default/xenforo/widgets/ajaxload.info_B4B4DC_facebook.gif') no-repeat left center;
				}
			
			.discussionListItem .posterDate
			{
				float: left;
			}
			
			
			
	/* stats section */
	
	.discussionListItem .stats dl
	{
		padding: 0 10px;
		border-left: 1px solid rgb(194, 219, 243);
		border-right: 1px solid rgb(194, 219, 243);
	}
		.discussionListItem .stats .major
		{
			font-size: 11pt;
line-height: 18px;

			
			margin-top: 5px;
		}
			
		.discussionListItem .stats .minor
		{
			font-size: 11px;

			
			margin-bottom: 5px;
		}
			
	/* last post section */
	
	.discussionListItem .lastPostInfo
	{
		padding: 5px;
	}
	
		.discussionListItem .lastPostInfo .username
		{
			font-size: 11pt;
line-height: 18px;

		}
		
	
			.discussionListItems .unread .lastPostInfo .username
			{
				font-weight: bold;
/*padding-right: 18px;
				background: transparent url('styles/default/xenforo/widgets/discussion-new.png') no-repeat right center;*/

			}
		
		.discussionListItem .lastPostInfo .dateTime
		{
			font-size: 11px;

		}


/* extra note row */

.discussionListItem .noteRow
{
	background-color: rgb(252, 252, 255);
	padding: 5px;
	text-align: center;
	font-size: 11px;
	color: rgb(31, 95, 161);
}

	.discussionListItem .noteRow.secondary
	{
		background-color: rgb(252, 252, 255);
	}




/* deleted item */

.discussionList .discussionListItem.deleted
{
}

	.discussionList .discussionListItem.deleted .avatar img
	{
		opacity: 0.5;
		filter: alpha(opacity='50');
	}
		
	.discussionList .discussionListItem.deleted .deletionNote
	{
		float: left;
		color: rgb(150,0,0);
	}
		
/* moderated item */

.discussionList .discussionListItem.moderated .listBlock
{
	background-color: rgb(252, 252, 255);
}

		
		
		
		
		
			
/* inline mod selected/checked classes */

.discussionListItem.InlineModChecked,
.discussionListItem.InlineModChecked .posterAvatar,
.discussionListItem.InlineModChecked .main,
.discussionListItem.InlineModChecked .stats,
.discussionListItem.InlineModChecked .lastPost,
.discussionListItem.deleted.InlineModChecked,
.discussionListItem.moderated.InlineModChecked
{
	background: rgb(255, 255, 200) url('styles/default/xenforo/gradients/category-23px-light.png') repeat-x top;

}
















		
/** bottom summary **/

.discussionList .sectionFooter
{
	overflow: hidden; zoom: 1;
}

	.discussionList .sectionFooter .contentSummary
	{
		float: left;
		display: block;
	}

		
/** thread list options **/

.DiscussionListOptions
{
	margin: 10px auto;
border: 1px solid rgb(142, 188, 234);
-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;
line-height: 20px;
overflow: hidden;
zoom: 1;

}

.hasJs .DiscussionListOptions
{
	display: none;
}

	.DiscussionListOptions dl,
	.DiscussionListOptions .controlGroup
	{
		float: left;
		margin-right: 10px;
		
		overflow: hidden; zoom: 1;
	}
	
		.DiscussionListOptions dt
		{
			float: left;
		}
		
		.DiscussionListOptions dd
		{
			margin-left: 120px;
		}
		
	.DiscussionListOptions .buttonGroup
	{
		float: right;
	}
	
		.DiscussionListOptions .buttonGroup input
		{
			min-width: 75px;
		}
	
#DiscussionListOptionsHandle
{
	position: absolute;
bottom: -17px;
line-height: 16px;
text-align: center;
right: 50%;
white-space: nowrap;

}
	
	#DiscussionListOptionsHandle a
	{
		font-size: 11px;
color: rgb(142, 188, 234);
background-color: rgb(252, 252, 255);
padding: 0 20px;
margin: 0 auto;
border: 1px solid rgb(142, 188, 234);
border-top: 1px none black;
-webkit-border-bottom-right-radius: 10px; -moz-border-radius-bottomright: 10px; -khtml-border-bottom-right-radius: 10px; border-bottom-right-radius: 10px;
-webkit-border-bottom-left-radius: 10px; -moz-border-radius-bottomleft: 10px; -khtml-border-bottom-left-radius: 10px; border-bottom-left-radius: 10px;
display: inline-block;
outline: 0;
position: relative;
right: -50%;

	}
	
.afterDiscussionListHandle
{
	margin-top: 20px;
}
	
/** item page nav **/

.discussionListItem .itemPageNav
{
	visibility: hidden;
}

.discussionListItem:hover .itemPageNav,
.Touch .discussionListItem .itemPageNav
{
	visibility: visible;
}

	.itemPageNav a,
	.itemPageNav span
	{
		font-size: 9px;
padding: 0 3px;
-webkit-border-radius: 3px; -moz-border-radius: 3px; -khtml-border-radius: 3px; border-radius: 3px;
display: inline-block;
text-align: center;

	}
	
	body .itemPageNav a
	{
		color: #8f6c3f;
background: #fff4e5 url('styles/default/xenforo/gradients/category-23px-light.png') repeat-x;
margin: -1px 0;
border: 1px solid #f9d9b0;

	}
	
	body .itemPageNav a:hover
	{
		color: #8f6c3f;
text-decoration: none;
background-color: #f9d9b0;
border-color: #f9bc6d;

	}

/** filters **/
		
.discussionListFilters
{
	font-size: 11px;
	overflow: hidden; zoom: 1;
}

	.discussionListFilters .filtersHeading
	{
		float: left;
		margin-right: 5px;
		color: rgb(77, 149, 221);
		font-weight: bold;
	}
	
	.discussionListFilters .removeFilter,
	.discussionListFilters .removeAllFilters
	{
		color: rgb(31, 95, 161);
		background: rgb(194, 219, 243) url('styles/default/xenforo/gradients/form-button-white-25px.png') repeat-x top;
		border: 1px solid rgb(194, 219, 243);
		-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;
		padding: 2px 10px;
	}
	
		.discussionListFilters .gadget
		{
			color: rgb(77, 149, 221);
			font-weight: bold;
			margin-left: 3px;
		}

	
		.discussionListFilters .removeFilter:hover,
		.discussionListFilters .removeAllFilters:hover
		{
			background-color: rgb(219, 234, 249);
			text-decoration: none;
			color: rgb(150,150,150);
			-webkit-box-shadow: 1px 1px 5px rgba(0,0,0, 0.15); -moz-box-shadow: 1px 1px 5px rgba(0,0,0, 0.15); -khtml-box-shadow: 1px 1px 5px rgba(0,0,0, 0.15); box-shadow: 1px 1px 5px rgba(0,0,0, 0.15);
		}
		
	.discussionListFilters .pairsInline dt
	{
		display: none;
	}
	
	.discussionListFilters .filterPairs
	{
		float: left;
	}
	
	.discussionListFilters .removeAll
	{
		float: right;
	}
	
		.discussionListFilters .removeAllFilters
		{
			padding: 2px 6px;
		}


@media (max-width:710px)
{
	.Responsive .discussionList .sectionHeaders .stats
	{
		display: none;
	}
	
	.Responsive .discussionList .sectionHeaders .main .title
	{
		float: none;
		width: auto;
	}
	
	.Responsive .discussionList .sectionHeaders .main .postDate
	{
		display: none;
	}
	
	.Responsive .discussionList .statsLastPost
	{
		display: none;
	}

	.Responsive .discussionListItem .listBlock.main,
	.Responsive .discussionListItem .listBlock.stats,
	.Responsive .discussionListItem .listBlock.lastPost
	{
		display: block;
	}
	
	.Responsive .discussionListItem .listBlock.stats
	{
		float: left;
		width: auto;
		margin-top: -5px;
		border: none;
		background: none;
		padding-left: 5px;
	}
	
		.Responsive.LoggedIn .discussionListItem .listBlock.stats
		{			
			padding-left: 20px;
		}
	
		.Responsive .discussionListItem .listBlock.stats dl
		{
			border: none;
			padding: 0;
		}
		
		.Responsive .discussionListItem .listBlock.stats dd,
		.Responsive .discussionListItem .listBlock.stats dt
		{
			float: none;
			display: inline;
		}
	
		.Responsive .discussionListItem .listBlock.stats .minor
		{
			display: none;
		}
		
		.Responsive .discussionListItem .listBlock.stats .major
		{
			font-size: 11px;
			margin-top: 0;
		}
	
	.Responsive .discussionListItem .listBlock.lastPost
	{
		float: right;
		width: auto;
		margin-top: -5px;
	}
		.Responsive .discussionListItem .listBlock.lastPost .lastPostInfo
		{
			padding-top: 0;
			padding-bottom: 0;
		}
	
		.Responsive .discussionListItem .listBlock.lastPost dt
		{
			display: none;
		}
}

@media (max-width:980px)
{
	.Responsive .discussionList .sectionHeaders .lastPost,
	.Responsive .discussionList .lastPost
	{
		width: 125px;
	}
}

@media (max-width:480px)
{
	.Responsive .discussionListItem .secondRow .startDate,
	.Responsive .discussionListItem .secondRow .EditControl
	{
		display: none;
	}
}


/* --- login_bar.css --- */

/** Login bar basics **/

#loginBar
{
	color: rgb(142, 188, 234);
background-color: #032A46;
border-bottom: 1px solid rgb(40, 128, 215);
position: relative;
z-index: 1;

}

	#loginBar .ctrlWrapper
	{
		margin: 0 10px;
	}

	#loginBar .pageContent
	{
		padding-top: 5px;
		position: relative;
		_height: 0px;
	}

	#loginBar a
	{
		color: rgb(77, 149, 221);

	}

	#loginBar form
	{
		padding: 5px 0;
margin: 0 auto;
display: none;
line-height: 20px;
position: relative;

	}
	
		#loginBar .xenForm .ctrlUnit,		
		#loginBar .xenForm .ctrlUnit > dt label
		{
			margin: 0;
			border: none;
		}
	
		#loginBar .xenForm .ctrlUnit > dd
		{
			position: relative;
		}
	
	#loginBar .lostPassword,
	#loginBar .lostPasswordLogin
	{
		font-size: 11px;
	}
	
	#loginBar .rememberPassword
	{
		font-size: 11px;
	}

	#loginBar .textCtrl
	{
		color: rgb(219, 234, 249);
background-color: rgb(14, 62, 110);
border-color: rgb(40, 128, 215);

	}
	
	#loginBar .textCtrl[type=text]
	{
		font-weight: bold;
font-size: 18px;

	}

	#loginBar .textCtrl:-webkit-autofill /* http://code.google.com/p/chromium/issues/detail?id=1334#c35 */
	{
		background: rgb(14, 62, 110) !important;
		color: rgb(219, 234, 249);
	}

	#loginBar .textCtrl:focus
	{
		background: black none;

	}
	
	#loginBar input.textCtrl.disabled
	{
		color: rgb(142, 188, 234);
background-color: #032A46;
border-style: dashed;

	}
	
	#loginBar .button
	{
		min-width: 85px;
		*width: 85px;
	}
	
		#loginBar .button.primary
		{
			font-weight: bold;
		}
		
/** changes when eAuth is present **/

#loginBar form.eAuth
{
	-x-max-width: 700px; /* normal width + 170px */
}

	#loginBar form.eAuth .ctrlWrapper
	{
		border-right: 1px dotted rgb(31, 95, 161);
		margin-right: 200px;
		-webkit-box-sizing: border-box; -moz-box-sizing: border-box; -ms-box-sizing: border-box; box-sizing: border-box;
	}

	#loginBar form.eAuth #eAuthUnit
	{
		position: absolute;
		top: 0px;
		right: 10px;
	}

		#eAuthUnit li
		{
			margin-top: 10px;
			line-height: 0;
		}
	
/** handle **/

#loginBar #loginBarHandle
{
	font-size: 11px;
color: rgb(219, 234, 249);
background-color: #032A46;
padding: 0 10px;
margin-right: 20px;
-webkit-border-bottom-right-radius: 10px; -moz-border-radius-bottomright: 10px; -khtml-border-bottom-right-radius: 10px; border-bottom-right-radius: 10px;
-webkit-border-bottom-left-radius: 10px; -moz-border-radius-bottomleft: 10px; -khtml-border-bottom-left-radius: 10px; border-bottom-left-radius: 10px;
position: absolute;
right: 0px;
bottom: -20px;
text-align: center;
z-index: 1;
line-height: 20px;
-webkit-box-shadow: 0px 2px 5px #032A46; -moz-box-shadow: 0px 2px 5px #032A46; -khtml-box-shadow: 0px 2px 5px #032A46; box-shadow: 0px 2px 5px #032A46;

}


@media (max-width:980px)
{
	.Responsive #loginBar form.eAuth .ctrlWrapper
	{
		border-right: none;
		margin-right: 10px;
	}

	.Responsive #loginBar form.eAuth #eAuthUnit
	{
		position: static;
		width: 180px;
		margin: 0 auto 10px;
	}
}


/* --- message.css --- */



.messageList
{
	
}

.messageList .message
{
	padding-top: 10px;
padding-bottom: 30px;
border-top: 1px solid rgb(194, 219, 243);

}

/* clearfix */ .messageList .message { zoom: 1; } .messageList .message:after { content: '.'; display: block; height: 0; clear: both; visibility: hidden; }

/*** Message block ***/

.message .messageInfo
{
	padding: 0;
margin-left: 140px;
border-bottom: 1px none black;

	zoom: 1;
}

	.message .newIndicator
	{
		font-size: 11px;
color: rgb(31, 95, 161);
background: rgb(77, 149, 221) url('styles/default/xenforo/gradients/form-button-white-25px.png') repeat-x top;
padding: 1px 5px;
margin: -5px -5px 5px 5px;
border: 1px solid rgb(77, 149, 221);
-webkit-border-radius: 3px; -moz-border-radius: 3px; -khtml-border-radius: 3px; border-radius: 3px;
-webkit-border-top-right-radius: 0px; -moz-border-radius-topright: 0px; -khtml-border-top-right-radius: 0px; border-top-right-radius: 0px;
display: block;
float: right;
position: relative;
-webkit-box-shadow: 1px 1px 3px rgba(0,0,0, 0.25); -moz-box-shadow: 1px 1px 3px rgba(0,0,0, 0.25); -khtml-box-shadow: 1px 1px 3px rgba(0,0,0, 0.25); box-shadow: 1px 1px 3px rgba(0,0,0, 0.25);

		
		margin-right: -25px;
	}
	
		.message .newIndicator span
		{
			background-color: rgb(77, 149, 221);
-webkit-border-top-right-radius: 3px; -moz-border-radius-topright: 3px; -khtml-border-top-right-radius: 3px; border-top-right-radius: 3px;
position: absolute;
top: -4px;
right: -1px;
width: 5px;
height: 4px;

		}

	.message .messageContent
	{
		padding-bottom: 2px;
min-height: 100px;
overflow: hidden;
*zoom: 1;

	}
	
	.message .messageTextEndMarker
	{
		height: 0;
		font-size: 0;
		overflow; hidden;
	}
	
	.message .editDate
	{
		text-align: right;
		margin-top: 5px;
		font-size: 11px;
		color: rgb(150,150,150);
	}

	.message .signature
	{
		font-size: 9pt;
color: rgb(150,150,150);
padding: 5px 0 0;
margin-top: 5px;
border-top: 1px dashed rgb(142, 188, 234);

	}

	.message .messageMeta
	{
		font-size: 12px;
padding: 15px 5px 5px;
margin: -5px;
overflow: hidden;
zoom: 1;

	}

		.message .privateControls
		{
			float: left;

		}

		.message .publicControls
		{
			float: right;

		}
		
			.message .privateControls .item
			{
				margin-right: 10px;
				float: left;
			}

				.message .privateControls .item:last-child
				{
					margin-right: 0;
				}

			.message .publicControls .item
			{
				margin-left: 10px;
				float: left;
			}
	
				.message .messageMeta .control
				{
					
				}
				
					.message .messageMeta .control:focus
					{
						
					}
				
					.message .messageMeta .control:hover
					{
						
					}
				
					.message .messageMeta .control:active
					{
						
					}
	/*** multiquote +/- ***/
			
	.message .publicControls .MultiQuoteControl
	{
		padding-left: 4px;
		padding-right: 4px;
		-webkit-border-radius: 2px; -moz-border-radius: 2px; -khtml-border-radius: 2px; border-radius: 2px;
		margin-left: 6px;
		margin-right: -4px;
	}
	
	
	.message .publicControls .MultiQuoteControl.active
	{
		background-color: rgb(194, 219, 243);
	}
	
		.messageNotices li
	{
		font-size: 11px;
background: #f9d9b0 url('styles/default/xenforo/gradients/form-button-white-25px.png') repeat-x top;
padding: 5px;
margin: 10px 0;
border: 1px solid #f9d9b0;
-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;
line-height: 16px;

	}
	
		.messageNotices .icon
		{
			float: right;
			width: 16px;
			height: 16px;
			background: url('styles/default/xenforo/xenforo-ui-sprite.png') no-repeat 1000px 1000px;
		}
	
			.messageNotices .warningNotice .icon { background-position: -48px -32px; }		
			.messageNotices .deletedNotice .icon { background-position: -64px -32px; }		
			.messageNotices .moderatedNotice .icon {background-position: -32px -16px; }
	
	.message .likesSummary
	{
		padding: 5px;
margin-top: 10px;
border: 1px solid rgb(194, 219, 243);
-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;

	}
	
	.message .messageText > *:first-child
	{
		margin-top: 0;
	}

/* inline moderation changes */

.InlineModChecked .messageUserBlock,
.InlineModChecked .messageInfo,
.InlineModChecked .messageNotices,
.InlineModChecked .bbCodeBlock .type,
.InlineModChecked .bbCodeBlock blockquote,
.InlineModChecked .attachedFiles .attachedFilesHeader,
.InlineModChecked .attachedFiles .attachmentList
{
	background: rgb(255, 255, 200) url('styles/default/xenforo/gradients/category-23px-light.png') repeat-x top;

}

.InlineModChecked .messageUserBlock div.avatarHolder,
.InlineModChecked .messageUserBlock .extraUserInfo
{
	background: transparent;
}

.InlineModChecked .messageUserBlock .arrow span
{
	border-left-color: rgb(255, 255, 200);
}

/* message list */

.messageList .newMessagesNotice
{
	margin: 10px auto;
	padding: 5px 10px;
	-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;
	border: 1px solid rgb(142, 188, 234);
	background: rgb(194, 219, 243) url(styles/default/xenforo/gradients/category-23px-light.png) repeat-x top;
	font-size: 11px;
}

/* deleted / ignored message placeholder */

.messageList .message.placeholder
{
}

.messageList .placeholder .placeholderContent
{	
	overflow: hidden; zoom: 1;
	color: rgb(40, 128, 215);
	font-size: 11px;
}

	.messageList .placeholder a.avatar
	{
		float: left;
		display: block;
	}
	
		.messageList .placeholder a.avatar img
		{
			display: block;
			width: 32px;
			height: 32px;
		}
		
	.messageList .placeholder .privateControls
	{
		margin-top: -5px;
	}
	

/* messages remaining link */

.postsRemaining a,
a.postsRemaining
{
	font-size: 11px;
	color: rgb(150,150,150);
}


@media (max-width:980px)
{
	.Responsive .message .newIndicator
	{
		margin-right: 0;
		-webkit-border-top-right-radius: 3px; -moz-border-radius-topright: 3px; -khtml-border-top-right-radius: 3px; border-top-right-radius: 3px;
	}
	
		.Responsive .message .newIndicator span
		{
			display: none;
		}
}

@media (max-width:480px)
{
	.Responsive .message .messageAdvertising
	{
		display: none;
	}
	.Responsive .message .messageInfo
	{
		margin-left: 0;
		padding: 0 10px;
	}

	.Responsive .message .messageContent
	{
		min-height: 0;
	}	

	.Responsive .message .newIndicator
	{
		margin-right: -5px;
		margin-top: -16px;
	}

	.Responsive .message .postNumber,
	.Responsive .message .authorEnd
	{
		display: none;
	}
	
	.Responsive .message .signature
	{
		display: none;
	}
	
	.Responsive .messageList .placeholder a.avatar
	{
		margin-right: 10px;
	}
}


/* --- message_user_info.css --- */

.messageUserInfo
{
	float: left;
width: 124px;

}

	.messageUserBlock
	{
		background: rgb(194, 219, 243) url('styles/default/xenforo/gradients/tab-selected-light.png') repeat-x bottom;
border: 1px solid rgb(194, 219, 243);
-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;

		
		position: relative;
	}
		
		.messageUserBlock div.avatarHolder
		{
			background-color: rgb(219, 234, 249);
padding: 10px;
-webkit-border-radius: 4px; -moz-border-radius: 4px; -khtml-border-radius: 4px; border-radius: 4px;

			
			position: relative;	
		}
		
			.messageUserBlock div.avatarHolder .avatar
			{
				display: block;
				font-size: 0;
			}
			
			.messageUserBlock div.avatarHolder .onlineMarker
			{
				position: absolute;
				top: 9px;
				left: 9px;
				
				border: 7px solid transparent;
border-top-color: rgb(127, 185, 0);
border-left-color: rgb(127, 185, 0);
-webkit-border-top-left-radius: 5px; -moz-border-radius-topleft: 5px; -khtml-border-top-left-radius: 5px; border-top-left-radius: 5px;
-webkit-border-top-right-radius: 3px; -moz-border-radius-topright: 3px; -khtml-border-top-right-radius: 3px; border-top-right-radius: 3px;
-webkit-border-bottom-left-radius: 3px; -moz-border-radius-bottomleft: 3px; -khtml-border-bottom-left-radius: 3px; border-bottom-left-radius: 3px;

			}
			
		.messageUserBlock h3.userText
		{
			padding: 6px;

		}
		
		.messageUserBlock .userBanner
		{
			display: block;
			margin-bottom: 5px;
			margin-left: -12px;
			margin-right: -12px;
		}
		
		.messageUserBlock .userBanner:last-child
		{
			margin-bottom: 0;
		}
	
		.messageUserBlock a.username
		{
			font-weight: bold;
display: block;
overflow: hidden;
line-height: 16px;

			
		}
		
		.messageUserBlock .userTitle
		{
			font-size: 11px;
display: block;

		}
		
		.messageUserBlock .extraUserInfo
		{
			font-size: 11px;
background-color: rgb(219, 234, 249);
padding: 4px 6px;
-webkit-border-radius: 4px; -moz-border-radius: 4px; -khtml-border-radius: 4px; border-radius: 4px;

		}
		
			.messageUserBlock .extraUserInfo dl
			{
				margin: 2px 0 0;
			}
							
			.messageUserBlock .extraUserInfo img
			{
				max-width: 100%;
			}
		
		.messageUserBlock .arrow
		{
			position: absolute;
			top: 10px;
			right: -10px;
			
			display: block;
			width: 0px;
			height: 0px;
			line-height: 0px;
			
			border: 10px solid transparent;
			border-left-color: rgb(194, 219, 243);
			-moz-border-left-colors: rgb(194, 219, 243);
			border-right: none;
			
			/* Hide from IE6 */
			_display: none;
		}
		
			.messageUserBlock .arrow span
			{
				position: absolute;
				top: -10px;
				left: -11px;
				
				display: block;
				width: 0px;
				height: 0px;
				line-height: 0px;
				
				border: 10px solid transparent;
				border-left-color: rgb(219, 234, 249);
				-moz-border-left-colors: rgb(219, 234, 249);
				border-right: none;
			}


@media (max-width:480px)
{
	.Responsive .messageUserInfo
	{
		float: none;
		width: auto; 
	}

	.Responsive .messageUserBlock
	{
		overflow: hidden;
		margin-bottom: 5px;
		position: relative;
	}

	.Responsive .messageUserBlock div.avatarHolder
	{
		float: left;
		padding: 5px;
	}

		.Responsive .messageUserBlock div.avatarHolder .avatar img
		{
			width: 48px;
			height: 48px;
		}
		
		.Responsive .messageUserBlock div.avatarHolder .onlineMarker
		{
			top: 4px;
			left: 4px;
			border-width: 6px;
		}

	.Responsive .messageUserBlock h3.userText
	{
		margin-left: 64px;
	}
	
	.Responsive .messageUserBlock .userBanner
	{
		max-width: 150px;
		margin-left: 0;
		margin-right: 0;
		-webkit-border-top-left-radius: 3px; -moz-border-radius-topleft: 3px; -khtml-border-top-left-radius: 3px; border-top-left-radius: 3px;
		-webkit-border-top-right-radius: 3px; -moz-border-radius-topright: 3px; -khtml-border-top-right-radius: 3px; border-top-right-radius: 3px;
		position: static;
		display: inline-block;
	}
	
		.Responsive .messageUserBlock .userBanner span
		{
			display: none;
		}

	.Responsive .messageUserBlock .extraUserInfo
	{
		display: none;
	}

	.Responsive .messageUserBlock .arrow
	{
		display: none;
	}
}


/* --- nat_public_css.css --- */

.natMenuLevel0
{
	padding-left: 0px;
}

.natMenuLevel1
{
	padding-left: 10px;
}

.natMenuLevel2
{
	padding-left: 20px;
}

.natMenuLevel3
{
	padding-left: 30px;
}

.natMenuLevel4
{
	padding-left: 40px;
}

.natMenuLevel5
{
	padding-left: 50px;
}

.natMenuLevel6
{
	padding-left: 60px;
}

.natMenuLevel7
{
	padding-left: 70px;
}

.natMenuLevel8
{
	padding-left: 80px;
}

.natMenuLevel9
{
	padding-left: 90px;
}

.natMenuLevel10
{
	padding-left: 100px;
}



/* STYLING TO MAKE COLUMN MENUS WORK */
div.natJSMenuColumns
{
	background-color: rgb(252, 252, 255);
	background-color: rgba(252, 252, 255, 0.96);
}

/* STYLING TO MAKE COLUMN MENUS WORK */
div.natJSMenuColumns ul
{
	float: left;
	border-bottom: 0px;
	background-color: transparent !important;
	max-height: none !important;
}







/* --- similar_threads.css --- */

.similarThreads dd.main span
{
	padding: 5px 10px;
	display: block;
}

.main.dismiss
{
	width: 24px;
}

.main.dismiss img
{
	width: 22px;
	cursor: pointer;
}

/* --- thread_view.css --- */

.thread_view .threadAlerts
{
	border: 1px solid rgb(194, 219, 243);
	-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;
	font-size: 11px;
	margin: 10px 0;
	padding: 5px;
	line-height: 16px;
	background-image: url('styles/default/xenforo/gradients/form-button-white-25px.png');
}
	
	.thread_view .threadAlerts dt
	{
		color: rgb(77, 149, 221);
		display: inline;
	}
	
	.thread_view .threadAlerts dd
	{
		color: rgb(14, 62, 110);
		font-weight: bold;
		display: inline;
	}
	
		.thread_view .threadAlerts .icon
		{
			float: right;
			width: 16px;
			height: 16px;
			margin-left: 5px;
			background: url('styles/default/xenforo/xenforo-ui-sprite.png') no-repeat -1000px -1000px;
		}
		
			.thread_view .threadAlerts .deletedAlert .icon { background-position: -64px -32px; }
			.thread_view .threadAlerts .moderatedAlert .icon { background-position: -32px -16px; }
			.thread_view .threadAlerts .lockedAlert .icon { background-position: -16px -16px; }
	
.thread_view .threadAlerts + * > .messageList
{
	border-top: none;
}

.thread_view .threadNotices
{
	background-color: rgb(219, 234, 249);
	border: 1px solid rgb(142, 188, 234);
	-webkit-border-radius: 5px; -moz-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px;
	padding: 10px;
	margin: 10px auto;
}

.thread_view .InlineMod
{
	overflow: hidden; zoom: 1;
}

/* --- wf_default.css --- */



.widget-container.sidebar.act-as-sidebar
{
	float: none;
font-size: inherit;
width: auto;

	display: inline;
}

.sidebar-widget
{
	
}

.hook-widget
{
	
}

.widget-rows > div
{
	margin-top: 5px;
}

.widget-tabs
{
	padding: 0;
margin: 0;
border: 0 none black;

}
	.widget-tabs .tabs
	{
		
	}

	.widget-tabs .widget-panes
	{
		padding: 10px;
border-right: 1px solid rgb(142, 188, 234);
border-bottom: 1px solid rgb(142, 188, 234);
border-left: 1px solid rgb(142, 188, 234);
-webkit-border-bottom-right-radius: 10px; -moz-border-radius-bottomright: 10px; -khtml-border-bottom-right-radius: 10px; border-bottom-right-radius: 10px;
-webkit-border-bottom-left-radius: 10px; -moz-border-radius-bottomleft: 10px; -khtml-border-bottom-left-radius: 10px; border-bottom-left-radius: 10px;

	}

.widget-columns
{
	display: table;
	table-layout: fixed;
	width: 100% !important;
}
.columns-container
{
	display: table-row;
}
.widget-column
{
	display: table-cell;
}


.widget-columns > .columns-container > .widget-column > div
{
	margin-left: 5px;
}
.widget-columns > .columns-container > .widget-column.column-1 > div
{
	margin-left: 0;
}
.widget-columns > .columns-container > .widget-column
{
	border-left: 1px solid rgb(142, 188, 234);
	margin-left: -1px !important;
}
.widget-columns > .columns-container > .widget-column.column-1
{
	border-left: 0;
	margin-left: 0 !important;
}

.widget-rows > div
{
	border-top: 1px solid rgb(142, 188, 234);
	margin-top: 5px;
	padding-top: 5px;
}
.widget-rows > div.row-1
{
	border-top: 0;
	margin-top: 0;
	padding-top: 0;
}


.hook-widget > h3
{
	color: rgb(31, 95, 161);
	font-size: 1.1em;
	font-weight: bold;
}

.widget .avatarHeap
{
	margin-right: -10px;
}

.WidgetFramework_WidgetRenderer_ProfilePosts_ProfilePostItem
{
	overflow: hidden; zoom: 1;

	margin: 5px 0;
	padding-top: 5px;
	border-top: 1px solid rgb(194, 219, 243);
}

.WidgetFramework_WidgetRenderer_ProfilePosts_ProfilePostItem:first-child
{
	border-top: none;
	padding-top: 0;
}

.WidgetFramework_WidgetRenderer_ProfilePosts_ProfilePostItem .avatar
{
	float: left;
	font-size: 0;
}

	.WidgetFramework_WidgetRenderer_ProfilePosts_ProfilePostItem .avatar img
	{
		width: 24px;
		height: 24px;
	}

.WidgetFramework_WidgetRenderer_ProfilePosts_ProfilePostItem .messageInfo
{
	margin-left: 34px;
}

.WidgetFramework_WidgetRenderer_ProfilePosts_ProfilePostItem .messageContent article,
.WidgetFramework_WidgetRenderer_ProfilePosts_ProfilePostItem .messageContent blockquote
{
	display: inline;
}

.WidgetFramework_WidgetRenderer_ProfilePosts_ProfilePostItem .poster
{
	font-weight: bold;
}

.WidgetFramework_WidgetRenderer_ProfilePosts_ProfilePostItem .messageMeta
{
	overflow: hidden; zoom: 1;
	font-size: 11px;
	line-height: 14px;
	padding-top: 4px;
}

.widget .statusPoster textarea {
	-webkit-box-sizing: border-box; -moz-box-sizing: border-box; -ms-box-sizing: border-box; box-sizing: border-box;
	width: 100%;
	margin: 0;
	resize: vertical;
	overflow: hidden;
}
.WidgetFramework_WidgetRenderer_Threads .unread > a
{
	font-weight: bold;
}
.WidgetFramework_WidgetRenderer_Threads .avatarList .username
{
	display: inline;
	font-size: inherit;
	margin: 0;
}

.WidgetFramework_WidgetRenderer_Threads .discussionList
{
	overflow: hidden;
	width: 100%;
}
.WidgetFramework_WidgetRenderer_Threads .DiscussionList
{
	min-width: 450px;
	width: 100%;
}

.WidgetFramework_WidgetRenderer_Threads_FullThreadList
{
}
	.WidgetFramework_WidgetRenderer_Threads_FullThreadList .subHeading
	{
		font-size: 1.3em;
	}
	.WidgetFramework_WidgetRenderer_Threads_FullThreadList .info
	{
		padding: 5px 10px;

		
			border-top: 1px dashed rgb(194, 219, 243);
		
	}
		.WidgetFramework_WidgetRenderer_Threads_FullThreadList .counters
		{
			float: right;
		}
	.WidgetFramework_WidgetRenderer_Threads_FullThreadList .message
	{
		margin-bottom: 10px;
	}
	/* clearfix */ .WidgetFramework_WidgetRenderer_Threads_FullThreadList .message { zoom: 1; } .WidgetFramework_WidgetRenderer_Threads_FullThreadList .message:after { content: '.'; display: block; height: 0; clear: both; visibility: hidden; }
		.WidgetFramework_WidgetRenderer_Threads_FullThreadList .messageInfo
		{
			margin: 0;
			padding: 5px 10px;
		}
		.WidgetFramework_WidgetRenderer_Threads_FullThreadList .message .newIndicator
		{
			font-size: 11px;
color: rgb(31, 95, 161);
background: rgb(77, 149, 221) url('styles/default/xenforo/gradients/form-button-white-25px.png') repeat-x top;
padding: 1px 5px;
margin: -5px -5px 5px 5px;
border: 1px solid rgb(77, 149, 221);
-webkit-border-radius: 3px; -moz-border-radius: 3px; -khtml-border-radius: 3px; border-radius: 3px;
-webkit-border-top-right-radius: 0px; -moz-border-radius-topright: 0px; -khtml-border-top-right-radius: 0px; border-top-right-radius: 0px;
display: block;
float: right;
position: relative;
-webkit-box-shadow: 1px 1px 3px rgba(0,0,0, 0.25); -moz-box-shadow: 1px 1px 3px rgba(0,0,0, 0.25); -khtml-box-shadow: 1px 1px 3px rgba(0,0,0, 0.25); box-shadow: 1px 1px 3px rgba(0,0,0, 0.25);


			margin-right: -25px;
		}
			.WidgetFramework_WidgetRenderer_Threads_FullThreadList .message .newIndicator span
			{
				background-color: rgb(77, 149, 221);
-webkit-border-top-right-radius: 3px; -moz-border-radius-topright: 3px; -khtml-border-top-right-radius: 3px; border-top-right-radius: 3px;
position: absolute;
top: -4px;
right: -1px;
width: 5px;
height: 4px;

			}

	.WidgetFramework_WidgetRenderer_Threads_FullThreadList .messageText .readMoreLink
	{
		display: block;
		text-align: right;
	}

.widget-poll .pollBlock .pollContent
{
	padding-left: 0;
	width: auto;
}

.widget-poll .pollBlock .pollOptions,
.widget-poll .pollBlock .pollResults
{
	border: 0;
}

.widget-poll .pollBlock .pollResult h3.optionText
{
	border: 0;
	overflow: hidden;
	text-overflow: ellipsis;
	white-space: nowrap;
}

.widget-poll .pollBlock .pollResult .count
{
	padding: 0;
}
.WidgetFramework_WidgetRenderer_ShareThisPage .sharePage .shareControl
{
    float: none;
}

.WidgetFramework_WidgetRenderer_FeedReader_Entries
{
}
	.WidgetFramework_WidgetRenderer_FeedReader_Entries .limitedHeight
	{
		max-height: 59px; 
	}

	.WidgetFramework_WidgetRenderer_FeedReader_Entries .WidgetFramework_WidgetRenderer_FeedReader_Entry
	{
		text-align: justify;
		display: block;
		overflow: hidden;
		margin-bottom: 3px;
	}

	.WidgetFramework_WidgetRenderer_FeedReader_Entries img.WidgetFramework_WidgetRenderer_FeedReader_Thumbnail
	{
		width: auto;
		float: right;
		margin-left: 3px;
	}
		.WidgetFramework_WidgetRenderer_FeedReader_Entries .WidgetFramework_WidgetRenderer_FeedReader_Entry:nth-child(2n) img.WidgetFramework_WidgetRenderer_FeedReader_Thumbnail
		{
			float: left;
			margin-right: 3px;
		}

.WidgetFramework_WidgetRenderer_XFRM_Resources .Hint
{
	float: right;
}



