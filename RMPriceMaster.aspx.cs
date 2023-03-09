using BusinessAccessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class RMPriceMaster : System.Web.UI.Page
    {
        ProRMPriceMaster pro = new ProRMPriceMaster();
        ClsRMPriceMaster cls = new ClsRMPriceMaster();
        int UserId;
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

                GridRM_RM_MasterListData();
                GridRM_PriceMasterListData();
                GetLoginDetails();
                RMCategoryDropDownCombo();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[0]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[0]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[0]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblPrice_Id.Text!="")
                {
                    Addbtn.Visible = false;
                    CancalBtn.Visible = true;
                    Editbtn.Visible = true;
                }
                else
                {
                    Addbtn.Visible = true;
                    CancalBtn.Visible = true;
                    Editbtn.Visible = false;
                }
               
            }
            else
            {
                Addbtn.Visible = false;
                Editbtn.Visible = false;
                CancalBtn.Visible = false;

            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[0]["GroupId"]);

            if (Convert.ToBoolean(dtMenuList.Rows[0]["CanEdit"]) == true)
            {
                Addbtn.Visible = true;
            }
            else
            {
                Addbtn.Visible = false;
            }


        }

        public void RMCategoryDropDownCombo()
        {
            ClsRMCategoryMaster cls = new ClsRMCategoryMaster();
            RMCategoryDropDownList.DataSource = cls.Get_RM_CategoryMaster();
            RMCategoryDropDownList.DataTextField = "RM_Category_Name";
            RMCategoryDropDownList.DataValueField = "RM_Category_id";
            RMCategoryDropDownList.DataBind();
            RMCategoryDropDownList.Items.Insert(0, "Select");
            RMDropdownList.Enabled = false;
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
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        protected void EditRMPriceBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int RawId = Convert.ToInt32(GridRM_Price_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblRM_Id.Text = RawId.ToString();
            DataTable dt = new DataTable();

            dt = cls.Get_RM_Price_MasterById(UserId, RawId);

            try
            {
                lblRM_Id.Text = dt.Rows[0]["RM_Id"].ToString();
                RMDropdownList.SelectedValue = dt.Rows[0]["RM_Id"].ToString();
                DOPtxt.Text = (dt.Rows[0]["DateOfPurchase"].ToString());
                ActualPricetxt.Text = dt.Rows[0]["ActualPrice"].ToString();
                PurityPercenttxt.Text = dt.Rows[0]["Purity"].ToString();
                Quantitytxt.Text = dt.Rows[0]["Quantity"].ToString();
                RateQtytxt.Text = dt.Rows[0]["Rate_Kg_Ltr"].ToString();
                TransportationRatetxt.Text = dt.Rows[0]["TransporationRate"].ToString();
                TotalRatetxt.Text = dt.Rows[0]["Total"].ToString();

                int Yes = Convert.ToInt32(dt.Rows[0]["IsRatePurity"]);
                if (Yes == 1)
                {
                    IsRateFullPurity.SelectedIndex = 0;
                }
                else
                {
                    IsRateFullPurity.SelectedIndex = 1;
                }


            }
            catch (Exception)
            {

                throw;
            }
            Addbtn.Visible = false;
            Editbtn.Visible = true;
        }

        protected void DelRMPriceBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int RM_PriceId = Convert.ToInt32(GridRM_Price_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.User_Id = UserId;
            pro.RM_Price_Id = RM_PriceId;
            int status = cls.Delete_RM_Price_Master(pro);
            if (status > 0)
            {
                GridRM_PriceMasterListData();
            }
        }

        public void DataClear()
        {
            lblRM_Id.Text = "";
            lblPrice_Id.Text = "";
            RMDropdownList.SelectedIndex = 0;
            DOPtxt.Text = "";
            ActualPricetxt.Text = "";
            PurityPercenttxt.Text = "";
            Quantitytxt.Text = "";
            RateQtytxt.Text = "";
            TransportationRatetxt.Text = "";
            TotalRatetxt.Text = "";
        }
        protected void GridRM_Price_MasterList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void Addbtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                pro.RM_Id = Convert.ToInt32(RMDropdownList.SelectedValue);
                pro.DateOfPurchase = Convert.ToDateTime(DOPtxt.Text);
                pro.Quntity = decimal.Parse(Quantitytxt.Text);
                pro.Rate_Kg_Ltr = decimal.Parse(RateQtytxt.Text);
                pro.User_Id = UserId;
                if (PurityPercenttxt.Text != "")
                {
                    pro.Purity = decimal.Parse(PurityPercenttxt.Text);

                }
                pro.Actual_PriceKG_Ltr = decimal.Parse(ActualPricetxt.Text);
                pro.Transportation_Rate = decimal.Parse(TransportationRatetxt.Text);
                pro.TotalRate_Per_Ltr = decimal.Parse(TotalRatetxt.Text);
                pro.RM_CategoryId = Convert.ToInt32(RMCategoryDropDownList.SelectedValue);
                if (IsRateFullPurity.SelectedValue == "0" || IsRateFullPurity.SelectedValue == "")
                {
                    pro.IsRateFullPurity = false;
                }
                else
                {
                    pro.IsRateFullPurity = true;
                }


                int status = cls.Insert_RM_Price_MasterData(pro);
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

                    GridRM_PriceMasterListData();
                    DataClear();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

                }

            }

        }
        public void GridRM_RM_MasterListData()
        {
            DataTable dt = new DataTable();
            dt = cls.Get_RM_MasterDataAll(pro);
            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "RM_Name";

            RMDropdownList.DataSource = dvOptions;
            RMDropdownList.DataTextField = "RM_Name";
            RMDropdownList.DataValueField = "RM_Id";
            RMDropdownList.DataBind();
            RMDropdownList.Items.Insert(0, "Select");
        }
        public void GridRM_PriceMasterListData()
        {
            GridRM_Price_MasterList.DataSource = cls.Get_RM_Price_MasterDataAll(pro);
            GridRM_Price_MasterList.DataBind();
            DataClear();
        }
        protected void IsRateFullPurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsRateFullPurity.SelectedValue == "Yes")
            {
                PurityPercenttxt.Enabled = true;
                decimal Actual;
                if (RateQtytxt.Text != "" && PurityPercenttxt.Text != "")
                {
                    Actual = (decimal.Parse(RateQtytxt.Text) * decimal.Parse(PurityPercenttxt.Text) / 100);
                    ActualPricetxt.Text = (Actual).ToString("0.00");
                    if (TransportationRatetxt.Text != "")
                    {
                        decimal Total = decimal.Parse(ActualPricetxt.Text) + decimal.Parse(TransportationRatetxt.Text);
                        TotalRatetxt.Text = Total.ToString("0.00");
                    }
                }
            }
            else
            {
                //PurityPercenttxt.Enabled = false;
                PurityPercenttxt.Text = "";
                decimal Actual;
                if (RateQtytxt.Text != "")
                {
                    Actual = (decimal.Parse(RateQtytxt.Text) * 100 / 100);
                    ActualPricetxt.Text = (Actual).ToString("0.00");
                    if (TransportationRatetxt.Text != "")
                    {
                        decimal Total = decimal.Parse(ActualPricetxt.Text) + decimal.Parse(TransportationRatetxt.Text);
                        TotalRatetxt.Text = Total.ToString("0.00");
                    }
                }
            }
        }

        protected void Quantitytxt_TextChanged(object sender, EventArgs e)
        {
            if (IsRateFullPurity.SelectedValue == "Yes")
            {
                PurityPercenttxt.Enabled = true;

                if (RateQtytxt.Text != "" && PurityPercenttxt.Text != "")
                {
                    decimal Actual;
                    Actual = (decimal.Parse(RateQtytxt.Text) * decimal.Parse(PurityPercenttxt.Text) / 100);
                    ActualPricetxt.Text = (Actual).ToString("0.00");
                    if (TransportationRatetxt.Text != "")
                    {
                        decimal Total = decimal.Parse(ActualPricetxt.Text) + decimal.Parse(TransportationRatetxt.Text);
                        TotalRatetxt.Text = Total.ToString("0.00");
                    }
                    else
                    {
                        decimal Total = decimal.Parse(ActualPricetxt.Text);
                        TotalRatetxt.Text = Total.ToString("0.00");
                    }
                }

            }
            else
            {
                //PurityPercenttxt.Enabled = false;
                decimal Actual;
                if (RateQtytxt.Text != "")
                {
                    Actual = (decimal.Parse(RateQtytxt.Text) * 100 / 100);
                    ActualPricetxt.Text = (Actual).ToString("0.00");
                    if (TransportationRatetxt.Text != "")
                    {
                        decimal Total = decimal.Parse(ActualPricetxt.Text) + decimal.Parse(TransportationRatetxt.Text);
                        TotalRatetxt.Text = Total.ToString("0.00");
                    }
                }
            }

        }

        protected void PurityPercenttxt_TextChanged(object sender, EventArgs e)
        {
            if (IsRateFullPurity.SelectedValue == "Yes")
            {
                PurityPercenttxt.Enabled = true;

                if (RateQtytxt.Text != "" && PurityPercenttxt.Text != "")
                {
                    decimal Actual;
                    Actual = (decimal.Parse(RateQtytxt.Text) * decimal.Parse(PurityPercenttxt.Text) / 100);
                    ActualPricetxt.Text = (Actual).ToString("0.00");
                    if (TransportationRatetxt.Text != "")
                    {
                        decimal Total = decimal.Parse(ActualPricetxt.Text) + decimal.Parse(TransportationRatetxt.Text);
                        TotalRatetxt.Text = Total.ToString("0.00");
                    }
                    else
                    {
                        decimal Total = decimal.Parse(ActualPricetxt.Text);
                        TotalRatetxt.Text = Total.ToString("0.00");
                    }

                }
            }
            else
            {
                //PurityPercenttxt.Enabled = false;
                decimal Actual;
                if (RateQtytxt.Text != "")
                {
                    Actual = (decimal.Parse(RateQtytxt.Text) * 100 / 100);
                    ActualPricetxt.Text = (Actual).ToString("0.00");
                    if (TransportationRatetxt.Text != "")
                    {
                        decimal Total = decimal.Parse(ActualPricetxt.Text) + decimal.Parse(TransportationRatetxt.Text);
                        TotalRatetxt.Text = Total.ToString("0.00");
                    }
                }
            }
        }

        protected void RateQtytxt_TextChanged(object sender, EventArgs e)
        {
            if (IsRateFullPurity.SelectedValue == "Yes")
            {
                PurityPercenttxt.Enabled = true;

                if (RateQtytxt.Text != "" && PurityPercenttxt.Text != "")
                {
                    decimal Actual;
                    Actual = (decimal.Parse(RateQtytxt.Text) * decimal.Parse(PurityPercenttxt.Text) / 100);
                    ActualPricetxt.Text = (Actual).ToString("0.00");
                    if (TransportationRatetxt.Text != "")
                    {
                        decimal Total = decimal.Parse(ActualPricetxt.Text) + decimal.Parse(TransportationRatetxt.Text);
                        TotalRatetxt.Text = Total.ToString("0.00");
                    }
                }

            }
            else
            {
                //PurityPercenttxt.Enabled = false;
                decimal Actual;
                if (RateQtytxt.Text != "")
                {
                    Actual = (decimal.Parse(RateQtytxt.Text) * 100 / 100);
                    ActualPricetxt.Text = (Actual).ToString("0.00");
                    if (TransportationRatetxt.Text != "")
                    {
                        decimal Total = decimal.Parse(ActualPricetxt.Text) + decimal.Parse(TransportationRatetxt.Text);
                        TotalRatetxt.Text = Total.ToString("0.00");
                    }
                }

            }
        }

        protected void TransportationRatetxt_TextChanged(object sender, EventArgs e)
        {
            if (TransportationRatetxt.Text != "")
            {
                decimal Total = decimal.Parse(ActualPricetxt.Text) + decimal.Parse(TransportationRatetxt.Text);
                TotalRatetxt.Text = Total.ToString("0.00");
            }
        }

        protected void Editbtn_Click(object sender, EventArgs e)
        {
            pro.User_Id = UserId;
            pro.RM_Price_Id = Convert.ToInt32(lblPrice_Id.Text);
            pro.RM_Id = Convert.ToInt32(lblRM_Id.Text);
            //pro.DateOfPurchase = Convert.ToDateTime(DateTime.Now);
            pro.DateOfPurchase = Convert.ToDateTime(DOPtxt.Text);


            pro.Quntity = decimal.Parse(Quantitytxt.Text);
            pro.Rate_Kg_Ltr = decimal.Parse(RateQtytxt.Text);
            if (PurityPercenttxt.Text != "")
            {
                pro.Purity = decimal.Parse(PurityPercenttxt.Text);

            }
            pro.Actual_PriceKG_Ltr = decimal.Parse(ActualPricetxt.Text);
            pro.Transportation_Rate = decimal.Parse(TransportationRatetxt.Text);
            pro.TotalRate_Per_Ltr = decimal.Parse(TotalRatetxt.Text);
            pro.User_Id = UserId;
            if (IsRateFullPurity.SelectedValue == "0")
            {
                pro.IsRateFullPurity = false;
            }
            else
            {
                pro.IsRateFullPurity = true;
            }

            int status = cls.Update_RM_Price_MasterData(pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

                lblPrice_Id.Text = "";

                RMDropdownList.SelectedIndex = 0;

                Addbtn.Visible = true;
                Editbtn.Visible = false;
                DataClear();
                GridRM_PriceMasterListData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Failed')", true);

            }

        }

        protected void DelRMPriceBtn_Click1(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int RM_PriceId = Convert.ToInt32(GridRM_Price_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.User_Id = UserId;
            pro.RM_Price_Id = RM_PriceId;
            int status = cls.Delete_RM_Price_Master(pro);
            if (status > 0)
            {
                GridRM_PriceMasterListData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Failed')", true);

            }
        }

        protected void EditRMPriceBtn_Click1(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int RawId = Convert.ToInt32(GridRM_Price_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblPrice_Id.Text = RawId.ToString();
            DataTable dt = new DataTable();
            RMDropdownListcombo();
            RMCategoryDropDownCombo();
  
            dt = cls.Get_RM_Price_MasterById(UserId, RawId);

            try
            {
                lblRM_Id.Text = dt.Rows[0]["RM_Id"].ToString();

                string RMValues = "";

                RMValues = dt.Rows[0]["RM_Id"].ToString();
                if (RMValues != "")
                {
                    RMDropdownList.SelectedValue = dt.Rows[0]["RM_Id"].ToString();
                }
                else
                {
                    RMDropdownList.SelectedValue = "0";
                }

                DateTime d = DateTime.Parse(dt.Rows[0]["DateOfPurchase"].ToString());
                DOPtxt.Text = d.ToString("yyyy-MM-dd");
                ActualPricetxt.Text = dt.Rows[0]["ActualPrice"].ToString();
                PurityPercenttxt.Text = dt.Rows[0]["Purity"].ToString();
                Quantitytxt.Text = dt.Rows[0]["Quantity"].ToString();
                RateQtytxt.Text = dt.Rows[0]["Rate_Kg_Ltr"].ToString();
                TransportationRatetxt.Text = dt.Rows[0]["TransporationRate"].ToString();
                TotalRatetxt.Text = dt.Rows[0]["Total"].ToString();
                RMCategoryDropDownList.SelectedValue= dt.Rows[0]["FkRMCategory_Id"].ToString();
                int Yes = Convert.ToInt32(dt.Rows[0]["IsRatePurity"]);
                if (Yes == 1)
                {
                    IsRateFullPurity.SelectedIndex = 0;
                }
                else
                {
                    IsRateFullPurity.SelectedIndex = 1;
                }
                if (PurityPercenttxt.Text == "0")
                {
                    PurityPercenttxt.Visible = false;
                }
                else
                {
                    PurityPercenttxt.Visible = true;
                }

                if (IsRateFullPurity.SelectedValue == "0")
                {
                    IsRateFullPurity.Visible = false;
                }
                else
                {
                    IsRateFullPurity.Visible = true;
                }


            }
            catch (Exception)
            {

                throw;
            }

            Addbtn.Visible = false;
            Editbtn.Visible = true;
        }

        protected void RMDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsRMPriceMaster cls = new ClsRMPriceMaster();
            if (RMDropdownList.SelectedValue == "Select")
            {

            }
            else
            {
                int RM_ID = Convert.ToInt32(RMDropdownList.SelectedValue);


                lblRM_Id.Text = RM_ID.ToString();
                DataTable dt = new DataTable();
                DataTable dtGetRM = new DataTable();
                ClsRMMaster clsRM = new ClsRMMaster();
                dt = clsRM.Get_RM_MasterBy_Id(RM_ID, UserId);
                //if (dt.Rows.Count>0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple RM !')", true);
                //    RMDropdownList.SelectedIndex = 0;
                //    RMCategoryDropDownList.SelectedIndex = 0;
                //    DataClear();
                //    return;
                //}
                string RatePurity = dt.Rows[0]["IsPurityRequired"].ToString();


                string DbRm_Id;
                dtGetRM = cls.Get_RM_Price_MasterById(UserId, RM_ID);
                if (dtGetRM.Rows.Count>0)
                {
                    DbRm_Id = dtGetRM.Rows[0]["RM_Id"].ToString();
                }
                else
                {
                    DbRm_Id = "0";
                }
               

                if (DbRm_Id != "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Raw Material Already  Inserted!')", true);
                    RMDropdownList.SelectedIndex = 0;
                    RMCategoryDropDownList.SelectedIndex = 0;
                    return;
                }
                if (RatePurity == "True")
                {
                    IsRateFullPurity.Visible = true;
                    PurityPercenttxt.Visible = true;
                    
                }
                else
                {
                    IsRateFullPurity.Visible = false;
                    PurityPercenttxt.Visible = false;


                }
                try
                {
                    //if (RMDropdownList.SelectedIndex == 0)
                    //{
                    //    GridRM_Price_MasterList.DataSource = cls.Get_RM_Price_MasterDataAll(pro);
                    //    GridRM_Price_MasterList.DataBind();
                    //}
                    //else
                    //{
                    //    GridRM_Price_MasterList.DataSource = cls.Select_RM_Price_MasterBy_RMCate_Id(UserId, RM_Cat_ID);
                    //    GridRM_Price_MasterList.DataBind();
                    //}

                }
                catch (Exception)
                {

                    throw;
                }
            }


        }

        protected void CancalBtn_Click(object sender, EventArgs e)
        {

            Addbtn.Visible = true;
            Editbtn.Visible = false;
            RateQtytxt.Text = "";
            ActualPricetxt.Text = "";
            TotalRatetxt.Text = "";
            TransportationRatetxt.Text = "";
            Quantitytxt.Text = "";
            PurityPercenttxt.Text = "";
            DOPtxt.Text = "";

            RMCategoryDropDownList.SelectedIndex = 0;
            RMDropdownList.SelectedIndex = 0;
            PurityPercenttxt.Visible = true;
            IsRateFullPurity.Visible = true;
        }

        protected void RMCategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RMCategoryDropDownList.SelectedValue != "Select")
            {
                int RmCategory_Id = Convert.ToInt32(RMCategoryDropDownList.SelectedValue);
                ClsRMMaster cls = new ClsRMMaster();
                DataTable dt = new DataTable();
                dt = cls.Get_RM_MasterByCategoryId(UserId, RmCategory_Id);

                DataView dvOptions = new DataView(dt);
                dvOptions.Sort = "RM_Name";

                RMDropdownList.DataSource = dvOptions;

                RMDropdownList.DataTextField = "RM_Name";
                RMDropdownList.DataValueField = "RM_Id";
                RMDropdownList.DataBind();
                RMDropdownList.Items.Insert(0, "Select");
                RMDropdownList.Enabled = true;
            }
            else
            {
                RMDropdownList.Enabled = false;
                RMDropdownList.SelectedIndex = 0;
            }


        }
        public void RMDropdownListcombo()
        {
            ClsRMMaster cls = new ClsRMMaster();

            DataTable dt = new DataTable();
            dt = cls.Get_RM_MasterDataAll(UserId);
            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "RM_Name";

            RMDropdownList.DataSource = dvOptions;
            RMDropdownList.DataTextField = "RM_Name";
            RMDropdownList.DataValueField = "RM_Id";
            RMDropdownList.DataBind();
            RMDropdownList.Items.Insert(0, "Select");
        }

        protected void GridRM_Price_MasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridRM_PriceMasterListData();

            GridRM_Price_MasterList.PageIndex = e.NewPageIndex;
            GridRM_Price_MasterList.DataBind();
        }
        [WebMethod]
        public static List<string> SearchBPMData(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_Search_RM_from_RM_Price";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchRM_Name", prefixText);

                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            customers.Add(sdr["RM_Name"].ToString());

                        }
                    }

                    conn.Close();

                    return customers;
                }
            }
        }
        protected void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void CancelSearch_Click(object sender, EventArgs e)
        {
            TxtSearch.Text = "";
            GridRM_PriceMasterListData();
           
        }

        protected void SearchId_Click(object sender, EventArgs e)
        {

        }
        private void BindGrid()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Search_RM_from_RM_Price", con);
                cmd.Parameters.AddWithValue("@SearchRM_Name", TxtSearch.Text.Trim());


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

            GridRM_Price_MasterList.DataSource = dt;
            GridRM_Price_MasterList.DataBind();
        }
    }
}
