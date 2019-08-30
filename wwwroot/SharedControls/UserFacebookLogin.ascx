<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserFacebookLogin.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.UserFacebookLogin" %>

<div id="fb-root"></div>
<script src="http://connect.facebook.net/en_US/all.js"></script>
<asp:Literal runat="server" ID="FBInitScript" />

<div id="facebookLogin">
	<div id="facebookLoginButton">
		<fb:login-button onclick="window.location.href=LoginURL;"></fb:login-button>
	</div>


	<script type="text/javascript">
		function DoFacebookLogin(uid, secret, sig) {
			var submitForm = document.createElement("FORM");
			document.body.appendChild(submitForm);
			submitForm.method = "POST";

			var inputElement = document.createElement("input");
			inputElement.type = "hidden";
			inputElement.name = "fb_login_data";
			inputElement.id = "fb_login_data";
			inputElement.value = uid + "." + secret + "." + sig;
			submitForm.appendChild(inputElement);

			submitForm.submit();
		}

		if (!UserLoggedIn) {
			FB.getLoginStatus(function(response) {
				//hvis login fejler her, så vil den bare poste om og om igen.
				if (response.session) {
					//alert(response.session.access_token);
					//alert(response.session.expires);
					//alert(response.session.secret);
					//alert(response.session.session_key);
					//alert(response.session.sig);
					//alert(response.session.uid);
				
					//DoFacebookLogin(response.session.uid, response.session.secret, response.session.sig);
				}
			});
		}
		else {
			document.getElementById('facebookLoginButton').style = "display: none;";
		}
	</script>
	(Testing...)
	<asp:Literal runat="server" ID="testout" />
</div>