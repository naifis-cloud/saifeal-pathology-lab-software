using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class dispatch_tests : System.Web.UI.Page
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
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 30, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view test dispatch page.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllPatients();
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Referral doctor
            {
                lblMessage.Text = "You are not authorised to view test dispatch page.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetAllPatients()
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetAllPatients();
    }

    public string ShowHide(int fiTestStatusId)
    {
        string lstrDisplayStyle = "";
        try
        {
            if (fiTestStatusId == 7 || fiTestStatusId == 6)
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

    public bool ShowHideButton(int fiTestStatusId)
    {
        bool lstrDisplayStyle = true;
        try
        {
            if (fiTestStatusId == 7 || fiTestStatusId == 6)
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

    protected void rptPatients_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        try
        {
            int liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (e.CommandName.Trim() == "eHandOver")
            {
                if (!BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 30, "Add"))
                    Commonfunction.showMsg("You are not authorised to dispatch the test reports.", this);

                else
                {
                    //Validation to check if the full payment is made. If yes, then only dispatch the report//
                    Label lblBalanceAmt = (Label)e.Item.FindControl("lblBalanceAmt");
                    TextBox txtHandOver = (TextBox)e.Item.FindControl("txtHandOver");
                    if (txtHandOver != null && lblBalanceAmt != null)
                    {
                        if (Convert.ToInt32(lblBalanceAmt.Text.Trim()) > 0)
                            Commonfunction.showMsg("Report cannot be dispatched before making full payment.", this);

                        else
                        {
                            if (!string.IsNullOrEmpty(txtHandOver.Text.Trim()))
                            {
                                int liRetVal = bllogic.UpdateDispatchDetails(Convert.ToInt32(e.CommandArgument), 6, txtHandOver.Text.Trim());
                                if (liRetVal > 0)
                                    Commonfunction.showMsg("Dispatch details saved successfully.", this);

                                GetAllPatients();
                            }
                            else
                                Commonfunction.showMsg("Enter Dispatch details in HandOver To box.", this);
                        }
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