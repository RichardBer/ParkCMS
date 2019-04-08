using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebEditor.HPControls
{
	public partial class HatList : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			MembershipUser user = Membership.GetUser();
			
			string imgText = "";
			if (user != null)
			{
				hatlistImage.ImageUrl = "/images/hatlist.png";
				imgText = "Se hattelisten";
			}
			else
			{
				hatlistImage.ImageUrl = "/images/hatlist-gray.png";
				imgText = "Du skal logge ind for at se hattelisten.";
			}
			hatlistImage.AlternateText = imgText;
			hatlistImage.ToolTip = imgText;
		}
	}
}