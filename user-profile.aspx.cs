using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class user_profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            Commonfunction.GetAllGenders(ddlSex);
            Commonfunction.GetAllStates(ddlState);
            ddlCity.Items.Insert(0, new ListItem("--Select--", ""));
            GetUserDetails(Convert.ToInt32(Session["UserId"]));
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
                hidUserCode.Value = Convert.ToString(lobjdtDetails.Rows[0]["stCode"]);
                txtName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stName"]);
                hidRoleId.Value = Convert.ToString(lobjdtDetails.Rows[0]["inRoleId"]);
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
                    hidUserStatus.Value = "true";
                else
                    hidUserStatus.Value = "false";
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
            if (hidUserStatus.Value == "true")
                lbIsActive = true;
            else
                lbIsActive = false;

            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                liUserId = Convert.ToInt32(Session["UserId"]);

            liRetVal = blLogic.InsertUpdateUser(liUserId, hidUserCode.Value.Trim(), txtName.Text.Trim(), Convert.ToInt32(hidRoleId.Value),
                txtAddress.Text.Trim(), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue),
                txtPinCode.Text.Trim(), txtEmail.Text.Trim(), txtMobile.Text.Trim(), txtPhone.Text.Trim(), ddlSex.SelectedValue.Trim(),
                Convert.ToInt32(txtAge.Text.Trim()), txtQualification.Text.Trim(), txtUsername.Text.Trim(), txtpassword.Text.Trim(), lbIsActive);

            if (liRetVal > 0)
                Commonfunction.showMsg("User details saved successfully.", this, "lab-dashboard.aspx");
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