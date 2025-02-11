<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgot-password.aspx.cs"
    Inherits="forgot_password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>SBS LIS | Forgot Password</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no'
        name='viewport'>
    <!-- Bootstrap 3.3.4 -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css"
        rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- iCheck -->
    <link href="plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" />
    <style>
        .login-box
        {
            width: 500px;
            margin: 7% auto;
        }
    </style>
</head>
<body class="login-page">
    <form id="form1" runat="server" defaultbutton="btnSubmit">
    <div class="login-box">
        <div class="login-logo">
            <b>SBS </b>LIS
        </div>
        <!-- /.login-logo -->
        <div class="login-box-body">
            <p class="login-box-msg">
                Enter your registered email address and select your respective role.<br />
                We will email you your login details.</p>
            <div class="form-group has-feedback">
                <asp:TextBox ID="txtemail" runat="server" class="form-control" placeholder="Email"
                    MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalEmail"
                    ControlToValidate="txtemail" runat="server" ErrorMessage="Enter Email Address.."
                    ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="regexpEmail"
                    runat="server" ControlToValidate="txtemail" ErrorMessage="Enter Valid Email Address.."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgLogin"></asp:RegularExpressionValidator>
                <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
            </div>
            <div class="form-group has-feedback">
                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRoles"
                    ControlToValidate="ddlRoles" runat="server" ErrorMessage="Select Role.." ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
            </div>
            <div class="row">
                <div class="col-xs-6">
                </div>
                <div class="col-xs-3" style="float:left;">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-block btn-flat"
                        Text="Submit" ValidationGroup="vgLogin" OnClick="btnSubmit_Click" />
                </div>
                <!-- /.col -->
                <div class="col-xs-3" style="float:right;">
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-block btn-flat"
                        Text="Cancel" CausesValidation="false" PostBackUrl="~/Default.aspx" />
                    <asp:ValidationSummary ID="valLogin" runat="server" ValidationGroup="vgLogin" ShowMessageBox="true"
                        DisplayMode="List" ShowSummary="false" />
                    <!-- /.col -->
                </div>
            </div>
        </div>
        <!-- /.login-box-body -->
    </div>
    <!-- /.login-box -->
    </form>
</body>
</html>
