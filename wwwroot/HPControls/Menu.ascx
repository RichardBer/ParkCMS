<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="WebEditor.Menu" %>
<%@ Import Namespace="SharedWeb" %>


<script language="javascript">
	var openMenu = "";
	var menuTimer;

	function HoverMenu(MenuId) {
		clearTimeout(menuTimer);
		if (openMenu != MenuId) {
			HideMenu();
			document.getElementById(MenuId).style.display = 'block';
			openMenu = MenuId;
		}
	}

	function ReleaseMenu() {
		clearTimeout(menuTimer);
		menuTimer = setTimeout('HideMenu();', 500);
	}

	function HideMenu() {
		if (openMenu != "") {
			document.getElementById(openMenu).style.display = 'none';
			openMenu = "";
		}
	}	
</script>


<ul id="menu">
	<li><a class="front" href="/"><%= Utils.GetText("MENU", "Frontpage")%></a></li>
	<li><a class="front2" href="register.aspx"><%= Utils.GetText("MENU", "Frontpage")%></a></li>
	<li onmouseover="HoverMenu('ForumSubmenu');" onmouseout="ReleaseMenu();">
		
		<ul id="ForumSubmenu" class="submenu" onmouseover="HoverMenu('ForumSubmenu');" onmouseout="ReleaseMenu();">

			<%
			if (ForumMenuLinks.Count > 0)
				{
				foreach (MenuHelper.MenuLink link in ForumMenuLinks) {
				%>
				<li><a href="<%= link.Url %>"><%= link.Name %></a></li>
				<%
				}
				%>
				<li class="menusplitter"><hr /></li>
				<%
			}
			%>

			<li><a href="forum.aspx?g=search"><%= Utils.GetText("MENU", "Search")%></a></li>
			<% if (LoggedIn) {%>
			<li><a href="forum.aspx?g=members"><%= Utils.GetText("MENU", "Members")%></a></li>
			<% } %>
		</ul>
	</li>

	<% if (LoggedIn) { %>
	<li onmouseover="HoverMenu('UserSubmenu');" onmouseout="ReleaseMenu();">
		<a class="user" href="forum.aspx?g=cp_profile"><%= UserName %></a>
		<ul id="UserSubmenu" class="submenu" onmouseover="HoverMenu('UserSubmenu');" onmouseout="ReleaseMenu()">
			<li><a href="forum.aspx?g=cp_profile"><%= Utils.GetText("MENU", "EditProfile")%></a></li>
			<li><a href="forum.aspx?g=cp_changepassword"><%= Utils.GetText("MENU", "ChangePassword")%></a></li>
			<li class="menusplitter"><hr /></li>
			<li><a href="forum.aspx?g=mytopics"><%= Utils.GetText("MENU", "MyTopics")%></a></li>
			<li><a href="forum.aspx?g=cp_pm&v=in"><%= Utils.GetText("MENU", "Inbox")%></a></li>
			<li class="menusplitter"><hr /></li>
			
			<% if (ShowModMenu) {%>
			<li><a class="admin" href="forum.aspx?g=moderate_index"><%= Utils.GetText("MENU", "Moderation")%></a></li>
			<% } %>
			
			<% if (ShowAdminMenu) { %>
			<li><a class="admin" href="forum.aspx?g=admin_admin"><%= Utils.GetText("MENU", "Administration")%></a></li>
			<li><a class="admin" href="forum.aspx?g=admin_users"><%= Utils.GetText("MENU", "UserAdministration")%></a></li>
			<li><a class="admin" href="ClearCacheUG.aspx">Ryd cache</a></li>
			<% } %>

			<li class="menusplitter"><hr /></li>
			
			<% if (ShowModMenu || ShowAdminMenu) {%>
			<li><a class="admin" href="javascript:Enter('Admin.aspx', 'hpbackend');"><%= Utils.GetText("MENU", "HpBackend")%></a></li>
			<li><a class="admin" href="javascript:Enter('Events.aspx?type=report', 'hpbackend');"><%= Utils.GetText("MENU", "HpReports")%></a></li>
			<% } %>

		</ul>
	</li>
	<li><asp:LinkButton CssClass="logout" ID="LogoutLink" runat="server" Text="Log ud" OnClick="Logout_Click" /></li>
	<% } %>
</ul>