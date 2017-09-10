using UnityEngine;
using System.Collections;

public class WaterLevel {
    protected float _highTide;
    protected float _lowTide;
    protected float _currValue;
    protected bool _enabled;

    public virtual float highTide {
        get {
            return _highTide;
        }
        set {
            _highTide = value;
            update(_currValue);
        }
    }

    public virtual float lowTide {
        get {
            return _highTide;
        }
        set {
            _lowTide = value;
            update(_currValue);
        }
    }

    public virtual float currValue {
        get {
            return _currValue;
        }
        set {
            _currValue = value;
            if (_currValue > _highTide) {
                _enabled = true;
            }
            if (_currValue < _highTide) {
                _enabled = false;
            }
        }
    }

    public virtual bool enabled {
        get {
            return _enabled;
        }
        set {
            _enabled = value;
        }
    }

    public WaterLevel(float lowTide, float highTide) {
        _highTide = highTide;
        _lowTide = lowTide;
        _currValue = _lowTide;
    }

    public virtual bool update(float value) {
        currValue = value;
        return _enabled;
    }
}

public class RisingWaterLevel : WaterLevel {
    protected float _risenLowTide;
    protected bool _canBeEnabled;

    public float risenWaterLevel {
        get {
            return _risenLowTide;
        }
    }

    public RisingWaterLevel(float lowTide, float highTide)
        : base(lowTide, highTide) {
        _risenLowTide = _lowTide;
    }

    public override float currValue {
        get {
            return _currValue;
        }
        set {
            _currValue = value;

            if (_currValue > _highTide && _canBeEnabled) {
                _enabled = true;
                _canBeEnabled = false;
            }

            if (_enabled) {
                _risenLowTide = Mathf.Max(_risenLowTide, _currValue - (_highTide - _lowTide));
            }

            if (_currValue < _risenLowTide) {
                _enabled = false;
            }

            if (_currValue < _lowTide) {
                _canBeEnabled = true;
                _risenLowTide = _lowTide;
            }
        }
    }
}
