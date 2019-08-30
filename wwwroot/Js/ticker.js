var reloadTimeOut = 20000;

var xmlhttp;
if (window.XMLHttpRequest)
  {// code for IE7+, Firefox, Chrome, Opera, Safari
  xmlhttp=new XMLHttpRequest();
  }
else
  {// code for IE6, IE5
    xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  }

function xmlhttpcallback() {
  if (xmlhttp.readyState  == 4) {
    if (xmlhttp.status == 200) {
      parseXML(xmlhttp.responseXML);
    }
    else {
      //fejl med indlæsning af xml
      setReloadTimer(1000);
      }
  }  
}

function reloadChatTicker() {
  xmlhttp.open("GET", "ChatTicker.aspx?" + Math.random(),  true);
  xmlhttp.onreadystatechange = xmlhttpcallback;
  xmlhttp.send(null);
}

function setReloadTimer(timeOut) {
  chatTickerTimer = setTimeout("reloadChatTicker()", timeOut);
}

var newChatTickerHTML;
var worldHTML;
var mapPosX;
var mapPosY;
var avatarOverlayURL;
var firstRun = true;

var worldText = "Velkommen til Hundeparken Underground";
var worldNameTimer = null;

// Adjust such that worldText becomes prefix + name, by first removing characters until worldText is as small (or smaller than) prefix
// then adding characters until we are done. We use timer to make each character change visible and adjust #world to match worldText.
function adjustWorldName(prefix, name, deleting) {
  var complete = prefix + name;
  if(worldText == complete) return;

  // determine whether to grow or shrink text
  if(deleting) {
    if(worldText == prefix || worldText == "") deleting = false;
    else worldText = worldText.substr(0, worldText.length - 1);
  } else {
    worldText += complete.substr(worldText.length, 1);
  }

  // show change
  var worldHTML = worldText + "<img id=\"underscore\" src=\"/images/ticker/ticker_underscore.gif\" alt=\"_\">";
  document.getElementById('world').innerHTML = worldHTML;  
  
  // schedule later change 
  if(worldNameTimer != null) clearTimeout(worldNameTimer);
  worldNameTimer = setTimeout(function() { adjustWorldName(prefix, name, deleting); }, 100);
}

function parseXML(xml) {
  if (xml === null) {
    setReloadTimer(1000);
    return;
  }

  ticker = xml.getElementsByTagName("ticker"); 
  var first = ticker.length == 0 ? ticker : ticker[0];
  chatindex = first.childNodes.length-1;

  if (first === null || chatindex < 0) {
    setReloadTimer(1000);
    return;
  }
  
  //kort koordinater
  mapPosX = parseFloat(first.childNodes[chatindex].attributes.getNamedItem("x").value);
  mapPosY = parseFloat(first.childNodes[chatindex].attributes.getNamedItem("y").value);
  
  //hvis vi er i et rum, som ikke er på oversigtskortet, så sætter vi nogle andre, pasende koordinater
  var worldName = first.attributes.getNamedItem("world").value;
  if (worldName === "Rumskibet - Danmark" || worldName=== "Rumskibet - Fælles" || worldName === "Rumskibet - Norge") {
    mapPosX = 1164;
    mapPosY = 1624;
  }
  
  //justering så positionen er centreret
  mapPosX = mapPosX - 179;
  mapPosY = mapPosY - 120;

  newChatTickerHTML = "";
  for (i=chatindex; i>=0; i--) {
    var current = first.childNodes[i];
    chattxt = current.text ? current.text : current.textContent;
    avatar = current.attributes.getNamedItem("avatar").value;
    
    if (i % 2 == 0) { 
      newChatTickerHTML += "<div class=\"chatbubble_left\">";
      newChatTickerHTML += "<div class=\"chatbubble\">" +chattxt+ "</div>";
      newChatTickerHTML += "</div>";
      newChatTickerHTML += "<img class=\"doggy_left\" id=\"doggy_img_" + i + "\" alt=\"" +avatar+ "\" title=\"" +avatar+ "\" src=\"AvatarImage.aspx?" +encodeURIComponent(avatar)+ "\"> ";
    } else {
      newChatTickerHTML += "<div class=\"chatbubble_right\">";
      newChatTickerHTML += "<div class=\"chatbubble\">" +chattxt+ "</div>";
      newChatTickerHTML += "</div>";
      newChatTickerHTML += "<img class=\"doggy_right\" id=\"doggy_img_" + i + "\" alt=\"" +avatar+ "\" title=\"" +avatar+ "\" src=\"AvatarImage.aspx?" +encodeURIComponent(avatar)+ "&flip=x\"> ";
    }
    
    
    //preload dog images
    imgObj = new Image();
    imgObj.src = "AvatarImage.aspx?" +encodeURIComponent(avatar);
  }

  //reload AvatarOverlay
  avatarOverlayURL = "/AvatarOverlay.aspx?" + Math.random();
  imgObj = new Image();
  imgObj.src = avatarOverlayURL;
  imgObj = null;

  setReloadTimer(reloadTimeOut);
  
  if (firstRun) {
    showChatTicker(worldName);
    firstRun=false;
  } else {
    setTimeout(function () { showChatTicker(worldName); }, 3000);
  }
}


var mapX = 200, mapY = 200;
var moveTimer = null;
function moveMap(x, y) {
  var dx = x - mapX, dy = y - mapY;
  mapX += Math.ceil(0.25 * dx);
  mapY += Math.ceil(0.25 * dy);

  document.getElementById('worldmap').style.backgroundPosition = "" + (-mapX) + "px " + (-mapY) + "px";
  document.getElementById('dogoverlay').style.backgroundPosition = "" + (-mapX) + "px " + (-mapY) + "px";

  if(x == mapX && y == mapY) return;  
  if(moveTimer != null) clearTimeout(moveTimer);
  moveTimer = setTimeout(function () {moveMap(x, y); }, 50);
}

function showChatTicker(worldName) {
  moveMap(mapPosX, mapPosY);
  document.getElementById('chatticker').innerHTML = newChatTickerHTML;
  document.getElementById('dogoverlay').style.backgroundImage = "url('" +avatarOverlayURL+ "')";
  adjustWorldName("Live fra ", worldName, true);  
}

setReloadTimer(1);s