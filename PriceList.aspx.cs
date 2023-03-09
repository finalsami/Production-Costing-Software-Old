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
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class PriceList : System.Web.UI.Page
    {
        int User_Id;
        int Company_Id;
        //string StrBPM_Id;
        //string StrPMRM_Category_Id;
        //string StrTradeName_Id;
        //string TradeName_Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            GetUserRights();
            if (!IsPostBack)
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();

                Grid_PriceList();
                CompanyListDropDownListCombo();
                DisplayView();


            }
        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[33]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[33]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[33]["CanDelete"]).ToString();
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
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[33]["GroupId"]);

            if (Convert.ToBoolean(dtMenuList.Rows[33]["CanEdit"]) == true)
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
        public void GetLoginDetails()
        {
            if (Session["UserName"] != null && Session["UserName"].ToString().ToUpper() != "")
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                User_Id = Convert.ToInt32(Session["UserId"].ToString());
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void Grid_PriceList()
        {
            ClsPriceList clsPrice = new ClsPriceList();
            DataTable dtComp = new DataTable();
            //dt = cls.Get_AllPriceListGrid();
            //Grid_PriceListDataGP.DataSource = dt;
            //Grid_PriceListDataGP.DataBind();


            if (lblCompanyMasterList_Id.Text == "1" || lblCompanyMasterList_Id.Text == "0" || lblCompanyMasterList_Id.Text == "")
            {
                CompanyDropdownList.SelectedIndex = 0;

                dtComp = clsPrice.Get_AllPriceListGridByCompany(1);

                Grid_PriceListDataGP.DataSource = dtComp;
                Grid_PriceListDataGP.DataBind();
                Grid_PriceListDataOtherCompany.Visible = false;
                Grid_PriceListDataGP.Visible = true;

            }
            else
            {

                Grid_PriceListDataOtherCompany.Visible = true;
                Grid_PriceListDataGP.Visible = false;
                dtComp = clsPrice.Get_AllPriceListGridByOtherCompany(Convert.ToInt32(lblCompanyMasterList_Id.Text));

                Grid_PriceListDataOtherCompany.DataSource = dtComp;
                Grid_PriceListDataOtherCompany.DataBind();
            }
        }

        private byte[] exportpdf(DataTable dtEmployee)
        {

            // creating document object  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
            rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
            Document doc = new Document(rec);
            doc.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            //Creating paragraph for header  
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.BLUE);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_LEFT;
            prgHeading.Add(new Chunk("Dynamic Report PDF".ToUpper(), fntHead));
            doc.Add(prgHeading);

            //Adding paragraph for report generated by  
            Paragraph prgGeneratedBY = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.BLUE);
            prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
            //prgGeneratedBY.Add(new Chunk("Report Generated by : ASPArticles", fntAuthor));  
            //prgGeneratedBY.Add(new Chunk("\nGenerated Date : " + DateTime.Now.ToShortDateString(), fntAuthor));  
            doc.Add(prgGeneratedBY);

            //Adding a line  
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Add(p);

            //Adding line break  
            doc.Add(new Chunk("\n", fntHead));

            //Adding  PdfPTable  
            PdfPTable table = new PdfPTable(dtEmployee.Columns.Count);

            for (int i = 0; i < dtEmployee.Columns.Count; i++)
            {
                string cellText = Server.HtmlDecode(dtEmployee.Columns[i].ColumnName);
                PdfPCell cell = new PdfPCell();
                cell.Phrase = new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, 1, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#C8C8C8"));
                //cell.Phrase = new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 10, 1, new BaseColor(grdStudent.HeaderStyle.ForeColor)));  
                //cell.BackgroundColor = new BaseColor(grdStudent.HeaderStyle.BackColor);  
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.PaddingBottom = 5;
                table.AddCell(cell);
            }

            //writing table Data  
            for (int i = 0; i < dtEmployee.Rows.Count; i++)
            {
                for (int j = 0; j < dtEmployee.Columns.Count; j++)
                {
                    table.AddCell(dtEmployee.Rows[i][j].ToString());
                }
            }

            doc.Add(table);
            doc.Close();

            byte[] result = ms.ToArray();
            return result;

        }

        protected void ViewReport_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Report')", true);

        }

        protected void PrintReport_Click(object sender, EventArgs e)
        {
            Button Report = sender as Button;
            GridViewRow gdrow = Report.NamingContainer as GridViewRow;
            string MergePriceList_State = "";
            string MergePriceList_Company = "";
            //string MergeData = (MergePriceList_State.IndexOf("(")).ToString();
            DataTable dtPriceList = new DataTable();
            ClsCompanyMaster cls = new ClsCompanyMaster();
            ProCompanyMaster pro = new ProCompanyMaster();
            if (lblCompanyMasterList_Id.Text == "1" || lblCompanyMasterList_Id.Text == "0" || lblCompanyMasterList_Id.Text == "")
            {
                MergePriceList_State = hfData.Value;

                lblPriceListName.Text = MergePriceList_State.Substring(0, MergePriceList_State.IndexOf('('));
                lblState_Id.Text = MergePriceList_State.Split('(', ')')[1];
                dtPriceList = cls.FinalPRiceListGP(lblPriceListName.Text, Convert.ToInt32(lblState_Id.Text));
                PriceList_Report.DataSource = dtPriceList;
                PriceList_Report.DataBind();
            }
            else
            {
                MergePriceList_Company = hfData.Value;
                lblPriceListName.Text = MergePriceList_Company.Substring(0, MergePriceList_Company.IndexOf('('));
                lblCompanyMasterList_Id.Text = MergePriceList_Company.Split('(', ')')[1];

                dtPriceList = cls.FinalPRiceListOtherCompany(lblPriceListName.Text, Convert.ToInt32(lblCompanyMasterList_Id.Text));
                PriceList_Report_OtherCompany.DataSource = dtPriceList;
                PriceList_Report_OtherCompany.DataBind();
            }
            if (!Directory.Exists(Server.MapPath("~/TempUse")))
            {
                Directory.CreateDirectory(Server.MapPath("~/TempUse"));
            }
         
            if (File.Exists(Server.MapPath("~/TempUse/PriceList.pdf")))
            {
                File.Delete(Server.MapPath("~/TempUse/PriceList.pdf"));
            }
            if (lblCompanyMasterList_Id.Text == "1" || lblCompanyMasterList_Id.Text == "0" || lblCompanyMasterList_Id.Text == "")
            {
                using (var stream = new FileStream(Server.MapPath("~/TempUse/PriceList.pdf"), FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 5, 5, 15, 5);
                    PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
                    document.Open();
                    iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    iTextSharp.text.Font font10Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10, 1);
                    iTextSharp.text.Font font14Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 14, 1);

                    PdfPTable tableHeader = new PdfPTable(2);
                    float[] Headerwidths = new float[] { 50f, 50f };
                    tableHeader.SetWidths(Headerwidths);
                    tableHeader.WidthPercentage = 100;
                    PdfPCell cell = new PdfPCell(new Phrase("Price List Report of " + dtPriceList.Rows[0]["CompanyName"], font14Bold));
                    cell.HorizontalAlignment = 1;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    cell.PaddingBottom = 10;
                    tableHeader.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Price list Name : " + dtPriceList.Rows[0]["PriceListName"], font10Bold));
                    cell.HorizontalAlignment = 3;
                    cell.Border = 0;
                    cell.PaddingBottom = 10;
                    tableHeader.AddCell(cell);

                    cell = new PdfPCell(new Phrase("State : " + dtPriceList.Rows[0]["StateName"], font10Bold));
                    cell.HorizontalAlignment = 2;
                    cell.Border = 0;
                    cell.PaddingBottom = 10;
                    tableHeader.AddCell(cell);

                    cell = new PdfPCell(new Phrase("As on date	 : " + dtPriceList.Rows[0]["AsOnDate"], font10Bold));
                    cell.HorizontalAlignment = 3;
                    cell.Border = 0;
                    cell.PaddingBottom = 10;
                    tableHeader.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font10Bold));
                    cell.HorizontalAlignment = 1;
                    cell.Border = 0;
                    cell.PaddingBottom = 10;
                    tableHeader.AddCell(cell);

                    PdfPTable table = new PdfPTable(11);
                    float[] widths = new float[] { 4, 20, 12, 8, 8, 8, 8, 8, 8, 8, 8 };

                    table.SetWidths(widths);

                    table.WidthPercentage = 100;
                    cell = new PdfPCell(new Phrase("No", font10Bold));
                    cell.Rowspan = 2;
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = ((int)VerticalAlign.Middle);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Product Name with Technical Name", font10Bold));
                    cell.Rowspan = 2;
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Category Name", font10Bold));
                    cell.Rowspan = 2;
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Packing Size", font10Bold));
                    cell.Rowspan = 2;
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Phrase("NCR", font10Bold));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("RPL", font10Bold));
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("MRP", font10Bold));
                    cell.Rowspan = 2;
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    table.AddCell(cell);

                    table.AddCell(new Phrase("Price/ L or KG", font10Bold));
                    table.AddCell(new Phrase("Price Per Unit", font10Bold));
                    table.AddCell(new Phrase("Unit Price with GST", font10Bold));
                    table.AddCell(new Phrase("Price/ L or KG", font10Bold));
                    table.AddCell(new Phrase("Price Per Unit", font10Bold));
                    table.AddCell(new Phrase("Unit Price with GST", font10Bold));

                    int iCounter = 1;
                    int fk_BPM_Id = 0;
                    foreach (DataRow dr in dtPriceList.Rows)
                    {
                        int Currentfk_BPM_Id = Convert.ToInt32(dr["fk_BPM_Id"].ToString());
                        if (fk_BPM_Id != Currentfk_BPM_Id)
                        {
                            fk_BPM_Id = Convert.ToInt32(dr["fk_BPM_Id"].ToString());
                            int Count = dtPriceList.AsEnumerable().Where(r => r.Field<int>("fk_BPM_Id") == Convert.ToInt32(dr["fk_BPM_Id"].ToString())).Count();

                            cell = new PdfPCell(new Phrase(iCounter.ToString(), font5));
                            cell.Rowspan = Count;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(dr["BPM_Product_Name"].ToString(), font5));
                            cell.Rowspan = Count;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(dr["ProductCategoryName"].ToString(), font5));
                            cell.Rowspan = Count;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            table.AddCell(cell);
                            iCounter++;
                        }
                        table.AddCell(new Phrase(dr["PackMeasure"].ToString(), font5));

                        table.AddCell(new Phrase(dr["NCR"].ToString().Split('|')[0], font5));
                        table.AddCell(new Phrase(dr["NCR"].ToString().Split('|')[1], font5));
                        table.AddCell(new Phrase(dr["NCR"].ToString().Split('|')[2], font5));
                        table.AddCell(new Phrase(dr["RPL"].ToString().Split('|')[0], font5));
                        table.AddCell(new Phrase(dr["RPL"].ToString().Split('|')[1], font5));
                        table.AddCell(new Phrase(dr["RPL"].ToString().Split('|')[2], font5));

                        table.AddCell(new Phrase(dr["MRP"].ToString(), font5));

                    }



                    //PdfPTable tableFooter = new PdfPTable(1);
                    //float[] Footerwidths = new float[] {5f};

                    //tableFooter.TotalWidth = document.PageSize.Width - document.LeftMargin;
                    //tableFooter.AddCell("Page");
                    //tableFooter.WriteSelectedRows(0, -1, document.LeftMargin + 50, document.PageSize.Height - 30, writer.DirectContent);
                    //cell.Border = 0;
                    //tableFooter.AddCell(cell);

                    document.Add(tableHeader);
                    //document.Add(tableFooter);
                    document.Add(table);
                    document.Close();

                }
            }
            else
            {
                using (var stream = new FileStream(Server.MapPath("~/TempUse/PriceList.pdf"), FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 5, 5, 15, 5);
                    PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
                    document.Open();
                    iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    iTextSharp.text.Font font10Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10, 1);
                    iTextSharp.text.Font font14Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 14, 1);

                    PdfPTable tableHeader = new PdfPTable(2);
                    float[] Headerwidths = new float[] { 50f, 50f };
                    tableHeader.SetWidths(Headerwidths);
                    tableHeader.WidthPercentage = 100;
                    PdfPCell cell = new PdfPCell(new Phrase("Price List Report of " + dtPriceList.Rows[0]["CompanyName"], font14Bold));
                    cell.HorizontalAlignment = 1;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    cell.PaddingBottom = 10;
                    tableHeader.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Price list Name : " + dtPriceList.Rows[0]["PriceListName"], font10Bold));
                    cell.HorizontalAlignment = 3;
                    cell.Border = 0;
                    cell.PaddingBottom = 10;
                    tableHeader.AddCell(cell);

                    //cell = new PdfPCell(new Phrase("State : " + dtPriceList.Rows[0]["StateName"], font10Bold));
                    //cell.HorizontalAlignment = 2;
                    //cell.Border = 0;
                    //cell.PaddingBottom = 10;
                    //tableHeader.AddCell(cell);

                    cell = new PdfPCell(new Phrase("As on date	 : " + dtPriceList.Rows[0]["AsOnDate"], font10Bold));
                    cell.HorizontalAlignment = 3;
                    cell.Border = 0;
                    cell.PaddingBottom = 10;
                    tableHeader.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font10Bold));
                    cell.HorizontalAlignment = 1;
                    cell.Border = 0;
                    cell.PaddingBottom = 10;
                    tableHeader.AddCell(cell);

                    PdfPTable table = new PdfPTable(8);
                    float[] widths = new float[] { 4, 20, 12, 8, 8, 8, 8, 8 };

                    table.SetWidths(widths);

                    table.WidthPercentage = 100;
                    cell = new PdfPCell(new Phrase("No", font10Bold));

                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = ((int)VerticalAlign.Middle);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Product Name with Technical Name", font10Bold));
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Category Name", font10Bold));

                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Packing Size", font10Bold));

                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    table.AddCell(cell);



                    table.AddCell(new Phrase("Price/ L or KG", font10Bold));
                    table.AddCell(new Phrase("Price Per Unit", font10Bold));
                    table.AddCell(new Phrase("Unit Price with GST", font10Bold));

                    cell = new PdfPCell(new Phrase("MRP", font10Bold));
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    table.AddCell(cell);

                    int iCounter = 1;
                    int fk_BPM_Id = 0;
                    foreach (DataRow dr in dtPriceList.Rows)
                    {
                        int Currentfk_BPM_Id = Convert.ToInt32(dr["fk_BPM_Id"].ToString());
                        if (fk_BPM_Id != Currentfk_BPM_Id)
                        {
                            fk_BPM_Id = Convert.ToInt32(dr["fk_BPM_Id"].ToString());
                            int Count = dtPriceList.AsEnumerable().Where(r => r.Field<int>("fk_BPM_Id") == Convert.ToInt32(dr["fk_BPM_Id"].ToString())).Count();

                            cell = new PdfPCell(new Phrase(iCounter.ToString(), font5));
                            cell.Rowspan = Count;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(dr["BPM_Product_Name"].ToString(), font5));
                            cell.Rowspan = Count;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(dr["ProductCategoryName"].ToString(), font5));
                            cell.Rowspan = Count;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            table.AddCell(cell);
                            iCounter++;
                        }
                        table.AddCell(new Phrase(dr["PackMeasure"].ToString(), font5));

                        table.AddCell(new Phrase(dr["FinalPrice"].ToString().Split('|')[0], font5));
                        table.AddCell(new Phrase(dr["FinalPrice"].ToString().Split('|')[1], font5));
                        table.AddCell(new Phrase(dr["FinalPrice"].ToString().Split('|')[2], font5));

                        table.AddCell(new Phrase(dr["MRP"].ToString(), font5));

                    }



                    //PdfPTable tableFooter = new PdfPTable(1);
                    //float[] Footerwidths = new float[] {5f};

                    //tableFooter.TotalWidth = document.PageSize.Width - document.LeftMargin;
                    //tableFooter.AddCell("Page");
                    //tableFooter.WriteSelectedRows(0, -1, document.LeftMargin + 50, document.PageSize.Height - 30, writer.DirectContent);
                    //cell.Border = 0;
                    //tableFooter.AddCell(cell);

                    document.Add(tableHeader);
                    //document.Add(tableFooter);
                    document.Add(table);
                    document.Close();

                }

            }
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=PriceListGPEstimate.pdf");

            Response.TransmitFile(Server.MapPath("~/TempUse/PriceList.pdf"));
            Response.End();
            //string path = Server.MapPath("~/Content/TempUse/PriceList.pdf");
            //WebClient client = new WebClient();
            //Byte[] buffer = client.DownloadData(path);

            //if (buffer != null)
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.AddHeader("content-length", buffer.Length.ToString());
            //    Response.BinaryWrite(buffer);
            //    Response.End();
            //}



        }

        public void CompanyListDropDownListCombo()
        {
            ClsCompanyMaster cls = new ClsCompanyMaster();

            DataTable dt = new DataTable();
            //pro.User_Id = User_Id;
            dt = cls.Get_CompanyMasterData();

            CompanyDropdownList.DataSource = dt;
            CompanyDropdownList.DataTextField = "CompanyMaster_Name";

            CompanyDropdownList.DataValueField = "CompanyMaster_Id";
            CompanyDropdownList.DataBind();
            CompanyDropdownList.Items.Insert(0, "Select Company");
        }

        protected void CompanyDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsCompanyMaster cls = new ClsCompanyMaster();
            ProCompanyMaster pro = new ProCompanyMaster();
            if (CompanyDropdownList.SelectedValue != "Select Company")
            {
                Grid_PriceListDataGP.Visible = true;

                int CompanyList_Id = Convert.ToInt32(CompanyDropdownList.SelectedValue);
                pro.CompanyMaster_Id = CompanyList_Id;
                lblCompanyMasterList_Id.Text = CompanyList_Id.ToString();
                DataTable dt = new DataTable();
                DataTable dtComp = new DataTable();
                dt = cls.Get_CompanyMasterBy_Id(pro);
                lblCompanyMasterList_Name.Text = dt.Rows[0]["CompanyMaster_Name"].ToString();
                CompanyHeadertxt.Text = dt.Rows[0]["CompanyMaster_Name"].ToString();
                ClsPriceList clsPrice = new ClsPriceList();

                if (lblCompanyMasterList_Id.Text == "1" || lblCompanyMasterList_Id.Text == "0")
                {
                    dtComp = clsPrice.Get_AllPriceListGridByCompany(Convert.ToInt32(lblCompanyMasterList_Id.Text));

                    Grid_PriceListDataGP.DataSource = dtComp;
                    Grid_PriceListDataGP.DataBind();
                    Grid_PriceListDataOtherCompany.Visible = false;
                }
                else
                {
                    Grid_PriceListDataOtherCompany.Visible = true;
                    Grid_PriceListDataGP.Visible = false;
                    dtComp = clsPrice.Get_AllPriceListGridByOtherCompany(Convert.ToInt32(lblCompanyMasterList_Id.Text));

                    Grid_PriceListDataOtherCompany.DataSource = dtComp;
                    Grid_PriceListDataOtherCompany.DataBind();
                }

            }
            else
            {
                lblCompanyMasterList_Name.Text = "";
                Grid_PriceListDataGP.Visible = false;
            }

        }

        protected void PrintReportOtherCompany_Click(object sender, EventArgs e)
        {

        }


    }

}
