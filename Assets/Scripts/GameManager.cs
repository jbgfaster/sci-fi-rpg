using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instanse;

    public GameObject chosenCharacter;

    [SerializeField] private MoveController moveController;

    void Awake()
    {
        instanse = this;
    }

    private void Start()
    {
        moveController = chosenCharacter.GetComponent<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    private void Control()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("Mouse click on tag: " + hitInfo.collider.gameObject.tag);
                if(hitInfo.collider.gameObject.tag == "Teammate"|| hitInfo.collider.gameObject.tag == "Player")
                {
                    chosenCharacter =hitInfo.collider.gameObject;
                    Start();
                    return;
                }
                if(hitInfo.collider.gameObject.tag=="Ground")
                {
                    chosenCharacter.GetComponent<CameraScript>().CamActivate();//��������� ������ ������ �������� �����, ����� �������� ���������� �������
                    moveController.MoveTo(GetClickPosition());
                    return;
                }
                if(hitInfo.collider.CompareTag("Enemy"))
                {
                    chosenCharacter.GetComponent<Skills>().skill1.Action(chosenCharacter, hitInfo.collider.gameObject);
                }
                if (hitInfo.collider.CompareTag("Door"))
                {
                    hitInfo.collider.gameObject.SendMessage("Open");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            moveController.RunSwitch();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Stealth Button");
            chosenCharacter.GetComponent<CharacterStats>().SwitchStealth();
        }
    }

    public void buttonStealth()
    {
        Debug.Log("Stealth Button");
        chosenCharacter.GetComponent<CharacterStats>().SwitchStealth(); 
    }
    public void buttonRun()
    {
        moveController.RunSwitch();
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
