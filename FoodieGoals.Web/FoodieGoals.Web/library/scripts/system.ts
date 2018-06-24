module System {
    "use strict";

    export function Initialize(): void {
        WebApi.InitializeWebApi();
    }

    export module Authentication {

    }

    export module UI {

    }

    export module WebApi {

        var apiOrigin = "http://foodiegoalsapi.azurewebsites.net/api/";
        var headers = { "AuthToken": 1 };

        export function InitializeWebApi() {
            if (window.location.origin.search("local") > 0) {
                apiOrigin = "http://api.foodiegoals.local/api/";
            } else {
                apiOrigin = "http://foodiegoalsapi.azurewebsites.net/api/";
            }

            //initialize header here
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
                //headers: headers,
                data: options,
                method: "GET"
            };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }

        export function Post(path: string, options?: Object | string, successMethod?: (data: Object, returnContext?: any) => any, failureMethod?: (statusCode: number, errorString: string, returnContext?: any) => any, returnContext?: any) {

            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                dataType: "json",
                method: "POST"
            };
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }

        export function Put(path: string, options?: Object | string, successMethod?: (data: Object, returnContext?: any) => any, failureMethod?: (statusCode: number, errorString: string, returnContext?: any) => any, returnContext?: any) {

            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                dataType: "json",
                method: "PUT"
            }
            ExecuteAjax(params, successMethod, failureMethod, returnContext);
        }

        export function Delete(path: string, options?: Object | string, successMethod?: (data: Object, returnContext?: any) => any, failureMethod?: (statusCode: number, errorString: string, returnContext?: any) => any, returnContext?: any) {

            var params = {
                url: apiOrigin + path,
                //headers: headers,
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
                    }
                    var compiledError = `${xhr.status} Error - ${xhr.statusText}`;
                    if (Helpers.IsNotNullNOREmpty(xhr.responseJSON)) {
                        if (Helpers.IsNotNullNOREmpty(xhr.responseJSON.ExceptionMessage)) {
                            compiledError += ": " + xhr.responseJSON.ExceptionMessage;
                        }
                        if (Helpers.IsNotNullNOREmpty(xhr.responseJSON.Message)) {
                            compiledError += ": " + xhr.responseJSON.Message;
                        }
                    }
                    if (Helpers.IsNotNullNOREmpty(failureMethod)) {
                        failureMethod(xhr.status, compiledError, returnContext);
                    } else {
                        GenericAPIFail(compiledError);
                    }
                }
            );
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
    }


}