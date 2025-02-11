using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_list_interpretation : System.Web.UI.Page
{
    bool lbUserRights = true;
    int liRoleFlag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            BusinessLogic.FillAllTests(ddlTests);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                // if both add and view rights are not there//
                if (!BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 11, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 11, "Edit"))
                {
                    lblMainMessage.Text = "You are not authorised to add/edit test interpretations.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                lblMainMessage.Text = "You are not authorised to add/edit test interpretations.";
                pnlMainContent.Visible = false;
                lblMainMessage.Visible = true;
            }
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        int liRetVal = 0, liTestInterPretId = 0;
        try
        {
            //if (!string.IsNullOrEmpty(hidTestInterId.Value.Trim()))
            //    liTestInterPretId = Convert.ToInt32(hidTestInterId.Value.Trim());

            liRetVal = blLogic.InsertUpdateTestInterpretation(Convert.ToInt32(ddlTests.SelectedValue), Server.HtmlEncode(txtInterpretation.Text.Trim()));

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Test Interpretation saved successfully.", this);
                //GetAllTestInterPrets();
                ddlTests.SelectedIndex = 0;
                txtInterpretation.Text = "";
                hidTestInterId.Value = "";
            }
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

    private void GetTestAbbrevDetails(int fiTestId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetTestInterPretDetails(fiTestId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidTestInterId.Value = Convert.ToString(fiTestId);
                //ddlTests.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inTestId"]);
                txtInterpretation.Text = Server.HtmlDecode(Convert.ToString(lobjdtDetails.Rows[0]["stInterPretText"]));
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

    protected void ddlTests_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTests.SelectedIndex > 0)
            GetTestAbbrevDetails(Convert.ToInt32(ddlTests.SelectedValue));
        else
            txtInterpretation.Text = "";
    }
}