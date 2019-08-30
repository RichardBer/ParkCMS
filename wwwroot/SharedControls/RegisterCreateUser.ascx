<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterCreateUser.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.RegisterCreate" %>


<script type="text/javascript">
	function DefaultButton(event, elementName) {
		if (event.keyCode == 13) {
			eval(document.getElementById(elementName).href);
			return false;
		}
	}
</script>


<h1><%= SharedWeb.Utils.GetText("REGISTRATION", "CreateProfileHeader")%></h1>

<div id="loginForm">

<asp:Literal ID="CreateUserWarning" runat="server" />

<asp:Login ID="LoginRegister" runat="server" RememberMeSet="True" OnLoginError="LoginRegister_LoginError"
	OnAuthenticate="LoginRegister_Authenticate" OnLoggedIn="LoginRegister_LoggedIn" VisibleWhenLoggedIn="True">

	<LayoutTemplate>
		<table>
		<tr>
			<td class="label">
				<asp:Label ID="UserNameLabel" class="username" runat="server" AssociatedControlID="UserName">
					<%= SharedWeb.Utils.GetText("REGISTRATION", "CreateProfileUsername")%>
				</asp:Label>
			</td>
			<td>
				<asp:TextBox ID="UserName" runat="server"></asp:TextBox>
			</td>
		</tr>
		
		<tr>
			<td class="label">
				<asp:Label ID="PasswordLabel" class="password" runat="server" AssociatedControlID="Password">
					<%= SharedWeb.Utils.GetText("REGISTRATION", "CreateProfilePassword")%>
				</asp:Label>
			</td>
			<td>
				<asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
			</td>				
		</tr>


		<tr>
			<td class="label">
				<asp:Label ID="ConfirmPasswordLabel" class="password" runat="server" AssociatedControlID="ConfirmPassword">
					<%= SharedWeb.Utils.GetText("REGISTRATION", "CreateProfileConfirmPassword")%>
				</asp:Label>
			</td>
			<td>
				<asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
			</td>				
		</tr>



		<tr>
			<td class="label">
				<asp:Label ID="EmailLabel" class="password" runat="server" AssociatedControlID="EmailLabel">
					<%= SharedWeb.Utils.GetText("REGISTRATION", "CreateProfileEmail")%>
				</asp:Label>
			</td>
			<td>
				<asp:TextBox ID="Email" runat="server"></asp:TextBox>
			</td>				
		</tr>
		<tr>
			<td></td>
			<td>
				<asp:LinkButton runat="server" ID="LoginButton" CommandName="Login" ValidationGroup="LoginRegister">
					<%= SharedWeb.Utils.GetText("REGISTRATION", "CreateProfileButton")%>
				</asp:LinkButton>
				<br <br /><br /><br />
			</td>
		</tr>
		</table>
	</LayoutTemplate>

</asp:Login>
</div>

<asp:PlaceHolder ID="FacebookPlaceholder" runat="server" />

<div style="float:right;width:700px;margin-right:130px;margin-top:20px">
		<table>
		<tr>
			<td style="text-align:left;color:#FFFFFF">
			<p><%= SharedWeb.Utils.GetText("REGISTRATION", "UserTerms1")%></p>
			<p><%= SharedWeb.Utils.GetText("REGISTRATION", "UserTerms2")%></p>
			<p><%= SharedWeb.Utils.GetText("REGISTRATION", "UserTerms3")%></p>
			</td>
		</tr>
		</table>
</div>
