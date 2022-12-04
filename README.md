# Computer_opponent
A modular computer opponent program for 2-player board games.

## Running the project
- Install [.NET 6.0](https://dotnet.microsoft.com/en-us/)
- Clone this project and enter the cloned folder
- Run the project with `dotnet run`

## Adding support for additional games
To create a new game to play against the computer opponent,
you'd need to create a class that implements the IGame interface.<br />
To get a grasp for how IGame's methods should be implemented, 
look at the implementation of the `ConnectN` and `TicTacToe` classes.
