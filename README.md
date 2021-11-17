## Battleship_sim
Battleship game simulation between 2 bots.
.Net5 backend, React frontend

[screen1](gitimg/1.png)
[screen2](gitimg/2.png)

### Rules
 - Ships cannot be placed adjacent to each other
 - Game generates 5 standard ships [5,4,3,3,2]
 - Unlimited ammo
 - Map size 10x10
 - The other player is informed of the sunken ship
### Game loop
Players are generated, each player places ship at random position and direction. Then each player takes shot at opposing player. Game lasts until one of them runs out of ships. Game log is sent back as response to the frontend server, which displays each turn from the start of the game to the end at which winner is declared.
### Shooting
Bot fires into random valid cells until it finds the ship, then it looks in adjacent fields for the rest of the ship until its sunk.
### Connection to frontend
Backend API exposes endpoint "/game" that returns game.