using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;

            Commonfunction.GetAllGenders(ddlSex);
            BusinessLogic.FillAllRoles(ddlRoles);
            Commonfunction.GetAllStates(ddlState);
            ddlCity.Items.Insert(0, new ListItem("--Select--", ""));

            if (ddlRoles.Items.FindByText("Collection Center") != null || ddlRoles.Items.FindByText("CollectionCenter") != null)
            {
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("Collection Center"));
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("CollectionCenter"));
            }
            if (ddlRoles.Items.FindByText("SubCollection Center") != null || ddlRoles.Items.FindByText("SubCollectionCenter") != null)
            {
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("SubCollection Center"));
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("SubCollectionCenter"));
            }
            if (ddlRoles.Items.FindByText("Referral Doctor") != null || ddlRoles.Items.FindByText("Referral Doctor") != null
                || ddlRoles.Items.FindByText("Doctor") != null)
            {
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("Referral Doctor"));
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("ReferralDoctor"));
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("Doctor"));
            }

            int liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 35, "Edit");
                    if (lbUserRights)
                        GetUserDetails(Convert.ToInt32(Request.QueryString["uid"]));
                }
                else
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 35, "Add");
                    if(lbUserRights)
                        txtCode.Text = Commonfunction.GenerateModuleCode("Users");
                }

                if (!lbUserRights)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
                        lblMessage.Text = "You are not authorised to edit a user.";
                    else
                        lblMessage.Text = "You are not authorised to add a user.";

                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as ColCenter OR SubCol center OR Referral doctor
            {
                lblMessage.Text = "You are not authorised to add a user.";
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
    }

    private void GetUserDetails(int fiUserId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetUserDetails(fiUserId);
            if (lobjdtDetails.Rows.Count > 0)
            {
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stCode"]);
                txtName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stName"]);
                ddlRoles.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inRoleId"]);
                txtAddress.Text = Convert.ToString(lobjdtDetails.Rows[0]["stAddress"]);

                if (ddlState.Items.FindByValue(Convert.ToString(lobjdtDetails.Rows[0]["inStateId"])) != null)
                {
                    ddlState.Items.FindByValue(Convert.ToString(lobjdtDetails.Rows[0]["inStateId"])).Selected = true;
                    Commonfunction.GetCities(ddlCity, Convert.ToInt32(ddlState.SelectedValue));

                    if (ddlCity.Items.FindByValue(Convert.ToString(lobjdtDetails.Rows[0]["inCityId"])) != null)
                        ddlCity.Items.FindByValue(Convert.ToString(lobjdtDetails.Rows[0]["inCityId"])).Selected = true;
                }
                txtPinCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stPinCode"]);
                txtEmail.Text = Convert.ToString(lobjdtDetails.Rows[0]["stEmail"]);
                txtMobile.Text = Convert.ToString(lobjdtDetails.Rows[0]["stMobileNo"]);
                txtPhone.Text = Convert.ToString(lobjdtDetails.Rows[0]["stPhoneNo"]);
                ddlSex.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["stSex"]);
                txtAge.Text = Convert.ToString(lobjdtDetails.Rows[0]["inAge"]);
                txtQualification.Text = Convert.ToString(lobjdtDetails.Rows[0]["stQualification"]);
                txtUsername.Text = Convert.ToString(lobjdtDetails.Rows[0]["stUserName"]);
                txtpassword.Attributes.Add("value", Convert.ToString(lobjdtDetails.Rows[0]["stPassword"]));

                if (Convert.ToBoolean(lobjdtDetails.Rows[0]["flgStatus"]) == true)
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
        BusinessLogic blLogic = new BusinessLogic();
        int liRetVal = 0, liUserId = 0;
        bool lbIsActive = true;
        try
        {
            if (rbtnInactive.Checked)
                lbIsActive = false;

            if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
                liUserId = Convert.ToInt32(Request.QueryString["uid"]);

            liRetVal = blLogic.InsertUpdateUser(liUserId, txtCode.Text.Trim(), txtName.Text.Trim(), Convert.ToInt32(ddlRoles.SelectedValue),
                txtAddress.Text.Trim(), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue),
                txtPinCode.Text.Trim(), txtEmail.Text.Trim(), txtMobile.Text.Trim(), txtPhone.Text.Trim(), ddlSex.SelectedValue.Trim(),
                Convert.ToInt32(txtAge.Text.Trim()), txtQualification.Text.Trim(), txtUsername.Text.Trim(), txtpassword.Text.Trim(), lbIsActive);

            if (liRetVal > 0)
                Commonfunction.showMsg("User details saved successfully.", this, "list-users.aspx");
            else if (liRetVal == -1)
                Commonfunction.showMsg("Code already exists.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Username already exists.", this);
            else if (liRetVal == -3)
                Commonfunction.showMsg("Email address already exists.", this);
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
}