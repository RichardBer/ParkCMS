using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SharedWeb.Shared.SharedControls
{
	public partial class User2 : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Facebook.EnableFacebookLogin)
			{
				if (Request.Cookies[Facebook.CookieID] != null)
				{
					if (Membership.GetUser() == null)
					{
						string accessToken = Request.Cookies[Facebook.CookieID].Value;

						//valider
						if (Facebook.GetUserInfo(accessToken) == null)
						{
							//Response.Write("Atoken invalid valid. Removing cookie.<br/>");
							accessToken = null;
							Facebook.DeleteFacebookCookie(HttpContext.Current);

						}
						else
						{
							//Response.Write("Atoken valid, Logging in.<br/>");

							//log ind
							long fbUID = Facebook.GetFacebookUserId(accessToken);
							object yafUID = Facebook.GetYafUser(fbUID);
							MembershipUser user = Membership.GetUser(yafUID);
							//Response.Write("fbuid: " +  fbUID.ToString() + ", yafuid: " + yafUID + ".<br/>");
							if (user != null)
							{
								FormsAuthentication.SetAuthCookie(user.UserName, false);
								Response.Redirect("default.aspx");
							}
							else
							{
								//Response.Write("User not found.<br/>");
							}
							
						}
					}
					else
					{
						/*
						if (Request.Cookies[Facebook.CookieID] != null)
						{
							Response.Write(Request.Cookies[Facebook.CookieID].Value);
						}
						*/
					}
				}
				else
				{
					//Response.Write("No cookie<br/>");
				}
			}
			
			
			Control UserLanguage = LoadControl("newUserLanguage.ascx");
			UserLanguagePlaceholder.Controls.Add(UserLanguage);
		
			if (Membership.GetUser() != null)
			{
				Control UserStartGame = LoadControl("newUserStartGame.ascx");
				UserLeftPlaceholder.Controls.Add(UserStartGame);
			}
			else
			{
				Control UserRegister = LoadControl("newUserRegister.ascx");
				UserLeftPlaceholder.Controls.Add(UserRegister);
				Control UserLogin = LoadControl("newUserLogin.ascx");
				UserLeftPlaceholder.Controls.Add(UserLogin);
			}
		}
	}
}