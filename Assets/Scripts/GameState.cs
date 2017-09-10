using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameState : IDisposable {
  public const float GRAVITY = -0.005f;
  public const float UPDRAFT = 0.0151f;
  public const float DAMPGING = 0.999f;

  public const float POD_SPAWN_CHANCE = 0.04f;

  public const int TURN_AMOUNT = 3;
  public const int TABLE_LENGTH = 360 / TURN_AMOUNT;

  public const float MIN_POD_SPEED = 0.025f;
  public const float MAX_POD_SPEED = 0.1f;
  public const float POD_RADIUS = 0.5f;

  public const float WIDTH = 40;
  public const float HEIGHT = 20;

  public const float LEAF_RADIUS = 0.15f;
  public const float LEAF_LENGTH = 0.35f;

  public enum Move {
    NONE,
    LEFT,
    RIGHT
  }

  [Serializable]
  public struct LeafState {
    public static float[] SIN_TABLE;
    public static float[] COS_TABLE;

    static LeafState() {
      SIN_TABLE = new float[TABLE_LENGTH];
      COS_TABLE = new float[TABLE_LENGTH];

      for (int i = 0; i < TABLE_LENGTH; i++) {
        SIN_TABLE[i] = Mathf.Sin(i * TURN_AMOUNT * Mathf.Deg2Rad);
        COS_TABLE[i] = Mathf.Cos(i * TURN_AMOUNT * Mathf.Deg2Rad);
      }
    }

    public Vector2 Position;
    public int Angle;
    public float Speed;

    public Vector2 EndA, EndB;

    public LeafState(Vector2 position, int angle, float speed) {
      Position = position;
      Angle = angle;
      Speed = speed;
      EndA = position;
      EndB = position;
    }

    public void Step(Move move) {
      if (move == Move.LEFT) {
        Angle = (Angle + 1) % TABLE_LENGTH;
      } else if (move == Move.RIGHT) {
        Angle = (Angle - 1 + TABLE_LENGTH) % TABLE_LENGTH;
      }

      float Sin = SIN_TABLE[Angle];
      float Cos = COS_TABLE[Angle];

      Speed += GRAVITY * Sin;

      Position.x += Cos * Speed - Sin * UPDRAFT * Cos;
      Position.y += Sin * Speed + Cos * UPDRAFT * Cos;

      var delta = new Vector2(Cos, Sin) * LEAF_LENGTH;
      EndA = Position + delta;
      EndB = Position - delta;

      Speed *= DAMPGING;
    }

    public float AngleDegrees {
      get {
        return Angle * TURN_AMOUNT;
      }
    }
  }

  [Serializable]
  public struct PodState {
    public Vector2 Position;
    public float Speed;
    public float Points;

    public PodState(Vector2 position, float speed, int points) {
      Position = position;
      Speed = speed;
      Points = points;
    }

    public bool Step() {
      Position.x += 0.01f * LeafState.SIN_TABLE[(int)(Position.y * 10 + 225) % LeafState.SIN_TABLE.Length];

      float multiplier = Mathf.Min(1, Position.y * 0.5f + HEIGHT * 0.25f + 0.1f);

      if (Points > -10) {
        if (Position.y < -HEIGHT / 2) {
          Points -= 0.2f;
        }
        Position.y -= Speed * multiplier;
        return false;
      } else {
        Position.y += Speed * multiplier;
        return Position.y > HEIGHT / 2;
      }
    }
  }

  private static Queue<GameState> _statePool = new Queue<GameState>();

  public LeafState Leaf;
  
  public int PodCount = 0;
  public int Points = 0;
  public int Frame = 0;

  [NonSerialized]
  public PodState[] Pods = new PodState[8];

  private bool _isValid = true;

  /*
  private GameState() { }
  */

  public bool IsValid {
    get {
      return _isValid;
    }
  }

  public void Dispose() {
    if (!_isValid) {
      throw new Exception("Cannot dispose of a state twice!");
    }

    _isValid = false;
    _statePool.Enqueue(this);
  }

  private static GameState spawnFromPool() {
    GameState newState;
    if (_statePool.Count == 0) {
      newState = new GameState();
    } else {
      newState = _statePool.Dequeue();

      if (newState._isValid) {
        throw new Exception("Tried to spawn a state that was already valid!");
      }
    }

    newState._isValid = true;
    return newState;
  }

  public GameState Clone() {
    GameState newState = spawnFromPool();
    CopyTo(newState);
    return newState;
  }

  public void CopyTo(GameState state) {
    state.Leaf = Leaf;

    if (state.Pods.Length < Pods.Length) {
      state.Pods = new PodState[Pods.Length];
    }
    Array.Copy(Pods, state.Pods, Pods.Length);

    state.PodCount = PodCount;
    state.Points = Points;
    state.Frame = Frame;
  }

  public void Step(Move move) {
    Leaf.Step(move);
    Frame++;

    for (int i = 0; i < PodCount; i++) {
      var pod = Pods[i];
      if (pod.Step()) {
        Pods[i] = Pods[--PodCount];
        continue;
      }

      if (DoesHitCapsule(pod.Position)) {
        Points += (int)pod.Points;
        Pods[i] = Pods[--PodCount];
        continue;
      }

      Pods[i] = pod;
    }
  }

  public void Step(Move move, System.Random random) {
    Step(move);

    if (random.NextDouble() < POD_SPAWN_CHANCE) {
      addPod(random, 1);
    }

    /*
    if (random.NextDouble() < SAW_SPAWN_CHANCE) {
      addPod(random, -10);
    }
     */
  }

  private void addPod(System.Random random, int points) {
    float x = (float)(random.NextDouble() * WIDTH - WIDTH / 2);
    float speed = (float)(random.NextDouble() * (MAX_POD_SPEED - MIN_POD_SPEED) + MIN_POD_SPEED);

    if (Pods.Length == PodCount) {
      var newArray = new PodState[Pods.Length * 2];
      Array.Copy(Pods, newArray, Pods.Length);
      Pods = newArray;
    }

    Pods[PodCount++] = new PodState(new Vector2(x, HEIGHT / 2), speed, points);
  }

  public bool DoesHitCapsule(Vector2 pos) {
    Vector2 pa = pos - Leaf.EndA;
    if (pa.sqrMagnitude > 4) {
      return false;
    }

    Vector2 ba = Leaf.EndB - Leaf.EndA;
    float h = Mathf.Clamp01(Vector2.Dot(pa, ba) / Vector2.Dot(ba, ba));
    return (pa - ba * h).sqrMagnitude < (LEAF_RADIUS + POD_RADIUS) * (LEAF_RADIUS + POD_RADIUS);
  }
}
