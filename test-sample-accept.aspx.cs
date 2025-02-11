using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class test_sample_accept : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;

            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            Commonfunction.GetAllTestStatus(ddlTestStatus);
            BusinessLogic.FillMainDepartments(ddlDepts);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 25, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view test sample accept/reject page.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllPatients(0);
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Referral doctor
            {
                lblMessage.Text = "You are not authorised to view test sample accept/reject page.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetAllPatients(0);
    }

    private void GetAllPatients(int fiTestStatusId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        string lstrFromDate = "", lstrToDate = "", lstrPatientName = "", lstrRegNo = "";
        int liDeptId = 0, liTestStatusId = 0;
        try
        {
            if (!string.IsNullOrEmpty(Request.Form.Get(txtFromdate.UniqueID)))
                lstrFromDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(Request.Form.Get(txtFromdate.UniqueID), new System.Globalization.CultureInfo("en-GB")));

            if (!string.IsNullOrEmpty(Request.Form.Get(txtTodate.UniqueID)))
                lstrToDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(Request.Form.Get(txtTodate.UniqueID), new System.Globalization.CultureInfo("en-GB")));

            if (!string.IsNullOrEmpty(txtPatientName.Text.Trim()))
                lstrPatientName = txtPatientName.Text.Trim();

            if (!string.IsNullOrEmpty(txtRegNo.Text.Trim()))
                lstrRegNo = txtRegNo.Text.Trim();

            if (ddlDepts.SelectedIndex > 0)
                liDeptId = Convert.ToInt32(ddlDepts.SelectedValue);

            if (ddlTestStatus.SelectedIndex > 0)
                liTestStatusId = Convert.ToInt32(ddlTestStatus.SelectedValue);
            else
            {
                if (fiTestStatusId > 0)
                    liTestStatusId = fiTestStatusId;
            }

            lobjdtDetails = bllogic.GetAllPatients(lstrFromDate, lstrToDate, liDeptId, liTestStatusId, lstrPatientName, lstrRegNo, "", 0, 0);
            rptPatients.DataSource = lobjdtDetails;
            rptPatients.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptPatients.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptPatients.Visible = false;
                trNoData.Visible = true;
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
        finally
        {
            bllogic = null;
        }
    }

    protected void btnPatientRegd_Click(object sender, EventArgs e)
    {
        GetAllPatients(1);
        btnPatientRegd.CssClass = "btn btn-block btn-primary";
        btnSampleColl.CssClass = "btn btn-block btn-default";
    }

    protected void btnSampleColl_Click(object sender, EventArgs e)
    {
        GetAllPatients(2);
        btnPatientRegd.CssClass = "btn btn-block btn-default";
        btnSampleColl.CssClass = "btn btn-block btn-primary";
    }

    public string GetTestStatusCSS(int fiTestStatusId)
    {
        string lstrTestCSS = "";

        if (fiTestStatusId == 1)
            lstrTestCSS = "label bg-orange";
        else if (fiTestStatusId == 2)
            lstrTestCSS = "label bg-green";
        else if (fiTestStatusId == 3)
            lstrTestCSS = "label bg-blue";
        else if (fiTestStatusId == 9)
            lstrTestCSS = "label bg-red";
        else if (fiTestStatusId == 5)
            lstrTestCSS = "label bg-maroon";
        else if (fiTestStatusId == 7)
            lstrTestCSS = "label bg-navy";

        return lstrTestCSS;
    }

    public string ShowHideBarCodeLink(int fiPatientId, int fiTestStatusId,int fiLinkFlag)
    {
        string lstrDisplayStyle = "";
        BusinessLogic bllogic = new BusinessLogic();
        
        try
        {
            if (fiLinkFlag == 1)
            {
                if (fiTestStatusId == 2)
                    lstrDisplayStyle = "display:none;";
                else
                    lstrDisplayStyle = "display:block;";
            }
            else if (fiLinkFlag == 2)
            {
                if (fiTestStatusId == 2)
                    lstrDisplayStyle = "display:block;";
                else
                    lstrDisplayStyle = "display:none;";
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
        finally
        {
            bllogic = null;
        }
        return lstrDisplayStyle;
    }


    public string ShowHideRejectLink(int fiTestStatusId,int fiFlag)
    {
        string lstrDisplayStyle = "";
        try
        {
            if (fiFlag == 1)
            {
                if (fiTestStatusId == 2)
                    lstrDisplayStyle = "display:block;";
                else
                    lstrDisplayStyle = "display:none;";
            }
            else if (fiFlag == 2)
            {
                // if the test samples are rejected then display reason link
                if (fiTestStatusId == 9)
                    lstrDisplayStyle = "display:block;";
                else
                    lstrDisplayStyle = "display:none;";
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
        return lstrDisplayStyle;
    }

    protected void rptPatients_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblTestStatusId = (Label)e.Item.FindControl("lblTestStatusId");
            Button btnAccept = (Button)e.Item.FindControl("btnAccept");
            //Button btnReject = (Button)e.Item.FindControl("btnReject");

            if (lblTestStatusId != null && btnAccept != null)
            {
                if (lblTestStatusId.Text.Trim() == "2") // sample collected 
                {
                    btnAccept.Visible = false;
                    //btnReject.Visible = true;
                }
                else if (lblTestStatusId.Text.Trim() == "6" || lblTestStatusId.Text.Trim() == "7") // sample collected 
                {
                    btnAccept.Visible = false;
                    //btnReject.Visible = false;
                }
                else
                {
                    btnAccept.Visible = true;
                    //btnReject.Visible = false;
                }
            }
        }
    }

    protected void rptPatients_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        try
        {
            int liCountCheck = 0, liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (!BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 25, "Add"))
                Commonfunction.showMsg("You are not authorised to accept test samples.", this);
            else
            {
                if (e.CommandName == "eAccept")
                {
                    if (liRoleFlag == 0) // if the role access is for the other users//
                    {
                        //liCountCheck = bllogic.ValidateTestBarcodes(Convert.ToInt32(e.CommandArgument));
                        //if (liCountCheck == 0)
                        //    Commonfunction.showMsg("Please assign test barcodes before accepting test samples.", this);
                        //else
                        //{
                        liCountCheck = bllogic.UpdateTestStatus(Convert.ToInt32(e.CommandArgument), 2);
                        if (liCountCheck > 0)
                        {
                            Commonfunction.showMsg("Sample(s) accepted successfully.", this);
                            GetAllPatients(0);
                        }
                        //}
                    }
                }
                //else if (e.CommandName == "eReject")
                //{
                //    //deleting all the test barcodes of the patient first and then updating the status as 'Rejected'
                //    bllogic.DeleteTestBarCodes(Convert.ToInt32(e.CommandArgument));

                //    liCountCheck = bllogic.UpdateTestStatus(Convert.ToInt32(e.CommandArgument), 9);
                //    if (liCountCheck > 0)
                //    {
                //        Commonfunction.showMsg("Test sample rejected successfully.", this);
                //        GetAllPatients(0);
                //    }
                //}
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
        finally
        {
            bllogic = null;
        }
    }
}