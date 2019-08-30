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
	public partial class LoginForm : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void LoginTop_Authenticate(object sender, AuthenticateEventArgs e)
		{
			var userName = this.LoginTop.FindControlAs<TextBox>("UserName");
			var password = this.LoginTop.FindControlAs<TextBox>("Password");
			
			e.Authenticated = YafContext.Current.CurrentMembership.ValidateUser(userName.Text.Trim(), password.Text.Trim());

			// vzrus: to clear the cache to show user in the list at once
			YafContext.Current.Cache.Remove(YafCache.GetBoardCacheKey(Constants.Cache.UsersOnlineStatus));
		}

		protected void LoginTop_LoginError(object sender, EventArgs e)
		{
			Response.Redirect("LoginError.aspx");
		}		

	}
}