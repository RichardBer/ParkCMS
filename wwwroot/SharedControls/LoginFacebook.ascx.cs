using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using SharedWeb;

namespace SharedWeb.Shared.SharedControls
{
	public partial class LoginFacebook : System.Web.UI.UserControl
	{
		private bool _showFaces = true;
		public bool ShowFaces
		{
			get
			{
				return _showFaces;
			}
			set
			{
				_showFaces = value;
			}
		}
	
		protected void Page_Load(object sender, EventArgs e)
		{
			faces.Visible = _showFaces;

			string fbRedirect = "http://" + Request.Url.Host + "/Login.aspx?step=fb";


			//determine returl url
			string returnFromLoginUrl = "";
			if (Request["fbreturn"] != null)
			{
				returnFromLoginUrl = Request["fbreturn"];
			}
			else if (Request["return"] != null)
			{
				returnFromLoginUrl = Request["return"];
			}
			else
			{
				returnFromLoginUrl = Utils.GetLoginReturnUrl(HttpContext.Current);
			}

			if (returnFromLoginUrl != "")
			{
				fbRedirect = fbRedirect + "&fbreturn=" + Server.UrlEncode(returnFromLoginUrl);
				//tilføj return url, så den huskes også via ture over facebook.
				//fbLink.NavigateUrl = "https://www.facebook.com/dialog/oauth?client_id=" + Facebook.AppID + "&redirect_uri=" + Server.UrlEncode( + "&response_type=code&scope=email";
			}


			fbLink.NavigateUrl = "https://www.facebook.com/dialog/oauth?client_id=" + Facebook.AppID + "&redirect_uri=" + Server.UrlEncode(fbRedirect) + "&response_type=code&scope=email";
			//Response.Write(fbLink.NavigateUrl);

			string accessToken = null;
			if ((Request["code"] != null) && (Request["step"] == "fb"))
			{
				accessToken = Facebook.GetAccessToken(Request["code"], fbRedirect);
			}

			if (accessToken != null)
			{
				//gem cookie
				HttpCookie aTokenCookie = new HttpCookie(Facebook.CookieID, accessToken);
				aTokenCookie.Expires = DateTime.Now.AddMonths(1);
				Response.SetCookie(aTokenCookie);

				long facebookUID = Facebook.GetFacebookUserId(accessToken);
                if (facebookUID == 0) Response.Write("Could not determine facebook UID from code: " + Request["code"] + "\n with token: " + accessToken);

                if(facebookUID != 0)
                {
                    object YafUserID = Facebook.GetYafUser(facebookUID);
                    MembershipUser user = null;
                    if (YafUserID != null)
                    {
                        //log eksisterende fb bruger ind
                        user = Membership.GetUser(YafUserID);
                        DoLogin(user, false);
                    }
                    else
                    {
                        user = Membership.GetUser();
                        if (user != null)
                        {
                            //Der er en bruger logget ind, opret facebook forbindelse til den.
                            Facebook.ConnectProfileToFB(user.ProviderUserKey.ToString(), facebookUID);
                            DoLogin(user, false);
                        }
                        else
                        {
                            Facebook.FbUserInfo fbUserInfo = Facebook.GetUserInfo(accessToken);
                            if (fbUserInfo == null)
                            {
                                Response.Write("Could not get necessary user information from Facebook.");
                                return;
                            }

                            MembershipUserCollection members = Membership.FindUsersByEmail(fbUserInfo.email);
                            MembershipUser[] membersArr = new MembershipUser[members.Count];
                            members.CopyTo(membersArr, 0);
                            if (members.Count > 0)
                            {
                                //forbind eksisterende bruger til facebook profil
                                user = membersArr[0];
                                Facebook.ConnectProfileToFB(user.ProviderUserKey.ToString(), facebookUID);

                                //log ind
                                DoLogin(user, false);
                            }
                            else
                            {
                                //opret ny bruger, og lav fb forbindelse
                                try
                                {
                                    user = Facebook.CreateNewFbUser(fbUserInfo);
                                }
                                catch (MembershipCreateUserException MemberShipException)
                                {
                                    Response.Write(Registration.GetErrorMessage(MemberShipException.StatusCode, fbUserInfo.email));
                                    return;
                                }

                                DoLogin(user, true);
                            }
                        }
                    }                    
                }
			}
		}

		private void DoLogin(MembershipUser user, bool newUser)
		{
			//hvis der er en logget ind, så skal vedkommende logges ud, inden fb brugeren
			//logges ind.

			FormsAuthentication.SetAuthCookie(user.UserName, false);

			if (newUser)
			{
				Response.Redirect("Register.aspx?step=startgame");
			}
			else if (Request["fbreturn"] != null)
			{
				Response.Redirect(Server.UrlDecode(Request["fbreturn"]));
			}
			else
			{
				Response.Redirect("Default.aspx");
			}
		}
		
		
	}
}