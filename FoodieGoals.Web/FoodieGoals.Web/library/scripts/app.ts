module App {
    "use strict";

    class Restaurant {
        Name: string;
        ID: number;
        ListID: number;
    }

    var restaurantArr = [];


    export function Initialize(): void {

        console.log("App initializing...");

        var personID = 1;

        System.WebApi.Get(`Person/${personID}`, null, GetPersonSuccess);

        $("#selectList").change(function (e) {
            var selected = $("#selectList").val();
            console.log(selected);
        });

    }

    function GetPersonSuccess(data) {
        $("#nameContainer").empty().append(`<p>My name is ${data.FirstName} ${data.LastName}</p>`);
    }

    function renderSelectedList() {

    }

}