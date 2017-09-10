using UnityEngine;
using System;
using System.Collections;

public class VelocityTracker<T> {
    protected T _location;

    protected T _previousLocation;
    protected T _smoothVelocity;

    protected float _smoothingFactor;
    protected float _dampingFactor;

    protected bool _hasFresh = false;
    protected bool _needToReset = false;

    protected Func<T, T, T> _add;
    protected Func<T, T, T> _delta;
    protected Func<T, float, T> _multiply;
    protected Func<T, T, float, T> _lerp;

    public VelocityTracker(float smoothingFactor, 
                           float dampingFactor, 
                           T initial,
                           Func<T,T,T> add, 
                           Func<T,T,T> delta, 
                           Func<T,float,T> multiply, 
                           Func<T,T,float,T> lerp){
        _smoothingFactor = 1.0f - smoothingFactor;
        _dampingFactor = dampingFactor;

        _location = initial;

        _add = add;
        _delta = delta;
        _multiply = multiply;
        _lerp = lerp;
    }

    public virtual bool isFresh() {
        return _hasFresh;
    }

    public virtual void putPosition(T curr, float multiplier = 1.0f, float weight = 1.0f) {
        if (_needToReset) {
            _previousLocation = curr;
            _needToReset = false;
        }

        T delta = _multiply(_delta(curr, _previousLocation), multiplier / Time.deltaTime);
        _smoothVelocity = _lerp(_smoothVelocity, delta, _smoothingFactor * weight);
        _previousLocation = curr;

        _location = _add(_location, _multiply(_smoothVelocity, Time.deltaTime));

        _hasFresh = true;
    }

    public virtual T getLocation() {
        //If our data is not fresh we do velocity extrapolation
        if (!_hasFresh) { 
            _location = _add(_location, _multiply(_smoothVelocity, Time.deltaTime));
            _smoothVelocity = _multiply(_smoothVelocity, _dampingFactor);
            _needToReset = true;
        }

        _hasFresh = false;
        
        return _location;
    }
}

public class AngularVelocityTracker : VelocityTracker<float> {
    public AngularVelocityTracker(float smoothingFactor, float dampingFactor, float initial) :
        base(smoothingFactor, dampingFactor, initial, 
        (a, b) => a + b, (a, b) => Mathf.DeltaAngle(b, a), (a, b) => a * b, (a, b, c) => Mathf.Lerp(a, b, c)) { }

}

public class VelocityTrackerVector3 : VelocityTracker<Vector3> {
    public VelocityTrackerVector3(float smoothingFactor, float dampingFactor, Vector3 initial) : 
        base (smoothingFactor, dampingFactor, initial, 
        (a,b) => a + b, (a,b) => a - b, (a,b) => a * b, (a,b,c) => Vector3.Lerp(a,b,c)){}
}

public class VelocityTrackerVector2 : VelocityTracker<Vector2> {
    public VelocityTrackerVector2(float smoothingFactor, float dampingFactor, Vector2 initial) : 
        base (smoothingFactor, dampingFactor, initial, 
        (a,b) => a + b, (a,b) => a - b, (a,b) => a * b, (a,b,c) => Vector3.Lerp(a,b,c)){}
}

public class VelocityTrackerFloat : VelocityTracker<float> {
    public VelocityTrackerFloat(float smoothingFactor, float dampingFactor, float initial) :
        base(smoothingFactor, dampingFactor, initial, 
        (a, b) => a + b, (a, b) => a - b, (a, b) => a * b, (a, b, c) => Mathf.Lerp(a, b, c)) { }
}