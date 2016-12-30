using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour {

    public Vector3 target;
    public TargetIndicator targetInst;


    private TargetIndicator myTargetInst;
    private Regiment regiment;
    NavMeshAgent agent;
    Camera mainCamera;
    int myCorner = 0;
    bool isMoving = false;
    bool isRotating = false;
    Vector3 startPosition = Vector3.zero;

    float rotateSpeed = 2f;
    float moveSpeed = 4f;
    

	void Start ()
    {
        target = transform.position;
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        myTargetInst = Instantiate(targetInst, transform.position, Quaternion.identity) as TargetIndicator;
        myTargetInst.enabled = false;

        regiment = GetComponent<Regiment>();
	}

	
	void Update ()
    {
        agent.SetDestination(target);
        Debug.DrawLine(transform.position, target);

        RaycastHit hit;
        if (Input.GetMouseButton(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Terrain")
                {
                    target = hit.point;
                    if (agent.SetDestination(target))
                    {
                        myTargetInst.enabled = true;
                        myTargetInst.transform.position = hit.point;
                        myCorner = 1;
                        startPosition = transform.position;
                        agent.speed = 0;
                        isMoving = true;
                        //Debug.Log("Corners: " + agent.path.corners.Length);
                    }
                }
            }
        }

        if (isMoving)
        {
            Vector3[] corners = agent.path.corners;

            // Find the vector pointing from our position to the target
            if (corners.Length > myCorner)
            {
                Vector3 dir = (new Vector3(corners[myCorner].x, transform.position.y, corners[myCorner].z) - transform.position).normalized;

                // Create the rotation we need to be in to look at the target
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                if (transform.rotation.eulerAngles != lookRotation.eulerAngles)
                {
                    transform.rotation = lookRotation;
                    regiment.UpdateLocalPositions();
                    isRotating = true;
                }
                

                if (isRotating)
                {
                    if (regiment.IsUnitsFinishedRotating())
                    {
                        isRotating = false;
                    }
                }
                else
                {
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, corners[myCorner]) <= moveSpeed * Time.deltaTime)
                    {
                        transform.position = corners[myCorner];
                    }
                }
                
            }
        }

        
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (agent)
        {
            for (int i = 0; i < agent.path.corners.Length; i++)
            {
                Gizmos.DrawSphere(agent.path.corners[i], 2f);
            }
        }
    }
}
