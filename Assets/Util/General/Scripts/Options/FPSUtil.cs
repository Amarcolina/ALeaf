using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class FPSUtil {
    private int _maxQueue;
    private Queue<long> _timestamps;
    private long _mostRecentTimestamp;
    private Stopwatch _stopwatch;

    public FPSUtil(int maxQueueSize = 1) {
        _maxQueue = maxQueueSize;
        _timestamps = new Queue<long>(maxQueueSize);

        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }

    public void step() {
        if (_timestamps.Count == _maxQueue) {
            _timestamps.Dequeue();
        }
        _mostRecentTimestamp = _stopwatch.ElapsedMilliseconds;
        _timestamps.Enqueue(_mostRecentTimestamp);
    }

    public float getFPS() {
        return 1000.0f * _timestamps.Count / (float)(_mostRecentTimestamp - _timestamps.Peek());
    }
}
