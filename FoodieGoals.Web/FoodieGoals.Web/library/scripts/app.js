var App;
(function (App) {
    "use strict";
    var Restaurant = /** @class */ (function () {
        function Restaurant() {
        }
        return Restaurant;
    }());
    var restaurantArr = [];
    var personID = 0;
    function Initialize() {
        console.log("App initializing...");
        InitializePerson();
        InitializeSearch();
        //$("#selectList").change(function (e) {
        //    var selected = $("#selectList").val();
        //    console.log(selected);
        //});
    }
    App.Initialize = Initialize;
    function InitializePerson() {
        System.WebApi.Get("person/users", null, GetTemporaryUsersSuccess); //temporary soln to avoid working on authentication
        var cookiePersonID = parseInt(Helpers.Cookie.Get("PersonID"));
        if (Helpers.IsNumber(cookiePersonID)) {
            personID = cookiePersonID;
            System.WebApi.Get("person/" + personID, null, GetPersonSuccess);
            System.WebApi.Get("person/" + personID + "/personrestaurant/goals", null, GetPersonRestaurantsSuccess);
        }
    }
    function GetTemporaryUsersSuccess(data) {
        if (Helpers.IsNullOrEmpty(data)) {
            alert("empty data when getting temporary users");
        }
        var tempUserSelector = $("#selectUser");
        for (var i = 0; i < data.length; i++) {
            var user = data[i];
            var userOption = $("<option value=" + user.ID + ">" + user.FirstName + " " + user.LastName + "</option>").appendTo(tempUserSelector);
        }
        if (personID > 0)
            $("#selectUser option[value=" + personID + "]").prop('selected', true);
        tempUserSelector.change(function (e) {
            var selectedPersonID = $("#selectUser option:selected").val();
            System.WebApi.Get("person/" + selectedPersonID, null, GetPersonSuccess);
            System.WebApi.Get("person/" + selectedPersonID + "/personrestaurant/goals", null, GetPersonRestaurantsSuccess);
        });
    }
    function GetPersonSuccess(personData) {
        if (Helpers.IsNullOrEmpty(personData)) {
            alert("empty data when getting temporary users");
        }
        personID = personData.ID;
        Helpers.Cookie.Set("PersonID", personID.toString());
        $("#nameContainer").empty().append("<p>My name is " + personData.FirstName + " " + personData.LastName + "</p>");
        var personListContainer = $("#selectPersonList").empty();
        var personListOptionGoals = $("<option value=\"goals\">Goals</option>").appendTo(personListContainer);
        var personListOptionVisited = $("<option value=\"visited\">Visited</option>").appendTo(personListContainer);
        if (Helpers.IsNotNullNOREmpty(personData.PersonLists))
            RenderPersonList(personData.PersonLists);
        personListContainer.change(function (e) {
            var listID = $("#selectPersonList option:selected").val();
            if (listID === "goals")
                System.WebApi.Get("person/" + personID + "/personrestaurant/goals", null, GetPersonRestaurantsSuccess);
            else if (listID === "visited")
                System.WebApi.Get("person/" + personID + "/personrestaurant/visited", null, GetPersonRestaurantsSuccess);
            else if (Helpers.IsNumber(listID))
                System.WebApi.Get("personlist/" + listID, null, GetPersonRestaurantsSuccess);
        });
    }
    function RenderPersonList(personLists) {
        if (Helpers.IsNullOrEmpty(personLists) || personLists.length <= 0) {
            return;
        }
        var personListContainer = $("#selectPersonList");
        for (var i = 0; i < personLists.length; i++) {
            var list = personLists[i];
            personListContainer.append("<option value=" + list.ID + ">" + list.Title + "</option>");
        }
    }
    function GetPersonRestaurantsSuccess(personRestaurantArr) {
        var container = $("#restaurantList").empty();
        if (Helpers.IsNullOrEmpty(personRestaurantArr) || personRestaurantArr.length <= 0) {
            container.append("<p>You haven't added any restaurants to this list yet.</p>");
            return;
        }
        for (var i = 0; i < personRestaurantArr.length; i++) {
            var personRestaurant = personRestaurantArr[i];
            RenderRestaurant(personRestaurant, container);
        }
    }
    function RenderRestaurant(personRestaurant, container) {
        if (Helpers.IsNullOrEmpty(container))
            var container = $("#restaurantList");
        var divRestaurant = $("<div class=\"restaurantcontainer\"></div>").appendTo(container);
        divRestaurant.append("<p>" + personRestaurant.Restaurant.Name + "</p>");
    }
    function InitializeSearch() {
        $("#btnMainSearch").click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            var searchTerm = $("inputMainSearch").val();
            System.WebApi.Get("restaurant/search?=" + Helpers.encodeURI(searchTerm), null, SearchSuccess);
        });
    }
    function SearchSuccess(searchResults) {
        var resultsContainer = $("#viewSearchResults").show();
        if (Helpers.IsNullOrEmpty(searchResults) || searchResults.length <= 0) {
            resultsContainer.append("<p>No results found.</p>");
        }
        for (var i = 0; i < searchResults.length; i++) {
            var restaurant = searchResults[i];
            resultsContainer.append("<p>" + restaurant.Name + "</p>");
        }
    }
})(App || (App = {}));
//# sourceMappingURL=app.js.map