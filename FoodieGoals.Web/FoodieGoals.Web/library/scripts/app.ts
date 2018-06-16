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

        

        $("#selectList").change(function (e) {
            var selected = $("#selectList").val();
            console.log(selected);
        });

    }

    function renderSelectedList() {

    }

}