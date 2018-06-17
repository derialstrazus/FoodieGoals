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
        
        InitializePerson();

        //$("#selectList").change(function (e) {
        //    var selected = $("#selectList").val();
        //    console.log(selected);
        //});
    }

    function InitializePerson() {
        System.WebApi.Get(`person/users`, null, GetTemporaryUsersSuccess);      //temporary soln to avoid working on authentication

        var cookiePersonID = parseInt(Helpers.Cookie.Get("PersonID"));
        if (Helpers.IsNumber(cookiePersonID)) {
            personID = cookiePersonID;
            System.WebApi.Get(`person/${personID}`, null, GetPersonSuccess);
        }
    }

    function GetTemporaryUsersSuccess(data) {
        if (Helpers.IsNullOrEmpty(data)) {
            alert("empty data when getting temporary users");
        }

        var tempUserSelector = $("#selectUser");
        for (var i = 0; i < data.length; i++) {
            var user = data[i];
            var userOption = $(`<option value=${user.ID}>${user.FirstName} ${user.LastName}</option>`).appendTo(tempUserSelector);
        }

        if (personID > 0)
            $(`#selectUser option[value=${personID}]`).prop('selected', true);

        tempUserSelector.change(function (e) {
            var selectedPersonID = $("#selectUser option:selected").val();
            System.WebApi.Get(`person/${selectedPersonID}`, null, GetPersonSuccess);
        });
    }

    function GetPersonSuccess(personData) {
        if (Helpers.IsNullOrEmpty(personData)) {
            alert("empty data when getting temporary users");
        }

        personID = personData.ID;
        Helpers.Cookie.Set("PersonID", personID.toString());

        $("#nameContainer").empty().append(`<p>My name is ${personData.FirstName} ${personData.LastName}</p>`);

    }

    function renderSelectedList() {

    }

}