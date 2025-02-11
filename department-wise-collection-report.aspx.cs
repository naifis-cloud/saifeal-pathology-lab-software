using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Data;
using System.IO;
using CommonFunctions;

public partial class department_wise_collection_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;

            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 33, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view departwise collection report.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                {
                    BusinessLogic.FillMainDepartments(ddlDepts);
                    GetDeptWiseCollectionReport();
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3)
            {
                lblMessage.Text = "You are not authorised to view departwise collection report.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetDeptWiseCollectionReport()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        string lstrFromDate = "", lstrToDate = "";
        int liNoOfDays = 0, liDeptId = 0;
        try
        {
            if (!string.IsNullOrEmpty(Request.Form.Get(txtFromdate.UniqueID)))
                lstrFromDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(Request.Form.Get(txtFromdate.UniqueID), new System.Globalization.CultureInfo("en-GB")));

            if (!string.IsNullOrEmpty(Request.Form.Get(txtTodate.UniqueID)))
                lstrToDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(Request.Form.Get(txtTodate.UniqueID), new System.Globalization.CultureInfo("en-GB")));

            if (ddlDepts.SelectedIndex > 0)
                liDeptId = Convert.ToInt32(ddlDepts.SelectedValue);

            if (ddlNoOfDays.SelectedIndex > 0)
                liNoOfDays = Convert.ToInt32(ddlNoOfDays.SelectedValue);
            else
                liNoOfDays = -1;

            lobjdtDetails = bllogic.GetDeptWiseCollectionReport(lstrFromDate, lstrToDate, liDeptId, liNoOfDays);
            rptDeptWiseCollection.DataSource = lobjdtDetails;
            rptDeptWiseCollection.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptDeptWiseCollection.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptDeptWiseCollection.Visible = false;
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetDeptWiseCollectionReport();
    }

    private void ExportData()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=DeptWiseCollectionReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        rptDeptWiseCollection.RenderControl(hw);
        Response.Write("<table>");
        Response.Output.Write(sw.ToString());
        Response.Write("</table>");
        Response.Flush();
        Response.End();
    }

    private void ExportPDFData()
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=DeptWiseCollectionReport.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        pnlDeptWiseCollection.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }

    protected void lnkbtnExcel_Click(object sender, EventArgs e)
    {
        ExportData();
    }

    protected void lnkbtnPDF_Click(object sender, EventArgs e)
    {
        ExportPDFData();
    }
}