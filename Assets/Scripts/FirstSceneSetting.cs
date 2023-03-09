using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneSetting : MonoBehaviour
{
   private void Awake()
    {
        PlayerPrefs.SetInt("Round", 0);
    }
}
