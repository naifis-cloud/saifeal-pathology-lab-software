using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;

public partial class test_payment_receipts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;

            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            BusinessLogic.FillMainDepartments(ddlDepts);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 28, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view payment receipts page.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllPatients("");
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Referral doctor
            {
                lblMessage.Text = "You are not authorised to view payment receipts page.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetAllPatients(string fstrBillStatus)
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

            lobjdtDetails = bllogic.GetAllPatients(lstrFromDate, lstrToDate, liDeptId, liTestStatusId, lstrPatientName, lstrRegNo, fstrBillStatus, 0, 0);
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

    public string GetPaymentStatusCSS(int fiBillStatusId)
    {
        string lstrTestCSS = "";

        if (fiBillStatusId == 1)
            lstrTestCSS = "label bg-red";

        else if (fiBillStatusId == 2)
            lstrTestCSS = "label bg-orange";

        else if (fiBillStatusId == 3)
            lstrTestCSS = "label bg-green";

        return lstrTestCSS;
    }

    protected void btnUnpaid_Click(object sender, EventArgs e)
    {
        GetAllPatients("UnPaid");
    }

    protected void btnPartialPaid_Click(object sender, EventArgs e)
    {
        GetAllPatients("Partially Paid");
    }

    protected void btnFullyPaid_Click(object sender, EventArgs e)
    {
        GetAllPatients("Paid");
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        GetAllPatients("");
    }

    protected void rptPatients_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        int liRoleFlag = 0;
        liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
        try
        {
            if (e.CommandName == "eUpdate")
            {
                if (!BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 28, "Add"))
                    Commonfunction.showMsg("You are not authorised to add payments.", this);
                else
                {
                    TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
                    Label lblPatientName = (Label)e.Item.FindControl("lblPatientName");
                    Label lblBalanceAmt = (Label)e.Item.FindControl("lblBalanceAmt");
                    if (txtAmount != null && lblPatientName != null && lblBalanceAmt != null)
                    {
                        if (!string.IsNullOrEmpty(txtAmount.Text.Trim()) && Convert.ToInt32(txtAmount.Text.Trim()) > 0)
                        {
                            //if amount being paid is less than or equal to balance amount left
                            if (Convert.ToInt32(txtAmount.Text.Trim()) <= Convert.ToInt32(lblBalanceAmt.Text.Trim()))
                            {
                                blLogic.InsertUpdatePayment(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(txtAmount.Text.Trim()), "BP", 0);
                                Commonfunction.showMsg("Payment updated successfully.", this);
                            }
                            else
                                Commonfunction.showMsg("Amount entered should not be greater than balance amount.", this);
                        }
                        else
                            Commonfunction.showMsg("Enter valid amount for " + lblPatientName.Text.Trim(), this);
                    }
                    GetAllPatients("");
                }
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

    protected void rptPatients_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblBillStatusId = (Label)e.Item.FindControl("lblBillStatusId");
            TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
            Button btnUpdate = (Button)e.Item.FindControl("btnUpdate");

            if (lblBillStatusId != null && txtAmount != null && btnUpdate != null)
            {
                if (Convert.ToInt32(lblBillStatusId.Text.Trim()) == 3)
                {
                    txtAmount.Enabled = false;
                    btnUpdate.Enabled = false;
                }
            }
        }
    }
}