<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterStartGame.ascx.cs" Inherits="WebEditor.RegisterStart" %>

<% if (Membership.GetUser() != null) {%>
<h1>Velkommen til Hundeparken!</h1>
<p>Du er nu oprettet i hundeparken. Hop ind og lav din hund med det samme!</p>
<div id="startgame">
	<a href="#" onclick="EnterGame();">
	<img id="pissingdog" src="/images/hundeanims/hund_anim_pisser.gif" />
	</a>
</div>

<%} else {%>

<p>Du er ikke logget ind.</p>


<% } %>