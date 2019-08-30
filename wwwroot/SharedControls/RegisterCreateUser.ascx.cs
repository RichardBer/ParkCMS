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
using GameLibrary.Forum;

namespace SharedWeb.Shared.SharedControls
{
	public partial class RegisterCreate : System.Web.UI.UserControl
	{
		string _warningMessage;
		private string WarningMessage
		{
			set
			{
				_warningMessage = value;
				if (_warningMessage == "")
					CreateUserWarning.Text = "";
				else
					CreateUserWarning.Text = "<div id=\"CreateUserWarning\">" + _warningMessage + "</div>";
			}
			get
			{
				return _warningMessage;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			TextBox username = (TextBox)this.LoginRegister.FindControl("UserName");
			TextBox password = (TextBox)this.LoginRegister.FindControl("Password");
			TextBox passwordConfirm = (TextBox)this.LoginRegister.FindControl("ConfirmPassword");
			TextBox email = (TextBox)this.LoginRegister.FindControl("Email");
			LinkButton linkButton = (LinkButton)this.LoginRegister.FindControl("LoginButton");

			username.Attributes.Add("onKeyPress", "javascript:DefaultButton(event, '" + linkButton.ClientID + "')");
			password.Attributes.Add("onKeyPress", "javascript:DefaultButton(event, '" + linkButton.ClientID + "')");
			passwordConfirm.Attributes.Add("onKeyPress", "javascript:DefaultButton(event, '" + linkButton.ClientID + "')");
			email.Attributes.Add("onKeyPress", "javascript:DefaultButton(event, '" + linkButton.ClientID + "')");


			//facebook
			if (Facebook.EnableFacebookLogin)
			{
				Control FacebookControl = LoadControl("LoginFacebook.ascx");
				FacebookPlaceholder.Controls.Add(FacebookControl);
			}
						
		
		}
		
		
		protected void LoginRegister_Authenticate(object sender, AuthenticateEventArgs e)
		{
			TextBox UsernameTB = (TextBox)this.LoginRegister.FindControl("UserName");
			TextBox PasswordTB = (TextBox)this.LoginRegister.FindControl("Password");
			TextBox ConfirmPasswordTB = (TextBox)this.LoginRegister.FindControl("ConfirmPassword");
			TextBox EmailTB = (TextBox)this.LoginRegister.FindControl("Email");

			string Username = UsernameTB.Text.Trim();
			string Password = PasswordTB.Text.Trim();
			string ConfirmPassword = ConfirmPasswordTB.Text.Trim();
			string Email = EmailTB.Text.Trim();

			//tjek at begge passwords er ens (javascript?)
			
			if (Password != ConfirmPassword)
			{
				WarningMessage = Utils.GetText("REGISTRATION", "CreateProfilePasswordConfirmationFailed");
				return;
			}

			//opretter brugeren i asp.nets membership system
			MembershipUser NewUser = null;
			try 
			{
				NewUser = Membership.CreateUser(Username, Password, Email);
			}
			catch (MembershipCreateUserException MemberShipException)
			{
				WarningMessage = Registration.GetErrorMessage(MemberShipException.StatusCode, Email);
				return;
			}
			
			if (NewUser != null) {
				//registrerer brugeren i yafs brugerliste
				int BoardID = Int32.Parse(ConfigurationSettings.AppSettings["YAF.BoardID"]);
				RoleMembershipHelper.UpdateForumUser(NewUser, BoardID);
				
				//giver brugeren rollen "unconfirmed"
				string UnconfirmedRole = ConfigurationSettings.AppSettings["SharedWeb.Registration.UnconfirmedRoleName"];
				Roles.AddUserToRole(Username, UnconfirmedRole);

				//Request.ServerVariables;


				//ip tjek
				string ThisIP = Request.ServerVariables["REMOTE_ADDR"];
				int MailsFromThisIP = (Application["MailsFrom_" + ThisIP] == null) ? 1 : (int)Application["MailsFrom_" + ThisIP];
				if (MailsFromThisIP > 3)
				{
					WarningMessage = MailsFromThisIP.ToString() + "Fejl 2: Kunne ikke sende mail.";
					return;
				}

				
				//Request.ServerVariables["REMOTE_ADDR"]
				
				//Sender bekræftelsesmail til bruger
				if (!Registration.SendConfirmMail(NewUser.UserName, NewUser.Email, NewUser.ProviderUserKey.ToString()))
				{
					WarningMessage = "Fejl: Kunne ikke sende mai222l.";
					return;
				}
				Application["MailsFrom_" + ThisIP] = MailsFromThisIP + 1;


				//Logger brugeren ind
				e.Authenticated = YafContext.Current.CurrentMembership.ValidateUser(Username, Password);

				// vzrus: to clear the cache to show user in the list at once
				YafContext.Current.Cache.Remove(YafCache.GetBoardCacheKey(Constants.Cache.UsersOnlineStatus));
			}
			else
			{
				WarningMessage = Utils.GetText("REGISTRATION", "CreateProfileErrorUnspecified");
			}
			
		}

		protected void LoginRegister_LoggedIn(object sender, EventArgs e)
		{
			Response.Redirect("Register.aspx?step=startgame");
		}


		protected void LoginRegister_LoginError(object sender, EventArgs e)
		{
			if (WarningMessage == "") WarningMessage = Utils.GetText("REGISTRATION", "CreateProfileErrorUnspecified") + ": Fejl ved første login";
		}					
	}
}