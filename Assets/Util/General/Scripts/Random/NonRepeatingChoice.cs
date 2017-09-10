using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NonRepeatingChoice {

    private int[] _unchosenValues;
    private int _size;

    private int[] _upperValues;

    private int[] _ret;

    public NonRepeatingChoice(params int[] upperValues) {
        _upperValues = upperValues;

        int size = 1;
        foreach (int v in _upperValues) {
            size *= v;
        }
        _unchosenValues = new int[size * _upperValues.Length];

        _ret = new int[_upperValues.Length];

        reset();
    }

    public void reset() {
        int[] value = new int[_upperValues.Length];
        value.Initialize();

        _size = 0;

        do {
            Array.Copy(value, 0, _unchosenValues, _size * value.Length, value.Length);
            _size++;
        } while (tryIncrement(ref value));
    }

    private bool tryIncrement(ref int[] value) {
        int offset = 0;
        while (true) {
            value[offset]++;
            if (value[offset] == _upperValues[offset]) {
                value[offset] = 0;
                offset++;

                if (offset == value.Length) {
                    return false;
                }
            } else {
                return true;
            }
        }
    }

    public bool hasNext() {
        return _size != 0;
    }

    public int[] next() {
        if (_size == 0) {
            reset();
        }

        int index = UnityEngine.Random.Range(0, _size) * _upperValues.Length;
        Array.Copy(_unchosenValues, index, _ret, 0, _upperValues.Length);

        _size--;
        if (_size != 0) {
            Array.Copy(_unchosenValues, _size * _upperValues.Length, _unchosenValues, index, _upperValues.Length);
        }

        return _ret;
    }
}
