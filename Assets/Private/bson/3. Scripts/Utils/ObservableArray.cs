using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class ObservableArray<T>
{
    private T[] _array;

    // �� ���� �� �߻��� �̺�Ʈ
    public event Action OnValueChanged;

    // �����ڿ��� �迭 �ʱ�ȭ
    public ObservableArray(int size)
    {
        _array = new T[size];
    }

    public ObservableArray(T[] array)
    {
        _array = new T[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            _array[i] = array[i];
        }
    }

    // �ε����� ���� �迭 ��ҿ� �����ϰ� ���� ������ �� ����
    public T this[int index]
    {
        get
        {
            return _array[index];
        }
        set
        {
            if (!_array[index].Equals(value))
            {
                _array[index] = value;
                // ���� ����Ǿ��� �� �̺�Ʈ �߻�
                OnValueChanged?.Invoke();
            }
        }
    }

    // �迭 ����
    public int Length => _array.Length;

    public object[] ToArrayString()
    {
        object[] ret = new object[_array.Length];
        
        for (int i = 0; i < _array.Length; i++)
        {
            ret[i] = _array[i].ToString();
        }

        return ret;
    }
}
