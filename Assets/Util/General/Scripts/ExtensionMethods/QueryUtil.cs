using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class QueryUtil {

  public static T Smallest<T>(this IEnumerable<T> values, Func<T, float> toFloat) {
    T retValue = default(T);
    float currMin = float.MaxValue;

    foreach (T value in values) {
      float thisMin = toFloat(value);
      if (thisMin < currMin) {
        retValue = value;
        currMin = thisMin;
      }
    }

    return retValue;
  }

  public static T selectRandom<T>(this IEnumerable<T> ts) {
    var arr = ts.ToArray();
    return arr[UnityEngine.Random.Range(0, arr.Length)];
  }

  public static float median(this IEnumerable<float> numbers) {
    return median(numbers, n => n);
  }

  public static T median<T>(this IEnumerable<T> values, Func<T, float> conversion) {
    List<T> list = values.ToList();
    list.Sort((a, b) => {
      float floatA = conversion(a);
      float floatB = conversion(b);
      if (floatA > floatB) return 1;
      if (floatA < floatB) return -1;
      return 0;
    });
    return list[list.Count / 2];
  }

  public static IEnumerable<int> range(int start, int end) {
    for (int i = start; i < end; i++) {
      yield return i;
    }
  }

  public static IEnumerable<Tuple<T, T>> getConnected<T>(this IEnumerable<T> values) {
    IEnumerator<T> enumerator = values.GetEnumerator();
    if (!enumerator.MoveNext()) {
      yield break;
    }

    T a = enumerator.Current;
    while (enumerator.MoveNext()) {
      T b = enumerator.Current;
      yield return new Tuple<T, T>(a, b);
      a = b;
    }
  }

  public static IEnumerable<Tuple<T, T>> getCyclic<T>(this IEnumerable<T> values) {
    IEnumerator<T> enumerator = values.GetEnumerator();
    if (!enumerator.MoveNext()) {
      yield break;
    }

    T first = enumerator.Current;
    T a = first;
    while (enumerator.MoveNext()) {
      T b = enumerator.Current;
      yield return new Tuple<T, T>(a, b);
      a = b;
    }

    yield return new Tuple<T, T>(a, first);
  }
}
