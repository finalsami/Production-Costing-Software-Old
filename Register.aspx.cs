using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
namespace Production_Costing_Software
{
    
    public partial class Register : System.Web.UI.Page
    {
        ClsRegisterMaster cls = new ClsRegisterMaster();
        ProUserLoginRegister pro = new ProUserLoginRegister();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            pro.FullName = FullNametxtx.Text;
            pro.Mobile = Mobiletxt.Text;
            pro.Password = Passwordtxt.Text;
            pro.Email = Emailtxt.Text;
            pro.DOB = Convert.ToDateTime(DOBtxt.Text);
            if (Gendertxt.SelectedValue=="Male")
            {
                pro.Gender = "1";
            }
            else if(Gendertxt.SelectedValue == "Female")
            {
                pro.Gender = "0";
            }
            else
            {
                pro.Gender = "NULL";
            }

            int status = cls.InsertRegisterMasterData(pro);

            if (status > 0)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}