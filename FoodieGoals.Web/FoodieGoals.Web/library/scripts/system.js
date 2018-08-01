var System;
(function (System) {
    "use strict";
    function Initialize() {
        WebApi.InitializeWebApi();
    }
    System.Initialize = Initialize;
    function InitializeLoginPage() {
        Authentication.ClearToken();
        Initialize();
        Authentication.BindLoginForms();
    }
    System.InitializeLoginPage = InitializeLoginPage;
    var Authentication;
    (function (Authentication) {
        var tokenKey = 'accessToken';
        function IsAuthenticated() {
            if (GetToken())
                return true;
            else
                return false;
        }
        Authentication.IsAuthenticated = IsAuthenticated;
        function SetToken(token) {
            sessionStorage.setItem(tokenKey, token);
        }
        Authentication.SetToken = SetToken;
        function GetToken() {
            return sessionStorage.getItem(tokenKey);
        }
        Authentication.GetToken = GetToken;
        function ClearToken() {
            sessionStorage.removeItem(tokenKey);
        }
        Authentication.ClearToken = ClearToken;
        function BindLoginForms() {
            BindSignIn();
            BindSignUp();
        }
        Authentication.BindLoginForms = BindLoginForms;
        function BindSignIn() {
            $("#btnSignIn").click(function (e) {
                e.stopImmediatePropagation();
                e.preventDefault();
                var loginData = {
                    grant_type: 'password',
                    username: $("#loginEmail").val(),
                    password: $("#loginPassword").val()
                };
                WebApi.PostLogin("Token", loginData, LoginSuccess, LoginFailure);
                //$.ajax({
                //    type: 'POST',
                //    url: '/Token',
                //    data: loginData
                //}).done(function (data) {
                //    self.user(data.userName);
                //    // Cache the access token in session storage.
                //    sessionStorage.setItem(tokenKey, data.access_token);
                //}).fail(showError);
            });
            function LoginSuccess(data) {
                console.log("Login Success!");
                sessionStorage.setItem(tokenKey, data.access_token);
                //Redirect to page
                window.location.href = window.location.origin + "/bootstrap.html";
            }
            function LoginFailure(status, error) {
                alert("Failed to login");
                console.log(error);
            }
        }
        function BindSignUp() {
            $("#btnSignUp").click(function (e) {
                e.stopImmediatePropagation();
                e.preventDefault();
                var data = {
                    Email: $("#signupEmail").val(),
                    Password: $("#signupPassword").val(),
                    ConfirmPassword: $("#signupPasswordAgain").val(),
                };
                //WebApi.Post("Token", loginData, LoginSuccess, LoginFailure);
                WebApi.Post("Account/Register", data, SignupSuccess);
            });
            function SignupSuccess(data) {
            }
        }
    })(Authentication = System.Authentication || (System.Authentication = {}));
    var WebApi;
    (function (WebApi) {
        var apiOrigin = "http://foodiegoalsapi.azurewebsites.net/api/";
        var token;
        var headers = {};
        function InitializeWebApi() {
            if (window.location.origin.search("local") > 0) {
                apiOrigin = "http://api.foodiegoals.local/api/";
            }
            else {
                apiOrigin = "http://foodiegoalsapi.azurewebsites.net/api/";
            }
            token = Authentication.GetToken();
            if (token) {
                headers["Authorization"] = 'Bearer ' + token;
            }
        }
        WebApi.InitializeWebApi = InitializeWebApi;
        function Get(path, options, successMethod, failureMethod, returnContext) {
            var params = {
                url: apiOrigin + path,
                headers: headers,
                data: options,
                method: "GET"
            };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }
        WebApi.Get = Get;
        function Post(path, options, successMethod, failureMethod, returnContext) {
            var params = {
                url: apiOrigin + path,
                headers: headers,
                data: options,
                dataType: "json",
                method: "POST"
            };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }
        WebApi.Post = Post;
        function PostLogin(path, options, successMethod, failureMethod, returnContext) {
            var params = { url: apiOrigin.substr(0, apiOrigin.indexOf('api/')) + path, headers: headers, data: options, dataType: "json", method: "POST" };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }
        WebApi.PostLogin = PostLogin;
        function Put(path, options, successMethod, failureMethod, returnContext) {
            var params = {
                url: apiOrigin + path,
                headers: headers,
                data: options,
                dataType: "json",
                method: "PUT"
            };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }
        WebApi.Put = Put;
        function Delete(path, options, successMethod, failureMethod, returnContext) {
            var params = {
                url: apiOrigin + path,
                headers: headers,
                data: options,
                dataType: "json",
                method: "DELETE"
            };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }
        WebApi.Delete = Delete;
        function ExecuteAjax(params, successMethod, failureMethod, returnContext) {
            $.ajax(params).then(function (data, textStatus, xhr) {
                if (Helpers.IsNotNullNOREmpty(successMethod))
                    successMethod(data, returnContext);
                else
                    GenericAPISuccess(data);
            }, function (xhr, status, error) {
                if (xhr.status == 401) {
                    CleanUpAndRedirectToLogin();
                }
                var compiledError = xhr.status + " Error - " + xhr.statusText;
                var response = xhr.responseJSON;
                if (Helpers.IsNotNullNOREmpty(response)) {
                    if (response.Message) {
                        compiledError += ": " + response.Message;
                    }
                    if (response.ExceptionMessage) {
                        compiledError += ": " + response.ExceptionMessage;
                    }
                    if (response.error) {
                        compiledError += ": " + response.error;
                    }
                    if (response.error_description) {
                        compiledError += ": " + response.error_description;
                    }
                }
                if (Helpers.IsNotNullNOREmpty(failureMethod)) {
                    failureMethod(xhr.status, compiledError, returnContext);
                }
                else {
                    GenericAPIFail(compiledError);
                }
            });
            function HandleError(xhr) {
            }
        }
        function CleanUpAndRedirectToLogin() {
            Authentication.ClearToken();
            alert("401 error.");
        }
        function GenericAPISuccess(data) {
            console.log(JSON.stringify(data, null, 2));
        }
        function GenericAPIFail(compiledError) {
            alert(compiledError);
        }
    })(WebApi = System.WebApi || (System.WebApi = {}));
})(System || (System = {}));
//# sourceMappingURL=system.js.map