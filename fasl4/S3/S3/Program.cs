using System;

public class DualQueue
{
    private int[] data;
    private int maxSize;
    private int front1, rear1;
    private int front2, rear2;
    private int count;

    public DualQueue(int size)
    {
        maxSize = size;
        data = new int[maxSize];
        front1 = rear1 = -1;
        front2 = rear2 = size;
        count = 0;
    }

    public bool Enqueue1(int value)
    {
        if (count >= maxSize)
        {
            Console.WriteLine("Cannot add a new element to the first row. The capacity is full.");
            return false;
        }

        if (front1 == -1)
        {
            front1 = 0;
            rear1 = 0;
        }
        else
        {
            rear1 = (rear1 + 1) % maxSize;
        }

        data[rear1] = value;
        count++;
        return true;
    }

    public bool Enqueue2(int value)
    {
        if (count >= maxSize)
        {
            Console.WriteLine("Cannot add a new element to the second queue. The capacity is full.");
            return false;
        }

        if (front2 == maxSize)
        {
            front2 = maxSize - 1;
            rear2 = maxSize - 1;
        }
        else
        {
            rear2 = (rear2 - 1 + maxSize) % maxSize;
        }

        data[rear2] = value;
        count++;
        return true;
    }

    public int Dequeue1()
    {
        if (front1 == -1)
        {
            Console.WriteLine("The first queue is empty.");
            return -1;
        }

        int value = data[front1];
        front1++;

        if (front1 > rear1)
        {
            front1 = rear1 = -1;
        }

        count--;
        return value;
    }

    public int Dequeue2()
    {
        if (front2 == maxSize)
        {
            Console.WriteLine("The second queue is empty.");
            return -1;
        }

        int value = data[front2];
        front2++;

        if (front2 > rear2)
        {
            front2 = maxSize;
        }

        count--;
        return value;
    }
}

class Program
{
    static void Main(string[] args)
    {
        DualQueue dualQueue = new DualQueue(100);

        dualQueue.Enqueue1(10);
        dualQueue.Enqueue1(20);

        Console.WriteLine(dualQueue.Dequeue1());
        Console.WriteLine(dualQueue.Dequeue1());

        dualQueue.Enqueue2(30);
        dualQueue.Enqueue2(40);

        Console.WriteLine(dualQueue.Dequeue2());
        Console.WriteLine(dualQueue.Dequeue2());
    }
}