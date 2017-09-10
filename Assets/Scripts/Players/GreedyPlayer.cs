using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GreedyPlayer : IPlayer {

  private int _lookAhead;
  private float _scaling;

  private GameState.Move[] _moves;
  private GameState.Move[] _bestMoves;

  private Vector3[] _positions;
  private Vector3[] _bestPositions;

  private float _bestCost;

  public GreedyPlayer(int lookAhead, float scaling) {
    _lookAhead = lookAhead;
    _scaling = scaling;

    _moves = new GameState.Move[lookAhead];
    _bestMoves = new GameState.Move[lookAhead];
    _positions = new Vector3[lookAhead];
    _bestPositions = new Vector3[lookAhead];
  }

  public GameState.Move GetMove(GameState currState) {
    return GameState.Move.LEFT;
  }

  public void OnDrawGizmos() { }

  /*
  public GameState.Move GetMove(GameState currState) {
    _bestCost = float.MaxValue;
    minDistOfMovesRecursive(currState, 0, 1);

    return _bestMoves[0];
  }

  private void minDistOfMovesRecursive(GameState startingState, int levelsDeep, float scale) {
    if (levelsDeep < _lookAhead) {
      using (GameState stateNone = GameState.Step(startingState, GameState.Move.NONE, Mathf.RoundToInt(scale))) {
        _moves[levelsDeep] = GameState.Move.NONE;
        _positions[levelsDeep] = stateNone.Leaf.Position;
        minDistOfMovesRecursive(stateNone, levelsDeep + 1, scale * _scaling);
      }
      using (GameState stateLeft = GameState.Step(startingState, GameState.Move.LEFT, Mathf.RoundToInt(scale))) {
        _moves[levelsDeep] = GameState.Move.LEFT;
        _positions[levelsDeep] = stateLeft.Leaf.Position;
        minDistOfMovesRecursive(stateLeft, levelsDeep + 1, scale * _scaling);
      }
      using (GameState stateRight = GameState.Step(startingState, GameState.Move.RIGHT, Mathf.RoundToInt(scale))) {
        _moves[levelsDeep] = GameState.Move.RIGHT;
        _positions[levelsDeep] = stateRight.Leaf.Position;
        minDistOfMovesRecursive(stateRight, levelsDeep + 1, scale * _scaling);
      }
    } else {
      float cost = costOfState(startingState);
      if (cost < _bestCost) {
        Array.Copy(_moves, _bestMoves, _moves.Length);
        Array.Copy(_positions, _bestPositions, _positions.Length);
        _bestCost = cost;
      }
    }
  }

  private float costOfState(GameState state) {
    float dist;
    if (state.Pods.Count == 0) {
      dist = Vector2.Distance(Vector2.zero, state.Leaf.Position);
    } else {
      dist = float.MaxValue;
      for (int i = 0; i < state.Pods.Count; i++) {
        dist = Mathf.Min(dist, Vector2.Distance(state.Leaf.Position, state.Pods[i].Position));
      }
    }

    float distToSaw = float.MaxValue;
    for (int i = 0; i < state.Saws.Count; i++) {
      distToSaw = Mathf.Min(distToSaw, Vector2.Distance(state.Leaf.Position, state.Saws[i].Position));
    }

    return -1 / (dist + 1) - 2 * state.Points + 10 * state.SawCount + 0.1f / distToSaw;
  }

  public void OnDrawGizmos() {
    Gizmos.color = Color.green;
    for (int i = 1; i < _bestPositions.Length; i++) {
      Gizmos.DrawLine(_bestPositions[i - 1], _bestPositions[i]);
    }
  }
   */
}
