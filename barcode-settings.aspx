<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="barcode-settings.aspx.cs" Inherits="barcode_settings" %>

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
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>
                    Barcode Settings
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Machines</li>
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
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label>Code</label>
                                                <input type="text" class="form-control" placeholder="code">
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Barcode Settings Name: </label>
                                            <input type="text" class="form-control" placeholder="Barcode name">
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Barcode Type</label>
                                            <select class="form-control">
                                                <option>Roll</option>
                                                <option>Cut</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <h4>label margins</h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Left: </label>
                                            <input type="text" class="form-control" placeholder="e.g 1.0 inch">
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Right: </label>
                                            <input type="text" class="form-control" placeholder="e.g 1.0 inch">
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Top: </label>
                                            <input type="text" class="form-control" placeholder="e.g 1.0 inch">
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Bottom: </label>
                                            <input type="text" class="form-control" placeholder="e.g 1.0 inch">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Label Height: </label>
                                            <input type="text" class="form-control" placeholder="e.g 1.0 inch">
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Label Width: </label>
                                            <input type="text" class="form-control" placeholder="e.g 1.0 inch">
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Gap between labels: </label>
                                            <input type="text" class="form-control" placeholder="e.g 1.0 inch">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <h4>Label Column one</h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Barcode by: </label>
                                            <select class="form-control">
                                                <option>Vial ID</option>
                                                <option>Patient Reg #</option>
                                                <option>Patient Name</option>
                                            </select>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Barcode Codec: </label>
                                            <select class="form-control">
                                                <option>Codec 128</option>
                                                <option>Codec 148</option>
                                            </select>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Value below barcode visible ?: </label>
                                            <select class="form-control">
                                                <option>Yes</option>
                                                <option>No</option>
                                            </select>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Value: </label>
                                            <select class="form-control">
                                                <option>Patient Name</option>
                                                <option>Patient Reg #</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <h4>Label Column two</h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Column visible: </label>
                                            <select class="form-control">
                                                <option>Yes</option>
                                                <option>No</option>
                                            </select>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Barcode Font Family: </label>
                                            <input type="text" class="form-control" placeholder="Tahoma">
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Barcode Font size: </label>
                                            <input type="text" class="form-control" placeholder="e.g 8">
                                        </div>
                                        <div class="col-xs-3">
                                            <label> Lab Name</label>
                                            <input type="text" class="form-control" placeholder="e.g Lab Name">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label> Additional Text on label</label>
                                            <select class="form-control">
                                                <option>Test Name</option>
                                                <option>Patient Name</option>
                                                <option>Patient Reg#</option>
                                                <option>Ref lab</option>
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
                                            <th>Code</th>
                                            <th>Barcode Name</th>
                                            <th>Status</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>0001</td>
                                            <td>Barcode one</td>

                                            <td><a href="#">set as current</a></td>
                                            <td>
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Edit</span></a>
                                                <a href="#" title="delete"><i class="fa fa-fw fa-trash-o"></i><span>Delete</span></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>0002</td>
                                            <td>Barcode two</td>
                                            <td><a href="#">set as current</a></td>
                                            <td>
                                                <a href="#" title="edit"><i class="fa fa-edit"></i><span>Edit</span></a>
                                                <a href="#" title="delete"><i class="fa fa-fw fa-trash-o"></i><span>Delete</span></a>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>Code</th>
                                            <th>Barcode Name</th>
                                            <th>Status</th>
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
</asp:Content>
