using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_list_samples : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["pname"])
                && !string.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                int liRoleFlag = 0;

                liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

                if (liRoleFlag == 0) // if the role access is for the other users//
                {
                    if (!BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 25, "Add"))
                    {
                        lblMessage.Text = "You are not authorised to assign test barcodes.";
                        pnlMainContent.Visible = false;
                        pnlHeader.Visible = false;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        GetPatientTestsForBarCode(Convert.ToInt32(Request.QueryString["pid"]));
                        lblPatientName.Text = "Generate Barcodes for - " + Request.QueryString["pname"].Trim();
                    }
                }
                else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Referral doctor
                {
                    lblMessage.Text = "You are not authorised to assign test barcodes.";
                    pnlMainContent.Visible = false;
                    pnlHeader.Visible = false;
                    lblMessage.Visible = true;
                }

                //Disable to view mode if clicked on view baarcode link//
                if (Request.QueryString["flag"].Trim() == "2")
                {
                    foreach (RepeaterItem rptItem in rptPatientTests.Items)
                    {
                        TextBox txtBarCode = (TextBox)rptItem.FindControl("txtBarCode");
                        if (txtBarCode != null)
                        {
                            txtBarCode.Enabled = false;
                        }
                    }
                    btnSave.Enabled = false;
                    lblPatientName.Text = "Barcodes for - " + Request.QueryString["pname"].Trim();
                }
            }
        }
    }

    private void GetPatientTestsForBarCode(int fiPatientId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetPatientTestsForBarCode(fiPatientId);
            rptPatientTests.DataSource = lobjdtDetails;
            rptPatientTests.DataBind();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        bool lbValid = true;
        try
        {
            foreach (RepeaterItem rptItem in rptPatientTests.Items)
            {
                TextBox txtBarCode = (TextBox)rptItem.FindControl("txtBarCode");
                if (txtBarCode != null)
                {
                    if (string.IsNullOrEmpty(txtBarCode.Text.Trim()))
                    {
                        //Code to save the barcodes in DB//
                        lbValid = false;
                        break;
                    }
                }
            }
            if (lbValid)
            {
                foreach (RepeaterItem rptItem in rptPatientTests.Items)
                {
                    TextBox txtBarCode = (TextBox)rptItem.FindControl("txtBarCode");
                    Label lblTestId = (Label)rptItem.FindControl("lblTestId");

                    if (txtBarCode != null && lblTestId != null)
                        bllogic.InsertTestBarCodes(Convert.ToInt32(Request.QueryString["pid"]), Convert.ToInt32(lblTestId.Text.Trim()), txtBarCode.Text.Trim());
                }
                Commonfunction.showMsg("Test Barcodes saved successfully.", this, "test-sample-accept.aspx");
            }
            else
                Commonfunction.showMsg("Please enter barcodes for all tests.", this, "test-sample-accept.aspx");
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