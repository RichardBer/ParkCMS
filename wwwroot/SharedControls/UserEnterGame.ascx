<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserEnterGame.ascx.cs" Inherits="SharedWeb.Shared.UserEnterGame" %>
<%@ Register Src="~/UserEnterGameButton.ascx" TagName="UserEnterGameButton" TagPrefix="Site" %>

<div id="entergame">

	<asp:Literal runat="server" ID="GameLoginDataScript" />
	<asp:Literal runat="server" ID="GameServersScript" />
	<script type="text/javascript">
		function GetRandomServer() {
			servernumber = Math.floor(Math.random() * gameServers.length);
			return gameServers[servernumber] + "/Game.aspx";
		}

		//Creates a new form element, populates with login data and submits to game.
		function EnterGame() {
			if (LoginData == "Unconfirmed") {
				alert("Du skal bekræfte din email-adresse, før du kan starte Hundeparken");
				return;
			}
		
			var submitForm = document.createElement("FORM");
			document.body.appendChild(submitForm);
			submitForm.method = "POST";
			submitForm.action = GetRandomServer();

			var inputElement = document.createElement("input");
			inputElement.type = "hidden";
			inputElement.name = "yaf";
			inputElement.id = "yaf";
			inputElement.value = LoginData
			submitForm.appendChild(inputElement);
			
			var hpw = window.open('about:blank', 'hundeparken', 'directories=0,height=768,location=0,menubar=0,resizable=1,scrollbars=0,status=0,toolbar=0,width=1024');
			submitForm.target = 'hundeparken';
			submitForm.submit();
		}
	</script>

	<a href="#" onclick="EnterGame();">
		<Site:UserEnterGameButton runat="server" id="EnterGamBtn" />
		<asp:Image ImageUrl="/images/start_blocked.gif" runat="server" ID="BlockedImg" visible="false" />
	</a>
</div>
