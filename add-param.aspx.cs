using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_param : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                if (liRoleFlag == 0) // if the role access is for the other users//
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 16, "Edit");
                    else
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 16, "Add");

                    if (lbUserRights)
                    {
                        BusinessLogic.FillMachines(ddlMachines);
                        BusinessLogic.FillSamples(ddlSampleType);

                        if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                            GetParameterDetails(Convert.ToInt32(Request.QueryString["pid"]), Convert.ToInt32(Request.QueryString["tid"]));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                            lblMessage.Text = "You are not authorised to edit a test parameter.";
                        else
                            lblMessage.Text = "You are not authorised to add a test parameter.";

                        pnlMainContent.Visible = false;
                        lblMessage.Visible = true;
                    }
                }
                else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Colcenter OR SubCol center OR Referral doctor
                {
                    lblMessage.Text = "You are not authorised to add/edit a test parameter.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
            }
        }
    }

    private void GetParameterDetails(int fiParamId, int fiTestId)
    {
        BusinessLogic blLogic = new BusinessLogic();
        DataTable lobjdtData = new DataTable();
        try
        {
            lobjdtData = blLogic.GetParameterDetails(fiParamId, fiTestId);
            if (lobjdtData.Rows.Count > 0)
            {
                ddlType.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["stType"]);
                txtCode.Text = Convert.ToString(lobjdtData.Rows[0]["stCode"]);
                txtName.Text = Convert.ToString(lobjdtData.Rows[0]["stName"]);
                txtOrderNo.Text = Convert.ToString(lobjdtData.Rows[0]["inOrderNo"]);

                if (ddlType.SelectedValue.Trim() == "P")
                {
                    txtParamDesc.Text = Convert.ToString(lobjdtData.Rows[0]["stParamDesc"]);
                    txtTechnology.Text = Convert.ToString(lobjdtData.Rows[0]["stTechnology"]);
                    txtMaterial.Text = Convert.ToString(lobjdtData.Rows[0]["stMaterial"]);
                    txtMethod.Text = Convert.ToString(lobjdtData.Rows[0]["stMethod"]);
                    txtShortForm.Text = Convert.ToString(lobjdtData.Rows[0]["stShortForm"]);
                    ddlMachines.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["inMachineId"]);
                    ddlSampleType.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["inSampleId"]);
                    txtDefaultValue.Text = Convert.ToString(lobjdtData.Rows[0]["stDefaultValue"]);
                    txtUpper.Text = Convert.ToString(lobjdtData.Rows[0]["inUpper"]);
                    txtLower.Text = Convert.ToString(lobjdtData.Rows[0]["inLower"]);
                    txtUnit.Text = Convert.ToString(lobjdtData.Rows[0]["stUnit"]);
                }
                else
                {
                    txtParamDesc.Enabled = false;
                    reqvalDesc.Enabled = false;
                    txtTechnology.Enabled = false;
                    //reqvalTechnology.Enabled = false;
                    txtMaterial.Enabled = false;
                    //reqvalMaterial.Enabled = false;
                    txtMethod.Enabled = false;
                    //reqvalMethod.Enabled = false;
                    txtShortForm.Enabled = false;
                    //reqvalShortForm.Enabled = false;
                    ddlMachines.Enabled = false;
                    //reqvalMachines.Enabled = false;
                    ddlSampleType.Enabled = false;
                    //reqvalSampleType.Enabled = false;
                    txtDefaultValue.Enabled = false;
                    txtUpper.Enabled = false;
                    reqvalUpper.Enabled = false;
                    txtLower.Enabled = false;
                    reqvalLower.Enabled = false;
                    txtUnit.Enabled = false;
                    //reqvalUnit.Enabled = false;
                }

                if (Convert.ToBoolean(lobjdtData.Rows[0]["btBold"]) == true)
                    rbtnBold.Checked = true;
                else
                    rbtnNoBold.Checked = true;

                if (Convert.ToBoolean(lobjdtData.Rows[0]["btUnderLine"]) == true)
                    rbtnUnderLine.Checked = true;
                else
                    rbtnNoLine.Checked = true;

                ddlColor.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["stColor"]);
                txtLines.Text = Convert.ToString(lobjdtData.Rows[0]["inLines"]);
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
        finally
        {
            blLogic = null;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        int liRetVal = 0, liTestId = 0, liParamId = 0, liMachineId = 0,
        liSampleId = 0, liLines = 0, liOrderNo = 0;
        bool lbBold = true, lbUnderline = true;
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
                liTestId = Convert.ToInt32(Request.QueryString["tid"]);

            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                liParamId = Convert.ToInt32(Request.QueryString["pid"]);

            if (rbtnNoBold.Checked)
                lbBold = false;

            if (rbtnNoLine.Checked)
                lbUnderline = false;

            if (ddlMachines.SelectedIndex == 0)
                liMachineId = 0;
            else
                liMachineId = Convert.ToInt32(ddlMachines.SelectedValue.Trim());

            if (ddlSampleType.SelectedIndex == 0)
                liSampleId = 0;
            else
                liSampleId = Convert.ToInt32(ddlSampleType.SelectedValue.Trim());

            //if (string.IsNullOrEmpty(txtLower.Text.Trim()))
            //    ldcLower = 0;
            //else
            //    ldcLower = Convert.ToDecimal(txtLower.Text.Trim());

            //if (string.IsNullOrEmpty(txtUpper.Text.Trim()))
            //    ldcUpper = 0;
            //else
            //    ldcUpper = Convert.ToDecimal(txtUpper.Text.Trim());

            if (string.IsNullOrEmpty(txtLines.Text.Trim()))
                liLines = 0;
            else
                liLines = Convert.ToInt32(txtLines.Text.Trim());

            if (!string.IsNullOrEmpty(txtOrderNo.Text.Trim()))
                liOrderNo = Convert.ToInt32(txtOrderNo.Text.Trim());

            liRetVal = blLogic.InsertUpdateTestParameter(liParamId, liTestId, ddlType.SelectedValue.Trim(),
                txtCode.Text.Trim(), txtName.Text.Trim(), liOrderNo, txtParamDesc.Text.Trim(),
                txtTechnology.Text.Trim(), txtMaterial.Text.Trim(), txtMethod.Text.Trim(), txtShortForm.Text.Trim(), liMachineId,
                liSampleId, txtDefaultValue.Text.Trim(), txtUpper.Text.Trim(), txtLower.Text.Trim(), txtUnit.Text.Trim(), lbBold, lbUnderline,
                ddlColor.SelectedValue.Trim(), liLines);

            if (liRetVal > 0)
                Commonfunction.showMsg("Test parameter details saved successfully.", this, "list-param-for-test.aspx?tid=" + Request.QueryString["tid"]);
            else if (liRetVal == -1)
                Commonfunction.showMsg("Code already exists.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Name already exists.", this);
            else if (liRetVal == -3)
                Commonfunction.showMsg("Order No already exists.", this);
        }
        catch (Exception foException)
        {
            throw new Exception(foException.StackTrace);
        }
        finally
        {
            blLogic = null;
        }
    }
}