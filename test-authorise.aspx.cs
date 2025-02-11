using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class test_authorise : System.Web.UI.Page
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
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 27, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view test authorization page.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllPatients(0);
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Referral doctor
            {
                lblMessage.Text = "You are not authorised to view test authorization page.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
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

            lobjdtDetails = bllogic.GetAllPatients(lstrFromDate, lstrToDate, liDeptId, liTestStatusId, lstrPatientName, lstrRegNo, "", 0,0);
            rptPatients.DataSource = lobjdtDetails;
            rptPatients.DataBind();

            hidCount.Value = Convert.ToString(rptPatients.Items.Count);

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

    public string ShowHideValuesLink(int fiTestStatusId)
    {
        string lstrDisplayStyle = "";
        try
        {
            if (fiTestStatusId == 2 || fiTestStatusId == 3)
                lstrDisplayStyle = "display:block;";
            else
                lstrDisplayStyle = "display:none;";
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }

        return lstrDisplayStyle;
    }

    public bool ShowHideCheckBox(int fiTestStatusId)
    {
        bool lstrDisplayStyle;

        try
        {
            // if test status is 'In Process'
            if (fiTestStatusId == 3)
                lstrDisplayStyle = true;
            else
                lstrDisplayStyle = false;
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }

        return lstrDisplayStyle;
    }

    public string ShowHideAuthoriseLink(int fiPatientId, int fiTestStatusId)
    {
        string lbDisplayStyle = "";
        BusinessLogic bllogic = new BusinessLogic();
        try
        {
            int liRetVal = bllogic.ValidatePatientTestParams(fiPatientId);

            //if the patient test params are entered//
            if (liRetVal > 0)
            {
                //if test is Dispatched OR Printed OR // fully authorised//
                if (fiTestStatusId == 6 || fiTestStatusId == 7 || fiTestStatusId == 8)
                    lbDisplayStyle = "display:none;";
                else
                    lbDisplayStyle = "display:block;";
            }
            else
            {
                liRetVal = bllogic.ValidatePatientTestDesc(fiPatientId);

                //if patient test description is saved//
                if (liRetVal > 0)
                {
                    //if test is Dispatched OR Printed OR // fully authorised//
                    if (fiTestStatusId == 6 || fiTestStatusId == 7 || fiTestStatusId == 8)
                        lbDisplayStyle = "display:none;";
                    else
                        lbDisplayStyle = "display:block;";
                }
                else
                    lbDisplayStyle = "display:none;";
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
        return lbDisplayStyle;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetAllPatients(0);
    }

    //protected void rptPatients_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    BusinessLogic bllogic = new BusinessLogic();
    //    try
    //    {
    //        int liCountCheck = 0, liRoleFlag = 0;
    //        liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

    //        if (e.CommandName == "eAuthorise")
    //        {
    //            if (!BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 27, "Add"))
    //                Commonfunction.showMsg("You are not authorised to authorise a test.", this);
    //            else
    //            {
    //                liCountCheck = bllogic.ValidatePatientTestParams(Convert.ToInt32(e.CommandArgument));
    //                if (liCountCheck == 0)
    //                    Commonfunction.showMsg("Please enter param values for all the tests before authorising it.", this);
    //                else
    //                {
    //                    //updating the test status to 'full authorise'
    //                    liCountCheck = bllogic.UpdateTestStatus(Convert.ToInt32(e.CommandArgument), 7);
    //                    if (liCountCheck > 0)
    //                    {
    //                        Commonfunction.showMsg("Test authorised successfully.", this);
    //                        GetAllPatients(0);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception foException)
    //    {
    //        throw new Exception(foException.Message);
    //    }
    //    finally
    //    {
    //        bllogic = null;
    //    }
    //}

    protected void btnAuthoriseAll_Click(object sender, EventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            int liCountCheck = 0;

            foreach (RepeaterItem rptItem in rptPatients.Items)
            {
                CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
                Label lblPatientId = (Label)rptItem.FindControl("lblPatientId");
                if (chkSelect != null && chkSelect.Visible == true && chkSelect.Checked == true && lblPatientId != null)
                {
                    liCountCheck = bllogic.ValidatePatientTestParams(Convert.ToInt32(lblPatientId.Text.Trim()));
                    if (liCountCheck == 0)
                        Commonfunction.showMsg("Please enter param values for all the tests before authorising it.", this);
                    else
                    {
                        lobjdtDetails = bllogic.GetPatientTests(Convert.ToInt32(lblPatientId.Text.Trim()));

                        if (lobjdtDetails.Rows.Count > 0)
                        {
                            foreach (DataRow lobjdtRow in lobjdtDetails.Rows)
                            {
                                bllogic.UpdateAuthoriseStatus(Convert.ToInt32(lblPatientId.Text.Trim()), Convert.ToInt32(lobjdtRow["inTestId"]));
                            }
                            lobjdtDetails = bllogic.GetTestAuthoriseCount(Convert.ToInt32(lblPatientId.Text.Trim()));
                            if (lobjdtDetails.Rows.Count > 0)
                            {
                                //if all the tests are authorised, updating the test status to 'full authorise' else 'partial authorise'
                                if (Convert.ToInt32(lobjdtDetails.Rows[0]["TotalTestCount"]) > 0)
                                {
                                    if (Convert.ToInt32(lobjdtDetails.Rows[0]["TestAuthCount"]) == Convert.ToInt32(lobjdtDetails.Rows[0]["TotalTestCount"]))
                                        liCountCheck = bllogic.UpdateTestStatus(Convert.ToInt32(lblPatientId.Text.Trim()), 7);
                                    else
                                        liCountCheck = bllogic.UpdateTestStatus(Convert.ToInt32(lblPatientId.Text.Trim()), 5);
                                }

                                if (liCountCheck > 0)
                                    Commonfunction.showMsg("Selected tests authorised successfully.", this);
                            }
                        }
                        else
                            Commonfunction.showMsg("Error occurred authorising one of the patient tests.", this);
                    }
                }
            }
            GetAllPatients(0);
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