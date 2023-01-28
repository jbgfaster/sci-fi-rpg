using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private void Start()
    {
        door.SetActive(true);
    }
    public void Open()
    {
        door.SetActive(!door.activeSelf);
    }
}
