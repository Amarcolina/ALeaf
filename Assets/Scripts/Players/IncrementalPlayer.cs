using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class IncrementalPlayer : IPlayer {

  private int _maxPathLength;

  private List<GameState.Move> _path = new List<GameState.Move>();

  private List<Vector3> _alternatePaths = new List<Vector3>();
  private List<Vector3> _gizmoPath = new List<Vector3>();

  private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();
  public long mili1 = 0;
  public long mili2 = 0;
  public int counter = 0;

  private GameState _tempState1 = new GameState();
  private GameState _tempState2 = new GameState();
  private GameState _tempState3 = new GameState();

  public IncrementalPlayer(int maxPathLength) {
    _maxPathLength = maxPathLength;
  }

  public GameState.Move GetMove(GameState currState) {
    double currCost = extendPathByOne(currState);

    doIncrementalModifications(currState, currCost);

    if (_path.Count <= _maxPathLength) {
      extendPathByOne(currState);
    }

    var moveToMake = _path[0];
    _path.RemoveAt(0);
    return moveToMake;
  }

  private void doIncrementalModifications(GameState currState, double currCost) {
    _alternatePaths.Clear();

    currState.CopyTo(_tempState1);

    for (int i = 0; i < _path.Count - 1; i++) {
      double alternateCostA;
      double alternateCostB;

      GameState.Move alternateMoveA = (GameState.Move)(((int)_path[i] + 1) % 3);
      GameState.Move alternateMoveB = (GameState.Move)(((int)_path[i] + 1) % 3);

      alternateCostA = costOfAlternatePath(_tempState1, i, alternateMoveA, _tempState2);
      alternateCostB = costOfAlternatePath(_tempState1, i, alternateMoveB, _tempState3);

      GameState.Move chosenMove = _path[i];

      if (alternateCostA < currCost) {
        chosenMove = alternateMoveA;
        currCost = alternateCostA;
      }

      if (alternateCostB < currCost) {
        chosenMove = alternateMoveB;
        currCost = alternateCostB;
      }

      _path[i] = chosenMove;
      _tempState1.Step(chosenMove);
    }
  }

  private double costOfAlternatePath(GameState currState,
                                   int replaceIndex,
                                   GameState.Move replace,
                                   GameState toUse) {
    currState.CopyTo(toUse);
    toUse.Step(replace);

    for (int i = replaceIndex + 1; i < _path.Count; i++) {
      toUse.Step(_path[i]);

      _alternatePaths.Add(toUse.Leaf.Position);
    }

    return costOfState(toUse);
  }

  private double extendPathByOne(GameState state) {
    _gizmoPath.Clear();

    state.CopyTo(_tempState1);
    for (int i = 0; i < _path.Count; i++) {
      _tempState1.Step(_path[i]);
      _gizmoPath.Add(_tempState1.Leaf.Position);
    }

    _tempState1.CopyTo(_tempState2);
    _tempState2.Step(GameState.Move.NONE);
    double bestCost = costOfState(_tempState2);
    GameState.Move bestMove = GameState.Move.NONE;

    _tempState1.CopyTo(_tempState2);
    _tempState2.Step(GameState.Move.LEFT);
    double costLeft = costOfState(_tempState2);
    if (costLeft < bestCost) {
      bestCost = costLeft;
      bestMove = GameState.Move.LEFT;
    }

    _tempState1.Step(GameState.Move.RIGHT);
    double costRight = costOfState(_tempState1);
    if (costRight < bestCost) {
      bestCost = costRight;
      bestMove = GameState.Move.RIGHT;
    }

    _path.Add(bestMove);
    return bestCost;
  }

  private double costOfState(GameState state) {
    double dx = Math.Max(0, Math.Abs(state.Leaf.Position.x) - 23);
    double dy = Math.Max(0, Math.Abs(state.Leaf.Position.y) - 9);

    double distToPod = double.MaxValue;
    double distToSaw = double.MaxValue;
    Vector2 leafPos = state.Leaf.Position;
    for (int i = 0; i < state.PodCount; i++) {
      var pod = state.Pods[i];
      double dist = Vector2.SqrMagnitude(leafPos - pod.Position);
      if (pod.Points > 0) {
        distToPod = Math.Min(dist, distToPod);
      } else {
        distToSaw = Math.Min(dist, distToSaw);
      }
    }

    return -1 / (distToPod + 1) - 2 * state.Points + 0.1 / (0.05 + distToSaw) + 5 * dx + 5 * dy;
  }

  public void OnDrawGizmos() {
    var r = GameObject.FindObjectOfType<LineRenderer>();

    if (r != null) {
      r.SetVertexCount(_gizmoPath.Count);
      for (int i = 0; i < _gizmoPath.Count; i++) {
        r.SetPosition(i, _gizmoPath[i]);
      }
    }

    Gizmos.color = Color.green;
    for (int i = 1; i < _gizmoPath.Count; i++) {
      Gizmos.DrawLine(_gizmoPath[i - 1], _gizmoPath[i]);
    }

    Gizmos.color = Color.blue;
    for (int i = 0; i < _alternatePaths.Count; i++) {
      Gizmos.DrawWireCube(_alternatePaths[i], Vector3.one * 0.05f);
    }
  }
}
