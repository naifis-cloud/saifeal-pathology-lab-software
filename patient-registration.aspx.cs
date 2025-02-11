using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using CommonFunctions;

public partial class patient_registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;

            Commonfunction.GetAllTitles(ddlTitle);
            Commonfunction.GetAllGenders(ddlGender);
            Commonfunction.GetAllPaymentTypes(ddlPayType);
            Commonfunction.GetAllTestStatus(ddlTestStatus);
            BusinessLogic.FillRefDoctors(ddlRefDoctors);
            BusinessLogic.FillMainDepartments(ddlDepts);
            BusinessLogic.FillAllTests(lstTests);

            //Filling the Discount percentage dropdown//
            for (int liCounter = 0; liCounter <= 100; liCounter++)
            {
                ddlDiscount.Items.Add(new ListItem(Convert.ToString(liCounter) + "%", Convert.ToString(liCounter)));
            }

            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 23, "Edit");
                    if (lbUserRights)
                        GetPatientDetails(Convert.ToInt32(Request.QueryString["pid"]));
                }
                else
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 23, "Add");

                if (!lbUserRights)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                        lblMessage.Text = "You are not authorised to edit patient details.";
                    else
                        lblMessage.Text = "You are not authorised to register a patient.";

                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            //else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as ColCenter OR SubCol center OR Referral doctor
            //{
            //    lblMessage.Text = "You are not authorised to register a patient.";
            //    pnlMainContent.Visible = false;
            //    lblMessage.Visible = true;
            //}
            else if (liRoleFlag == 3) // if logged in as ColCenter OR SubCol center OR Referral doctor
            {
                lblMessage.Text = "You are not authorised to register a patient.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }

            if (string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                ddlTestStatus.SelectedValue = "1";

                if (ddlPayType.Items.FindByValue("C") != null)
                    ddlPayType.Items.FindByValue("C").Selected = true;

                txtRegNo.Text = Commonfunction.GenerateModuleCode("Patients");
            }
        }
    }

    protected void btnAddTest_Click(object sender, EventArgs e)
    {
        ArrayList arraylist1 = new ArrayList();
        try
        {
            if (lstTests.SelectedIndex >= 0)
            {
                //Adding the selected items in an array//
                for (int i = 0; i < lstTests.Items.Count; i++)
                {
                    if (lstTests.Items[i].Selected)
                    {
                        if (!arraylist1.Contains(lstTests.Items[i]))
                            arraylist1.Add(lstTests.Items[i]);
                    }
                }

                //Adding the items in second listbox from an array//
                for (int i = 0; i < arraylist1.Count; i++)
                {
                    if (!lstSelectedTests.Items.Contains(((ListItem)arraylist1[i])))
                        lstSelectedTests.Items.Add(((ListItem)arraylist1[i]));

                    lstTests.Items.Remove(((ListItem)arraylist1[i]));
                }
                lstSelectedTests.SelectedIndex = -1;
                CalCulateTestPrice();
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
    }

    protected void btnRemoveTest_Click(object sender, EventArgs e)
    {
        ArrayList arraylist2 = new ArrayList();
        try
        {
            if (lstSelectedTests.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstSelectedTests.Items.Count; i++)
                {
                    if (lstSelectedTests.Items[i].Selected)
                    {
                        if (!arraylist2.Contains(lstSelectedTests.Items[i]))
                        {
                            arraylist2.Add(lstSelectedTests.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arraylist2.Count; i++)
                {
                    if (!lstTests.Items.Contains(((ListItem)arraylist2[i])))
                    {
                        lstTests.Items.Add(((ListItem)arraylist2[i]));
                    }
                    lstSelectedTests.Items.Remove(((ListItem)arraylist2[i]));
                }
                lstTests.SelectedIndex = -1;
                CalCulateTestPrice();
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
    }

    //Calculating the test rates based on the number of tests selected 

    private void CalCulateTestPrice()
    {
        int liTotalPrice = 0, inTestPrice = 0;
        BusinessLogic blLogic = new BusinessLogic();

        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["RoleFlag"])))
            {
                foreach (ListItem lstItem in lstSelectedTests.Items)
                {
                    //Getting the rate of a particular test based on the user type //
                    inTestPrice = blLogic.GetTestRate(Convert.ToInt32(lstItem.Value), Convert.ToInt32(Session["RoleFlag"]),
                        Convert.ToInt32(Session["UserId"]));

                    liTotalPrice += inTestPrice;
                }
                txtTotal.Text = Convert.ToString(liTotalPrice);
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

    //private void CalCulateTestPrice()
    //{
    //    int liTotalPrice = 0;
    //    BusinessLogic blLogic = new BusinessLogic();
    //    DataTable lobjdtData = new DataTable();
    //    try
    //    {
    //        foreach (ListItem lstItem in lstSelectedTests.Items)
    //        {
    //            lobjdtData = blLogic.GetTestDetails(Convert.ToInt32(lstItem.Value));
    //            if (lobjdtData.Rows.Count > 0)
    //                liTotalPrice += Convert.ToInt32(lobjdtData.Rows[0]["inTestRate"]);
    //        }
    //        txtTotal.Text = Convert.ToString(liTotalPrice);
    //    }
    //    catch (Exception foException)
    //    {
    //        throw new Exception(foException.Message);
    //    }
    //    finally
    //    {
    //        blLogic = null;
    //        lobjdtData = null;
    //    }
    //}

    private void GetPatientDetails(int fiPatientId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtData = new DataTable();
        try
        {
            lobjdtData = bllogic.GetPatientDetails(fiPatientId);
            if (lobjdtData.Rows.Count > 0)
            {
                txtRegNo.Text = Convert.ToString(lobjdtData.Rows[0]["stRegNo"]);
                ddlType.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["stType"]);
                txtEmail.Text = Convert.ToString(lobjdtData.Rows[0]["stEmail"]);
                ddlTitle.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["stTitle"]);
                txtPatientName.Text = Convert.ToString(lobjdtData.Rows[0]["stPatientName"]);

                if (ddlGender.Items.FindByText(Convert.ToString(lobjdtData.Rows[0]["stGender"])) != null)
                    ddlGender.Items.FindByText(Convert.ToString(lobjdtData.Rows[0]["stGender"])).Selected = true;

                txtAge.Text = Convert.ToString(lobjdtData.Rows[0]["inAge"]);
                txtMobile.Text = Convert.ToString(lobjdtData.Rows[0]["stMobile"]);
                txtSampleTime.Text = Convert.ToString(lobjdtData.Rows[0]["stSampleTime"]);
                txtPartner.Text = Convert.ToString(lobjdtData.Rows[0]["stRefByPartner"]);
                txtAddress.Text = Convert.ToString(lobjdtData.Rows[0]["stAddress"]);
                txtHistory.Text = Convert.ToString(lobjdtData.Rows[0]["stClinicHistory"]);
                txtRemarks.Text = Convert.ToString(lobjdtData.Rows[0]["stRemarks"]);

                if (Convert.ToInt32(lobjdtData.Rows[0]["inRefDoctorId"]) != 0)
                {
                    ddlRefDoctors.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["inRefDoctorId"]);
                    txtRefDoctorName.Enabled = false;
                    //reqvalDoctors.Enabled = true;
                    //reqvalOtherRefDoctor.Enabled = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(lobjdtData.Rows[0]["stOtherRefDoctor"])))
                    {
                        ddlRefDoctors.SelectedValue = "Other";
                        txtRefDoctorName.Enabled = true;
                        txtRefDoctorName.Text = Convert.ToString(lobjdtData.Rows[0]["stOtherRefDoctor"]);
                        //reqvalDoctors.Enabled = false;
                        //reqvalOtherRefDoctor.Enabled = true;
                    }
                }

                ddlDepts.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["inMaindeptId"]);
                txtVIALId.Text = Convert.ToString(lobjdtData.Rows[0]["stVialID"]);
                txtVisit.Text = Convert.ToString(lobjdtData.Rows[0]["inVisitChgs"]);
                txtEmergency.Text = Convert.ToString(lobjdtData.Rows[0]["inEmergencyChgs"]);
                txtOtherChgs.Text = Convert.ToString(lobjdtData.Rows[0]["inOtherChgs"]);
                txtTotal.Text = Convert.ToString(lobjdtData.Rows[0]["inTotalTestChgs"]);
                ddlPayType.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["stPayType"]);
                ddlDiscount.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["inDiscountPerc"]);
                txtDiscAmount.Text = Convert.ToString(lobjdtData.Rows[0]["inDiscountAmt"]);
                txtNetAmount.Text = Convert.ToString(lobjdtData.Rows[0]["inNetAmt"]);
                txtAdvanceAmt.Text = Convert.ToString(lobjdtData.Rows[0]["inAdvanceAmt"]);
                txtBalanceAmt.Text = Convert.ToString(lobjdtData.Rows[0]["inBalanceAmt"]);
                ddlTestStatus.SelectedValue = Convert.ToString(lobjdtData.Rows[0]["inTestStatusId"]);

                //Getting patient tests//
                //Getting the selected tests//

                lobjdtData = new DataTable();

                lobjdtData = bllogic.GetPatientTests(fiPatientId);
                if (lobjdtData.Rows.Count > 0)
                {
                    //Adding the items in second listbox from an array//
                    foreach (DataRow lodtRow in lobjdtData.Rows)
                    {
                        ListItem lstItem = new ListItem();
                        lstItem.Value = Convert.ToString(lodtRow["inTestId"]);
                        lstItem.Text = Convert.ToString(lodtRow["stTestCode"]) + " - " + Convert.ToString(lodtRow["stTestName"]);
                        lstSelectedTests.Items.Add(lstItem);

                        lstTests.Items.Remove(lstItem);
                    }
                    lstSelectedTests.SelectedIndex = -1;
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
            lobjdtData = null;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0, liPatientId = 0, liRefDoctorId = 0, liVisitChgs = 0, liEmergencyChgs = 0, liOtherChgs = 0,
        liDiscAmt = 0, liAdvanceAmt = 0, liAge = 0;
        try
        {
            if (Convert.ToInt32(Request.Form.Get(txtTotal.UniqueID)) != 0 && Convert.ToInt32(Request.Form.Get(txtNetAmount.UniqueID)) != 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                    liPatientId = Convert.ToInt32(Request.QueryString["pid"]);

                if (ddlRefDoctors.SelectedIndex != 0 && ddlRefDoctors.SelectedItem.Text.Trim() != "Other")
                    liRefDoctorId = Convert.ToInt32(ddlRefDoctors.SelectedValue);

                if (!string.IsNullOrEmpty(txtVisit.Text.Trim()))
                    liVisitChgs = Convert.ToInt32(txtVisit.Text.Trim());

                if (!string.IsNullOrEmpty(txtEmergency.Text.Trim()))
                    liEmergencyChgs = Convert.ToInt32(txtEmergency.Text.Trim());

                if (!string.IsNullOrEmpty(txtOtherChgs.Text.Trim()))
                    liOtherChgs = Convert.ToInt32(txtOtherChgs.Text.Trim());

                if (!string.IsNullOrEmpty(txtOtherChgs.Text.Trim()))
                    liOtherChgs = Convert.ToInt32(txtOtherChgs.Text.Trim());

                if (!string.IsNullOrEmpty(txtDiscAmount.Text.Trim()))
                    liDiscAmt = Convert.ToInt32(txtDiscAmount.Text.Trim());

                if (!string.IsNullOrEmpty(txtAdvanceAmt.Text.Trim()))
                    liAdvanceAmt = Convert.ToInt32(txtAdvanceAmt.Text.Trim());

                if (!string.IsNullOrEmpty(txtAge.Text.Trim()))
                    liAge = Convert.ToInt32(txtAge.Text.Trim());

                liRetVal = bllogic.InsertUpdatePatient(liPatientId, txtRegNo.Text.Trim(), ddlType.SelectedItem.Text.Trim(), txtEmail.Text.Trim(), ddlTitle.SelectedItem.Text.Trim(),
                    txtPatientName.Text.Trim(), ddlGender.SelectedItem.Text.Trim(), liAge, txtMobile.Text.Trim(), txtSampleTime.Text.Trim(),
                    txtPartner.Text.Trim(), txtAddress.Text.Trim(), txtHistory.Text.Trim(), txtRemarks.Text.Trim(), liRefDoctorId, txtRefDoctorName.Text.Trim(),
                    Convert.ToInt32(ddlDepts.SelectedValue), txtVIALId.Text.Trim(), liVisitChgs, liEmergencyChgs,
                    liOtherChgs, Convert.ToInt32(Request.Form.Get(txtTotal.UniqueID)), ddlPayType.SelectedItem.Text.Trim(), Convert.ToInt32(ddlDiscount.SelectedValue),
                    liDiscAmt, Convert.ToInt32(Request.Form.Get(txtNetAmount.UniqueID)), liAdvanceAmt, Convert.ToInt32(Request.Form.Get(txtBalanceAmt.UniqueID)),
                    Convert.ToInt32(ddlTestStatus.SelectedValue));

                if (liRetVal > 0)
                {
                    //Inserting into payments table if any advance payment is made//

                    if (string.IsNullOrEmpty(Request.QueryString["pid"]))
                        bllogic.InsertUpdatePayment(liRetVal, liAdvanceAmt, "AP", 0);
                    else
                        bllogic.InsertUpdatePayment(liRetVal, liAdvanceAmt, "AP", 1);


                    //Deleting and adding the patient's selected tests//
                    bllogic.DeletePatientTests(liRetVal);

                    foreach (ListItem lstItem in lstSelectedTests.Items)
                    {
                        bllogic.InsertPatientTests(liRetVal, Convert.ToInt32(lstItem.Value));
                    }
                    Commonfunction.showMsg("Patient details saved successfully.", this, "test-status.aspx");
                }

                else if (liRetVal == -1)
                {
                    Commonfunction.showMsg("Patient email already exists.", this);
                    this.ClientScript.RegisterStartupScript(this.GetType(), "key1", "<script>CalculateNetAmount()</script>");
                }
                else if (liRetVal == -2)
                {
                    Commonfunction.showMsg("Patient VialID already exists.", this);
                    this.ClientScript.RegisterStartupScript(this.GetType(), "key1", "<script>CalculateNetAmount()</script>");
                }
                else
                {
                    Commonfunction.showMsg("Error occurred while saving data!.", this);
                    this.ClientScript.RegisterStartupScript(this.GetType(), "key1", "<script>CalculateNetAmount()</script>");
                }
            }
            else
            {
                Commonfunction.showMsg("Net Amount/Total Tests Amount cannot be 0.", this);
                this.ClientScript.RegisterStartupScript(this.GetType(), "key1", "<script>CalculateNetAmount()</script>");
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