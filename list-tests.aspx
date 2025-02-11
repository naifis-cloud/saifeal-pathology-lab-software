<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="list-tests.aspx.cs" Inherits="list_tests" %>

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
            List Tests
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
                        <th>ICD 10</th>
                        <th>Type</th>
                        <%--<th>Rank</th>--%>
                        <th>Method</th>
                        <th>Range</th>
                        <th>Parameter</th>
                        <th nowrap = "nowrap">Action</th>
                        <th>Description</th>
                        <th>Action</th>
                      </tr>
                    </thead>
                    <tbody>
                        <tr id = "trNoData" runat = "server">
                            <td colspan = "9">
                                <center>
                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No tests added." style="text-align:center;"></asp:Label>
                                </center>
                            </td>
                        </tr>
                        <asp:Repeater ID = "rptTests" runat = "server" OnItemDataBound="rptTests_ItemDataBound"
                        OnItemCommand = "rptTests_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("stTestCode")%></td>
                                    <td><%#Eval("stTestName")%></td>
                                    <td><%#Eval("stICD10Code")%></td>
                                    <td><asp:Label ID = "lblType" runat = "server" Text = '<%#Eval("stType")%>'></asp:Label></td>
                                    <%--<td><%#Eval("inOrder")%></td>--%>
                                    <td><%#Eval("stTestMethod")%></td>
                                    <td><%#Eval("stRange")%></td>
                                    <td>
                                        <asp:Label ID = "lblParamType" runat = "server" Text = '<%#Eval("stParams")%>'></asp:Label>
                                    </td>
                                    <td nowrap = "nowrap">
                                        <a href="list-param-for-test.aspx?tid=<%#Eval("inTestId")%>" title="Show Parameters"><i class="fa fa-edit"></i> <span>Parameters</span></a>
                                    </td>
                                    <td nowrap = "nowrap">
                                        <a id = "anchDesc" runat = "server" title="Add Description" visible = "false"><i class="fa fa-edit"></i> <span>Add Description</span></a>
                                        <asp:Label ID = "lblTestId" runat = "server" Text = '<%#Eval("inTestId")%>' Visible = "false"></asp:Label>
                                    </td>
                                    <td>
                                        <a href="add-test.aspx?tid=<%#Eval("inTestId")%>" title="edit"><i class="fa fa-edit"></i> <span>Edit</span></a>
                                        <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                        CommandName = "eDelete" CommandArgument = '<%#Eval("inTestId")%>'>
                                        <i class="fa fa-fw fa-trash-o"></i><span>Delete</span>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
            </asp:Panel>
        </section>
    </div>
</asp:Content>
