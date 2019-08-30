<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeEmail.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.ChangeEmail" %>
<%@ Import Namespace="SharedWeb" %>

<h1><%= Utils.GetText("CHANGEEMAIL", "Header") %></h1>

<p><asp:Literal ID="Msg" runat="server" /></p>

<asp:Panel runat="server" ID="changeEmailForm">
	<div class="simpleForm">
		<table>
			<tr>
				<td><%= Utils.GetText("CHANGEEMAIL", "NewEmail")%></td>
				<td><asp:TextBox runat="server" ID="newEmailBox" /></td>
			</tr>
			<tr>
				<td></td>
				<td><asp:LinkButton CssClass="linkButton" runat="server"><%= Utils.GetText("CHANGEEMAIL", "ChangeEmailButton")%></asp:LinkButton></td>
			</tr>
		</table>
	</div>
</asp:Panel>