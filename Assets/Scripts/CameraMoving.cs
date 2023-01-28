using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private Transform camTransform;
    private CameraScript camScript;
    private Transform playerTransform;

    private void Start()
    {
        camTransform = gameObject.transform;
        camScript = gameObject.GetComponent<CameraScript>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(!camScript.isActive&& (camTransform.position - playerTransform.position).magnitude>1)
        {
            camTransform.Translate((playerTransform.position- camTransform.position).normalized*Time.deltaTime*speed*1);
        }
        else if (!camScript.isActive)
        {
            camTransform.position = playerTransform.position;
        }
        if (Input.GetKey(KeyCode.W))
        {
            camScript.CamActivate();
            camTransform.Translate(Vector3.forward*Time.deltaTime* speed,Space.World);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            camScript.CamActivate();
            camTransform.Translate(Vector3.back * Time.deltaTime* speed, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            camScript.CamActivate();
            camTransform.Translate(Vector3.left * Time.deltaTime* speed, Space.World);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            camScript.CamActivate();
            camTransform.Translate(Vector3.right * Time.deltaTime* speed, Space.World);
        }
    }

}
