using System;
using System.Configuration;
using System.Web.Security;
using GameLibrary.Forum;

using YAF.Classes.Core;

namespace SharedWeb.Shared
{
	public partial class UserEnterGame : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			MembershipUser currentUser = Membership.GetUser();

			if (currentUser != null)
			{
				if (Registration.AllowEnterGame(currentUser))
				{
					string gameServers = ConfigurationSettings.AppSettings["SharedWeb.EnterGame.GameServers"];
					gameServers = gameServers.Replace(" ", "");
					GameServersScript.Text = "<script type=\"text/javascript\">var gameServers = new Array();";
					int gameServerID = 0;
					foreach (string serverHost in gameServers.Split(','))
					{
						GameServersScript.Text += string.Format("gameServers[{0}] = \"{1}\";", gameServerID, serverHost);
						gameServerID++;
					}
					GameServersScript.Text += "</script>";


					string loginResponse = ForumHelper.CurrentLoginResponse(YafContext.Current.Localization.LanguageCode);
					GameLoginDataScript.Text = string.Format("<script type=\"text/javascript\">var LoginData='{0}'</script>", loginResponse);
				}
				else
				{
					GameLoginDataScript.Text = "<script type=\"text/javascript\">var LoginData='Unconfirmed'</script>";
					BlockedImg.Visible = true;
				}
				
			}

		}
	}
}