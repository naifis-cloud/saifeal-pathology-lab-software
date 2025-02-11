<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-list-items.aspx.cs" Inherits="add_list_items" %>

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
                <h1>Add / List Items
            <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">List Items</li>
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
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "20" placeholder="Item code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Item code" ValidationGroup="vgItems"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-9">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Item Name</label>
                                                <asp:TextBox ID = "txtName" runat = "server" MaxLength = "50" placeholder="Item Name" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                                ControlToValidate="txtName" runat="server" ErrorMessage="Item Name" ValidationGroup="vgItems"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Decription</label>
                                        <asp:TextBox ID = "txtAddress" runat = "server" MaxLength = "200" placeholder="Decription" CssClass = "form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAddress"
                                         ControlToValidate="txtAddress" runat="server" ErrorMessage="Decription" ValidationGroup="vgItems"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <label>Supplier</label>
                                            <asp:DropDownList ID = "ddlSupplier" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalSupplier"
                                            ControlToValidate="ddlSupplier" runat="server" ErrorMessage="Supplier" ValidationGroup="vgItems"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Category</label>
                                            <asp:TextBox ID = "txtCategory" runat = "server" MaxLength = "50" placeholder="Category" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalPinCode"
                                            ControlToValidate="txtCategory" runat="server" ErrorMessage="Category" ValidationGroup="vgItems"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Brand</label>
                                            <asp:TextBox ID = "txtBrand" runat = "server" MaxLength = "50" placeholder="Brand" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalBrand"
                                            ControlToValidate="txtBrand" runat="server" ErrorMessage="Brand" ValidationGroup="vgItems"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>                  
                                    <br />     
                                     <div class="row">
                                        <div class="col-xs-4">
                                            <label>Quantity</label>
                                            <asp:TextBox ID = "txtQuantity" runat = "server" MaxLength = "50" placeholder="Quantity" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalQty"
                                            ControlToValidate="txtQuantity" runat="server" ErrorMessage="Quantity" ValidationGroup="vgItems"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltLower" runat = "server" TargetControlID = "txtQuantity" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Status</label>
                                            <div class="checkbox">
                                                <label>
                                                    <asp:RadioButton ID = "rbtnPerish" runat = "server" GroupName = "perish" Checked = "true" />
                                                    Perishable                 
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID = "rbtnNonPerish" runat = "server" GroupName = "perish"/>
                                                    Non Perishable                 
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                        Text = "Submit" ValidationGroup="vgItems" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgItems" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
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
                                            <th>Item</th>
                                            <th>Supplier</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    <tr id = "trNoData" runat = "server">
                                        <td colspan = "4">
                                            <center>
                                                <asp:Label ID = "lblMsg" runat = "server" Text = "No items added." style="text-align:center;"></asp:Label>
                                            </center>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID = "rptItems" runat = "server" OnItemCommand = "rptItems_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("stCode")%></td>
                                                <td><%#Eval("stName")%></td>
                                                <td><%#Eval("SupplierName")%></td>
                                                <td>
                                                    <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inItemId")%>'>
                                                        <i class="fa fa-edit"></i> <span>Edit</span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                     CommandName = "eDelete" CommandArgument = '<%#Eval("inItemId")%>'>
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
    <asp:HiddenField ID="hidItemId" runat="server" />
</asp:Content>
