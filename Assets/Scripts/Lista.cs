using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lista<T>
{
    private T[] array;
    private int length;
    private int capacity;

    public Lista()
    {
        capacity = 4; 
        array = new T[capacity];
        length = 0;
    }

    public void Add(T item)
    {
        if (length == capacity)
        {
            ResizeArray();
        }
        array[length] = item;
        length = length + 1;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= length)
        {
            throw new System.IndexOutOfRangeException();
        }
        return array[index];
    }

    public int Length
    {
        get { return length; }
    }

    private void ResizeArray()
    {
        capacity *= 2; 
        T[] newArray = new T[capacity];
        for (int i = 0; i < length; i++)
        {
            newArray[i] = array[i];
        }
        array = newArray;
    }
}
