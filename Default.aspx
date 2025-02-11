<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>SBS LIS | Log in</title>
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
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
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
                Sign in to start your session</p>
            <div class="form-group has-feedback">
                <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Username"
                    MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalEmail"
                    ControlToValidate="txtUserName" runat="server" ErrorMessage="Enter Username.."
                    ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                <%--<asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="regexpEmail"
                    runat="server" ControlToValidate="txtUserName" ErrorMessage="Enter Valid Email Address.."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgLogin"></asp:RegularExpressionValidator>--%>
                <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
            </div>
            <div class="form-group has-feedback">
                <asp:TextBox ID="txtpassword" runat="server" class="form-control" TextMode="Password"
                    placeholder="Password" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalpassword"
                    ControlToValidate="txtpassword" runat="server" ErrorMessage="Enter Password.."
                    ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                <span class="glyphicon glyphicon-lock form-control-feedback"></span>
            </div>
            <div class="form-group has-feedback">
                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRoles"
                    ControlToValidate="ddlRoles" runat="server" ErrorMessage="Select Role.." ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
            </div>
            <div class="row">
                <div class="col-xs-8">
                    <div class="checkbox icheck">
                        <label>
                            <asp:CheckBox ID="chkRemember" runat="server" Text="" />
                            Remember Me
                        </label>
                    </div>
                </div>
                <!-- /.col -->
                <div class="col-xs-4">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-block btn-flat"
                        Text="Sign In" ValidationGroup="vgLogin" OnClick="btnSubmit_Click" />
                    <asp:ValidationSummary ID="valLogin" runat="server" ValidationGroup="vgLogin" ShowMessageBox="true"
                        DisplayMode="List" ShowSummary="false" />
                </div>
                <!-- /.col -->
            </div>
            <div class="social-auth-links text-center" style="display: none">
                <p>
                    - OR -</p>
                <a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook">
                </i>Sign in using Facebook</a> <a href="#" class="btn btn-block btn-social btn-google-plus btn-flat">
                    <i class="fa fa-google-plus"></i>Sign in using Google+</a>
            </div>
            <!-- /.social-auth-links -->
            <a href="forgot-password.aspx">I forgot my password</a><br>
            <%--<a href="register.html" class="text-center">Register a new membership</a>--%>
        </div>
        <!-- /.login-box-body -->
    </div>
    <!-- /.login-box -->
    <!-- jQuery 2.1.4 -->
    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' // optional
            });
        });
    </script>
    </form>
</body>
</html>
