<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="sms-module.aspx.cs" Inherits="sms_module" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <!-- Content Wrapper. Contains page content -->
        <div class="content-wrapper">
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <h1>SMS Messaging Module
            <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Collection Centres</li>
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
                                        <div class="col-xs-12">
                                            <label>Segment by Groups</label>
                                            <div class="checkbox">
                                                <label>
                                                    <input type="checkbox">
                                                    All Employees                
                                                </label>
                                                <label>
                                                    <input type="checkbox">
                                                    All Referral Doctors                  
                                                </label>
                                                <label>
                                                    <input type="checkbox">
                                                    All Patients                  
                                                </label>
                                                <label>
                                                    <input type="checkbox">
                                                    All Collection Centres                  
                                                </label>
                                                <label>
                                                    <input type="checkbox">
                                                    All Sub Collection Centres                  
                                                </label>
                                            </div>
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

                    <!-- /.col -->
                </div>
                <div class="row">
                    <div class="col-xs-12">


                        <div class="box">
                            <div class="box-header">
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Select All</th>
                                            <th>Name</th>
                                            <th>Segment Type</th>
                                            <th>Mobile Number</th>
                                            <th>Email ID</th>
                                            <th>Birthday</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox">
                                                                      
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Chris Gayle</td>
                                            <td>Patient</td>
                                            <td>+91 9888778899</td>
                                            <td>chris@gmail.com</td>
                                            <td>NA</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox">
                                                       
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Virat Kohli</td>
                                            <td>Patient</td>
                                            <td>+91 8877788899</td>
                                            <td>virat@gmail.com</td>
                                            <td>NA</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox">
                                                       
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Dr Drek Remore</td>
                                            <td>Doctor</td>
                                            <td>+91 94455778899</td>
                                            <td>drdrek@gmail.com</td>
                                            <td>12-12-2014</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox">
                                                       
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Shamsher Singh</td>
                                            <td>Collect Centre</td>
                                            <td>+91 32588778899</td>
                                            <td>shamshr@gmail.com</td>
                                            <td>NA</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox">
                                                       
                                                    </label>
                                                </div>
                                            </td>
                                            <td>shah Rahul</td>
                                            <td>Collect Centre</td>
                                            <td>+91 32588778899</td>
                                            <td>shah@gmail.com</td>
                                            <td>NA</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox">
                                                       
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Chris Varma</td>
                                            <td>Patient</td>
                                            <td>+91 3258978899</td>
                                            <td>chris@gmail.com</td>
                                            <td>NA</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox">
                                                       
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Dr Vivek Singhania</td>
                                            <td>Patient</td>
                                            <td>+91 341258899</td>
                                            <td>vivek@gmail.com</td>
                                            <td>01-05-2014</td>
                                        </tr>
                                       
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>Select All</th>
                                            <th>Name</th>
                                            <th>Type</th>
                                            <th>Mobile Number</th>
                                            <th>Email ID</th>
                                            <th>Birthday</th>

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
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                            <form role="form">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Address</label>
                                        <textarea class="form-control" rows="3" placeholder="Enter Message..."></textarea>
                                        </div>


                                    </div>

                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <button type="submit" class="btn btn-primary">Send Message</button>
                                </div>
                            </form>
                        </div>
                    </div>

                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </section>
            <!-- /.content -->
        </div>
        <!-- /.content-wrapper -->
</asp:Content>

