<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="newUserStartGame.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.newUserStartGame" %>
<%@ Register Src="~/UserStartGameButton.ascx" TagName="UserStartGameButton" TagPrefix="Site" %>

<div id="startGame">
	<asp:Literal runat="server" ID="GameLoginDataScript" />
	<asp:Literal runat="server" ID="GameServersScript" />
	<script type="text/javascript">
		//Creates a new form element and populates with login data
		var gameLoginForm;
		function BuildLoginForm()
		{
			gameLoginForm = document.createElement("FORM");
			document.body.appendChild(gameLoginForm);
			gameLoginForm.method = "POST";

			inputElement = document.createElement("input");
			inputElement.type = "hidden";
			inputElement.name = "yaf";
			inputElement.id = "yaf";
			inputElement.value = LoginData
			gameLoginForm.appendChild(inputElement);
		}
		
		
		//Logs ind to given game page
		function Enter(formAction) {
			BuildLoginForm();
			gameLoginForm.action = formAction;
			gameLoginForm.submit();
		}

		//Get random game server
		function GetRandomServer() {
			servernumber = Math.floor(Math.random() * gameServers.length);
			return gameServers[servernumber] + "/mpgame.aspx";
		}
		
		//Enters game (in new window)
		function EnterGame() {
			if (LoginData == "Unconfirmed") {
				alert("<%= SharedWeb.Utils.GetText("MENU", "StartGameBlocked")%>");
				return;
			}

			BuildLoginForm();
			gameLoginForm.action = GetRandomServer();
			var hpw = window.open('about:blank', 'hpsagagame', 'directories=0,height=768,location=0,menubar=0,resizable=1,scrollbars=0,status=0,toolbar=0,width=1024');
			gameLoginForm.target = 'hpsagagame';
			gameLoginForm.submit();
		}
	</script>

	<a href="javascript:EnterGame();">
		<Site:UserStartGameButton runat="server" id="StartGameBtn" />
	</a>
</div>
