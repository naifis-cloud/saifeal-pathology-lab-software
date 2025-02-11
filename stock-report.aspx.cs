using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class stock_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;

            BusinessLogic.FillAllSuppliers(ddlSupplier);

            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 41, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view stock report.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllItemsBySupplier(0);
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3)
            {
                lblMessage.Text = "You are not authorised to view stock report.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlSupplier.SelectedIndex > 0)
            GetAllItemsBySupplier(Convert.ToInt32(ddlSupplier.SelectedValue));
        else
            GetAllItemsBySupplier(0);
    }

    private void GetAllItemsBySupplier(int fiSupplierId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetItemsBySupplier(fiSupplierId);
            rptItems.DataSource = lobjdtDetails;
            rptItems.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptItems.Visible = true;
                trNoData.Visible = false;
                pnlDownload.Visible = true;
            }
            else
            {
                rptItems.Visible = false;
                trNoData.Visible = true;
                pnlDownload.Visible = false;
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

    private void ExportData()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=StockReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        rptItems.RenderControl(hw);
        Response.Write("<table>");
        Response.Output.Write(sw.ToString());
        Response.Write("</table>");
        Response.Flush();
        Response.End();
    }

    private void ExportPDFData()
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=StockReport.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        pnlStockReport.RenderControl(hw);
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