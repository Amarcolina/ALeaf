using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueueVelocityFilter : IVelocityFilter {
    private Queue<TimestampedPosition> _positions = new Queue<TimestampedPosition>();
    private float _window;

    private TimestampedPosition _top;

    private struct TimestampedPosition {
        public readonly Vector3 position;
        public readonly float time;

        public TimestampedPosition(Vector3 position, float time) {
            this.position = position;
            this.time = time;
        }
    }

    public QueueVelocityFilter(float timeWindow) {
        _window = timeWindow;
    }

    public void reportPosition(Vector3 position, float time) {
        while (_positions.Count != 0 && _positions.Peek().time < time - _window) {
            _positions.Dequeue();
        }

        _top = new TimestampedPosition(position, time);
        _positions.Enqueue(_top);
    }

    public Vector3 getVelocity() {
        if (_positions.Count <= 1) {
            return Vector3.zero;
        }

        TimestampedPosition bottom = _positions.Peek();
        return (_top.position - bottom.position) / (_top.time - bottom.time);
    }
}
