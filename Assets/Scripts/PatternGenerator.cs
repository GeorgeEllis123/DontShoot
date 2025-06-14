using System.Collections.Generic;
using UnityEngine;

public class PatternGenerator : MonoBehaviour
{
    [System.Serializable]
    public class EasyPattern
    {
        public bool[] pattern = new bool[6];
    }

    [SerializeField] private List<EasyPattern> easyPatterns; 
    void Start()
    {
        //GenerateChallenge();
    }

    public bool[] GenerateEasy()
    {
        int patternSize = easyPatterns.Count;
        int randomIndex = Random.Range(0, patternSize);

        return easyPatterns[randomIndex].pattern;
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

        //Debug.Log("Play Pattern is: " + tempPattern[0] + ", " + tempPattern[1] + ", " + tempPattern[2]);

        bool[] finalPattern = new bool[6];
        tempPattern.CopyTo(finalPattern, 0);
        tempPattern.CopyTo(finalPattern, 3);

        return finalPattern;
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

        //Debug.Log("Challenge Pattern is: " + tempPattern[0] + ", " + tempPattern[1] + ", " + tempPattern[2] + ", " + tempPattern[3] + 
        //    ", " + tempPattern[4] + ", " + tempPattern[5]);
        return tempPattern;
    }
    
}
