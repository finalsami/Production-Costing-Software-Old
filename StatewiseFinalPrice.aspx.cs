using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
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
    public partial class StatewiseFinalPrice : System.Web.UI.Page
    {
        int UserId;
        int Company_Id;
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
                ProductCategoryCombo();
                StateWiseCombo();
                ProductTradeNameCombo();
                BulkProductDropDownListData();
                PackMeasurementData();
                HidePackMeasurementDropDownList.Visible = false;
                Grid_StateWiseFinalPriceData();
                StateWiseGridView();
                BulkProductListboxData();
                PackingStyleDropDownListData();

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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[29]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[29]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[29]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblStatewiseFinalPrice_Id.Text != "")
                {
                    AddStatewiseFinalPrice.Visible = false;
                    UpdateStatewiseFinalPrice.Visible = true;
                    PdfReport.Visible = true;
                    ExcelReport.Visible = true;
                }
                else
                {
                    AddStatewiseFinalPrice.Visible = true;
                    UpdateStatewiseFinalPrice.Visible = false;

                    PdfReport.Visible = true;
                    ExcelReport.Visible = true;
                }

            }
            else
            {
                AddStatewiseFinalPrice.Visible = false;
                UpdateStatewiseFinalPrice.Visible = false;

                PdfReport.Visible = false;
                ExcelReport.Visible = false;
            }


        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[29]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[29]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[29]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblStatewiseFinalPrice_Id.Text != "")
                {
                    AddStatewiseFinalPrice.Visible = false;
                    UpdateStatewiseFinalPrice.Visible = true;
                    PdfReport.Visible = true;
                    ExcelReport.Visible = true;
                }
                else
                {
                    AddStatewiseFinalPrice.Visible = true;
                    UpdateStatewiseFinalPrice.Visible = false;
                    PdfReport.Visible = true;
                    ExcelReport.Visible = true;
                }

            }
            else
            {
                AddStatewiseFinalPrice.Visible = false;
                UpdateStatewiseFinalPrice.Visible = false;

                PdfReport.Visible = false;
                ExcelReport.Visible = false;
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
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void BulkProductListboxData()
        {
            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
            pro.Company_Id = Company_Id;
            DataTable dt = new DataTable();
            dt = cls.GET_BulkProductFromStatewiseFinalPrice(pro);

            BulkProductListbox.DataSource = dt;
            BulkProductListbox.DataTextField = "BPM_Product_Name";
            BulkProductListbox.DataValueField = "Fk_BPM_Id";
            BulkProductListbox.DataBind();
            BulkProductListbox.Items.Insert(0, "Select");
        }
        public void StateWiseGridView()
        {
            ClsTransportationCostFactors cls = new ClsTransportationCostFactors();
            StateWiseGridDropdownList.DataSource = cls.Get_AllStateData();
            StateWiseGridDropdownList.DataTextField = "StateName";
            StateWiseGridDropdownList.DataValueField = "StateID";
            StateWiseGridDropdownList.DataBind();
            StateWiseGridDropdownList.Items.Insert(0, "All State");
        }
        public void PackMeasurementData()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            PackMeasurementDropDownList.DataSource = cls.GetEnumMasterMeasurement();
            PackMeasurementDropDownList.DataTextField = "EnumDescription";
            PackMeasurementDropDownList.DataValueField = "PkEnumId";
            PackMeasurementDropDownList.DataBind();
            PackMeasurementDropDownList.Items.Insert(0, "Select");

        }
        //public void PackingSizeDropDownListData()
        //{
        //    ClsFinishedGoodsMaster clsPack = new ClsFinishedGoodsMaster();
        //    ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
        //    DataTable dtPack = new DataTable();
        //    pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);

        //    dtPack = clsPack.Get_FinishedGoodsMasterByBPM_Id(Convert.ToInt32(lblBPM_Id.Text), decimal.Parse(lblPacksize.Text), Convert.ToInt32(lblpackMeasurement.Text));
        //    //lblpackMeasurement.Text = dtPack.Rows[0]["UnitMeasurement"].ToString();
        //    //PackMeasurementDropDownList.SelectedValue = dtPack.Rows[0]["Fk_UnitMeasuremnt_Id"].ToString();
        //    //lblPacksize.Text = dtPack.Rows[0]["Pack_size"].ToString();
        //    PackingSizeDropDownList.DataSource = dtPack;
        //    dtPack.Columns.Add("PackingSizeCombo", typeof(string), "PackingSize + ' (' + PackMeasurement + ')'").ToString();
        //    PackingSizeDropDownList.DataTextField = "PackUnitMeasurement";
        //    PackingSizeDropDownList.DataValueField = "PackingSizeCombo";
        //    PackingSizeDropDownList.DataBind();
        //    PackingSizeDropDownList.Items.Insert(0, "Select");
        //}
        public void BulkProductDropDownListData()
        {
            ClsCategoryMappingMaster cls = new ClsCategoryMappingMaster();
            DataTable BulkDtDropdown = new DataTable();
            ProCategoryMappingMaster pro = new ProCategoryMappingMaster();
            pro.Comapny_Id = Company_Id;
            BulkDtDropdown = cls.GET_BPM_from_CategoryMappingMaster(pro);
            BulkDtDropdown.Columns.Add("BPMValue", typeof(string), "Fk_BPM_Id +'('+Fk_PM_RM_Category_Id+ ')'").ToString();

            BulkProductDropDownList.DataSource = BulkDtDropdown;
            BulkProductDropDownList.DataTextField = "BPM_Product_Name";

            BulkProductDropDownList.DataValueField = "BPMValue";
            BulkProductDropDownList.DataBind();
            BulkProductDropDownList.Items.Insert(0, "Select");
        }

        public void StateWiseCombo()
        {
            //ClsTransportationCostFactors cls = new ClsTransportationCostFactors();
            ClsTransportationCostFactors cls = new ClsTransportationCostFactors();
            ProTransportationCostFactors pro = new ProTransportationCostFactors();
            pro.Company_Id = Company_Id;
            StateNameDropdown.DataSource = cls.Get_State_From_TransportationCostFactors(pro);
            StateNameDropdown.DataTextField = "StateName";
            StateNameDropdown.DataValueField = "Fk_State_Id";
            StateNameDropdown.DataBind();
            StateNameDropdown.Items.Insert(0, "Select");
        }
        public void ProductTradeNameCombo()
        {
            ClsTradeNameMaster cls = new ClsTradeNameMaster();
            ProTradeNameMaster pro = new ProTradeNameMaster();
            pro.Comapny_Id = Company_Id;
            ProductTradeNameDropDown.DataSource = cls.Get_TradeNameMasterAll(pro);
            ProductTradeNameDropDown.DataTextField = "TradeName";
            ProductTradeNameDropDown.DataValueField = "TradeName_Id";
            ProductTradeNameDropDown.DataBind();
            ProductTradeNameDropDown.Items.Insert(0, "Select");


        }
        public void PackingStyleDropDownListData()
        {
            ClsPackingStyleNameMaster cls = new ClsPackingStyleNameMaster();
            PackingStyleDropDownList.DataSource = cls.Get_PackingStyleNameAll(UserId);
            PackingStyleDropDownList.DataTextField = "PackingStyleName";
            PackingStyleDropDownList.DataValueField = "PackingStyleName_Id";
            PackingStyleDropDownList.DataBind();
            PackingStyleDropDownList.Items.Insert(0, "Select");
        }
        public void ProductCategoryCombo()
        {
            ClsProductCategoryMaster cls = new ClsProductCategoryMaster();
            ProductCategoryDropDownList.DataSource = cls.Get_ProductCategoryMasterAll();
            ProductCategoryDropDownList.DataTextField = "ProductCategoryName";
            ProductCategoryDropDownList.DataValueField = "Product_Category_Id";
            ProductCategoryDropDownList.DataBind();
            ProductCategoryDropDownList.Items.Insert(0, "Select");
        }

        protected void ProductTradeNameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
            if (ProductTradeNameDropDown.SelectedValue != "Select")
            {
                int TradeName_Id = Convert.ToInt32(ProductTradeNameDropDown.SelectedValue);
                pro.TradeName_Id = TradeName_Id;
                lblTradeName_Id.Text = TradeName_Id.ToString();
                pro.Company_Id = Company_Id;
                DataTable dt = new DataTable();
                dt = cls.Get_StatewiseDataFrom_PackSize_BPM_Id(pro);
                if (dt.Rows.Count > 0)
                {
                    string BulkIdFromDB = (dt.Rows[0]["Fk_BPM_Id"] + " (" + dt.Rows[0]["Fk_PMRM_Category_Id"] + ")" + "[" + dt.Rows[0]["PackSize"] + "-" + dt.Rows[0]["PackingMeasurement"] + "]").ToString();
                    BulkProductDropDownList.SelectedValue = BulkIdFromDB;
                    ProductCategoryDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_ProductCategory_Id"]).ToString();
                    PackingStyleDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_PackingStyleName_Id"]).ToString();
                    PackMeasurementDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["PackingMeasurement"]).ToString();
                    NRVtxt.Text = dt.Rows[0]["NetFinishedGoodsCostPerUnit"].ToString();
                    lblPacksize.Text = (dt.Rows[0]["PackSize"]).ToString();
                    lblpackMeasurement.Text = dt.Rows[0]["PackingMeasurement"].ToString();
                    lblBPM_Id.Text = (BulkProductDropDownList.SelectedValue);
                    PackingSizeDropDownList.Enabled = true;
                    //BulkProductDropDownList.Enabled = true;
                    //PackingStyleDropDownList.Enabled = true;

                    //string BPM_Full_Id = BulkProductNameDropDownList.SelectedValue.ToString();

                    //lblPMRM_Category_Id.Text = BPM_Full_Id.Split('(', ')')[1];
                    //string BracketMeasurement = BPM_Full_Id.Split('(', ')')[2];
                    //lblPAckSize.Text = Regex.Match(BracketMeasurement, @"\d+").Value;
                    //lblPackMeasurement.Text = BracketMeasurement.Split('-', ']')[1];
                    //lblPackSize.Text = PackSize;
                    //lblPMRM_Category_Id.Text = Shipper_Id;

                    //lblPackMeasurement.Text = PackUniMeasurement;
                    string Get_BPM_Id = Regex.Match(BulkIdFromDB, @"\d+").Value;
                    lblBPM_Id.Text = Get_BPM_Id;


                    //PackingSizeDropDownListData();
                    PackingStyleDropDownListData();

                    ClsTransportationCostMaster clsTranspor = new ClsTransportationCostMaster();
                    DataTable DtTransport = new DataTable();
                    pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);
                    pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
                    DtTransport = clsTranspor.Get_TransportationBy_State_BPM_Id(pro);
                    if (DtTransport.Rows.Count > 0)
                    {
                        Transporttxt.Text = DtTransport.Rows[0]["TotalTransportationExpense"].ToString();

                    }
                    if (NRVtxt.Text != "" && Transporttxt.Text != "")
                    {
                        FinalNRVtxt.Text = (decimal.Parse(NRVtxt.Text) + decimal.Parse(Transporttxt.Text)).ToString(); ;
                    }
                }


            }
        }

        protected void PD_Schemetxt_TextChanged(object sender, EventArgs e)
        {
            TotalPDtxt.Text = (decimal.Parse(string.IsNullOrEmpty(PD_Schemetxt.Text.Trim()) ? "0" : PD_Schemetxt.Text.Trim()) + decimal.Parse(
                string.IsNullOrEmpty(AddiPDtxt.Text.Trim()) ? "0" : AddiPDtxt.Text.Trim())).ToString();

            FinalRPLPriceWithPDtxt.Text = (decimal.Parse(TotalPDtxt.Text) + (decimal.Parse(SuggestedRPL_Pricetxt.Text))).ToString("0.00");

            NetProfitRStxt.Text = (decimal.Parse(FinalRPLPriceWithPDtxt.Text) - decimal.Parse(TotalPDtxt.Text) - decimal.Parse(TOTALTXT.Text) - decimal.Parse(FinalNRVtxt.Text)).ToString("0.00");
            NetProfitRPLPertxt.Text = (decimal.Parse(NetProfitRStxt.Text) * ((decimal.Parse("100") / decimal.Parse(SuggestedRPL_Pricetxt.Text)))).ToString("0.00");
            if (decimal.Parse(NetProfitRPLPertxt.Text) > 0)
            {
                NetProfitRPLPertxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NetProfitRPLPertxt.ForeColor = System.Drawing.Color.Red;
            }
            if (decimal.Parse(NetProfitRStxt.Text) > 0)
            {
                NetProfitRStxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NetProfitRStxt.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void AddiPDtxt_TextChanged(object sender, EventArgs e)
        {

            TotalPDtxt.Text = (decimal.Parse(string.IsNullOrEmpty(PD_Schemetxt.Text.Trim()) ? "0" : PD_Schemetxt.Text.Trim()) + decimal.Parse(
                string.IsNullOrEmpty(AddiPDtxt.Text.Trim()) ? "0" : AddiPDtxt.Text.Trim())).ToString();
            //if (PD_Schemetxt.Text != "" && AddiPDtxt.Text != "" || PD_Schemetxt.Text != "0" && AddiPDtxt.Text != "0")
            //{
            //    TotalPDtxt.Text = (decimal.Parse(PD_Schemetxt.Text) + decimal.Parse(AddiPDtxt.Text)).ToString();
            //}
            FinalRPLPriceWithPDtxt.Text = (decimal.Parse(TotalPDtxt.Text) + (decimal.Parse(SuggestedRPL_Pricetxt.Text))).ToString("0.00");

            NetProfitRStxt.Text = (decimal.Parse(FinalRPLPriceWithPDtxt.Text) - decimal.Parse(TotalPDtxt.Text) - decimal.Parse(TOTALTXT.Text) - decimal.Parse(FinalNRVtxt.Text)).ToString("0.00");
            NetProfitRPLPertxt.Text = (decimal.Parse(NetProfitRStxt.Text) * ((decimal.Parse("100") / decimal.Parse(SuggestedRPL_Pricetxt.Text)))).ToString("0.00");
            if (decimal.Parse(NetProfitRPLPertxt.Text) > 0)
            {
                NetProfitRPLPertxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NetProfitRPLPertxt.ForeColor = System.Drawing.Color.Red;
            }
            if (decimal.Parse(NetProfitRStxt.Text) > 0)
            {
                NetProfitRStxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NetProfitRStxt.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void RPL_ApproProfittxt_TextChanged(object sender, EventArgs e)
        {
            if (FinalNRVtxt.Text != "" && FinalNRVtxt.Text != "0")
            {
                if (RPL_ApproProfittxt.Text != "0")
                {

                    RPL_ProfitAmttxt.Text = (decimal.Parse(FinalNRVtxt.Text) * (decimal.Parse((RPL_ApproProfittxt.Text))) / decimal.Parse("100")).ToString("0.00");
                    SuggestedRPL_Pricetxt.Text = (decimal.Parse(FinalNRVtxt.Text) + decimal.Parse(TotalPDtxt.Text) + decimal.Parse(RPL_ProfitAmttxt.Text)).ToString("0.00");
                }
                if (RPL_ApproProfittxt.Text == "0")
                {
                    RPL_ProfitAmttxt.Text = (decimal.Parse(FinalNRVtxt.Text) * (decimal.Parse(("1")) / (decimal.Parse("100")))).ToString("0.00");
                    SuggestedRPL_Pricetxt.Text = (decimal.Parse(FinalNRVtxt.Text)) + (decimal.Parse(TotalPDtxt.Text)) + (decimal.Parse(RPL_ProfitAmttxt.Text)).ToString("0.00");



                }
                DEPOEXPENCEtxt.Text = ((decimal.Parse(lblDepotExpence.Text)) * (decimal.Parse((SuggestedRPL_Pricetxt.Text))) / (decimal.Parse("100"))).ToString("0.00");
                INTERESTTXT.Text = (decimal.Parse(lblInterest.Text) * (decimal.Parse((SuggestedRPL_Pricetxt.Text)) / (decimal.Parse("100")))).ToString("0.00");
                INCENTIVEtxt.Text = (decimal.Parse(lblIncentive.Text) * (decimal.Parse((SuggestedRPL_Pricetxt.Text)) / (decimal.Parse("100")))).ToString("0.00");
                MARKETINGTXT.Text = (decimal.Parse(lblMarketing.Text) * (decimal.Parse((SuggestedRPL_Pricetxt.Text)) / (decimal.Parse("100")))).ToString("0.00");
                STAFFEXPENCEtxt.Text = (decimal.Parse(lblStaffExpence.Text) * (decimal.Parse((SuggestedRPL_Pricetxt.Text)) / (decimal.Parse("100")))).ToString("0.00");
                RPLOthertxt.Text = (decimal.Parse(lblRPLOther.Text) * (decimal.Parse((SuggestedRPL_Pricetxt.Text)) / (decimal.Parse("100")))).ToString("0.00");



                TotalPDtxt.Text = (decimal.Parse(string.IsNullOrEmpty(PD_Schemetxt.Text.Trim()) ? "0" : PD_Schemetxt.Text.Trim()) + decimal.Parse(
                string.IsNullOrEmpty(AddiPDtxt.Text.Trim()) ? "0" : AddiPDtxt.Text.Trim())).ToString();

                FinalRPLPriceWithPDtxt.Text = (decimal.Parse(TotalPDtxt.Text) + (decimal.Parse(SuggestedRPL_Pricetxt.Text))).ToString("0.00");
                TOTALTXT.Text = ((decimal.Parse(DEPOEXPENCEtxt.Text)) + (decimal.Parse(INTERESTTXT.Text)) + (decimal.Parse(INCENTIVEtxt.Text)) + (decimal.Parse(MARKETINGTXT.Text)) + (decimal.Parse(STAFFEXPENCEtxt.Text)) + decimal.Parse(RPLOthertxt.Text)).ToString();

                NetProfitRStxt.Text = (decimal.Parse(FinalRPLPriceWithPDtxt.Text) - decimal.Parse(TotalPDtxt.Text) - decimal.Parse(TOTALTXT.Text) - decimal.Parse(FinalNRVtxt.Text)).ToString("0.00");
                NetProfitRPLPertxt.Text = (decimal.Parse(NetProfitRStxt.Text) * ((decimal.Parse("100") / decimal.Parse(SuggestedRPL_Pricetxt.Text)))).ToString("0.00");
                if (decimal.Parse(NetProfitRPLPertxt.Text) > 0)
                {
                    NetProfitRPLPertxt.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    NetProfitRPLPertxt.ForeColor = System.Drawing.Color.Red;
                }
                if (decimal.Parse(NetProfitRStxt.Text) > 0)
                {
                    NetProfitRStxt.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    NetProfitRStxt.ForeColor = System.Drawing.Color.Red;
                }






                GrossProfitRStxt.Text = ((decimal.Parse(SuggestedRPL_Pricetxt.Text)) - (decimal.Parse(FinalNRVtxt.Text))).ToString("0.00");
                PerGrossProfittxt.Text = (decimal.Parse(GrossProfitRStxt.Text) * ((decimal.Parse("100") / decimal.Parse(FinalNRVtxt.Text)))).ToString("0.00");

                if (decimal.Parse(GrossProfitRStxt.Text) > 0)
                {
                    GrossProfitRStxt.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    GrossProfitRStxt.ForeColor = System.Drawing.Color.Red;
                }


                if (decimal.Parse(PerGrossProfittxt.Text) > 0)
                {
                    PerGrossProfittxt.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    PerGrossProfittxt.ForeColor = System.Drawing.Color.Red;
                }


                if (Diff_RPL_To_NCRtxt.Text != "" || Diff_RPL_To_NCRtxt.Text != "0")
                {
                    //decimal netsuggesstedcalc = decimal.Parse(SuggestedRPL_Pricetxt.Text) - decimal.Parse(TotalPDtxt.Text);

                    //SuggestedPriceNCRtxt.Text = (netsuggesstedcalc - ((netsuggesstedcalc) * (decimal.Parse(Diff_RPL_To_NCRtxt.Text) / decimal.Parse("100")))).ToString("0.00");
                    //SuggestedPriceNCRtxt.Text = (netsuggesstedcalc - ((decimal.Parse(SuggestedRPL_Pricetxt.Text)) * (decimal.Parse(Diff_RPL_To_NCRtxt.Text) / decimal.Parse("100")))).ToString("0.00");
                    decimal netsuggesstedcalc = decimal.Parse(SuggestedRPL_Pricetxt.Text) * ((decimal.Parse(Diff_RPL_To_NCRtxt.Text) / decimal.Parse("100")));
                    SuggestedPriceNCRtxt.Text = (decimal.Parse(SuggestedRPL_Pricetxt.Text) * ((decimal.Parse(Diff_RPL_To_NCRtxt.Text) / decimal.Parse("100")))).ToString("0.00");
                    NCRPricetxt.Text = (decimal.Parse(SuggestedRPL_Pricetxt.Text) - (netsuggesstedcalc)).ToString("0.00"); NCRDepoExpencetxt.Text = (decimal.Parse(lblNCRDepoExpence.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / decimal.Parse("100"))).ToString("0.00");
                    NCRDepoExpencetxt.Text = (decimal.Parse(lblNCRDepoExpence.Text) * (decimal.Parse(NCRPricetxt.Text) / decimal.Parse("100"))).ToString("0.00");
                    NCRInteresttxt.Text = (decimal.Parse(lblNCRInterest.Text) * (decimal.Parse((NCRPricetxt.Text)) / decimal.Parse("100"))).ToString("0.00");
                    NCRIncentivetxt.Text = (decimal.Parse(lblNCRIncentive.Text) * (decimal.Parse((NCRPricetxt.Text)) / decimal.Parse("100"))).ToString("0.00");
                    NCRMarketingtxt.Text = (decimal.Parse(lblNCRMarketing.Text) * (decimal.Parse((NCRPricetxt.Text)) / decimal.Parse("100"))).ToString("0.00");
                    NCRStaffExpencetxt.Text = (decimal.Parse(lblNCRStaffExpence.Text) * (decimal.Parse((NCRPricetxt.Text)) / decimal.Parse("100"))).ToString("0.00");

                    NCROthertxt.Text = (decimal.Parse(lblNCROther.Text) * (decimal.Parse((NCRPricetxt.Text)) / decimal.Parse("100"))).ToString("0.00");

                    NCRTotaltxt.Text = (decimal.Parse(NCRDepoExpencetxt.Text) + decimal.Parse(NCRInteresttxt.Text) + decimal.Parse(NCRIncentivetxt.Text) + decimal.Parse(NCRMarketingtxt.Text) + decimal.Parse(NCRStaffExpencetxt.Text) + decimal.Parse(NCROthertxt.Text)).ToString();



                    NCRGrossProfitRStxt.Text = ((decimal.Parse(NCRPricetxt.Text)) - ((decimal.Parse(SuggestedRPL_Pricetxt.Text)))).ToString("0.00");

                    if (decimal.Parse(NCRGrossProfitRStxt.Text) > 0)
                    {
                        NCRGrossProfitRStxt.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        NCRGrossProfitRStxt.ForeColor = System.Drawing.Color.Red;
                    }
                    NCRPerGrossProfittxt.Text = (decimal.Parse(NCRGrossProfitRStxt.Text) * ((decimal.Parse("100") / decimal.Parse(SuggestedRPL_Pricetxt.Text)))).ToString("0.00");

                    NCRTotalPDtxt.Text = (decimal.Parse(string.IsNullOrEmpty(NCRPD_Schemetxt.Text.Trim()) ? "0" : NCRPD_Schemetxt.Text.Trim()) + decimal.Parse(
            string.IsNullOrEmpty(NCRAddiPDtxt.Text.Trim()) ? "0" : NCRAddiPDtxt.Text.Trim())).ToString();

                    FinalNRCPriceWithPDtxt.Text = (decimal.Parse(NCRTotalPDtxt.Text) + (decimal.Parse(NCRPricetxt.Text))).ToString("0.00");

                    NCRNetProfitRStxt.Text = (decimal.Parse(FinalNRCPriceWithPDtxt.Text) - decimal.Parse(SuggestedRPL_Pricetxt.Text) - decimal.Parse(NCRTotalPDtxt.Text) - decimal.Parse(NCRTotaltxt.Text)).ToString("0.00");

                    NCRNetProfitRPLPertxt.Text = (decimal.Parse(NCRNetProfitRStxt.Text) * ((decimal.Parse("100") / decimal.Parse(NCRPricetxt.Text)))).ToString("0.00");

                    if (decimal.Parse(NCRPerGrossProfittxt.Text) > 0)
                    {
                        NCRPerGrossProfittxt.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        NCRPerGrossProfittxt.ForeColor = System.Drawing.Color.Red;
                    }

                    if (decimal.Parse(NCRNetProfitRStxt.Text) > 0)
                    {
                        NCRNetProfitRStxt.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        NCRNetProfitRStxt.ForeColor = System.Drawing.Color.Red;
                    }
                    if (decimal.Parse(NCRNetProfitRPLPertxt.Text) > 0)
                    {
                        NCRNetProfitRPLPertxt.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        NCRNetProfitRPLPertxt.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void StateNameDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            CleareDataForState();
            if (StateNameDropdown.SelectedValue != "Select")
            {

                DataTable dt = new DataTable();
                ClsExpenceType cls = new ClsExpenceType();
                ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
                pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);
                pro.Company_Id = Company_Id;
                dt = cls.Get_ExpenceBy_State_Id(pro);

                if (dt.Rows.Count > 0)
                {
                    lblPriceType_Id.Text = dt.Rows[0]["FkPriceTypeId"].ToString();
                    if (lblPriceType_Id.Text == "8")
                    {
                        lblStaffExpence.Text = dt.Rows[0]["StaffExpense"].ToString();
                        lblDepotExpence.Text = dt.Rows[0]["DepoExpence"].ToString();
                        lblIncentive.Text = dt.Rows[0]["Incentive"].ToString();
                        lblInterest.Text = dt.Rows[0]["Interest"].ToString();
                        lblRPLOther.Text = dt.Rows[0]["Other"].ToString();
                        lblMarketing.Text = dt.Rows[0]["Marketing"].ToString();

                        lblNCRStaffExpence.Text = dt.Rows[1]["StaffExpense"].ToString();
                        lblNCRDepoExpence.Text = dt.Rows[1]["DepoExpence"].ToString();
                        lblNCRIncentive.Text = dt.Rows[1]["Incentive"].ToString();
                        lblNCRInterest.Text = dt.Rows[1]["Interest"].ToString();
                        lblNCROther.Text = dt.Rows[1]["Other"].ToString();
                        lblNCRMarketing.Text = dt.Rows[1]["Marketing"].ToString();
                    }
                    else
                    {

                        lblNCRStaffExpence.Text = dt.Rows[0]["StaffExpense"].ToString();
                        lblNCRDepoExpence.Text = dt.Rows[0]["DepoExpence"].ToString();
                        lblNCRIncentive.Text = dt.Rows[0]["Incentive"].ToString();
                        lblNCRInterest.Text = dt.Rows[0]["Interest"].ToString();
                        lblNCROther.Text = dt.Rows[0]["Other"].ToString();
                        lblNCRMarketing.Text = dt.Rows[0]["Marketing"].ToString();

                        lblStaffExpence.Text = dt.Rows[1]["StaffExpense"].ToString();
                        lblDepotExpence.Text = dt.Rows[1]["DepoExpence"].ToString();
                        lblIncentive.Text = dt.Rows[1]["Incentive"].ToString();
                        lblInterest.Text = dt.Rows[1]["Interest"].ToString();
                        lblRPLOther.Text = dt.Rows[1]["Other"].ToString();
                        lblMarketing.Text = dt.Rows[1]["Marketing"].ToString();


                    }

                }
                //if (dt.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        if (row.IsNull("Depot Expence"))
                //        {

                //        }
                //        else
                //        {
                //            lblDepotExpence.Text = dt.Rows[2]["Depot Expence"].ToString();
                //        }
                //        if (row.IsNull("Incentive"))
                //        {

                //        }
                //        else
                //        {
                //            lblIncentive.Text = dt.Rows[4]["Incentive"].ToString();
                //        }
                //        if (row.IsNull("Marketing"))
                //        {

                //        }
                //        else
                //        {
                //            lblMarketing.Text = dt.Rows[6]["Marketing"].ToString();
                //        }
                //        if (row.IsNull("Intrest"))
                //        {

                //        }
                //        else
                //        {
                //            lblInterest.Text = dt.Rows[8]["Intrest"].ToString();
                //        }
                //        if (row.IsNull("Staff Expence"))
                //        {

                //        }
                //        else
                //        {
                //            lblStaffExpence.Text = dt.Rows[0]["Staff Expence"].ToString();
                //        }
                //        if (row.IsNull("Staff Expence"))
                //        {

                //        }
                //        else
                //        {
                //            lblNCRStaffExpence.Text = dt.Rows[1]["Staff Expence"].ToString();
                //        }
                //        if (row.IsNull("Depot Expence"))
                //        {

                //        }
                //        else
                //        {
                //            lblNCRDepoExpence.Text = dt.Rows[3]["Depot Expence"].ToString();
                //        }
                //        if (row.IsNull("Incentive"))
                //        {

                //        }
                //        else
                //        {
                //            lblNCRIncentive.Text = dt.Rows[5]["Incentive"].ToString();
                //        }
                //        if (row.IsNull("Marketing"))
                //        {

                //        }
                //        else
                //        {
                //            lblNCRMarketing.Text = dt.Rows[7]["Marketing"].ToString();
                //        }
                //        if (row.IsNull("Intrest"))
                //        {

                //        }
                //        else
                //        {
                //            lblNCRInterest.Text = dt.Rows[9]["Intrest"].ToString();
                //        }
                //    }
                //}

            }
            else
            {
                CleareData();
            }
        }

        protected void AddStatewiseFinalPrice_Click(object sender, EventArgs e)
        {
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);

            pro.PMRM_Category_Id = Convert.ToInt32(lblPMRMCategory_Id.Text);

            pro.PD_Scheme = decimal.Parse(PD_Schemetxt.Text);
            pro.Add_PD = decimal.Parse(AddiPDtxt.Text);
            pro.NCR_Add_PD = decimal.Parse(NCRAddiPDtxt.Text);
            pro.NCR_PD_Scheme = decimal.Parse(NCRPD_Schemetxt.Text);
            pro.RPL_Approx_Profit = decimal.Parse(RPL_ApproProfittxt.Text);

            pro.Diff_RPL_NCR = decimal.Parse(Diff_RPL_To_NCRtxt.Text);

            pro.Company_Id = Company_Id;

            try
            {
                int status = cls.Insert_StateWiseFinalPrice(pro);

                if (status > 0)
                {
                    Grid_StateWiseFinalPriceData();
                    CleareData();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                    BulkProductListboxData();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed!')", true);

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void Grid_StateWiseFinalPriceData()
        {
            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
            pro.Company_Id = Company_Id;
            Grid_StatewiseFinalPrice.DataSource = cls.Get_StateWiseFinalPriceAll(pro);

            Grid_StatewiseFinalPrice.DataBind();
        }
        public void CleareData()
        {
            lblDepotExpence.Text = "0";
            lblIncentive.Text = "0";
            lblMarketing.Text = "0";
            lblInterest.Text = "0";
            lblStaffExpence.Text = "0";
            lblNCRStaffExpence.Text = "0";
            lblNCRDepoExpence.Text = "0";
            lblNCRIncentive.Text = "0";
            lblNCRMarketing.Text = "0";
            lblDepotExpence.Text = "0";
            lblNCRInterest.Text = "0";
            BulkProductDropDownList.SelectedIndex = 0;
            StateNameDropdown.SelectedIndex = 0;
            ProductTradeNameDropDown.SelectedIndex = 0;
            //PackingSizeDropDownList.SelectedIndex = -1;
            PackMeasurementDropDownList.SelectedIndex = 0;
            //PackingStyleDropDownList.SelectedIndex = 0;
            ProductCategoryDropDownList.SelectedIndex = 0;
            PackingStyleDropDownList.SelectedIndex = 0;
            NRVtxt.Text = "0";
            Transporttxt.Text = "0";
            FinalNRVtxt.Text = "0";
            PD_Schemetxt.Text = "0";
            AddiPDtxt.Text = "0";
            TotalPDtxt.Text = "0";
            RPL_ApproProfittxt.Text = "0";
            RPL_ProfitAmttxt.Text = "0";
            SuggestedRPL_Pricetxt.Text = "0";
            STAFFEXPENCEtxt.Text = "0";
            DEPOEXPENCEtxt.Text = "0";
            INCENTIVEtxt.Text = "0";
            MARKETINGTXT.Text = "0";
            INTERESTTXT.Text = "0";
            TOTALTXT.Text = "0";
            GrossProfitRStxt.Text = "0";
            PerGrossProfittxt.Text = "0";
            NetProfitRStxt.Text = "0";
            NetProfitRPLPertxt.Text = "0";
            Diff_RPL_To_NCRtxt.Text = "0";
            SuggestedPriceNCRtxt.Text = "0";
            NCRStaffExpencetxt.Text = "0";
            NCRDepoExpencetxt.Text = "0";
            NCRIncentivetxt.Text = "0";
            NCRMarketingtxt.Text = "0";
            NCRInteresttxt.Text = "0";
            NCRTotaltxt.Text = "0";
            NCRGrossProfitRStxt.Text = "0";
            NCRPerGrossProfittxt.Text = "0";
            NCRNetProfitRStxt.Text = "0";
            NCRNetProfitRPLPertxt.Text = "0";
            lblStatewiseFinalPrice_Id.Text = "";
            lblBPM_Id.Text = "0";
            PackingStyleDropDownList.Enabled = false;
            //PackingSizeDropDownList.Enabled = false;
            MasterPackSizetxt.Text = "";
            NCRPricetxt.Text = "0";
            FinalRPLPriceWithPDtxt.Text = "0";
            NCRPD_Schemetxt.Text = "0";
            NCRAddiPDtxt.Text = "0";
            NCRTotalPDtxt.Text = "0";
            FinalNRCPriceWithPDtxt.Text = "0";
            lblNCROther.Text = "0";
            NCROthertxt.Text = "0";
            lblRPLOther.Text = "0";
            RPLOthertxt.Text = "0";
        }
        public void CleareDataForState()
        {
            lblDepotExpence.Text = "0";
            lblIncentive.Text = "0";
            lblMarketing.Text = "0";
            lblInterest.Text = "0";
            lblStaffExpence.Text = "0";
            lblNCRStaffExpence.Text = "0";
            lblNCRDepoExpence.Text = "0";
            lblNCRIncentive.Text = "0";
            lblNCRMarketing.Text = "0";
            lblDepotExpence.Text = "0";
            lblNCRInterest.Text = "0";
            BulkProductDropDownList.SelectedIndex = 0;
            //StateNameDropdown.SelectedIndex = 0;
            ProductTradeNameDropDown.SelectedIndex = 0;
            //PackingSizeDropDownList.SelectedIndex = -1;
            PackMeasurementDropDownList.SelectedIndex = 0;
            //PackingStyleDropDownList.SelectedIndex = 0;
            ProductCategoryDropDownList.SelectedIndex = 0;
            PackingStyleDropDownList.SelectedIndex = 0;
            NRVtxt.Text = "0";
            Transporttxt.Text = "0";
            FinalNRVtxt.Text = "0";
            PD_Schemetxt.Text = "0";
            AddiPDtxt.Text = "0";
            TotalPDtxt.Text = "0";
            RPL_ApproProfittxt.Text = "0";
            RPL_ProfitAmttxt.Text = "0";
            SuggestedRPL_Pricetxt.Text = "0";
            STAFFEXPENCEtxt.Text = "0";
            DEPOEXPENCEtxt.Text = "0";
            INCENTIVEtxt.Text = "0";
            MARKETINGTXT.Text = "0";
            INTERESTTXT.Text = "0";
            TOTALTXT.Text = "0";
            GrossProfitRStxt.Text = "0";
            PerGrossProfittxt.Text = "0";
            NetProfitRStxt.Text = "0";
            NetProfitRPLPertxt.Text = "0";
            Diff_RPL_To_NCRtxt.Text = "0";
            SuggestedPriceNCRtxt.Text = "0";
            NCRStaffExpencetxt.Text = "0";
            NCRDepoExpencetxt.Text = "0";
            NCRIncentivetxt.Text = "0";
            NCRMarketingtxt.Text = "0";
            NCRInteresttxt.Text = "0";
            NCRTotaltxt.Text = "0";
            NCRGrossProfitRStxt.Text = "0";
            NCRPerGrossProfittxt.Text = "0";
            NCRNetProfitRStxt.Text = "0";
            NCRNetProfitRPLPertxt.Text = "0";
            lblStatewiseFinalPrice_Id.Text = "";
            lblBPM_Id.Text = "0";
            PackingStyleDropDownList.Enabled = false;
            //PackingSizeDropDownList.Enabled = false;
            MasterPackSizetxt.Text = "";
            NCRPricetxt.Text = "0";
            FinalRPLPriceWithPDtxt.Text = "0";
            NCRPD_Schemetxt.Text = "0";
            NCRAddiPDtxt.Text = "0";
            NCRTotalPDtxt.Text = "0";
            FinalNRCPriceWithPDtxt.Text = "0";
            lblNCROther.Text = "0";
            NCROthertxt.Text = "0";
            lblRPLOther.Text = "0";
            RPLOthertxt.Text = "0";
        }
        protected void UpdateStatewiseFinalPrice_Click(object sender, EventArgs e)
        {
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);

            pro.PMRM_Category_Id = Convert.ToInt32(lblPMRMCategory_Id.Text);

            pro.PD_Scheme = decimal.Parse(PD_Schemetxt.Text);
            pro.Add_PD = decimal.Parse(AddiPDtxt.Text);
            pro.NCR_Add_PD = decimal.Parse(NCRAddiPDtxt.Text);
            pro.NCR_PD_Scheme = decimal.Parse(NCRPD_Schemetxt.Text);
            pro.RPL_Approx_Profit = decimal.Parse(RPL_ApproProfittxt.Text);

            pro.Diff_RPL_NCR = decimal.Parse(Diff_RPL_To_NCRtxt.Text);

            pro.StatewiseFinalPrice_Id = Convert.ToInt32(lblStatewiseFinalPrice_Id.Text);
            pro.Company_Id = Company_Id;
            try
            {
                int status = cls.Update_StateWiseFinalPrice(pro);

                if (status > 0)
                {
                    Grid_StateWiseFinalPriceData();
                    CleareData();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);
                    if (lblStatewiseFinalPrice_Id.Text != "")
                    {
                        AddStatewiseFinalPrice.Visible = false;
                        UpdateStatewiseFinalPrice.Visible = true;
                        PdfReport.Visible = true;
                        ExcelReport.Visible = true;
                    }
                    else
                    {
                        AddStatewiseFinalPrice.Visible = true;
                        UpdateStatewiseFinalPrice.Visible = false;
                        PdfReport.Visible = true;
                        ExcelReport.Visible = true;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Failed!')", true);
                    if (lblStatewiseFinalPrice_Id.Text != "")
                    {
                        AddStatewiseFinalPrice.Visible = false;
                        UpdateStatewiseFinalPrice.Visible = true;
                        PdfReport.Visible = true;
                        ExcelReport.Visible = true;
                    }
                    else
                    {
                        AddStatewiseFinalPrice.Visible = true;
                        UpdateStatewiseFinalPrice.Visible = false;
                        PdfReport.Visible = true;
                        ExcelReport.Visible = true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void Diff_RPL_To_NCRtxt_TextChanged(object sender, EventArgs e)
        {
            if (Diff_RPL_To_NCRtxt.Text != "")
            {
                //decimal netsuggesstedcalc = decimal.Parse(SuggestedRPL_Pricetxt.Text) - decimal.Parse(TotalPDtxt.Text);

                //SuggestedPriceNCRtxt.Text = (netsuggesstedcalc - ((netsuggesstedcalc) * (decimal.Parse(Diff_RPL_To_NCRtxt.Text) / decimal.Parse("100")))).ToString("0.00");
                //SuggestedPriceNCRtxt.Text = (netsuggesstedcalc - ((decimal.Parse(SuggestedRPL_Pricetxt.Text)) * (decimal.Parse(Diff_RPL_To_NCRtxt.Text) / decimal.Parse("100")))).ToString("0.00");
                decimal netsuggesstedcalc = decimal.Parse(SuggestedRPL_Pricetxt.Text) * ((decimal.Parse(Diff_RPL_To_NCRtxt.Text) / decimal.Parse("100")));
                SuggestedPriceNCRtxt.Text = (decimal.Parse(SuggestedRPL_Pricetxt.Text) * ((decimal.Parse(Diff_RPL_To_NCRtxt.Text) / decimal.Parse("100")))).ToString("0.00");
                NCRPricetxt.Text = (decimal.Parse(SuggestedRPL_Pricetxt.Text) - (netsuggesstedcalc)).ToString("0.00");
                NCRDepoExpencetxt.Text = (decimal.Parse(lblNCRDepoExpence.Text) * (decimal.Parse(NCRPricetxt.Text) / decimal.Parse("100"))).ToString("0.00");
                NCRInteresttxt.Text = (decimal.Parse(lblNCRInterest.Text) * (decimal.Parse((NCRPricetxt.Text)) / decimal.Parse("100"))).ToString("0.00");
                NCRIncentivetxt.Text = (decimal.Parse(lblNCRIncentive.Text) * (decimal.Parse((NCRPricetxt.Text)) / decimal.Parse("100"))).ToString("0.00");
                NCRMarketingtxt.Text = (decimal.Parse(lblNCRMarketing.Text) * (decimal.Parse((NCRPricetxt.Text)) / decimal.Parse("100"))).ToString("0.00");
                NCRStaffExpencetxt.Text = (decimal.Parse(lblNCRStaffExpence.Text) * (decimal.Parse((NCRPricetxt.Text)) / decimal.Parse("100"))).ToString("0.00");

                NCROthertxt.Text = (decimal.Parse(lblNCROther.Text) * (decimal.Parse((NCRPricetxt.Text)) / decimal.Parse("100"))).ToString("0.00");

                NCRTotaltxt.Text = (decimal.Parse(NCRDepoExpencetxt.Text) + decimal.Parse(NCRInteresttxt.Text) + decimal.Parse(NCRIncentivetxt.Text) + decimal.Parse(NCRMarketingtxt.Text) + decimal.Parse(NCRStaffExpencetxt.Text) + decimal.Parse(NCROthertxt.Text)).ToString();

                NCRGrossProfitRStxt.Text = ((decimal.Parse(NCRPricetxt.Text)) - ((decimal.Parse(FinalNRVtxt.Text)))).ToString("0.00");



                if (decimal.Parse(NCRGrossProfitRStxt.Text) > 0)
                {
                    NCRGrossProfitRStxt.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    NCRGrossProfitRStxt.ForeColor = System.Drawing.Color.Red;
                }

                NCRPerGrossProfittxt.Text = (decimal.Parse(NCRGrossProfitRStxt.Text) * ((decimal.Parse("100") / decimal.Parse(FinalNRVtxt.Text)))).ToString("0.00");

                if (decimal.Parse(NCRPerGrossProfittxt.Text) > 0)
                {
                    NCRPerGrossProfittxt.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    NCRPerGrossProfittxt.ForeColor = System.Drawing.Color.Red;
                }
                NCRTotalPDtxt.Text = (decimal.Parse(string.IsNullOrEmpty(NCRPD_Schemetxt.Text.Trim()) ? "0" : NCRPD_Schemetxt.Text.Trim()) + decimal.Parse(string.IsNullOrEmpty(NCRAddiPDtxt.Text.Trim()) ? "0" : NCRAddiPDtxt.Text.Trim())).ToString();




                FinalNRCPriceWithPDtxt.Text = (decimal.Parse(NCRTotalPDtxt.Text) + (decimal.Parse(NCRPricetxt.Text))).ToString("0.00");

                NCRNetProfitRStxt.Text = (decimal.Parse(FinalNRCPriceWithPDtxt.Text) - decimal.Parse(FinalNRVtxt.Text) - decimal.Parse(NCRTotalPDtxt.Text) - decimal.Parse(NCRTotaltxt.Text)).ToString("0.00");
                if (decimal.Parse(NCRNetProfitRStxt.Text) > 0)
                {
                    NCRNetProfitRStxt.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    NCRNetProfitRStxt.ForeColor = System.Drawing.Color.Red;
                }

                NCRNetProfitRPLPertxt.Text = (decimal.Parse(NCRNetProfitRStxt.Text) * ((decimal.Parse("100") / decimal.Parse(NCRPricetxt.Text)))).ToString("0.00");
                if (decimal.Parse(NCRNetProfitRPLPertxt.Text) > 0)
                {
                    NCRNetProfitRPLPertxt.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    NCRNetProfitRPLPertxt.ForeColor = System.Drawing.Color.Red;
                }
            }

        }

        protected void StateWiseGridDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
            pro.Company_Id = Company_Id;

            if (StateWiseGridDropdownList.SelectedValue != "All State")
            {
                pro.State_Id = Convert.ToInt32(StateWiseGridDropdownList.SelectedValue);

                Grid_StatewiseFinalPrice.DataSource = cls.Get_StateWiseFinalPriceByStateId(pro);

                Grid_StatewiseFinalPrice.DataBind();
            }
            else
            {
                Grid_StatewiseFinalPrice.DataSource = cls.Get_StateWiseFinalPriceAll(pro);

                Grid_StatewiseFinalPrice.DataBind();
            }

        }

        protected void EditFinalPrice_Click(object sender, EventArgs e)
        {
            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int StatewiseFinalPrice_Id = Convert.ToInt32(Grid_StatewiseFinalPrice.DataKeys[gdrow.RowIndex].Value.ToString());
            lblStatewiseFinalPrice_Id.Text = StatewiseFinalPrice_Id.ToString();
            DataTable dt = new DataTable();
            pro.StatewiseFinalPrice_Id = StatewiseFinalPrice_Id;
            pro.Company_Id = Company_Id;
            dt = cls.Get_StateWiseFinalPriceById(pro);

            if (dt.Rows.Count > 0)
            {
                BulkProductDropDownListData();
                string BulkIdFromDB = (dt.Rows[0]["Fk_BPM_Id"] + "(" + dt.Rows[0]["Fk_PMRM_Category_Id"] + ")").ToString();
                BulkProductDropDownList.SelectedValue = BulkIdFromDB;
                //BulkProductDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_BPM_Id"]).ToString();

                string Get_BPM_Id = Regex.Match(BulkIdFromDB, @"\d+").Value;
                lblBPM_Id.Text = Get_BPM_Id;
                lblPMRMCategory_Id.Text = BulkIdFromDB.Split('(', ')')[1];
                string BracketMeasurement = BulkIdFromDB.Split('(', ')')[2];
                //lblPacksize.Text = Regex.Match(BracketMeasurement, @"\d+").Value;
                //lblpackMeasurement.Text = BracketMeasurement.Split('-', ']')[1];
                //PackingSizeDropDownListData();
                PackingStyleDropDownListData();
                StateNameDropdown.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_State_Id"]).ToString();
                SuggestedRPL_Pricetxt.Text = dt.Rows[0]["Suggested_RPL_Price"].ToString();
                ProductCategoryDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["Product_Category_Id"]).ToString();
                pro.ProductCategory_Id = Convert.ToInt32(ProductCategoryDropDownList.SelectedValue);
                pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);
                pro.Company_Id = Company_Id;
                DataTable dtState = new DataTable();
                ClsExpenceType clsState = new ClsExpenceType();
                dtState = clsState.Get_ExpenceBy_State_Id(pro);
                if (dt.Rows.Count > 0)
                {


                    lblStaffExpence.Text = dtState.Rows[0]["StaffExpense"].ToString();
                    lblDepotExpence.Text = dtState.Rows[0]["DepoExpence"].ToString();
                    lblIncentive.Text = dtState.Rows[0]["Incentive"].ToString();
                    lblInterest.Text = dtState.Rows[0]["Interest"].ToString();
                    lblRPLOther.Text = dtState.Rows[0]["Other"].ToString();
                    lblMarketing.Text = dtState.Rows[0]["Marketing"].ToString();


                    lblNCRStaffExpence.Text = dtState.Rows[1]["StaffExpense"].ToString();
                    lblNCRDepoExpence.Text = dtState.Rows[1]["DepoExpence"].ToString();
                    lblNCRIncentive.Text = dtState.Rows[1]["Incentive"].ToString();
                    lblNCRInterest.Text = dtState.Rows[1]["Interest"].ToString();
                    lblNCROther.Text = dtState.Rows[1]["Other"].ToString();
                    lblNCRMarketing.Text = dtState.Rows[1]["Marketing"].ToString();


                }
            }
            ProductTradeNameDropDown.SelectedValue = Convert.ToInt32(dt.Rows[0]["TradeName_Id"]).ToString();
            //PackingSizeDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["PackingSize"]).ToString();
            PackMeasurementDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_PackMesurement"]).ToString();
            PackingStyleDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_Packing_Style_Name_Id"]).ToString();

            ProductCategoryDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["Product_Category_Id"]).ToString();

            NRVtxt.Text = dt.Rows[0]["NRV"].ToString();
            Transporttxt.Text = dt.Rows[0]["Transport"].ToString();
            FinalNRVtxt.Text = dt.Rows[0]["FinalNRV"].ToString();
            PD_Schemetxt.Text = dt.Rows[0]["PD_Scheme"].ToString();
            AddiPDtxt.Text = dt.Rows[0]["Add_PD"].ToString(); ;
            TotalPDtxt.Text = dt.Rows[0]["Total_PD"].ToString();
            RPL_ApproProfittxt.Text = dt.Rows[0]["RPL_Approx_Profit"].ToString();
            RPL_ProfitAmttxt.Text = dt.Rows[0]["RPL_Profit_Amt"].ToString();
            SuggestedRPL_Pricetxt.Text = dt.Rows[0]["Suggested_RPL_Price"].ToString();
            STAFFEXPENCEtxt.Text = (decimal.Parse(lblStaffExpence.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100)).ToString("0.00");
            DEPOEXPENCEtxt.Text = (decimal.Parse(lblDepotExpence.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100)).ToString("0.00");
            INCENTIVEtxt.Text = (decimal.Parse(lblIncentive.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100)).ToString("0.00");
            MARKETINGTXT.Text = (decimal.Parse(lblMarketing.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100)).ToString("0.00");
            INTERESTTXT.Text = (decimal.Parse(lblInterest.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100)).ToString("0.00");
            RPLOthertxt.Text = (decimal.Parse(lblRPLOther.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100)).ToString("0.00");
            //TOTALTXT.Text = dt.Rows[0]["Total"].ToString();
            GrossProfitRStxt.Text = dt.Rows[0]["GrossProfitRs"].ToString();
            PerGrossProfittxt.Text = dt.Rows[0]["PerGrossProfit"].ToString();
            NetProfitRStxt.Text = dt.Rows[0]["RPLNetProfitAmount"].ToString();
            NetProfitRPLPertxt.Text = dt.Rows[0]["RPLNetProfitPer"].ToString();
            Diff_RPL_To_NCRtxt.Text = dt.Rows[0]["Diff_RPL_NCR"].ToString();
            SuggestedPriceNCRtxt.Text = dt.Rows[0]["RPLtoNCRDifferenceAmt"].ToString();
            NCRStaffExpencetxt.Text = (decimal.Parse(lblNCRStaffExpence.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100)).ToString("0.00");
            NCRDepoExpencetxt.Text = (decimal.Parse(lblNCRDepoExpence.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100)).ToString("0.00");
            NCRIncentivetxt.Text = (decimal.Parse(lblNCRIncentive.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100)).ToString("0.00");
            NCRMarketingtxt.Text = (decimal.Parse(lblNCRMarketing.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100)).ToString("0.00");
            NCRInteresttxt.Text = (decimal.Parse(lblNCRInterest.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100)).ToString("0.00");
            NCROthertxt.Text = (decimal.Parse(lblNCROther.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100)).ToString("0.00");

            //NCRTotaltxt.Text = dt.Rows[0]["NCR_Total"].ToString();
            NCRGrossProfitRStxt.Text = dt.Rows[0]["NCR_GrossProfitRs"].ToString();
            NCRPerGrossProfittxt.Text = dt.Rows[0]["NCR_PerGrossProfit"].ToString();
            NCRNetProfitRStxt.Text = dt.Rows[0]["NCR_NetProfitRs"].ToString();
            NCRNetProfitRPLPertxt.Text = dt.Rows[0]["NCR_NetProfitPer"].ToString();

            NCRPricetxt.Text = dt.Rows[0]["NRCPrice"].ToString();
            FinalRPLPriceWithPDtxt.Text = dt.Rows[0]["FinalRPLPriceWithPD"].ToString();
            NCRTotalPDtxt.Text = dt.Rows[0]["NRCTotal_PD"].ToString();
            NCRPD_Schemetxt.Text = dt.Rows[0]["NCR_PD_Scheme"].ToString();
            NCRAddiPDtxt.Text = dt.Rows[0]["NCR_Add_PD"].ToString();
            FinalNRCPriceWithPDtxt.Text = dt.Rows[0]["NRCFinalPrice"].ToString();
            MasterPackSizetxt.Text = dt.Rows[0]["UnitPackingSize"].ToString();


            TOTALTXT.Text = (decimal.Parse(lblStaffExpence.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100) + decimal.Parse(lblDepotExpence.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100) + decimal.Parse(lblIncentive.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100) + decimal.Parse(lblInterest.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100) + decimal.Parse(lblRPLOther.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100) + decimal.Parse(lblMarketing.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100)).ToString("0.00");

            NCRTotaltxt.Text = (decimal.Parse(lblNCRStaffExpence.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100) + decimal.Parse(lblNCRDepoExpence.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100) + decimal.Parse(lblNCRIncentive.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100) + decimal.Parse(lblNCRInterest.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100) + decimal.Parse(lblNCROther.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100) + decimal.Parse(lblNCRMarketing.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100)).ToString("0.00");


            if (lblStatewiseFinalPrice_Id.Text != "")
            {
                AddStatewiseFinalPrice.Visible = false;
                UpdateStatewiseFinalPrice.Visible = true;
                PdfReport.Visible = true;
                ExcelReport.Visible = true;
            }
            else
            {
                AddStatewiseFinalPrice.Visible = true;
                UpdateStatewiseFinalPrice.Visible = false;
                PdfReport.Visible = true;
                ExcelReport.Visible = true;
            }

            if (decimal.Parse(NCRNetProfitRPLPertxt.Text) > 0)
            {
                NCRNetProfitRPLPertxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NCRNetProfitRPLPertxt.ForeColor = System.Drawing.Color.Red;
            }
            if (decimal.Parse(NCRNetProfitRStxt.Text) > 0)
            {
                NCRNetProfitRStxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NCRNetProfitRStxt.ForeColor = System.Drawing.Color.Red;
            }
            if (decimal.Parse(NCRPerGrossProfittxt.Text) > 0)
            {
                NCRPerGrossProfittxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NCRPerGrossProfittxt.ForeColor = System.Drawing.Color.Red;
            }
            if (decimal.Parse(NCRGrossProfitRStxt.Text) > 0)
            {
                NCRGrossProfitRStxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NCRGrossProfitRStxt.ForeColor = System.Drawing.Color.Red;
            }
            if (decimal.Parse(NetProfitRPLPertxt.Text) > 0)
            {
                NetProfitRPLPertxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NetProfitRPLPertxt.ForeColor = System.Drawing.Color.Red;
            }
            if (decimal.Parse(NetProfitRStxt.Text) > 0)
            {
                NetProfitRStxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NetProfitRStxt.ForeColor = System.Drawing.Color.Red;
            }
            if (decimal.Parse(GrossProfitRStxt.Text) > 0)
            {
                GrossProfitRStxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                GrossProfitRStxt.ForeColor = System.Drawing.Color.Red;
            }


            if (decimal.Parse(PerGrossProfittxt.Text) > 0)
            {
                PerGrossProfittxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                PerGrossProfittxt.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void DelFinalPrice_Click(object sender, EventArgs e)
        {
            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int StatewiseFinalPrice_Id = Convert.ToInt32(Grid_StatewiseFinalPrice.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.StatewiseFinalPrice_Id = StatewiseFinalPrice_Id;
            pro.Company_Id = Company_Id;
            DataTable dt = new DataTable();
            dt = cls.Get_StateWiseFinalPriceById(pro);

            if (dt.Rows.Count > 0)
            {
                BulkProductDropDownListData();
                string BulkIdFromDB = (dt.Rows[0]["Fk_BPM_Id"] + "(" + dt.Rows[0]["Fk_PMRM_Category_Id"] + ")").ToString();
                BulkProductDropDownList.SelectedValue = BulkIdFromDB;
                lblState_Id.Text = dt.Rows[0]["Fk_State_Id"].ToString();

                string Get_BPM_Id = Regex.Match(BulkIdFromDB, @"\d+").Value;
                lblBPM_Id.Text = Get_BPM_Id;
                lblPMRMCategory_Id.Text = BulkIdFromDB.Split('(', ')')[1];
                pro.PMRM_Category_Id = Convert.ToInt32(lblPMRMCategory_Id.Text);
                pro.Company_Id = Company_Id;
                pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
                pro.State_Id = Convert.ToInt32(lblState_Id.Text);
            }

            try
            {
                int status = cls.Delete_StateWiseFinalPrice(pro);

                if (status > 0)
                {
                    Grid_StateWiseFinalPriceData();
                    CleareData();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Delete Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Delete Failed!')", true);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        protected void CancelStatewiseFinalPrice_Click(object sender, EventArgs e)
        {
            lblDepotExpence.Text = "0";
            lblIncentive.Text = "0";
            lblMarketing.Text = "0";
            lblInterest.Text = "0";
            lblStaffExpence.Text = "0";
            lblNCRStaffExpence.Text = "0";
            lblNCRDepoExpence.Text = "0";
            lblNCRIncentive.Text = "0";
            lblNCRMarketing.Text = "0";
            lblDepotExpence.Text = "0";
            lblNCRInterest.Text = "0";
            BulkProductDropDownList.SelectedIndex = 0;
            StateNameDropdown.SelectedIndex = 0;
            ProductTradeNameDropDown.SelectedIndex = 0;
            //PackingSizeDropDownList.SelectedIndex = -1;
            PackMeasurementDropDownList.SelectedIndex = 0;
            //PackingStyleDropDownList.SelectedIndex = 0;
            ProductCategoryDropDownList.SelectedIndex = 0;

            NRVtxt.Text = "0";
            Transporttxt.Text = "0";
            FinalNRVtxt.Text = "0";
            PD_Schemetxt.Text = "0";
            AddiPDtxt.Text = "0";
            TotalPDtxt.Text = "0";
            RPL_ApproProfittxt.Text = "0";
            RPL_ProfitAmttxt.Text = "0";
            SuggestedRPL_Pricetxt.Text = "0";
            STAFFEXPENCEtxt.Text = "0";
            DEPOEXPENCEtxt.Text = "0";
            INCENTIVEtxt.Text = "0";
            MARKETINGTXT.Text = "0";
            INTERESTTXT.Text = "0";
            TOTALTXT.Text = "0";
            GrossProfitRStxt.Text = "0";
            PerGrossProfittxt.Text = "0";
            NetProfitRStxt.Text = "0";
            NetProfitRPLPertxt.Text = "0";
            Diff_RPL_To_NCRtxt.Text = "0";
            SuggestedPriceNCRtxt.Text = "0";
            NCRStaffExpencetxt.Text = "0";
            NCRDepoExpencetxt.Text = "0";
            NCRIncentivetxt.Text = "0";
            NCRMarketingtxt.Text = "0";
            NCRInteresttxt.Text = "0";
            NCRTotaltxt.Text = "0";
            NCRGrossProfitRStxt.Text = "0";
            NCRPerGrossProfittxt.Text = "0";
            NCRNetProfitRStxt.Text = "0";
            NCRNetProfitRPLPertxt.Text = "0";
            lblStatewiseFinalPrice_Id.Text = "";
            lblBPM_Id.Text = "0";
            PackingStyleDropDownList.Enabled = false;
            //PackingSizeDropDownList.Enabled = false;
            AddStatewiseFinalPrice.Visible = true;
            UpdateStatewiseFinalPrice.Visible = false;
            FinalNRCPriceWithPDtxt.Text = "0";
            lblNCROther.Text = "0";
            lblRPLOther.Text = "0";
            NCROthertxt.Text = "0";
            RPLOthertxt.Text = "0";
            FinalRPLPriceWithPDtxt.Text = "0";
            NCRPricetxt.Text = "0";
            MasterPackSizetxt.Text = "Master Pack";
        }



        protected void PackingSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BulkProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StateNameDropdown.SelectedValue != "Select")
            {

                ProStatewisefinalPrice proCheck = new ProStatewisefinalPrice();
                ClsStatewiseFinalPrice clsCheck = new ClsStatewiseFinalPrice();
                proCheck.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);

                string Check_BPM_Id = BulkProductDropDownList.SelectedValue;
                Check_BPM_Id = Regex.Match(Check_BPM_Id, @"\d+").Value;
                proCheck.BPM_Id = Convert.ToInt32(Check_BPM_Id);
                DataTable dtCheck = new DataTable();
                dtCheck = clsCheck.CHECK_StateWiseFinalPrice(proCheck);
                if (dtCheck.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple State & Bulk Name !')", true);
                    CleareData();
                    return;
                }



                ClearText();
                ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
                DataTable dt = new DataTable();
                ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
                string BPM_Full_Id = BulkProductDropDownList.SelectedValue.ToString();
                string Get_BPM_Id = Regex.Match(BPM_Full_Id, @"\d+").Value;
                lblBPM_Id.Text = Get_BPM_Id;
                //lblPMRMCategory_Id.Text = BPM_Full_Id.Split('(', ')')[1];
                string PMRMCategory_Id = BPM_Full_Id.Split('(', ')')[1];
                lblPMRMCategory_Id.Text = PMRMCategory_Id;
                //lblPacksize.Text = Regex.Match(BracketMeasurement, @"\d+").Value;
                //lblpackMeasurement.Text = BracketMeasurement.Split('-', ']')[1];
                //lblPackSize.Text = PackSize;
                //lblPMRM_Category_Id.Text = Shipper_Id;

                //lblPackMeasurement.Text = PackUniMeasurement;

                pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
                pro.PMRM_Category_Id = Convert.ToInt32(PMRMCategory_Id);
                pro.Company_Id = Company_Id;
                pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);
                //pro.PackSize = Convert.ToInt32(lblPacksize.Text);
                //pro.PackMeasurement = Convert.ToInt32(lblpackMeasurement.Text);

                dt = cls.Get_StatewiseDataFrom_PackSize_BPM_Id(pro);
                if (dt.Rows.Count > 0)
                {
                    //PackingSizeDropDownListData();
                    //string BulkIdFromDB = (dt.Rows[0]["Fk_BPM_Id"] + "("+ dt.Rows[0]["Fk_UnitMeasurement_Id"] + ")").ToString();
                    //BulkProductDropDownList.SelectedValue = BulkIdFromDB;
                    ProductCategoryDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["ProductCategory_Id"]).ToString();
                    lblProductCategory_Id.Text = Convert.ToInt32(dt.Rows[0]["ProductCategory_Id"]).ToString();
                    PackingStyleDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["PackingStyleName_Id"]).ToString();
                    //PackMeasurementDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_UnitMeasurement"]).ToString();
                    NRVtxt.Text = dt.Rows[0]["NRV"].ToString();
                    Transporttxt.Text = dt.Rows[0]["Transport"].ToString();
                    //lblPacksize.Text = (dt.Rows[0]["Packing_Size"]).ToString();
                    //lblpackMeasurement.Text = dt.Rows[0]["Fk_UnitMeasurement"].ToString();
                    ProductTradeNameDropDown.SelectedValue = dt.Rows[0]["TradeName_Id"].ToString();
                    //lblBPM_Id.Text = (BulkProductDropDownList.SelectedValue);
                    //PackingSizeDropDownList.Enabled = true;
                    //BulkProductDropDownList.Enabled = true;
                    //PackingStyleDropDownList.Enabled = true;




                    //PackingSizeDropDownListData();
                    PackingStyleDropDownListData();

                    //ClsTransportationCostMaster clsTranspor = new ClsTransportationCostMaster();
                    //DataTable DtTransport = new DataTable();
                    //pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);
                    //pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
                    //pro.Company_Id = Company_Id;
                    //pro.PMRM_Category_Id = Convert.ToInt32(lblPMRMCategory_Id.Text);
                    //DtTransport = clsTranspor.Get_TransportationBy_State_BPM_Id(pro);
                    //if (DtTransport.Rows.Count > 0)
                    //{
                    //    Transporttxt.Text = DtTransport.Rows[0]["CostPerLtrKG"].ToString();

                    //}
                    if (NRVtxt.Text != "" && Transporttxt.Text != "")
                    {
                        FinalNRVtxt.Text = (decimal.Parse(NRVtxt.Text) + decimal.Parse(Transporttxt.Text)).ToString("0.00");
                    }

                    //DataTable dtMasterPack = new DataTable();
                    //ClsPackingMateriaMaster clsMasterPack = new ClsPackingMateriaMaster();
                    //ClsPackingMateriaMaster proMasterPack = new ClsPackingMateriaMaster();
                    //dtMasterPack = clsMasterPack.GetMasterPackSizeByBPM_Id(UserId, Convert.ToInt32(lblBPM_Id.Text), Convert.ToInt32(lblPMRMCategory_Id.Text));
                    //MasterPackSizetxt.Text = dtMasterPack.Rows[0]["TotalPackSize"].ToString();
                    MasterPackSizetxt.Text = dt.Rows[0]["PackingUnitMeasurement"].ToString();


                }
                pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);
                pro.ProductCategory_Id = Convert.ToInt32(lblProductCategory_Id.Text);

                pro.Company_Id = Company_Id;
                DataTable dtState = new DataTable();
                ClsExpenceType clsState = new ClsExpenceType();
                dtState = clsState.Get_ExpenceBy_State_Id(pro);
                if (dtState.Rows.Count > 0)
                {


                    lblStaffExpence.Text = dtState.Rows[0]["StaffExpense"].ToString();
                    lblDepotExpence.Text = dtState.Rows[0]["DepoExpence"].ToString();
                    lblIncentive.Text = dtState.Rows[0]["Incentive"].ToString();
                    lblInterest.Text = dtState.Rows[0]["Interest"].ToString();
                    lblRPLOther.Text = dtState.Rows[0]["Other"].ToString();
                    lblMarketing.Text = dtState.Rows[0]["Marketing"].ToString();

                    TOTALTXT.Text = (decimal.Parse(lblStaffExpence.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100) + decimal.Parse(lblDepotExpence.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100) + decimal.Parse(lblIncentive.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100) + decimal.Parse(lblInterest.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100) + decimal.Parse(lblRPLOther.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100) + decimal.Parse(lblMarketing.Text) * (decimal.Parse(SuggestedRPL_Pricetxt.Text) / 100)).ToString("0.00");

                    lblNCRStaffExpence.Text = dtState.Rows[1]["StaffExpense"].ToString();
                    lblNCRDepoExpence.Text = dtState.Rows[1]["DepoExpence"].ToString();
                    lblNCRIncentive.Text = dtState.Rows[1]["Incentive"].ToString();
                    lblNCRInterest.Text = dtState.Rows[1]["Interest"].ToString();
                    lblNCROther.Text = dtState.Rows[1]["Other"].ToString();
                    lblNCRMarketing.Text = dtState.Rows[1]["Marketing"].ToString();
                    NCRTotaltxt.Text = (decimal.Parse(lblNCRStaffExpence.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100) + decimal.Parse(lblNCRDepoExpence.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100) + decimal.Parse(lblNCRIncentive.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100) + decimal.Parse(lblNCRInterest.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100) + decimal.Parse(lblNCROther.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100) + decimal.Parse(lblNCRMarketing.Text) * (decimal.Parse(SuggestedPriceNCRtxt.Text) / 100)).ToString("0.00");


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select State Expence Not found!')", true);
                    CleareData();
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select State Before Continue')", true);

            }


        }

        public void ClearText()
        {
            //lblDepotExpence.Text = "0";
            //lblIncentive.Text = "0";
            //lblMarketing.Text = "0";
            //lblInterest.Text = "0";
            //lblStaffExpence.Text = "0";
            //lblNCRStaffExpence.Text = "0";
            //lblNCRDepoExpence.Text = "0";
            //lblNCRIncentive.Text = "0";
            //lblNCRMarketing.Text = "0";
            //lblDepotExpence.Text = "0";
            //lblNCRInterest.Text = "0";
            NRVtxt.Text = "0";
            Transporttxt.Text = "0";
            FinalNRVtxt.Text = "0";
            PD_Schemetxt.Text = "0";
            AddiPDtxt.Text = "0";
            TotalPDtxt.Text = "0";
            RPL_ApproProfittxt.Text = "0";
            RPL_ProfitAmttxt.Text = "0";
            SuggestedRPL_Pricetxt.Text = "0";
            STAFFEXPENCEtxt.Text = "0";
            DEPOEXPENCEtxt.Text = "0";
            INCENTIVEtxt.Text = "0";
            MARKETINGTXT.Text = "0";
            INTERESTTXT.Text = "0";
            TOTALTXT.Text = "0";
            GrossProfitRStxt.Text = "0";
            PerGrossProfittxt.Text = "0";
            NetProfitRStxt.Text = "0";
            NetProfitRPLPertxt.Text = "0";
            Diff_RPL_To_NCRtxt.Text = "0";
            SuggestedPriceNCRtxt.Text = "0";
            NCRStaffExpencetxt.Text = "0";
            NCRDepoExpencetxt.Text = "0";
            NCRIncentivetxt.Text = "0";
            NCRMarketingtxt.Text = "0";
            NCRInteresttxt.Text = "0";
            NCRTotaltxt.Text = "0";
            NCRGrossProfitRStxt.Text = "0";
            NCRPerGrossProfittxt.Text = "0";
            NCRNetProfitRStxt.Text = "0";
            NCRNetProfitRPLPertxt.Text = "0";
            lblStatewiseFinalPrice_Id.Text = "";
            lblBPM_Id.Text = "0";
            FinalNRCPriceWithPDtxt.Text = "0";
            NCROthertxt.Text = "0";
            RPLOthertxt.Text = "0";
            lblNCROther.Text = "0";
            lblRPLOther.Text = "0";
            NCRPricetxt.Text = "0";
            FinalRPLPriceWithPDtxt.Text = "0";

        }
        protected void ChkBulkSubmit_Click1(object sender, EventArgs e)
        {
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            string YrStrList = "";
            foreach (System.Web.UI.WebControls.ListItem listItem in BulkProductListbox.Items)
            {
                if (listItem.Selected)
                {


                    YrStrList = YrStrList + listItem.Value + ",";

                }

            }

            if (StateWiseGridDropdownList.SelectedValue == "All State")
            {
                pro.State_Id = 0;
            }
            else
            {
                pro.State_Id = Convert.ToInt32(StateWiseGridDropdownList.SelectedValue);
            }
            if (YrStrList == "" || YrStrList == "Select,")
            {
                YrStrList = "0";
            }
            else
            {
                YrStrList = YrStrList.Remove(YrStrList.Length - 1);

            }

            if (MasterPackAllSizeDropdown.SelectedValue == "1")

            {
                pro.MasterPack = 1;
            }
            else
            {
                pro.MasterPack = 0;
            }
            string sBulkListStr = YrStrList;

            pro.Company_Id = Company_Id;
            string StateValue = StateWiseGridDropdownList.SelectedValue;

            Grid_StatewiseFinalPrice.DataSource = cls.Get_StateWiseFinalPriceByMasterPack(pro, sBulkListStr);
            Grid_StatewiseFinalPrice.DataBind();
        }

        protected void MasterPackAllSizeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();
            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            string YrStrList = "";
            foreach (System.Web.UI.WebControls.ListItem listItem in BulkProductListbox.Items)
            {
                if (listItem.Selected)
                {


                    YrStrList = YrStrList + listItem.Value + ",";

                }

            }

            if (StateWiseGridDropdownList.SelectedValue == "All State")
            {
                pro.State_Id = 0;
            }
            else
            {
                pro.State_Id = Convert.ToInt32(StateWiseGridDropdownList.SelectedValue);
            }
            if (YrStrList == "" || YrStrList == "Select")
            {
                YrStrList = "0";
            }
            else
            {
                YrStrList = YrStrList.Remove(YrStrList.Length - 1);

            }

            if (MasterPackAllSizeDropdown.SelectedValue == "1")

            {
                pro.MasterPack = 1;
            }
            else
            {
                pro.MasterPack = 0;
            }
            string sBulkListStr = YrStrList;

            pro.Company_Id = Company_Id;
            string StateValue = StateWiseGridDropdownList.SelectedValue;

            Grid_StatewiseFinalPrice.DataSource = cls.Get_StateWiseFinalPriceByMasterPack(pro, sBulkListStr);
            Grid_StatewiseFinalPrice.DataBind();
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "StatewisefinalPrice.xlsx"));
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Grid_StatewiseFinalPrice.AllowPaging = false;
            //Grid_comp_FactoryExpenceListData();
            //Change the Header Row back to white color
            Grid_StatewiseFinalPrice.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < Grid_StatewiseFinalPrice.HeaderRow.Cells.Count; i++)
            {
                Grid_StatewiseFinalPrice.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            Grid_StatewiseFinalPrice.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        [Obsolete]
        protected void PdfReport_Click(object sender, EventArgs e)
        {
            ExportGridToPDF();
        }

        [Obsolete]
        private void ExportGridToPDF()
        {

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    Grid_StatewiseFinalPrice.AllowPaging = false;
                    //this.Comp_Get_CompanyFectoryExpenceAllReport();

                    Grid_StatewiseFinalPrice.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StatewiseFinalPrice.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        [Obsolete]
        protected void ExcelReport_Click(object sender, EventArgs e)
        {
            ExportGridToExcel(Grid_StatewiseFinalPrice);
        }

        protected void NCRPD_Schemetxt_TextChanged(object sender, EventArgs e)
        {
            NCRTotalPDtxt.Text = (decimal.Parse(string.IsNullOrEmpty(NCRPD_Schemetxt.Text.Trim()) ? "0" : NCRPD_Schemetxt.Text.Trim()) + decimal.Parse(
               string.IsNullOrEmpty(NCRAddiPDtxt.Text.Trim()) ? "0" : NCRAddiPDtxt.Text.Trim())).ToString();


            FinalNRCPriceWithPDtxt.Text = (decimal.Parse(NCRTotalPDtxt.Text) + (decimal.Parse(NCRPricetxt.Text))).ToString("0.00");

            NCRNetProfitRStxt.Text = (decimal.Parse(FinalNRCPriceWithPDtxt.Text) - decimal.Parse(FinalNRVtxt.Text) - decimal.Parse(NCRTotalPDtxt.Text) - decimal.Parse(NCRTotaltxt.Text)).ToString("0.00");


            NCRNetProfitRPLPertxt.Text = (decimal.Parse(NCRNetProfitRStxt.Text) * ((decimal.Parse("100") / decimal.Parse(NCRPricetxt.Text)))).ToString("0.00");
            if (decimal.Parse(NCRNetProfitRPLPertxt.Text) > 0)
            {
                NCRNetProfitRPLPertxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NCRNetProfitRPLPertxt.ForeColor = System.Drawing.Color.Red;
            }
            if (decimal.Parse(NCRNetProfitRStxt.Text) > 0)
            {
                NCRNetProfitRStxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NCRNetProfitRStxt.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void NCRAddiPDtxt_TextChanged(object sender, EventArgs e)
        {


            NCRTotalPDtxt.Text = (decimal.Parse(string.IsNullOrEmpty(NCRPD_Schemetxt.Text.Trim()) ? "0" : NCRPD_Schemetxt.Text.Trim()) + decimal.Parse(
               string.IsNullOrEmpty(NCRAddiPDtxt.Text.Trim()) ? "0" : NCRAddiPDtxt.Text.Trim())).ToString();



            FinalNRCPriceWithPDtxt.Text = (decimal.Parse(NCRTotalPDtxt.Text) + (decimal.Parse(NCRPricetxt.Text))).ToString("0.00");

            NCRNetProfitRStxt.Text = (decimal.Parse(FinalNRCPriceWithPDtxt.Text) - decimal.Parse(FinalNRVtxt.Text) - decimal.Parse(NCRTotalPDtxt.Text) - decimal.Parse(NCRTotaltxt.Text)).ToString("0.00");


            NCRNetProfitRPLPertxt.Text = (decimal.Parse(NCRNetProfitRStxt.Text) * ((decimal.Parse("100") / decimal.Parse(NCRPricetxt.Text)))).ToString("0.00");

            if (decimal.Parse(NCRNetProfitRPLPertxt.Text) > 0)
            {
                NCRNetProfitRPLPertxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NCRNetProfitRPLPertxt.ForeColor = System.Drawing.Color.Red;
            }
            if (decimal.Parse(NCRNetProfitRStxt.Text) > 0)
            {
                NCRNetProfitRStxt.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                NCRNetProfitRStxt.ForeColor = System.Drawing.Color.Red;
            }
        }




    }

}

