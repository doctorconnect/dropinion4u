function addScrollers()
{
 // startScroll('id of scroller div','content of scroller');
    startScroll('jsMarquee','Coronavirus disease (COVID-19) is an infectious disease caused by a newly discovered coronavirus..<br \/><br \/>Dr.opinoin The first step to publish your manuscript is to select appropriate journal <br \/><br \/> https://www.coronatracker.com/blog/index.php/about/. <br \/><br \/> India Coronavirus update with statistics and graphs: total and ... https://www.worldometers.info/coronavirus/country/india/ .'
);
}
var speed=15; // scroll speed (bigger = faster)
var dR=false; // reverse direction
var step = 2;
function objWidth(obj)
{
 if(obj.offsetWidth) return obj.offsetWidth;
 if (obj.clip) return obj.clip.width; return 0;
}
function objHeight(obj)
{
 if(obj.offsetHeight) return obj.offsetHeight;
 if (obj.clip) return obj.clip.height; return 0;
}
function scrF(i,sH,eH)
{
 var x=parseInt(i.top)+(dR? step: -step);
 if(dR && x>sH)x=-eH;
 else if(x<2-eH)x=sH;i.top = x+'px';
}
function startScroll(sN,txt)
{
 var scr=document.getElementById(sN);
 var sW = objWidth(scr)-6;
 var sH = objHeight(scr);
 scr.innerHTML = '<div id="'+sN+'in" style="position:absolute; left:3px; width:'+sW+';">'+txt+'<\/div>';
 var sTxt=document.getElementById(sN+'in');
 var eH=objHeight(sTxt); sTxt.style.top=(dR? -eH : sH)+'px';
 sTxt.style.clip='rect(0,'+sW+'px,'+eH+'px,0)';
 setInterval(function() {scrF(sTxt.style,sH,eH);},1000/speed);
}
window.onload = addScrollers;
