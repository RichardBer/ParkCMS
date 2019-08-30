<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="WebEditor.Default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<title>ParkCMS</title>
 <head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
 <script type="text/javascript" src="swfobject.js"></script>
 <script type="text/javascript"> 
</script>
<style type="text/css">
	/* hide from ie on mac \*/
	html {
		height: 100%;
		overflow: hidden;
	}
	#flashcontent {
		height: 100%;
		width: 100%;
		position:relative;
		/*float:right;*/
		background-color:#040;
	}
	/* end hide */

	body {
		height: 100%;
		margin: 0;
		padding: 0;
		color: White;
		background-color: #000;
	}
	em 
	{
		color: red;
		font-size: x-large;
	}

 </style>
 </head>
 <body>
	<div id="flashcontent">
     <form action="" method="post">
      <asp:Literal ID="swf" runat="server" />
      <asp:Literal ID="version" runat="server" />
      <asp:Literal ID="loader" runat="server" />
      <asp:Literal ID="host" runat="server" />
      <asp:Literal ID="name" runat="server" />
      <asp:Literal ID="data" runat="server" />
      <asp:Literal ID="submit" runat="server" />
      <asp:Literal ID="debug" runat="server" />
     <asp:Literal ID="Literal1" runat="server" />
     </form>
     <asp:Literal ID="warning" runat="server" />
	</div>
	
	<asp:PlaceHolder ID="script" runat="server" visible="false">
	<script type="text/javascript">
		// <![CDATA[
	        var so = new SWFObject(document.getElementById("swf").value, "loader", "100%", "100%", "9", "#004400");
        	so.addParam("scale", "noscale");
	        so.addVariable("lan", "danish");
	        so.addVariable("username", document.getElementById("name").value);
	        so.addVariable("resp", document.getElementById("data").value);
	        so.addVariable("policyport", "843");
	        so.addVariable("host", document.getElementById("host").value);
	        so.addVariable("infoport", "2900");
	        so.addVariable("debug", document.getElementById("debug").value);
	        so.addVariable("loadSrc", document.getElementById("loader").value);
	        so.addVariable("loadRGB", "000000");
	        so.addVariable("minSecs", "4");
	        so.addVariable("version", document.getElementById("version").value);
	        
	        so.write("flashcontent");
		// ]]>
	</script>
	</asp:PlaceHolder>
 </body>
</html>