function snippetSwitcher(divID) {
  var snippets = new Array();
  var currSnippet = 0;
  var timerID;
  var timerMsec;
  var nextBtn;
  var prevBtn;
  
  var loop = false;
  this.setLoop = function(sLoop) {
    loop = sLoop;
  }
  
  function showSnippet() {
    document.getElementById(divID).innerHTML = snippets[currSnippet];
    if (nextBtn) setNextBtnSrc(false);
    if (prevBtn) setPrevBtnSrc(false);
  }
  
  this.add = function(snippet) {
    snippets[snippets.length] = snippet;
  }
  
  this.count = function() {
    return snippets.length;
  }
 
 
  this.show = function(snippetId) {
    currSnippet = snippetId;
    showSnippet();
  }
 
  this.showFirst = function() {
    currSnippet = 0;
    showSnippet();
  }

  this.showRand = function() {
    timerPause();
    currSnippet = Math.floor(1 + Math.random() * snippets.length) -1;
    showSnippet();
  }
  
  function doShowNext() {
    if (currSnippet >= snippets.length-1) {
      if (loop) {
        currSnippet = 0;
      } else {
        return;
      }
    } else {
      currSnippet++;
    }
    showSnippet();    
  }
  this.showNext = function() {
    timerPause();
    doShowNext();
  }

  
  function doShowPrev() {
    if (currSnippet <= 0) {
      if (loop) {
        currSnippet = snippets.length-1;
      } else {
        return;
      }
    } else {
      currSnippet--;
    }
    showSnippet();    
  }
  this.showPrev = function() {
    timerPause();
    doShowPrev();
  }
  
  function setNextBtnSrc(hover) {
    if ((!loop) && (currSnippet >= snippets.length-1)) {
      nextBtn.src = '_img/right_gray.gif';
    } else {
      if (!hover) {
        nextBtn.src = '_img/right.gif';
      } else {
        nextBtn.src = '_img/right_hover.gif';
      }
    }
  }

  function setPrevBtnSrc(hover) {
    if ((!loop) && (currSnippet == 0)) {
      prevBtn.src = '_img/left_gray.gif';
    } else {
      if (!hover) {
        prevBtn.src = '_img/left.gif';
      } else {
        prevBtn.src = '_img/left_hover.gif';
      }
    }
  }  
  
  
  this.getNextBtn = function() {
    nextBtn = document.createElement('input');
    nextBtn.type = 'image';
    nextBtn.onclick = this.showNext;
    nextBtn.className="switcher_btn";
    nextBtn.onmouseover = function() {setNextBtnSrc(true)};
    nextBtn.onmouseout = function() {setNextBtnSrc(false)};
    setNextBtnSrc(false);
    return nextBtn;
  }

  this.getPrevBtn = function() {
    prevBtn = document.createElement('input');
    prevBtn.type = 'image';
    prevBtn.onclick = this.showPrev;
    prevBtn.className="switcher_btn";
    prevBtn.onmouseover = function() {setPrevBtnSrc(true);};
    prevBtn.onmouseout = function() {setPrevBtnSrc(false);};
    setPrevBtnSrc(false);
    return prevBtn;
  }

  function timerTick() {
    clearTimeout(timerID);

    if (currSnippet >= snippets.length-1) {
      currSnippet = 0;
    } else {
      currSnippet++;
    }
    showSnippet();    

    timerID = setTimeout(timerTick, timerMsec);
  }
  
  function timerPause() {
    if (timerMsec > 0) {
      clearTimeout(timerID);
      timerID = setTimeout(timerTick, 30000);
    }
  }

  this.setTimer = function (seconds) {
    timerMsec = (seconds * 1000)
    if (timerMsec > 0) {
      timerID = setTimeout(timerTick, timerMsec);
    } else {
      clearTimeout(timerID);
    }
  }
  
}