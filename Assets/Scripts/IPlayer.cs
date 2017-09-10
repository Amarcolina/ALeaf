using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public interface IPlayer {
  GameState.Move GetMove(GameState currState);
  void OnDrawGizmos();
}
