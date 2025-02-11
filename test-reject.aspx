<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="test-reject.aspx.cs"
    Inherits="add_list_samples" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>SBS LIS | Pathology lab automation software</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no'
        name='viewport'>
    <!-- FontAwesome 4.3.0 -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css"
        rel="stylesheet" type="text/css" />
    <!-- Ionicons 2.0.0 -->
    <link href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css"
        rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="~/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins 
         folder instead of downloading all of them to reduce the load. -->
    <link href="~/dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />
</head>
<body class="skin-blue sidebar-mini">
    <form id="form1" runat="server">
    <div>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <asp:Panel ID = "pnlHeader"  runat = "server">
                <h4>Test Rejection for  - <asp:Label ID = "lblPatientName" runat = "server"></asp:Label>
                    <small></small>
                </h4>
            </asp:Panel>
        </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <table id="example1" class="table">
                                <tr>
                                    <td>
                                        <b>Rejection Reason: (Max 500 characters)</b><br />
                                        <asp:TextBox ID = "txtReason" TextMode = "MultiLine" runat = "server" Width = "650px" MaxLength = "500" Rows = "8" Columns = "6" Height = "150px"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalReason"
                                            ControlToValidate="txtReason" runat="server" ErrorMessage="Enter valid Rejection Reason" ValidationGroup="vgRejection"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align = "center">
                                        <asp:Button ID = "btnReject" runat = "server" CssClass = "btn btn-primary" Width = "150px" Text = "Reject" OnClick="btnSave_Click"
                                         ValidationGroup="vgRejection"/>
                                        <asp:ValidationSummary ID="valColCenter" runat="server" 
                                            ValidationGroup="vgRejection" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
                <!-- /.col -->
            </div>
            </asp:Panel>
            <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
    </form>
</body>
</html>
