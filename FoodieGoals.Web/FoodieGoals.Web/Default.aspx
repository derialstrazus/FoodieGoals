﻿<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <title>Foodie Goals</title>

    <link href="library/styles/bootstrap.min.css" rel="stylesheet" />
    <link href="library/styles/pagebootstrap.min.css" rel="stylesheet" />
    <link href="library/styles/variables.min.css" rel="stylesheet" />
    <link href="library/styles/icons.min.css" rel="stylesheet" />

    <!--<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Lato:300,400,400i,700" />-->
    <link href="https://fonts.googleapis.com/css?family=Noto+Sans|Roboto" rel="stylesheet">

</head>

<body>

    <!--<div id="divUserSelector">
        <select id="selectUser"></select>
    </div>-->

    <nav class="navbar navbar-light">
        <a class="navbar-brand" href="#">FoodieGoals</a>

        <div id="loginPanel">
            <button class="btn">Login</button>
            <button class="btn">Logout</button>
        </div>

    </nav>

    <!--<div id="loginPanel">

    </div>-->

    <div id="mainContainer">
        
        <div id="nameContainer"></div>

        <div class="panel">
            <div id="divSearch">
                <input id="inputMainSearch" type="text" placeholder="Find Restaurant..." />
                <button id="btnMainSearch">Go</button>
            </div>

            <div id="viewSearchResults" style="display: none;"></div>
        </div>

        <div class="panel">
            <div id="divAddRestaurant">
                <button id="btnDisplayRestaurantForm">Add a restaurant</button>
            </div>

            <div id="divAddRestaurantSuccess" style="display: none;">
                <p>Thanks for letting us know about this cool place!</p>
                <p>We've also gone ahead and added it to your goals.</p>
            </div>

            <form id="formAddRestaurant" style="display: none;">
                <div>
                    <label>Name: </label>
                    <input id="inputAddRestaurantName" type="text" />
                </div>
                <div>
                    <label>Description: </label>
                    <input id="inputAddRestaurantDesc" type="text" />
                </div>
                <div>
                    <label>Address: </label>
                    <input id="inputAddRestaurantAddressStreet" type="text" />
                </div>
                <div>
                    <label>City: </label>
                    <input id="inputAddRestaurantAddressCity" type="text" />
                </div>
                <div>
                    <label>State: </label>
                    <input id="inputAddRestaurantAddressState" type="text" />
                </div>
                <div>
                    <label>Zip Code: </label>
                    <input id="inputAddRestaurantAddressZip" type="text" />
                </div>
                <button id="btnSaveRestaurant">Save</button>
            </form>
        </div>

        <div class="panel">
            <div id="viewList">
                <p>Lists</p>
                <select id="selectPersonList"></select>

                <div id="divList">
                </div>
            </div>
        </div>

        <div id="restaurantList">
        </div>

    </div>

    <script src="library/scripts/external/jquery-1.10.2.min.js"></script>
    <script src="library/scripts/external/bootstrap.bundle.min.js"></script>
    <script src="library/scripts/helpers.js"></script>
    <script src="library/scripts/system.js"></script>
    <script src="library/scripts/app.js"></script>
    <script>
        $(document).ready(function () {
            console.log("Ready!");
            InitializeSystem();
            InitializeApp();
        });

        function InitializeSystem() {
            try {
                System.Initialize();
            } catch (e) {
                console.log(e);
            }
        }

        function InitializeApp() {
            try {
                App.Initialize();
            } catch (e) {
                console.log(e);
            }
        }
    </script>
</body>
</html>