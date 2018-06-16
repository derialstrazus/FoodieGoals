var App;
(function (App) {
    "use strict";
    var Restaurant = /** @class */ (function () {
        function Restaurant() {
        }
        return Restaurant;
    }());
    var restaurantArr = [];
    function Initialize() {
        console.log("App initializing...");
        var personID = 1;
        System.WebApi.Get("Person/" + personID, null, GetPersonSuccess);
        $("#selectList").change(function (e) {
            var selected = $("#selectList").val();
            console.log(selected);
        });
    }
    App.Initialize = Initialize;
    function GetPersonSuccess(data) {
        $("#nameContainer").empty().append("<p>My name is " + data.FirstName + " " + data.LastName + "</p>");
    }
    function renderSelectedList() {
    }
})(App || (App = {}));
//# sourceMappingURL=app.js.map