using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SharedWeb.Shared.SharedControls
{
	public partial class Register : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			switch (Request["step"])
			{
				case "confirm":
					Control ConfirmUser = LoadControl("RegisterConfirmUser.ascx");
					RegisterPlaceHolder.Controls.Add(ConfirmUser);
					break;
				case "startgame":
					Control StartGame = LoadControl("RegisterStartGame.ascx");
					RegisterPlaceHolder.Controls.Add(StartGame);
					break;
				case "changeemail":
					Control ChangeEmail = LoadControl("RegisterChangeEmail.ascx");
					RegisterPlaceHolder.Controls.Add(ChangeEmail);
					break;
				case "resend":
					Control ResendEmail = LoadControl("RegisterResendEmail.ascx");
					RegisterPlaceHolder.Controls.Add(ResendEmail);
					break;
				default:
					Control CreateUser = LoadControl("RegisterCreateUser.ascx");
					RegisterPlaceHolder.Controls.Add(CreateUser);
					break;
			}

		}
	}
}