using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using YAF.Classes;
using YAF.Classes.Core;
using YAF.Classes.Utils;


namespace SharedWeb.Shared
{
	public partial class UserLogout : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

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
	}
}