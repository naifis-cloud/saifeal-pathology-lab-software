<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="test-barcodes.aspx.cs"
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
                <h4> <asp:Label ID = "lblPatientName" runat = "server"></asp:Label>
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
                            <table id="example1" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Sr.No</th>
                                        <th>Tests</th>
                                        <th>Barcode</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    
                                     <asp:Repeater ID = "rptPatientTests" runat = "server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#(Container.ItemIndex+1)%></td>
                                                <td><asp:Label ID = "lblTestName" runat = "server" Text = '<%#Eval("stTestName")%>'></asp:Label>
                                                <asp:Label ID = "lblTestId" runat = "server" Text = '<%#Eval("inTestId")%>' Visible = "false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID = "txtBarCode" runat = "server" MaxLength = "50" placeholder="Barcode" CssClass = "form-control"
                                                    Text = "<%#Guid.NewGuid().ToString().ToUpper().Substring(0,10)%>"></asp:TextBox>
                                                    <%--ReadOnly = "true"--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        
                        <!-- /.box-body -->
                    </div>
                    <div>
                        <asp:Button ID = "btnSave" runat = "server" CssClass = "btn btn-primary" Text = "Save Barcodes" OnClick="btnSave_Click"/>
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
