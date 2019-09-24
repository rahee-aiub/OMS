<%@ Page Language="C#" Title="OMS Login" AutoEventWireup="true" CodeBehind="A2ZLogin.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Loginstyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField ID="OrgPass" runat="server" />
        <asp:HiddenField ID="hdnfldMac" runat="server" />
        <asp:HiddenField ID="hdnfldMacCompare" runat="server" />
        <asp:HiddenField ID="hdnIDFlag" runat="server" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="container">
            <div class="form-container flip">
                <div id="main" runat="server">
                    <div class="login-form">
                        <h3 class="title">Login</h3>
                        <div class="form-group" >
                            <asp:TextBox ID="txtIdNo" runat="server" class="form-input" tooltip-class="username-tooltip" placeholder="UserId" OnTextChanged="txtUserId_TextChanged" required="true"></asp:TextBox>
                        </div>
                        <div class="form-group" >
                            <asp:TextBox ID="txtPassword" runat="server" class="form-input" tooltip-class="password-tooltip" TextMode="Password" MaxLength="8" placeholder="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnLogin" runat="server" class="login-button" Text="Login" OnClick="btnLogin_Click" />
                            <asp:Button ID="btChangePassword" runat="server" class="login-button" Text="Change Password" OnClick="btChangePassword_Click1"  />
                        </div>
                    </div>
                </div>
                <div id="Pass" runat="server">
                    <div class="login-form2">
                        <h3 class="title">Change Password</h3>
                        <div class="form-group" >
                            <asp:TextBox ID="txtOldPassword" runat="server" MaxLength="8" class="form-input" TextMode="Password" placeholder="Old Password" OnTextChanged="txtUserId_TextChanged" ></asp:TextBox>
                        </div>
                        <div class="form-group" >
                            <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="8" class="form-input" tooltip-class="password-tooltip" TextMode="Password"  placeholder="Password"></asp:TextBox>
                        </div>
                        <div class="form-group" ">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="8" class="form-input" tooltip-class="password-tooltip" TextMode="Password"  placeholder="Confirm Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnPasswordChange" runat="server" class="login-button" Text="Submit" OnClick="btnPasswordChange_Click"   />
                           <asp:Button ID="btnCancel" runat="server" class="login-button" Text="Cancel" OnClick="btnCancel_Click1"  />
                        </div>
                    </div>
                </div>

                <div class="loading">
                    <div class="loading-spinner-large" style="display: none;"></div>
                    <div class="loading-spinner-small" style="display: none;"></div>
                </div>
            </div>
        </div>

    </form>

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
 
     <div align="center">
        <table>
            <thead>
                <tr>
                    <th> <h2> Developed By AtoZ Computer Services - Version 1.0<br />
                              Last Update: January, 2019</h2>
                    </th>
                </tr>
            </thead>
        </table>
    </div>
                   

</body>
</html>
