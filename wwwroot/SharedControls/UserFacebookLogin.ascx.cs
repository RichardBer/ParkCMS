using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace SharedWeb.Shared.SharedControls
{
	public partial class UserFacebookLogin : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//http://developers.facebook.com/docs/authentication/permissions/
			string permissions = "email";
			string returnUrl = "http://saga2.dr.dk/LoginFacebook.aspx";
			string redUrl = "https://www.facebook.com/dialog/oauth?client_id=" + Facebook.AppID + "&redirect_uri=" + Server.UrlEncode(returnUrl) + "&scope=" + permissions;

			bool UserLoggedIn = (Membership.GetUser() != null);
			FBInitScript.Text = "<script>" +
								"  FB.init({" +
								"	  appId  : '" + Facebook.AppID + "'," +
								"     status : true," +
								"     cookie : true," +
								"     xfbml  : true" +
								"  });" +
								"  var LoginURL='" + redUrl + "';" + 
								"  var UserLoggedIn=" + UserLoggedIn.ToString().ToLower() + ";" +
								"</script>";


			



			// Login fra DoFacebookLogin, bruges ikke pt.
			/*			
			//string FbLoginData = Request["fb_login_data"]
			string FbLoginData = "599118532.XqOgLvsSBWkXRQZ3RYtv2A__.39275383a5b53c046538527faaa6609c";
			if (FbLoginData != null)
			{
				//Valider FB data via signatur m.m.
				string FbUID = FbLoginData.Split('.')[0];
				string FbSecret = FbLoginData.Split('.')[1];
				string FbSig = FbLoginData.Split('.')[2];

				//valider
				HMACSHA256 hmac = new HMACSHA256();
				ASCIIEncoding Enc = new ASCIIEncoding();
				hmac.Key = Enc.GetBytes(Facebook.AppSecret);
				string ExpectedSig = Enc.GetString(hmac.ComputeHash(Enc.GetBytes(FbSecret)));

				bool same = (ExpectedSig == FbSig);
			
				testout.Text = same.ToString() + " " + ExpectedSig + "|" + FbSig;
				
				//Logger ind:
				//FormsAuthentication.RedirectFromLoginPage("Simoneseren", false);
			}
			*/
			
		}
	}
}