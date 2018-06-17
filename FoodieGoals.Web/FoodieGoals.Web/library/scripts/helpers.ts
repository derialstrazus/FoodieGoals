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

    export module Cookie {

        export function Set(key: string, value: string, expireIn?: number): void {

            expireIn = expireIn || 7;
            var d = new Date();
            d.setTime(d.getTime() + (expireIn * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = key + "=" + value + "; " + expires;

        }

        export function Get(key: string): string {

            var name = key + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";

        }

        export function Remove(key: string): boolean {

            Set(key, '');
            return Helpers.IsNullOrEmpty(Get(key));

        }

        export function RemoveAll() {

            // This function will attempt to remove a cookie from all paths.
            var pathBits = location.pathname.split('/');
            var pathCurrent = ' path=';

            // do a simple pathless delete first.
            document.cookie = name + '=; expires=Thu, 01-Jan-1970 00:00:01 GMT;';
            var cookies = document.cookie.split(";");

            for (var i = 0; i < pathBits.length; i++) {
                pathCurrent += ((pathCurrent.substr(-1) != '/') ? '/' : '') + pathBits[i];
                for (var x = 0; x < cookies.length; x++) {
                    var cookie = cookies[x];
                    var eqPos = cookie.indexOf("=");
                    var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                    document.cookie = name + '=; expires=Thu, 01-Jan-1970 00:00:01 GMT;' + pathCurrent + ';';
                }
            }

            for (var i = 0; i < cookies.length; i++) {
                var cookie = cookies[i];
                var eqPos = cookie.indexOf("=");
                var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                Set(name, "", -1);
            }

        }

    }
}