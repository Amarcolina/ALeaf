using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class ArrayUtil {

  public static void fill<T>(this T[] t, T v) {
    for (int i = 0; i < t.Length; i++) {
      t[i] = v;
    }
  }

  public static void fill<T>(this T[] t, System.Func<T> f) {
    for (int i = 0; i < t.Length; i++) {
      t[i] = f();
    }
  }

  public static void fill<T>(this T[] t, System.Func<int, T> f) {
    for (int i = 0; i < t.Length; i++) {
      t[i] = f(i);
    }
  }

  public static void fill<T>(this T[,] t, T v) {
    for (int i = 0; i < t.GetLength(0); i++) {
      for (int j = 0; j < t.GetLength(1); j++) {
        t[i, j] = v;
      }
    }
  }

  public static void fill<T>(this T[,] t, Func<T> f) {
    for (int i = 0; i < t.GetLength(0); i++) {
      for (int j = 0; j < t.GetLength(1); j++) {
        t[i, j] = f();
      }
    }
  }

  public static void fill<T>(this T[,] t, Func<int, int, T> f) {
    for (int i = 0; i < t.GetLength(0); i++) {
      for (int j = 0; j < t.GetLength(1); j++) {
        t[i, j] = f(i, j);
      }
    }
  }

  public static void fill<T>(this T[, ,] t, T v) {
    for (int i = 0; i < t.GetLength(0); i++) {
      for (int j = 0; j < t.GetLength(1); j++) {
        for (int k = 0; k < t.GetLength(2); k++) {
          t[i, j, k] = v;
        }
      }
    }
  }

  public static void fill<T>(this T[, ,] t, Func<T> f) {
    for (int i = 0; i < t.GetLength(0); i++) {
      for (int j = 0; j < t.GetLength(1); j++) {
        for (int k = 0; k < t.GetLength(2); k++) {
          t[i, j, k] = f();
        }
      }
    }
  }

  public static void fill<T>(this T[, ,] t, Func<int, int, int, T> f) {
    for (int i = 0; i < t.GetLength(0); i++) {
      for (int j = 0; j < t.GetLength(1); j++) {
        for (int k = 0; k < t.GetLength(2); k++) {
          t[i, j, k] = f(i, j, k);
        }
      }
    }
  }

  public static void RemoveLast<T>(this List<T> list) {
    list.RemoveAt(list.Count - 1);
  }
}
