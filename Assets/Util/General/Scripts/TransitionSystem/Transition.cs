using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Transition : MonoBehaviour {
    private static int _transitionsLeft = 0;
    private static string _firstLoadedLevel = null;
    protected bool isTransitioningOut = false;

    private static List<Transition> sceneTransitions = new List<Transition>();

    private static AsyncOperation _loadLevelOperation = null;

    protected virtual void OnEnable() {
        sceneTransitions.Add(this);
    }

    protected virtual void OnDisable() {
        sceneTransitions.Remove(this);
    }

    public static bool requestTransitionTo(string levelName) {
        if (Application.isLoadingLevel) {
            return false;
        }

        if (_firstLoadedLevel == null) {
            _firstLoadedLevel = Application.loadedLevelName;
        }

        if (Application.loadedLevelName == levelName) {
            return false;
        }

        if (_transitionsLeft != 0) {
            return false;
        }

        _loadLevelOperation = Application.LoadLevelAsync(levelName);

        if (sceneTransitions.Count == 0) {
            _loadLevelOperation = null;
            return true;
        }

        _loadLevelOperation.allowSceneActivation = false;

        _transitionsLeft = sceneTransitions.Count;
        foreach (Transition t in sceneTransitions) {
            t.beginTransitionOut();
        }
        return true;
    }

    public static bool requestTransitionToFirst() {
        if (_firstLoadedLevel == null) {
            return false;
        }

        return requestTransitionTo(_firstLoadedLevel);
    }

    public abstract void beginTransitionOut();

    protected void reportTransitionFinished() {
        _transitionsLeft--;
        if (_transitionsLeft == 0) {
            _loadLevelOperation.allowSceneActivation = true;
            _loadLevelOperation = null;
        }
    }

    public static float getSceneLoadProgress() {
        if (_loadLevelOperation == null) {
            return 1.0f;
        }
        return _loadLevelOperation.progress;
    }
}
