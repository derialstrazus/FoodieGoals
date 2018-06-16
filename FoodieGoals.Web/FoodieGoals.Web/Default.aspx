<head>
    <title>Foodie Goals</title>

    <link href="library/styles/page.min.css" rel="stylesheet" />
    
</head>

<body>

    <div id="mainContainer">
        <h1>#FoodieGoals</h1>
        <div id="nameContainer">
            <p>My name is Billy Bob</p>
        </div>

        <div>
            <input id="inputMainSearch" type="text" placeholder="Find Restaurant..." />
            <button id="btnMainSearch">Go</button>
        </div>

        <hr />

        <div>
            <p>Lists</p>
            <select id="selectList">
                <option value="1">Goals</option>
                <option value="2">Visited</option>
                <option value="3">Reservation Req</option>
                <option value="4">Desserts</option>
                <option value="5">Drinks</option>
            </select>
        </div>

        <div id="divList">
        </div>
    </div>

    <script src="library/scripts/external/jquery-1.10.2.min.js"></script>
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