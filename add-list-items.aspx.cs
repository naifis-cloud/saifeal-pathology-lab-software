using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;

public partial class add_list_items : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            BusinessLogic.FillAllSuppliers(ddlSupplier);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                // if both add and view rights are not there//
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 39, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 39, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 39, "View");
                    if (lbUserRights)
                        GetAllItems();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view items.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidItemId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 39, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add an item.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                        else
                            txtCode.Text = Commonfunction.GenerateModuleCode("Items");
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view items.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidItemId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view items.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    private void GetItemDetails(int fiItemId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetItemDetails(fiItemId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidItemId.Value = Convert.ToString(fiItemId);
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stCode"]);
                txtName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stName"]);
                txtAddress.Text = Convert.ToString(lobjdtDetails.Rows[0]["stAddress"]);
                ddlSupplier.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inSupplierId"]);

                txtCategory.Text = Convert.ToString(lobjdtDetails.Rows[0]["stCategory"]);
                txtBrand.Text = Convert.ToString(lobjdtDetails.Rows[0]["stBrand"]);
                txtQuantity.Text = Convert.ToString(lobjdtDetails.Rows[0]["inQuantity"]);

                if (Convert.ToBoolean(lobjdtDetails.Rows[0]["flgStatus"]) == true)
                    rbtnPerish.Checked = true;
                else
                    rbtnNonPerish.Checked = true;
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
        int liRetVal = 0, liItemId = 0;
        bool lbIsActive = true;
        try
        {
            if (!string.IsNullOrEmpty(hidItemId.Value.Trim()))
                liItemId = Convert.ToInt32(hidItemId.Value.Trim());

            if (rbtnNonPerish.Checked)
                lbIsActive = false;

            liRetVal = blLogic.InsertUpdateItem(liItemId, txtCode.Text.Trim(), txtName.Text.Trim(), txtAddress.Text.Trim(),
                Convert.ToInt32(ddlSupplier.SelectedValue), txtCategory.Text.Trim(), txtBrand.Text.Trim(),
                Convert.ToInt32(txtQuantity.Text.Trim()), lbIsActive);

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Item details saved successfully.", this);
                GetAllItems();
                txtCode.Text = "";
                txtName.Text = "";
                txtAddress.Text = "";
                ddlSupplier.SelectedIndex = 0;
                txtCategory.Text = "";
                txtBrand.Text = "";
                txtQuantity.Text = "";
                rbtnPerish.Checked = true;
                rbtnNonPerish.Checked = false;
                hidItemId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 39, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 39, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add an item.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 39, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 39, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view items.";
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

    private void GetAllItems()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllItems(0, 0, "I.dtCreationDate DESC", "");
            rptItems.DataSource = lobjdtDetails;
            rptItems.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptItems.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptItems.Visible = false;
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


    protected void rptItems_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0, liRoleFlag = 0;
        try
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 39, "Edit"))
                    Commonfunction.showMsg("You are not authorised to edit an item.", this);
                else
                {
                    GetItemDetails(Convert.ToInt32(e.CommandArgument));
                    lblAddMessage.Text = "";
                    pnlAddContent.Visible = true;
                    lblAddMessage.Visible = false;
                }
            }
            else if (e.CommandName == "eDelete")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 39, "Delete"))
                    Commonfunction.showMsg("You are not authorised to delete an item.", this);
                else
                {
                    liRetVal = bllogic.DeleteItem(Convert.ToInt32(e.CommandArgument));

                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Item deleted successfully.", this);
                        GetAllItems();
                        txtCode.Text = "";
                        txtName.Text = "";
                        txtAddress.Text = "";
                        ddlSupplier.SelectedIndex = 0;
                        txtCategory.Text = "";
                        txtBrand.Text = "";
                        txtQuantity.Text = "";
                        rbtnPerish.Checked = true;
                        rbtnNonPerish.Checked = false;
                        hidItemId.Value = "";
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