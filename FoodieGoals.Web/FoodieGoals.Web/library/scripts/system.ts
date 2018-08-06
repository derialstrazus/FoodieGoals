module System {
    "use strict";

    export function Initialize(): void {
        WebApi.InitializeWebApi();
    }

    export function InitializeLoginPage(): void {
        Authentication.ClearToken();
        Initialize();
        Authentication.BindLoginForms();
    }

    export module Authentication {

        export var User;

        const tokenKey = 'accessToken';

        export function IsAuthenticated(): boolean {
            if (GetToken())
                return true;
            else
                return false;
        }

        export function SetToken(token: string) {
            sessionStorage.setItem(tokenKey, token);
        }

        export function GetToken(): string {
            return sessionStorage.getItem(tokenKey);
        }

        export function ClearToken() {
            sessionStorage.removeItem(tokenKey);
        }

        export function BindLoginForms() {
            BindSignIn();
            BindSignUp();            
        }

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
                window.location.href = window.location.origin;
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

    }

    export module UI {

    }

    export module WebApi {

        var apiOrigin = "http://foodiegoalsapi.azurewebsites.net/api/";
        var token;
        var headers = {};

        export function InitializeWebApi() {            
            if (window.location.origin.search("local") > 0) {
                apiOrigin = "http://api.foodiegoals.local/api/";
            } else {
                apiOrigin = "http://foodiegoalsapi.azurewebsites.net/api/";
            }

            token = Authentication.GetToken();            
            if (token) {
                headers["Authorization"] = 'Bearer ' + token;
            }
        }

        export function Get(
            path: string,
            options?: Object | string,
            successMethod?: (data: Object, returnContext?: any) => any,
            failureMethod?: (statusCode: number, errorString: string, returnContext?: any) => any,
            returnContext?: any)
        {
            var params = {
                url: apiOrigin + path,
                headers: headers,
                data: options,
                method: "GET"
            };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }

        export function Post(path: string, options?: Object | string, successMethod?: (data: Object, returnContext?: any) => any, failureMethod?: (statusCode: number, errorString: string, returnContext?: any) => any, returnContext?: any) {
            var params = {
                url: apiOrigin + path,
                headers: headers,
                data: options,
                dataType: "json",
                method: "POST"
            };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }

        export function PostLogin(path: string, options?: Object | string, successMethod?: (data: Object, returnContext?: any) => any, failureMethod?: (statusCode: number, errorString: string, returnContext?: any) => any, returnContext?: any) {
            var params = { url: apiOrigin.substr(0, apiOrigin.indexOf('api/')) + path, headers: headers, data: options, dataType: "json", method: "POST" };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }

        export function Put(path: string, options?: Object | string, successMethod?: (data: Object, returnContext?: any) => any, failureMethod?: (statusCode: number, errorString: string, returnContext?: any) => any, returnContext?: any) {
            var params = {
                url: apiOrigin + path,
                headers: headers,
                data: options,
                dataType: "json",
                method: "PUT"
            }
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }

        export function Delete(path: string, options?: Object | string, successMethod?: (data: Object, returnContext?: any) => any, failureMethod?: (statusCode: number, errorString: string, returnContext?: any) => any, returnContext?: any) {
            var params = {
                url: apiOrigin + path,
                headers: headers,
                data: options,
                dataType: "json",
                method: "DELETE"
            }
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }

        function ExecuteAjax(params, successMethod?: (data: Object, returnContext?: any) => any, failureMethod?: (statusCode: number, errorString: string, returnContext?: any) => any, returnContext?) {
            $.ajax(params).then(
                function (data, textStatus, xhr) {
                    if (Helpers.IsNotNullNOREmpty(successMethod))
                        successMethod(data, returnContext);
                    else
                        GenericAPISuccess(data);
                },
                function (xhr, status, error) {
                    if (xhr.status == 401) {
                        CleanUpAndRedirectToLogin();
                        return;
                    }
                    var compiledError = `${xhr.status} Error - ${xhr.statusText}`;
                    var response = xhr.responseJSON
                    if (Helpers.IsNotNullNOREmpty(response)) {
                        if (response.Message)               { compiledError += ": " + response.Message; }
                        if (response.ExceptionMessage)      { compiledError += ": " + response.ExceptionMessage; }                        
                        if (response.error)                 { compiledError += ": " + response.error; }
                        if (response.error_description)     { compiledError += ": " + response.error_description; }
                    }
                    if (Helpers.IsNotNullNOREmpty(failureMethod)) {
                        failureMethod(xhr.status, compiledError, returnContext);
                    } else {
                        GenericAPIFail(compiledError);
                    }
                }
            );

            function HandleError(xhr) {

            }
        }

        function CleanUpAndRedirectToLogin() {
            Authentication.ClearToken();
            window.location.href = "login";
        }

        function GenericAPISuccess(data) {
            console.log(JSON.stringify(data, null, 2));
        }

        function GenericAPIFail(compiledError) {
            alert(compiledError);
        }
    }


}