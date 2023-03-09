using BusinessAccessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace Production_Costing_Software
{
    public partial class BulkRecipe_BOM_ : System.Web.UI.Page
    {
        ClsBulkRecipeBOM cls = new ClsBulkRecipeBOM();
        ProBulkRecipeBOM pro = new ProBulkRecipeBOM();
        int UserId;
        //string SubstringSearch = "";

        decimal SubTotalInputTechnial;
        decimal SubTotalAmount;
        int Solvent = 0;
        string FormulationWithValue = "0";
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

                MainCategoryDataCombo();
                MeasurementDataCombo();
                BulkProductDropDownListCombo();

                Grid_BR_BOM_MasterData();
                FormulationSelectionCombo();
                //FormulationDropdown.Items.Insert(0, "Select");
                //ChkReqruiredFormulation.Checked = true;
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[3]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[3]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[3]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                AddBRbtn.Visible = true;
            }
            else
            {
                AddBRbtn.Visible = false;
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[3]["GroupId"]);
            if (lblCanEdit.Text == "True")
            {
                AddBRbtn.Visible = true;
            }
            else
            {
                AddBRbtn.Visible = false;
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
                pro.User_Id = Convert.ToInt32(Session["UserId"]);
                UserId = pro.User_Id;
                UserId = Convert.ToInt32(Session["UserId"]);

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void FormulationSelectionCombo()
        {
            ClsFormulationMaster cls = new ClsFormulationMaster();

            FormulationDropdown.DataSource = cls.Get_FormulationMasterALL(UserId);

            FormulationDropdown.DataTextField = "FormulationName";
            FormulationDropdown.DataValueField = "FM_Id";
            //FormulationDropdown.Items.Insert(0, "Select");
            FormulationDropdown.DataBind();

            FormulationDropdown.Items.Insert(0, "Select");

        }
        public void MainCategoryDataCombo()
        {
            ClsMainCategoryMaster cls = new ClsMainCategoryMaster();
            MainCategoryDropdown.DataSource = cls.GetMainCategoryData();

            MainCategoryDropdown.DataTextField = "MainCategory_Name";
            MainCategoryDropdown.DataValueField = "PkMainCategory_Id";
            MainCategoryDropdown.DataBind();
            MainCategoryDropdown.Items.Insert(0, "Select");

        }
        public void MeasurementDataCombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            MeasurementDropdown.DataSource = cls.GetEnumMasterMeasurement();

            MeasurementDropdown.DataTextField = "EnumDescription";
            MeasurementDropdown.DataValueField = "PkEnumId";
            MeasurementDropdown.DataBind();
            MeasurementDropdown.Items.Insert(0, "Select");

        }

        protected void MainCategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (MainCategoryDropdown.SelectedIndex == 0)
            //{
            //    ClsBulkProductMaster clsBPM = new ClsBulkProductMaster();
            //    BulkProductDropDownList.DataSource = clsBPM.Get_BP_MasterDataCombo(UserId);
            //    BulkProductDropDownList.DataTextField = "BulkProductName";
            //    BulkProductDropDownList.DataValueField = "BPM_Product_Id";

            //    BulkProductDropDownList.DataBind();
            //    //BulkProductDropDownList.Items.Insert(0, "Select");

            //    Grid_BR_BOM_MasterDataList.DataSource = cls.Get_BR_BOM_MasterDataALL(UserId);
            //    Grid_BR_BOM_MasterDataList.DataBind();
            //}
            //else
            //{
            //    int MainCategory_Id = Convert.ToInt32(MainCategoryDropdown.SelectedValue);

            //    BulkProductDropDownList.DataSource = cls.Get_BP_MasterDataComboMainById(UserId, MainCategory_Id);
            //    BulkProductDropDownList.DataTextField = "BPM_Product_Name";
            //    BulkProductDropDownList.DataValueField = "BPM_Product_Id";
            //    BulkProductDropDownList.DataBind();
            //    //BulkProductDropDownList.Items.Insert(0, "Select");

            //    Grid_BR_BOM_MasterDataList.DataSource = cls.Get_BR_BOM_MasterDataByMainCategoryId(UserId, MainCategory_Id);
            //    Grid_BR_BOM_MasterDataList.DataBind();

            //}
        }

        public void BulkProductDropDownListCombo()
        {
            ClsBulkProductMaster cls = new ClsBulkProductMaster();
            BulkProductDropDownList.DataSource = cls.Get_BP_MasterDataCombo(UserId);
            BulkProductDropDownList.DataTextField = "BulkProductName";
            BulkProductDropDownList.DataValueField = "BPM_Product_Id";
            BulkProductDropDownList.DataBind();
            BulkProductDropDownList.Items.Insert(0, "Select");
        }

        protected void AddBRbtn_Click(object sender, EventArgs e)
        {
            pro.MainCategory_Id = Convert.ToInt32(MainCategoryDropdown.SelectedValue);
            pro.EnumMaster_UnitMeasurement_Id = Convert.ToInt32(MeasurementDropdown.SelectedValue);
            pro.BPM_Product_Id = Convert.ToInt32(BulkProductDropDownList.SelectedValue);
            pro.BR_BOM_BatchSize = Convert.ToInt32(BatchSizetxt.Text);
            pro.User_Id = UserId;
            int status = cls.Insert_BR_BOM_MasterData(pro);

            if (status > 0)
            {
                Grid_BR_BOM_MasterData();
            }

            Response.Redirect(Request.Url.AbsoluteUri);
        }
        public void Grid_BR_BOM_MasterData()
        {
            DataTable dt = new DataTable();

            Grid_BR_BOM_MasterDataList.DataSource = dt = cls.Get_BP_MasterDataAll(UserId);
            Grid_BR_BOM_MasterDataList.DataBind();


        }


        protected void InputTechnicalBtn_Click(object sender, EventArgs e)
        {
            //ClearData();

            Searchtxt.Text = "";
            RequiredFormulationtxt.Text = "";
            InputKGtxt.Text = "";
            //ChkReqruiredFormulation.Checked = true;
            //ChkFormulationDropDown.Checked = false;


            ProBulkRecipeBOM pro = new ProBulkRecipeBOM();
            Button Add = sender as Button;
            GridViewRow gdrow = Add.NamingContainer as GridViewRow;
            pro.BR_BOM_Id = Convert.ToInt32(Grid_BR_BOM_MasterDataList.DataKeys[gdrow.RowIndex].Value.ToString());
            DataTable dt1 = new DataTable();
            dt1 = cls.Get_BR_BOM_MasterDataById(pro, UserId);
            lblBR_BOM_Id.Text = pro.BR_BOM_Id.ToString();


            pro.MainCategory_Id = Convert.ToInt32(dt1.Rows[0]["Fk_MainCategory_Id"].ToString());
            lblMainCategoryName.Text = (dt1.Rows[0]["MainCategory_Name"].ToString());
            lblMainCategoryId.Text = (dt1.Rows[0]["Fk_MainCategory_Id"].ToString());
            lblBatchSize.Text = (dt1.Rows[0]["BatchSize"].ToString());

            lblBPM_Name.Text = (dt1.Rows[0]["BPM_Product_Name"].ToString());
            lblBPM_Product_Id.Text = (dt1.Rows[0]["BPM_Product_Id"].ToString());
            lblBOM_Batchsize.Text = (dt1.Rows[0]["EnumDescription"].ToString());
            lblMeasurement.Text = lblBOM_Batchsize.Text;
            BatchSizeInputtxt.Text = lblBatchSize.Text;

            //if (dt1.Rows.Count > 0)
            //{
            //    AddFinalBOM.Visible = false;
            //    UpdateFinalBOM.Visible = true;
            //}

            DataTable dt = new DataTable();
            FormulationSelectionCombo();

            IngrediantGridviewData();

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
            InputFormualtion.Visible = true;
            HideSolventTxt.Visible = true;
            //DefaultValuesSPGR();
        }


        [WebMethod]
        public static List<string> SearchRMData(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_AutoComplete_Search_RM";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchRM_Name", prefixText);

                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            customers.Add(sdr["SearchText"].ToString());

                        }
                    }

                    conn.Close();

                    return customers;
                }
            }
        }

        [WebMethod]
        public static List<string> SearchBPMData(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_SearchBulkProductfromBRBOM";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchBulkProduct", prefixText);

                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            customers.Add(sdr["BulkProductName"].ToString());

                        }
                    }

                    conn.Close();

                    return customers;
                }
            }
        }

        //[WebMethod]
        string PurityPercentfloat = "";
        string PurityPercent = "";
        protected void Searchtxt_TextChanged(object sender, EventArgs e)
        {

            string SearchRM = Searchtxt.Text;
            string SubstringSearch = "";

            string PurityPercentWithBraket = Regex.Match(SearchRM, @"\(.*?\)").Value;
            if (PurityPercentWithBraket.Contains("."))
            {
                PurityPercentfloat = Regex.Match(PurityPercentWithBraket, @"\d+.+\d").Value;
                if (SearchRM.Contains("Purity"))
                {
                    int iPurityIndex = SearchRM.IndexOf("(Purity");
                    SubstringSearch = SearchRM.Substring(0, iPurityIndex).Trim();
                    //bstringSearch = SearchRM.Remove(SearchRM.Length - 14).Trim();

                }
                else
                {
                    SubstringSearch = SearchRM;
                }
            }
            else
            {
                PurityPercent = Regex.Match(PurityPercentWithBraket, @"\d+").Value;
                if (SearchRM.Contains("Purity"))
                {
                    // SubstringSearch = SearchRM.Remove(SearchRM.Length - 13);
                    int iPurityIndex = SearchRM.IndexOf("(Purity");
                    SubstringSearch = SearchRM.Substring(0, iPurityIndex).Trim();
                }
                else
                {
                    SubstringSearch = SearchRM;
                }
            }
            //if (SearchRM.Contains("Purity"))
            //{
            //    SubstringSearch = SearchRM.Remove(SearchRM.Length - 13);

            //}
            //else
            //{
            //    SubstringSearch = SearchRM;
            //}

            DataTable dt = new DataTable();
            DateTime Datenow = DateTime.Now;
            if (PurityPercentfloat != "")
            {
                dt = cls.SearchRMDataWithPurityFloat(SubstringSearch, PurityPercentfloat);

            }
            else
            {
                dt = cls.SearchRMDataWithPurity(SubstringSearch, PurityPercent);

            }
            string purity = dt.Rows[0]["RM_IsPurityRequired"].ToString();
            int RM_Category_Id = Convert.ToInt32(dt.Rows[0]["Fk_RM_Category_Id"]);


            if (purity == "False")
            {
                Searchtxt.Text = dt.Rows[0]["RM_Name"].ToString();
                lblRMId.Text = dt.Rows[0]["RM_Id"].ToString();
                lblPriceMaster_Id.Text = dt.Rows[0]["RM_Price_Id"].ToString();

            }
            else
            {
                dt.Columns.Add("RM_Purity", typeof(string), "RM_Name + ' (Purity- ' + Purity +'%)'").ToString();
                Searchtxt.Text = dt.Rows[0]["RM_Purity"].ToString();
                lblRMId.Text = dt.Rows[0]["RM_Id"].ToString();
                lblPurityPercent.Text = dt.Rows[0]["Purity"].ToString();
                lblPriceMaster_Id.Text = dt.Rows[0]["RM_Price_Id"].ToString();

            }




            //dt = cls.Get_BOM_InputTechnical_MultiSelectCombo(UserId);

            //dt.Columns.Add("RM_Purity", typeof(string), "RM_Name + ' (Purity- ' + Purity +'%)'").ToString();
            //MultiRMPurityList.DataSource = dt;
            //MultiRMPurityList.DataTextField = "RM_Purity";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);

        }

        protected void MainCategoryDropdown_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //MainCategoryDropdownList.DataSource = cls.GetMainCategoryData();

            //MainCategoryDropdownList.DataTextField = "MainCategory_Name";
            //MainCategoryDropdownList.DataValueField = "PkMainCategory_Id";
            //MainCategoryDropdownList.Items.Insert(0, "Select");
            //MainCategoryDropdownList.DataBind();
        }

        protected void ChkReqruiredFormulation_CheckedChanged(object sender, EventArgs e)
        {

            if (ChkReqruiredFormulation.Checked == true)
            {
                RequiredFormulationtxt.Visible = true;
                InputKGtxt.Visible = false;
                lblInputKG.Visible = false;

            }
            else
            {
                RequiredFormulationtxt.Visible = false;
                InputKGtxt.Visible = true;
                lblInputKG.Visible = true;
            }
        }
        public void IngrediantGridviewData()
        {
            DataTable dt = new DataTable();

            ClsBulkRecipeBOM cls = new ClsBulkRecipeBOM();
            dt = cls.GetIngredientMaster(UserId, Convert.ToInt32(lblBR_BOM_Id.Text), Convert.ToInt32(lblMainCategoryId.Text));
            GridBOM_Formulation.DataSource = dt;
            GridBOM_Formulation.DataBind();

            if (dt.Rows.Count > 0)
            {
                lblBR_BOM_Id.Text = dt.Rows[0]["BR_BOM_Id"].ToString();
            }
            else
            {
                InputSubTotaltxt.Text = "0.00";
                TotalAmounttxt.Text = "0.00";
                lblGridSubTotalAmount.Text = "0.00";
            }

            DataTable dt1 = new DataTable();
            dt1 = cls.GetIngredientMasterAndFinalBulkCostById(UserId, Convert.ToInt32(lblBR_BOM_Id.Text));
            if (dt.Rows.Count > 0)
            {
                if (dt1.Rows.Count > 0)
                {
                    lblFinalCostBulk_BRBOM_Id.Text = dt1.Rows[0]["FinalCostBulk_BRBOM_Id"].ToString();

                    FormulationWithValue = Convert.ToInt32(dt1.Rows[0]["FormulationWith"]).ToString();

                    FormulationAmounttxt.Text = dt1.Rows[0]["FormulationAmount"].ToString();
                    BatchSizeInputtxt.Text = dt1.Rows[0]["BatchSizeInput"].ToString();
                    SPGRtxt.Text = Convert.ToDecimal(dt1.Rows[0]["SPGR"]).ToString("0.00");
                    TotalAmounttxt.Text = Convert.ToDecimal(dt1.Rows[0]["TotalAmount"]).ToString("0.00");
                    TotalOutput_LTR.Text = Convert.ToDecimal(dt1.Rows[0]["TotalOutput_LTR"]).ToString("0.00");
                    Costtxt.Text = Convert.ToDecimal(dt1.Rows[0]["TotalCost"]).ToString("0.00");
                    FormulationLosttxt.Text = Convert.ToDecimal(dt1.Rows[0]["FormulationLost"]).ToString("0.00");
                    FormulationALostmounttxt.Text = Convert.ToDecimal(dt1.Rows[0]["FormulationLostAmount"]).ToString("0.00");
                    FinalCostBulktxt.Text = Convert.ToDecimal(dt1.Rows[0]["FinalCostBulk"]).ToString("0.00");
                    InputSubTotaltxt.Text = SubTotalInputTechnial.ToString();
                    TotalAmounttxt.Text = SubTotalAmount.ToString();

                    if (FormulationWithValue == "0")
                    {
                        FormulationDropdown.SelectedIndex = -1;
                        ChkFormulationDropDown.Checked = false;
                    }
                    else
                    {
                        FormulationDropdown.Enabled = false;
                        FormulationDropdown.SelectedValue = Convert.ToInt32(dt1.Rows[0]["FormulationWith"]).ToString();
                        ChkFormulationDropDown.Checked = true;
                        ChkFormulationDropDown_CheckedChanged(null, null);
                        FormulationDropdown_SelectedIndexChanged(null, null);
                    }
                    //lblGridSubTotalAmount.Text = TotalAmounttxt.Text;

                    UpdateFinalBOM.Visible = true;
                    AddFinalBOM.Visible = false;
                }
                else
                {
                    ClearData();
                    DefaultValuesSPGR();
                }



                //    pro.SubTotalInputTechnical = float.Parse(InputSubTotaltxt.Text);
                //    pro.SubTotalAmount = float.Parse(TotalAmounttxt.Text);
                //    pro.FormulationWith = Convert.ToInt32(FormulationDropdown.SelectedValue);
                //    pro.FormulationAmount = float.Parse(FormulationAmounttxt.Text);
                //    pro.BatchSize_InputKg = float.Parse(BatchSizeInputtxt.Text);
                //    pro.SPGR = float.Parse(SPGRtxt.Text);
                //    pro.TotalAmount = float.Parse(TotalAmounttxt.Text);
                //    pro.TotalOutput_LTR = float.Parse(TotalOutput_LTR.Text);
                //    pro.TotalCost = float.Parse(Costtxt.Text);
                //    pro.FormulationLost = float.Parse(FormulationLosttxt.Text);
                //    pro.FolrmulationLostAmount = float.Parse(FormulationALostmounttxt.Text);
                //    pro.FinalCostBulk = float.Parse(FinalCostBulktxt.Text);
                //    pro.User_Id = UserId;
                //    pro.BR_BOM_Id = Convert.ToInt32(lblBR_BOM_Id.Text);
                //    pro.BPM_Product_Id = Convert.ToInt32(lblBPM_Product_Id.Text);


                //    UpdateFinalBOM.Visible = true;
                //    AddFinalBOM.Visible = false;
                //    int status = 0;
                //    status = cls.UpdateFinalCostBulk_BRBOM(UserId, Convert.ToInt32(lblMainCategoryId.Text), Convert.ToInt32(lblBR_BOM_Id.Text), Convert.ToInt32(lblFinalCostBulk_BRBOM_Id.Text), pro);
                //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

                //}
                //else
                //{
                //    ClearData();
                //    UpdateFinalBOM.Visible = false;
                //    AddFinalBOM.Visible = true;
                //}

            }
            else
            {
                ClearData();
                DefaultValuesSPGR();
            }
            if (lblFinalCostBulk_BRBOM_Id.Text != "")
            {
                UpdateFinalBOM.Visible = true;
                AddFinalBOM.Visible = false;
            }
            else
            {
                UpdateFinalBOM.Visible = false;
                AddFinalBOM.Visible = true;
            }


        }
        public void ClearData()
        {
            FormulationAmounttxt.Text = "0.00";
            //BatchSizeInputtxt.Text = "";
            SPGRtxt.Text = "0.00";

            TotalOutput_LTR.Text = "0.00";
            FormulationLosttxt.Text = "0.00";
            if (FormulationALostmounttxt.Enabled == true)
            {
                FormulationALostmounttxt.Enabled = false;
                FormulationAmounttxt.Text = "0.00";
            }
            FormulationALostmounttxt.Enabled = true;

            Solventtxt.Text = "0.00";
            if (FinalCostBulktxt.Text != "0.00")
            {
                UpdateFinalBOM.Visible = true;
                AddFinalBOM.Visible = false;
            }
            else
            {
                UpdateFinalBOM.Visible = false;
                AddFinalBOM.Visible = true;

            }

            if (FormulationDropdown.Enabled == false)
            {
                Costtxt.Text = TotalAmounttxt.Text;
                FinalCostBulktxt.Text = TotalAmounttxt.Text;
            }
            else
            {
                Costtxt.Text = "0";
                FinalCostBulktxt.Text = "0";
                TotalAmounttxt.Text = "0";
            }
            //ChkReqruiredFormulation.Checked = true;
            //RequiredFormulationtxt.Visible = true;
            //InputKGtxt.Visible = false;

        }
        protected void AddInputTechnicalBtn_Click(object sender, EventArgs e)
        {

            //ClearcData();
            if (BatchSizeInputtxt.Text == InputSubTotaltxt.Text)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add More Qty')", true);

            }
            else
            {
                ProBulkRecipeBOM pro = new ProBulkRecipeBOM();
                ClsBulkRecipeBOM cls = new ClsBulkRecipeBOM();
                pro.RM_Id = Convert.ToInt32(lblRMId.Text);
                pro.PriceMaster_Id = Convert.ToInt32(lblPriceMaster_Id.Text);
                pro.BPM_Product_Id = Convert.ToInt32(lblBPM_Product_Id.Text);
                pro.Ingrdient_Name = Searchtxt.Text;
                pro.User_Id = UserId;
                pro.MainCategory_Id = Convert.ToInt32(lblMainCategoryId.Text);
                //pro.BR_BOM_BatchSize = Convert.ToInt32(lblBatchSize.Text);
                if (Solventtxt.Text != "")
                {
                    pro.Solvent = decimal.Parse(Solventtxt.Text);

                }
                else
                {
                    pro.Solvent = 0;

                }
                //bool ForumulationRequired = ChkReqruiredFormulation.Checked;
                if (ChkReqruiredFormulation.Checked == true)
                {
                    pro.isForumulationRequired = true;
                    if (RequiredFormulationtxt.Text == "")
                    {
                        pro.Formulation = 0;
                    }
                    else
                    {
                        pro.Formulation = decimal.Parse(RequiredFormulationtxt.Text);

                    }
                }
                else
                {
                    pro.isForumulationRequired = false;
                    if (InputKGtxt.Text == "")
                    {
                        pro.BatchSize_InputKg = 0;
                    }
                    else
                    {
                        pro.BatchSize_InputKg = decimal.Parse(InputKGtxt.Text);

                    }
                }
                try
                {
                    pro.BR_BOM_Id = Convert.ToInt32(lblBR_BOM_Id.Text);
                    if (lblPurityPercent.Text != "")
                    {
                        pro.PurityPercent = float.Parse(lblPurityPercent.Text);

                    }
                    else
                    {
                        pro.PurityPercent = 0;

                    }
                    int status = cls.InsertIngredientMasterData(pro);
                    if (status > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                        ClearData();
                        DefaultValuesSPGR();
                        IngrediantGridviewData();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

                    }





                }
                catch (Exception)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "Data Not Inserted !');", true);

                    throw;
                }
            }

            //string Search = Searchtxt.Text;

            //        string SearchTxt = Search.Substring(0, Search.Length - 13);

            //        DataTable dt = new DataTable();
            //        dt = cls.GetRM_ID_BySearchTxt(UserId, SearchTxt.Trim());
            //        pro.RM_Id = Convert.ToInt32(dt.Rows[0]["RM_Id"]);






            ClearRecipe();




        }
        public void ClearRecipe()
        {
            Searchtxt.Text = "";
            //ChkReqruiredFormulation.Checked = false;
            RequiredFormulationtxt.Text = "";
            InputKGtxt.Text = "";
        }
        protected void ChkSolvent_CheckedChanged(object sender, EventArgs e)
        {

            //if (Chksolvent == true)
            //{

            //    Solventtxt.Text = (Convert.ToInt32(BatchSizeInputtxt.Text) - Convert.ToInt32(InputSubTotaltxt.Text)).ToString();

            //    pro.ChkSolvent = Convert.ToInt32(Solventtxt.Text);
            //    Solventtxt.ReadOnly = true;

            //}
            //else
            //{
            //    Solventtxt.ReadOnly = true;
            //    Solventtxt.Text = "";
            //    pro.BatchSize_InputKg = Convert.ToInt32(Solventtxt.Text == "0");
            //}

            if (ChkSolvent.Checked == true)
            {

                InputFormualtion.Visible = false;
                Solventtxt.Text = (Convert.ToDecimal(BatchSizeInputtxt.Text) - Convert.ToDecimal(InputSubTotaltxt.Text)).ToString("0.00");

                pro.Solvent = decimal.Parse(Solventtxt.Text);
                //InputKGtxt.ReadOnly = true;
            }
            else
            {
                InputFormualtion.Visible = true;


                //InputKGtxt.ReadOnly = false;
                Solventtxt.Text = "";
            }

        }
        protected void FormulationDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProFormulationMaster pro = new ProFormulationMaster();
            ClsFormulationMaster cls = new ClsFormulationMaster();
            if (FormulationDropdown.SelectedValue != "Select")
            {
                int FM_ID = Convert.ToInt32(FormulationDropdown.SelectedValue);

                DataTable dt1 = new DataTable();

                if (lblGridSubTotalAmount.Text != "")
                {
                    TotalAmounttxt.Text = lblGridSubTotalAmount.Text;
                }
                else
                {
                    TotalAmounttxt.Text = "0";
                }

                dt1 = cls.Get_FormulationMasterById(UserId, FM_ID);

                pro.FM_Id = Convert.ToInt32(dt1.Rows[0]["FM_Id"].ToString());
                pro.FM_BatchSize = Convert.ToInt32(dt1.Rows[0]["BatchSize"].ToString());
                pro.FM_Total_Cost = decimal.Parse(dt1.Rows[0]["Cost_Per_Ltr_BatchSize"].ToString());
                //FormulationTotalCosttxt.Text = dt1.Rows[0]["TotalCost"].ToString();
                lblFormulationAddBuffer.Text = dt1.Rows[0]["FinalCostPerLtrBatchSize"].ToString();

                FormulationAmounttxt.Text = "";
                FormulationAmounttxt.Text = (decimal.Parse(BatchSizeInputtxt.Text) * (decimal.Parse(lblFormulationAddBuffer.Text))).ToString("0.00");
                TotalAmounttxt.Text = (decimal.Parse(TotalAmounttxt.Text) + (decimal.Parse(FormulationAmounttxt.Text))).ToString("0.00");
                DefaultValuesSPGR();
                SPGRtxt_TextChanged(null, null);
            }
        }

        protected void SPGRtxt_TextChanged(object sender, EventArgs e)
        {
            if (SPGRtxt.Text == "" || Convert.ToDouble(SPGRtxt.Text) <= 0)
            {
                SPGRtxt.Text = "1";
            }
            TotalOutput_LTR.Text = (decimal.Parse(lblBatchSize.Text) / decimal.Parse(SPGRtxt.Text)).ToString("0.00");
            Costtxt.Text = (decimal.Parse(TotalAmounttxt.Text) / decimal.Parse(TotalOutput_LTR.Text)).ToString("0.00");

            FormulationLosttxt_TextChanged(null, null);
        }

        protected void DelFormulationBtn_Click(object sender, EventArgs e)
        {

            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int Ingredient_Id = Convert.ToInt32(GridBOM_Formulation.DataKeys[gdrow.RowIndex].Value.ToString());

            int status = cls.DeleteIngredientMaster(Ingredient_Id, UserId);
            if (status > 0)
            {
                IngrediantGridviewData();
                FnUpdateFinalBOM();

                //if (InputSubTotaltxt.Text != "")
                //{
                //    ClearData();
                //    UpdateFinalBOM.Visible = true;
                //    AddFinalBOM.Visible = false;
                //    //TotalAmounttxt.Text = "0";
                //    //Costtxt.Text = "0";
                //    DefaultValuesSPGR();
                //}
                //else
                //{
                //    UpdateFinalBOM.Visible = false;
                //    AddFinalBOM.Visible = true;
                //    ClearData();
                //    DefaultValuesSPGR();
                //}
            }
        }
        public void DefaultValuesSPGR()
        {
            if (lblMeasurement.Text == "KG" || lblMeasurement.Text == "GM")
            {
                if (string.IsNullOrEmpty(SPGRtxt.Text.Trim()) || Convert.ToDouble(SPGRtxt.Text.Trim()) <= 0)
                {
                    SPGRtxt.Text = "1.000";
                }
                TotalOutput_LTR.Text = (decimal.Parse(BatchSizeInputtxt.Text) / decimal.Parse(SPGRtxt.Text)).ToString("0.00");

                if (TotalAmounttxt.Text != "0.00" || TotalAmounttxt.Text != "0" || TotalAmounttxt.Text != "")
                {
                    try
                    {
                        Costtxt.Text = (decimal.Parse(TotalAmounttxt.Text) / (decimal.Parse(TotalOutput_LTR.Text))).ToString("0.00");
                        FinalCostBulktxt.Text = Costtxt.Text;
                        if (FormulationLosttxt.Text != "0.00" && FormulationLosttxt.Text != "" && FormulationLosttxt.Text != "0")
                        {
                            FormulationALostmounttxt.Text = (decimal.Parse(Costtxt.Text) * (decimal.Parse(FormulationLosttxt.Text))).ToString("0.00");
                            FinalCostBulktxt.Text = (decimal.Parse(FormulationALostmounttxt.Text) + decimal.Parse(FormulationLosttxt.Text)).ToString("0.00");
                        }
                        if (FormulationLosttxt.Text == "0.00" || FormulationLosttxt.Text == "0")
                        {
                            Costtxt.Text = (decimal.Parse(TotalAmounttxt.Text) / (decimal.Parse(TotalOutput_LTR.Text))).ToString();
                            FinalCostBulktxt.Text = Costtxt.Text;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
            }
        }
        protected void ChkFormulationDropDown_CheckedChanged(object sender, EventArgs e)
        {

            if (ChkFormulationDropDown.Checked)
            {
                FormulationDropdown.Enabled = true;
            }
            else
            {
                if (FormulationAmounttxt.Text != "")
                {
                    TotalAmounttxt.Text = lblGridSubTotalAmount.Text;
                    FormulationDropdown.Enabled = false;
                    FormulationDropdown.SelectedIndex = 0;
                    FormulationAmounttxt.Text = "0.00";
                    DefaultValuesSPGR();
                }
            }
            SPGRtxt_TextChanged(null, null);
        }

        protected void FormulationLosttxt_TextChanged(object sender, EventArgs e)
        {
            if (Costtxt.Text != "" && FormulationLosttxt.Text != "")
            {
                decimal count;
                count = (decimal.Parse(Costtxt.Text) * decimal.Parse(FormulationLosttxt.Text)) / 100;
                FormulationALostmounttxt.Text = count.ToString("0.00");
                FinalCostBulktxt.Text = (decimal.Parse(Costtxt.Text) + decimal.Parse(FormulationALostmounttxt.Text)).ToString("0.00");
            }

        }

        protected void GridBOM_Formulation_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SubTotalInputTechnial += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InputTechnical"));
                SubTotalAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.Cells[0].Text = "Solvent";
                //e.Row.Cells[0].Font.Bold = true;
                if (Solvent > 0)
                {
                    e.Row.Cells[1].Text = "Solvent";
                    e.Row.Cells[1].Font.Bold = true;
                }
                e.Row.Cells[2].Text = "SubTotal";
                e.Row.Cells[2].Font.Bold = true;

                e.Row.Cells[4].Text = SubTotalInputTechnial.ToString(); ;
                e.Row.Cells[4].Font.Bold = true;

                e.Row.Cells[7].Text = SubTotalAmount.ToString(); ;
                e.Row.Cells[7].Font.Bold = true;
                lblGridSubTotalAmount.Text = SubTotalAmount.ToString();
            }
            InputSubTotaltxt.Text = SubTotalInputTechnial.ToString();
            TotalAmounttxt.Text = SubTotalAmount.ToString();
            if (FormulationDropdown.Enabled == false)
            {
                Costtxt.Text = TotalAmounttxt.Text;
                FinalCostBulktxt.Text = TotalAmounttxt.Text;
            }

        }


        protected void GridBOM_Formulation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            GridViewRow row = (GridViewRow)GridBOM_Formulation.Rows[e.RowIndex];

            int Ingredient_Id = Convert.ToInt32(GridBOM_Formulation.DataKeys[row.RowIndex].Value.ToString());

            int status = cls.DeleteIngredientMaster(Ingredient_Id, UserId);
            if (status > 0)
            {
                IngrediantGridviewData();
            }
        }


        protected void GridBOM_Formulation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int IngredientID = Convert.ToInt32(GridBOM_Formulation.DataKeys[e.RowIndex].Value.ToString());
            TextBox IngredientName = GridBOM_Formulation.Rows[e.RowIndex].FindControl("txt_Formulation") as TextBox;
            TextBox Formulation = GridBOM_Formulation.Rows[e.RowIndex].FindControl("txt_Formulation") as TextBox;

            GridBOM_Formulation.EditIndex = -1;

            int BOM_Id = Convert.ToInt32(lblBR_BOM_Id.Text);


            int status = cls.UpdateIngredientMasterData(IngredientID, Convert.ToInt32(lblRMId.Text), BOM_Id, Convert.ToInt32(IngredientName.Text), Convert.ToInt32(Formulation.Text));
            IngrediantGridviewData();

        }

        protected void AddFinalBOM_Click(object sender, EventArgs e)
        {


            pro.SubTotalInputTechnical = decimal.Parse(InputSubTotaltxt.Text);
            pro.SubTotalAmount = decimal.Parse(TotalAmounttxt.Text);
            if (FormulationDropdown.SelectedValue == "Select")
            {
                pro.FormulationWith = 0;
                pro.FormulationAmount = 0;
                pro.TotalOutput_LTR = 0;

                pro.SPGR = 0;
                pro.FormulationLost = 0;
                pro.FolrmulationLostAmount = 0;
                pro.FinalCostBulk = decimal.Parse(FinalCostBulktxt.Text);
                pro.BatchSize_InputKg = decimal.Parse(BatchSizeInputtxt.Text);

            }
            else
            {
                pro.BatchSize_InputKg = decimal.Parse(BatchSizeInputtxt.Text);
                pro.FormulationWith = Convert.ToInt32(FormulationDropdown.SelectedValue);

                pro.TotalAmount = decimal.Parse(TotalAmounttxt.Text);
                pro.TotalCost = decimal.Parse(Costtxt.Text);

                pro.FinalCostBulk = decimal.Parse(FinalCostBulktxt.Text);
                pro.User_Id = UserId;
                pro.BR_BOM_Id = Convert.ToInt32(lblBR_BOM_Id.Text);
                pro.BPM_Product_Id = Convert.ToInt32(lblBPM_Product_Id.Text);
                pro.FormulationLost = decimal.Parse(FormulationLosttxt.Text);
                pro.FolrmulationLostAmount = decimal.Parse(FormulationALostmounttxt.Text);
                pro.FormulationAmount = decimal.Parse(FormulationAmounttxt.Text);
                pro.SPGR = decimal.Parse(SPGRtxt.Text);
                pro.TotalOutput_LTR = decimal.Parse(TotalOutput_LTR.Text);

                pro.FinalCostBulk = decimal.Parse(FinalCostBulktxt.Text);
            }

            //pro.FormulationWith = Convert.ToInt32(FormulationDropdown.SelectedValue);
            //pro.BatchSize_InputKg = float.Parse(BatchSizeInputtxt.Text);
            //pro.SPGR = float.Parse(SPGRtxt.Text);
            //pro.TotalAmount = float.Parse(TotalAmounttxt.Text);
            //pro.TotalOutput_LTR = float.Parse(TotalOutput_LTR.Text);
            //pro.TotalCost = float.Parse(Costtxt.Text);

            //pro.FinalCostBulk = float.Parse(FinalCostBulktxt.Text);
            pro.User_Id = UserId;
            pro.BR_BOM_Id = Convert.ToInt32(lblBR_BOM_Id.Text);
            pro.BPM_Product_Id = Convert.ToInt32(lblBPM_Product_Id.Text);

            try
            {
                int status = cls.InsertFinalCostBulk_BRBOM(pro);
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

                    Grid_BR_BOM_MasterData();

                }
            }
            catch (Exception)
            {

                throw;
            }



            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected int FnUpdateFinalBOM()
        {
            int status = 0;

            pro.SubTotalInputTechnical = decimal.Parse(InputSubTotaltxt.Text);
            pro.SubTotalAmount = decimal.Parse(TotalAmounttxt.Text);
            if (FormulationDropdown.SelectedValue == "Select")
            {
                pro.FormulationWith = 0;
                pro.FormulationAmount = 0;
                pro.TotalOutput_LTR = 0;

                pro.SPGR = 0;
                pro.FormulationLost = 0;
                pro.FolrmulationLostAmount = 0;
                pro.FinalCostBulk = decimal.Parse(FinalCostBulktxt.Text);
                pro.BatchSize_InputKg = decimal.Parse(BatchSizeInputtxt.Text);
                pro.User_Id = UserId;
                pro.BR_BOM_Id = Convert.ToInt32(lblBR_BOM_Id.Text);
                pro.BPM_Product_Id = Convert.ToInt32(lblBPM_Product_Id.Text);
            }
            else
            {
                pro.FormulationLost = decimal.Parse(FormulationLosttxt.Text);
                pro.BatchSize_InputKg = decimal.Parse(BatchSizeInputtxt.Text);
                pro.TotalAmount = decimal.Parse(TotalAmounttxt.Text);
                pro.TotalCost = decimal.Parse(Costtxt.Text);
                pro.FormulationWith = Convert.ToInt32(FormulationDropdown.SelectedValue);
                pro.FormulationAmount = decimal.Parse(FormulationAmounttxt.Text);
                pro.FinalCostBulk = decimal.Parse(FinalCostBulktxt.Text);
                pro.User_Id = UserId;
                pro.SPGR = decimal.Parse(SPGRtxt.Text);
                pro.BR_BOM_Id = Convert.ToInt32(lblBR_BOM_Id.Text);
                pro.BPM_Product_Id = Convert.ToInt32(lblBPM_Product_Id.Text);
            }

            if (lblFinalCostBulk_BRBOM_Id.Text != "")
            {
                status = cls.UpdateFinalCostBulk_BRBOM(UserId, Convert.ToInt32(lblBR_BOM_Id.Text), Convert.ToInt32(lblFinalCostBulk_BRBOM_Id.Text), pro);
            }
            return status;
        }

        protected void UpdateFinalBOM_Click(object sender, EventArgs e)
        {

            int status = 0;
            if (lblFinalCostBulk_BRBOM_Id.Text != "")
            {
                status = FnUpdateFinalBOM();
            }
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

                Response.Redirect("BulkRecipe(BOM).aspx");
            }
            Grid_BR_BOM_MasterData();
        }

        protected void TotalAmounttxt_TextChanged(object sender, EventArgs e)
        {
            if (SPGRtxt.Text != "" && Costtxt.Text != "")
            {
                TotalOutput_LTR.Text = (decimal.Parse(lblBatchSize.Text) / decimal.Parse(SPGRtxt.Text)).ToString("0.00");
                Costtxt.Text = (decimal.Parse(TotalAmounttxt.Text) / decimal.Parse(TotalOutput_LTR.Text)).ToString("0.00");
            }
            TotalOutput_LTR.Text = (decimal.Parse(lblBatchSize.Text) / decimal.Parse(SPGRtxt.Text)).ToString("0.00");
            Costtxt.Text = (decimal.Parse(TotalAmounttxt.Text) / decimal.Parse(TotalOutput_LTR.Text)).ToString("0.00");

        }

        protected void Costtxt_TextChanged(object sender, EventArgs e)
        {
            decimal count;
            if (Costtxt.Text != "" && FormulationLosttxt.Text != "")
            {
                count = (decimal.Parse(Costtxt.Text) * decimal.Parse(FormulationLosttxt.Text)) / 100;
                FormulationALostmounttxt.Text = count.ToString("0.00");
                FinalCostBulktxt.Text = (decimal.Parse(Costtxt.Text) - decimal.Parse(FormulationALostmounttxt.Text)).ToString("0.00");
            }

        }


        protected void Grid_BR_BOM_MasterDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }

        protected void Grid_BR_BOM_MasterDataList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_BR_BOM_MasterData();


            Grid_BR_BOM_MasterDataList.PageIndex = e.NewPageIndex;
            Grid_BR_BOM_MasterDataList.DataBind();
        }

        protected void BulkProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = cls.Get_BP_MasterDataAll(UserId);

            foreach (DataRow row in dt.Rows)
            {
                if (BulkProductDropDownList.SelectedValue == row["BPM_Product_Id"].ToString())
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Can not Use  Multiple Time!')", true);
                    //lblMasterPacking_Id.Text = row["Fk_BPM_Product_Id"].ToString();
                    BulkProductDropDownList.SelectedIndex = 0;
                    return;
                }
            }
        }

        protected void SearchId_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }


        private void BindGrid()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_SearchBulkProductfromBRBOM", con);
                cmd.Parameters.AddWithValue("@SearchBulkProduct", txtSearch.Text.Trim());


                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                cmd.Dispose();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dt.Dispose();
            }

            Grid_BR_BOM_MasterDataList.DataSource = dt;
            Grid_BR_BOM_MasterDataList.DataBind();
        }

        protected void CancelSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            Grid_BR_BOM_MasterData();
        }

        protected void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}






//protected void SPGRtxt_TextChanged(object sender, EventArgs e)
//{
//    TotalOutput_LTR.Text = (decimal.Parse(1000.ToString()) / decimal.Parse(SPGRtxt.Text)).ToString("0.00");
//}

//protected void FormulationDropdown_SelectedIndexChanged(object sender, EventArgs e)
//{
//    ProFormulationMaster pro = new ProFormulationMaster();
//    ClsFormulationMaster cls = new ClsFormulationMaster();
//    int FM_ID = Convert.ToInt32(FormulationDropdown.SelectedValue);

//    DataTable dt1 = new DataTable();



//    dt1 = cls.Get_FormulationMasterById(UserId, FM_ID);

//    pro.FM_Id = Convert.ToInt32(dt1.Rows[0]["FM_Id"].ToString());
//    pro.FM_BatchSize = Convert.ToInt32(dt1.Rows[0]["BatchSize"].ToString());
//    pro.FM_Total_Cost = float.Parse(dt1.Rows[0]["TotalCost"].ToString());

//    FormulationSelectedtxt.Text  = (decimal.Parse(BatchSizeInput.Text) * (decimal.Parse(pro.FM_Total_Cost.ToString()) / decimal.Parse(pro.FM_BatchSize.ToString()))).ToString();
//    TotalAmounttxt.Text = (decimal.Parse(Amounttxt.Text) + decimal.Parse(FormulationSelectedtxt.Text)).ToString("0.00");
//}
