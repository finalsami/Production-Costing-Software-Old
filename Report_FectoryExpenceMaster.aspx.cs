using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace Production_Costing_Software
{
    public partial class Report_FectoryExpenceMaster : System.Web.UI.Page
    {
        int User_Id;
        String CompanyName;
        int Company_Id;
        Comp_ProCoWiseFactoryExpenceMaster pro = new Comp_ProCoWiseFactoryExpenceMaster();
        Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            if (!IsPostBack)
            {
                lblCompany_Id.Text = Session["Company_Id"].ToString().ToUpper();
                Grid_CompanyFactoryExpenceAllReportData();
            }
        }

        public void Grid_CompanyFactoryExpenceAllReportData()
        {
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            Comp_ProCoWiseFactoryExpenceMaster pro = new Comp_ProCoWiseFactoryExpenceMaster();
            pro.Fk_CompanyList_Id = Convert.ToInt32(lblCompany_Id.Text);
            DataTable dt = new DataTable();
            dt = cls.Comp_Get_BulkProdcutOfCompanyFectoryExpenceReport(pro);

            BulkProductListbox.DataSource = dt;
            BulkProductListbox.DataTextField = "BPM_Product_Name";
            BulkProductListbox.DataValueField = "Fk_BPM_Id";
            BulkProductListbox.DataBind();
            BulkProductListbox.Items.Insert(0, "Select");

            Grid_CompanyFactoryExpenceAllReport.DataSource = cls.Comp_Get_CompanyFectoryExpenceAllReport(pro);
            Grid_CompanyFactoryExpenceAllReport.DataBind();
        }
        public void GetLoginDetails()
        {

            User_Id = Convert.ToInt32(Session["UserId"]);
            if (Session["CompanyMasterList_Name"]!=null)
            {
                CompanyName = Session["CompanyMasterList_Name"].ToString();
                Company_Id = Convert.ToInt32(Session["Company_Id"]);
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        protected void ExcelReport_Click(object sender, EventArgs e)
        {
            ExportGridToExcel(Grid_CompanyFactoryExpenceAllReport);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        private void ExportGridToExcel(Control GridView)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CompanyFactoryExpenceReport.xls"));
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Grid_CompanyFactoryExpenceAllReport.AllowPaging = false;
            //Grid_comp_FactoryExpenceListData();
            //Change the Header Row back to white color
            Grid_CompanyFactoryExpenceAllReport.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < Grid_CompanyFactoryExpenceAllReport.HeaderRow.Cells.Count; i++)
            {
                Grid_CompanyFactoryExpenceAllReport.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            Grid_CompanyFactoryExpenceAllReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }

        [Obsolete]
        private void ExportGridToPDF()
        {

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    Grid_CompanyFactoryExpenceAllReport.AllowPaging = false;
                    //this.Comp_Get_CompanyFectoryExpenceAllReport();

                    Grid_CompanyFactoryExpenceAllReport.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=FectoryExpence.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }

        [Obsolete]
        protected void PdfReport_Click(object sender, EventArgs e)
        {
            ExportGridToPDF();
        }

     



        protected void ChkBulkSubmit_Click(object sender, EventArgs e)
        {
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            Comp_ProCoWiseFactoryExpenceMaster pro = new Comp_ProCoWiseFactoryExpenceMaster();
            pro.Fk_CompanyList_Id = Convert.ToInt32(lblCompany_Id.Text);
            string YrStrList = "";
            foreach (System.Web.UI.WebControls.ListItem listItem in BulkProductListbox.Items)
            {
                if (listItem.Selected)
                {


                    YrStrList = YrStrList + listItem.Value + ",";

                }

            }
            string IsMasterPAck = "0";
            if (MasterPackingAndAllDropdownList.SelectedValue == "0")
            {
                IsMasterPAck = "0";
            }
            else
            {
                IsMasterPAck = "1";
            }
            if (YrStrList == "" || YrStrList == "Select,")
            {
                YrStrList = "0";
            }
            else
            {
                YrStrList = YrStrList.Remove(YrStrList.Length - 1);

            }
            string sBulkListStr = YrStrList;
    

            Grid_CompanyFactoryExpenceAllReport.DataSource = cls.Comp_Get_CompanyFectoryExpenceMasterByBulkCheckBoxList(pro, sBulkListStr, IsMasterPAck);
            Grid_CompanyFactoryExpenceAllReport.DataBind();
        }

        protected void MasterPackingAndAllDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            Comp_ProCoWiseFactoryExpenceMaster pro = new Comp_ProCoWiseFactoryExpenceMaster();
            pro.Fk_CompanyList_Id = Convert.ToInt32(lblCompany_Id.Text);
            DataTable dt = new DataTable();
            dt = cls.Comp_Get_BulkProdcutOfCompanyFectoryExpenceReport(pro);

            if (MasterPackingAndAllDropdownList.SelectedValue == "0")
            {
                Grid_CompanyFactoryExpenceAllReport.DataSource = cls.Comp_Get_CompanyFectoryExpenceAllReport(pro);
                Grid_CompanyFactoryExpenceAllReport.DataBind();

            }
            else
            {
                Grid_CompanyFactoryExpenceAllReport.DataSource = cls.Comp_Get_CompanyFectoryExpenceMasterOnlyMasterPack(pro);
                Grid_CompanyFactoryExpenceAllReport.DataBind();

            }

        }
    }
}