<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="list-users.aspx.cs" Inherits="list_users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="Scripts/facebox/facebox.css" media="screen" rel="stylesheet" type="text/css" />
    <script src="Scripts/facebox/facebox.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $('a[rel*=facebox]').facebox({
                loadingImage: 'Scripts/facebox/loading.gif',
                closeImage: 'Scripts/facebox/closelabel.png'
            })
        })
    </script>

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
            List Users
            <small></small>
          </h1>
          <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Tables</a></li>
            <li class="active">List Users</li>
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
                        <th>Address</th>
                        <th>City</th>
                        <th>Role</th>
                        <th>Status</th>
                        <th>Password</th>
                        <th>Action</th>
                      </tr>
                    </thead>
                    <tbody>
                        <tr id = "trNoData" runat = "server">
                            <td colspan = "8">
                                <center>
                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No users added." style="text-align:center;"></asp:Label>
                                </center>
                            </td>
                        </tr>
                         <asp:Repeater ID = "rptUsers" runat = "server" OnItemCommand = "rptUsers_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("stCode")%></td>
                                    <td><%#Eval("stName")%></td>
                                    <td><%#Eval("stAddress")%></td>
                                    <td><%#Eval("stCity")%></td>
                                    <td><%#Eval("stRoleName")%></td>
                                    <td><%#Eval("Status")%></td>
                                    <td>
                                    <a rel="facebox" title="Show Password" href = "userdetails.aspx?uid=<%#Eval("inUserId")%>&flag=3">
                                        <i class="fa fa-fw fa-street-view"></i> <span>View</span>
                                    </a>
                                    <asp:LinkButton ID = "lnkbtnPassword" runat = "server" ToolTip = "Email Password" CommandName = "ePassword" CommandArgument = '<%#Eval("inUserId")%>'>
                                        <i class="fa fa-fw fa-mail-forward"></i><span>Email</span>
                                      </asp:LinkButton>
                                  </td>
                                    <td>
                                        <a href = 'add-users.aspx?uid=<%#Eval("inUserId")%>' title = "edit">
                                            <i class="fa fa-edit"></i> <span>Edit</span>
                                        </a>
                                        <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                            CommandName = "eDelete" CommandArgument = '<%#Eval("inUserId")%>'>
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
        <div id="passwordbox" style="display: none;">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="right" colspan="3" class="btn-close">
                        <img src="Images/closewindow.png" title="Close window" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" valign="top" class="logintbl">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td class="heading" colspan="2">
                                    User Details
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 20px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="labels">
                                    <b>User Name</b>
                                </td>
                                <td class="labels">
                                    <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="labels">
                                    <b>User Password</b>
                                </td>
                                <td class="labels">
                                    <asp:Label ID="lblPassword" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
