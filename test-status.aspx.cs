using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class test_status : System.Web.UI.Page
{
    public bool lbUserRights = true;
    public int liRoleFlag = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
        if (!IsPostBack)
        {
            Commonfunction.GetAllTestStatus(ddlTestStatus);
            BusinessLogic.FillMainDepartments(ddlDepts);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 23, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view test status.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllPatients(0);
            }
            // if logged in as ColCenter OR SubCol center OR Referral doctor, do not allow to view patients//
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3)
            {
                lblMessage.Text = "You are not authorised to view test status.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetAllPatients(int fiRefDoctorId)
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

            lobjdtDetails = bllogic.GetAllPatients(lstrFromDate, lstrToDate, liDeptId, liTestStatusId, lstrPatientName, lstrRegNo, "", fiRefDoctorId,0);
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
        if (liRoleFlag == 0)
            GetAllPatients(0);
    }
}