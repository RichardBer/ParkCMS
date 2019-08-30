<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterCreateUser.ascx.cs" Inherits="WebEditor.RegisterCreate" %>

<h1>Opret profil</h1>

<asp:Literal ID="CreateUserWarning" runat="server" />

<div id="CreateUser">
<asp:Login ID="LoginRegister" runat="server" RememberMeSet="True" OnLoginError="LoginRegister_LoginError"
	OnAuthenticate="LoginRegister_Authenticate" OnLoggedIn="LoginRegister_LoggedIn" VisibleWhenLoggedIn="True">

	<LayoutTemplate>
		<tr>
			<td class="label">
				<asp:Label ID="UserNameLabel" class="username" runat="server" AssociatedControlID="UserName">
					Brugernavn
				</asp:Label>
			</td>
			<td>
				<asp:TextBox ID="UserName" runat="server"></asp:TextBox>
			</td>
		</tr>
		
		<tr>
			<td class="label">
				<asp:Label ID="PasswordLabel" class="password" runat="server" AssociatedControlID="Password">
					Password
				</asp:Label>
			</td>
			<td>
				<asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
			</td>				
		</tr>


		<tr>
			<td class="label">
				<asp:Label ID="ConfirmPasswordLabel" class="password" runat="server" AssociatedControlID="ConfirmPassword">
					Gentag Password
				</asp:Label>
			</td>
			<td>
				<asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
			</td>				
		</tr>



		<tr>
			<td class="label">
				<asp:Label ID="EmailLabel" class="password" runat="server" AssociatedControlID="EmailLabel">
					Email
				</asp:Label>
			</td>
			<td>
				<asp:TextBox ID="Email" runat="server"></asp:TextBox>
			</td>				
		</tr>

		<tr>
			<td colspan="2" class="bottom">
				<asp:ImageButton ImageUrl="/images/opret_btn.gif" runat="server" id="LoginButton" CommandName="Login" ValidationGroup="LoginRegister" />
			</td>
		</tr>
	</LayoutTemplate>

</asp:Login>
</div>
