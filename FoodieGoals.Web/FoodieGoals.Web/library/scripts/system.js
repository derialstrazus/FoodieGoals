var System;
(function (System) {
    "use strict";
    function Initialize() {
        WebApi.InitializeWebApi();
    }
    System.Initialize = Initialize;
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
        function Get(path, options, successMethod, failureMethod, returnContext) {
            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                method: "GET"
            };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }
        WebApi.Get = Get;
        function Post(path, options, successMethod, failureMethod, returnContext) {
            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                dataType: "json",
                method: "POST"
            };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }
        WebApi.Post = Post;
        function Put(path, options, successMethod, failureMethod, returnContext) {
            var params = {
                url: apiOrigin + path,
                //headers: headers,
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
                //headers: headers,
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
                if (Helpers.IsNotNullNOREmpty(xhr.responseJSON)) {
                    if (Helpers.IsNotNullNOREmpty(xhr.responseJSON.ExceptionMessage)) {
                        compiledError += ": " + xhr.responseJSON.ExceptionMessage;
                    }
                }
                if (Helpers.IsNotNullNOREmpty(failureMethod)) {
                    failureMethod(xhr.status, compiledError, returnContext);
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
        function GenericAPIFail(compiledError) {
            alert(compiledError);
        }
    })(WebApi = System.WebApi || (System.WebApi = {}));
})(System || (System = {}));
//# sourceMappingURL=system.js.map