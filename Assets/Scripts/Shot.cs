using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : Skill
{
    [SerializeField] private GameObject bullet;

    public override void Action(GameObject owner, GameObject opposite)
    {
        if (opposite.tag != " ")
        {
            GameObject temp = Instantiate(bullet);
            temp.transform.position = transform.position;
            temp.transform.LookAt(opposite.transform);
        }
    }
}
