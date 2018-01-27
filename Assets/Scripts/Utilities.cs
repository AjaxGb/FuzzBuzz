using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {

	public static T MinBy<T, K>(this IEnumerable<T> source, Func<T, K> toValue) where K : IComparable<K> {

		IEnumerator<T> enumerator = source.GetEnumerator();
		if (!enumerator.MoveNext()) return default(T);

		T best = enumerator.Current;
		K bestValue = toValue(best);

		while (enumerator.MoveNext()) {
			K currValue = toValue(enumerator.Current);
			if (currValue.CompareTo(bestValue) < 0) {
				best = enumerator.Current;
				bestValue = currValue;
			}
		}

		return best;
	} 
}
