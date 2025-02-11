using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;
using AjaxControlToolkit;

public partial class add_collection_centre : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;

            BusinessLogic.FillRateLists(ddlRateType);
            Commonfunction.GetAllStates(ddlState);
            ddlCity.Items.Insert(0, new ListItem("--Select--", ""));

            int liCCId = 0, liRoleFlag = 0;

            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                if (int.TryParse(Request.QueryString["ccid"], out liCCId))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 1, "Edit");
                    if (lbUserRights)
                        GetColCenterDetails(liCCId);
                }
                else
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 1, "Add");
                    if (lbUserRights)
                        txtCode.Text = Commonfunction.GenerateModuleCode("ColCenter");
                }

                if (!lbUserRights)
                {
                    if (int.TryParse(Request.QueryString["ccid"], out liCCId))
                        lblMessage.Text = "You are not authorised to edit a collection center.";
                    else
                        lblMessage.Text = "You are not authorised to add a collection center.";

                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1) // if logged in as Col center
            {
                //if not edit mode then do not allow to create new collection center 
                if (!int.TryParse(Request.QueryString["ccid"], out liCCId))
                {
                    lblMessage.Text = "You are not authorised to add a collection center.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetColCenterDetails(liCCId);
            }
            else if (liRoleFlag == 2 || liRoleFlag == 3) // if logged in as SubCol center OR Referral doctor
            {
                lblMessage.Text = "You are not authorised to add a collection center.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex > 0)
            Commonfunction.GetCities(ddlCity, Convert.ToInt32(ddlState.SelectedValue));
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("--Select--", ""));
        }
        ToolkitScriptManager tman = (ToolkitScriptManager)this.Master.FindControl("ScriptManager1");
        if (tman != null)
        {
            tman.SetFocus(ddlState);
        }
    }

    private void GetColCenterDetails(int fiCollCenterId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtColcenter = new DataTable();
        try
        {
            lobjdtColcenter = bllogic.GetCollCenterDetails(fiCollCenterId);
            if (lobjdtColcenter.Rows.Count > 0)
            {
                txtCode.Text = Convert.ToString(lobjdtColcenter.Rows[0]["stCenterCode"]);
                txtName.Text = Convert.ToString(lobjdtColcenter.Rows[0]["stCenterName"]);
                txtAddress.Text = Convert.ToString(lobjdtColcenter.Rows[0]["stCenterAddress"]);

                if (ddlState.Items.FindByValue(Convert.ToString(lobjdtColcenter.Rows[0]["inStateId"])) != null)
                {
                    ddlState.Items.FindByValue(Convert.ToString(lobjdtColcenter.Rows[0]["inStateId"])).Selected = true;
                    Commonfunction.GetCities(ddlCity, Convert.ToInt32(ddlState.SelectedValue));

                    if (ddlCity.Items.FindByValue(Convert.ToString(lobjdtColcenter.Rows[0]["inCityId"])) != null)
                        ddlCity.Items.FindByValue(Convert.ToString(lobjdtColcenter.Rows[0]["inCityId"])).Selected = true;
                }
                txtPinCode.Text = Convert.ToString(lobjdtColcenter.Rows[0]["stPinCode"]);
                txtEmail.Text = Convert.ToString(lobjdtColcenter.Rows[0]["stEmailAddress"]);
                txtMobile.Text = Convert.ToString(lobjdtColcenter.Rows[0]["stMobileNo"]);
                txtPhone.Text = Convert.ToString(lobjdtColcenter.Rows[0]["stPhoneNo"]);
                txtDoctorName.Text = Convert.ToString(lobjdtColcenter.Rows[0]["stDoctorName"]);

                txtLabUnit.Text = Convert.ToString(lobjdtColcenter.Rows[0]["stLabUnit"]);

                ddlRateType.SelectedValue = Convert.ToString(lobjdtColcenter.Rows[0]["inRateListId"]);

                txtDepAmt.Text = Convert.ToString(lobjdtColcenter.Rows[0]["inDepositAmt"]);
                txtUsername.Text = Convert.ToString(lobjdtColcenter.Rows[0]["stUserName"]);

                txtpassword.Attributes.Add("value", Convert.ToString(lobjdtColcenter.Rows[0]["stPassword"]));

                if (Convert.ToBoolean(lobjdtColcenter.Rows[0]["flgStatus"]) == true)
                    rbtnActive.Checked = true;
                else
                    rbtnInactive.Checked = true;

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
        int liRetVal = 0, liColCenterId = 0;
        bool flgIsActive = true;

        try
        {
            if (rbtnInactive.Checked)
                flgIsActive = false;

            if (!string.IsNullOrEmpty(Request.QueryString["ccid"]))
                liColCenterId = Convert.ToInt32(Request.QueryString["ccid"]);

            liRetVal = bllogic.InsertUpdateCollCenter(liColCenterId, txtCode.Text.Trim(), txtName.Text.Trim(), txtAddress.Text.Trim(), Convert.ToInt32(ddlState.SelectedValue),
                Convert.ToInt32(ddlCity.SelectedValue), txtPinCode.Text.Trim(), txtEmail.Text.Trim(), txtMobile.Text.Trim(), txtPhone.Text.Trim(),
                txtDoctorName.Text.Trim(), txtLabUnit.Text.Trim(), Convert.ToInt32(ddlRateType.SelectedValue), Convert.ToInt32(txtDepAmt.Text.Trim()),
                txtUsername.Text.Trim(), txtpassword.Text.Trim(), flgIsActive);

            if (liRetVal > 0)
                Commonfunction.showMsg("Collection center details saved successfully.", this, "list-collection-centre.aspx");
            else if (liRetVal == -1)
                Commonfunction.showMsg("Collection center code already exists.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Collection center name already exists.", this);
            else if (liRetVal == -3)
                Commonfunction.showMsg("Username already exists.", this);
            else if (liRetVal == -4)
                Commonfunction.showMsg("Email address already exists.", this);
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