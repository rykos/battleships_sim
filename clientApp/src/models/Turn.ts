import { Position } from './Position';
import { CellStatus } from './CellStatus';
export interface Turn {
    position: Position;
    cellStatus: CellStatus;
}