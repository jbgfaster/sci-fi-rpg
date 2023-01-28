using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleMask;

    private bool inSight = false;
    private Transform currentTarget;
    private float timer = 0f;

    void Update()
    {
          currentTarget = GetTarget(gameObject, "Player");
        if (currentTarget == null)
        { 
            return; 
        }
        Vector3 targetDirection = currentTarget.position - transform.position;
        if (!Physics.Raycast(transform.position, targetDirection, 5.0f, obstacleMask)) 
        {
            inSight = true;
        }
        else
        {
            inSight = false;
        }
        Attack();
    }

    void Attack()
    {
        timer -= Time.deltaTime;
        if(inSight && timer<0)
        {
            timer = 1.0f;
            gameObject.GetComponent<Skills>().skill1.Action(gameObject,currentTarget.gameObject);
        }
    }

    public Transform GetTarget(GameObject soldier, string tagOpponent)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tagOpponent);
        if (targets.Length < 1)
        {
            return null;
        }
        float nearTarget = 9999;
        int index = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            float targetDistance = (targets[i].transform.position - soldier.transform.position).magnitude;
            if (targetDistance < nearTarget)
            {
                nearTarget = targetDistance;
                index = i;
            }
        }
        return targets[index].transform;
    }

}
