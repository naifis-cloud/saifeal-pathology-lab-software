<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-list-sub-departments.aspx.cs" Inherits="add_list_sub_departments" %>

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
          <h1>
           Add / List Sub Departments
            <small></small>
          </h1>
          <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Tables</a></li>
            <li class="active">Collection Centres</li>
          </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMainMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                  <div class="col-xs-5">
                    <asp:Label ID = "lblAddMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                    <asp:Panel ID = "pnlAddContent" runat = "server">
                                                                                                                                                                                                                                      <div class="box box-primary">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="form-group">
                                    <label>Main Department</label>
                                    <asp:DropDownList ID = "ddlMainDepts" runat = "server" CssClass = "form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMainDepts"
                                    ControlToValidate="ddlMainDepts" runat="server" ErrorMessage="Main Department" ValidationGroup="vgSubDepts"></asp:RequiredFieldValidator>
                                </div>
                            <div class="row">
                                <div class="col-xs-3">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Code</label>
                                        <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "200" placeholder="Enter code" CssClass = "form-control"
                                        Enabled = "false"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                        ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgSubDepts"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xs-9">
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Sub Department Name</label>
                                        <asp:TextBox ID = "txtName" runat = "server" MaxLength = "200" placeholder="Name" CssClass = "form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDeptName"
                                        ControlToValidate="txtName" runat="server" ErrorMessage="Sub Department Name" ValidationGroup="vgSubDepts"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Sort Order</label>
                                <asp:TextBox ID = "txtSortOrder" runat = "server" MaxLength = "200" placeholder="sort order no" CssClass = "form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalSortOrder"
                                ControlToValidate="txtSortOrder" runat="server" ErrorMessage="Sort order no" ValidationGroup="vgSubDepts"></asp:RequiredFieldValidator>
                                <ajax:FilteredTextBoxExtender ID = "fltSortOrder" runat = "server" TargetControlID = "txtSortOrder" FilterType = "Numbers">
                                </ajax:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group">
                                <label>Remarks</label>
                                <asp:TextBox ID = "txtRemarks" runat = "server" MaxLength = "200" placeholder="Remarks..." CssClass = "form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="RequiredFieldValidator1"
                                ControlToValidate="txtRemarks" runat="server" ErrorMessage="Remarks..." ValidationGroup="vgSubDepts"></asp:RequiredFieldValidator>
                            </div>
                                   
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                            Text = "Submit" ValidationGroup="vgSubDepts" OnClick="btnSubmit_Click"/>
                            <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                            ValidationGroup="vgSubDepts" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                        </div>
                    </div>
                        </asp:Panel>
                    </div>
                <div class="col-xs-7">
                 <asp:Label ID = "lblViewMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                    <asp:Panel ID = "pnlViewContent" runat = "server">
                        <div class="box">
                            <div class="box-header"></div>
                           <div class="box-body">
                          <table id="example1" class="table table-bordered table-striped">
                            <thead>
                              <tr>
                                <th>Code</th>
                                <th>Name</th>
                                <th>Main Department</th>
                                <th>Sort Order</th>
                                <th>Action</th>
                              </tr>
                            </thead>
                            <tbody>
                                 <tr id = "trNoData" runat = "server">
                                    <td colspan = "4">
                                        <center>
                                            <asp:Label ID = "lblMsg" runat = "server" Text = "No Sub Departments added." style="text-align:center;"></asp:Label>
                                        </center>
                                    </td>
                                </tr>
                                <asp:Repeater ID = "rptSubDepts" runat = "server" OnItemCommand = "rptSubDepts_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("stSubDeptCode")%></td>
                                        <td><%#Eval("stSubDeptName")%></td>
                                        <td><%#Eval("stMainDeptName")%></td>
                                        <td><%#Eval("inSortOrder")%></td>
                                        <td>
                                            <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inSubDeptId")%>'>
                                                <i class="fa fa-edit"></i> <span>Edit</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                CommandName = "eDelete" CommandArgument = '<%#Eval("inSubDeptId")%>'>
                                                <i class="fa fa-fw fa-trash-o"></i><span>Delete</span>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </tbody>
                          </table>
                </div>
                        </div><!-- /.box -->
                    </asp:Panel>
                </div><!-- /.col -->
              </div><!-- /.row -->
        </asp:Panel>
        </section>
        <!-- /.content -->
    </div>
    <asp:HiddenField ID="hidSubDeptId" runat="server" />
</asp:Content>
