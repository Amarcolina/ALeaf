using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class HumanPlayer : IPlayer {

  public GameState.Move GetMove(GameState currState) {
    bool pressingLeft = Input.GetKey(KeyCode.LeftArrow);
    bool pressingRight = Input.GetKey(KeyCode.RightArrow);

    if (pressingLeft == pressingRight) {
      return GameState.Move.NONE;
    } else if (pressingLeft) {
      return GameState.Move.LEFT;
    } else {
      return GameState.Move.RIGHT;
    }
  }

  public void OnDrawGizmos() { }
}
