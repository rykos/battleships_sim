import { getuid } from 'process';
import React, { Component, Fragment } from 'react';
import './App.scss';
import { PlayerGrid } from './components/PlayerGrid';
import { CellStatus } from './models/CellStatus';
import { Game } from './models/Game';

class App extends Component<{}, {}> {
  game: Game | undefined;
  constructor(p: any) {
    super(p);
    fetch("http://localhost:5000/game").then(x => {
      x.json().then(json => {
        this.game = json as Game;
        this.forceUpdate();
      })
    }).catch(err => {
      alert(err);
    });
  }

  componentDidMount() {

  }

  render(): React.ReactNode {
    let content;
    if (this.game)
      content = <Fragment>
        <PlayerGrid player={this.game!.p1}></PlayerGrid>
        <PlayerGrid player={this.game!.p2}></PlayerGrid>
      </Fragment>
    return (
      <div className='flex justify-between'>
        {content}
      </div>
    )
  }
}

export default App;