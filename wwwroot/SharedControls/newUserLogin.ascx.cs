using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SharedWeb.Shared.SharedControls
{
	public partial class newLogin : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string returnUrl = Utils.GetLoginReturnUrl(HttpContext.Current);
			if (returnUrl != "")
			{
				LoginLink.NavigateUrl = "~/Login.aspx?return=" + Server.UrlEncode(returnUrl);
			}
			
			LoginLink.Text = Utils.GetText("MENU", "LOGINBUTTONTEXT");
		}
	}
}