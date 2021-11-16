import { Component, ReactNode } from "react";
import { CellStatus } from "../models/CellStatus";
import { Player } from "../models/Player";

interface PlayerGridProps {
    player: Player;
}

export class PlayerGrid extends Component<PlayerGridProps> {
    render(): ReactNode {
        let py = -1;
        let px = -1;
        let qwe = this.props.player.map.map(x => {
            py++;
            px = -1;
            return <div key={`${px},${py}`} className='flex'>
                {
                    x.map(y => {
                        px++;
                        return <div key={`${px},${py}`} id={`${px},${py}`} className={`space-x-5 border border-gray-500 flex justify-center items-center bg-blue-300 ${this.props.player.map[px][py] == CellStatus.ship ? "bg-purple-500" : ""}`} style={{ width: "40px", height: "40px" }}>{this.props.player.map[px][py]}</div>;
                    })
                }</div>
        });
        return (
            <div>
                {qwe}
            </div>
        )
    }
}