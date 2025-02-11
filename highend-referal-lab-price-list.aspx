<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="highend-referal-lab-price-list.aspx.cs" Inherits="highend_referal_lab_price_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
        <!-- Content Wrapper. Contains page content -->
        <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
          <h1>
            High End Referral labsP
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
            <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                            <form role="form">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-1">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Code</label>
                                                <input type="code" class="form-control" id="Code1" placeholder="Ref code">
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Name</label>
                                                <input type="name" class="form-control" id="Name3" placeholder="Test Name">
                                            </div>
                                        </div>
                                        <div class="col-xs-1">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Cat</label>
                                                <input type="name" class="form-control" id="Name4" placeholder="Category">
                                            </div>
                                        </div>
                                         <div class="col-xs-1">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Fees</label>
                                                <input type="name" class="form-control" id="Name7" placeholder="Patient Fees">
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Method</label>
                                                <input type="name" class="form-control" id="Name6" placeholder="Method">
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Highend Lab</label>
                                            <select class="form-control">
                                                <option>Metropolis</option>
                                                <option>Thyrocare</option>
                                                <option>Lal Labs</option>
                                                <option>Ranbaxy</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Sample Instructions</label>
                                                <input type="code" class="form-control" id="Code2" placeholder="Sample Instructions">
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Ambient</label>
                                                <input type="name" class="form-control" id="Name8" placeholder="Ambient">
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">At 2-8 C</label>
                                                <input type="name" class="form-control" id="Name9" placeholder="At 2-8">
                                            </div>
                                        </div>
                                         <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">At -20C</label>
                                                <input type="name" class="form-control" id="Name10" placeholder="At -20C">
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Ship At</label>
                                                <input type="name" class="form-control" id="Name11" placeholder="Ship At">
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Analytical Descriptions</label>
                                                <input type="name" class="form-control" id="Name12" placeholder="Analytical Descriptions">
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Test Schedule</label>
                                                <input type="code" class="form-control" id="Code3" placeholder="Test Schedule">
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Reported On</label>
                                                <input type="name" class="form-control" id="Name13" placeholder="Reported On">
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Related InHouse Test</label>
                                            <select class="form-control">
                                                <option>Blood Test</option>
                                                <option>CBC</option>
                                                <option>Lipid</option>
                                                <option>Urine</option>
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
                    
                    <!-- /.col -->
                </div>
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
                        <th>Cat</th>
                        <th>Method</th>
                        <th>Analytical Desc</th>
                          <th>Schedule</th>
                          <th>Reported On</th>
                          <th>HighEnd Lab</th>
                          <th>Fees</th>
                          <th>Action</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr>
                        <td>0001</td>
                        <td>CBC</td>
                        <td>L7</td>
                        <td>ELISA</td>
                        <td>Test Description</td>
                          <td>Everyday</td>
                          <td>Daily</td>
                          <td>Metropolis</td>
                          <td><input type="name" class="form-control" id="Name2" placeholder="250" disabled></td>
                          <td><a href="#">
                <a href="#" title="edit"><i class="fa fa-edit"></i> <span>Edit</span></a>
                <a href="#" title="delete"><i class="fa fa-fw fa-trash-o"></i><span>Delete</span></a>
              </a></td>
                      </tr>
                      <tr>
                        <td>0001</td>
                        <td>Lipid Profile</td>
                        <td>L7</td>
                        <td>ELISA</td>
                        <td>Test Description</td>
                          <td>Everyday</td>
                          <td>Daily</td>
                          <td>Metropolis</td>
                          <td><input type="name" class="form-control" id="Name1" placeholder="250" disabled></td>
                          <td><a href="#">
                <a href="#" title="edit"><i class="fa fa-edit"></i> <span>Edit</span></a>
                <a href="#" title="delete"><i class="fa fa-fw fa-trash-o"></i><span>Delete</span></a>
              </a></td>
                      </tr>
                        <tr>
                        <td>0001</td>
                        <td>Malaria</td>
                        <td>L7</td>
                        <td>CLIA</td>
                        <td>Test Description</td>
                          <td>Everyday</td>
                          <td>Daily</td>
                          <td>Metropolis</td>
                          <td><input type="name" class="form-control" id="Name5" placeholder="250" disabled></td>
                          <td><a href="#">
                <a href="#" title="edit"><i class="fa fa-edit"></i> <span>Edit</span></a>
                <a href="#" title="delete"><i class="fa fa-fw fa-trash-o"></i><span>Delete</span></a>
              </a></td>
                      </tr>
                        <tr>
                        <td>0001</td>
                        <td>Dengue</td>
                        <td>L7</td>
                        <td>Enzyme</td>
                        <td>Test Description</td>
                          <td>Everyday</td>
                          <td>Daily</td>
                          <td>Metropolis</td>
                          <td><input type="name" class="form-control" id="Name14" placeholder="250" disabled></td>
                          <td><a href="#">
                <a href="#" title="edit"><i class="fa fa-edit"></i> <span>Edit</span></a>
                <a href="#" title="delete"><i class="fa fa-fw fa-trash-o"></i><span>Delete</span></a>
              </a></td>
                      </tr>
                     <tr>
                        <td>0001</td>
                        <td>CBC</td>
                        <td>L7</td>
                        <td>ELISA</td>
                        <td>Test Description</td>
                          <td>Everyday</td>
                          <td>Daily</td>
                          <td>Ranbaxy</td>
                          <td><input type="name" class="form-control" id="Name15" placeholder="250" disabled></td>
                          <td><a href="#">
                <a href="#" title="edit"><i class="fa fa-edit"></i> <span>Edit</span></a>
                <a href="#" title="delete"><i class="fa fa-fw fa-trash-o"></i><span>Delete</span></a>
              </a></td>
                      </tr>
                      <tr>
                        <td>0001</td>
                        <td>Lipid Profile</td>
                        <td>L7</td>
                        <td>ELISA</td>
                        <td>Test Description</td>
                          <td>Everyday</td>
                          <td>Daily</td>
                          <td>Ranbaxy</td>
                          <td><input type="name" class="form-control" id="Name16" placeholder="250" disabled></td>
                          <td><a href="#">
                <a href="#" title="edit"><i class="fa fa-edit"></i> <span>Edit</span></a>
                <a href="#" title="delete"><i class="fa fa-fw fa-trash-o"></i><span>Delete</span></a>
              </a></td>
                      </tr>
                        <tr>
                        <td>0001</td>
                        <td>Malaria</td>
                        <td>L7</td>
                        <td>CLIA</td>
                        <td>Test Description</td>
                          <td>Everyday</td>
                          <td>Daily</td>
                          <td>Ranbaxy</td>
                          <td><input type="name" class="form-control" id="Name17" placeholder="250" disabled></td>
                          <td><a href="#">
                <a href="#" title="edit"><i class="fa fa-edit"></i> <span>Edit</span></a>
                <a href="#" title="delete"><i class="fa fa-fw fa-trash-o"></i><span>Delete</span></a>
              </a></td>
                      </tr>
                        <tr>
                        <td>0001</td>
                        <td>Dengue</td>
                        <td>L7</td>
                        <td>Enzyme</td>
                        <td>Test Description</td>
                          <td>Everyday</td>
                          <td>Daily</td>
                          <td>Ranbaxy</td>
                          <td><input type="name" class="form-control" id="Name18" placeholder="250" disabled></td>
                          <td><a href="#">
                <a href="#" title="edit"><i class="fa fa-edit"></i> <span>Edit</span></a>
                <a href="#" title="delete"><i class="fa fa-fw fa-trash-o"></i><span>Delete</span></a>
              </a></td>
                      </tr>
                    </tbody>
                    <tfoot>
                      <tr>
                       <th>Code</th>
                        <th>Name</th>
                        <th>Cat</th>
                        <th>Method</th>
                        <th>Analytical Desc</th>
                          <th>Schedule</th>
                          <th>Reported On</th>
                          <th>HighEnd Lab</th>
                          <th>Fees</th>
                          <th>Action</th>
                      </tr>
                    </tfoot>
                  </table>
                </div><!-- /.box-body -->
              </div><!-- /.box -->
            </div><!-- /.col -->
          </div><!-- /.row -->
        </section><!-- /.content -->
      </div>
        <!-- /.content-wrapper -->

</asp:Content>

