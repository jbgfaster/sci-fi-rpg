using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public bool stealthMode = false;


    public void SwitchStealth()
    {
        stealthMode = !stealthMode;
        Debug.Log("Switch stealth to: "+ stealthMode);
    }

}
