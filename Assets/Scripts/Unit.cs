using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    public Vector3 desiredPosition { get; private set; }
    float moveSpeedBase;
    List<float> moveSpeedModifiers;
    void Awake()
    {
        desiredPosition = Vector3.zero;
        moveSpeedBase = 3.6f;

        moveSpeedModifiers = new List<float>();

    }
	void Start () {
	    
	}
	
	void Update () {
        Utility.DebugLogIfSelected(gameObject, "DesiredPos: " + desiredPosition);
        if (transform.position != desiredPosition)
        {
           
            if (((transform.localPosition - desiredPosition).sqrMagnitude) < GetMoveSpeed() * Time.deltaTime)
            {
                transform.localPosition = desiredPosition;
            }
            else
            {
                transform.localPosition += Utility.FromTo(transform.localPosition, desiredPosition).normalized * GetMoveSpeed() * Time.deltaTime;
            }
        }
	}

    public void SetDesiredPosition(Vector3 pos)
    {
        desiredPosition = pos;
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
