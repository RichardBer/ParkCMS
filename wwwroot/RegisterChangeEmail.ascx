<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterChangeEmail.ascx.cs" Inherits="WebEditor.RegisterChangeEmail" %>
<h1>Skift email adresse</h1>
<asp:Literal runat="server" ID="ChangeEmailMsg" />
<br />
<asp:Label ID="EnterNewEmailLabel" text="Indtast ny email adresse:" runat=server AssociatedControlID="NewEmail" />
<asp:TextBox ID="NewEmail" runat="server" />
<asp:Button ID="ChangeEmailBtn" runat="server" onclick="ChangeEmailBtn_Click" text="Ok" />