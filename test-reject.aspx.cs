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
        BusinessLogic blLogic = new BusinessLogic();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["pname"])
               && !string.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                lblPatientName.Text = Request.QueryString["pname"].Trim();

                //Displaying the rejection reason//
                if (Request.QueryString["flag"].Trim() == "2")
                {
                    DataTable lobjdtPatientDetails = new DataTable();
                    lobjdtPatientDetails = blLogic.GetPatientDetails(Convert.ToInt32(Request.QueryString["pid"]));
                    if (lobjdtPatientDetails.Rows.Count > 0)
                    {
                        txtReason.Text = Convert.ToString(lobjdtPatientDetails.Rows[0]["stRejectReason"]);
                        txtReason.Enabled = false;
                        btnReject.Enabled = false;
                    }
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        try
        {
            if (!BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 25, "Add"))
                Commonfunction.showMsg("You are not authorised to reject test sample(s).", this, "test-sample-accept.aspx");
            else
            {
                //deleting all the test barcodes of the patient first and then updating the status as 'Rejected'
                bllogic.DeleteTestBarCodes(Convert.ToInt32(Request.QueryString["pid"]));

                int liCountCheck = bllogic.UpdateRejectionDetails(Convert.ToInt32(Request.QueryString["pid"]), 9, txtReason.Text.Trim());
                if (liCountCheck > 0)
                    Commonfunction.showMsg("Test sample(s) rejected successfully.", this, "test-sample-accept.aspx");
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