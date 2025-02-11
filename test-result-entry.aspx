<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="test-result-entry.aspx.cs" Inherits="test_result_entry" %>

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

    <script language="javascript" type="text/javascript">
        function clearDateValue(objDateId) {
            document.getElementById(objDateId).value = "";
        }

        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10001;
        }
        function onCalendarHidden(sender, args) {
            sender._popupBehavior._element.style.zIndex = 0;
        }
    
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
                <h1>Test Result Entry
           
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Test Result Entry</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label>Date From:&nbsp;&nbsp;
                                                <a href="javascript:void(0);" id = "anchClearFromDt" title="edit" style="font-weight:normal;" 
                                                 onclick = "clearDateValue('ctl00_ContentPlaceHolder1_txtFromdate');"><span>Clear</span></a>
                                                </label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <img id = "imgFromDate" src = "images/date.png" title = "Select date" />
                                                    </div>
                                                    <asp:TextBox ID = "txtFromdate" CssClass = "form-control pull-right" runat = "server" MaxLength = "10" 
                                                        ReadOnly = "true"></asp:TextBox>
                                                    <ajax:CalendarExtender ID = "calExtFromDate" runat = "server" TargetControlID = "txtFromdate"
                                                    PopupButtonID = "imgFromDate" Format = "dd-MM-yyyy" Animated = "true"
                                                    OnClientShown="onCalendarShown"  OnClientHidden="onCalendarHidden"></ajax:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label>Date To:
                                                &nbsp;&nbsp;
                                                <a href="javascript:void(0);" id = "anchClearToDt" title="edit" style="font-weight:normal;"
                                                onclick = "clearDateValue('ctl00_ContentPlaceHolder1_txtTodate');"><span>Clear</span></a>
                                                </label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <img id = "imgToDate" src = "images/date.png" title = "Select date" />
                                                    </div>
                                                    <asp:TextBox ID = "txtTodate" CssClass = "form-control pull-right" runat = "server" MaxLength = "10"
                                                        ReadOnly = "true"></asp:TextBox>
                                                    <ajax:CalendarExtender ID = "calExtTodate" runat = "server" TargetControlID = "txtTodate"
                                                    PopupButtonID = "imgToDate" Format = "dd-MM-yyyy" Animated = "true" OnClientShown="onCalendarShown"  
                                                    OnClientHidden="onCalendarHidden"></ajax:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Department</label>
                                            <asp:DropDownList ID = "ddlDepts" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Test Status</label>
                                            <asp:DropDownList ID = "ddlTestStatus" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Patient Name</label>
                                            <asp:TextBox ID = "txtPatientName" placeholder="patient name" CssClass = "form-control" runat = "server" MaxLength = "50"></asp:TextBox>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Reg no</label>
                                            <asp:TextBox ID = "txtRegNo" placeholder="Reg no" CssClass = "form-control" runat = "server" MaxLength = "20"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" Text = "Submit" OnClick="btnSubmit_Click"/>
                                </div>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="box">
                            <div class="box-header">
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <%--<table>
                                    <tr><td><button class="btn btn-block btn-info">In Process</button></td>
                                        <td><button class="btn btn-block btn-danger">Partial Tested</button></td>
                                        <td><button class="btn btn-block btn-warning">Fully Tested</button></td></tr>
                                </table>--%>
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Reg No</th>
                                            <th>Patient Name</th>
                                            <th>Sex</th>
                                            <th>Age</th>
                                            <th>Test Name</th>
                                            <th>Test Status</th>
                                            <th>VialID</th>
                                            <th>Enter Values</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id = "trNoData" runat = "server">
                                            <td colspan = "9">
                                                <center>
                                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No data found!" style="text-align:center;"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID = "rptPatients" runat = "server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("stRegNo")%></td>
                                                    <td>
                                                        <asp:Label ID = "lblPatientName" runat = "server" Text = '<%#Eval("stPatientName")%>'></asp:Label>
                                                        <asp:Label ID = "lblTestStatusId" runat = "server" Text = '<%#Eval("inTestStatusId")%>' Visible = "false"></asp:Label>
                                                    </td>
                                                    <td><%#Eval("stGender")%></td>
                                                    <td><%#Eval("inAge")%></td>
                                                    <td><%#Eval("TestNames")%></td>
                                                    <td>
                                                        <span class="<%#GetTestStatusCSS(Convert.ToInt32(Eval("inTestStatusId")))%>"><%#Eval("TestStatus")%></span>
                                                        <%--<span class="label label-warning">Patient Registered</span>--%>
                                                    </td>
                                                    <td><%#Eval("stVialID")%></td>
                                                    <td>
                                                        <a rel="facebox" href = "patient-test-params.aspx?pid=<%#Eval("inPatientId")%>&pname=<%#Eval("stPatientName")%>&flag=0" 
                                                        title="edit" style="<%#ShowHideValuesLink(Convert.ToInt32(Eval("inTestStatusId")))%>"><i class="fa fa-edit"></i><span>Enter</span></a>
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
                <!-- /.row -->
            </asp:Panel>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
