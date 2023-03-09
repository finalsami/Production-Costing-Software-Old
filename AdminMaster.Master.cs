using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{

    public partial class AdminMaster : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {
                Response.Redirect("~/Login.aspx", true);
            }
            GetLoginDetails();
            if (!IsPostBack)
            {
                try
                {
                    lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                    UserIdAdmin.Text = Session["UserId"].ToString();
                    lblGroupId.Text = Session["GroupId"].ToString();
                    //lblRoleId.Text = Session["RoleId"].ToString();
                    lblUserId.Text = Session["UserId"].ToString();
                    AdminDropdown.SelectedItem.Text = (lblUserNametxt.Text);

                }
                catch (Exception)
                {

                    Response.Redirect("~/Login.aspx");


                }

                CompanyListDropDownListCombo();

                ShowMenulist();
            }
        }
        public void GetLoginDetails()
        {

            lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
            UserIdAdmin.Text = Session["UserId"].ToString();
            lblGroupId.Text = Session["GroupId"].ToString();
            //lblRoleId.Text = Session["RoleId"].ToString();
            lblUserId.Text = Session["UserId"].ToString();
            AdminDropdown.SelectedItem.Text = (lblUserNametxt.Text);
        }
        public void ShowMenulist()
        {
            dvPages.Visible = true;
            dvAccessDenied.Visible = false;

            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            //DataTable dtRole = new DataTable();
            //ClsRoleMaster clsRole = new ClsRoleMaster();

            //dtRole = clsRole.Get_AllGroupNameFromRoleMaster(pro);
            //int GroupId = Convert.ToInt32(dtRole.Rows[0]["GroupId"]);
            //string GetSubMenuUrl = "";
            if (lblGroupId.Text != "1")
            {
                //PriceMaster--------------------------
                if (Convert.ToInt32(dtMenuList.Rows[0]["SubMenuId"]) == 1 && (Convert.ToBoolean(dtMenuList.Rows[0]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[0]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[0]["CanDelete"]) == true))
                {
                    DivPriceHidden.Visible = true;
                    HdPriceMasterId.Visible = true;
                    ArrPriceMasterId.Visible = true;
                    ulPriceid.Visible = true;
                    liRMPriceMasterid.Visible = true;
                }

                if (Convert.ToInt32(dtMenuList.Rows[1]["SubMenuId"]) == 2 && (Convert.ToBoolean(dtMenuList.Rows[1]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[1]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[1]["CanDelete"]) == true))
                {
                    DivPriceHidden.Visible = true;
                    HdPriceMasterId.Visible = true;
                    ArrPriceMasterId.Visible = true;
                    ulPriceid.Visible = true;
                    liPM_RM_PriceMasterid.Visible = true;
                }
                //Costing-------------------------------
                if (Convert.ToInt32(dtMenuList.Rows[2]["SubMenuId"]) == 3 && (Convert.ToBoolean(dtMenuList.Rows[2]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[2]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[2]["CanDelete"]) == true))
                {
                    DivCostHidden.Visible = true;
                    HdCostId.Visible = true;
                    ArrCostId.Visible = true;
                    ulSubCostingid.Visible = true;
                    liCostVariableid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[3]["SubMenuId"]) == 4 && (Convert.ToBoolean(dtMenuList.Rows[3]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[3]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[3]["CanDelete"]) == true))
                {
                    DivCostHidden.Visible = true;

                    HdCostId.Visible = true;
                    ArrCostId.Visible = true;
                    ulSubCostingid.Visible = true;
                    liBulkRecipeBOMid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[4]["SubMenuId"]) == 5 && (Convert.ToBoolean(dtMenuList.Rows[4]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[4]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[4]["CanDelete"]) == true))
                {
                    DivCostHidden.Visible = true;

                    HdCostId.Visible = true;
                    ArrCostId.Visible = true;
                    ulSubCostingid.Visible = true;
                    liProductwiseLabourCostid.Visible = true;
                }
                //Report---------------------------------
                if (Convert.ToInt32(dtMenuList.Rows[5]["SubMenuId"]) == 6 && (Convert.ToBoolean(dtMenuList.Rows[5]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[5]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[5]["CanDelete"]) == true))
                {
                    DivReportHidden.Visible = true;
                    HDReportingid.Visible = true;
                    ArrReportId.Visible = true;
                    ulSubReportid.Visible = true;
                    liFinishGoodsPricingReportid.Visible = true;
                }
                //Category-------------------------------
                if (Convert.ToInt32(dtMenuList.Rows[6]["SubMenuId"]) == 7 && (Convert.ToBoolean(dtMenuList.Rows[6]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[6]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[6]["CanDelete"]) == true))
                {
                    DivCategoryHidden.Visible = true;
                    DHCategoriesid.Visible = true;
                    ArrCategoryId.Visible = true;
                    ulCategoriesid.Visible = true;
                    liMainCategoryid.Visible = true;

                }
                if (Convert.ToInt32(dtMenuList.Rows[7]["SubMenuId"]) == 8 && (Convert.ToBoolean(dtMenuList.Rows[7]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[7]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[7]["CanDelete"]) == true))
                {
                    DivCategoryHidden.Visible = true;
                    DHCategoriesid.Visible = true;
                    ArrCategoryId.Visible = true;
                    ulCategoriesid.Visible = true;
                    liRMCategoryid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[8]["SubMenuId"]) == 9 && (Convert.ToBoolean(dtMenuList.Rows[8]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[8]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[8]["CanDelete"]) == true))
                {
                    DivCategoryHidden.Visible = true;
                    DHCategoriesid.Visible = true;
                    ArrCategoryId.Visible = true;
                    ulCategoriesid.Visible = true;
                    liPM_RM_Categoryid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[9]["SubMenuId"]) == 10 && (Convert.ToBoolean(dtMenuList.Rows[9]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[9]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[9]["CanDelete"]) == true))
                {
                    DivCategoryHidden.Visible = true;
                    DHCategoriesid.Visible = true;
                    ArrCategoryId.Visible = true;
                    ulCategoriesid.Visible = true;
                    liPackingCategoryid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[10]["SubMenuId"]) == 11 && (Convert.ToBoolean(dtMenuList.Rows[10]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[10]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[10]["CanDelete"]) == true))
                {
                    DivCategoryHidden.Visible = true;
                    DHCategoriesid.Visible = true;
                    ArrCategoryId.Visible = true;
                    ulCategoriesid.Visible = true;
                    liPackingStyleCategoryid.Visible = true;
                }
                //if (Convert.ToInt32(dtMenuList.Rows[11]["SubMenuId"]) == 18 && (Convert.ToBoolean(dtMenuList.Rows[11]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[11]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[11]["CanDelete"]) == true))
                //{
                //    DivCategoryHidden.Visible = true;
                //}
                //Masters--------------------------------
                if (Convert.ToInt32(dtMenuList.Rows[11]["SubMenuId"]) == 12 && (Convert.ToBoolean(dtMenuList.Rows[11]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[11]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[11]["CanDelete"]) == true))
                {
                    DivMasterHidden.Visible = true;
                    HDMastersid.Visible = true;
                    ArrMasterId.Visible = true;
                    ulMastersid.Visible = true;
                    liRM_Masterid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[12]["SubMenuId"]) == 13 && (Convert.ToBoolean(dtMenuList.Rows[12]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[12]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[12]["CanDelete"]) == true))
                {
                    DivMasterHidden.Visible = true;
                    HDMastersid.Visible = true;
                    ArrMasterId.Visible = true;
                    ulMastersid.Visible = true;
                    liBulkProductMasterid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[13]["SubMenuId"]) == 14 && (Convert.ToBoolean(dtMenuList.Rows[13]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[13]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[13]["CanDelete"]) == true))
                {
                    DivMasterHidden.Visible = true;
                    HDMastersid.Visible = true;
                    ArrMasterId.Visible = true;
                    ulMastersid.Visible = true;
                    liFormulationMasterid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[14]["SubMenuId"]) == 15 && (Convert.ToBoolean(dtMenuList.Rows[14]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[14]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[14]["CanDelete"]) == true))
                {
                    DivMasterHidden.Visible = true;
                    HDMastersid.Visible = true;
                    ArrMasterId.Visible = true;
                    ulMastersid.Visible = true;
                    liProductInterestMasterid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[15]["SubMenuId"]) == 16 && (Convert.ToBoolean(dtMenuList.Rows[15]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[15]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[15]["CanDelete"]) == true))
                {
                    DivMasterHidden.Visible = true;
                    HDMastersid.Visible = true;
                    ArrMasterId.Visible = true;
                    ulMastersid.Visible = true;
                    liPM_RM_Masterid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[16]["SubMenuId"]) == 17 && (Convert.ToBoolean(dtMenuList.Rows[16]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[16]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[16]["CanDelete"]) == true))
                {
                    DivMasterHidden.Visible = true;
                    HDMastersid.Visible = true;
                    ArrMasterId.Visible = true;
                    ulMastersid.Visible = true;
                    liPackingStyleNameid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[17]["SubMenuId"]) == 18 && (Convert.ToBoolean(dtMenuList.Rows[17]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[17]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[17]["CanDelete"]) == true))
                {
                    DivMasterHidden.Visible = true;
                    HDMastersid.Visible = true;
                    ArrMasterId.Visible = true;
                    ulMastersid.Visible = true;
                    liPackingMaterialMasterid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[18]["SubMenuId"]) == 19 && (Convert.ToBoolean(dtMenuList.Rows[18]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[18]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[18]["CanDelete"]) == true))
                {
                    DivMasterHidden.Visible = true;
                    HDMastersid.Visible = true;
                    ArrMasterId.Visible = true;
                    ulMastersid.Visible = true;
                    liPackingStyleMasterid.Visible = true;
                }
                // ComapnyMaster----------------------
                if (Convert.ToInt32(dtMenuList.Rows[19]["SubMenuId"]) == 20 && (Convert.ToBoolean(dtMenuList.Rows[19]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[19]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[19]["CanDelete"]) == true))
                {
                    DivCompanyHidden.Visible = true;
                    HDCompanyid.Visible = true;
                    ArrComapanyId.Visible = true;
                    ulCompanyMasterid.Visible = true;
                    liCompanyMasterid.Visible = true;
                }
                //UserManagement---------------------
                if (Convert.ToInt32(dtMenuList.Rows[20]["SubMenuId"]) == 21 && (Convert.ToBoolean(dtMenuList.Rows[20]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[20]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[20]["CanDelete"]) == true))
                {
                    DivUserMgmtHidden.Visible = true;
                    HDUserManagementid.Visible = true;
                    ArrUsermgntId.Visible = true;
                    ulUserManaghementid.Visible = true;
                    liUserMasterid.Visible = true;
                }
                if (Convert.ToInt32(dtMenuList.Rows[21]["SubMenuId"]) == 22 && (Convert.ToBoolean(dtMenuList.Rows[21]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[21]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[21]["CanDelete"]) == true))
                {
                    DivUserMgmtHidden.Visible = true;
                    HDUserManagementid.Visible = true;
                    ArrUsermgntId.Visible = true;
                    ulUserManaghementid.Visible = true;
                    liRoleMasterid.Visible = true;
                }
                //RMEstimationReport-------------------------------
                if (Convert.ToInt32(dtMenuList.Rows[31]["SubMenuId"]) == 31 && (Convert.ToBoolean(dtMenuList.Rows[31]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[31]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[31]["CanDelete"]) == true))
                {
                    DivPriceEsitmationReport.Visible = true;
                    DivEsitmationReport.Visible = true;
                    //ArrCostfectorId.Visible = true;
                    ulEsitmation.Visible = true;
                    liRMEstimationReport.Visible = true;

                }
                //PriceEsitmationReport------------------------------ -
                if (Convert.ToInt32(dtMenuList.Rows[32]["SubMenuId"]) == 32 && (Convert.ToBoolean(dtMenuList.Rows[32]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[32]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[32]["CanDelete"]) == true))
                {
                    DivPriceEsitmationReport.Visible = true;
                    DivEsitmationReport.Visible = true;
                    //ArrCostfectorId.Visible = true;
                    ulEsitmation.Visible = true;
                    liPMRMEstimationReport.Visible = true;

                }
                if (Convert.ToInt32(dtMenuList.Rows[33]["SubMenuId"]) == 33 && (Convert.ToBoolean(dtMenuList.Rows[33]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[33]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[33]["CanDelete"]) == true))
                {
                    DivPriceEsitmationReport.Visible = true;
                    DivEsitmationReport.Visible = true;
                    //ArrCostfectorId.Visible = true;
                    ulEsitmation.Visible = true;
                    liPriceList.Visible = true;

                }
                if (Convert.ToInt32(dtMenuList.Rows[34]["SubMenuId"]) == 34 && (Convert.ToBoolean(dtMenuList.Rows[34]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[34]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[34]["CanDelete"]) == true))
                {
                    DivPriceEsitmationReport.Visible = true;
                    DivEsitmationReport.Visible = true;
                    //ArrCostfectorId.Visible = true;
                    ulEsitmation.Visible = true;
                    liPriceList.Visible = true;

                }
                if (Convert.ToInt32(dtMenuList.Rows[35]["SubMenuId"]) == 35 && (Convert.ToBoolean(dtMenuList.Rows[35]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[35]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[35]["CanDelete"]) == true))
                {
                    DivPriceEsitmationReport.Visible = true;
                    DivEsitmationReport.Visible = true;
                    //ArrCostfectorId.Visible = true;
                    ulEsitmation.Visible = true;
                    liPriceList.Visible = true;

                }
                if (Convert.ToInt32(dtMenuList.Rows[36]["SubMenuId"]) == 36 && (Convert.ToBoolean(dtMenuList.Rows[36]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[36]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[36]["CanDelete"]) == true))
                {
                    DivPriceEsitmationReport.Visible = true;
                    DivEsitmationReport.Visible = true;
                    //ArrCostfectorId.Visible = true;
                    ulEsitmation.Visible = true;
                    liPriceLis_GP.Visible = true;


                }
                if (Convert.ToInt32(dtMenuList.Rows[37]["SubMenuId"]) == 37 && (Convert.ToBoolean(dtMenuList.Rows[37]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[37]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[37]["CanDelete"]) == true))
                {
                    DivPriceEsitmationReport.Visible = true;
                    DivEsitmationReport.Visible = true;
                    //ArrCostfectorId.Visible = true;
                    ulEsitmation.Visible = true;
                    liPriceLis_GP.Visible = true;
                    liPriceList_GP_Actual.Visible = true;
                    liPriceList_GP_ActualEstimate.Visible = true;
                    liPriceList_Other_Actual.Visible = true;


                }
                if (Convert.ToInt32(dtMenuList.Rows[37]["SubMenuId"]) == 38 && (Convert.ToBoolean(dtMenuList.Rows[38]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[38]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[38]["CanDelete"]) == true))
                {
                    DivPriceEsitmationReport.Visible = true;
                    DivEsitmationReport.Visible = true;
                    //ArrCostfectorId.Visible = true;
                    ulEsitmation.Visible = true;
                    liPriceLis_GP.Visible = true;
                    liPriceList_GP_Actual.Visible = true;
                    liPriceList_GP_ActualEstimate.Visible = true;
                    liMRP_Price.Visible = true;
                    liPriceList_Other_Actual.Visible = true;


                }
            }
            else
            {
                //---------PriceMaster-----------
                DivPriceHidden.Visible = true;
                HdPriceMasterId.Visible = true;
                ArrPriceMasterId.Visible = true;
                ulPriceid.Visible = true;
                liRMPriceMasterid.Visible = true;
                //--------PMRM PriceMaster-----------
                DivPriceHidden.Visible = true;
                ArrPriceMasterId.Visible = true;
                ulPriceid.Visible = true;
                liPM_RM_PriceMasterid.Visible = true;
                //-----------Cost--------------
                DivCostHidden.Visible = true;
                HdCostId.Visible = true;
                ArrCostId.Visible = true;
                ulSubCostingid.Visible = true;
                liCostVariableid.Visible = true;
                liBulkRecipeBOMid.Visible = true;
                liProductwiseLabourCostid.Visible = true;

                //--------Report-----------------

                DivReportHidden.Visible = true;
                HDReportingid.Visible = true;
                ArrReportId.Visible = true;
                ulSubReportid.Visible = true;
                liFinishGoodsPricingReportid.Visible = true;
                //---------Categories----------------
                DivCategoryHidden.Visible = true;
                DHCategoriesid.Visible = true;
                ArrCategoryId.Visible = true;
                ulCategoriesid.Visible = true;
                liMainCategoryid.Visible = true;
                liRMCategoryid.Visible = true;
                liPM_RM_Categoryid.Visible = true;
                liPackingCategoryid.Visible = true;
                liPackingStyleCategoryid.Visible = true;

                //----------Masters-------------------
                DivMasterHidden.Visible = true;
                HDMastersid.Visible = true;
                ArrMasterId.Visible = true;
                ulMastersid.Visible = true;
                liRM_Masterid.Visible = true;
                liBulkProductMasterid.Visible = true;
                liFormulationMasterid.Visible = true;
                liProductInterestMasterid.Visible = true;
                liPM_RM_Masterid.Visible = true;
                liPackingStyleNameid.Visible = true;
                liPackingMaterialMasterid.Visible = true;
                liPackingStyleMasterid.Visible = true;
                liPackingDifferenceMaster.Visible = true;
                //---------Company-------------------
                DivCompanyHidden.Visible = true;
                HDCompanyid.Visible = true;
                ArrComapanyId.Visible = true;
                ulCompanyMasterid.Visible = true;
                liCompanyMasterid.Visible = true;
                //-----UserManagement-----------
                DivUserMgmtHidden.Visible = true;
                HDUserManagementid.Visible = true;
                ArrUsermgntId.Visible = true;
                ulUserManaghementid.Visible = true;
                liUserMasterid.Visible = true;
                liRoleMasterid.Visible = true;
                //------------Estimation-------------------------
                DivEsitmationReport.Visible = true;
                DivPriceEsitmationReport.Visible = true;
                DivArrEstimateReport.Visible = true;
                liRMEstimationReport.Visible = true;
                liPMRMEstimationReport.Visible = true;
                liPriceList_GP_Actual.Visible = true;
                liMRP_Price.Visible = true;
                liPriceList_Other_Actual.Visible = true;
                //------------PriceList-------------------------
                DivPriceList.Visible = true;
                DivPriceList1.Visible = true;
                DivArrPriceList.Visible = true;
                liEsitmationPriceList.Visible = false;
                liPriceList.Visible = true;
                liPriceLis_GP.Visible = false;
            }

            string strCurrentUrl = HttpContext.Current.Request.Url.AbsoluteUri.Substring(HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf('/') + 1, HttpContext.Current.Request.Url.AbsoluteUri.Length - HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf('/') - 1);
            if (strCurrentUrl.Contains("?"))
            {
                strCurrentUrl = strCurrentUrl.Substring(0, strCurrentUrl.IndexOf("?"));
            }
            DataTable dt = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dt = cls.GetRightsPagetByGroupId(pro);
            string str = "";
            for (int iter = 0; iter < dt.Rows.Count; iter++)
            {
                str += dt.Rows[iter]["MenuUrl"].ToString() + ",";
            }

            str += "PriceList_GP_ActualEstimateNew.aspx,";


            if (!str.Contains(strCurrentUrl))
            {
                //Response.Redirect("~/AccessDenied.aspx");
                dvPages.Visible = false;
                dvAccessDenied.Visible = true;
            }
        }
        public void CompanyListDropDownListCombo()
        {
            ClsCompanyMaster cls = new ClsCompanyMaster();

            DataTable dt = new DataTable();
            //pro.User_Id = User_Id;
            dt = cls.Get_CompanyMasterData();

            CompanyListDropdown.DataSource = dt;
            CompanyListDropdown.DataTextField = "CompanyMaster_Name";

            CompanyListDropdown.DataValueField = "CompanyMaster_Id";
            CompanyListDropdown.DataBind();
            CompanyListDropdown.Items.Insert(0, "Select Company");
        }
        protected void CompanyListDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsCompanyMaster cls = new ClsCompanyMaster();
            ProCompanyMaster pro = new ProCompanyMaster();
            if (CompanyListDropdown.SelectedValue != "Select Company")
            {
                int CompanyList_Id = Convert.ToInt32(CompanyListDropdown.SelectedValue);
                pro.CompanyMaster_Id = CompanyList_Id;
                lblCompanyMasterList_Id.Text = CompanyList_Id.ToString();
                DataTable dt = new DataTable();
                dt = cls.Get_CompanyMasterBy_Id(pro);
                lblCompanyMasterList_Name.Text = dt.Rows[0]["CompanyMaster_Name"].ToString();

                Session["CompanyMasterList_Name"] = lblCompanyMasterList_Name.Text;
                Session["CompanyMaster_Id"] = lblCompanyMasterList_Id.Text;
                Response.Cookies["CompanyMasterList_Name"].Value = lblCompanyMasterList_Name.Text.Trim();
                Response.Cookies["CompanyMaster_Id"].Value = lblCompanyMasterList_Id.Text.Trim();
                Response.Redirect("~/WelcomePageCompany.aspx");
            }
            else
            {
                lblCompanyMasterList_Id.Text = "0";
            }

        }

        protected void AdminDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AdminDropdown.SelectedValue == "1")
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
            else
            {

            }
        }
    }
}