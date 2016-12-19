using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Individual : MonoBehaviour {

    Vector3 desiredLocalPosition;
    float moveSpeedBase;
    List<float> moveSpeedModifiers;
    void Awake()
    {
        desiredLocalPosition = Vector3.zero;
        moveSpeedBase = 3.6f;

        moveSpeedModifiers = new List<float>();

    }
	void Start () {
	    
	}
	
	void Update () {
        if (transform.localPosition != desiredLocalPosition)
        {
            if (((transform.localPosition - desiredLocalPosition).sqrMagnitude) < GetMoveSpeed() * Time.deltaTime)
            {
                transform.localPosition = desiredLocalPosition;
            }
            else
            {
                transform.localPosition += Utility.FromTo(transform.localPosition, desiredLocalPosition).normalized * GetMoveSpeed() * Time.deltaTime;
            }
        }
	}

    public void SetDesiredLocalPosition(Vector3 lPos)
    {
        desiredLocalPosition = lPos;
    }

    public float GetMoveSpeed()
    {
        float moveSpeedMod = 1f;
        foreach (float f in moveSpeedModifiers)
        {
            moveSpeedMod *= f;
        }
        return moveSpeedBase * moveSpeedMod;
    }
}
