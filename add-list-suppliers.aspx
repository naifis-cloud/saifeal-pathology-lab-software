<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-list-suppliers.aspx.cs" Inherits="add_list_suppliers" %>

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
                <h1>Add / List Suppliers
            <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">List Suppliers</li>
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
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Code</label>
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "20" placeholder="Enter code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Supplier code" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-9">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Supplier Name</label>
                                                <asp:TextBox ID = "txtName" runat = "server" MaxLength = "50" placeholder="Supplier name" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                                ControlToValidate="txtName" runat="server" ErrorMessage="Supplier Name" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Address</label>
                                        <asp:TextBox ID = "txtAddress" runat = "server" MaxLength = "200" placeholder="Address" CssClass = "form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAddress"
                                         ControlToValidate="txtAddress" runat="server" ErrorMessage="Address" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="row">
                                        <asp:UpdatePanel ID = "updStateCity" runat = "server">
                                            <ContentTemplate>
                                                <div class="col-xs-4">
                                                    <label>State</label>&nbsp;&nbsp;&nbsp;
                                                    <img id = "imgloading" runat = "server" title="loading..." src="~/images/ajax-loader.gif" style="display:none;" />
                                                    <asp:DropDownList ID = "ddlState" runat = "server" CssClass = "form-control" AutoPostBack = "true"
                                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalState"
                                                    ControlToValidate="ddlState" runat="server" ErrorMessage="State" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-xs-4">
                                                    <label>City</label>
                                                    <asp:DropDownList ID = "ddlCity" runat = "server" CssClass = "form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCity"
                                                    ControlToValidate="ddlCity" runat="server" ErrorMessage="City" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                        <div class="col-xs-4">
                                            <label>Pincode</label>
                                            <asp:TextBox ID = "txtPinCode" runat = "server" MaxLength = "10" placeholder="pin code" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalPinCode"
                                            ControlToValidate="txtPinCode" runat="server" ErrorMessage="Pincode" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Email</label>
                                            <asp:TextBox ID = "txtEmail" runat = "server" MaxLength = "50" placeholder="email id" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalEmail"
                                            ControlToValidate="txtEmail" runat="server" ErrorMessage="Email Address" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="regexpEmail"
                                            runat="server" ControlToValidate="txtEmail" ErrorMessage="Valid Email Address"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgSuppliers"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Mobile</label>
                                            <asp:TextBox ID = "txtMobile" runat = "server" MaxLength = "20" placeholder="mobile" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMobile"
                                            ControlToValidate="txtMobile" runat="server" ErrorMessage="mobile" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Phone</label>
                                            <asp:TextBox ID = "txtPhone" runat = "server" MaxLength = "20" placeholder="phone" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalphone"
                                            ControlToValidate="txtPhone" runat="server" ErrorMessage="phone" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>CST</label>
                                            <asp:TextBox ID = "txtCSTNo" runat = "server" MaxLength = "30" placeholder="CST" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCSTNo"
                                            ControlToValidate="txtCSTNo" runat="server" ErrorMessage="CST" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Tin</label>
                                            <asp:TextBox ID = "txtTIN" runat = "server" MaxLength = "30" placeholder="TIN" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTIN"
                                            ControlToValidate="txtTIN" runat="server" ErrorMessage="TIN" ValidationGroup="vgSuppliers"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xs-6">
                                            <label>Supplier Type</label>
                                            <asp:DropDownList ID = "ddlSupplierType" runat = "server" CssClass = "form-control">
                                                <asp:ListItem Text = "Local" Value = "Local"></asp:ListItem> 
                                                <asp:ListItem Text = "InterState" Value = "InterState"></asp:ListItem> 
                                                <asp:ListItem Text = "International" Value = "International"></asp:ListItem> 
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Payment Mode</label>
                                            <asp:DropDownList ID = "ddlPaymentMode" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xs-6">
                                            <label>Status</label>
                                            <div class="checkbox">
                                                <label>
                                                    <asp:RadioButton ID = "rbtnActive" runat = "server" GroupName = "status" Checked = "true" />
                                                    Active                  
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID = "rbtnInactive" runat = "server" GroupName = "status" />
                                                    In Active                  
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                        Text = "Submit" ValidationGroup="vgSuppliers" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgSuppliers" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
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
                                            <th>Supplier</th>
                                            <th>City</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    <tr id = "trNoData" runat = "server">
                                        <td colspan = "4">
                                            <center>
                                                <asp:Label ID = "lblMsg" runat = "server" Text = "No suppliers added." style="text-align:center;"></asp:Label>
                                            </center>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID = "rptSuppliers" runat = "server" OnItemCommand = "rptSuppliers_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("stCode")%></td>
                                                <td><%#Eval("stName")%></td>
                                                <td><%#Eval("stCity")%></td>
                                                <td>
                                                    <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inSupplierId")%>'>
                                                        <i class="fa fa-edit"></i> <span>Edit</span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                     CommandName = "eDelete" CommandArgument = '<%#Eval("inSupplierId")%>'>
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
                        <!-- /.box -->
                        </asp:Panel>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </asp:Panel>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
    <asp:HiddenField ID="hidSupplierId" runat="server" />
</asp:Content>
