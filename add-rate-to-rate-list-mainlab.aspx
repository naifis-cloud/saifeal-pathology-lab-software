<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-rate-to-rate-list-mainlab.aspx.cs" Inherits="add_rate_to_rate_list_mainlab" %>

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
                <h1>Add Rate to Rate List Main Lab
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Add Rate to Rate List Main Lab</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMainMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Rate Lists</label>
                                            <asp:DropDownList ID = "ddlRateList" runat = "server" CssClass = "form-control" AutoPostBack = "true"
                                                OnSelectedIndexChanged="ddlRateList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRateList"
                                             ControlToValidate="ddlRateList" runat="server" ErrorMessage="Select Rate List" ValidationGroup="vgAssignRates"></asp:RequiredFieldValidator>    
                                        </div>
                                    </div>
                                </div>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="box">
                            <div class="box-body">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID = "btnAssignRates" runat = "server" CssClass = "btn btn-primary" 
                                            Text = "Assign Rates" ValidationGroup="vgAssignRates" OnClick="btnAssignRates_Click"/>
                                            <asp:ValidationSummary ID="valColCenter" runat="server" 
                                            ValidationGroup="vgAssignRates" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Test No</th>
                                            <th>Test Name</th>
                                            <th>Department</th>
                                            <th>Rate</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id = "trNoData" runat = "server">
                                            <td colspan = "4">
                                                <center>
                                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No tests found." style="text-align:center;"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID = "rptTests" runat = "server" OnItemDataBound="rptTests_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <asp:Label ID = "lblTestId" runat = "server" Text = '<%#Eval("inTestId")%>' Visible = "false"></asp:Label>
                                                    <td><%#Eval("stTestCode")%></td>
                                                    <td>
                                                        <asp:Label ID = "lblTestName" runat = "server" Text = '<%#Eval("stTestName")%>'></asp:Label>
                                                    </td>
                                                    <td><%#Eval("stMainDeptName")%></td>
                                                    <td>
                                                        <asp:TextBox ID = "txtRate" runat = "server" MaxLength = "5" placeholder="Enter Rate" 
                                                        CssClass = "form-control" Text = '<%#Eval("inRate")%>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRate" runat="server"></asp:RequiredFieldValidator>
                                                        <ajax:FilteredTextBoxExtender ID = "fltRate" runat = "server" TargetControlID = "txtRate" FilterType = "Numbers">
                                                        </ajax:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
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
    <!-- /.content-wrapper -->
    <asp:HiddenField ID = "hidRecords" runat = "server" />
</asp:Content>
