using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecroKey : MonoBehaviour
{
    [SerializeField] private bool isClear;

    [SerializeField] private ButtonController buttonController;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            buttonController.ReStartScene(isClear);
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttonController.QuitEvent();
        }
    }
}
