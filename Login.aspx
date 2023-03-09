<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Production_Costing_Software.Login" %>

<%@ Import Namespace="System.Web.Security" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Login - Cost Production Software</title>
    <link href="css/styles.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/js/all.min.js" crossorigin="anonymous"></script>
    <link href="Content/bootstrap-5.1.0-dist/css/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript">  
        function alertMessage() {
            alert('Please Login !');
        }
    </script>
</head>


<body class="bg-primary">

    <div id="layoutAuthentication">
        <div id="layoutAuthentication_content">
            <main>
                <div class="container">
                    <div class="row mt-4">
                        <div class="col-md-4 offset-md-4">
                            <div class="login-form bg-light mt-4 p-4 mt-5" style="border-radius: 5px; box-shadow: 0px 10px 13px -7px #000000, 5px 5px 15px 5px rgba(0,0,0,0);">
                                <div>
                                    <h4 class="text-center">
                                        <img src="Content/LogoImage/emen%20logo.png" style="width: 100px; height: 22px" /></h4>
                                    <h6 class="text-center " style="text-shadow: 2px 2px 2px rgba(23,10,6,0.22); font-size: smaller">RETAIL INFOTECH PVT.LTD.</h6>
                                </div>
                                <form runat="server" class="row g-3" autocomplete="on">

                                    <%--                                    <h5 class="text-center" style="text-shadow: 2px 2px 2px rgba(23,10,6,0.39); font-weight: bolder; font-size: x-large">Login</h5>--%>
                                    <div class="col-12">
                                        <div class="input-group mb-3" style="border-radius: 5px; box-shadow: 0px 0px 12px -7px #000000, 5px 5px 15px 5px rgba(0,0,0,0);">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="User">UserName</span>
                                            </div>
                                            <%--<input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="User">--%>
                                            <asp:TextBox ID="UserNametxt" runat="server" class="form-control" aria-label="Sizing example input" aria-describedby="User"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="input-group mb-3" style="border-radius: 5px; box-shadow: 0px 0px 12px -7px #000000, 5px 5px 15px 5px rgba(0,0,0,0);">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="Pass">Password</span>
                                            </div>
                                            <asp:TextBox ID="Passwordtxt" TextMode="Password" runat="server" class="form-control" aria-label="Sizing example input" aria-describedby="User"></asp:TextBox>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblCompanyMasterList_Name" runat="server" Visible="false" Text=""></asp:Label>
                                    <asp:Label ID="lblRoleId" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblGroupId" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblUserId" runat="server" Visible="false" Text=""></asp:Label>
                                    <div class="col-md-6">
                                        <div class="form-check">
                                            <asp:CheckBox ID="chkRememberMe" runat="server" />
                                            <label class="form-check-label" for="rememberMe">Remember me</label>
                                        </div>
                                    </div>
                                    <div class="mt-4 mb-0">
                                        <div class=" col-6 col-lg-12 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="Loginbtn" OnClick="Loginbtn_Click" Style="border-radius: 5px; box-shadow: 0px 8px 13px -7px #000000, 5px 5px 15px 5px rgba(0,0,0,0);" class="btn btn-success btn-group-lg align-content-center" runat="server" Text="Login" />
                                            </div>
                                        </div>
                                    </div>
                                    <hr class="mt-4" />
                                    <%--<div class="col-12">
                                    <p class="text-center mb-0">Have not account yet? <a href="Register.aspx">Signup</a></p>
                                </div>--%>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </div>

    </div>

    <div id="layoutAuthentication_footer">
        <footer class="py-4 bg-light fixed-bottom ">
            <div class="container-fluid px-4">
                <div class="d-flex align-items-center justify-content-between small">
                    <div class="text-muted">Copyright &copy; Emen InfoTech 2021</div>
                    <div>
                        <a href="#">Privacy Policy</a>
                        &middot;
                               
                                    <a href="#">Terms &amp; Conditions</a>
                    </div>
                </div>
            </div>
        </footer>
    </div>


    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
    <script src="js/scripts.js"></script>
</body>

<script src="Content/bootstrap-5.1.0-dist/js/bootstrap.js"></script>




</html>
