<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FoodieGoals.Web.Login" %>

<!DOCTYPE html>





<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Lato:300,400,400i,700" />

</head>

<body>
    <h1>Login Page!</h1>

    <div id="divLogin">
        <form>
            <div>
                <input type="text" id="loginEmail" placeholder="Email" />
            </div>
            <div>
                <input type="password" id="loginPassword" placeholder="Password" />
            </div>
            <button id="btnSignIn">Sign In</button>
        </form>
    </div>

    <hr />

    <div id="divSignup">
        <form>
            <div>
                <input type="text" id="signupEmail" placeholder="Email" />
            </div>
            <div>
                <input type="password" id="signupPassword" placeholder="Password" />
            </div>
            <div>
                <input type="password" id="signupPasswordAgain" placeholder="Confirm Password" />
            </div>
            <button id="btnSignUp">Sign Up</button>
        </form>
    </div>

    <hr />

    <div>
        <button id="btnTest">Test</button>
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
