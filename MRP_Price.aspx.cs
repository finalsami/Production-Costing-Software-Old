using BusinessAccessLayer;
using DataAccessLayer;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class MRP_Price : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            //GetUserRights();
            if (!IsPostBack)
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();

                Grid_MRPPriceList();
                DisplayView();
            }
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[40]["GroupId"]);
            lblCanView.Text = Convert.ToBoolean(dtMenuList.Rows[40]["CanView"]).ToString();
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[40]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[40]["CanEdit"]) == true)
            {

            }

        }

        public void GetLoginDetails()
        {
            if (Session["UserName"] != null)
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }
        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[40]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[40]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[40]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                //if (lblCompanyFactoryExpence_Id.Text != "")
                //{
                //    AddComp_FactoryExpenceBtn.Visible = false;
                //    UpdateComp_FactoryExpence.Visible = true;
                //    ReportView.Visible = true;
                //}
                //else
                //{
                //    AddComp_FactoryExpenceBtn.Visible = true;
                //    UpdateComp_FactoryExpence.Visible = false;

                //    ReportView.Visible = true;
                //}

            }
            else
            {
                //AddComp_FactoryExpenceBtn.Visible = false;
                //UpdateComp_FactoryExpence.Visible = false;

                //ReportView.Visible = false;
            }

        }

        public void Grid_MRPPriceList()
        {
            ClsMRPPrice cls = new ClsMRPPrice();
            DataTable dtMRP = new DataTable();
            dtMRP = cls.Get_MRPPrice_Grid();

            //DataTable dtCheck = new DataTable();
            //dtCheck = cls.CHECK_MRP_Price();
            //if (dtCheck.Rows.Count > 0)
            //{
            //    Grid_MRPPrice.DataSource = dtCheck;
            //    Grid_MRPPrice.DataBind();
            //    foreach (GridViewRow row2 in Grid_MRPPrice.Rows)
            //    {
                
            //        lblIsMasterPacking.Text = (row2.FindControl("lblIsMasterPacking") as Label).Text;

            //        if (lblIsMasterPacking.Text == "True")
            //        {
                
            //            row2.BackColor = System.Drawing.Color.Cornsilk;
            //            row2.Font.Bold = true;
            //        }

            //    }
            //}
       
                Grid_MRPPrice.DataSource = dtMRP;
                Grid_MRPPrice.DataBind();
                foreach (GridViewRow row2 in Grid_MRPPrice.Rows)
                {

                    lblIsMasterPacking.Text = (row2.FindControl("lblIsMasterPacking") as Label).Text;

                    if (lblIsMasterPacking.Text == "True")
                    {

                        row2.BackColor = System.Drawing.Color.Cornsilk;
                        row2.Font.Bold = true;
                    }

                
            }


        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            ProMRPPrice pro = new ProMRPPrice();
            ClsMRPPrice cls = new ClsMRPPrice();

            int status = 0;



            DataTable dtMRP = new DataTable();
            dtMRP = cls.CHECK_MRP_Price();
            if (dtMRP.Rows.Count > 0)
            {
                foreach (GridViewRow row1 in Grid_MRPPrice.Rows)
                {

                    pro.BPM_Id = (Convert.ToInt32((row1.FindControl("lblFk_BPM_Id") as Label).Text));
                    pro.PackMeasurement = (Convert.ToInt32((row1.FindControl("lblPackingMeasurement") as Label).Text));
                    pro.Packing_Size = (Convert.ToDecimal((row1.FindControl("lblPacking_Size") as Label).Text));
                    pro.PMRM_Category_Id = (Convert.ToInt32((row1.FindControl("lblPM_RM_Category_Id") as Label).Text));

                    pro.SuggestedRPLPrice = (Convert.ToDecimal((row1.FindControl("lblSuggestedRPLPrice") as Label).Text));
                    pro.SuggestedRPLPriceGST = (Convert.ToDecimal((row1.FindControl("lblSuggestedRPLPriceGST") as Label).Text));
                    pro.Percent_Margin_MRP = (Convert.ToDecimal((row1.FindControl("Percent_Of_Margin_MRPtxt") as TextBox).Text));
                    pro.Suggested_MRP = (Convert.ToDecimal((row1.FindControl("lblSuggested_MRP") as Label).Text));
                    pro.Final_MRP = (Convert.ToDecimal((row1.FindControl("Final_MRPtxt") as TextBox).Text));
                    pro.GP = (Convert.ToDecimal((row1.FindControl("Gptxt") as TextBox).Text));
                    pro.Agrostar = (Convert.ToDecimal((row1.FindControl("Agrostartxt") as TextBox).Text));
                    pro.Gramofone = (Convert.ToDecimal((row1.FindControl("Gramofonetxt") as TextBox).Text));
                    pro.MPPL = (Convert.ToDecimal((row1.FindControl("MPPLtxt") as TextBox).Text));
                    pro.Dehaat = (Convert.ToDecimal((row1.FindControl("Dehaattxt") as TextBox).Text));


                    status = cls.UPDATE_MRP_Price(pro);

                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated!')", true);
                Grid_MRPPriceList();

            }
            else
            {
                foreach (GridViewRow row2 in Grid_MRPPrice.Rows)
                {
                    pro.BPM_Id = (Convert.ToInt32((row2.FindControl("lblFk_BPM_Id") as Label).Text));
                    pro.PackMeasurement = (Convert.ToInt32((row2.FindControl("lblPackingMeasurement") as Label).Text));
                    pro.Packing_Size = (Convert.ToDecimal((row2.FindControl("lblPacking_Size") as Label).Text));
                    pro.PMRM_Category_Id = (Convert.ToInt32((row2.FindControl("lblPM_RM_Category_Id") as Label).Text));

                    pro.SuggestedRPLPrice = (Convert.ToDecimal((row2.FindControl("lblSuggestedRPLPrice") as Label).Text));
                    pro.SuggestedRPLPriceGST = (Convert.ToDecimal((row2.FindControl("lblSuggestedRPLPriceGST") as Label).Text));

                    pro.Percent_Margin_MRP = (Convert.ToDecimal((row2.FindControl("Percent_Of_Margin_MRPtxt") as TextBox).Text));
                    pro.Suggested_MRP = (Convert.ToDecimal((row2.FindControl("lblSuggested_MRP") as Label).Text));
                    pro.Final_MRP = (Convert.ToDecimal((row2.FindControl("Final_MRPtxt") as TextBox).Text));
                    pro.GP = (Convert.ToDecimal((row2.FindControl("Gptxt") as TextBox).Text));
                    pro.Agrostar = (Convert.ToDecimal((row2.FindControl("Agrostartxt") as TextBox).Text));
                    pro.Gramofone = (Convert.ToDecimal((row2.FindControl("Gramofonetxt") as TextBox).Text));
                    pro.MPPL = (Convert.ToDecimal((row2.FindControl("MPPLtxt") as TextBox).Text));
                    pro.Dehaat = (Convert.ToDecimal((row2.FindControl("Dehaattxt") as TextBox).Text));
                    status = cls.INSERT_MRP_Price(pro);

                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted!')", true);
                Grid_MRPPriceList();

            }
        }

        protected void Grid_MRPPrice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                lblDynamicColumnCount.Text = (e.Row.Cells.Count).ToString();

            }

            for (int rowIndex = Grid_MRPPrice.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_MRPPrice.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_MRPPrice.Rows[rowIndex + 1];
                for (int cellCount = 0; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    Label lblPrevStatus = gvPreviousRow.FindControl("lblBPMName") as Label;
                    Label lblStatus = gvRow.FindControl("lblBPMName") as Label;
                    if (lblPrevStatus.Text == lblStatus.Text)
                    {
                        if (gvPreviousRow.Cells[1].RowSpan < 2)
                        {

                            gvRow.Cells[1].RowSpan = 2;
                            gvRow.Cells[17].RowSpan = 2;
                        }
                        else
                        {

                            gvRow.Cells[1].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                            gvRow.Cells[17].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                        }

                        gvPreviousRow.Cells[1].Visible = false;
                        gvPreviousRow.Cells[17].Visible = false;
                    }
                }
            }

        }

        protected void Final_MRPtxt_TextChanged(object sender, EventArgs e)
        {
            decimal Suggested_MRP = 0;



            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            Suggested_MRP = (Convert.ToDecimal((gvr.FindControl("Final_MRPtxt") as TextBox).Text));
            (gvr.FindControl("Gptxt") as TextBox).Text = (gvr.FindControl("Final_MRPtxt") as TextBox).Text;
            (gvr.FindControl("Agrostartxt") as TextBox).Text = (gvr.FindControl("Final_MRPtxt") as TextBox).Text;
            (gvr.FindControl("Gramofonetxt") as TextBox).Text = (gvr.FindControl("Final_MRPtxt") as TextBox).Text;
            (gvr.FindControl("MPPLtxt") as TextBox).Text = (gvr.FindControl("Final_MRPtxt") as TextBox).Text;
            (gvr.FindControl("Dehaattxt") as TextBox).Text = (gvr.FindControl("Final_MRPtxt") as TextBox).Text;

        }

        protected void Percent_Of_Margin_MRPtxt_TextChanged(object sender, EventArgs e)
        {
            //decimal SuggestedRPLPrice = 0;
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            decimal Percent_MRP = (Convert.ToDecimal((gvr.FindControl("Percent_Of_Margin_MRPtxt") as TextBox).Text));
            lblIsMasterPacking.Text = (gvr.FindControl("lblIsMasterPacking") as Label).Text;
            bool MasterPack= Convert.ToBoolean(lblIsMasterPacking.Text);
            int BPM_Id = (Convert.ToInt32((gvr.FindControl("lblFk_BPM_Id") as Label).Text));
            //SuggestedRPLPrice = (Convert.ToDecimal((gvr.FindControl("lblSuggestedRPLPrice") as Label).Text));
            //(gvr.FindControl("lblSuggested_MRP") as Label).Text = (((SuggestedRPLPrice * Percent_MRP) / 100) + SuggestedRPLPrice).ToString("0.00");
            if (MasterPack == true)
            {
                foreach (GridViewRow row1 in Grid_MRPPrice.Rows)
                {
                    int BPM = (Convert.ToInt32((row1.FindControl("lblFk_BPM_Id") as Label).Text));
                    if (BPM_Id == (Convert.ToInt32((row1.FindControl("lblFk_BPM_Id") as Label).Text)))
                    {
                        (row1.FindControl("Percent_Of_Margin_MRPtxt") as TextBox).Text = (gvr.FindControl("Percent_Of_Margin_MRPtxt") as TextBox).Text;
                        //(row1.FindControl("lblSuggested_MRP") as Label).Text = (((SuggestedRPLPrice * Percent_MRP) / 100) + SuggestedRPLPrice).ToString("0.00");
                        (row1.FindControl("lblSuggested_MRP") as Label).Text = ((Convert.ToDecimal((row1.FindControl("lblSuggestedRPLPriceGST") as Label).Text) * Convert.ToDecimal((row1.FindControl("Percent_Of_Margin_MRPtxt") as TextBox).Text) / 100) + (Convert.ToDecimal((row1.FindControl("lblSuggestedRPLPrice") as Label).Text))).ToString("0.00");
                    }
                }

            }
            else
            {
                int BPM = (Convert.ToInt32((gvr.FindControl("lblFk_BPM_Id") as Label).Text));
                if (BPM_Id == (Convert.ToInt32((gvr.FindControl("lblFk_BPM_Id") as Label).Text)))
                {
                    (gvr.FindControl("Percent_Of_Margin_MRPtxt") as TextBox).Text = (gvr.FindControl("Percent_Of_Margin_MRPtxt") as TextBox).Text;
                    //(row1.FindControl("lblSuggested_MRP") as Label).Text = (((SuggestedRPLPrice * Percent_MRP) / 100) + SuggestedRPLPrice).ToString("0.00");
                    (gvr.FindControl("lblSuggested_MRP") as Label).Text = ((Convert.ToDecimal((gvr.FindControl("lblSuggestedRPLPriceGST") as Label).Text) * Convert.ToDecimal((gvr.FindControl("Percent_Of_Margin_MRPtxt") as TextBox).Text) / 100) + (Convert.ToDecimal((gvr.FindControl("lblSuggestedRPLPrice") as Label).Text))).ToString("0.00");
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        [Obsolete]
        private void ExportGridToPDF()
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("BulkProductName"), new DataColumn("Packing_Size"), new DataColumn("GP"),
                new DataColumn("Agrostar"), new DataColumn("Gramofone"), new DataColumn("MPPL") , new DataColumn("Dehaat") });

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                string BPM_Id = "";
                string TitleHeader = "MRP Price List on" + DateTime.Now;
                string BulkProductName = "";
                string PackingSize = "";
                string GP = "";
                string Agrostar = "";
                string Gramofone = "";
                string MPPL = "";
                string Dehaat = "";
                foreach (GridViewRow row in Grid_MRPPrice.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        row.Visible = (row.FindControl("CheckBox_Check") as CheckBox).Checked;
                        string NewBPM_Id = (row.FindControl("lblFk_BPM_Id") as Label).Text;

                        CheckBox chkRow = (row.FindControl("CheckBox_Check") as CheckBox);

                        if (chkRow.Checked || NewBPM_Id == BPM_Id)
                        {

                            BulkProductName = (row.Cells[1].FindControl("lblBPMName") as Label).Text;
                            PackingSize = (row.Cells[2].FindControl("lblPackingSize") as Label).Text;
                            GP = (row.Cells[3].FindControl("Gptxt") as TextBox).Text;
                            Agrostar = (row.Cells[4].FindControl("Agrostartxt") as TextBox).Text;
                            Gramofone = (row.Cells[5].FindControl("Gramofonetxt") as TextBox).Text;
                            MPPL = (row.Cells[6].FindControl("MPPLtxt") as TextBox).Text;
                            Dehaat = (row.Cells[7].FindControl("Dehaattxt") as TextBox).Text;
                            dt.Rows.Add(BulkProductName, PackingSize, GP, Agrostar, Gramofone, MPPL, Dehaat);
                            BPM_Id = (row.FindControl("lblFk_BPM_Id") as Label).Text; ;
                        }
                    }
                }
                dt.Rows.Add(BulkProductName, PackingSize, GP, Agrostar, Gramofone, MPPL, Dehaat);

                MRP_Report.DataSource = dt;
                MRP_Report.DataBind();
                MRP_Report.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A2, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.AddHeader("Testing Header", "Testing Demo");
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=PriceListGPEstimate [" + DateTime.Now.ToShortDateString() + "].pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();

            }
        }

        [Obsolete]
        protected void btnMRP_Report_Click(object sender, EventArgs e)
        {

            ExportGridToPDF();
        }

        public void GetReport()
        {
            ClsMRPPrice cls = new ClsMRPPrice();
            DataTable dtCheck = new DataTable();
            dtCheck = cls.CHECK_MRP_Price();
            if (dtCheck.Rows.Count > 0)
            {


            }
        }

        protected void CheckBox_Check_CheckedChanged(object sender, EventArgs e)
        {
            bool CheckBoxChanged = false;
            foreach (GridViewRow row1 in Grid_MRPPrice.Rows)
            {
                CheckBoxChanged = (row1.FindControl("CheckBox_Check") as CheckBox).Checked;
            }
        }

        protected void MRP_Report_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                lblDynamicColumnCount.Text = (e.Row.Cells.Count).ToString();

            }

            for (int rowIndex = MRP_Report.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = MRP_Report.Rows[rowIndex];
                GridViewRow gvPreviousRow = MRP_Report.Rows[rowIndex + 1];
                for (int cellCount = 0; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    Label lblPrevStatus = gvPreviousRow.FindControl("lblBPMName") as Label;
                    Label lblStatus = gvRow.FindControl("lblBPMName") as Label;
                    if (lblPrevStatus.Text == lblStatus.Text)
                    {
                        if (gvPreviousRow.Cells[0].RowSpan < 2)
                        {

                            gvRow.Cells[0].RowSpan = 2;
                        }
                        else
                        {

                            gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                        }

                        gvPreviousRow.Cells[0].Visible = false;
                    }
                }
            }

        }
    }
}