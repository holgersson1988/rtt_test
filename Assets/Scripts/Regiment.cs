using UnityEngine;
using System.Collections;

public class Regiment : MonoBehaviour {

    /// <summary>
    /// Prefabs
    /// </summary>
    public Unit unitPrefab;
    //public GameObject emptyPrefab;
    public Unit[] units;
    
    public Transform flag;
    public Transform mainCamera;

    private int width;
    private int numberOfUnits;
    private GameObject[] unitPositions;
    

    private float unitWidth = 1.4f;
    private float unitDepth = 1.4f;

    private IEnumerator placing;

    private bool isSelected = false;

    private const int maxSize = 40;

	// Use this for initialization
	void Start () {
        numberOfUnits = maxSize;
        units = new Unit[maxSize];
        unitPositions = new GameObject[maxSize];
        width = 8;

        for (int i = 0; i < maxSize; i++) 
        {
            units[i] = Instantiate(unitPrefab, transform.position, Quaternion.identity) as Unit;
            unitPositions[i] = new GameObject();
            unitPositions[i].transform.SetParent(transform);
        }
        placing = DebugPlaceUnits(0.02f);
        StartCoroutine(placing);
	}
	
	// Update is called once per frame
	void Update () {
        flag.rotation = Quaternion.LookRotation(Utility.FromTo(new Vector3(mainCamera.position.x, flag.position.y, mainCamera.position.z), flag.position));
        UpdateLocalPositions();

    }

    public void UpdateLocalPositions()
    {
        for (int i = 0; i < maxSize; i++)
        {
            if(units[i] != null)
            {
                units[i].SetDesiredPosition(unitPositions[i].transform.position);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1.0f);

        if (unitPositions == null)
            return;
        for (int i = 0; i < unitPositions.Length; i++)
        {
            Gizmos.DrawCube(unitPositions[i].transform.position, Vector3.one);
        }
    }

    private void PositionUnit(int i)
    {
        Quaternion startRot = transform.rotation;
        transform.rotation = Quaternion.identity;

        int row = Mathf.FloorToInt(i / width);
        int rowWidth = GetWidthOfRow(row);
        int positionInRow = i % width;
        float centerOffset = (rowWidth % 2 == 0 ? 0.5f : 0f);

        Vector3 desiredLocalPosition = new Vector3(
            (-(float)rowWidth * 0.5f + (float)positionInRow + centerOffset) * unitWidth,
            0,
            (float)-row * unitDepth);

        unitPositions[i].transform.localPosition = desiredLocalPosition;

        Debug.Log("Before: " + unitPositions[i].transform.localPosition);
        transform.rotation = startRot;
        Debug.Log("After: " + unitPositions[i].transform.localPosition);

        if (units[i] != null)
        {
            units[i].SetDesiredPosition(unitPositions[i].transform.localPosition);
        }
    }

    IEnumerator DebugPlaceUnits(float duration)
    {
        int i = 0;

        while (i < numberOfUnits)
        {
            PositionUnit(i);
            i++;
            yield return new WaitForSeconds(duration);
        }
    }

    public bool IsUnitsFinishedRotating()
    {
        foreach (Unit unit in units)
        {
            if (unit.transform.position != unit.desiredPosition)
            {
                return false;
            }
        }
        return true;
    }


    public int GetDepth()
    {
        return Mathf.FloorToInt(numberOfUnits / width) + 1;
    }

    public int GetWidthOfRow(int row)
    {
        if (GetDepth() > row + 1)
        {
            return width;
        }
        else
        {
            return numberOfUnits % width;
        }
    }
}
