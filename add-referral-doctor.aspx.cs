using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;
using AjaxControlToolkit;

public partial class add_referral_doctor : System.Web.UI.Page
{
    bool lbUserRights = true;
    int liRefDoctorId = 0, liRoleFlag = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            ddlCollCenter.Items.Insert(0, new ListItem("--Select--", ""));

            Commonfunction.GetAllGenders(ddlSex);
            Commonfunction.GetAllStates(ddlState);
            ddlCity.Items.Insert(0, new ListItem("--Select--", ""));

            if (!int.TryParse(Request.QueryString["rfid"], out liRefDoctorId))
                txtCode.Text = Commonfunction.GenerateModuleCode("RefDoc");

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                ddlCollCenter.Items.Insert(1, new ListItem("Main Lab", "0"));
                if (int.TryParse(Request.QueryString["rfid"], out liRefDoctorId))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 5, "Edit");
                    if (lbUserRights)
                        GetRefDoctorDetails(liRefDoctorId);
                }
                else
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 5, "Add");

                if (!lbUserRights)
                {
                    if (int.TryParse(Request.QueryString["rfid"], out liRefDoctorId))
                        lblMessage.Text = "You are not authorised to view referral doctors.";
                    else
                        lblMessage.Text = "You are not authorised to add a referral doctor.";

                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (liRoleFlag == 3)
                {
                    if (int.TryParse(Request.QueryString["rfid"], out liRefDoctorId))
                        GetRefDoctorDetails(liRefDoctorId);
                    else
                    {
                        lblMessage.Text = "You are not authorised to add a referral doctor.";
                        pnlMainContent.Visible = false;
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    if (liRoleFlag == 1) //if logged in as Colcenter
                    {
                        ddlAffiliateType.Items.RemoveAt(0);
                        FillAffiliateDropdown(Convert.ToInt32(Session["UserId"]), 0);
                    }

                    else if (liRoleFlag == 2) //if logged in as subcolcenter
                    {
                        ddlAffiliateType.SelectedValue = "SC";
                        ddlAffiliateType.Enabled = false;
                        FillAffiliateDropdown(0, Convert.ToInt32(Session["UserId"]));
                    }
                    //lblMessage.Text = "You are not authorised to add a referral doctor.";
                    //pnlMainContent.Visible = false;
                    //lblMessage.Visible = true;
                }
            }
        }
    }

    private void GetRefDoctorDetails(int fiRefDoctorId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtRefDoctorDetails = new DataTable();
        try
        {
            lobjdtRefDoctorDetails = bllogic.GetReferalDoctorDetails(fiRefDoctorId);
            if (lobjdtRefDoctorDetails.Rows.Count > 0)
            {
                txtCode.Text = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stCode"]);
                txtName.Text = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stName"]);

                ddlAffiliateType.SelectedValue = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stAffiliateType"]);
                if (liRoleFlag == 1)
                    FillAffiliateDropdown(Convert.ToInt32(Session["UserId"]), 0);
                else if (liRoleFlag == 2)
                    FillAffiliateDropdown(Convert.ToInt32(Session["UserId"]), 0);
                else
                    FillAffiliateDropdown(0, 0);

                ddlCollCenter.SelectedValue = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["inColCenterId"]);

                txtAddress.Text = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stAddress"]);

                if (ddlState.Items.FindByValue(Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["inStateId"])) != null)
                {
                    ddlState.Items.FindByValue(Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["inStateId"])).Selected = true;
                    Commonfunction.GetCities(ddlCity, Convert.ToInt32(ddlState.SelectedValue));

                    if (ddlCity.Items.FindByValue(Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["inCityId"])) != null)
                        ddlCity.Items.FindByValue(Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["inCityId"])).Selected = true;
                }

                txtPinCode.Text = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stPinCode"]);
                txtEmail.Text = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stEmailAddress"]);
                txtMobile.Text = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stMobileNo"]);
                txtPhone.Text = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stPhoneNo"]);
                ddlSex.SelectedValue = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stSex"]);
                txtDOB.Text = String.Format("{0:dd/MM/yyyy}", lobjdtRefDoctorDetails.Rows[0]["dtDOB"]);
                txtQualification.Text = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stQualification"]);
                ddlReferral.SelectedValue = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["inRefPercentage"]);
                txtClinicName.Text = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stClinicName"]);
                txtUserName.Text = Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stUserName"]);
                txtPassword.Attributes.Add("value", Convert.ToString(lobjdtRefDoctorDetails.Rows[0]["stPassword"]));

                if (Convert.ToBoolean(lobjdtRefDoctorDetails.Rows[0]["flgStatus"]) == true)
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

    protected void ddlAffiliateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

        if (liRoleFlag == 1)
            FillAffiliateDropdown(Convert.ToInt32(Session["UserId"]), 0);

        else if (liRoleFlag == 2)
            FillAffiliateDropdown(0, Convert.ToInt32(Session["UserId"]));

        else
            FillAffiliateDropdown(0, 0);
    }

    private void FillAffiliateDropdown(int fiColCenterid, int fiSubColCenterId)
    {
        ddlCollCenter.Items.Clear();
        if (ddlAffiliateType.SelectedValue == "M")
        {
            ddlCollCenter.Items.Insert(0, new ListItem("-Select--", ""));
            ddlCollCenter.Items.Insert(1, new ListItem("Main Lab", "0"));
        }
        else if (ddlAffiliateType.SelectedValue == "C")
            BusinessLogic.FillCollCenters(ddlCollCenter, fiColCenterid);

        else if (ddlAffiliateType.SelectedValue == "SC")
            BusinessLogic.FillSubCollCenters(ddlCollCenter, fiColCenterid, fiSubColCenterId);

        ToolkitScriptManager tman = (ToolkitScriptManager)this.Master.FindControl("ScriptManager1");
        if (tman != null)
        {
            tman.SetFocus(ddlAffiliateType);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0, liRefDoctorId = 0;
        bool flgIsActive = true;

        try
        {
            if (rbtnInactive.Checked)
                flgIsActive = false;

            if (!string.IsNullOrEmpty(Request.QueryString["rfid"]))
                liRefDoctorId = Convert.ToInt32(Request.QueryString["rfid"]);

            liRetVal = bllogic.InsertUpdateReferalDoctor(liRefDoctorId, txtCode.Text.Trim(), txtName.Text.Trim(), ddlAffiliateType.SelectedValue,
                Convert.ToInt32(ddlCollCenter.SelectedValue), txtAddress.Text.Trim(), Convert.ToInt32(ddlState.SelectedValue),
                Convert.ToInt32(ddlCity.SelectedValue), txtPinCode.Text.Trim(), txtEmail.Text.Trim(), txtMobile.Text.Trim(),
                txtPhone.Text.Trim(), ddlSex.SelectedValue.Trim(), DateTime.ParseExact(String.Format("{0:dd/MM/yyyy}",
                txtDOB.Text.Trim()), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                txtQualification.Text.Trim(), Convert.ToInt32(ddlReferral.Text.Trim()), txtClinicName.Text.Trim(),
                txtUserName.Text.Trim(), txtPassword.Text.Trim(), flgIsActive);

            if (liRetVal > 0)
                Commonfunction.showMsg("Referal doctor details saved successfully.", this, "list-referral-doctors.aspx");
            else if (liRetVal == -1)
                Commonfunction.showMsg("Referal doctor code already exists.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Referal doctor name already exists.", this);
            else if (liRetVal == -3)
                Commonfunction.showMsg("Referal doctor Username already exists.", this);
            else if (liRetVal == -4)
                Commonfunction.showMsg("Referal doctor Email address already exists.", this);
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