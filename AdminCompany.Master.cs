using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Data;

namespace Production_Costing_Software
{
    public partial class AdminCompany : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            if (!IsPostBack)
            {
                try
                {
                    if (Session["UserName"] == null || Session["CompanyMaster_Id"]==null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    else
                    {
                        lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();

                    }
                    UserIdAdmin.Text = Session["UserId"].ToString();
                    lblCompanyMasterList_Id.Text = Session["CompanyMaster_Id"].ToString();
                    lblCompanyMasterList_Name.Text = Session["CompanyMasterList_Name"].ToString();
                    CompanyListDropdown.SelectedValue = (lblCompanyMasterList_Id.Text);
                    AdminCompanyDropdown.SelectedItem.Text = (lblUserNametxt.Text);
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
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();

            }
            //lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
            UserIdAdmin.Text = Session["UserId"].ToString();
            lblGroupId.Text = Session["GroupId"].ToString();
            //lblRoleId.Text = Session["RoleId"].ToString();
            lblUserId.Text = Session["UserId"].ToString();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[0]["GroupId"]);
            //string GetSubMenuUrl = "";
            if (lblGroupId.Text !="1")
            {
                //CompanywiseFactoryExpense--------------------------
                if (Convert.ToInt32(dtMenuList.Rows[23]["SubMenuId"]) == 24 && (Convert.ToBoolean(dtMenuList.Rows[23]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[23]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[23]["CanDelete"]) == true))
                {
                    DivCostfector.Visible = true;
                    HdCostfectorrId.Visible = true;
                    ArrCostfectorId.Visible = true;
                    ulCostfectorid.Visible = true;
                    liCompanywiseFactoryExpenseid.Visible = true;
                }
                //TransportationCostFactors
                if (Convert.ToInt32(dtMenuList.Rows[24]["SubMenuId"]) == 25 && (Convert.ToBoolean(dtMenuList.Rows[24]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[24]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[24]["CanDelete"]) == true))
                {
                    DivCostfector.Visible = true;
                    HdCostfectorrId.Visible = true;
                    ArrCostfectorId.Visible = true;
                    ulCostfectorid.Visible = true;
                    liTransportationCostFactors.Visible = true;
                }
                //StatewiseCostFactors-------------------------------
                if (Convert.ToInt32(dtMenuList.Rows[25]["SubMenuId"]) == 26 && (Convert.ToBoolean(dtMenuList.Rows[25]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[25]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[25]["CanDelete"]) == true))
                {
                    DivCostfector.Visible = true;
                    HdCostfectorrId.Visible = true;
                    ArrCostfectorId.Visible = true;
                    ulCostfectorid.Visible = true;
                    liStatewiseCostFactors.Visible = true;
                }
                //TradeNameMaster
                if (Convert.ToInt32(dtMenuList.Rows[26]["SubMenuId"]) == 27 && (Convert.ToBoolean(dtMenuList.Rows[26]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[26]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[26]["CanDelete"]) == true))
                {
                    DivCostfector.Visible = true;
                    HdCostfectorrId.Visible = true;
                    ArrCostfectorId.Visible = true;
                    ulCostfectorid.Visible = true;
                    liTradeNameMaster.Visible = true;
                }

                //ProductCategoriesMaster
                if (Convert.ToInt32(dtMenuList.Rows[27]["SubMenuId"]) == 28 && (Convert.ToBoolean(dtMenuList.Rows[27]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[27]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[27]["CanDelete"]) == true))
                {
                    DivCostfector.Visible = true;
                    HdCostfectorrId.Visible = true;
                    ArrCostfectorId.Visible = true;
                    ulCostfectorid.Visible = true;
                    liProductCategoriesMaster.Visible = true;
                }
                //CategoryMappingMaster---------------------------------
                if (Convert.ToInt32(dtMenuList.Rows[28]["SubMenuId"]) == 29 && (Convert.ToBoolean(dtMenuList.Rows[28]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[28]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[28]["CanDelete"]) == true))
                {
                    DivCostfector.Visible = true;
                    HdCostfectorrId.Visible = true;
                    ArrCostfectorId.Visible = true;
                    ulCostfectorid.Visible = true;
                    liCategoryMappingMaster.Visible = true;
                }
                //StatewiseFinalPrice-------------------------------
                if (Convert.ToInt32(dtMenuList.Rows[29]["SubMenuId"]) == 30 && (Convert.ToBoolean(dtMenuList.Rows[29]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[29]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[29]["CanDelete"]) == true))
                {
                    DivCostfector.Visible = true;
                    HdCostfectorrId.Visible = true;
                    //ArrCostfectorId.Visible = true;
                    ulCostfectorid.Visible = true;
                    liStatewiseFinalPrice.Visible = false;

                }
                //OtherCompanyPriceList-------------------------------
              if (lblCompanyMasterList_Id.Text != "1")
                {
                    if (Convert.ToInt32(dtMenuList.Rows[31]["SubMenuId"]) == 31 && (Convert.ToBoolean(dtMenuList.Rows[31]["CanView"]) == true || Convert.ToBoolean(dtMenuList.Rows[31]["CanEdit"]) == true || Convert.ToBoolean(dtMenuList.Rows[31]["CanDelete"]) == true))
                    {
                        DivPriceList.Visible = true;
                        HdPriceListId.Visible = true;
                        //ArrCostfectorId.Visible = true;
                        ArrPriceListId.Visible = true;
                        ulOtherCompanyPriceList.Visible = true;
                        liOtherCompanyPriceListMaster.Visible = true;

                    }
                }
                
            }
            else
            {
                DivCostfector.Visible = true;
                HdCostfectorrId.Visible = true;
                ArrCostfectorId.Visible = true;
                ulCostfectorid.Visible = true;
                DivPriceList.Visible = true;
                HdPriceListId.Visible = true;
                ArrPriceListId.Visible = true;
                ulOtherCompanyPriceList.Visible = true;
                liCompanywiseFactoryExpenseid.Visible = true;
                liTransportationCostFactors.Visible = true;
                liStatewiseCostFactors.Visible = true;
                liTradeNameMaster.Visible = true;
                liProductCategoriesMaster.Visible = true;
                liCategoryMappingMaster.Visible = true;
                liStatewiseFinalPrice.Visible = false;
                if (lblCompanyMasterList_Id.Text=="1")
                {
                    liOtherCompanyPriceListMaster.Visible = false;
                    DivPriceList.Visible = false;
                    HdPriceListId.Visible = false;
                    //ArrCostfectorId.Visible = true;
                    ArrPriceListId.Visible = false;
                    ulOtherCompanyPriceList.Visible = false;

                }
                else
                {
                    liOtherCompanyPriceListMaster.Visible = true;

                }

            }

            string strCurrentUrl = HttpContext.Current.Request.Url.AbsoluteUri.Substring(HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf('/') + 1, HttpContext.Current.Request.Url.AbsoluteUri.Length - HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf('/') - 1);
            DataTable dt = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dt = cls.GetRightsPagetByGroupId(pro);
            string str = "";
            for (int iter = 0; iter < dt.Rows.Count; iter++)
            {
                str += dt.Rows[iter]["MenuUrl"].ToString() + ",";
            }

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
            CompanyListDropdown.Items.Insert(0, "Product Costing Software");
        }
        protected void CompanyListDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsCompanyMaster cls = new ClsCompanyMaster();
            ProCompanyMaster pro = new ProCompanyMaster();
            if (CompanyListDropdown.SelectedValue != "Product Costing Software" )
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
                CompanyListDropdown.SelectedValue = lblCompanyMasterList_Id.Text;
                Response.Redirect(Request.Url.AbsoluteUri);

            }
            else
            {
                lblCompanyMasterList_Id.Text = "0";
                Response.Redirect("~/WelcomePage.aspx");
            }
        }

        protected void AdminCompanyDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AdminCompanyDropdown.SelectedValue == "1")
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