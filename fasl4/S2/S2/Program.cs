using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<(T item, int priority)> elements = new List<(T, int)>();

    public void Enqueue(T item, int priority)
    {
        elements.Add((item, priority));
    }

    public T Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException("The priority queue is empty.");

        int highestPriorityIndex = 0;
        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].priority < elements[highestPriorityIndex].priority)
            {
                highestPriorityIndex = i;
            }
        }

        T item = elements[highestPriorityIndex].item;
        elements.RemoveAt(highestPriorityIndex);
        return item;
    }

    public bool IsEmpty()
    {
        return elements.Count == 0;
    }

    public int Count()
    {
        return elements.Count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        PriorityQueue<string> pq = new PriorityQueue<string>();

        pq.Enqueue("Low priority task", 3);
        pq.Enqueue("Medium priority task", 2);
        pq.Enqueue("High priority task", 1);

        while (!pq.IsEmpty())
        {
            Console.WriteLine(pq.Dequeue());
        }
    }
}