<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.ascx.cs" Inherits="WebEditor.UserLogin" %>
<%@ Register Src="~/EnterGame.ascx" TagName="EnterGame" TagPrefix="Site" %>

<% if (Membership.GetUser() == null) { %>
	<div id="signup">
		<a href="forum.aspx?g=register"><img src="/images/opret_profil.gif" alt="Opret Profil"/></a>
	</div>
	<div id="login">
	<asp:Login ID="LoginTop" runat="server" RememberMeSet="True" OnLoginError="LoginTop_LoginError"
		OnAuthenticate="LoginTop_Authenticate" VisibleWhenLoggedIn="True">
		<LayoutTemplate>
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
		</LayoutTemplate>
	</asp:Login>
	</div>
<% } else { %>
	<div id="logout">
		<asp:ImageButton ImageUrl="/images/logaf.gif" runat="server" id="Logout" OnClick="Logout_Click" />
	</div>
	<div id="entergame">
		<Site:EnterGame runat="server" />
	</div>
<%}%>