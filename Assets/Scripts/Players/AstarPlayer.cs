using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class AstarPlayer : IPlayer {

  public GameState.Move GetMove(GameState currState) {
    return GameState.Move.NONE;
  }

  public void OnDrawGizmos() { }
}
