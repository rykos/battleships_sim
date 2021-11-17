import React, { Component, Fragment } from 'react';
import './App.scss';
import { GameGrid } from './components/GameGrid';
import { PlayerGrid } from './components/PlayerGrid';
import { CellStatus } from './models/CellStatus';
import { Game } from './models/Game';

class App extends Component<{}, { game: Game | undefined }> {
  interval: number | undefined;
  constructor(p: any) {
    super(p);
    this.state = {
      game: undefined
    }
  }

  componentDidMount() {
    this.getSimulation();
  }

  getSimulation() {
    fetch("http://localhost:5000/game").then(x => {
      x.json().then(json => {
        let game = json as Game;
        game.finished = false;
        this.setState({
          game: game
        }, () => {
          console.log(this.state.game);
          this.gameLoop();
        })
      })
    }).catch(err => {
      alert(err);
    });
  }

  async gameLoop() {
    let j = 0;
    let i = 0;
    let g = this.state.game!;
    this.interval = setInterval(() => {
      if (j % 2 === 0) {
        if (g.p1.turns[i]) {
          let turn = g.p1.turns[i];
          if (turn.cellStatus !== CellStatus.water)
            g.p2.map[turn.position.x][turn.position.y] = CellStatus.destroyed;
          else
            g.p2.map[turn.position.x][turn.position.y] = CellStatus.miss;
          this.setState({
            game: g
          })
        }
      }
      else {
        if (g.p2.turns[i]) {
          let turn = g.p2.turns[i];
          if (turn.cellStatus !== CellStatus.water)
            g.p1.map[turn.position.x][turn.position.y] = CellStatus.destroyed;
          else
            g.p1.map[turn.position.x][turn.position.y] = CellStatus.miss;
          this.setState({
            game: g
          })
        }
        i++;
      }
      if (i >= this.state.game!.p1.turns.length) {
        let game = this.state.game;
        game!.finished = true;
        this.setState({ game: game })
        clearInterval(this.interval);
      }
      j++
    }, 200) as any;
  }

  restartClicked() {
    if (this.interval)
      clearInterval(this.interval);
    this.getSimulation();
  }

  render(): React.ReactNode {
    let result;
    let content;
    if (this.state.game) {
      if (this.state.game.finished)
        result = <div className='flex text-2xl items-center' style={{ minWidth: "140px" }}>player {this.state.game.p1.turns.length > this.state.game.p2.turns.length ? "1" : "2"} wins</div>
      content = <Fragment>
        <PlayerGrid player={this.state.game?.p1} name='Player 1'></PlayerGrid>
        {result}
        <PlayerGrid player={this.state.game?.p2} name='Player 2'></PlayerGrid>
      </Fragment>
    }
    return (
      <div className='flex justify-center'>
        <div className='flex flex-col p-10' style={{ width: "1200px" }}>
          <div className='flex justify-between'>
            {content}
          </div>
          <div className='flex justify-center mt-5 text-xl' style={{ height: "50px" }}>
            <div className='restart-button' onClick={() => { this.restartClicked(); }}>Again</div>
          </div>
        </div>
      </div>
    )
  }
}

export default App;