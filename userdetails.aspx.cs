using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class userdetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["uid"]) && !string.IsNullOrEmpty(Request.QueryString["flag"]))
                GetCenterDetails(Convert.ToInt32(Request.QueryString["uid"]), Convert.ToInt32(Request.QueryString["flag"]));
        }
    }

    public void GetCenterDetails(int fiUserId, int fiFlag)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjData = new DataTable();
        try
        {
            if (fiFlag == 0)
                lobjData = bllogic.GetCollCenterDetails(fiUserId);

            else if (fiFlag == 1)
                lobjData = bllogic.GetSubCollCenterDetails(fiUserId);

            else if (fiFlag == 2)
                lobjData = bllogic.GetReferalDoctorDetails(fiUserId);

            else if (fiFlag == 3)
                lobjData = bllogic.GetUserDetails(fiUserId);

            if (lobjData.Rows.Count > 0)
            {
                lblUserName.Text = Convert.ToString(lobjData.Rows[0]["stUserName"]);
                lblPassword.Text = Convert.ToString(lobjData.Rows[0]["stPassword"]);
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
        finally
        {
            bllogic = null;
            lobjData = null;
        }
    }



}