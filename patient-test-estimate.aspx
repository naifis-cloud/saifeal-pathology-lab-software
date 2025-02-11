<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="patient-test-estimate.aspx.cs" Inherits="patient_test_estimate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Content Wrapper. Contains page content -->
        <div class="content-wrapper">
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <h1>Add New Patient Estimate           
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Masters</a></li>
                    <li class="active">Add Test</li>
                </ol>
            </section>

            <!-- Main content -->
            <section class="content">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- general form elements -->
                        <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                            <form role="form">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Type</label>
                                                <input type="code" class="form-control" id="Code1" placeholder="OPD / IPD">
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Mobile</label>
                                                <input type="name" class="form-control" id="Name1" placeholder="Mobile">
                                                <i class="fa fa-fw fa-refresh"></i>
                                            </div>
                                        </div>
                                        <div class="col-xs-1">
                                            <label>Title</label>
                                            <select class="form-control">
                                                <option>Mr</option>
                                                <option>Ms</option>
                                                <option>Baby</option>
                                                <option>Boy</option>
                                            </select>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Patient Name</label>
                                                <input type="code" class="form-control" id="Code2" placeholder="Patient Name">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <label>Gender</label>
                                            <select class="form-control">
                                                <option>Male</option>
                                                <option>Female</option>
                                                <option>Baby</option>
                                                <option>Boy</option>
                                                <option>Infant</option>
                                            </select>
                                        </div>
                                        <div class="col-xs-1">
                                            <label>Age</label>
                                            <input type="text" class="form-control" placeholder="Age">
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Mobile</label>
                                            <input type="text" class="form-control" placeholder="Mobile">
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Sample Time</label>
                                            <input type="text" class="form-control" placeholder="">
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Ref by Partner</label>
                                            <select class="form-control">
                                                <option>Partner 1</option>
                                                <option>Partner 2</option>
                                                <option>Partner 3</option>
                                                <option>Partner 4</option>
                                                <option>Partner 5</option>
                                            </select>

                                        </div>
                                        <div class="col-xs-2">
                                            <label>&nbsp;</label>
                                            <input type="text" class="form-control" placeholder="">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Address</label>
                                            <input type="text" class="form-control" placeholder="">
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Clinical History</label>
                                            <input type="text" class="form-control" placeholder="">
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Remarks</label>
                                            <input type="text" class="form-control" placeholder="">
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Ref by Doctor</label>
                                            <select class="form-control">
                                                <option>Doctor 1</option>
                                                <option>Doctor 2</option>
                                                <option>Doctor 3</option>
                                                <option>Doctor 4</option>
                                                <option>Doctor 5</option>
                                            </select>

                                        </div>
                                        <div class="col-xs-2">
                                            <label>&nbsp;</label>
                                            <input type="text" class="form-control" placeholder="">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Select Department</label>
                                            <select class="form-control">
                                                <option>Haematology</option>
                                                <option>Microbiology</option>
                                                <option>Biochemistry</option>
                                              <option>Cysto</option>

                                            </select>

                                        </div>
                                    </div>
                                    <div class="clearfix">
                                        &nbsp;<br />
                                    </div>
                                    <div class="row">

                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <div class="col-xs-3">
                                                    <button class="btn btn-block btn-primary btn-sm">Add Test</button>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <label>Lists Tests</label>
                                            <select multiple class="form-control">
                                                <option>option 1</option>
                                                <option>option 2</option>
                                                <option>option 3</option>
                                                <option>option 4</option>
                                                <option>option 5</option>
                                            </select>
                                        </div>

                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <div class="col-xs-3">
                                                    <button class="btn btn-block btn-primary btn-sm">Remove Test</button>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <label>Selected Tests</label>
                                            <select multiple class="form-control">
                                                <option>option 1</option>
                                                <option>option 2</option>
                                                <option>option 3</option>
                                                <option>option 4</option>
                                                <option>option 5</option>
                                            </select>
                                       </div>
                                    </div>
                                    <div class="clearfix">
                                        &nbsp;<br /></div>
                                    
                                   <div class="clearfix">
                                        &nbsp;<br /></div>
                                        <div class="row">
                                            
                                            <div class="col-xs-2">
                                                <div class="form-group">
                                                    <label for="exampleInputPassword1">Total Tests</label>
                                                    <input type="name" class="form-control" id="Name4" placeholder="Total Tests">
                                                </div>
                                            </div>
                                            <div class="col-xs-2">
                                                    <div class="form-group">
                                                        <label for="exampleInputPassword1">Net Amt</label>
                                                        <input type="name" class="form-control" id="Name2" placeholder="Net Amount" disabled>
                                                    </div>
                                                    </div>
                                        </div>
                                        
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <button type="submit" class="btn btn-primary">Submit</button>
                                    </div>
                            </form>
                        </div>
                        <!-- /.box -->



                    </div>
                    <!--/.col (left) -->
                    <!-- right column -->
                    <!--/.col (right) -->
                </div>
                <!-- /.row -->
            </section>
            <!-- /.content -->
        </div>
        <!-- /.content-wrapper -->
</asp:Content>

