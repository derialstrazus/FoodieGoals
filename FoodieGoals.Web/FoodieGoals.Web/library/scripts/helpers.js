var Helpers;
(function (Helpers) {
    function IsNullOrEmpty(data) {
        return (data === null || data === undefined || data === "");
    }
    Helpers.IsNullOrEmpty = IsNullOrEmpty;
    function IsNotNullNOREmpty(data) {
        return (data !== null && data !== undefined) && data !== '';
    }
    Helpers.IsNotNullNOREmpty = IsNotNullNOREmpty;
    function IsNumber(data) {
        return !IsNullOrEmpty(data) && !isNaN(parseFloat(data));
    }
    Helpers.IsNumber = IsNumber;
    function getQueryString(name, url) {
        if (IsNullOrEmpty(url) || !url)
            url = window.location.href;
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
    Helpers.getQueryString = getQueryString;
    function encodeURI(s) {
        if (IsNullOrEmpty(s))
            return "";
        s = s.toLowerCase().trim();
        s = encodeURIComponent(s);
        return s;
    }
    Helpers.encodeURI = encodeURI;
    function decodeURI(s) {
        if (IsNullOrEmpty(s))
            return "";
        s = decodeURIComponent(s);
        return s;
    }
    Helpers.decodeURI = decodeURI;
    function validateEmail(email) {
        // http://stackoverflow.com/a/46181/11236
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        var reDiff = /^(([^<>()[]\.,;:s@"]+(.[^<>()[]\.,;:s@"]+)*)|(".+"))@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}])|(([a-zA-Z-0-9]+.)+[a-zA-Z]{2,}))$/igm;
        return re.test(email) || reDiff.test(email);
    }
    Helpers.validateEmail = validateEmail;
    Helpers.csharpMinDate = "1900-01-01T00:00:00";
    Helpers.sqlMinDate = "0001-01-01T00:00:00";
})(Helpers || (Helpers = {}));
//# sourceMappingURL=helpers.js.map