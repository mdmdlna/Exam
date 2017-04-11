<!--#include file="conn.inc"-->
<html>

<head>
<meta http-equiv="Content-Language" content="zh-cn">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<title>新建网页 2</title>
<link rel="stylesheet" type="text/css" href="css.css">
</head>

<body>

<table border="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="100%" height="30" style="border-left-style: solid; border-left-width: 0; border-right-style: solid; border-right-width: 0; border-top-style: solid; border-top-width: 0; border-bottom: 1px solid #000000">
    <img border="0" src="images/selectsubject.jpg"></td>
  </tr>
</table>
<%
response.cookies("downloadok")=""
userid=request("userid")

if userid <>  0 then
sql1="select * from exam_testuser where havetest=0 and userid="& request.cookies("userid") 

set rs1=server.createobject("adodb.recordset")
rs1.open sql1,conn,3,2
if rs1.eof then
%>
<table border="0" cellspacing="0" bordercolor="#111111" width="480">
  <tr>
    <td width="100%" height="30">&nbsp;现在您还没有需要参加考试的科目！ </td>
  </tr>
</table>
<%
else
i=1
%>
<table border="1" cellspacing="1" style="border-collapse: collapse" bordercolor="#C0C0C0" id="AutoNumber2" width="480" cellpadding="0">
  <tr>
    <td height="22" width="20" bgcolor="#E1E1E1"></td>
    <td bgcolor="#E1E1E1">&nbsp;<b>科目名称</b></td>
    <td bgcolor="#E1E1E1" width="80">
    <p align="center"><b>考试时间</b></td>
    <td bgcolor="#E1E1E1" width="80">
    <p align="center"><b>操作</b></td>
  </tr>
  <%
do while not rs1.eof

sql="select * from exam_test where getdate() between starttime and endtime and not passuserid is null and testid=" & rs1("testid")
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
if not rs.eof then
%>
  <tr>
    <td align="center" height="20" width="20" bgcolor="#E1E1E1"><%=i%></td>
    <td bgcolor="#FFFFFF">
    <p align="left">&nbsp;<%=rs("subject")%></td>
    <td bgcolor="#FFFFFF">
    <p align="center">&nbsp;<%=rs("testtime")%>分钟</td>
    <td bgcolor="#FFFFFF">
    <p align="center"><a href="paper.asp?testid=<%=rs1("testid")%>">进入考场</a></td>
  </tr>
  <%
end if
rs1.movenext
i=i+1
loop
%>
</table>
<%
end if
%>

<%
else
i=1
%>
<table border="1" cellspacing="1" style="border-collapse: collapse" bordercolor="#C0C0C0" id="AutoNumber2" width="480" cellpadding="0">
  <tr>
    <td height="22" width="20" bgcolor="#E1E1E1"></td>
    <td bgcolor="#E1E1E1">&nbsp;<b>科目名称</b></td>
    <td bgcolor="#E1E1E1" width="80">
    <p align="center"><b>考试时间</b></td>
    <td bgcolor="#E1E1E1" width="80">
    <p align="center"><b>操作</b></td>
  </tr>
<%
sql="select * from exam_test where getdate() between starttime and endtime and not passuserid is null "
set rs=server.createobject("adodb.recordset")
rs.open sql,conn,3,2
do while not rs.eof
%>
  <tr>
    <td align="center" height="20" width="20" bgcolor="#E1E1E1"><%=i%></td>
    <td bgcolor="#FFFFFF">
    <p align="left">&nbsp;<%=rs("subject")%></td>
    <td bgcolor="#FFFFFF">
    <p align="center">&nbsp;<%=rs("testtime")%>分钟</td>
    <td bgcolor="#FFFFFF">
    <p align="center"><a href="paper.asp?testid=<%=rs("testid")%>">进入考场</a></td>
  </tr>
  <% 
i=i+1
rs.movenext
loop
%>
</table>
<%
end if
%>
 


</body>

</html>
