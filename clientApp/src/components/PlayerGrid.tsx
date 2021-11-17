import { Component, ReactNode } from "react";
import { CellStatus } from "../models/CellStatus";
import { Player } from "../models/Player";

interface PlayerGridProps {
    player: Player;
}

export class PlayerGrid extends Component<PlayerGridProps> {

    getColor(x: number, y: number): string {
        let cs = this.props.player.map[x][y];
        if (cs === CellStatus.water)
            return 'bg-blue-300';
        else if (cs === CellStatus.hit)
            return 'bg-red-100';
        else if (cs === CellStatus.destroyed)
            return 'bg-red-300';
        else if (cs === CellStatus.miss)
            return 'bg-gray-300';
        else if (cs === CellStatus.ship)
            return 'bg-blue-900';


        return 'bg-blue-300';
    }

    render(): ReactNode {
        let py = -1;
        let px = -1;
        let grid = this.props.player.map.map(x => {
            py++;
            px = -1;
            return <div key={`${px},${py}`} className='flex'>
                {
                    x.map(y => {
                        px++;
                        return <div key={`${px},${py}`} id={`${px},${py}`} className={`player-grid  ${this.getColor(px, py)}`} style={{ width: "40px", height: "40px" }}></div>;
                    })
                }</div>
        });
        return (
            <div>
                {grid}
            </div>
        )
    }
}