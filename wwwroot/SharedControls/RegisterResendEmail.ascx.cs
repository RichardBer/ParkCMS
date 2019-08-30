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

namespace SharedWeb.Shared.SharedControls
{
	public partial class RegisterResendEmail : System.Web.UI.UserControl
	{
		MembershipUser User = Membership.GetUser();

		string _message;
		private string Message
		{
			set
			{
				_message = value;
				if (_message == "")
					ResendMessage.Text = "";
				else
					ResendMessage.Text = "<p>" + _message + "</p>";
			}
			get
			{
				return _message;
			}
		}
			
		protected void Page_Load(object sender, EventArgs e)
		{
			if (User != null)
			{

				//ip tjek
				string ThisIP = Request.ServerVariables["REMOTE_ADDR"];
				int MailsFromThisIP = (Application["MailsFrom_" + ThisIP] == null) ? 1 : (int)Application["MailsFrom_" + ThisIP];
				if (MailsFromThisIP > 3)
				{
					Message = "Fejl 2: Kunne ikke sende mail.";
					return;
				}
				Application["MailsFrom_" + ThisIP] = MailsFromThisIP + 1;
				

				if (Registration.SendConfirmMail(User.UserName, User.Email, User.ProviderUserKey.ToString()))
				{
					Message = string.Format(Utils.GetText("REGISTRATION", "ResendEmailSuccess"), User.Email);
					Application["MailsFrom_" + ThisIP] = MailsFromThisIP + 1;
				}
				else
				{
					Message = "Fejl: Kunne ikke sende mail til " + User.Email + ".";
				}
			}
			else
			{
				Message = Utils.GetText("REGISTRATION", "NotLoggedIn");
			}
		}
	}
}