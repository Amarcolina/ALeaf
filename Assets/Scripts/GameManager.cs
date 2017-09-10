using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum PlayerType {
  Human,
  Greedy_1,
  Greedy_3,
  Greedy_5,
  GreedyScaling_11,
  GreedyScaling_12,
  GreedyScaling_15,
  GreedyScaling_20,
  MonteCarlo,
  Dijkstra,
  Incremental_20,
  Incremental_30,
  Incremental_40
}

public static class PlayerTypeExtension {
  public static IPlayer CreatePlayer(this PlayerType type) {
    switch (type) {
      case PlayerType.Human:
        return new HumanPlayer();
      case PlayerType.Greedy_1:
        return new GreedyPlayer(1, 1);
      case PlayerType.Greedy_3:
        return new GreedyPlayer(3, 1);
      case PlayerType.Greedy_5:
        return new GreedyPlayer(5, 1);
      case PlayerType.GreedyScaling_11:
        return new GreedyPlayer(5, 1.1f);
      case PlayerType.GreedyScaling_12:
        return new GreedyPlayer(5, 1.2f);
      case PlayerType.GreedyScaling_15:
        return new GreedyPlayer(5, 1.5f);
      case PlayerType.GreedyScaling_20:
        return new GreedyPlayer(5, 2.1f);
      case PlayerType.MonteCarlo:
        return new MonteCarloPlayer(3, 10);
      case PlayerType.Dijkstra:
        return new DijkstraPlayer(1000);
      case PlayerType.Incremental_20:
        return new IncrementalPlayer(20);
      case PlayerType.Incremental_30:
        return new IncrementalPlayer(30);
      case PlayerType.Incremental_40:
        return new IncrementalPlayer(40);
      default:
        throw new Exception("Unexpected Player Type!");
    }
  }
}

public class GameManager : MonoBehaviour {

  [SerializeField]
  private GameObject _podPrefab;

  [SerializeField]
  private Color _podColor = Color.green;

  [SerializeField]
  private Color _sawColor = Color.red;

  [SerializeField]
  private Transform _leafTransform;

  [SerializeField]
  private PlayerType _playerType = PlayerType.Human;

  [SerializeField]
  private int frameSkip = 1;

  [SerializeField]
  private int seed = 0;

  [SerializeField]
  private GameState _state;

  [SerializeField]
  private LineRenderer _lineRenderer;

  private List<Renderer> _currPods = new List<Renderer>();

  private System.Random _random;

  private IPlayer _player;
  private bool doSpawning = true;

  void Start() {
    Application.targetFrameRate = 60;

    _player = _playerType.CreatePlayer();
    _random = new System.Random(seed);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.S)) {
      doSpawning = false;
    }

    if (Input.GetKeyDown(KeyCode.L)) {
      _lineRenderer.enabled = !_lineRenderer.enabled;
    }

    for (int i = 0; i < frameSkip; i++) {
      GameState.Move move = _player.GetMove(_state);

      _state.Step(move, _random);
    }

    _leafTransform.position = _state.Leaf.Position;
    _leafTransform.eulerAngles = new Vector3(0, 0, _state.Leaf.AngleDegrees);

    while (_currPods.Count > _state.PodCount) {
      Renderer pod = _currPods[_currPods.Count - 1];
      _currPods.RemoveAt(_currPods.Count - 1);
      DestroyImmediate(pod.gameObject);
    }

    while (_currPods.Count < _state.PodCount) {
      Renderer newPod = Instantiate(_podPrefab).GetComponent<Renderer>();
      newPod.transform.parent = transform;
      _currPods.Add(newPod);
    }

    for (int i = 0; i < _state.PodCount; i++) {
      Renderer pod = _currPods[i];
      pod.transform.position = _state.Pods[i].Position;
      float percentGood = Mathf.InverseLerp(-10, 1, _state.Pods[i].Points);
      pod.material.SetColor("_TintColor", Color.Lerp(_sawColor, _podColor, percentGood));
      pod.transform.localScale = Vector3.one * Mathf.Lerp(0.6f, 0.3f, percentGood);
    }

    gameObject.name = _playerType.ToString() + " " + _state.Points;

    //_player.OnDrawGizmos();
  }

  public bool drawGizmos = false;
  void OnDrawGizmos() {
    if (drawGizmos) {
      /*
      for (float dx = -2; dx <= 2; dx += 0.05f) {
        for (float dy = -2; dy <= 2; dy += 0.05f) {
          Vector2 pos = _state.Leaf.Position + new Vector2(dx, dy);
          if (!_state.DoesHitCapsule(pos)) {
            Gizmos.DrawWireCube(pos, Vector3.one * 0.05f);
          }
        }
      }
      */

      if (_player != null) {
        _player.OnDrawGizmos();
      }
    }
  }
}
