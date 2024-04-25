using System;
using System.Collections;
using System.Collections.Generic;

namespace FortniteShopSort;

public sealed class BinaryHeapMin<T> : IEnumerable<T>, IEnumerable where T : IComparable<T>
{
    private readonly List<T> _internalList = new();

    public int Count => _internalList.Count;

    public override string ToString() =>
        string.Join(',', _internalList);

    public void Push(T val)
    {
        _internalList.Add(val);
        SiftUp();
    }

    public T Peek() =>
        _internalList[0];

    public T Pop()
    {
        int n = _internalList.Count - 1;
        (T val, _internalList[0]) = (_internalList[0], _internalList[n]);

        _internalList.RemoveAt(n);
        SiftDown();

        return val;
    }

    private void SiftUp()
    {
        int n = _internalList.Count - 1;
        int p = Parent(n);

        while (n > 0 && _internalList[n].CompareTo(_internalList[p]) < 0)
        {
            (_internalList[n], _internalList[p]) = (_internalList[p], _internalList[n]);

            n = p;
            p = Parent(n);
        }
    }

    private void SiftDown()
    {
        int n = 0;
        int l = ChildLeft(n);
        int r = ChildRight(n);

        while (l < _internalList.Count)
        {
            int s = r < _internalList.Count && _internalList[r].CompareTo(_internalList[l]) < 0
                ? r
                : l;

            if (_internalList[s].CompareTo(_internalList[n]) >= 0)
                break;

            (_internalList[n], _internalList[s]) = (_internalList[s], _internalList[n]);

            n = s;
            l = ChildLeft(n);
            r = ChildRight(n);
        }
    }

    private static int Parent(int n) =>
        (n - 1) / 2;

    private static int ChildLeft(int n) =>
        2 * n + 1;

    private static int ChildRight(int n) =>
        2 * n + 2;

    public IEnumerator<T> GetEnumerator()
    {
        while (Count > 0)
        {
            yield return Pop();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}