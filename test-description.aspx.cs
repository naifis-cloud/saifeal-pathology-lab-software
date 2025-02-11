using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_list_interpretation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                GetTestDetails(Convert.ToInt32(Request.QueryString["tid"]));
                GetTestDescription(Convert.ToInt32(Request.QueryString["tid"]));
            }
        }
    }

    private void GetTestDetails(int fiTestId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetTestDetails(fiTestId);
            if (lobjdtDetails.Rows.Count > 0)
                lblTestName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stTestName"]);
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
        int liRetVal = 0, liTestDescId = 0;
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["tid"].Trim()))
                liTestDescId = Convert.ToInt32(Request.QueryString["tid"].Trim());

            liRetVal = blLogic.InsertUpdateTestDescription(liTestDescId, Server.HtmlEncode(txtDescription.Text.Trim()));

            if (liRetVal > 0)
                Commonfunction.showMsg("Test Description saved successfully.", this, "list-tests.aspx");
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

    private void GetTestDescription(int fiTestId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetTestDescription(fiTestId);

            if (lobjdtDetails.Rows.Count > 0)
                txtDescription.Text = Server.HtmlDecode(Convert.ToString(lobjdtDetails.Rows[0]["stDescription"]));
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