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


using YAF.Classes.Core;
using GameLibrary.Forum;

namespace SharedWeb.Shared.SharedControls
{
	public partial class UnconfirmedUserNotification : System.Web.UI.UserControl
	{
		private string TimeDescription(int Days)
		{
			switch (Days)
			{
				case 0: return Utils.GetText("CONFIRMEMAILNOTIFICATION", "ConfirmToday");
				case 1: return Utils.GetText("CONFIRMEMAILNOTIFICATION", "ConfirmTomorrow");
				default: return string.Format(Utils.GetText("CONFIRMEMAILNOTIFICATION", "ConfirmDays"), Days.ToString());
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.ServerVariables["SCRIPT_NAME"] == "/Register.aspx") return;

			MembershipUser User = Membership.GetUser();
			if (User != null)
			{
				if (!Registration.EmailConfirmed(User))
				{
					if (Registration.AllowEnterGame(User))
					{
						string NotificationMsg = "<p><b>" + TimeDescription(Registration.DaysToConfirmation(User)) + "</b></p>" +
												 "<p>" + string.Format(Utils.GetText("CONFIRMEMAILNOTIFICATION", "HelpText"), User.Email) + "</p>" +
												 "<p><a href=\"Register.aspx?step=changeemail\">" + Utils.GetText("CONFIRMEMAILNOTIFICATION", "ChangeEmailLink") + "</a> | <a href=\"Register.aspx?step=resend\">" + Utils.GetText("CONFIRMEMAILNOTIFICATION", "ResendEmailLink") + "</a><p/>";

					}
					else
					{
						string NotificationMsg = "<p><b>" + Utils.GetText("CONFIRMEMAILNOTIFICATION", "ConfirmNow") + "</b></p>" +
												 "<p>" + string.Format(Utils.GetText("CONFIRMEMAILNOTIFICATION", "HelpText"), User.Email) + "</p>" +
												 "<p><a href=\"Register.aspx?step=changeemail\">" + Utils.GetText("CONFIRMEMAILNOTIFICATION", "ChangeEmailLink") + "</a> | <a href=\"Register.aspx?step=resend\">" + Utils.GetText("CONFIRMEMAILNOTIFICATION", "ResendEmailLink") + "</a><p/>";

						Notification.Text = "<div id=\"unconfirmedUserNotification\" class=\"blocking\">" + NotificationMsg + "</div>";
					}
				}

			}
		}
	}
}