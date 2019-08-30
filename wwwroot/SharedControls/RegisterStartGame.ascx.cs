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

namespace SharedWeb.Shared.SharedControls
{
	public partial class RegisterStart : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Membership.GetUser() == null)
			{
				WelcomeStart.Visible = false;
				ErrMessage.Text = "<p>" + Utils.GetText("REGISTRATION", "StartGameLoginFail") + "</p>";
				ErrMessage.Visible = true;
			}
		}
	}
}