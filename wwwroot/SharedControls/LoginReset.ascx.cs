using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SharedWeb.Shared.SharedControls
{
	public partial class LoginReset : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if ((Request["user"] != null) && (Request["key"] != null))
			{
				Control SendPasswordMail = LoadControl("LoginResetSendPasswordMail.ascx");
				LoginResetPlaceholder.Controls.Add(SendPasswordMail);
			}
			else
			{
				Control SendConfirmMail = LoadControl("LoginResetSendConfirmMail.ascx");
				LoginResetPlaceholder.Controls.Add(SendConfirmMail);
			}
		}
	}
}