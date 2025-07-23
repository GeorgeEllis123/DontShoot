using System.Collections.Generic;
using UnityEngine;

public class NewPatternGenerator : MonoBehaviour
{
    [System.Serializable]
    public class PatternLot
    {
        public bool[] pattern = new bool[6];
    }
    public bool isThisDayOne = false;
    // Up to 3 different pools of patterns to pick from
    [SerializeField] private List<PatternLot> lot1;
    [SerializeField] private List<PatternLot> lot2;
    [SerializeField] private List<PatternLot> lot3;
    private int lastPatternIndex = -1;

    public bool[] GetPattern(int lot) // picks from the given lot, for random pass anything != 1-3
    {
        List<PatternLot> chosenLot = new List<PatternLot>();
        if (lot == 1)
        {
            chosenLot = lot1;
        }
        else if (lot == 2)
        {
            chosenLot = lot2;
        }
        else if (lot == 3)
        {
            chosenLot = lot3;
        }

        if (chosenLot.Count > 0)
        {
            int patternSize = chosenLot.Count;
            int randomIndex = Random.Range(0, patternSize);
            if (!isThisDayOne)
            {
                while (randomIndex == lastPatternIndex)
                {
                    randomIndex = Random.Range(0, patternSize);
                }
                lastPatternIndex = randomIndex;
            }

            return chosenLot[randomIndex].pattern;
        } else {
            Debug.Log("Generated a random pattern. Lot " + lot + " is either DNE or empty");
            return GenerateRandom();
        }
    }

    private bool[] GenerateRandom()
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

        return tempPattern;
    }
}
