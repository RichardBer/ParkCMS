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

namespace YAF.Pages.Admin
{
  using System;
  using System.Data;
  using YAF.Classes;
  using YAF.Classes.Core;
  using YAF.Classes.Data;
  using YAF.Classes.Utils;

  /// <summary>
  /// Summary description for bannedip_edit.
  /// </summary>
  public partial class extensions_edit : AdminPage
  {
    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
      string strAddEdit = (Request.QueryString.GetFirstOrDefault("i") == null) ? "Add" : "Edit";

      if (!IsPostBack)
      {
        this.PageLinks.AddLink(PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink("Administration", YafBuildLink.GetLink(ForumPages.admin_admin));
        this.PageLinks.AddLink(strAddEdit + " File Extensions", string.Empty);

        BindData();
      }

      this.extension.Attributes.Add("style", "width:250px");
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      if (this.Request.QueryString.GetFirstOrDefault("i").IsSet())
      {
        DataRow row = DB.extension_edit(Security.StringToLongOrRedirect(Request.QueryString.GetFirstOrDefault("i"))).Rows[0];
        this.extension.Text = (string) row["Extension"];
      }
    }

    /// <summary>
    /// The add_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Add_Click(object sender, EventArgs e)
    {
      string ext = this.extension.Text.Trim();

      if (!IsValidExtension(ext))
      {
        BindData();
      }
      else
      {
        DB.extension_save(Request.QueryString.GetFirstOrDefault("i"), PageContext.PageBoardID, ext);
        YafBuildLink.Redirect(ForumPages.admin_extensions);
      }
    }

    /// <summary>
    /// The is valid extension.
    /// </summary>
    /// <param name="newExtension">
    /// The new extension.
    /// </param>
    /// <returns>
    /// The is valid extension.
    /// </returns>
    protected bool IsValidExtension(string newExtension)
    {
      if (newExtension.IsNotSet())
      {
        PageContext.AddLoadMessage("You must enter something.");
        return false;
      }

      if (newExtension.IndexOf('.') != -1)
      {
        PageContext.AddLoadMessage("Remove the period in the extension.");
        return false;
      }

      // TODO: maybe check for duplicate?
      return true;
    }

    /// <summary>
    /// The cancel_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Cancel_Click(object sender, EventArgs e)
    {
      YafBuildLink.Redirect(ForumPages.admin_extensions);
    }

    #region Web Form Designer generated code

    /// <summary>
    /// The on init.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnInit(EventArgs e)
    {
      save.Click += new EventHandler(Add_Click);
      cancel.Click += new EventHandler(Cancel_Click);

      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      InitializeComponent();
      base.OnInit(e);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }

    #endregion
  }
}