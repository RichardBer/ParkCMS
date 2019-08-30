using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using YAF.Classes.Data;
using YAF.Classes.Core;


namespace SharedWeb.Shared.SharedControls
{
	public partial class ForumNews : System.Web.UI.UserControl
	{
		protected string FormatMessage(object messageText, object messageID)
		{
			string formattedMessage = messageText.ToString();
			string strMessageId = messageID.ToString();

			//split message			
			if (formattedMessage.IndexOf("===") >= 0)
			{
				formattedMessage = Regex.Split(formattedMessage, "===")[0];
				formattedMessage += string.Format("<a href=\"forum.aspx?g=posts&t={0}\">{1}</a>", strMessageId, SharedWeb.Utils.GetText("HPSAGA_FRONTPAGENEWS", "ReadMore"));
			}
		
			MessageFlags messageFlags = new MessageFlags();
			messageFlags.IsBBCode = true;
			formattedMessage = YafFormatMessage.FormatMessage(formattedMessage, messageFlags);

			return formattedMessage;
		}
		
		protected string FormatDate(object messageDate)
		{
			DateTime date = (DateTime)messageDate;
			string culture = Utils.GetCurrentCulture();
			return date.ToString("d. MMMM", CultureInfo.CreateSpecificCulture(culture));
		}
	
		protected void Page_Load(object sender, EventArgs e)
		{
			DataTable dataTable = new DataTable();
			
			if (Cache[CacheKeys.FrontNews] != null)
			{
				dataTable = (DataTable)Cache[CacheKeys.FrontNews];
			}
			else
			{
				string connString = ConfigurationManager.ConnectionStrings["yafnet"].ConnectionString;
				SqlConnection sqlConn = new SqlConnection(connString);
				sqlConn.Open();

				const string sql = "SELECT TOP 3 yaf_topic.topicid, yaf_topic.topic, yaf_message.message, yaf_message.posted, " +
							 "  (" +
							 "	SELECT count(*) FROM yaf_message WHERE topicid=yaf_topic.topicid " +
							 "	)-1 AS replies " +
							 "FROM yaf_topic, yaf_message "  +
							 "WHERE " +
							 "  yaf_topic.priority=2 AND " +
							 "  yaf_topic.topicid=yaf_message.topicid AND " +
							 "  yaf_message.replyto IS NULL AND " +
							 "  yaf_Message.IsDeleted = 0" +
							 "ORDER BY posted DESC ";

				SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, sqlConn);
				dataAdapter.Fill(dataTable);
				sqlConn.Close();

				Cache.Insert(CacheKeys.FrontNews, dataTable, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
			}
		

			NewsRepeater.DataSource = dataTable;
			NewsRepeater.DataBind();
		}
	}
}