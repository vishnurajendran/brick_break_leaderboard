# Brick Breaker Leaderboard Backend Server

This project implements the back-end server for global leaderboard feature of brick breaker game client

## Features
1. Leaderboard page
2. Score submit API
3. Leaderboard API

## Build Steps
### .Net Version
.Net Core 7.0 (because my rider is shipped with .net 7.0 :p)
to install sdk and runtime follow official [documentation](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

Once the correct dotnet version is installed, Follow the next steps to build the project

### Installation
1. Clone the repository using `git clone https://github.com/vishnurajendran/brick_break_leaderboard.git`
2. Naviagate the repo `cd brick_break_leaderboard`
3. Run the build command `./build.sh`

    > **Note**: If you get a permission denied message run `chmod 777 build.sh`
4. Wait for build to complete

    > **NOTE**: **This should succeed always unless your dotnet is not setup correctly**

### Running the server
1. If the build completes, simply run `./run_server.sh`

    > **NOTE**: If you get a permission denied message run `chmod 777 run_server.sh`

## Runtime
The server runs on port 9000, Ensure HTTP traffic is allowed on this port

## API

### Submit Score API

endpoint: `/api/submit-score`

    rquest-type       : POST
    content-type      : `application/json`
    data format       : json string
    request-structure :
    {
        EntryName: "<Enter Name>",
        Score: <score-int>
    }
     
### Get Leaderboard API
endpoint: `/api/get-leaderboard`

    rquest-type       : GET
