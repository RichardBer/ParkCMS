<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginResetSendPasswordMail.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.LoginResetChangePassword" %>


<h1><%= SharedWeb.Utils.GetText("RESETPASSWORD", "Header") %></h1>

<p><asp:Literal runat="server" ID="Msg" /></p>
<asp:LinkButton runat="server" ID="ResetPassBtn" Visible="false" CssClass="linkButton" OnClick="ResetPassBtn_Click"><%= SharedWeb.Utils.GetText("RESETPASSWORD", "Step2Button")%></asp:LinkButton>
