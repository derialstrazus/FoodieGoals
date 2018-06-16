module Helpers {

    export function IsNullOrEmpty(data: any) {
        return (data === null || data === undefined || data === "");
    }

    export function IsNotNullNOREmpty(data: any) {
        return (data !== null && data !== undefined) && data !== '';
    }

    export function IsNumber(data: any) {
        return !IsNullOrEmpty(data) && !isNaN(parseFloat(data));
    }

    export function getQueryString(name: string, url?: string): string {

        if (IsNullOrEmpty(url) || !url) url = window.location.href;

        var queryString = url.split('?', 2)[1] || '';
        // split query string into key/value pairs
        var parameters = queryString
            .split('&')
            .map(function (param) {
                var pair = param.split('=', 2);

                return {
                    name: decodeURIComponent(pair[0]),
                    value: decodeURIComponent(pair[1] || '')
                };
            });

        // get the parameter with name if it exists
        var paramValue = parameters.filter(function (param) {
            return param.name === name;
        })[0];

        var returnString = paramValue ? paramValue.value : null;

        return returnString;

    }

    export function encodeURI(s: string) {

        if (IsNullOrEmpty(s)) return "";
        s = s.toLowerCase().trim();
        s = encodeURIComponent(s);
        return s;

    }

    export function decodeURI(s: string) {

        if (IsNullOrEmpty(s)) return "";
        s = decodeURIComponent(s);
        return s;

    }

    export function validateEmail(email: string): boolean {
        // http://stackoverflow.com/a/46181/11236
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        var reDiff = /^(([^<>()[]\.,;:s@"]+(.[^<>()[]\.,;:s@"]+)*)|(".+"))@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}])|(([a-zA-Z-0-9]+.)+[a-zA-Z]{2,}))$/igm;
        return re.test(email) || reDiff.test(email);

    }

    export var csharpMinDate = "1900-01-01T00:00:00";
    export var sqlMinDate = "0001-01-01T00:00:00";

}