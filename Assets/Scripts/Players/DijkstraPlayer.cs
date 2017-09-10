using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DijkstraPlayer : IPlayer {

  private PriorityQueue<float, MoveGameState> stateQueue = new PriorityQueue<float, MoveGameState>();
  private List<Vector3> exploredStates = new List<Vector3>();
  private int _checks;

  private GameState.Move _best;
  private float _bestCost;
  private Vector3 _bestPos;

  private GameState _modState = new GameState();

  private struct MoveGameState {
    public GameState.Move First;
    public GameState State;

    public MoveGameState(GameState.Move first, GameState state) {
      First = first;
      State = state;
    }
  }

  public DijkstraPlayer(int checks) {
    _checks = checks;
  }

  public GameState.Move GetMove(GameState current) {
    /*
    stateQueue.Clear();
    exploredStates.Clear();
    _bestCost = float.MaxValue;
    */

    //enqueueState(GameState.Step(current, GameState.Move.NONE), GameState.Move.NONE);
    //enqueueState(GameState.Step(current, GameState.Move.LEFT), GameState.Move.LEFT);
    //enqueueState(GameState.Step(current, GameState.Move.RIGHT), GameState.Move.RIGHT);

    /*
    for (int i = 0; i < _checks; i++) {
      var d = stateQueue.DequeueValue();
      explorestate(d.State, d.First);
      exploredStates.Add(d.State.Leaf.Position);
      d.State.Dispose();
    }
    */

    /*
    _best = stateQueue.PeekValue().First;
    _bestPos = stateQueue.PeekValue().State.Leaf.Position;
    */

    /*
    foreach (var p in stateQueue) {
      p.Value.State.Dispose();
    }
    
    return _best;
     */
    return GameState.Move.LEFT;
  }

  /*
  private void explorestate(GameState state, GameState.Move first) {
    enqueueState(GameState.Step(state, GameState.Move.NONE), first);
    enqueueState(GameState.Step(state, GameState.Move.LEFT), first);
    enqueueState(GameState.Step(state, GameState.Move.RIGHT), first);
  }

  private void enqueueState(GameState state, GameState.Move first) {
    float cost = minDistOfState(state);

    if (cost < _bestCost) {
      _bestCost = cost;
      _best = first;
    }

    //exploredStates.Add(state.Leaf.Position);
    stateQueue.Add(new KeyValuePair<float, MoveGameState>(cost, new MoveGameState(first, state)));
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
    return -2.0f * state.Points - 1.0f / dist;
  }

  public void OnDrawGizmos() {
    Gizmos.color = Color.white;
    foreach (var p in exploredStates) {
      Gizmos.DrawWireCube(p, Vector3.one * 0.05f);
    }

    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(_bestPos, Vector3.one * 0.05f);
  }
  */

  public void OnDrawGizmos() { }
}
