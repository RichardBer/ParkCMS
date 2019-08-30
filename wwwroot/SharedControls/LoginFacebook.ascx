<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginFacebook.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.LoginFacebook" %>


<div id="facebookLogin">
	
	<div id="fb-root"></div>
	<script src="http://connect.facebook.net/en_US/all.js"></script>
	<script>
		FB.init({
			appId: '<%= SharedWeb.Facebook.AppID %>',
			status: true, // check login status
			cookie: true, // enable cookies to allow the server to access the session
			xfbml: true  // parse XFBML
		});
	</script>
	

	<asp:Panel ID="faces" runat="server">
		<fb:facepile app_id="100197020057410" width="200" max_rows="1"></fb:facepile>		
	</asp:Panel>
	
	<asp:HyperLink CssClass="fbButton" runat="server" ID="fbLink">
		<span>Login med Facebook</span>
	</asp:HyperLink>
	
	<p>Saga skriver ikke på din Facebook væg, og sender ikke beskeder til dine venner.</p>

</div>