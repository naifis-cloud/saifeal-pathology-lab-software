using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class patient_test_params : System.Web.UI.Page
{
    bool lbParamsForAllTests = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            int liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["pname"])
                && !string.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                if (liRoleFlag == 0) // if the role access is for the other users//
                {
                    if (!BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 26, "Add"))
                    {
                        lblMessage.Text = "You are not authorised to enter test results.";
                        pnlMainContent.Visible = false;
                        pnlHeader.Visible = false;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        GetPatientTestsForParamsEntry(Convert.ToInt32(Request.QueryString["pid"]));
                        lblPatientName.Text = Request.QueryString["pname"].Trim();

                        if (!lbParamsForAllTests)
                            btnSave.Visible = false;
                    }
                }
                else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Referral doctor
                {
                    lblMessage.Text = "You are not authorised to enter test results.";
                    pnlMainContent.Visible = false;
                    pnlHeader.Visible = false;
                    lblMessage.Visible = true;
                }
            }
        }
    }

    private void GetPatientTestsForParamsEntry(int fiPatientId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetPatientTestsForParamsEntry(fiPatientId);
            rptPatientTests.DataSource = lobjdtDetails;
            rptPatientTests.DataBind();
            hidTestCount.Value = Convert.ToString(rptPatientTests.Items.Count);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        bool lbValid = true;
        try
        {
            //for validating the test parameters grid
            foreach (RepeaterItem rptItem in rptPatientTests.Items)
            {
                Repeater rptTestsParams = (Repeater)rptItem.FindControl("rptTestsParams");
                if (rptTestsParams != null)
                {
                    foreach (RepeaterItem rptParamItem in rptTestsParams.Items)
                    {
                        TextBox txtParamValue = (TextBox)rptParamItem.FindControl("txtParamValue");
                        if (txtParamValue != null)
                        {
                            if (string.IsNullOrEmpty(txtParamValue.Text.Trim()))
                            {
                                lbValid = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    lbValid = false;
                    break;
                }
            }

            if (lbValid)
            {
                foreach (RepeaterItem rptItem in rptPatientTests.Items)
                {
                    Repeater rptTestsParams = (Repeater)rptItem.FindControl("rptTestsParams");
                    Repeater rptTestDesc = (Repeater)rptItem.FindControl("rptTestDesc");
                    Label lblTestId = (Label)rptItem.FindControl("lblTestId");

                    //For saving test parameters//
                    if (rptTestsParams != null && lblTestId != null)
                    {
                        foreach (RepeaterItem rptParamItem in rptTestsParams.Items)
                        {
                            TextBox txtParamValue = (TextBox)rptParamItem.FindControl("txtParamValue");
                            Label lblParamId = (Label)rptParamItem.FindControl("lblParamId");
                            if (txtParamValue != null && lblParamId != null)
                            {
                                bllogic.InsertPatientTestParams(Convert.ToInt32(Request.QueryString["pid"]),
                                    Convert.ToInt32(lblTestId.Text.Trim()), Convert.ToInt32(lblParamId.Text.Trim()), txtParamValue.Text.Trim());
                            }
                        }
                    }

                    // for saving test description//
                    if (rptTestDesc != null && lblTestId != null)
                    {
                        foreach (RepeaterItem rptParamItem in rptTestDesc.Items)
                        {
                            TextBox txtTestDesc = (TextBox)rptParamItem.FindControl("txtTestDesc");
                            if (txtTestDesc != null)
                            {
                                bllogic.InsertPatientTestDesc(Convert.ToInt32(Request.QueryString["pid"]),
                                    Convert.ToInt32(lblTestId.Text.Trim()), Server.HtmlEncode(txtTestDesc.Text.Trim()));
                            }
                        }
                    }
                }

                //Updating the test status to 'In Process'
                bllogic.UpdateTestStatus(Convert.ToInt32(Request.QueryString["pid"]), 3);

                if (Convert.ToInt32(Request.QueryString["flag"]) == 0)
                    Commonfunction.showMsg("Patient test parameters saved successfully.", this, "test-result-entry.aspx");
                else
                    Commonfunction.showMsg("Patient test parameters saved successfully.", this, "test-authorise.aspx");

            }
            else
            {
                if (Convert.ToInt32(Request.QueryString["flag"]) == 0)
                    Commonfunction.showMsg("Please enter parameter values for all tests.", this, "test-result-entry.aspx");
                else
                    Commonfunction.showMsg("Please enter parameter values for all tests.", this, "test-authorise.aspx");
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
    protected void rptPatientTests_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        string lstrTestDeptId = "";
        try
        {
            Repeater rptTestsParams = (Repeater)e.Item.FindControl("rptTestsParams");
            Repeater rptTestDesc = (Repeater)e.Item.FindControl("rptTestDesc");
            Label lblTestId = (Label)e.Item.FindControl("lblTestId");
            HtmlTableRow trNoData = (HtmlTableRow)e.Item.FindControl("trNoData");
            HiddenField hidTestParamCount = (HiddenField)e.Item.FindControl("hidTestParamCount");
            Panel tblTestParams = (Panel)e.Item.FindControl("tblTestParams");
            Panel tblTestDescription = (Panel)e.Item.FindControl("tblTestDescription");

            if (rptTestsParams != null && lblTestId != null && trNoData != null && hidTestParamCount != null
                && rptTestDesc != null && tblTestParams != null && tblTestDescription != null)
            {
                //Checking if the test department is X-Ray/Sonography
                lobjdtDetails = bllogic.GetTestDetails(Convert.ToInt32(lblTestId.Text.Trim()));
                if (lobjdtDetails.Rows.Count > 0)
                {
                    lstrTestDeptId = Convert.ToString(lobjdtDetails.Rows[0]["inMainDeptId"]);
                    //if the current test department id matches with the X-Ray/Sonography Id's
                    if (Convert.ToString(ConfigurationManager.AppSettings["MainDeptId"]).Contains(lstrTestDeptId.Trim()))
                    {
                        tblTestDescription.Visible = true;
                        tblTestParams.Visible = false;
                        //cValParamData.Enabled = false;
                        lbParamsForAllTests = true;

                        //Getting the saved test description//
                        lobjdtDetails = bllogic.GetPatientTestDesc(Convert.ToInt32(Request.QueryString["pid"]), Convert.ToInt32(lblTestId.Text.Trim()));

                        if (lobjdtDetails.Rows.Count <= 0)
                        {
                            //Getting the original test description//
                            lobjdtDetails = bllogic.GetTestDescription(Convert.ToInt32(lblTestId.Text.Trim()));
                        }
                        rptTestDesc.DataSource = lobjdtDetails;
                        rptTestDesc.DataBind();
                    }
                    else
                    {
                        tblTestDescription.Visible = false;
                        tblTestParams.Visible = true;
                        //cValParamData.Enabled = true;

                        lobjdtDetails = bllogic.GetPatientTestParameters(0, 0, "inOrderNo ASC", "", Convert.ToInt32(lblTestId.Text.Trim()), 1,
                            Convert.ToInt32(Request.QueryString["pid"]));

                        if (lobjdtDetails.Rows.Count <= 0)
                            lobjdtDetails = bllogic.GetAllTestParameters(0, 0, "inOrderNo ASC", "", Convert.ToInt32(lblTestId.Text.Trim()), 1);

                        if (lobjdtDetails.Rows.Count > 0)
                        {
                            rptTestsParams.DataSource = lobjdtDetails;
                            rptTestsParams.DataBind();
                            trNoData.Visible = false;
                            rptTestsParams.Visible = true;
                            lbParamsForAllTests = true;
                        }
                        else
                        {
                            trNoData.Visible = true;
                            rptTestsParams.Visible = false;
                        }
                        hidTestParamCount.Value = Convert.ToString(lobjdtDetails.Rows.Count);
                    }
                }
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