using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SwiperWatcher : MonoBehaviour
{
    private int _swipersIn = 0;

    public event Action FirstSwiperEnter;
    public event Action LastSwiperLeft;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_swipersIn == 0)
            FirstSwiperEnter?.Invoke();

        _swipersIn++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _swipersIn--;

        if (_swipersIn == 0)
            LastSwiperLeft?.Invoke();
    }
}
