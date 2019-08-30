<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.LoginForm" %>
<%@ Import Namespace="SharedWeb" %>

<script type="text/javascript">
	function LoginButton(event, elementName) {
		if (event.keyCode == 13) {
			eval(document.getElementById(elementName).href);
			return false;
		}
	}
</script>


<h1><%= Utils.GetText("LOGIN", "LoginHeader")%></h1>

<div id="loginForm" class="simpleForm">
	<asp:Login ID="LoginCtrl" runat="server" RememberMeSet="True" OnLoginError="LoginForm_LoginError"
		OnAuthenticate="LoginForm_Authenticate" VisibleWhenLoggedIn="True">
		<LayoutTemplate>
			<table>
				<tr>
					<td>
						<asp:Label ID="UserNameLabel" class="username" runat="server" AssociatedControlID="UserName">
							<%= Utils.GetText("LOGIN", "LoginUsername")%>
						</asp:Label>
					</td>
					<td>
						<asp:TextBox ID="UserName" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="PasswordLabel" class="password" runat="server" AssociatedControlID="Password">
							<%= Utils.GetText("LOGIN", "LoginPassword")%>
						</asp:Label>
					</td>
					<td>
						<asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:CheckBox runat="server" ID="RememberMe" Checked="false" /><asp:Label ID="Label1" runat="server" AssociatedControlID="RememberMe">
							<%= Utils.GetText("LOGIN", "LoginRememberMe")%>
						</asp:Label>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:LinkButton runat="server" ID="LoginButton" CssClass="linkButton" CommandName="Login" ValidationGroup="LoginTop">
							<%= Utils.GetText("LOGIN", "LoginButton")%></asp:LinkButton><br />
						
						
					</td>
				</tr>
			
				<tr>
					<td></td>
				</tr>
			</table>
			<strong>miniparken@protonmail.com - ParkCMS 2019.</strong>
		</LayoutTemplate>
	</asp:Login>
</div>

<asp:PlaceHolder ID="FacebookPlaceholder" runat="server" />

<div style="float:right;width:700px;margin-right:130px;margin-top:20px">
		<table>
		<tr>
			<td style="text-align:left;color:#939296">
			</td>
		</tr>
		</table>
</div>

