<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="list-profiles.aspx.cs" Inherits="list_profiles" %>

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
          <h1>
            List Profiles
            <small></small>
          </h1>
          <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Tables</a></li>
            <li class="active">Profiles</li>
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
                </div><!-- /.box-header -->
                <div class="box-body">
                  <table id="example1" class="table table-bordered table-striped">
                    <thead>
                      <tr>
                        <th>Code</th>
                        <th>Name</th>
                        <th>Method</th>
                        <th>Department</th>
                        <th>Action</th>
                      </tr>
                    </thead>
                    <tbody>
                        <tr id = "trNoData" runat = "server">
                            <td colspan = "5">
                                <center>
                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No profiles added." style="text-align:center;"></asp:Label>
                                </center>
                            </td>
                        </tr>
                         <asp:Repeater ID = "rptProfiles" runat = "server" OnItemCommand = "rptProfiles_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("stProfileCode")%></td>
                                    <td><%#Eval("stProfileName")%></td>
                                    <td><%#Eval("stProfileMethod")%></td>
                                    <td><%#Eval("stMainDeptName")%></td>
                                    <td>
                                        <a href = 'add-profile.aspx?prfid=<%#Eval("inProfileId")%>' title = "edit">
                                            <i class="fa fa-edit"></i> <span>Edit</span>
                                        </a>
                                        <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                            CommandName = "eDelete" CommandArgument = '<%#Eval("inProfileId")%>'>
                                            <i class="fa fa-fw fa-trash-o"></i><span>Delete</span>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                  </table>
                </div><!-- /.box-body -->
              </div><!-- /.box -->
            </div><!-- /.col -->
          </div><!-- /.row -->
            </asp:Panel>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
