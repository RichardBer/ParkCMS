<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnterGame.ascx.cs" Inherits="WebEditor.EnterGame" %>

<asp:Literal runat="server" ID="GameLoginData" />

<script type="text/javascript">
	function GetRandomHpServer() {
		hpservernumber = Math.floor(Math.random()*2+2);
		if (hpservernumber == 1) hpservernumber = "";
		return "http://hundepark" + hpservernumber + ".dr.dk/";
	}
	
	function EnterGame() {
		var submitForm = document.createElement("FORM");
		document.body.appendChild(submitForm);
		submitForm.method = "POST";
		submitForm.action = GetRandomHpServer();

		var inputElement = document.createElement("input");
		inputElement.type = "hidden";
		inputElement.name = "yaf";
		inputElement.id = "yaf";
		inputElement.value = LoginData
		submitForm.appendChild(inputElement);
		
		//alert(inputElement.value);
		var hpw = window.open('about:blank', 'hundeparken', 'directories=0,height=768,location=0,menubar=0,resizable=1,scrollbars=0,status=0,toolbar=0,width=1024');
		submitForm.target = 'hundeparken';
		submitForm.submit();
	}
</script>

<a href="#" onclick="EnterGame();">
	<asp:Image ID="DogImg1" runat="server" class="dog1" />
	<asp:Image ID="DogImg2" runat="server" class="dog2" />
	<!--
	<img id="dog1" src="AvatarImage.aspx?ViggoVimpel" alt="Test Hund 1" />
	<img id="dog2" src="AvatarImage.aspx?ViggoV&flip=x" alt="Test Hund 2" />
	-->
</a>