using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyList<T> : List<T>
{
    public System.Action onChangeList;

    public new void Add(T item)
    {
        base.Add(item);
        onChangeList?.Invoke();
    }

    public new void Remove(T item)
    {
        base.Remove(item);
        onChangeList?.Invoke();
    }

    public new void RemoveAt(int index)
    {
        base.RemoveAt(index);
        onChangeList?.Invoke();
    }
}
