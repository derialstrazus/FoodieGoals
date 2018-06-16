module System {
    "use strict";

    export function Initailize(): void {
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

        export function Get(path: string, options?: Object | string, successMethod?: Function, failureMethod?: Function, context?: any) {

            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                method: "GET"
            };
            ExecuteAjax(params, successMethod, failureMethod);
        }

        export function Post(path: string, options?: Object | string, successMethod?: Function, failureMethod?: Function, context?: any) {

            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                dataType: "json",
                method: "POST"
            };
            ExecuteAjax(params, successMethod, failureMethod);
        }

        export function Put(path: string, options?: Object | string, successMethod?: Function, failureMethod?: Function, context?: any) {

            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                dataType: "json",
                method: "PUT"
            }
            ExecuteAjax(params, successMethod, failureMethod);
        }

        export function Delete(path: string, options?: Object | string, successMethod?: Function, failureMethod?: Function, context?: any) {

            var params = {
                url: apiOrigin + path,
                //headers: headers,
                data: options,
                dataType: "json",
                method: "DELETE"
            }
            ExecuteAjax(params, successMethod, failureMethod);

        }

        function ExecuteAjax(params, successMethod, failureMethod) {
            $.ajax(params).then(
                function (data, textStatus, xhr) {
                    if (successMethod !== null && successMethod !== undefined)
                        successMethod(data);
                    else
                        GenericAPISuccess(data);
                },
                function (xhr, status, error) {
                    if (xhr.status == 401) {
                        CleanUpAndRedirectToLogin();
                    }                    
                    var compiledError = `${xhr.status}Error - ${xhr.statusText}: ${xhr.responseJSON.ExceptionMessage}`
                    if (failureMethod !== null && failureMethod !== undefined) {
                        failureMethod(compiledError);
                    } else {
                        GenericAPIFail(compiledError);
                    }
                }
            );
        }

        function CleanUpAndRedirectToLogin() {
            alert("401 error.");
        }

        export function GenericAPISuccess(data) {
            console.log(JSON.stringify(data, null, 2));
        }

        export function GenericAPIFail(compiledError) {
            alert(compiledError);
        }
    }
}