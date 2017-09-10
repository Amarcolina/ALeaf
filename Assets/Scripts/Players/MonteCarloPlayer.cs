using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MonteCarloPlayer : IPlayer {

  private int _initialDepth;
  private int _lengthRun;


  public MonteCarloPlayer(int initialDepth, int lengthRun) {
    _initialDepth = initialDepth;
    _lengthRun = lengthRun;
  }

  public GameState.Move GetMove(GameState currState) {
    return GameState.Move.LEFT;
  }

  /*
  public GameState.Move GetMove(GameState currState) {
    using (GameState stateNone = GameState.Step(currState, GameState.Move.NONE))
    using (GameState stateLeft = GameState.Step(currState, GameState.Move.LEFT))
    using (GameState stateRight = GameState.Step(currState, GameState.Move.RIGHT)) {
      float minDist = minDistOfMovesRecursive(stateNone, _initialDepth);
      GameState.Move bestMove = GameState.Move.NONE;

      float distLeft = minDistOfMovesRecursive(stateLeft, _initialDepth);
      if (distLeft < minDist) {
        minDist = distLeft;
        bestMove = GameState.Move.LEFT;
      }

      float distRight = minDistOfMovesRecursive(stateRight, _initialDepth);
      if (distRight < minDist) {
        minDist = distRight;
        bestMove = GameState.Move.RIGHT;
      }

      return bestMove;
    }
  }

  private float minDistOfMovesRecursive(GameState startingState, int levelsDeep) {
    if (levelsDeep > 1) {
      float minDist = float.MaxValue;

      using (GameState stateNone = GameState.Step(startingState, GameState.Move.NONE))
      using (GameState stateLeft = GameState.Step(startingState, GameState.Move.LEFT))
      using (GameState stateRight = GameState.Step(startingState, GameState.Move.RIGHT)) {
        minDist = Mathf.Min(minDist, minDistOfMovesRecursive(stateNone, levelsDeep - 1));
        minDist = Mathf.Min(minDist, minDistOfMovesRecursive(stateLeft, levelsDeep - 1));
        minDist = Mathf.Min(minDist, minDistOfMovesRecursive(stateRight, levelsDeep - 1));
      }

      return minDist;
    } else {
      return minDistOfMovesRandom(startingState);
    }
  }

  private float minDistOfMovesRandom(GameState startingState) {

    float minDist = float.MaxValue;

    var currState = startingState;
    for (int i = 0; i < _lengthRun; i++) {
      GameState.Move randomMove = (GameState.Move)UnityEngine.Random.Range(0, 2);
      var newState = GameState.Step(currState, randomMove);

      if (currState != startingState) {
        currState.Dispose();
      }
      currState = newState;

      minDist = Mathf.Min(minDist, minDistOfState(currState));
    }

    currState.Dispose();
    return minDist;
  }

  private float minDistOfState(GameState state) {
    float dist;
    if (state.Pods.Count == 0) {
      dist = Vector2.Distance(Vector2.zero, state.Leaf.Position);
    } else {
      dist = float.MaxValue;
      for (int i = 0; i < state.Pods.Count; i++) {
        dist = Mathf.Min(dist, Vector2.Distance(state.Leaf.Position, state.Pods[i].Position));
      }
    }

    return -1 / dist - 5 * state.Points;
  }
  */

  public void OnDrawGizmos() { }
}
