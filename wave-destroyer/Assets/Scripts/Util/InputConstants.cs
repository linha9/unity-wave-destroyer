﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputConstants : MonoBehaviour
{
    public static InputConstants Instance { get; private set; }

    private void Awake()
    {
        if (Instance = null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
    }
}
