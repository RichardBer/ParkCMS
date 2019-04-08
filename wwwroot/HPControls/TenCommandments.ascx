<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TenCommandments.ascx.cs" Inherits="WebEditor.TenCommandments" %>

<div id="tibud">
	<h2>De 10 Bud</h2>
	<div id="tibud_text"></div>

	<script type="text/javascript" src="js/snippetswitcher.js"></script>
	<script type="text/javascript">
	var tenCommSwitcher = new snippetSwitcher('tibud_text');
	tenCommSwitcher.add('<p>1. Vær mod andre, som de skal være mod dig.</p><img src="/images/tibud/1.gif">');
	tenCommSwitcher.add('<p>2. Du må ikke stjæle eller snyde i byttehandler.</p><img src="/images/tibud/2.gif">');
	tenCommSwitcher.add('<p>3. Du må ikke spamme.</p><img src="/images/tibud/3.gif">');
	tenCommSwitcher.add('<p>4. Et byt er et byt, så tænk dig om.</p><img src="/images/tibud/4.gif">');
	tenCommSwitcher.add('<p>5. Du må ikke udgive dig for at være moderator eller admin.</p><img src="/images/tibud/5.gif">');
	tenCommSwitcher.add('<p>6. Et nej er et nej. Stop hvis der bliver sagt stop.</p><img src="/images/tibud/6.gif">');
	tenCommSwitcher.add('<p>7. Du skal være flink over for de nye hunde og hjælpe dem hvis de spørger om hjælp.</p><img src="/images/tibud/7.gif">');
	tenCommSwitcher.add('<p>8. Tal pænt til andre hunde.</p><img src="/images/tibud/8.gif">');
	tenCommSwitcher.add('<p>9. Du må ikke mobbe.</p><img src="/images/tibud/9.gif">');
	tenCommSwitcher.add('<p>10. Du må ikke lave falske anmeldelser.</p><img src="/images/tibud/10.gif">');
	tenCommSwitcher.setLoop(true);
	tenCommSwitcher.showRand();
	</script>

	<div class="clear"></div>

	<input type="image" id="tibud_next" src="/images/btn_next.gif" alt="Næste bud" onmouseover="document.getElementById('tibud_next').src='/images/btn_next_hover.gif';" onmouseout="document.getElementById('tibud_next').src='/images/btn_next.gif';" onclick="tenCommSwitcher.showNext(); return false;">
</div>
	