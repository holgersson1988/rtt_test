using UnityEngine;
using System.Collections;

public class Regiment : MonoBehaviour {

    /// <summary>
    /// Prefabs
    /// </summary>
    public Individual individualPrefab;
    public Individual[] individuals;
    public Transform flag;
    public Transform mainCamera;

    private int width;
    private int numberOfUnits;

    private float unitWidth = 1.4f;
    private float unitDepth = 1.4f;

    private IEnumerator placing;

    private bool selected = false;

    private const int maxSize = 40;
	// Use this for initialization
	void Start () {
        numberOfUnits = maxSize;
        individuals = new Individual[maxSize];
        width = 8;

        for (int i = 0; i < maxSize; i++) 
        {
            individuals[i] = Instantiate(individualPrefab, transform.position, Quaternion.identity) as Individual;
            individuals[i].transform.SetParent(transform);
        }
        placing = DebugPlaceUnits(0.02f);
        StartCoroutine(placing);
        //PositionUnits();
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 lookPoint = flag.position - mainCamera.position;
        //lookPoint.y = mainCamera.position.y;
        flag.rotation = Quaternion.LookRotation(Utility.FromTo(new Vector3(mainCamera.position.x, (flag.position.y + mainCamera.position.y)/2f, mainCamera.position.z), flag.position));
        //flag.LookAt(mainCamera.position);
        //transform.Rotate(Vector3.up * 2f);
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1.0f);
    }

    private void PositionUnit(int i)
    {
        Quaternion startRot = transform.rotation;
        transform.rotation = Quaternion.identity;
        //for (int i = 0; i < individuals.Length; i++)
        {
            int row = Mathf.FloorToInt(i / width);
            int rowWidth = GetWidthOfRow(row);
            int positionInRow = i % width;
            Debug.Log("Row/RowWidth/PosInRow: " + row + "/" + rowWidth + "/" + positionInRow);
            float centerOffset = (rowWidth % 2 == 0 ? 0.5f : 0f);
            Individual ind = individuals[i];
            ind.SetDesiredLocalPosition(new Vector3(
                (-(float)rowWidth * 0.5f + (float)positionInRow + centerOffset) * unitWidth,
                0,
                (float)row * unitDepth));
            //ind.transform.localPosition = new Vector3(
            //    (-(float)rowWidth * 0.5f + (float)positionInRow + centerOffset) * unitSpacing,
            //    0,
            //    (float)row * unitSpacing);
        }


        transform.rotation = startRot;
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
            Debug.Log("Less than width");
            return numberOfUnits % width;
        }
    }
}
