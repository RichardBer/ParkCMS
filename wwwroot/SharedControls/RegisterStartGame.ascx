<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterStartGame.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.RegisterStart" %>
<%@ Register Src="~/RegisterWelcomeStart.ascx" TagName="RegisterWelcomeStart" TagPrefix="Site" %>

<div id="registerStartGame">
<Site:RegisterWelcomeStart ID="WelcomeStart" runat="server" />
<asp:Literal ID="ErrMessage" runat="server" Visible="false" />
</div>