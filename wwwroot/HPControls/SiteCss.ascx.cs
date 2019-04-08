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

namespace WebEditor
{
	public partial class SiteCss : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string ThemeName = "";

			switch (DateTime.Now.Month )
			{
				case 12:
				case 1:
				case 2:
					ThemeName = "";
					break;
				case 3:
				case 4:
                    ThemeName = "";
					break;
				case 5:
				case 6:
				case 7:
				case 8:
					ThemeName = "";
					break;
				case 9:
				case 10:
				case 11:
					ThemeName = "";
					break;
			}
			
			hpThemeCss.Text = string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"css/{0}\" media=\"screen\" />", ThemeName);
		}
	}
}