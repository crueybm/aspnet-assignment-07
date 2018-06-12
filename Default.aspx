<%--
    Name: Brandon Cruey 
    email: crueybm@mail.uc.edu
    Class: Web Server Application Development
    Date: 03/21/2018
    Assignment: 07
    Description: In this program, the user will enter a website into a textbox, and press a button.  The program will search all
                 of the html and text on the page, finding all email addresses, and giving the user an option to select a list of 
                 addresses inside a checkboxlist.  Once email addresses are selected, the user can send an auto-generated email
                 by clicking another button.
    Citation:    https://www.aspsnippets.com/Articles/Send-email-using-Gmail-SMTP-Mail-Server-in-ASPNet.aspx
--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assignment 07</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-sm-4">
                    <%--Declares label "txtTitle"--%>
                    <asp:Label ID="txtTitle" runat="server" Text="Enter a URL: "></asp:Label>
                    <br />
                    <%--Declares label "txtValue"--%>
                    <asp:Label ID="txtValue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <%--Declares textBox "txtURL"--%>
                    <asp:TextBox ID="txtURL" runat="server"></asp:TextBox>
                    <br />
                    <%--Declares button "btnUrl"--%>
                    <asp:Button ID="btnUrl" runat="server" Text="Submit" OnClick="btnUrl_Click" AutoPostBack="false"/><br />
                    <%--Declares listBox "lbxEmail"--%>
                    <asp:ListBox ID="lbxEmail" runat="server"></asp:ListBox><br />
                </div>     
                <div class="col-sm-4">
                    <%--Label for cblEmails--%>
                    <asp:Label ID="lblCheckBoxList" runat="server" Text="Select Address(es):"></asp:Label>
                    <br />
                    <%--Creates a scrolling div that will display a vertical maximum of 300px before the user scrolls down--%>
                    <div style="overflow-y: scroll; height: 300px"> 
                        <%--Declares checkboxlist "cblEmails"--%>
                        <asp:CheckBoxList ID="cblEmails" runat="server" OnSelectedIndexChanged="cblEmails_SelectedIndexChanged"></asp:CheckBoxList>
                    </div>
                </div>
                <div class="col-sm-4">
                    <%--Label for subject/body--%>
                    <asp:Label ID="lblForm" runat="server" Text="Enter Subject/Body:"></asp:Label>
                    <%--Declares hidden label "txtEmail"--%>
                    <asp:Label ID="txtEmail" runat="server" Text="crueyit3047@gmail.com" Visible="false"></asp:Label>
                    <%--Declares hidden label "txtPassword"--%>
                    <asp:Label ID="txtPassword" runat="server" Text="!MrR0b0t" Visible="false"></asp:Label>
                    <%--Declares label txtSubject--%>
                    <asp:Label ID="lblSubject" runat="server" Text="Enter a Subject:" Visible="true"></asp:Label>
                    <br />
                    <%--Creates a textbox for the user to enter a subject--%>
                    <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                    <br />
                    <%--Declares label txtBody--%>
                    <asp:Label ID="lblBody" runat="server" Text="Enter a Message:" Visible="true"></asp:Label>
                    <br />
                    <%--Creates a textbox for the user to enter a message--%>
                    <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <%--Declares button "btnSubmit"--%>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
