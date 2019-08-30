using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;



using YAF.Classes.Core;
using GameLibrary.Forum;


namespace SharedWeb.Shared.SharedControls
{
	public partial class newUserStartGame : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			MembershipUser currentUser = Membership.GetUser();

			if (currentUser != null)
			{
				if (Registration.AllowEnterGame(currentUser))
				{
					string GameServers = ConfigurationSettings.AppSettings["SharedWeb.EnterGame.GameServers"];
					GameServers = GameServers.Replace(" ", "");
					GameServersScript.Text = "<script type=\"text/javascript\">var gameServers = new Array();";
					int GameServerID = 0;
					foreach (string ServerHost in GameServers.Split(','))
					{
						GameServersScript.Text += "gameServers[" + GameServerID.ToString() + "] = \"" + ServerHost + "\";";
						GameServerID++;
					}
					GameServersScript.Text += "</script>";


					string LoginResponse = GameLibrary.Forum.ForumHelper.CurrentLoginResponse(YafContext.Current.Localization.LanguageCode);
					GameLoginDataScript.Text = string.Format("<script type=\"text/javascript\">var LoginData='{0}'</script>", LoginResponse);
				}
				else
				{
					GameLoginDataScript.Text = "<script type=\"text/javascript\">var LoginData='Unconfirmed'</script>";
				}

			}

		}
	}
}