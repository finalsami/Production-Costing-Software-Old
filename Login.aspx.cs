using BusinessAccessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class Login : System.Web.UI.Page
    {
        ClsLogin cls = new ClsLogin();
        ProUserLoginRegister pro = new ProUserLoginRegister();
        string UserLoginName;
        string UserLoginPassword;
        string UserGroup;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["UserName"] != null && Session["Password"] != null)
                {
                    if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                    {
                        UserNametxt.Text = Request.Cookies["UserName"].Value;
                        Passwordtxt.Attributes["value"] = Request.Cookies["Password"].Value;
                    }
                    Response.Redirect("~/WelcomePage.aspx");
                }              

            }

        }

        protected void Loginbtn_Click(object sender, EventArgs e)
        {

            try
            {
                pro.UserName = UserNametxt.Text.ToUpper(); ;
                pro.Password = Passwordtxt.Text;
                DataTable dt = new DataTable();
                dt = cls.UserLogin(pro);
                if (dt.Rows.Count > 0)
                
                {
                    UserLoginName = dt.Rows[0]["UserName"].ToString().ToUpper();
                    UserLoginPassword = dt.Rows[0]["Password"].ToString();
                    UserGroup = dt.Rows[0]["GroupId"].ToString();
                    //UserRole = dt.Rows[0]["RoleId"].ToString();
                    try
                    {
                        if (UserNametxt.Text.ToUpper() == UserLoginName && Passwordtxt.Text == UserLoginPassword)
                        {
                            lblUserId.Text = dt.Rows[0]["UserId"].ToString();
                            lblGroupId.Text = dt.Rows[0]["GroupId"].ToString();
                            //lblRoleId.Text = dt.Rows[0]["RoleId"].ToString();

                            Session["UserName"] = UserNametxt.Text;
                            Session["Password"] = Passwordtxt.Text;
                            Session["UserId"] = lblUserId.Text;
                            Session["GroupId"] = UserGroup;
                            //Session["RoleId"] = UserRole;



                            if (chkRememberMe.Checked)
                            {
                                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                                Response.Cookies["UserId"].Expires = DateTime.Now.AddDays(30);
                                Response.Cookies["GroupId"].Expires = DateTime.Now.AddDays(30);
                                //Response.Cookies["RoleId"].Expires = DateTime.Now.AddDays(30);
                               
                            }
                            else
                            {
                                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies["UserId"].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies["GroupId"].Expires = DateTime.Now.AddDays(-1);
                                //Response.Cookies["RoleId"].Expires = DateTime.Now.AddDays(-1);

                            }
                            Response.Cookies["UserName"].Value = UserNametxt.Text.Trim();
                            Response.Cookies["Password"].Value = Passwordtxt.Text.Trim();
                            Response.Cookies["UserId"].Value = lblUserId.Text.Trim();
                            Response.Cookies["GroupId"].Value = lblUserId.Text.Trim();
                            //Response.Cookies["RoleId"].Value = lblUserId.Text.Trim();
                            Response.Redirect("~/WelcomePage.aspx");

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Login Failed !')", true);
                        }




                    }
                    catch (Exception)
                    {
                        throw;

                    }


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No User Found  !')", true);
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}

