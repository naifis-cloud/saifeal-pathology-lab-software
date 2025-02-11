using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;
using AjaxControlToolkit;

public partial class add_list_suppliers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            Commonfunction.GetAllPaymentTypes(ddlPaymentMode);
            Commonfunction.GetAllStates(ddlState);
            ddlCity.Items.Insert(0, new ListItem("--Select--", ""));

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                // if both add and view rights are not there//
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 40, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 40, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 40, "View");
                    if (lbUserRights)
                        GetAllSuppliers();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view suppliers.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidSupplierId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 40, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add a supplier.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                        else
                            txtCode.Text = Commonfunction.GenerateModuleCode("Suppliers");
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view suppliers.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidSupplierId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view suppliers.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetAllCities();   
    }

    private void GetAllCities()
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

    private void GetSupplierDetails(int fiSupplierId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetSupplierDetails(fiSupplierId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidSupplierId.Value = Convert.ToString(fiSupplierId);
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stCode"]);
                txtName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stName"]);
                txtAddress.Text = Convert.ToString(lobjdtDetails.Rows[0]["stAddress"]);
                ddlState.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inStateId"]);
                GetAllCities();
                ddlCity.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inCityId"]);
                txtPinCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stPinCode"]);
                txtEmail.Text = Convert.ToString(lobjdtDetails.Rows[0]["stEmail"]);
                txtMobile.Text = Convert.ToString(lobjdtDetails.Rows[0]["stMobile"]);
                txtPhone.Text = Convert.ToString(lobjdtDetails.Rows[0]["stPhone"]);
                txtCSTNo.Text = Convert.ToString(lobjdtDetails.Rows[0]["stCSTNo"]);
                txtTIN.Text = Convert.ToString(lobjdtDetails.Rows[0]["stTINNo"]);
                ddlSupplierType.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["stType"]);
                ddlPaymentMode.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["stPaymentMode"]);

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
        int liRetVal = 0, liSupplierId = 0;
        bool lbIsActive = true;
        try
        {
            if (!string.IsNullOrEmpty(hidSupplierId.Value.Trim()))
                liSupplierId = Convert.ToInt32(hidSupplierId.Value.Trim());

            if (rbtnInactive.Checked)
                lbIsActive = false;

            liRetVal = blLogic.InsertUpdateSupplier(liSupplierId, txtCode.Text.Trim(), txtName.Text.Trim(), txtAddress.Text.Trim(),
                Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue), txtPinCode.Text.Trim(), txtEmail.Text.Trim(),
                txtMobile.Text.Trim(), txtPhone.Text.Trim(), txtCSTNo.Text.Trim(), txtTIN.Text.Trim(), ddlSupplierType.SelectedValue.Trim(),
                ddlPaymentMode.SelectedValue.Trim(), lbIsActive);

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Supplier details saved successfully.", this);
                GetAllSuppliers();
                txtCode.Text = "";
                txtCode.Text = "";
                txtName.Text = "";
                txtAddress.Text = "";
                ddlState.SelectedIndex = 0;
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, new ListItem("--Select--", ""));
                txtPinCode.Text = "";
                txtEmail.Text = "";
                txtMobile.Text = "";
                txtPhone.Text = "";
                txtCSTNo.Text = "";
                txtTIN.Text = "";
                ddlSupplierType.SelectedIndex = 0;
                ddlPaymentMode.SelectedIndex = 0;
                rbtnActive.Checked = true;
                rbtnInactive.Checked = false;
                hidSupplierId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 40, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 40, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add a supplier.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 40, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 40, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view suppliers.";
                    pnlViewContent.Visible = false;
                    lblViewMessage.Visible = true;
                }
            }
            else if (liRetVal == -1)
                Commonfunction.showMsg("Code already exists.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Name already exists.", this);
            else
                Commonfunction.showMsg("Error occurred while saving data!.", this);
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

    private void GetAllSuppliers()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllSuppliers(0, 0, "SP.dtCreationDate DESC", "");
            rptSuppliers.DataSource = lobjdtDetails;
            rptSuppliers.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptSuppliers.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptSuppliers.Visible = false;
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


    protected void rptSuppliers_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0, liRoleFlag = 0;
        try
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 40, "Edit"))
                    Commonfunction.showMsg("You are not authorised to edit a supplier.", this);
                else
                {
                    GetSupplierDetails(Convert.ToInt32(e.CommandArgument));
                    lblAddMessage.Text = "";
                    pnlAddContent.Visible = true;
                    lblAddMessage.Visible = false;
                }
            }
            else if (e.CommandName == "eDelete")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 40, "Delete"))
                    Commonfunction.showMsg("You are not authorised to delete a supplier.", this);
                else
                {
                    liRetVal = bllogic.DeleteSupplier(Convert.ToInt32(e.CommandArgument));

                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Supplier deleted successfully.", this);
                        GetAllSuppliers();
                        txtCode.Text = "";
                        txtCode.Text = "";
                        txtName.Text = "";
                        txtAddress.Text = "";
                        ddlState.SelectedIndex = 0;
                        ddlCity.Items.Clear();
                        ddlCity.Items.Insert(0, new ListItem("--Select--", ""));
                        txtPinCode.Text = "";
                        txtEmail.Text = "";
                        txtMobile.Text = "";
                        txtPhone.Text = "";
                        txtCSTNo.Text = "";
                        txtTIN.Text = "";
                        ddlSupplierType.SelectedIndex = 0;
                        ddlPaymentMode.SelectedIndex = 0;
                        rbtnActive.Checked = true;
                        rbtnInactive.Checked = false;
                        hidSupplierId.Value = "";
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