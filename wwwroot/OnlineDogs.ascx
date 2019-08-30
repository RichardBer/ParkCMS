<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineDogs.ascx.cs" Inherits="WebEditor.OnlineDogs" %>

<script type="text/javascript" charset="utf-8">

function randomDogWord() {

	prefix = new Array('gøende', 'aktive', 'smukke', 'grineren','paringslystne','savlende','logrende','halsende','legesyge','loppebefængte','knurrende','frække','samspils-ramte','totalt nederen','chattende','kradsende','loppende','ruhårede','langhårede','kupperede','våde','trimmede','veldresserede','afrettede');
	postfix = new Array('hunde', 'køtere','tæver','firbenede venner','vuffere','vuffeli-vovvov\'ere','pelsklædte','gadekryds','bastarder','tæppe-tissere','loppesække','hunde','vovser','vuffere','slagter-hunde');
	
	var  	pre, post;
	pre = Math.floor(1 + Math.random() * (prefix.length));
	post = Math.floor(1 + Math.random() * (postfix.length));

	document.write(prefix[pre-1] +' ' + postfix[post-1]);
}

function randomDogImg() {
	var  	face;
	face = Math.floor(1 + Math.random() * 6);
	switch ( face) {
	case 1:
		document.write('<img src="/images/hundeanims/hund_anim_logre.gif">');
		break;
	case 2:
		document.write('<img src="/images/hundeanims/hund_anim_parring.gif">');
		break;
	case 3:		
		document.write('<img src="/images/hundeanims/hund_anim_pisser.gif">');
		break;
	case 4:
		document.write('<img src="/images/hundeanims/hund_anim_prutter.gif">');
		break;
	case 5:
		document.write('<img src="/images/hundeanims/hund_anim_skider.gif">');
		break;
	case 6:		
		document.write('<img src="/images/hundeanims/hund_anim_snus_selv.gif">');
		break;
	}
}
</script>

<div id="antalbrugere">
	<p>Der er lige nu <script src="Stats.aspx"></script> <script>randomDogWord();</script> i parken!</p>
	<script>randomDogImg();</script>
</div>
<div style="clear:both;"></div>