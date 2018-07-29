module App {
    "use strict";

    class Restaurant {
        Name: string;
        ID: number;
        ListID: number;
    }

    var restaurantArr = [];
    var personID: number = 0;


    export function Initialize(): void {

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

    function ClearEverything() {
        $("#viewSearchResults").empty().hide();
        $("#nameContainer").empty();
        $("#selectPersonList").empty();
        ClearAddRestaurantForm();
    }

    function InitializePerson() {
        //System.WebApi.Get(`person/users`, null, GetTemporaryUsersSuccess);      //temporary soln to avoid working on authentication



        var cookiePersonID = parseInt(Helpers.Cookie.Get("PersonID"));
        if (Helpers.IsNumber(cookiePersonID)) {
            //personID = cookiePersonID;
            personID = 1;
            System.WebApi.Get(`person/${personID}`, null, GetPersonSuccess);
            System.WebApi.Get(`person/${personID}/personrestaurant/goals`, null, GetPersonRestaurantsSuccess);
        }
    }

    //function GetTemporaryUsersSuccess(data) {
    //    if (Helpers.IsNullOrEmpty(data)) {
    //        alert("empty data when getting temporary users");
    //    }

    //    var tempUserSelector = $("#selectUser");
    //    for (var i = 0; i < data.length; i++) {
    //        var user = data[i];
    //        var userOption = $(`<option value=${user.ID}>${user.FirstName} ${user.LastName}</option>`).appendTo(tempUserSelector);
    //    }

    //    if (personID > 0)
    //        $(`#selectUser option[value=${personID}]`).prop('selected', true);

    //    tempUserSelector.change(function (e) {
    //        var selectedPersonID = $("#selectUser option:selected").val();
    //        System.WebApi.Get(`person/${selectedPersonID}`, null, SwitchPersonSuccess);
    //        System.WebApi.Get(`person/${selectedPersonID}/personrestaurant/goals`, null, GetPersonRestaurantsSuccess);
    //    });
    //}

    function GetPersonSuccess(personData) {
        if (Helpers.IsNullOrEmpty(personData)) {
            alert("empty data when getting temporary users");
        }

        ClearEverything();

        personID = personData.ID;
        Helpers.Cookie.Set("PersonID", personID.toString());

        $("#nameContainer").empty().append(`<p>My name is ${personData.FirstName} ${personData.LastName}</p>`);

        var personListContainer = $("#selectPersonList").empty();
        var personListOptionGoals = $(`<option value="goals">Goals</option>`).appendTo(personListContainer);
        var personListOptionVisited = $(`<option value="visited">Visited</option>`).appendTo(personListContainer);
        if (Helpers.IsNotNullNOREmpty(personData.PersonLists))
            RenderPersonList(personData.PersonLists);

        personListContainer.change(function (e) {
            var listID = $("#selectPersonList option:selected").val();
            if (listID === "goals")
                System.WebApi.Get(`person/${personID}/personrestaurant/goals`, null, GetPersonRestaurantsSuccess, null, false);
            else if (listID === "visited")
                System.WebApi.Get(`person/${personID}/personrestaurant/visited`, null, GetPersonRestaurantsSuccess, null, false);
            else if (Helpers.IsNumber(listID))
                System.WebApi.Get("personlist/" + listID, null, GetPersonRestaurantsSuccess, null, true);
        });
    }

    function RenderPersonList(personLists) {
        if (Helpers.IsNullOrEmpty(personLists) || personLists.length <= 0) {
            return;
        }

        var personListContainer = $("#selectPersonList")
        for (var i = 0; i < personLists.length; i++) {
            var list = personLists[i];
            personListContainer.append(`<option value=${list.ID}>${list.Title}</option>`);
        }
    }



    function GetPersonRestaurantsSuccess(personRestaurantArr, isCustomList: Boolean = false) {
        var container = $("#restaurantList").empty();

        if (Helpers.IsNullOrEmpty(personRestaurantArr) || personRestaurantArr.length <= 0) {
            container.append("<p>You haven't added any restaurants to this list yet.</p>");
            return;
        }

        for (var i = 0; i < personRestaurantArr.ListRestaurants.length; i++) {
            var personRestaurant = personRestaurantArr.ListRestaurants[i];
            RenderRestaurant(personRestaurant, container, isCustomList);
        }
    }

    function RenderRestaurant(personRestaurant, container: JQuery, isCustomList: Boolean = false) {
        //isCustomList defaults false, which means by default it is either goals or visited.
        //this sounds dangerous.  I'm not sure if I'm ok with that.  This might let the user make some dangerous calls.
        if (Helpers.IsNullOrEmpty(container))
            var container = $("#restaurantList");

        var divRestaurant = $(`<div class="restaurantcontainer"></div>`).appendTo(container);
        var divRestaurantPic = $(`<div class="restaurantpic"></div>`).appendTo(divRestaurant);
        var divRestaurantDetails = $(`<div class="restaurantdetails"></div>`).appendTo(divRestaurant);

        var picFileName = "temp" + personRestaurant.RestaurantID % 10 + ".jpg";
        divRestaurantPic.append(`<img src="/library/images/${picFileName}"></img>`);

        var divRestaurantHeader = $(`<div class="restaurantheader"></div>`).appendTo(divRestaurantDetails);
        var headerText = personRestaurant.Name;
        if (Helpers.IsNotNullNOREmpty(personRestaurant.Address)) {
            headerText += ` @ ${personRestaurant.Address.AddressString}`;
        }
        divRestaurantHeader.append(`<h3>${headerText}</h3>`);

        var divRestaurantContents = $(`<div class="restaurantcontents"></div>`).appendTo(divRestaurantDetails);
        var contentText = "";

        if (isCustomList)
            contentText = personRestaurant.ListComments;
        else
            contentText = personRestaurant.Notes;

        if (!contentText)           //if still empty, use global summary
            contentText = personRestaurant.Summary;

        divRestaurantContents.append(`<p>${contentText}</p>`);

        var divRestaurantButtons = $(`<div class="restaurantbuttons"></div>`).appendTo(divRestaurant);
        var editCommentsButton = $(`<button><span class="icon-file-text2" title="Edit Notes"></span></button>`).appendTo(divRestaurantButtons);
        //var editCommentsButton = $(`<span class="icon-file-text2" title="Edit Notes"></span>`).appendTo(divRestaurantButtons);
        editCommentsButton.click(function (e) {
            e.stopPropagation();
            e.preventDefault();
            alert("Still working on Edit Comments");
            //TODO: I need a UI.Modal
            var sendThis = personRestaurant;

            //if (isCustomList) {
            //    if (!personRestaurant.IsListRestaurant) {
            //        alert("DEV: Used wrong API for this object.  This object is a personrestaurant, so you need to use the personrestaurant API.");
            //        return;
            //    }
            //    sendThis["Comment"] = ???
            //    System.WebApi.Put("listrestaurants/" + personRestaurant.ID, sendThis, DeleteListRestaurantSuccess);
            //}
            //else {
            //    if (personRestaurant.IsListRestaurant) {
            //        alert("DEV: Used wrong API for this object.  This object is a listrestaurant, so you need to use the listrestaurant API.");
            //        return;
            //    }
            //    sendThis["Notes"] = ???
            //    System.WebApi.Put("personrestaurants/" + personRestaurant.ID, sendThis, DeletePersonRestaurantSuccess);
            //}
            //TODO: This needs to be different depending on whether its a list or if its on goals
        });

        if (!personRestaurant.HasVisited) {
            var visitedButton = $(`<button><span class="icon-checkmark" title="Visited"></span></button>`).appendTo(divRestaurantButtons);
            visitedButton.click(function (e) {
                e.stopPropagation();
                e.preventDefault();

                var sendThis = personRestaurant;
                if (sendThis.IsListRestaurant) {
                    alert("DEV: Passed wrong object.  You need to pass personrestaurant not listrestaurant.");
                    return;
                }
                sendThis["HasVisited"] = true;
                sendThis["LastVisited"] = new Date().toISOString();
                System.WebApi.Put("personrestaurants/" + personRestaurant.ID, sendThis, MarkRestaurantAsVisitedSuccess, null, personRestaurant);
            });
        }

        //TODO: I need an intermediary between this action, to warn users that they are deleting something
        var deleteButton = $(`<button><span class="icon-cross" title="Delete"></span></button>`).appendTo(divRestaurantButtons);
        deleteButton.click(function (e) {
            e.stopPropagation();
            e.preventDefault();
            //alert("Still working on Delete");
            if (isCustomList) {
                if (!personRestaurant.IsListRestaurant) {
                    alert("DEV: Used wrong API for this object.  This object is a personrestaurant, so you need to use the personrestaurant API.");
                    return;
                }
                System.WebApi.Delete("listrestaurants/" + personRestaurant.ID, null, DeleteListRestaurantSuccess);
            }
            else {
                if (personRestaurant.IsListRestaurant) {
                    alert("DEV: Used wrong API for this object.  This object is a listrestaurant, so you need to use the listrestaurant API.");
                    return;
                }
                //TODO:  Add warning that this will delete from all your existing lists.  Honestly this should be on a different button
                System.WebApi.Delete("personrestaurants/" + personRestaurant.ID, null, DeletePersonRestaurantSuccess);
            }
        });

    }

    function MarkRestaurantAsVisitedSuccess(data, context) {
        alert(`Marked ${context.Name} as visited.`);
        var listID = $("#selectPersonList option:selected").val();
        if (listID === "goals")
            System.WebApi.Get(`person/${personID}/personrestaurant/goals`, null, GetPersonRestaurantsSuccess);
    }

    function DeleteListRestaurantSuccess(restaurant) {
        alert("Successfully removed " + restaurant.Name + " from this list.");
    }

    function DeletePersonRestaurantSuccess(restaurant) {
        alert("Successfully removed " + restaurant.Name + " from your collections.");
        var listID = $("#selectPersonList option:selected").val();
        if (listID === "goals")
            System.WebApi.Get(`person/${personID}/personrestaurant/goals`, null, GetPersonRestaurantsSuccess);    
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
        var restaurantContainer = $(`<div class="restaurant searchresult"></div>`).appendTo(container);
        var restaurantParagraph = $(`<p></p>`).appendTo(restaurantContainer);
        var addToListButton = $(`<span class="btnAddToList">[+]</span>`).appendTo(restaurantParagraph);
        restaurantParagraph.append(`${restaurant.Name}`);

        addToListButton.click(function (e) {
            e.stopPropagation;
            e.preventDefault();
            System.WebApi.Post(`person/${personID}/personrestaurant/${restaurant.ID}`, null, AddRestaurantToGoalsSuccess, null, restaurant);
        });
    }

    function AddRestaurantToGoalsSuccess(data, context) {
        alert(`Added ${context.Name} to your goals`)
        System.WebApi.Get(`person/${personID}/personrestaurant/goals`, null, GetPersonRestaurantsSuccess);
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
            }
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

    
}