using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using YAF.Classes;
using YAF.Classes.Core;
using YAF.Classes.Utils;


namespace SharedWeb.Shared.SharedControls
{
	public partial class newUserLanguage : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			MembershipUser user = Membership.GetUser();
			if (user == null)
			{
				//tjek for cookie
				if (Request.Cookies["UserLanguage"] != null)
				{
					if (Request.Cookies["UserLanguage"]["File"] != null)
					{
						SetLanguageFile(Request.Cookies["UserLanguage"]["File"]);
					}
				}
			}

			BtnDK.ImageUrl = (Utils.GetCurrentLanguage() == "danish.xml") ? "/images/flag_danish_selected.png" : "/images/flag_danish.png";
			BtnNO.ImageUrl = (Utils.GetCurrentLanguage() == "norwegian.xml") ? "/images/flag_norwegian_selected.png" : "/images/flag_norwegian.png";
			BtnSE.ImageUrl = (Utils.GetCurrentLanguage() == "swedish.xml") ? "/images/flag_swedish_selected.png" : "/images/flag_swedish.png";
		}
		
		private void SetLanguageFile(string filename)
		{
			string lang = Utils.GetCurrentLanguage();
			if (lang != filename) 
			{
				//gem i cookie
				HttpCookie languageCookie = new HttpCookie("UserLanguage");
				languageCookie["File"] = filename;
				languageCookie.Expires = DateTime.Now.AddMonths(3);
				Response.Cookies.Add(languageCookie);

				//gem setting i brugerdb
				MembershipUser user = Membership.GetUser();
				if (user != null)
				{
					string culture = Utils.GetCurrentCulture(filename);
					string sqlUpdateLanguage = string.Format("UPDATE yaf_User SET LanguageFile='{0}', Culture='{1}' WHERE Name='{2}'", filename, culture, user.UserName);
					string connString = ConfigurationManager.ConnectionStrings["yafnet"].ConnectionString;
					SqlConnection sqlConn = new SqlConnection(connString);
					try
					{
						SqlCommand updateLangCommand = new SqlCommand(sqlUpdateLanguage, sqlConn);
						updateLangCommand.Connection.Open();
						updateLangCommand.ExecuteNonQuery();
					}
					finally
					{
						sqlConn.Close();
					}

					UserMembershipHelper.ClearCacheForUserId(UserMembershipHelper.GetUserIDFromProviderUserKey(user.ProviderUserKey));
				}
			
				//set aktiv som current default. Brugt hvis der ikke er en bruger
				YafContext.Current.BoardSettings.Language = filename;

				Response.Redirect(Request.RawUrl);
			}
		}
		
		protected void BtnDK_Click(object sender, ImageClickEventArgs e)
		{
			SetLanguageFile("danish.xml");
		}

		protected void BtnNO_Click(object sender, ImageClickEventArgs e)
		{
			SetLanguageFile("norwegian.xml");
		}

		protected void BtnSE_Click(object sender, ImageClickEventArgs e)
		{
			SetLanguageFile("swedish.xml");
		}
		

	}
}