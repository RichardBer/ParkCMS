<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteTop.ascx.cs" Inherits="WebEditor.SiteTop" %>
<%@ Register Src="~/SharedControls/UnconfirmedUserNotification.ascx" TagName="UnconfirmedUserNotification" TagPrefix="Shared" %>
<%@ Register Src="~/SharedControls/User.ascx" TagName="User" TagPrefix="Shared" %>

<script type="text/javascript" src="http://www.dr.dk/drdktopbar/topbar.js"></script>


<div id="outerwrap">
<div id="header"><h1><a href="/">Hundeparken</a></h1></div>

<div id="innerwrap">
	<div id="menu">
		<ul>
			<li><a href="/">Forside</a></li>
			<li><a href="Forum.aspx">Forum</a></li>
			<li><a href="Help.aspx">Hjælp</a></li>
		</ul>

		<Shared:User runat="server" ID="UserCtrl" />
	</div>
	
	
	<Shared:UnconfirmedUserNotification id="Uun" runat="server" />
	
	<div style="clear:both;"></div>
	
	<div id="content">