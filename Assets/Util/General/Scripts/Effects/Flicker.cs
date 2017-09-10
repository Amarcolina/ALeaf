using UnityEngine;
using System.Collections;

[System.Serializable]
public class Flicker {
    public float baseIntensity = 1.0f;
    public float goalIntensity = 0.0f;
    [Range(0, 1)]
    public float threshold = 0.0f;
    [Range(0, 1)]
    public float flickerProbability = 1.0f;
    [Range(0, 1)]
    public float coherency = 0.0f;
    [Range(0, 0.5f)]
    public float flickerPeriod = 0.0f;

    private float _timeUntilFlicker = 0.0f;
    private float[] _intensities;

    /* On every Update a new light value is calculated.  The new light 
     * intensity is calculated using the current light intensty as well
     * as all of the arguments and the following method.
     * 
     * A Destination intensity is calculated.  Whether or not a flicker
     * happens is calculated using the flicerProbability.  If a flicker
     * does not happen, the Destination is baseIntensity.  If a flicker
     * does happen, the Destination is randomly weighted between
     * baseIntensity and goalIntensty.  A higher threshold value pushes 
     * the destination away from the centerpoint and towards the endpoints.
     * 
     * The new light intensity is a weighted combination of the current
     * light intensity and the destination intensity.  The weighting
     * is dependent on the coherency.  If the coherency is 1.0f, the 
     * new light intensity is equal to the old light intensity.  If the
     * coherency is 0.0f, the new light intensity is equal to the destination.
     * 
     * The flickerPeriod controls how often the flicker is updated in seconds.
     */

    public float this[int index] {
        get {
            return _intensities[index];
        }
    }

    public void init(int floats, float initial) {
        _intensities = new float[floats];
        for (int i = 0; i < floats; i++) {
            _intensities[i] = initial;
        }
    }

    public void updateFlicker() {
        if (shouldFlicker(Time.deltaTime)) {
            for (int i = 0; i < _intensities.Length; i++) {
                _intensities[i] = updateBrightness(_intensities[i]);
            }
        }
    }

    public bool shouldFlicker(float deltaTime) {
        _timeUntilFlicker -= deltaTime;
        if (_timeUntilFlicker > 0.0f) {
            return false;
        }
        _timeUntilFlicker = Mathf.Max(0, _timeUntilFlicker + flickerPeriod);
        return true;
    }

    private float updateBrightness(float value) {
        float destination = 0.0f;
        if (Random.value <= flickerProbability) {
            float rand = Random.value;
            destination = rand * goalIntensity + (1 - rand) * baseIntensity;
        } else {
            destination = baseIntensity;
        }

        float midPoint = (baseIntensity + goalIntensity) / 2.0f;
        float halfMag = Mathf.Abs(baseIntensity - goalIntensity) / 2.0f;

        if (destination > midPoint) {
            if (destination <= (midPoint + threshold * halfMag)) {
                destination = midPoint + threshold * halfMag;
            }
        } else {
            if (destination >= (midPoint - threshold * halfMag)) {
                destination = midPoint - threshold * halfMag;
            }
        }

        float powCoherency = Mathf.Pow(coherency, 0.5f);
        return value * powCoherency + destination * (1 - powCoherency);
    }

    /**
     * A quick way to set the paramaters through scripting
     */
    public void setParamaters(float bIntensity, float gIntensity, float thresh, float probability, float coher, float period) {
        baseIntensity = bIntensity;
        goalIntensity = gIntensity;
        threshold = thresh;
        coherency = coher;
        flickerProbability = probability;
        flickerPeriod = period;
    }
}