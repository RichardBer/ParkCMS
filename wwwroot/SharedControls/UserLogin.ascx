<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.ascx.cs" Inherits="SharedWeb.Shared.LoginForm" %>

<div id="login">
<asp:Login ID="LoginTop" runat="server" RememberMeSet="False" OnLoginError="LoginTop_LoginError"
	OnAuthenticate="LoginTop_Authenticate" VisibleWhenLoggedIn="True">
	<LayoutTemplate>
		<table>
			<tr>
				<td>
					<asp:Label ID="UserNameLabel" class="username" runat="server" AssociatedControlID="UserName">
						Brugernavn
					</asp:Label>
				</td>
				<td colspan="2">
					<asp:Label ID="PasswordLabel" class="password" runat="server" AssociatedControlID="Password">
						Password
					</asp:Label>
				</td>
			</tr>

			<tr>
				<td>
					<asp:TextBox ID="UserName" runat="server"></asp:TextBox>
				</td>
				
				<td>
					<asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
				</td>
				
				<td>
					<asp:ImageButton ImageUrl="/images/login_btn.gif" runat="server" id="LoginButton" CommandName="Login" ValidationGroup="LoginTop" />
				</td>
			</tr>
		</table>
	</LayoutTemplate>
</asp:Login>
</div>