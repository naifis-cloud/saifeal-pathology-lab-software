using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;

public partial class add_rate_to_rate_list_collectionlab : System.Web.UI.Page
{
    bool lbUserRights = true;
    int liRoleFlag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            BusinessLogic.FillCollectionRateLists(ddlRateList);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 22, "View");

                if (!lbUserRights)
                {
                    lblMainMessage.Text = "You are not authorised to view rates assignment for Collection Lab Rate List.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3)
            {
                lblMainMessage.Text = "You are not authorised to view rates assignment for Collection Lab Rate List.";
                pnlMainContent.Visible = false;
                lblMainMessage.Visible = true;
            }
        }
    }

    private void GetAllCollectionLabRates(int fiRateListId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetCollectionLabRates(fiRateListId);
            rptCollRates.DataSource = lobjdtDetails;
            rptCollRates.DataBind();

            hidRecords.Value = Convert.ToString(rptCollRates.Items.Count);

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptCollRates.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptCollRates.Visible = false;
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

    protected void btnAssignRates_Click(object sender, EventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        bool IsDataValid = true;
        try
        {
            if (liRoleFlag == 0 && (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 22, "Add")
                || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 22, "Edit")))
            {

                //Deleting all the current rates if exists//
                bllogic.DeleteCollectionLabRates(Convert.ToInt32(ddlRateList.SelectedValue));

                foreach (RepeaterItem rptItem in rptCollRates.Items)
                {
                    Label lblTestId = (Label)rptItem.FindControl("lblTestId");
                    TextBox txtRate = (TextBox)rptItem.FindControl("txtRate");
                    Label lblTestName = (Label)rptItem.FindControl("lblTestName");

                    if (lblTestId != null && txtRate != null && lblTestName != null)
                    {
                        if (Convert.ToInt32(txtRate.Text.Trim()) != 0)
                            bllogic.InsertCollectionLabRates(Convert.ToInt32(ddlRateList.SelectedValue), Convert.ToInt32(lblTestId.Text.Trim()),
                                Convert.ToInt32(txtRate.Text.Trim()));
                        else
                        {
                            Commonfunction.showMsg("Rate for " + lblTestName.Text.Trim() + " cannot be 0.", this);
                            IsDataValid = false;
                            break;
                        }
                    }
                }

                if (IsDataValid)
                    Commonfunction.showMsg("Rates asigned successfully.", this, "add-rate-to-rate-list-collectionlab.aspx");
            }
            else
                Commonfunction.showMsg("You ar not authorised to assign rates to Collection Lab Rate List.", this);
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

    protected void rptCollRates_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TextBox txtRate = (TextBox)e.Item.FindControl("txtRate");
            Label lblTestName = (Label)e.Item.FindControl("lblTestName");

            RequiredFieldValidator reqvalRate = (RequiredFieldValidator)e.Item.FindControl("reqvalRate");

            if (txtRate != null && reqvalRate != null && lblTestName != null)
            {
                reqvalRate.ControlToValidate = "txtRate";
                reqvalRate.ValidationGroup = "vgAssignRates";
                reqvalRate.ErrorMessage = "Enter rate for " + lblTestName.Text.Trim();
            }
        }
    }
    protected void ddlRateList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRateList.SelectedIndex > 0)
            GetAllCollectionLabRates(Convert.ToInt32(ddlRateList.SelectedValue));
        else
        {
            trNoData.Visible = true;
            rptCollRates.Visible = false;
        }
    }
}