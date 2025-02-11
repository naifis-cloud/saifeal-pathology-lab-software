<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="list-test-estimate.aspx.cs" Inherits="list_test_estimate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content Wrapper. Contains page content -->
        <div class="content-wrapper">
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <h1>List All Tests
           
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">All Tests</li>
                </ol>
            </section>

            <!-- Main content -->
            <section class="content">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                            <form role="form">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label>Date range:</label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-right" id="reservation" />
                                                </div>
                                                <!-- /.input group -->
                                            </div>
                                            <!-- /.form group -->
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Department</label>
                                            <select class="form-control">
                                                <option>Select</option>
                                                <option>option 1</option>
                                                <option>option 2</option>
                                                <option>option 3</option>
                                                <option>option 4</option>
                                                <option>option 5</option>
                                            </select>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Patient Name</label>
                                            <input type="text" class="form-control" placeholder="patient name">
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Reg no</label>
                                            <input type="text" class="form-control" placeholder="Reg no">
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <button type="submit" class="btn btn-primary">Submit</button>
                                </div>
                            </form>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="box">
                            <div class="box-header">
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Reg No</th>
                                            <th>Patient Name</th>
                                            <th>Sex</th>
                                            <th>Age</th>
                                            <th>Ref Doc</th>
                                            <th>Center</th>
                                            <th>Test Name</th>
                                            <th>Amount</th>
                                            <th>Action</th>
                                            <th>Print</th>
                                            <th>Register</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>0001</td>
                                            <td>Chris Gayle</td>
                                            <td>Male</td>
                                            <td>40</td>
                                            <td>Virat Kohli</td>
                                            <td>Bangalore</td>
                                            <td>AEC</td>
                                            <td>200</td>
                                            <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Edit</span></a>
                                            </a></td>
                                            <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Print</span></a>
                                            </a></td>
                                            <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Register</span></a>
                                            </a></td>
                                        </tr>
                                        <tr>
                                            <td>0002</td>
                                            <td>AB Devillers</td>
                                            <td>Male</td>
                                            <td>38</td>
                                            <td>Virat Kohli</td>
                                            <td>Bangalore</td>
                                            <td>AEC</td>
                                            <td>100</td>
                                            <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Edit</span></a>
                                            </a></td>
                                            <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Print</span></a>
                                            </a></td>
                                             <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Register</span></a>
                                            </a></td>
                                        </tr>
                                        <tr>
                                            <td>0001</td>
                                            <td>Chris Gayle</td>
                                            <td>Male</td>
                                            <td>40</td>
                                            <td>Virat Kohli</td>
                                            <td>Bangalore</td>
                                            <td>AEC</td>
                                            <td>200</td>
                                            <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Edit</span></a>
                                            </a></td>
                                            <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Print</span></a>
                                            </a></td>
                                             <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Register</span></a>
                                            </a></td>
                                        </tr>
                                        <tr>
                                            <td>0004</td>
                                            <td>Antony</td>
                                            <td>Male</td>
                                            <td>40</td>
                                            <td>Virat Kohli</td>
                                            <td>Bangalore</td>
                                            <td>AEC</td>
                                           
                                            <td>200</td>
                                            <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Edit</span></a>
                                            </a></td>
                                            <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Print</span></a>
                                            </a></td>
                                             <td><a href="#">
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Register</span></a>
                                            </a></td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>Reg No</th>
                                            <th>Patient Name</th>
                                            <th>Sex</th>
                                            <th>Age</th>
                                            <th>Ref Doc</th>
                                            <th>Center</th>
                                            <th>Test Name</th>
                                            <th>Amount</th>
                                            <th>Action</th>
                                            <th>Print</th>
                                            <th>Register</th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </section>
            <!-- /.content -->
        </div>
        <!-- /.content-wrapper -->
</asp:Content>

