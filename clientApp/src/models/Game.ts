import { Player } from "./Player";

export interface Game {
    p1: Player;
    p2: Player;
    finished: boolean;
}