<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="list-collection-centre.aspx.cs" Inherits="list_collection_centre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Scripts/facebox/facebox.css" media="screen" rel="stylesheet" type="text/css" />
    <script src="Scripts/facebox/facebox.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) 
        {
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
            List Collection Centres
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
          <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
          <asp:Panel ID = "pnlMainContent" runat = "server">
           <div class="row">
            <div class="col-xs-12">
              <div class="box">
                <div class="box-header">
                </div>
                <div class="box-body">
                  <table id="example1" class="table table-bordered table-striped">
                    <thead>
                      <tr>
                        <th>Code</th>
                        <th>Name</th>
                        <th>Address</th>
                        <th>City</th>
                        <th>Contact Person</th>
                          <th>Status</th>
                          <th>Password</th>
                          <th>Action</th>
                      </tr>
                    </thead>
                    <tbody>
                        <tr id = "trNoData" runat = "server">
                            <td colspan = "8">
                                <center>
                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No Collection Centers added." style="text-align:center;"></asp:Label>
                                </center>
                            </td>
                        </tr>
                        <asp:Repeater ID = "rptCollCenters" runat = "server" OnItemCommand = "rptCollCenters_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("stCenterCode")%></td>
                                <td><%#Eval("stCenterName")%></td>
                                <td><%#Eval("stCenterAddress")%></td>
                                <td><%#Eval("stCity")%></td>
                                <td><%#Eval("stDoctorName")%></td>
                                <td><%#Eval("Status")%></td>
                                  <td>
                                    <a rel="facebox" title="Show Password" href = "userdetails.aspx?uid=<%#Eval("inColCenterId")%>&flag=0">
                                        <i class="fa fa-fw fa-street-view"></i> <span>View</span>
                                    </a>
                                    <%--<a href="#" title="delete"><i class="fa fa-fw fa-mail-forward"></i><span>Email</span></a>--%>
                                    <asp:LinkButton ID = "lnkbtnPassword" runat = "server" ToolTip = "Email Password" CommandName = "ePassword" CommandArgument = '<%#Eval("inColCenterId")%>'>
                                        <i class="fa fa-fw fa-mail-forward"></i><span>Email</span>
                                      </asp:LinkButton>
                                  </td>
                                  <td>
                                      <a href="add-collection-centre.aspx?ccid=<%#Eval("inColCenterId")%>" title="edit"><i class="fa fa-edit"></i> <span>Edit</span></a>
                                      <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                       CommandName = "eDelete" CommandArgument = '<%#Eval("inColCenterId")%>'>
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
