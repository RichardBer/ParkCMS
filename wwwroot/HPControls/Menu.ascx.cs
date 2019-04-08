using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


using YAF.Classes;
using YAF.Classes.Core;
using YAF.Classes.Utils;

using SharedWeb;

namespace WebEditor
{
	public partial class Menu : System.Web.UI.UserControl
	{
		public bool ShowAdminMenu = false;
		public bool ShowModMenu = false;
		public bool LoggedIn = false;
		public string UserName = "";
		public List<MenuHelper.MenuLink> HelpMenuLinks = new List<MenuHelper.MenuLink>();
		public List<MenuHelper.MenuLink> ForumMenuLinks = new List<MenuHelper.MenuLink>();
	
		protected void Page_Load(object sender, EventArgs e)
		{
			HelpMenuLinks = GetHelpMenuLinks();
			ForumMenuLinks = GetForumMenuLinks();
			LogoutLink.Text = Utils.GetText("MENU", "LogOut");
		
			MembershipUser user = Membership.GetUser();
			if (user != null)
			{
				LoggedIn = true;
				UserName = user.UserName;
				ShowModMenu = RoleMembershipHelper.IsUserInRole(user.UserName, "Moderator");
				ShowAdminMenu = RoleMembershipHelper.IsUserInRole(user.UserName, "Administrators");
			}
		}

		protected void Logout_Click(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();

			// Clearing user cache with permissions data and active users cache...
			YafContext.Current.Cache.Remove(YafCache.GetBoardCacheKey(Constants.Cache.ActiveUserLazyData.FormatWith(YafContext.Current.PageUserID)));
			YafContext.Current.Cache.Remove(YafCache.GetBoardCacheKey(Constants.Cache.UsersOnlineStatus));

			Session.Abandon();

			Response.Redirect(Request.RawUrl);
		}
		
		
		private List<MenuHelper.MenuLink> GetHelpMenuLinks()
		{
			List<MenuHelper.MenuLink> links = new List<MenuHelper.MenuLink>();

			if (Cache[CacheKeys.HelpMenu] != null)
			{
				links = (List<MenuHelper.MenuLink>)Cache[CacheKeys.HelpMenu];
			}
			else
			{
				links = MenuHelper.GetSubMenuLinksFromForumPosts(2);
				Cache.Insert(CacheKeys.HelpMenu, links, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
			}
			
			return links;
		}


		private List<MenuHelper.MenuLink> GetForumMenuLinks()
		{
			List<MenuHelper.MenuLink> links = new List<MenuHelper.MenuLink>();

			if (Cache[CacheKeys.ForumMenu] != null)
			{
				links = (List<MenuHelper.MenuLink>)Cache[CacheKeys.ForumMenu];
			}
			else
			{
				links = MenuHelper.GetForumMenuLinks();
				Cache.Insert(CacheKeys.ForumMenu, links, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);

			}

			return links;
		}

		
	}
}