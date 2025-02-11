<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="set-doctor-referal.aspx.cs" Inherits="set_doctor_referal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
        <!-- Content Wrapper. Contains page content -->
        <div class="content-wrapper">
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <h1>Set Doctor Referal
           
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Doctor Referal</li>
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
                                            <label>By Days</label>
                                            <select class="form-control">
                                                <option>Select</option>
                                                <option>Today</option>
                                                <option>Last 7 Days</option>
                                                <option>Last 30 Days</option>
                                            </select>
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
                                            <th>Date</th>
                                            <th>Department</th>
                                            <th>Patient</th>
                                            <th>Test</th>
                                            <th>Doctor</th>
                                            <th>Total</th>
                                            <th>Disc Amt</th>
                                            <th>Net Amt</th>
                                            <th>Doc Referal</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>12/01/2015</td>
                                            <td>Pathology</td>
                                            <td>Virat</td>
                                            <td>Blood profile</td>
                                            <td>AB Deviliers</td>
                                            <td>150</td>
                                            <td>50</td>
                                            <td>100</td>
                                            <td><input type="name" class="form-control" id="Name5" placeholder="100" disabled></td>
                                            <td><a href="#">Edit </a>| <a href="#">Save</a></td>
                                        </tr>
                                        <tr>
                                            <td>12/01/2015</td>
                                            <td>Pathology</td>
                                            <td>Virat</td>
                                            <td>Lipid profile</td>
                                            <td>AB Deviliers</td>
                                            <td>150</td>
                                            <td>50</td>
                                            <td>100</td>
                                            <td><input type="name" class="form-control" id="Name1" placeholder="150" disabled></td>
                                            <td><a href="#">Edit </a>| <a href="#">Save</a></td>
                                        </tr>
                                        <tr>
                                            <td>12/01/2015</td>
                                            <td>Pathology</td>
                                            <td>Virat</td>
                                            <td>CBC profile</td>
                                            <td>AB Deviliers</td>
                                            <td>150</td>
                                            <td>50</td>
                                            <td>100</td>
                                            <td><input type="name" class="form-control" id="Name2" placeholder="250" disabled></td>
                                            <td><a href="#">Edit </a>| <a href="#">Save</a></td>
                                        </tr>
                                        
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                         <th>Date</th>
                                            <th>Department</th>
                                            <th>Patient</th>
                                            <th>Test</th>
                                            <th>Doctor</th>
                                            <th>Total</th>
                                            <th>Disc Amt</th>
                                            <th>Net Amt</th>
                                            <th>Doc Referal</th>
                                            <th>Action</th>
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

