using System.Collections.Generic;

public sealed class ConcurrentQueue<T>
{
    readonly object syncRoot = new object();

    readonly Queue<T> queue = new Queue<T>();

    public object SyncRoot
    {
        get
        {
            return syncRoot;
        }
    }

    public int Count
    {
        get
        {
            lock (syncRoot)
                return queue.Count;
        }
    }

    public bool IsEmpty
    {
        get
        {
            lock (syncRoot)
                return queue.Count == 0;
        }
    }

    public T Dequeue()
    {
        lock (syncRoot)
            return queue.Dequeue();
    }

    public void Enqueue(T item)
    {
        lock (syncRoot)
            queue.Enqueue(item);
    }

    public void Clear()
    {
        lock (syncRoot)
            queue.Clear();
    }
}