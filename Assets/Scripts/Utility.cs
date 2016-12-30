using UnityEngine;
using System.Collections;
using UnityEditor;


public static class Utility 
{

    // Colors
    public static Color cGreen = new Color(0.56f, 0.96f, 0f, 1f);
    public static Color cYellow = new Color(1f, 0.88f, 0f, 0.24f);
    public static Color cOrange = new Color(1f, 0.5f, 0f, 1f);
    public static Color cRed = new Color(0.95f, 0.15f, 0.12f, 1f);
    public static Color cBlue = new Color(0.2f, 0.6f, 1f, 1f);
    public static Color cWhite = new Color(0.9f, 0.9f, 0.9f, 1f);

    private static int m_frameCounter = 0;
    private static float m_timeCounter = 0.0f;
    private static float m_lastFramerate = 0.0f;
    private static float m_refreshTime = 0.5f;


    private static float CountFps()
    {
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which would make no sense.
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }
        return m_lastFramerate;
    }


    /// <summary>
    /// Returns a random float in range [-1, 1] with normal distribution
    /// </summary>
    /// <returns>Returns a random float in range [-1, 1] with normal distribution</returns>
    public static float RandomNormalDistribution()
	{
		float n1 = Random.Range(-1f, 1f);
		float n2 = Random.Range(-1f, 1f);
		return (n1 * n2);
	}

	/// <summary>
	/// Returns the Vector3 that points from "from" to "to"
	/// </summary>
	/// <returns>The to.</returns>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	public static Vector3 FromTo(Vector3 from, Vector3 to)
	{
		return to - from;
	}

	/// <summary>
	/// Returns the rotation around the y-axis of a Vector3
	/// </summary>
	/// <returns>The angle in degrees around the y-axis of the vector.</returns>
	/// <param name="vec">Vec.</param>
	public static float GetYAngleFromVector(Vector3 vec)
	{
		return Mathf.Atan2(vec.x, vec.z) * 180f / Mathf.PI;
	}

    /// <summary>
    /// Returns a random point (x, 0, z) inside the given radius
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector3 GetRandomPointInRadius(float radius)
    {
        //Random rand = new Random();
        Vector3 newPos = Vector3.zero;
        float a = 2 * Mathf.PI * Random.Range(0f,1f);
        float r = Mathf.Sqrt(Random.Range(0f, 1f));
        float x = radius * r * Mathf.Cos(a);
        float z = radius * r * Mathf.Sin(a);
        newPos.x = x;
        newPos.z = z;
        return newPos;
    }

    public static Vector3 GetPointOnCircleFromAngle(float angle)
    {
        float x = Mathf.Cos(0f) * Mathf.Sin(angle);
        float y = Mathf.Sin(0f) * Mathf.Sin(angle);
        float z = Mathf.Cos(angle);

        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Clamps the incoming radian angle to [-PI, PI]
    /// </summary>
    /// <param name="rad"></param>
    /// <returns></returns>
    public static float ClampAngleRad(float rad)
    {
        if (rad < -Mathf.PI)
        {
            do
            {
                rad += 2 * Mathf.PI;
            } while (rad < -Mathf.PI);
            return rad;
        }
        else if (rad > Mathf.PI)
        {
            do
            {
                rad -= 2 * Mathf.PI;
            } while (rad > Mathf.PI);
            return rad;
        }
        else
        {
            return rad;
        }
    }


    public static float AngleDifference(float target, float source)
    {
        float a = target - source;
        a = (a + Mathf.PI) % (2.0f * Mathf.PI) - Mathf.PI;
        return a;
    }

    /// <summary>
    /// Returns true (dividend/devisor) of the time
    /// </summary>
    /// <param name="dividend"></param>
    /// <param name="devisor"></param>
    /// <returns></returns>
    public static bool RandomChance(int dividend, int devisor)
    {
        return (Random.Range(0, devisor) < dividend);
    }

    public static float Sinerp(float t)
    {
        return Mathf.Sin(t * Mathf.PI * 0.5f);
    }

    public static float Coserp(float t)
    {
        return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
    }

    public static float Exp(float t)
    {
        return t * t;
    }

    public static float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }

    public static float SmootherStep(float t)
    {
        return t * t * t * (t * (6f * t - 15f) + 10f);
    }

    // Debug
    public static void DebugLogIfSelected(GameObject go, string str)
    {
        if (Selection.activeGameObject == go)
        {
            Debug.Log(str);
        }
    }
    
}
