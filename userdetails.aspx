<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="userdetails.aspx.cs"
    Inherits="userdetails" %>

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
                    <h4>Login Details</h4>
                </asp:Panel>
            </section>
            <!-- Main content -->
            <section class="content" style="height:100px;">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box">
                            <!-- /.box-header -->
                            <div class="box-body">
                                <table class="table table-bordered" style="width:500px;">
                                    <tr>
                                        <th>
                                            <b>User name</b>
                                        </th>
                                        <td>
                                            <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <b>Password</b>
                                        </th>
                                        <td>
                                            <asp:Label ID="lblPassword" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
