using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using YAF.Classes.Core;

namespace SharedWeb.Shared.SharedControls
{
	public partial class ChangeEmail : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// Step 2:Tjekker email validering, og gennemfører ændring
			if (Request["step"] == "confirm")
			{
				string userId = Request["user"];
				string key = Request["key"];

				CommitEmailChange(userId, key);
				changeEmailForm.Visible = false;
			}
			else
			{
				// 1. Validerer og gemmer email, og sender en bekræftelsesmail
				MembershipUser user = Membership.GetUser();
				if (user == null)
				{
					Msg.Text = Utils.GetText("CHANGEEMAIL", "NotLoggedIn");
					changeEmailForm.Visible = false;
					return;
				}
				
				Msg.Text = string.Format(Utils.GetText("CHANGEEMAIL", "Instruction"), user.UserName, user.Email);

				string newEmail = newEmailBox.Text;
				if (!string.IsNullOrEmpty(newEmail))
				{
					ValidateAndSendEmail(user, newEmail);
				}
			}
			
		}
		
		private void ValidateAndSendEmail(MembershipUser user, string newEmail)
		{
			DeleteFromChangeEmailTable(user.ProviderUserKey);
			
			//valider email ved at oprette og slette ny bruger
			string emailValidationMsg = ValidateEmailMessage(newEmail);
			if (emailValidationMsg != "valid")
			{
				Msg.Text = emailValidationMsg;
				return;
			}
			SaveNewEmail(user.ProviderUserKey, newEmail);
			
			
			//Send email
			if (SharedWeb.Registration.SendConfirmMail(user.UserName, newEmail, user.ProviderUserKey.ToString(), "ChangeEmail.aspx"))
			{
				Msg.Text = string.Format(Utils.GetText("CHANGEEMAIL", "EmailSent"), newEmail);
				changeEmailForm.Visible = false;
			}
			else
			{
				Msg.Text = string.Format(Utils.GetText("CHANGEEMAIL", "EmailSendError"), newEmail);
			}
		}
		
		private void CommitEmailChange(string userId, string key)
		{
			object provUserKey = (object)userId;
			MembershipUser user = Membership.GetUser(provUserKey);

			string newEmail = GetNewEmail(user.ProviderUserKey);
			
			if (key == SharedWeb.Registration.GetValidationKey(newEmail, userId))
			{
				user.Email = newEmail;
				Membership.UpdateUser(user);
				int BoardID = Int32.Parse(ConfigurationSettings.AppSettings["YAF.BoardID"]);
				RoleMembershipHelper.UpdateForumUser(user, BoardID);
				DeleteFromChangeEmailTable(user.ProviderUserKey);
				Msg.Text = string.Format(Utils.GetText("CHANGEEMAIL", "EmailChanged"), newEmail);
			}
			else
			{
				Msg.Text = Utils.GetText("CHANGEEMAIL", "InvalidLink");
			}

		}
		
		
		private void DBNonQuery(string sql)
		{
			//Response.Write(sql + "<br />");
			//return;
			string connString = ConfigurationManager.ConnectionStrings["yafnet"].ConnectionString;
			SqlConnection sqlConn = new SqlConnection(connString);
			sqlConn.Open();
			SqlCommand sqlCmd = new SqlCommand(sql, sqlConn);
			try
			{
				sqlCmd.ExecuteNonQuery();
			}
			finally
			{
				sqlConn.Close();
			}
		}
		
		private void DeleteFromChangeEmailTable(object providerUserKey)
		{
			string userId = providerUserKey.ToString();
			string sql = string.Format("DELETE FROM forum_change_email WHERE UserID='{0}'", userId);
			DBNonQuery(sql);
		}

		private void SaveNewEmail(object providerUserKey, string email)
		{
			string userId = providerUserKey.ToString();
			string sql = string.Format("INSERT INTO forum_change_email (UserID, NewEmail) VALUES ('{0}', '{1}')", userId, email);
			DBNonQuery(sql);
		}
		
		private string GetNewEmail(object providerUserKey)
		{
			string userId = providerUserKey.ToString();
			string sql = string.Format("SELECT NewEmail FROM forum_change_email WHERE UserID='{0}'", userId);
			string connString = ConfigurationManager.ConnectionStrings["yafnet"].ConnectionString;
			SqlConnection sqlConn = new SqlConnection(connString);
			sqlConn.Open();
			SqlCommand sqlCmd = new SqlCommand(sql, sqlConn);
			try
			{
				return (string)sqlCmd.ExecuteScalar();
			}
			finally
			{
				sqlConn.Close();
			}
			
		}		

		private string ValidateEmailMessage(string email)
		{
			string testUserName = System.Guid.NewGuid().ToString();
			string testPassword = System.Guid.NewGuid().ToString();
			
			try
			{
				MembershipUser testUser = Membership.CreateUser(testUserName, testPassword, email);	
				Membership.DeleteUser(testUserName, true);
			}
			catch (MembershipCreateUserException exc)
			{
				if (exc.StatusCode == MembershipCreateStatus.DuplicateEmail)
				{
					return SharedWeb.Registration.GetErrorMessage(exc.StatusCode, email);
				}
				if (exc.StatusCode == MembershipCreateStatus.InvalidEmail)
				{
					return SharedWeb.Registration.GetErrorMessage(exc.StatusCode, email);
				}
				throw;
			}
			return "valid";
		}

				
	}
}