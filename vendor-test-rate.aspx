<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="vendor-test-rate.aspx.cs" Inherits="vendor_test_rate" %>

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
                <h1>Add / List Vendor Test Rates
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Vendor Pathology Labs</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMainMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <div class="col-xs-6">
                            <asp:Label ID = "lblAddMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                            <asp:Panel ID = "pnlAddContent" runat = "server">
                                <div class="box box-primary">
                                    <!-- /.box-header -->
                                                                                                                                                                                                                                                                                                                                        <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <label>Vendor Lab</label>                                             
                                                <asp:DropDownList ID = "ddlVendorLabs" runat = "server" CssClass = "form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalLab"
                                                ControlToValidate="ddlVendorLabs" runat="server" ErrorMessage="Vendor Lab" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <label>Tests</label>                                             
                                                <asp:DropDownList ID = "ddlTests" runat = "server" CssClass = "form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTest"
                                                ControlToValidate="ddlTests" runat="server" ErrorMessage="Test" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <label>Rate</label>                                             
                                                <asp:TextBox ID = "txtRate" runat = "server" MaxLength = "6" placeholder="rate" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRate"
                                                ControlToValidate="txtRate" runat="server" ErrorMessage="Rate" ValidationGroup="vgVendorLabs">
                                                </asp:RequiredFieldValidator>
                                                <ajax:FilteredTextBoxExtender ID = "fltRate" runat = "server" TargetControlID = "txtRate" FilterType = "Numbers">
                                                </ajax:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                    <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                    Text = "Submit" ValidationGroup="vgVendorLabs" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgVendorLabs" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                                </div>
                            </asp:Panel>
                    </div>
                    <div class="col-xs-6">
                    <asp:Label ID = "lblViewMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                    <asp:Panel ID = "pnlViewContent" runat = "server">
                    <div class="box">
                        <div class="box-header">
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <table id="example1" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Lab</th>
                                        <th>Test</th>
                                        <th>Rate</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id = "trNoData" runat = "server">
                                        <td colspan = "4">
                                            <center>
                                                <asp:Label ID = "lblMsg" runat = "server" Text = "No rate lists added." style="text-align:center;"></asp:Label>
                                            </center>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID = "rptVendorRates" runat = "server" OnItemCommand = "rptVendorRates_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("stVendorLabName")%></td>
                                                <td><%#Eval("stTestName")%></td>
                                                <td><%#Eval("inRate")%></td>
                                                <td>
                                                    <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inVendorTestRateId")%>'>
                                                        <i class="fa fa-edit"></i> <span>Edit</span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                     CommandName = "eDelete" CommandArgument = '<%#Eval("inVendorTestRateId")%>'>
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
                    </div>
                    </asp:Panel>
                    <!-- /.box -->
                </div>
                <!-- /.col -->
                </div>
            </asp:Panel>
        <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
    <asp:HiddenField ID = "hidVendorRateId" runat = "server" />
</asp:Content>
