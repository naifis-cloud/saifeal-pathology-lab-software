using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            Commonfunction.GetAllGenders(ddlSex);
            BusinessLogic.FillMainDepartments(ddlMainDepts);
            BusinessLogic.FillMachines(ddlMachines);
            BusinessLogic.FillSamples(ddlSampleType);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 15, "Edit");
                    if (lbUserRights)
                        GetTestDetails(Convert.ToInt32(Request.QueryString["tid"]));
                }
                else
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 15, "Add");
                    if (lbUserRights)
                        txtCode.Text = Commonfunction.GenerateModuleCode("Test");
                }

                if (!lbUserRights)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
                        lblMessage.Text = "You are not authorised to edit a test.";
                    else
                        lblMessage.Text = "You are not authorised to add a test.";

                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Colcenter OR SubCol center OR Referral doctor
            {
                lblMessage.Text = "You are not authorised to add a test.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetTestDetails(int fiTestId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetTestDetails(fiTestId);
            if (lobjdtDetails.Rows.Count > 0)
            {
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stTestCode"]);
                txtName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stTestName"]);
                txtRoutineName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stTestRoutine"]);

                if (ddlMainDepts.Items.FindByValue(Convert.ToString(lobjdtDetails.Rows[0]["inMainDeptId"])) != null)
                    ddlMainDepts.Items.FindByValue(Convert.ToString(lobjdtDetails.Rows[0]["inMainDeptId"])).Selected = true;

                txtTestDesc.Text = Convert.ToString(lobjdtDetails.Rows[0]["stTestDesc"]);
                txtOrder.Text = Convert.ToString(lobjdtDetails.Rows[0]["inOrder"]);
                txtMethod.Text = Convert.ToString(lobjdtDetails.Rows[0]["stTestMethod"]);
                txtICDCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stICD10Code"]);
                txtHCPCSCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stHCPCSCode"]);
                txtCPTCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stCPTCode"]);
                txtTechnology.Text = Convert.ToString(lobjdtDetails.Rows[0]["stTechnology"]);
                txtMaterial.Text = Convert.ToString(lobjdtDetails.Rows[0]["stMaterial"]);
                txtRemarks.Text = Convert.ToString(lobjdtDetails.Rows[0]["stRemarks"]);
                txtConditions.Text = Convert.ToString(lobjdtDetails.Rows[0]["stConditions"]);

                if (Convert.ToInt32(lobjdtDetails.Rows[0]["inMachineId"]) > 0)
                    ddlMachines.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inMachineId"]);

                if (Convert.ToInt32(lobjdtDetails.Rows[0]["inSampleType"]) > 0)
                    ddlSampleType.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inSampleType"]);

                txtFormula.Text = Convert.ToString(lobjdtDetails.Rows[0]["stFormula"]);

                if (!string.IsNullOrEmpty(Convert.ToString(lobjdtDetails.Rows[0]["inDays"])))
                    txtDays.Text = Convert.ToString(lobjdtDetails.Rows[0]["inDays"]);

                txtShortForm.Text = Convert.ToString(lobjdtDetails.Rows[0]["stShortForm"]);
                txtSpecInstructions.Text = Convert.ToString(lobjdtDetails.Rows[0]["stInstructions"]);
                ddlSex.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["stSex"]);

                if (Convert.ToString(lobjdtDetails.Rows[0]["stAttachGraph"]) == "Y")
                    rbtnAttachGraphYes.Checked = true;
                else
                    rbtnAttachGraphNo.Checked = true;

                if (Convert.ToString(lobjdtDetails.Rows[0]["stIsActive"]) == "Y")
                    rbtnActive.Checked = true;
                else
                    rbtnInactive.Checked = true;


                if (Convert.ToString(lobjdtDetails.Rows[0]["stParams"]) == "S")
                    rbtnParamsSingle.Checked = true;
                else
                    rbtnParamsMultiple.Checked = true;

                if (Convert.ToString(lobjdtDetails.Rows[0]["stType"]) == "D")
                    rbtnDesc.Checked = true;
                else
                    rbtnNonDesc.Checked = true;

                if (Convert.ToString(lobjdtDetails.Rows[0]["stRange"]) == "Y")
                    rbtnAddRefRangeYes.Checked = true;
                else
                    rbtnAddRefRangeNo.Checked = true;

                //txtTestRate.Text = Convert.ToString(lobjdtDetails.Rows[0]["inTestRate"]);
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0, liTestId = 0, liMachineId = 0, liSampleType = 0, liDays = 0;
        string lstrAttachGraph = "Y", lstrIsActive = "Y", lstrParams = "S", lstrType = "D", lstrRefRange = "Y";

        try
        {
            if (rbtnAttachGraphNo.Checked)
                lstrAttachGraph = "N";

            if (rbtnInactive.Checked)
                lstrIsActive = "N";

            if (rbtnParamsMultiple.Checked)
                lstrParams = "M";

            if (rbtnNonDesc.Checked)
                lstrType = "N";

            if (rbtnAddRefRangeNo.Checked)
                lstrRefRange = "N";

            if (ddlMachines.SelectedIndex > 0)
                liMachineId = Convert.ToInt32(ddlMachines.SelectedValue);

            if (ddlSampleType.SelectedIndex > 0)
                liSampleType = Convert.ToInt32(ddlSampleType.SelectedValue);

            if (!string.IsNullOrEmpty(txtDays.Text.Trim()))
                liDays = Convert.ToInt32(txtDays.Text.Trim());

            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
                liTestId = Convert.ToInt32(Request.QueryString["tid"]);

            liRetVal = bllogic.InsertUpdateTest(liTestId, txtCode.Text.Trim(), txtName.Text.Trim(), txtRoutineName.Text.Trim(),
                Convert.ToInt32(ddlMainDepts.SelectedValue), txtTestDesc.Text.Trim(), Convert.ToInt32(txtOrder.Text.Trim()),
                txtMethod.Text.Trim(), txtICDCode.Text.Trim(), txtHCPCSCode.Text.Trim(), txtCPTCode.Text.Trim(), txtTechnology.Text.Trim(),
                txtMaterial.Text.Trim(), txtRemarks.Text.Trim(), txtConditions.Text.Trim(), liMachineId,
                liSampleType, txtFormula.Text.Trim(), liDays, txtShortForm.Text.Trim(),
                txtSpecInstructions.Text.Trim(), ddlSex.SelectedValue, lstrAttachGraph, lstrIsActive, lstrParams, lstrType, lstrRefRange, 0);
            //Convert.ToInt32(txtTestRate.Text.Trim())

            if (liRetVal > 0)
                Commonfunction.showMsg("Test details saved successfully.", this, "list-tests.aspx");
            else if (liRetVal == -1)
                Commonfunction.showMsg("Test code already exists.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Test name already exists.", this);
            else
                Commonfunction.showMsg("Error occurred while saving data!.", this);
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