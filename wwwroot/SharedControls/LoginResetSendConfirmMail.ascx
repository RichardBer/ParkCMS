<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginResetSendConfirmMail.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.LoginResetSendMail" %>
<%@ Import Namespace="SharedWeb" %>

<h1><%= Utils.GetText("RESETPASSWORD", "Header") %></h1>

<asp:Literal runat="server" ID="Msg" />

<asp:Panel ID="ResetForm" runat="server">
	<p><%= Utils.GetText("RESETPASSWORD", "Step1Description") %></p>
	<p><%= Utils.GetText("RESETPASSWORD", "Step1Instruction") %></p>
	<table>
		<tr>
			<td><%= Utils.GetText("RESETPASSWORD", "Step1Username") %></td>
			<td><asp:TextBox runat="server" ID="usernameTextBox" /></td>
		</tr>
		<tr>
			<td><%= Utils.GetText("RESETPASSWORD", "Step1Email") %></td>
			<td><asp:TextBox runat="server" ID="emailTextBox" /></td>
		</tr>
		<tr>
			<td></td>
			<td><asp:LinkButton runat="server" ID="ResetPassBtn" CssClass="linkButton" OnClick="ResetPassBtn_Click"><%= Utils.GetText("RESETPASSWORD", "Step1Button") %></asp:LinkButton></td>
		</tr>
	</table>
</asp:Panel>