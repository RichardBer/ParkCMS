using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Configuration;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using SharedWeb;

namespace SharedWeb.Shared.SharedControls
{
	public partial class LoginResetSendMail : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}


		protected void ResetPassBtn_Click(object sender, EventArgs e)
		{
			string username = usernameTextBox.Text.Trim();
			string email = emailTextBox.Text.Trim();

			if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(email))
			{
				return;
			}

			//find brugernavn fra email			
			if (string.IsNullOrEmpty(username))
			{
				username = Membership.GetUserNameByEmail(email);
				if (username == null)
				{
					Msg.Text = string.Format(Utils.GetText("RESETPASSWORD", "Step1NoEmail"), email);
					return;
				}
			}

			//find bruger fra brugernavn
			MembershipUser user = Membership.GetUser(username);
			emailTextBox.Text = "";
			if (user == null)
			{
				Msg.Text = string.Format(Utils.GetText("RESETPASSWORD", "Step1NoUser"), username);
				return;
			}

			ResetForm.Visible = false;
			SendConfirmEmail(user);
		}


		private void SendConfirmEmail(MembershipUser user)
		{
			string FromEmail = ConfigurationSettings.AppSettings["SharedWeb.Registration.EmailFromAddress"];
			string FromName = ConfigurationSettings.AppSettings["SharedWeb.Registration.EmailFromName"];

			SmtpClient SmtpCl = new SmtpClient();

#if (DEBUG)
			SmtpCl.Host = "localhost";
			SmtpCl.Port = 25;
			SmtpCl.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
			SmtpCl.PickupDirectoryLocation = "C:\\Simons filer\\SMTP Pickup";
#else
				Configuration Config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
				MailSettingsSectionGroup Settings = (MailSettingsSectionGroup)Config.GetSectionGroup("system.net/mailSettings");
				SmtpCl.Host = Settings.Smtp.Network.Host;
				SmtpCl.Port = Settings.Smtp.Network.Port;
				SmtpCl.DeliveryMethod = SmtpDeliveryMethod.Network;
#endif

			string ConfirmLink = "LoginReset.aspx?user=" + user.ProviderUserKey.ToString() + "&key=" + Registration.GetValidationKey(user.Email, user.ProviderUserKey.ToString());

			MailMessage Mail = new MailMessage();
			Mail.Subject = Utils.GetText("RESETPASSWORD", "Step1EmailSubject");
			Mail.From = new MailAddress(FromEmail, FromName);
			//if (FromEmail == "hundeparken@dr.dk") Mail.Bcc.Add(new MailAddress("sihl@dr.dk"));
			Mail.IsBodyHtml = false;

			System.Collections.Specialized.NameValueCollection CustHeaders = new System.Collections.Specialized.NameValueCollection();
			CustHeaders.Add("Auto-Submitted", "auto-generated");
			Mail.Headers.Add(CustHeaders);

			//Tekst del
			string BodyTxt = Utils.GetText("RESETPASSWORD", "Step1EmailText");
			BodyTxt += Utils.GetText("REGISTRATION", "EMAILSIGNATURETEXT");
			BodyTxt = string.Format(BodyTxt, user.UserName, ConfirmLink);
			AlternateView TextPart = AlternateView.CreateAlternateViewFromString(BodyTxt, Encoding.UTF8, "text/plain");
			TextPart.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
			Mail.AlternateViews.Add(TextPart);

			//HTML del
			string BodyHTML = Utils.GetText("RESETPASSWORD", "Step1EmailHTML");
			BodyHTML += Utils.GetText("REGISTRATION", "EMAILSIGNATUREHTML");
			BodyHTML = string.Format(BodyHTML, user.UserName, ConfirmLink);
			AlternateView HTMLPart = AlternateView.CreateAlternateViewFromString(BodyHTML, Encoding.UTF8, "text/html");
			HTMLPart.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
			Mail.AlternateViews.Add(HTMLPart);
			Mail.BodyEncoding = Encoding.UTF8;

			try
			{
				Mail.To.Add(user.Email);
				SmtpCl.Send(Mail);
						   
				Msg.Text = "<p>" + Utils.GetText("RESETPASSWORD", "Step1EmailSent1") + "</p>" +
						   "<p>" + Utils.GetText("RESETPASSWORD", "Step1EmailSent2") + "</p>";
				return;
			}
			catch
			{
				Msg.Text = "<p>" + Utils.GetText("RESETPASSWORD", "Step1EmailSendError") + "</p>";
				return;
			}
		}


	}
}