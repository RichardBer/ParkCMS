<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterConfirmUser.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.RegisterConfirmUser" %>

<h1><%= SharedWeb.Utils.GetText("REGISTRATION", "ConfirmEmailHeader") %></h1>

<p><asp:Literal ID="ConfirmUserMessage" runat="server" /></p>