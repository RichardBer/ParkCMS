<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterResendEmail.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.RegisterResendEmail" %>
<h1><%= SharedWeb.Utils.GetText("REGISTRATION", "ResendEmailHeader")%></h1>
<asp:Literal ID="ResendMessage" runat="server" />