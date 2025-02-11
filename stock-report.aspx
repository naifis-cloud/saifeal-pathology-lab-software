<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="stock-report.aspx.cs" Inherits="stock_report" %>

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
                <h1>Stock Report
                   <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Stock Report</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                                <div class="box-body">
                                    <div class="row">
                                        <%--<div class="col-xs-4">
                                            <div class="form-group">
                                                <label>Date range:</label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-right" id="reservation" />
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="col-xs-4">
                                            <label>Supplier</label>
                                            <asp:DropDownList ID = "ddlSupplier" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                        Text = "Fetch Report" ValidationGroup="vgItems" OnClick="btnSubmit_Click"/>
                                </div>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="box">
                            <div class="box-header">
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <asp:Panel ID = "pnlDownload" runat = "server">
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
                                </asp:Panel>
                                <asp:Panel ID = "pnlStockReport" runat = "server">
                                    <table id="example1" class="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>Item No</th>
                                                <th>Name</th>
                                                <th>Qty</th>
                                                <th>Supplier</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                           <tr id = "trNoData" runat = "server">
                                            <td colspan = "4">
                                                <center>
                                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No items found." style="text-align:center;"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID = "rptItems" runat = "server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("stCode")%></td>
                                                    <td><%#Eval("stName")%></td>
                                                    <td><%#Eval("inQuantity")%></td>
                                                    <td><%#Eval("SupplierName")%></td>
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
