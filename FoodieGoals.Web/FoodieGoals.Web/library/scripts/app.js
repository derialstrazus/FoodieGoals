var App;
(function (App) {
    "use strict";
    var Restaurant = /** @class */ (function () {
        function Restaurant() {
        }
        return Restaurant;
    }());
    function Initialize() {
        console.log("App initializing...");
        $("#selectList").change(function (e) {
            var selected = $("#selectList").val();
            console.log(selected);
        });
    }
    App.Initialize = Initialize;
    function renderSelectedList() {
    }
})(App || (App = {}));
//# sourceMappingURL=app.js.map