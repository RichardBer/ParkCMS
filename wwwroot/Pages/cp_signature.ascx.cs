/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj�rnar Henden
 * Copyright (C) 2006-2010 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

using YAF.Classes.Core;
using YAF.Classes;
using YAF.Classes.Utils;

namespace YAF.Pages // YAF.Pages
{
	/// <summary>
	/// Summary description for cp_signature.
	/// </summary>
	public partial class cp_signature : ForumPageRegistered
	{
		public cp_signature()
			: base("CP_SIGNATURE")
		{
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!PageContext.BoardSettings.AllowSignatures && !(PageContext.IsAdmin || PageContext.IsForumModerator))
			{
				// Not accessbile...
				YafBuildLink.AccessDenied();
			}

			if (!IsPostBack)
			{
				PageLinks.AddLink(PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
				PageLinks.AddLink(PageContext.PageUserName, YafBuildLink.GetLink(ForumPages.cp_profile));
				PageLinks.AddLink(GetText("TITLE"), "");
			}
		}
	}
}
