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


namespace SharedWeb.Shared.SharedControls
{
	public partial class LoginForm : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//enter submitter:
			var userName = this.LoginCtrl.FindControlAs<TextBox>("UserName");
			var password = this.LoginCtrl.FindControlAs<TextBox>("Password");
			var linkButton = this.LoginCtrl.FindControlAs<LinkButton>("LoginButton");
			userName.Attributes.Add("onKeyPress", "javascript:DefaultButton(event, '" + linkButton.ClientID + "')");
			password.Attributes.Add("onKeyPress", "javascript:DefaultButton(event, '" + linkButton.ClientID + "')");
			
			
			//facebook
			if (Facebook.EnableFacebookLogin)
			{
				Control FacebookControl = LoadControl("LoginFacebook.ascx");
				FacebookPlaceholder.Controls.Add(FacebookControl);
			}
			
			
		}

		protected void LoginForm_Authenticate(object sender, AuthenticateEventArgs e)
		{
			var userName = this.LoginCtrl.FindControlAs<TextBox>("UserName");
			var password = this.LoginCtrl.FindControlAs<TextBox>("Password");
			var rememberMe = this.LoginCtrl.FindControlAs<CheckBox>("RememberMe");

			e.Authenticated = YafContext.Current.CurrentMembership.ValidateUser(userName.Text.Trim(), password.Text.Trim());
			if (e.Authenticated)
			{
				FormsAuthentication.SetAuthCookie(userName.Text.Trim(), rememberMe.Checked);

				// vzrus: to clear the cache to show user in the list at once
				YafContext.Current.Cache.Remove(YafCache.GetBoardCacheKey(Constants.Cache.UsersOnlineStatus));

				if (Request["return"] != null)
				{
					Response.Redirect(Server.UrlDecode(Request["return"]));
				}
			}

		}

		protected void LoginForm_LoginError(object sender, EventArgs e)
		{
			var userName = this.LoginCtrl.FindControlAs<TextBox>("UserName");
			Response.Redirect("LoginError.aspx?username=" + Server.UrlEncode(userName.Text.Trim()));
		}				
	}
}