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

namespace SharedWeb.Shared
{
	public partial class Login : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Membership.GetUser() != null)
			{
				Control UserLogout = LoadControl("UserLogout.ascx");
				UserLeftPlaceholder.Controls.Add(UserLogout);
				Control UserEnterGame = LoadControl("UserEnterGame.ascx");
				UserLeftPlaceholder.Controls.Add(UserEnterGame);
			}
			else
			{
				Control UserRegister = LoadControl("UserRegister.ascx");
				UserLeftPlaceholder.Controls.Add(UserRegister);
				Control UserLogin = LoadControl("UserLogin.ascx");
				UserLeftPlaceholder.Controls.Add(UserLogin);
			}
			
			if (Facebook.EnableFacebookLogin)
			{
				Control UserFacebookLogin = LoadControl("UserFacebookLogin.ascx");
				UserFacebookPlaceHolder.Controls.Add(UserFacebookLogin);
			}
		}
	}
}