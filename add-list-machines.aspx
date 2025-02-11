<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-list-machines.aspx.cs" Inherits="add_list_machines" %>

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
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add / List Machines
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Machines</li>
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
                            <!-- form start -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label>Code</label>                                             
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "20" placeholder="code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Machine</label>
                                            <asp:TextBox ID = "txtMachineName" runat = "server" MaxLength = "50" placeholder="machine name" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                            ControlToValidate="txtMachineName" runat="server" ErrorMessage="Machine Name" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Description</label>
                                            <asp:TextBox ID = "txtDescription" runat = "server" MaxLength = "200" placeholder="Description" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDescription"
                                            ControlToValidate="txtDescription" runat="server" ErrorMessage="Description" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                            Text = "Submit" ValidationGroup="vgColCenter" OnClick="btnSubmit_Click"/>
                            <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgColCenter" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
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
                                        <th>Code</th>
                                        <th>Machine</th>
                                        <th>Description</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id = "trNoData" runat = "server">
                                        <td colspan = "4">
                                            <center>
                                                <asp:Label ID = "lblMsg" runat = "server" Text = "No machines added." style="text-align:center;"></asp:Label>
                                            </center>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID = "rptMachines" runat = "server" OnItemCommand = "rptMachines_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("stMachineCode")%></td>
                                                <td><%#Eval("stMachineName")%></td>
                                                <td><%#Eval("stMachineDesc")%></td>
                                                <td>
                                                    <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inMachineId")%>'>
                                                        <i class="fa fa-edit"></i> <span>Edit</span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                     CommandName = "eDelete" CommandArgument = '<%#Eval("inMachineId")%>'>
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
    <asp:HiddenField ID="hidMachineId" runat="server" />
</asp:Content>
