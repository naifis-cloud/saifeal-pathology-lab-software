using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;
using AjaxControlToolkit;

public partial class add_sub_collection_centre : System.Web.UI.Page
{
    int liRoleFlag = 0, miColCenterId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            bool lbUserRights = true;

            //if logged in as collection center then show only his center in affiliate dropdown//
            if (liRoleFlag == 1)
                miColCenterId = Convert.ToInt32(Session["UserId"]);

            BusinessLogic.FillCollCenters(ddlCollCenter, miColCenterId);
            BusinessLogic.FillCollectionRateLists(ddlRateType);
            Commonfunction.GetAllStates(ddlState);
            ddlCity.Items.Insert(0, new ListItem("--Select--", ""));

            int liCCId = 0;

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                if (int.TryParse(Request.QueryString["sccid"], out liCCId))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 3, "Edit");
                    if (lbUserRights)
                        GetSubColCenterDetails(liCCId);
                }
                else
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 3, "Add");
                    if (lbUserRights)
                        txtCode.Text = Commonfunction.GenerateModuleCode("SubColCenter");
                }

                if (!lbUserRights)
                {
                    if (int.TryParse(Request.QueryString["sccid"], out liCCId))
                        lblMessage.Text = "You are not authorised to view this page.";
                    else
                        lblMessage.Text = "You are not authorised to add a subcollection center.";

                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            // if subcollection center is logged in, do not allow to view collection center//
            else if (liRoleFlag == 2)
            {
                if (!int.TryParse(Request.QueryString["sccid"], out liCCId))
                {
                    lblMessage.Text = "You are not authorised to create a subcollection center.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetSubColCenterDetails(liCCId);
            }
            else if (liRoleFlag == 3) // if logged in as Referral doctor
            {
                lblMessage.Text = "You are not authorised to add a subcollection center.";
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

    private void GetSubColCenterDetails(int fiSubCollCenterId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtColcenter = new DataTable();
        try
        {
            lobjdtColcenter = bllogic.GetSubCollCenterDetails(fiSubCollCenterId);
            if (lobjdtColcenter.Rows.Count > 0)
            {
                ddlCollCenter.SelectedValue = Convert.ToString(lobjdtColcenter.Rows[0]["inColCenterId"]);
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
        int liRetVal = 0, liSubColCenterId = 0;
        bool flgIsActive = true;

        try
        {
            if (rbtnInactive.Checked)
                flgIsActive = false;

            if (!string.IsNullOrEmpty(Request.QueryString["sccid"]))
                liSubColCenterId = Convert.ToInt32(Request.QueryString["sccid"]);

            liRetVal = bllogic.InsertUpdateSubCollCenter(liSubColCenterId, Convert.ToInt32(ddlCollCenter.SelectedValue), txtCode.Text.Trim(),
                txtName.Text.Trim(), txtAddress.Text.Trim(), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue),
                txtPinCode.Text.Trim(), txtEmail.Text.Trim(), txtMobile.Text.Trim(), txtPhone.Text.Trim(), txtDoctorName.Text.Trim(),
                txtLabUnit.Text.Trim(), Convert.ToInt32(ddlRateType.SelectedValue), Convert.ToInt32(txtDepAmt.Text.Trim()),
                txtUsername.Text.Trim(), txtpassword.Text.Trim(), flgIsActive);

            if (liRetVal > 0)
                Commonfunction.showMsg("Subcollection center details saved successfully.", this, "list-sub-collection-centres.aspx");
            else if (liRetVal == -1)
                Commonfunction.showMsg("Subcollection center code already exists.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Subcollection center name already exists.", this);
            else if (liRetVal == -3)
                Commonfunction.showMsg("Subcollection user name already exists.", this);
            else if (liRetVal == -4)
                Commonfunction.showMsg("Subcollection email address already exists.", this);
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