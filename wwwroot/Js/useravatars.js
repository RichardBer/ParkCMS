var userAvatarsXMLHTTP;
if (window.XMLHttpRequest)
  {// code for IE7+, Firefox, Chrome, Opera, Safari
    userAvatarsXMLHTTP=new XMLHttpRequest();
  }
else
  {// code for IE6, IE5
    userAvatarsXMLHTTP=new ActiveXObject("Microsoft.XMLHTTP");
  }

function userAvatarsXMLHTTPCallback() {
  if (userAvatarsXMLHTTP.readyState  == 4) {
    if (userAvatarsXMLHTTP.status == 200) {
      xml = userAvatarsXMLHTTP.responseXML;
      
      avatars = xml.getElementsByTagName("avatars"); 
      first = avatars.length == 0 ? avatars : avatars[0];
      avatarindex = first.childNodes.length-1;
      
      var userAvatars = new Array();
      for (i=0; i<=avatarindex; i++) {
        var current = first.childNodes[i];
        avatarName = current.text ? current.text : current.textContent;
        avatarTint = first.childNodes[i].attributes.getNamedItem("tint").value;
        avatarYear = first.childNodes[i].attributes.getNamedItem("years").value;
        
        userAvatars.push(
            avatar = {
              name:avatarName,
              tint:avatarTint,
              years:avatarYear
            }
          );
      }
      userAvatarsCallback(userAvatars);
    }
    else {
      //fejl med indlæsning af xml
      }
  }  
}

function getUserAvatars(basepath, cookie, sig) {
  var userAvatarsXMLUrl = basepath + "_inc/useravatarsxml.aframe?rnd=" + Math.random() + "&sig=" + sig + "&cookie=" + cookie;

  userAvatarsXMLHTTP.open("GET", userAvatarsXMLUrl,  true);
  userAvatarsXMLHTTP.onreadystatechange = userAvatarsXMLHTTPCallback;
  userAvatarsXMLHTTP.send(null);
}s