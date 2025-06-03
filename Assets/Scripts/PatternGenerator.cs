using System.Collections;
using UnityEngine;

public class PatternGenerator : MonoBehaviour
{

    void Start()
    {
        GeneratePlay(); 
        GenerateChallenge();
    }

    public bool[] GeneratePlay()
    {
        bool[] tempPattern = new bool[3];

        // Assign the boolean values in the array 
        for (int i = 0; i < tempPattern.Length; i++)
        {
            int tempValue = Random.Range(0, 2);

            // Convert 0 to False 
            if (tempValue == 0)
            {
                tempPattern[i] = false;
            }
            // Convert 1 to true
            else if (tempValue == 1)
            {
                tempPattern[i] = true;
            }
            else
            {
                Debug.LogWarning("Next is not generating 0 or 1 and has genertated: " + tempValue);
            }
        }

        Debug.Log("Play Pattern is: " + tempPattern[0] + ", " + tempPattern[1] + ", " + tempPattern[2]);
        return tempPattern;
    }

    public bool[] GenerateChallenge()
    {
        bool[] tempPattern = new bool[6];

        // Assign the boolean values in the array 
        for (int i = 0; i < tempPattern.Length; i++)
        {
            int tempValue = Random.Range(0, 2);

            // Convert 0 to False 
            if (tempValue == 0)
            {
                tempPattern[i] = false;
            }
            // Convert 1 to true
            else if (tempValue == 1)
            {
                tempPattern[i] = true;
            }
            else
            {
                Debug.LogWarning("Next is not generating 0 or 1 and has genertated: " + tempValue);
            }
        }

        Debug.Log("Challenge Pattern is: " + tempPattern[0] + ", " + tempPattern[1] + ", " + tempPattern[2] + ", " + tempPattern[3] + 
            ", " + tempPattern[4] + ", " + tempPattern[5]);
        return tempPattern;
    }
    
}
