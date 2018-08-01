<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FoodieGoals.Web.Login" %>

<!DOCTYPE html>





<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <title></title>

    <link href="library/styles/bootstrap.min.css" rel="stylesheet" />
    <link href="library/styles/pagelogin.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Lato:300,400,400i,700" />

</head>

<body class="text-center">

    <div class="loginmaincontainer">

        <h1 class="mb-3">Please Sign In</h1>

        <div id="divLogin">
            <form>
                <input class="form-control" type="email" id="loginEmail" placeholder="Email" />
                <input class="form-control" type="password" id="loginPassword" placeholder="Password" />
                <button class="btn btn-large" id="btnSignIn">Sign In</button>
            </form>
        </div>

        <hr />

        <h3>- or -</h3>

        <hr />

        <h1 class="mb-3">Sign Up</h1>        

        <div id="divSignup">
            <form>
                <input class="form-control" type="email" id="signupEmail" placeholder="Email" />
                <input class="form-control" type="password" id="signupPassword" placeholder="Password" />
                <input class="form-control" type="password" id="signupPasswordAgain" placeholder="Confirm Password" />
                <button class="btn" id="btnSignUp">Sign Up</button>
            </form>
        </div>

    </div>

    <script src="library/scripts/external/jquery-1.10.2.min.js"></script>
    <script src="library/scripts/helpers.js"></script>
    <script src="library/scripts/system.js"></script>
    <script src="library/scripts/app.js"></script>

    <script>
        $(document).ready(function () {
            console.log("Ready!");
            System.InitializeLoginPage();
        });
    </script>
</body>






</html>
