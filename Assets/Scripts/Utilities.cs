using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {

	public static T MinBy<T, K>(this IEnumerable<T> source, K maxValue, Func<T, K> toValue) where K : IComparable<K> {
		T best = default(T);
		K bestValue = maxValue;

		foreach (T curr in source) {
			K currValue = toValue(curr);
			if (currValue.CompareTo(bestValue) < 0) {
				best = curr;
				bestValue = currValue;
			}
		}

		return best;
	} 
}
