<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumNews.ascx.cs" Inherits="SharedWeb.Shared.SharedControls.ForumNews" %>

<div id="news">
<h1><%= SharedWeb.Utils.GetText("HPSAGA_FRONTPAGENEWS", "NewsHeader")%></h1>
	<asp:Repeater ID="NewsRepeater" runat="server">
		<ItemTemplate>
			<h2><a href="forum.aspx?g=posts&t=<%# Eval("TopicID") %>"><%# Eval("Topic") %></a></h2>
			<p class="date"><%# FormatDate(Eval("Posted")) %></p>
			<p><%# FormatMessage(Eval("Message"), Eval("TopicID")) %></p>
			<p class="comments"><a href="forum.aspx?g=posts&t=<%# Eval("TopicID") %>"><%# Eval("replies") %> <%= SharedWeb.Utils.GetText("HPSAGA_FRONTPAGENEWS", "Comments")%></a></p>
		</ItemTemplate>
		<SeparatorTemplate>
			<hr />
		</SeparatorTemplate>
	</asp:Repeater>
</div>
