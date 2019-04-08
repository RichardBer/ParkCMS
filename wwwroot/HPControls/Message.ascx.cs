using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebEditor
{
	public partial class UserMessage : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}
		
		private const string BeginHTML = "<div id=\"message\">";
		private const string EndHTML = "</div>";
		
		string _text;
		public string Text
			{
				set
				{
					_text = value;
					if (_text == "")
					{
						MessageHTML.Text = "";
					}
					else
					{
						MessageHTML.Text = BeginHTML + _text + EndHTML;
					}
				}
				get
				{
					return _text;
				}
			}

		

	}
}