<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Production_Costing_Software.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Register  Admin</title>
    <link href="css/styles.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/js/all.min.js" crossorigin="anonymous"></script>
    <link href="Content/bootstrap-5.1.0-dist/css/bootstrap.min.css" rel="stylesheet" />
</head>

<body class="bg-primary">
    <div id="layoutAuthentication">
        <div id="layoutAuthentication_content">
            <main>
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-lg-7">
                            <div class="card shadow-lg border-0 rounded-lg mt-5">
                                <div class="card-header">
                                    <h3 class="text-center font-weight-light my-4">Create Account</h3>
                                </div>
                                <div class="card-body">
                                
                                        <div class="row mb-3">
                                            <div class="col-md-6">
                                                <div class="form-floating mb-3 mb-md-0">
                                                    <%--<input  id="inputFirstName" type="text"  />--%>
                                                    <asp:TextBox ID="FullNametxtx" runat="server" type="text" class="form-control" placeholder="Enter Full  Name"></asp:TextBox>
                                                    <label for="inputFirstName">FullName</label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-floating ">
                                                    <asp:DropDownList ID="Gendertxt" CssClass="form-select mb-3 mb-md-0" runat="server">
                                                        <asp:ListItem>--Gender--</asp:ListItem>
                                                        <asp:ListItem>Male</asp:ListItem>
                                                        <asp:ListItem>Female</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-floating mb-3">
                                            <asp:TextBox ID="Emailtxt" TextMode="Email" runat="server" class="form-control" placeholder="Enter Email"></asp:TextBox>
                                            <label for="inputEmail">Email address</label>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-6">
                                                <div class="form-floating mb-3 mb-md-0">
                                                    <asp:TextBox ID="Passwordtxt" TextMode="Password" runat="server" class="form-control" placeholder="Enter Password"></asp:TextBox>
                                                    <label for="inputPassword">Password</label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-floating mb-3 mb-md-0">
                                                    <asp:TextBox ID="RePasswordtxt" TextMode="Password" runat="server" class="form-control" placeholder="Enter Re Password"></asp:TextBox>
                                                    <label for="inputPasswordConfirm">Confirm Password</label>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-6">
                                                <div class="form-floating mb-3 mb-md-0">
                                                    <asp:TextBox ID="Mobiletxt" TextMode="Number" runat="server" class="form-control" placeholder="Enter Mobile"></asp:TextBox>
                                                    <label for="inputMobile">Mobile 10 digit</label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-floating mb-3 mb-md-0">
                                                    <asp:TextBox ID="DOBtxt" runat="server" TextMode="Date" class="form-control" placeholder="Enter DOB"></asp:TextBox>
                                                    <label for="inputPasswordConfirm">Date of Birth</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mt-4 mb-0">
                                            <div class="d-grid">
                                                <asp:Button ID="RegisterBtn" OnClick="RegisterBtn_Click" class="btn btn-success btn-block" runat="server" Text="Register" href="Login.aspx" />
                                            </div>
                                        </div>
                                    
                                </div>
                                <div class="card-footer text-center py-3">
                                    <div class="small"><a href="Login.aspx">Have an account? Go to login</a></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </div>

    </div>
    <div id="layoutAuthentication_footer">
        <footer class="py-4 bg-light fixed-bottom">
            <div class="container-fluid px-4">
                <div class="d-flex align-items-center justify-content-between small">
                    <div class="text-muted">Copyright &copy; emen infotech 2021</div>
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
    <script src="Content/bootstrap-5.1.0-dist/js/bootstrap.js"></script>
</body>

</html>
