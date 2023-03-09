using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class TransportationCostMaster : System.Web.UI.Page
    {
        int User_Id;
        ClsTransportationCostMaster cls = new ClsTransportationCostMaster();
        ProTransportationCostMaster pro = new ProTransportationCostMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            if (!IsPostBack)
            {
                BulkProductDropDownListCombo();
                //BulkProductDropDownListCombo1();
                GridPackingStyleNameMaster();
                GetTransporatioanCostGridData();
            }
        }
        public void GetLoginDetails()
        {

            User_Id = Convert.ToInt32(Session["UserId"]);

        }

        public void GetTransporatioanCostGridData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("sp_Get_TransporatationCostMasterAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            using (DataSet ds = new DataSet())
            {
                adp.Fill(ds);
                Grid_TransportationCostMaster.DataSource = ds.Tables[0];
                Grid_TransportationCostMaster.DataBind();
                //Grid_TransportationCostFactors.DataSource = ds.Tables[1];

                //Grid_TransportationCostFactors.DataBind();

            }
            cmd.Dispose();
        }


        //Grid_TransportationCostFactors.DataSource = cls.Get_TransporatationCostMasterAll();
        //    Grid_TransportationCostFactors.DataBind();


        public void BulkProductDropDownListCombo()
        {
            //ClsBulkProductMaster cls = new ClsBulkProductMaster();
            //BulkProductDropdownList.DataSource = cls.Get_BP_MasterDataCombo(User_Id);
            //BulkProductDropdownList.DataTextField = "BulkProductName";
            //BulkProductDropdownList.DataValueField = "BPM_Product_Id";
            //BulkProductDropdownList.DataBind();
            //BulkProductDropdownList.Items.Insert(0, "Select");
            ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();
            DataTable BulkDtDropdown = new DataTable();
            BulkDtDropdown = cls.Get_BulkProductComboByFactoryExpence();
            BulkDtDropdown.Columns.Add("BPMValue", typeof(string), "Fk_BPM_Id + ' (' + Fk_PMRM_Category_Id + ')'+'['+PackingSize+ '-'+PackingMeasurement+ ']'").ToString();


            DataView dvOptions = new DataView(BulkDtDropdown);
            dvOptions.Sort = "BPM_Product_Name";
            BulkProductDropdownList.DataSource = BulkDtDropdown;
            BulkProductDropdownList.DataTextField = "BPM_Product_Name";

            BulkProductDropdownList.DataValueField = "BPMValue";
            BulkProductDropdownList.DataBind();
            BulkProductDropdownList.Items.Insert(0, "Select");
        }
        public void GridPackingStyleNameMaster()
        {
            //ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            //PackingStyleDropdownList.DataSource = cls.Get_FinalGoodsPackSizeByBPM_Id(User_Id, Convert.ToInt32(lblBPM_Id.Text));
            //PackingStyleDropdownList.DataTextField = "PackingStyleName";
            //PackingStyleDropdownList.DataValueField = "Fk_Packing_Style_Name_Id";
            //PackingStyleDropdownList.DataBind();
        }
        public void Packingsizecombo()
        {
            //ClsPackingMateriaMaster clsPack = new ClsPackingMateriaMaster();
            //DataTable dtPack = new DataTable();
            //PackingSizeDropdownList.DataSource = dtPack = clsPack.Get_FinalGoodsPackSizeByBPM_Id(User_Id, Convert.ToInt32(lblBPM_Id.Text));
            ////lblpackMeasurement.Text = dtPack.Rows[0]["PackUnitMeasure"].ToString();
            ////lblPacksize.Text = dtPack.Rows[0]["Pack_size"].ToString();

            //dtPack.Columns.Add("PackingSize", typeof(string), "Pack_size + ' ' + PackUnitMeasure +''").ToString();
            //PackingSizeDropdownList.DataTextField = "PackingSize";
            //PackingSizeDropdownList.DataValueField = "Pack_Measurement";
            //PackingSizeDropdownList.DataBind();
            //PackingSizeDropdownList.Items.Insert(0, "Select");
        }

        protected void BulkProductDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string BPM_Full_Id = BulkProductDropdownList.SelectedValue.ToString();
            string Get_BPM_Id = Regex.Match(BPM_Full_Id, @"\d+").Value;
            lblBPM_Id.Text = Get_BPM_Id;
            lblPMRM_Category_Id.Text = BPM_Full_Id.Split('(', ')')[1];
            string BracketMeasurement = BPM_Full_Id.Split('(', ')')[2];
            lblPack_Size.Text = Regex.Match(BracketMeasurement, @"\d+").Value;
            lblPackMeasurement.Text = BracketMeasurement.Split('-', ']')[1];
            //lblPackSize.Text = PackSize;
            //lblPMRM_Category_Id.Text = Shipper_Id;

            //lblPackMeasurement.Text = PackUniMeasurement;

            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.PMRM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
            pro.PackSize = Convert.ToInt32(lblPack_Size.Text);
            pro.PackMeasurement = Convert.ToInt32(lblPackMeasurement.Text);
            ClsProductwiseLabourCost clsPack = new ClsProductwiseLabourCost();

            PackingStyleDropdownList.DataSource = clsPack.Get_PackingStyleByBPM_Id(User_Id, Convert.ToInt32(lblBPM_Id.Text));
            //lblpackMeasurement.Text = dtPack.Rows[0]["PackUnitMeasure"].ToString();
            //lblPacksize.Text = dtPack.Rows[0]["Pack_size"].ToString();

            PackingStyleDropdownList.DataTextField = "PackingStyleName";
            PackingStyleDropdownList.DataValueField = "PackingStyleName_Id";
            PackingStyleDropdownList.DataBind();
            PackingStyleDropdownList.Items.Insert(0, "Select");

            //PackingSizeDropdownList.DataSource = dtPack;
            //dtPack.Columns.Add("PackingSize", typeof(string), "Pack_size + ' ' + PackUnitMeasure +''").ToString();
            //PackingSizeDropdownList.DataTextField = "PackingSize";
            //PackingSizeDropdownList.DataValueField = "Pack_size";
            //PackingSizeDropdownList.DataBind();
        }

        protected void UpdateTransportationCostMasterbtn_Click(object sender, EventArgs e)
        {

        }

        protected void AddTransportationCostMasterbtn_Click(object sender, EventArgs e)
        {
            ProTransportationCostMaster pro = new ProTransportationCostMaster();
            string BPM_Full_Id = BulkProductDropdownList.SelectedValue.ToString();
            string Get_BPM_Id = Regex.Match(BPM_Full_Id, @"\d+").Value;
            lblBPM_Id.Text = Get_BPM_Id;
            lblPMRM_Category_Id.Text = BPM_Full_Id.Split('(', ')')[1];
            string BracketMeasurement = BPM_Full_Id.Split('(', ')')[2];
            lblPack_Size.Text = Regex.Match(BracketMeasurement, @"\d+").Value;
            lblPackMeasurement.Text = BracketMeasurement.Split('-', ']')[1];
            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);

            //pro.PackingStyleCategory_Name = PackingStyleCategoryNametxt.Text;
            pro.PackingSize = float.Parse(lblPack_Size.Text);
            pro.PackingStyle_Id = Convert.ToInt32(PackingStyleDropdownList.SelectedValue);
            pro.PackMeasurement = Convert.ToInt32(lblPackMeasurement.Text);
            pro.PMRM_Category_Id= Convert.ToInt32(lblPMRM_Category_Id.Text);
            pro.Boxweight_KG = float.Parse(BoxWeightPerKGtxt.Text);
            pro.User_Id = User_Id;
            int status = cls.Insert_TransportationCostMaster(pro);

            if (status > 0)
            {
                GetTransporatioanCostGridData();
                CleareData();

            }
        }
        public void CleareData()
        {

        }
        protected void PackingStyleDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PackingStyleDropdownList.SelectedValue == "Select")
            {

            }
            else
            {
                int PackingStyle_Id = Convert.ToInt32(PackingStyleDropdownList.SelectedValue);

                ClsPackingMateriaMaster clsPack = new ClsPackingMateriaMaster();
                DataTable dtPack = new DataTable();
                pro.User_Id = User_Id;
                pro.PackingStyle_Id = PackingStyle_Id;
                dtPack = clsPack.Get_PackingSizeByPackingStyleAndBPM_Id(User_Id, Convert.ToInt32(lblBPM_Id.Text), PackingStyle_Id);

                dtPack.Columns.Add("PackingSizeValue", typeof(string), "Packing_Size + ' (' + Fk_UnitMeasurement_Id + ')'").ToString();
                PackingSizeDropdownList.DataSource = dtPack;
                PackingSizeDropdownList.DataTextField = "PackingUnitMeasurement";
                PackingSizeDropdownList.DataValueField = "PackingSizeValue";
                PackingSizeDropdownList.DataBind();
                PackingSizeDropdownList.Items.Insert(0, "Select");

                //lblPackMeasurement.Text = dtPack.Rows[0]["PackUnitMeasure"].ToString();
                //lblPack_Size.Text = dtPack.Rows[0]["Pack_size"].ToString();

                //if (lblPackMeasurement.Text == "ml")
                //{
                //    lblPackMeasurement.Text = "6";
                //}
                //else if (lblPackMeasurement.Text == "Kg")
                //{
                //    lblPackMeasurement.Text = "2";
                //}
                //else if (lblPackMeasurement.Text == "Ltr")
                //{
                //    lblPackMeasurement.Text = "1";
                //}
                //else if (lblPackMeasurement.Text == "Unit")
                //{
                //    lblPackMeasurement.Text = "3";
                //}
                //else
                //{
                //    lblPackMeasurement.Text = "7";
                //}
            }

        }

        protected void CancalTransportationBtn_Click(object sender, EventArgs e)
        {
            BulkProductDropdownList.SelectedIndex = -1;


            BulkProductDropdownList.SelectedIndex = -1;
            PackingStyleDropdownList.SelectedIndex = -1;
            PackingSizeDropdownList.SelectedIndex = -1;
            BoxWeightPerKGtxt.Text = "";
            lblBPM_Id.Text = "";
            lblPack_Size.Text = "";
        }

        protected void Grid_TransportationCostFactors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GridViewRow row = new GridViewRow(0,6, DataControlRowType.Header, DataControlRowState.Normal);
            //TableHeaderCell cell = new TableHeaderCell();
            //cell.Text = "Gujarat";
            //cell.ColumnSpan = 2;
            //row.Controls.Add(cell);

            //cell = new TableHeaderCell();
            //cell.ColumnSpan = 2;
            //cell.Text = "Maharastra";
            //row.Controls.Add(cell);


            //Grid_TransportationCostFactors.HeaderRow.Parent.Controls.AddAt(6, row);
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    e.Row.Cells[5].Text = "Gujarat";
            //}
        }

        protected void PackingSizeDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PackingSize;
            if (PackingSizeDropdownList.SelectedValue!="Select")
            {
                PackingSize = PackingSizeDropdownList.SelectedValue.ToString();
                lblPack_Size.Text = Regex.Match(PackingSize, @"\d+").Value;
            
                    lblPackMeasurement.Text = PackingSize.Split('(', ')')[1];

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select PackSize')", true);
                return;
            }


            //Get_BPM_Id = lbl_BPM_Id.Text.Substring(0, 3).Trim();

        }

    

        protected void EditTransportationCostFactorsBtn_Click(object sender, EventArgs e)
        {

        }

        protected void DelTransportationCostMasterBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int TransporatationCost_Id = Convert.ToInt32(Grid_TransportationCostMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.TransportationCostMaster_Id = TransporatationCost_Id;
            int status = cls.Delete_TransportationCostMaster(pro);
            if (status > 0)
            {
                GetTransporatioanCostGridData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted ')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Deleted May Be Used in Another Data! ')", true);

            }
        }
    }
}