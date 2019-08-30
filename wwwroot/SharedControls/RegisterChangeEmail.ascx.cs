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

using GameLibrary.Forum;
using YAF.Classes.Core;

namespace SharedWeb.Shared.SharedControls
{
	public partial class RegisterChangeEmail : System.Web.UI.UserControl
	{
		MembershipUser User = Membership.GetUser();

		string _message;
		private string Message
		{
			set
			{
				_message = value;
				if (_message == "")
					ChangeEmailMsg.Text = "";
				else
					ChangeEmailMsg.Text = "<p>" + _message + "</p>";
			}
			get
			{
				return _message;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			EnterNewEmailLabel.Text = Utils.GetText("REGISTRATION", "ChangeEmailText");
		
			if (User != null)
			{
				string UnconfirmedRole = ConfigurationSettings.AppSettings["SharedWeb.Registration.UnconfirmedRoleName"];
				if (RoleMembershipHelper.IsUserInRole(User.UserName, UnconfirmedRole))
				{
					Message = Utils.GetText("REGISTRATION", "ChangeEmailTextInfo");
				}
				else
				{
					Message = Utils.GetText("REGISTRATION", "ChangeEmailOnlyUnconfirmed");
					EnterNewEmailLabel.Visible = false;
					NewEmail.Visible = false;
					ChangeEmailBtn.Visible = false;
				}
			}
			else
			{
				Message = Utils.GetText("REGISTRATION", "NotLoggedIn");
				EnterNewEmailLabel.Visible = false;
				NewEmail.Visible = false;
				ChangeEmailBtn.Visible = false;
			}
		}

		protected void ChangeEmailBtn_Click(object sender, EventArgs e)
		{
			if (NewEmail.Text.Trim() == "") return;
		
			if (User != null)
			{
				try
				{
					User.Email = NewEmail.Text;
					Membership.UpdateUser(User);
				}
				catch
				{
					Message = string.Format(Utils.GetText("REGISTRATION", "ChangeEmailInvalidEmail"), NewEmail.Text);
					return;
				}

				//ip tjek
				string ThisIP = Request.ServerVariables["REMOTE_ADDR"];
				int MailsFromThisIP = (Application["MailsFrom_" + ThisIP] == null) ? 1 : (int)Application["MailsFrom_" + ThisIP];
				if (MailsFromThisIP > 3)
				{
					Message = MailsFromThisIP.ToString() + "Fejl 2: Kunne ikke sende mail.";
					return;
				}

				if (Registration.SendConfirmMail(User.UserName, User.Email, User.ProviderUserKey.ToString()))
				{
					Message = string.Format(Utils.GetText("REGISTRATION", "ChangeEmailSuccess"), User.Email);
					Application["MailsFrom_" + ThisIP] = MailsFromThisIP + 1;
				}
				else
				{
					Message = "Der skete en fejl: Kunne ikke afsende en ny bekræftelsesmail.";
					return;
				}
			}
			else
			{
				Message = Utils.GetText("REGISTRATION", "NotLoggedIn");
			}

		}
	}
}