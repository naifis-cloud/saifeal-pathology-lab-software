<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="list-param-for-test.aspx.cs" Inherits="list_param_for_test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Parameters for Test - <asp:Label ID = "lblTestName" runat = "server"></asp:Label>
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Collection Centres</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box">
                            <%--<div class="box-header with-border">
                                <h3 class="box-title">Test Name: AEC</h3>
                            </div>--%>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-xs-2">
                                        <asp:Button ID = "btnAddParam" runat = "server" Text = "Add New Parameter" CssClass = "btn btn-primary"/>
                                    </div>
                                    <%--<div class="col-xs-3">
                                        <button class="btn btn-block btn-primary btn-sm">Add Group Head</button>
                                    </div>
                                    <div class="col-xs-3">
                                        <button class="btn btn-block btn-primary btn-sm">Leave A line</button>
                                    </div>--%>
                                </div>
                                <br />
                            <table id="example1" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Type</th>
                                        <th>Code</th>
                                        <th>Name</th>
                                        <th>Order</th>
                                        <th>Method</th>
                                        <th>Machine</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id = "trNoData" runat = "server">
                                        <td colspan = "7">
                                            <center>
                                                <asp:Label ID = "lblMsg" runat = "server" Text = "No parameters added." style="text-align:center;"></asp:Label>
                                            </center>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID = "rptParameters" runat = "server" OnItemCommand = "rptParameters_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("stType")%></td>
                                                <td><%#Eval("stCode")%></td>
                                                <td><%#Eval("stName")%></td>
                                                <td><%#Eval("inOrderNo")%></td>
                                                <td><%#Eval("stMethod")%></td>
                                                <td><%#Eval("stMachineName")%></td>
                                                <td>
                                                    <a href = 'add-param.aspx?tid=<%#Eval("inTestId")%>&pid=<%#Eval("inParamId")%>' title = "edit">
                                                        <i class="fa fa-edit"></i> <span>Edit</span>
                                                    </a>
                                                    <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                        CommandName = "eDelete" CommandArgument = '<%#Eval("inParamId")%>'>
                                                        <i class="fa fa-fw fa-trash-o"></i><span>Delete</span>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.box-body -->
                        <%--<div class="box-footer clearfix">
                            <a>Exit</a>
                        </div>--%>
                    </div>
                    <!-- /.box -->

                    <!-- /.box -->
                </div>
            </div>
            </asp:Panel>
        <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
