using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingByClick : MonoBehaviour
{
    [SerializeField] MoveController moveController;

    private void Start()
    {
        moveController = GetComponent<MoveController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameObject.GetComponent<CameraScript>().CamActivate();//активация камеры должна работать иначе, через менеджер управления камерой

            moveController.MoveTo(GetClickPosition());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            moveController.RunSwitch();
        }
    }

    private Vector3 GetClickPosition()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);            
        }
        return gameObject.transform.position;
    }
}
