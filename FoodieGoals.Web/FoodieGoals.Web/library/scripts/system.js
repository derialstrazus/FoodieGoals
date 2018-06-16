var System;
(function (System) {
    "use strict";
    function Initailize() {
        WebApi.InitializeWebApi();
    }
    System.Initailize = Initailize;
    var WebApi;
    (function (WebApi) {
        var apiOrigin = "http://foodiegoalsapi.azurewebsites.net/api/";
        var headers = { "AuthToken": 1 };
        function InitializeWebApi() {
            if (window.location.origin.search("local") > 0) {
                apiOrigin = "http://api.foodiegoals.local/api/";
            }
            else {
                apiOrigin = "http://foodiegoalsapi.azurewebsites.net/api/";
            }
            //initialize header here
        }
        WebApi.InitializeWebApi = InitializeWebApi;
        function Get(path, options, successMethod, failureMethod, context) {
            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                method: "GET"
            };
            ExecuteAjax(params, successMethod, failureMethod);
        }
        WebApi.Get = Get;
        function Post(path, options, successMethod, failureMethod, context) {
            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                dataType: "json",
                method: "POST"
            };
            ExecuteAjax(params, successMethod, failureMethod);
        }
        WebApi.Post = Post;
        function Put(path, options, successMethod, failureMethod, context) {
            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                dataType: "json",
                method: "PUT"
            };
            ExecuteAjax(params, successMethod, failureMethod);
        }
        WebApi.Put = Put;
        function Delete(path, options, successMethod, failureMethod, context) {
            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                dataType: "json",
                method: "DELETE"
            };
            ExecuteAjax(params, successMethod, failureMethod);
        }
        WebApi.Delete = Delete;
        function ExecuteAjax(params, successMethod, failureMethod) {
            $.ajax(params).then(function (data, textStatus, xhr) {
                if (successMethod !== null && successMethod !== undefined)
                    successMethod(data);
                else
                    GenericAPISuccess(data);
            }, function (xhr, status, error) {
                if (xhr.status == 401) {
                    CleanUpAndRedirectToLogin();
                }
                var compiledError = xhr.status + "Error - " + xhr.statusText + ": " + xhr.responseJSON.ExceptionMessage;
                if (failureMethod !== null && failureMethod !== undefined) {
                    failureMethod(compiledError);
                }
                else {
                    GenericAPIFail(compiledError);
                }
            });
        }
        function CleanUpAndRedirectToLogin() {
            alert("401 error.");
        }
        function GenericAPISuccess(data) {
            console.log(JSON.stringify(data, null, 2));
        }
        WebApi.GenericAPISuccess = GenericAPISuccess;
        function GenericAPIFail(compiledError) {
            alert(compiledError);
        }
        WebApi.GenericAPIFail = GenericAPIFail;
    })(WebApi = System.WebApi || (System.WebApi = {}));
})(System || (System = {}));
//# sourceMappingURL=system.js.map