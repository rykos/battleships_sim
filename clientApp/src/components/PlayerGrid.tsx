import { Component, ReactNode } from "react";
import { Player } from "../models/Player";
import { GameGrid } from "./GameGrid";

interface PlayerGridProps {
    player: Player;
    name: string;
}
export class PlayerGrid extends Component<PlayerGridProps> {
    render(): ReactNode {
        let yaxis = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        let xaxis = ['', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'];
        if (this.props.player)
            return (
                <div className='player-grid-container'>
                    <div className='flex'>
                        {xaxis.map(x => {
                            return <div key={x} style={{ width: "40px", height: "40px" }}>{x}</div>;
                        })}
                    </div>
                    <div className='flex'>
                        <div className='flex flex-col'>
                            {yaxis.map(x => {
                                return <div key={x} style={{ width: "40px", height: "40px" }}>{x}</div>;
                            })}
                        </div>
                        <GameGrid player={this.props.player}></GameGrid>
                    </div>
                    <div>{this.props.name}</div>
                </div>
            )
    }
}