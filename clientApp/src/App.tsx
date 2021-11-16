import React, { Component, Fragment } from 'react';
import './App.scss';
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
        result = <div className='flex text-2xl items-center'>player {this.state.game.p1.turns.length > this.state.game.p2.turns.length ? "1" : "2"} wins</div>
      content = <Fragment>
        <div>
          <div className='text-2xl text-center'>Player 1</div>
          <PlayerGrid player={this.state.game?.p1}></PlayerGrid>
        </div>
        {result}
        <div>
          <div className='text-2xl text-center'>Player 2</div>
          <PlayerGrid player={this.state.game?.p2}></PlayerGrid>
        </div>
      </Fragment>
    }
    return (
      <div className='flex flex-col p-10'>
        <div className='flex justify-between'>
          {content}
        </div>
        <div className='flex justify-center mt-5' style={{ height: "50px" }}>
          <div className='cursor-pointer bg-blue-400 rounded flex justify-center items-center w-1/6' onClick={() => { this.restartClicked(); }}>Again</div>
        </div>
      </div>
    )
  }
}

export default App;