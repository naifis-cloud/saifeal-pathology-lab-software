<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="check-my-price-list-report.aspx.cs" Inherits="check_my_price_list_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- page script -->
    <script type="text/javascript">
        $(function () {
            $('#example1').dataTable({
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": false
            });
        });
    </script>

    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Check My Price List Report
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Collection Summary Report</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box">
                            <div class="box-header">
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <table>
                                    <tr>
                                        <td>
                                            <span class="active">Download options:</span>&nbsp;
                                            <asp:LinkButton ID = "lnkbtnExcel" runat = "server" onclick="lnkbtnExcel_Click">
                                                <img src = "images/excel.png" title = "Export to excel" />
                                            </asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID = "lnkbtnPDF" runat = "server" onclick="lnkbtnPDF_Click">
                                                <img src= "images/pdf.png" title = "Export to PDF" />
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table><br />
                                <asp:Panel ID = "pnlPriceList" runat = "server">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Test Code</th>
                                            <th>Test Name</th>
                                            <th>Department</th>
                                            <th>Price</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id = "trNoData" runat = "server">
                                            <td colspan = "11">
                                                <center>
                                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No data found!" style="text-align:center;"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID = "rptTests" runat = "server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("stTestCode")%></td>
                                                    <td><%#Eval("stTestName")%></td>
                                                    <td><%#Eval("stMainDeptName")%></td>
                                                    <td><%#Eval("inTestRate")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                </asp:Panel>
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </asp:Panel>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
