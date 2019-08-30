<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="newUserLanguage.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.newUserLanguage" %>

<div id="language">
	<asp:ImageButton ID="BtnDK" AlternateText="Dansk" ImageUrl="/images/flag_danish.png"
		runat="server" onclick="BtnDK_Click" />
	<asp:ImageButton ID="BtnNO" AlternateText="Norsk" ImageUrl="/images/flag_norwegian.png" 
		runat="server" onclick="BtnNO_Click" />
	<asp:ImageButton ID="BtnSE" AlternateText="Svenska" ImageUrl="/images/flag_swedish.png" runat="server" 
		onclick="BtnSE_Click" />
</div>