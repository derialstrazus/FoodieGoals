<head>
    <title>Foodie Goals</title>

    <link href="library/styles/page.min.css" rel="stylesheet" />
    
</head>

<body>

    <div id="mainContainer">
        <h1>Foodie Goals</h1>

        <div>
            <input id="inputMainSearch" type="text" placeholder="Find Restaurant..." />
            <button id="btnMainSearch">Go</button>
        </div>

        <hr />

        <div>
            <p>Lists</p>
            <select>
                <option>Goals</option>
                <option>Visited</option>
            </select>
        </div>

        <div id="divList">
        </div>
    </div>

    <script src="library/scripts/external/jquery-1.10.2.min.js"></script>
    <script src="library/scripts/app.js"></script>
    <script>
        $(document).ready(function () {
            console.log("Ready!");
            App.Initialize();
        });
    </script>
</body>