<!--#include file="conn.inc"-->
<html>

<head>

<%
Response.Buffer=True
Response.ExpiresAbsolute=Now()-1
Response.Expires=0
Response.CacheControl="no-cache"
%>

<meta http-equiv="Content-Language" content="zh-cn">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<title>南通农行网上考试</title>
<link rel="stylesheet" type="text/css" href="css.css">
<style>
<!--
.outborder   { border-left: 1px solid #333333; border-right: 1px solid #000000; 
               border-top: 1px solid #99CCFF; border-bottom: 1px solid #000000; 
               background-color: #336699 }
.inputborder { color: #00FF00; text-align: center; border-style: solid; border-width: 1; 
               background-color: #333333 }
-->
</style>
<script language="javascript">
<!--
float_init	= 1;
function DHTML_Init(Object) { 
  if (navigator.userAgent.match(/Mozilla\/5\../) && float_init) { 
 	 SetObjectOffsetTop(Object, undefined);
  } 
}
function All (ID) { 
  if (document.all)	{  return document.all[ID];   } 
  else if (document.documentElement){
  return document.getElementById (ID); 
  } 
  else if (document.layers)	{ return document.layers[ID]; }
}
function GetWindowOffsetTop() {
  if (window.innerHeight)	{ return window.pageYOffset; }
  else if (document.body)	{ return document.body.scrollTop; }
} 
function GetWindowHeight() { 
  if (window.innerHeight)	{ return window.innerHeight; } 
  else if (document.body)	{ return document.body.clientHeight; }} 
function GetObjectHeight(Object) {
  DHTML_Init(Object);
  if (document.all || document.documentElement)	{
    Clip = Object.style.clip; 
  if (! Clip) { return Object.offsetHeight; }
  else	{ return GetClipElement (Clip, 'Bottom'); }} 
  else if (document.layers)	{ return Object.clip.height; }} 
function GetClipElement (Clip, Element) {
  Clip = Clip.substr(Clip.indexOf('(') + 1); 
  Clip = Clip.substr(0, Clip.length - 1);
  Clippers = Clip.split (" "); 
  for (i = 0; i < Clippers.length; i++) { 
    if (Clippers[i] != 'auto') {
    Clippers[i] = Clippers[i].replace (/D/g, ""); }
  } 
  ClipTop = Number(Clippers[0]); 
  ClipRight = Number(Clippers[1]); 
  ClipBottom = Number(Clippers[2]);
  ClipLeft = Number(Clippers[3]); 
  if (Element == 'Top')		{ return ClipTop; } 
    else if (Element == 'Right')	{ return ClipRight; } 
    else if (Element == 'Bottom')	{ return ClipBottom; } 
    else if (Element == 'Left')	{ return ClipLeft; } 
    else				{ return undefined; }} 
function GetObjectOffsetTop(Object) { 
  DHTML_Init(Object); 
  if (Object.offsetTop)	{ return Object.offsetTop; } 
  else if (document.layers)	{ return Object.top; }
} 
function SetObjectOffsetTop(Object, Offset) { 
  if (Object.style)	{ Object.style.top = Offset; } 
  else if (Object.top)	{ Object.top = Offset; }} 
  CenterMenu = 1;	 
  MenuBorder = 100; 
  TimeCheck  = 250;	 
  TimeUpdate = 15; 
  DivUpdate  = 15;	 
  Minimum    = 50;	 
  AddHeight  = -4; 
function ScrollMenu() { 
  Menu		= All('persistMenu'); 
  WinTop	= GetWindowOffsetTop(); 
  WinHeight	= GetWindowHeight() + AddHeight;
  MenuTop	= GetObjectOffsetTop(Menu); 
  MenuHeight = GetObjectHeight (Menu); 
  MenuNew	= (CenterMenu) ? Math.round (WinTop + (WinHeight - MenuHeight) / 1) : WinTop + MenuBorder; 
  if (MenuNew < Minimum) 
  { MenuNew = Minimum; }
  if (MenuTop != MenuNew) { 
  if ( (MenuTop + MenuHeight) < WinTop || MenuTop > (WinTop + WinHeight) ) { 
    SetObjectOffsetTop (Menu, (MenuTop < MenuNew) ? (WinTop - MenuHeight) : (WinTop + WinHeight)); 
  }
  else { 
     Add = (MenuTop < MenuNew) ? 1 : -1; 
     SetObjectOffsetTop (Menu, MenuTop + Math.round((MenuNew - MenuTop) / DivUpdate) + Add); 
     } 
     } 
         window.setTimeout('ScrollMenu()', (GetObjectOffsetTop(Menu) == MenuNew) ? TimeCheck : TimeUpdate); 
     }

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_showHideLayers() { //v3.0
  var i,p,v,obj,args=MM_showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v='hide')?'hidden':v; }
    obj.visibility=v; }
}
//-->
</script>
</head>

<body bgcolor="#FFFFFF">

<%
sql="select * from exam_database where mark=1"
set rsok=server.createobject("adodb.recordset")
rsok.open sql,conn,3,2
do while not rsok.eof
  rsok("mark")=0
  rsok.update
rsok.movenext
loop


if request("downloadok")<>"y" then
sql="select * from exam_test where testid="& request("testid")
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
subject=rs("subject")
singlecount=rs("singlecount")
singleper=rs("singleper")
multicount=rs("multicount")
multiper=rs("multiper")
judgecount=rs("judgecount")
judgeper=rs("judgeper")
starttime=rs("starttime")
endtime=rs("endtime")
testtime=rs("testtime")
rs.close
set rs=nothing
%>
<p align="center"><b><font face="黑体" size="5" color="#FF0000">南通农行<%=subject%>考试</font></b></p>
<form method="POST" action="createresult.asp" onSubmit="return submitit();" name="testform">
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
    <tr>
      <td width="100%" height="25"><b><font size="3" color="#000080">一、单项选择题(每题<%=singleper%>分,共<%=singlecount%>题)</font></b></td>
    </tr>
  </table>
  
  
<%
randomize
'高管 for i=1 to singlecount
for i=1 to 16
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='单选题' and gglx=1"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.move temp
rs("mark")=1

%>
  <table border="0" cellspacing="1" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" id="AutoNumber2" cellpadding="0">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i%>、<%=rs("question")%></b></td>
    </tr>
    <%
  if rs("text1")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="A">A、<%=rs("text1")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text2")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="B">B、<%=rs("text2")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text3")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="C">C、<%=rs("text3")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text4")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="D">D、<%=rs("text4")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text5")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="E">E、<%=rs("text5")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text6")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="F">F、<%=rs("text6")%></td>
    </tr>
    <%
  end if
  %>
  </table>
  <%
j=j+1
rs.movenext
next
rs.close
set rs=nothing
%>


<%
randomize
'高管 for i=1 to singlecount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='单选题' and gglx=2"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.move temp
rs("mark")=1

%>
  <table border="0" cellspacing="1" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" id="AutoNumber2" cellpadding="0">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+16%>、<%=rs("question")%></b></td>
    </tr>
    <%
  if rs("text1")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="A">A、<%=rs("text1")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text2")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="B">B、<%=rs("text2")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text3")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="C">C、<%=rs("text3")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text4")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="D">D、<%=rs("text4")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text5")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="E">E、<%=rs("text5")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text6")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="F">F、<%=rs("text6")%></td>
    </tr>
    <%
  end if
  %>
  </table>
  <%
j=j+1
rs.movenext
next
rs.close
set rs=nothing
%>


<%
randomize
'高管 for i=1 to singlecount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='单选题' and gglx=3"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.move temp
rs("mark")=1

%>
  <table border="0" cellspacing="1" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" id="AutoNumber2" cellpadding="0">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+22%>、<%=rs("question")%></b></td>
    </tr>
    <%
  if rs("text1")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="A">A、<%=rs("text1")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text2")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="B">B、<%=rs("text2")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text3")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="C">C、<%=rs("text3")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text4")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="D">D、<%=rs("text4")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text5")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="E">E、<%=rs("text5")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text6")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="F">F、<%=rs("text6")%></td>
    </tr>
    <%
  end if
  %>
  </table>
  <%
j=j+1
rs.movenext
next
rs.close
set rs=nothing
%>

<%
randomize
'高管 for i=1 to singlecount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='单选题' and gglx=4"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.move temp
rs("mark")=1

%>
  <table border="0" cellspacing="1" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" id="AutoNumber2" cellpadding="0">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+28%>、<%=rs("question")%></b></td>
    </tr>
    <%
  if rs("text1")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="A">A、<%=rs("text1")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text2")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="B">B、<%=rs("text2")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text3")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="C">C、<%=rs("text3")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text4")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="D">D、<%=rs("text4")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text5")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="E">E、<%=rs("text5")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text6")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="F">F、<%=rs("text6")%></td>
    </tr>
    <%
  end if
  %>
  </table>
  <%
j=j+1
rs.movenext
next
rs.close
set rs=nothing
%>


<%
randomize
'高管 for i=1 to singlecount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='单选题' and gglx=5"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.move temp
rs("mark")=1

%>
  <table border="0" cellspacing="1" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" id="AutoNumber2" cellpadding="0">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+34%>、<%=rs("question")%></b></td>
    </tr>
    <%
  if rs("text1")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="A">A、<%=rs("text1")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text2")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="B">B、<%=rs("text2")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text3")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="C">C、<%=rs("text3")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text4")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="D">D、<%=rs("text4")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text5")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="E">E、<%=rs("text5")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text6")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="F">F、<%=rs("text6")%></td>
    </tr>
    <%
  end if
  %>
  </table>
  <%
j=j+1
rs.movenext
next
rs.close
set rs=nothing
%>


<table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
    <tr>
      <td width="100%" height="25"><b><font size="3" color="#000080">二、多项选择题(每题<%=multiper%>分,共<%=multicount%>题)</font></b></td>
    </tr>
  </table>

<%
randomize
'高管for i=1 to multicount
for i=1 to 16
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='多选题' and  gglx=1"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.movefirst
rs.move temp
rs("mark")=1
%>
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i%>、<%=rs("question")%></b></td>
    </tr>
    <%
  if rs("text1")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="A">A、<%=rs("text1")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text2")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="B">B、<%=rs("text2")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text3")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="C">C、<%=rs("text3")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text4")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="D">D、<%=rs("text4")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text5")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="E">E、<%=rs("text5")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text6")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="F">F、<%=rs("text6")%></td>
    </tr>
    <%
  end if
  %>
  </table>
  <%
j=j+1
rs.movenext
next
if multicount > 0 then
rs.close
set rs=nothing
end if
%>


<%
randomize
'高管for i=1 to multicount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='多选题' and  gglx=2"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.movefirst
rs.move temp
rs("mark")=1
%>
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+16%>、<%=rs("question")%></b></td>
    </tr>
    <%
  if rs("text1")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="A">A、<%=rs("text1")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text2")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="B">B、<%=rs("text2")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text3")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="C">C、<%=rs("text3")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text4")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="D">D、<%=rs("text4")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text5")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="E">E、<%=rs("text5")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text6")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="F">F、<%=rs("text6")%></td>
    </tr>
    <%
  end if
  %>
  </table>
  <%
j=j+1
rs.movenext
next
if multicount > 0 then
rs.close
set rs=nothing
end if
%>


<%
randomize
'高管for i=1 to multicount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='多选题' and  gglx=3"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.movefirst
rs.move temp
rs("mark")=1
%>
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+22%>、<%=rs("question")%></b></td>
    </tr>
    <%
  if rs("text1")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="A">A、<%=rs("text1")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text2")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="B">B、<%=rs("text2")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text3")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="C">C、<%=rs("text3")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text4")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="D">D、<%=rs("text4")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text5")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="E">E、<%=rs("text5")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text6")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="F">F、<%=rs("text6")%></td>
    </tr>
    <%
  end if
  %>
  </table>
  <%
j=j+1
rs.movenext
next
if multicount > 0 then
rs.close
set rs=nothing
end if
%>


<%
randomize
'高管for i=1 to multicount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='多选题' and  gglx=4"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.movefirst
rs.move temp
rs("mark")=1
%>
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+28%>、<%=rs("question")%></b></td>
    </tr>
    <%
  if rs("text1")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="A">A、<%=rs("text1")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text2")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="B">B、<%=rs("text2")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text3")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="C">C、<%=rs("text3")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text4")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="D">D、<%=rs("text4")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text5")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="E">E、<%=rs("text5")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text6")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="F">F、<%=rs("text6")%></td>
    </tr>
    <%
  end if
  %>
  </table>
  <%
j=j+1
rs.movenext
next
if multicount > 0 then
rs.close
set rs=nothing
end if
%>


<%
randomize
'高管for i=1 to multicount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='多选题' and  gglx=5"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.movefirst
rs.move temp
rs("mark")=1
%>
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+34%>、<%=rs("question")%></b></td>
    </tr>
    <%
  if rs("text1")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="A">A、<%=rs("text1")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text2")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="B">B、<%=rs("text2")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text3")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="C">C、<%=rs("text3")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text4")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="D">D、<%=rs("text4")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text5")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="E">E、<%=rs("text5")%></td>
    </tr>
    <%
  end if
  %> <%
  if rs("text6")<>"" then
  %>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="NO<%=rs("id")%>" value="F">F、<%=rs("text6")%></td>
    </tr>
    <%
  end if
  %>
  </table>
  <%
j=j+1
rs.movenext
next
if multicount > 0 then
rs.close
set rs=nothing
end if
%>

<table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
    <tr>
      <td width="100%" height="25"><b><font size="3" color="#000080">三、判断(每题<%=judgeper%>分,共<%=judgecount%>题)</font></b></td>
    </tr>
  </table>

  
  <%
randomize
'高管for i=1 to judgecount
for i=1 to 16
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='判断题' and gglx=1"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.move temp
rs("mark")=1
%>
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i%>、<%=rs("question")%></b></td>
    </tr>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="y">正确</td>
    </tr>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="n">不正确</td>
    </tr>
  </table>
  <%
j=j+1
rs.movenext
next
if judgecount > 0 then
rs.close
set rs=nothing
end if
%>

<%
randomize
'高管for i=1 to judgecount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='判断题' and gglx=2"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.move temp
rs("mark")=1
%>
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+16%>、<%=rs("question")%></b></td>
    </tr>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="y">正确</td>
    </tr>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="n">不正确</td>
    </tr>
  </table>
  <%
j=j+1
rs.movenext
next
if judgecount > 0 then
rs.close
set rs=nothing
end if
%>


<%
randomize
'高管for i=1 to judgecount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='判断题' and gglx=3"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.move temp
rs("mark")=1
%>
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+22%>、<%=rs("question")%></b></td>
    </tr>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="y">正确</td>
    </tr>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="n">不正确</td>
    </tr>
  </table>
  <%
j=j+1
rs.movenext
next
if judgecount > 0 then
rs.close
set rs=nothing
end if
%>

<%
randomize
'高管for i=1 to judgecount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='判断题' and gglx=4"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.move temp
rs("mark")=1
%>
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+28%>、<%=rs("question")%></b></td>
    </tr>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="y">正确</td>
    </tr>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="n">不正确</td>
    </tr>
  </table>
  <%
j=j+1
rs.movenext
next
if judgecount > 0 then
rs.close
set rs=nothing
end if
%>

<%
randomize
'高管for i=1 to judgecount
for i=1 to 6
sql="select * from exam_database where mark=0 and subject='"& subject &"' and type='判断题' and gglx=5"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
count=rs.recordcount
temp=fix(count*rnd)
rs.move temp
rs("mark")=1
%>
  <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
    <tr>
      <td width="100%" bgcolor="#EFEFEF" height="20">&nbsp;&nbsp;<b><%=i+34%>、<%=rs("question")%></b></td>
    </tr>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="y">正确</td>
    </tr>
    <tr>
      <td width="100%">&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="NO<%=rs("id")%>" value="n">不正确</td>
    </tr>
  </table>
  <%
j=j+1
rs.movenext
next
if judgecount > 0 then
rs.close
set rs=nothing
end if
%>



<%
response.cookies("downloadok")="y"
%>
  <p align="center">　</p>
  <p align="center">　</p>
  <p align="center">　</p>
  <p align="center">　</p>
  <p align="center">　</p>
  <input type="hidden" name="testid" value="<%=request("testid")%>" size="20">
  <input type="hidden" name="subject" value="<%=subject%>" size="20">
  <input type="hidden" name="singlecount" value="<%=singlecount%>" size="20">
  <input type="hidden" name="singleper" value="<%=singleper%>" size="20">
  <input type="hidden" name="multicount" value="<%=multicount%>" size="20">
  <input type="hidden" name="multiper" value="<%=multiper%>" size="20">
  <input type="hidden" name="judgecount" value="<%=judgecount%>" size="20">
  <input type="hidden" name="judgeper" value="<%=judgeper%>" size="20">
  <input type="hidden" name="subjectid" value="<%=request("id")%>" size="20">
  <input type="hidden" name="starttime" value="<%=time()%>" size="20">
  <input type="hidden" name="endtime" value="<%=dateadd("n",testtime,time())%>" size="20">
  <input type="hidden" name="testtime" value="<%=testtime%>" size="20">
  <!-- PersistentLayer-->
  <div id="persistMenu" style="position: absolute; height:150px; width:230px; left:360px; top:1px;z-index: 100; visibility: hidden" class="blueborder">
    <table border="1" cellspacing="0" style="border-collapse: collapse; border-width: 1" bordercolor="#111111" width="230" id="AutoNumber1" class="outborder" cellpadding="0" height="20">
      <tr>
        <td>
        <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="160" id="AutoNumber5" height="20">
          <tr>
            <td width="20"><font color="#FFFFFF">
            <img border="0" src="images/timer.gif" width="15" height="15"></font></td>
            <td><b><font color="#FFCC00">计时器</font></b></td>
          </tr>
        </table>
        </td>
      </tr>
    </table>
    <table border="1" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="230" id="AutoNumber2" class="outborder" cellpadding="0">
      <tr>
        <td>
        <div align="center">
          <center>
          <table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" id="AutoNumber3">
            <tr>
              <td height="30">
              <p align="center"><font color="#FFFFFF">开始时间</font></td>
              <td>
              <input type="text" name="mystarttime" size="20" class="inputborder" readonly></td>
            </tr>
            <tr>
              <td height="30">
              <p align="center"><font color="#FFFFFF">结束时间</font></td>
              <td>
              <input type="text" name="myendtime" size="20" class="inputborder" readonly></td>
            </tr>
            <tr>
              <td height="30">
              <p align="center"><font color="#FFFFFF">剩余时间</font></td>
              <td>
              <input type="text" name="lefttime" size="20" class="inputborder" readonly></td>
            </tr>
          </table>
          </center>
        </div>
        </td>
      </tr>
    </table>
    <table border="1" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="230" id="AutoNumber4" class="outborder" height="30">
      <tr>
        <td width="100%">
        <p align="center">
        <input type="submit" value="我要交卷" name="B3" class="s02"></td>
      </tr>
    </table>
  </div>
  <!--End PersistentLayer-->
</form>
<script language="javascript">
alert("确定开始考试并启动计时器！");
MM_showHideLayers('persistMenu','','show');
ScrollMenu();
document.all.mystarttime.value=document.all.starttime.value;



var myh=(document.all.testtime.value-document.all.testtime.value%60)/60 ;
var mym=document.all.testtime.value%60;
var mys=0;



  


document.all.myendtime.value = document.all.endtime.value;
document.all.mystarttime.value = document.all.starttime.value;


function lefttime(){
mys--;
  if (mys<0)
  {
    mys=59;
    mym--;
  }
  if (mym<0)
  {
    mym=59;
    myh--;
    if (myh<0)
    {
    alert("考试时间到，确定查看成绩！");
    testform.submit();
    }
  }
document.all.lefttime.value=myh+":"+mym+":"+mys;
setTimeout("lefttime(myh)",1000);
} 
lefttime();
function submitit(){
if (confirm("未到交卷时间，您确定要提前交卷么？")) 
  return true; 
else 
  return false; 
} 
</script>
<%
sql="select * from exam_database where mark=1"
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
do while not rs.eof
  rs("mark")=0
  rs.update
rs.movenext
loop
else
%>
<p></p>
<p align="center"><b><font face="Wingdings" size="7" color="#FF0000">I</font></b>发生致命 
错误！因为您刷新了此页。</p>
<p align="center">请【<a href="relogin.asp">重新登陆</a>】参加考试。</p>
<p align="center">如果上述错误发生超过三次，系统将取消您的考试资格，！</p>
<%
end if
%>

</body>

</html>
