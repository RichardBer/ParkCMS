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
	public partial class LoginResetChangePassword : System.Web.UI.UserControl
	{
		private bool Validated = false;
		private MembershipUser user = null;
	
		protected void Page_Load(object sender, EventArgs e)
		{
		
			try
			{
				object provUserKey = new Guid(Request["user"]);
				user = Membership.GetUser(provUserKey);
			}
			catch
			{
			}
			
			if (user == null)
			{
				Msg.Text = Utils.GetText("RESETPASSWORD", "Step2InvalidLink");
				return;
			}
			else
			{
				if (Request["key"] == Registration.GetValidationKey(user.Email, user.ProviderUserKey.ToString()))
				{
					ResetPassBtn.Visible = true;
					Validated = true;
					Msg.Text = string.Format(Utils.GetText("RESETPASSWORD", "Step2Confirm"), user.UserName);
					return;
				}
				else
				{
					Msg.Text = Utils.GetText("RESETPASSWORD", "Step2InvalidLink");
					return;
				}
			}

		

		}
		
		protected void ResetPassBtn_Click(object sender, EventArgs e)
		{
			if (!Validated) return;
			if (user == null) return;
			
			string newPass = Utils.RandomString(14);
			user.ChangePassword(user.ResetPassword(), newPass);
			SendPassEmail(newPass, user.UserName);
		}


		private void SendPassEmail(string newPass, string username)
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


			MailMessage Mail = new MailMessage();
			Mail.Subject = Utils.GetText("RESETPASSWORD", "Step2EmailSubject");
			Mail.From = new MailAddress(FromEmail, FromName);
			//if (FromEmail == "hundeparken@dr.dk") Mail.Bcc.Add(new MailAddress("sihl@dr.dk"));
			Mail.IsBodyHtml = false;

			System.Collections.Specialized.NameValueCollection CustHeaders = new System.Collections.Specialized.NameValueCollection();
			CustHeaders.Add("Auto-Submitted", "auto-generated");
			Mail.Headers.Add(CustHeaders);

			//Tekst del
			string BodyTxt = Utils.GetText("RESETPASSWORD", "Step2EmailText");
			BodyTxt += Utils.GetText("REGISTRATION", "EMAILSIGNATURETEXT");
			BodyTxt = string.Format(BodyTxt, user.UserName, newPass);
			AlternateView TextPart = AlternateView.CreateAlternateViewFromString(BodyTxt, Encoding.UTF8, "text/plain");
			TextPart.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
			Mail.AlternateViews.Add(TextPart);

			//HTML del
			string BodyHTML = Utils.GetText("RESETPASSWORD", "Step2EmailHTML");
			BodyHTML += Utils.GetText("REGISTRATION", "EMAILSIGNATUREHTML");
			BodyHTML = string.Format(BodyHTML, user.UserName, newPass);
			AlternateView HTMLPart = AlternateView.CreateAlternateViewFromString(BodyHTML, Encoding.UTF8, "text/html");
			HTMLPart.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
			Mail.AlternateViews.Add(HTMLPart);
			Mail.BodyEncoding = Encoding.UTF8;

			try
			{
				Mail.To.Add(user.Email);
				SmtpCl.Send(Mail);

				Msg.Text = "<p>" + Utils.GetText("RESETPASSWORD", "Step2EmailSent") + "</p>";
				return;
			}
			catch
			{
				Msg.Text = "<p>" + Utils.GetText("RESETPASSWORD", "Step2EmailSendError") + "</p>";
				return;
			}
		}
		
		
		
	}
}