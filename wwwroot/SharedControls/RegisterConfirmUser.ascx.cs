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
	public partial class RegisterConfirmUser : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string UserID = Request["user"];
			string ValidationKey = Request["key"];

			if (UserID != null && ValidationKey != null)
			{

				MembershipUser User = null;
				try
				{
					Guid UserKey = new Guid(UserID);
					User = Membership.GetUser(UserKey);
				}
				catch (Exception CreateUserException)
				{
					ConfirmUserMessage.Text = Utils.GetText("REGISTRATION", "ConfirmEmailFail");
					return;
				}
				
				
				if (User != null)
				{
					if (Registration.GetValidationKey(User.Email, UserID).ToLower() == ValidationKey.ToLower())
					{
						//Fjerner unconfirmed rollen
						string UnconfirmedRole = ConfigurationSettings.AppSettings["SharedWeb.Registration.UnconfirmedRoleName"];
						Roles.RemoveUserFromRole(User.UserName, UnconfirmedRole);
						//Giver standard rolle:
						int BoardID = Int32.Parse(ConfigurationSettings.AppSettings["YAF.BoardID"]);
						RoleMembershipHelper.SetupUserRoles(BoardID, User.UserName);
						ConfirmUserMessage.Text = Utils.GetText("REGISTRATION", "ConfirmEmailSuccess");
					}
					else
					{
						ConfirmUserMessage.Text = Utils.GetText("REGISTRATION", "ConfirmEmailFail");
					}
					
				}
				else
				{
					ConfirmUserMessage.Text = Utils.GetText("REGISTRATION", "ConfirmEmailFail");
				}
			}
			else
			{
				ConfirmUserMessage.Text = Utils.GetText("REGISTRATION", "ConfirmEmailFail");
			}
			
		}
	}
}