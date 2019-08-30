<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterChangeEmail.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.RegisterChangeEmail" %>
<h1><%= SharedWeb.Utils.GetText("REGISTRATION", "ChangeEmailHeader")%></h1>
<asp:Literal runat="server" ID="ChangeEmailMsg" />
<br />
<asp:Label ID="EnterNewEmailLabel" runat=server AssociatedControlID="NewEmail" />
<asp:TextBox ID="NewEmail" runat="server" />
<asp:Button ID="ChangeEmailBtn" runat="server" onclick="ChangeEmailBtn_Click" text="OK" />