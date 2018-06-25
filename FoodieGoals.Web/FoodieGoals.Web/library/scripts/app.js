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
        ClearEverything();
        InitializePerson();
        InitializeSearch();
        InitializeAddRestaurant();
        //$("#selectList").change(function (e) {
        //    var selected = $("#selectList").val();
        //    console.log(selected);
        //});
    }
    App.Initialize = Initialize;
    function ClearEverything() {
        $("#viewSearchResults").empty().hide();
        $("#nameContainer").empty();
        $("#selectPersonList").empty();
        ClearAddRestaurantForm();
    }
    function InitializePerson() {
        System.WebApi.Get("person/users", null, GetTemporaryUsersSuccess); //temporary soln to avoid working on authentication
        var cookiePersonID = parseInt(Helpers.Cookie.Get("PersonID"));
        if (Helpers.IsNumber(cookiePersonID)) {
            personID = cookiePersonID;
            System.WebApi.Get("person/" + personID, null, SwitchPersonSuccess);
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
            System.WebApi.Get("person/" + selectedPersonID, null, SwitchPersonSuccess);
            System.WebApi.Get("person/" + selectedPersonID + "/personrestaurant/goals", null, GetPersonRestaurantsSuccess);
        });
    }
    function SwitchPersonSuccess(personData) {
        if (Helpers.IsNullOrEmpty(personData)) {
            alert("empty data when getting temporary users");
        }
        ClearEverything();
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
        for (var i = 0; i < personRestaurantArr.ListRestaurants.length; i++) {
            var personRestaurant = personRestaurantArr.ListRestaurants[i];
            RenderRestaurant(personRestaurant, container);
        }
    }
    function RenderRestaurant(personRestaurant, container) {
        if (Helpers.IsNullOrEmpty(container))
            var container = $("#restaurantList");
        var divRestaurant = $("<div class=\"restaurantcontainer\"></div>").appendTo(container);
        var divRestaurantPic = $("<div class=\"restaurantpic\"></div>").appendTo(divRestaurant);
        var divRestaurantDetails = $("<div class=\"restaurantdetails\"></div>").appendTo(divRestaurant);
        var picFileName = "temp" + personRestaurant.RestaurantID % 10 + ".jpg";
        divRestaurantPic.append("<img src=\"/library/images/" + picFileName + "\"></img>");
        var displayText = personRestaurant.Name;
        if (Helpers.IsNotNullNOREmpty(personRestaurant.Address)) {
            displayText += " @ " + personRestaurant.Address.AddressString;
        }
        divRestaurantDetails.append("<h3>" + displayText + "</h3>");
        if (Helpers.IsNotNullNOREmpty(personRestaurant.ListComments))
            divRestaurantDetails.append("<p>" + personRestaurant.ListComments + "</p>");
        else if (Helpers.IsNotNullNOREmpty(personRestaurant.Notes))
            divRestaurantDetails.append("<p>" + personRestaurant.Notes + "</p>");
        else if (Helpers.IsNotNullNOREmpty(personRestaurant.Summary))
            divRestaurantDetails.append("<p>" + personRestaurant.Summary + "</p>");
        var divRestaurantButtons = $("<div class=\"restaurantbuttons\"></div>").appendTo(divRestaurantDetails);
        var editCommentsButton = $("<span class=\"icon-file-text2\" title=\"Edit Notes\"></span>").appendTo(divRestaurantButtons);
        editCommentsButton.click(function (e) {
            e.stopPropagation();
            e.preventDefault();
            alert("Still working on Edit Comments");
        });
        if (!personRestaurant.HasVisited) {
            var visitedButton = $("<span class=\"icon-checkmark\" title=\"Visited\"></span>").appendTo(divRestaurantButtons);
            visitedButton.click(function (e) {
                e.stopPropagation();
                e.preventDefault();
                var sendThis = personRestaurant;
                sendThis["Sequence"] = personRestaurant.PersonRestaurantSequence;
                sendThis["HasVisited"] = true;
                sendThis["LastVisited"] = new Date().toISOString();
                System.WebApi.Put("personrestaurants/" + personRestaurant.ID, sendThis, MarkRestaurantAsVisitedSuccess, null, personRestaurant);
            });
        }
        var deleteButton = $("<span class=\"icon-cross\" title=\"Delete\"></span>").appendTo(divRestaurantButtons);
        deleteButton.click(function (e) {
            e.stopPropagation();
            e.preventDefault();
            alert("Still working on Delete");
            //System.WebApi.Delete("");
        });
    }
    function MarkRestaurantAsVisitedSuccess(data, context) {
        alert("Marked " + context.Name + " as visited.");
        var listID = $("#selectPersonList option:selected").val();
        if (listID === "goals")
            System.WebApi.Get("person/" + personID + "/personrestaurant/goals", null, GetPersonRestaurantsSuccess);
    }
    function InitializeSearch() {
        $("#btnMainSearch").click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            var searchTerm = $("#inputMainSearch").val();
            System.WebApi.Get("restaurant/search?searchterm=" + Helpers.encodeURI(searchTerm), null, SearchSuccess);
        });
    }
    function SearchSuccess(searchResults) {
        var resultsContainer = $("#viewSearchResults").empty().show();
        if (Helpers.IsNullOrEmpty(searchResults) || searchResults.length <= 0) {
            resultsContainer.append("<p>No results found.</p>");
        }
        for (var i = 0; i < searchResults.length; i++) {
            var restaurant = searchResults[i];
            RenderRestaurantToSearch(restaurant, resultsContainer);
            //resultsContainer.append(`<p>${restaurant.Name}</p>`);
        }
    }
    function RenderRestaurantToSearch(restaurant, container) {
        var restaurantContainer = $("<div class=\"restaurant searchresult\"></div>").appendTo(container);
        var restaurantParagraph = $("<p></p>").appendTo(restaurantContainer);
        var addToListButton = $("<span class=\"btnAddToList\">[+]</span>").appendTo(restaurantParagraph);
        restaurantParagraph.append("" + restaurant.Name);
        addToListButton.click(function (e) {
            e.stopPropagation;
            e.preventDefault();
            System.WebApi.Post("person/" + personID + "/personrestaurant/" + restaurant.ID, null, AddRestaurantToGoalsSuccess, null, restaurant);
        });
    }
    function AddRestaurantToGoalsSuccess(data, context) {
        alert("Added " + context.Name + " to your goals");
        System.WebApi.Get("person/" + personID + "/personrestaurant/goals", null, GetPersonRestaurantsSuccess);
    }
    function InitializeAddRestaurant() {
        ClearAddRestaurantForm();
        $("#btnDisplayRestaurantForm").click(function (e) {
            $("#formAddRestaurant").toggle();
            $("#divAddRestaurantSuccess").hide('fast');
        });
        $("#btnSaveRestaurant").click(function (e) {
            e.preventDefault();
            var name = $("#inputAddRestaurantName").val();
            var desc = $("#inputAddRestaurantDesc").val();
            //var address = $("#inputAddRestaurantAddress").val();        //parse this using google API on back
            var sendThis = {
                Name: name,
                Summary: desc,
                Address: {
                    StreetLine1: $("#inputAddRestaurantAddressStreet").val(),
                    City: $("#inputAddRestaurantAddressCity").val(),
                    State: $("#inputAddRestaurantAddressState").val(),
                    Country: "USA",
                    PostalCode: $("#inputAddRestaurantAddressZip").val()
                }
            };
            System.WebApi.Post("restaurant", sendThis, AddRestaurantSuccess);
        });
    }
    function ClearAddRestaurantForm() {
        $("#formAddRestaurant").hide();
        $("#formAddRestaurant input").val("");
    }
    function AddRestaurantSuccess(restaurant) {
        alert("Success!");
        ClearAddRestaurantForm();
        $("#divAddRestaurantSuccess").show('slow');
    }
})(App || (App = {}));
//# sourceMappingURL=app.js.map