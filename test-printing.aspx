﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="test-printing.aspx.cs" Inherits="test_printing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <h1>Test Printing
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Test Printing</li>
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
                                <table cellpadding = "2" cellspacing = "5">
                                    <tr>
                                        <%--<td><button class="btn btn-block btn-info">Partial Printed</button></td>
                                        <td><button class="btn btn-block btn-danger">Fully Printed</button></td>--%>
                                        <td><asp:Button ID="btnPrint" runat="server" CssClass="btn btn-block btn-warning" 
                                                Text="Second Time Printed" OnClick="btnPrint_Click"/>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnViewAll" runat="server" CssClass="btn btn-block btn-warning" 
                                                Text="View All" OnClick="btnViewAll_Click"/>
                                        </td>
                                    </tr>
                                </table>
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Reg No</th>
                                            <th>Patient Name</th>
                                            <th>Sex</th>
                                            <th>Age</th>
                                            <th>Ref Doc</th>
                                            <th>Test Name</th>
                                            <th>Test Status</th>
                                            <th>Balance</th>
                                            <th>Total</th>
                                            <th>Report</th>
                                            <th>Doctor</th>
                                            <th>Patient</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id = "trNoData" runat = "server">
                                            <td colspan = "11">
                                                <center>
                                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No data found!" style="text-align:center;"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID = "rptPatients" runat = "server" OnItemCommand="rptPatients_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("stRegNo")%></td>
                                                    <td><%#Eval("stPatientName")%></td>
                                                    <td><%#Eval("stGender")%></td>
                                                    <td><%#Eval("inAge")%></td>
                                                    <td><%#Eval("RefDoctorName")%></td>
                                                    <td><%#Eval("TestNames")%></td>
                                                    <td><%#Eval("TestStatus")%></td>
                                                    <td><%#Eval("inBalanceAmt")%></td>
                                                    <td><%#Eval("inNetAmt")%></td>
                                                    <td>
                                                        <a href="test-report.aspx?pid=<%#Eval("inPatientId")%>" target = "_blank" title="Print" style="<%#ShowHideReportLink(Convert.ToInt32(Eval("inTestStatusId")))%>">
                                                        <i class="fa fa-edit"></i><span>Print</span></a>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID = "lnkbtnDocEmail" runat = "server" CommandName = "eEmailDoc" CommandArgument = '<%#Eval("inPatientId")%>'
                                                        Visible = '<%#ShowHideEmailLink(Convert.ToInt32(Eval("inTestStatusId")))%>'>
                                                            <i class="fa fa-edit"></i><span>Email</span>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID = "lnkbtnPatientEmail" runat = "server" CommandName = "eEmailPatient" CommandArgument = '<%#Eval("inPatientId")%>'
                                                        Visible = '<%#ShowHideEmailLink(Convert.ToInt32(Eval("inTestStatusId")))%>'>
                                                            <i class="fa fa-edit"></i><span>Email</span>
                                                        </asp:LinkButton>
                                                        <%--<a href="#" title="edit" style="<%#ShowHideValuesLink(Convert.ToInt32(Eval("inTestStatusId")))%>"><i class="fa fa-edit"></i><span>SMS</span></a>--%>
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
