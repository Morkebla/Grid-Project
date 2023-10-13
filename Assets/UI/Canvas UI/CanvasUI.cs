using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    Canvas canvas;
    bool canvasSwitch = true;
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        canvas.enabled = false;
    }
    private void Update()
    {
        if (canvas != null && Application.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                canvas.enabled = !canvas.enabled;
            }
        }
    }
}
