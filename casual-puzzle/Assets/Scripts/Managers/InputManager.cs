using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    int [] propertiesArray = new int[3];
    int ROW_NUMBER_INDEX = 0, COLUMN_NUMBER_INDEX = 1, COLOR_NUMBER_INDEX = 2; 
    ArrayList levelBoundaries;
    void Awake()
    {
        SetProperties();
    }

    private void SetProperties() {
        levelBoundaries = new ArrayList();
        propertiesArray[ROW_NUMBER_INDEX] = PlayerPrefs.GetInt("rowNumber");
        propertiesArray[COLUMN_NUMBER_INDEX] = PlayerPrefs.GetInt("columnNumber");
        propertiesArray[COLOR_NUMBER_INDEX] = PlayerPrefs.GetInt("colorNumber");
        ArrayList levels = new ArrayList{PlayerPrefs.GetInt("firstLevelLimit"), PlayerPrefs.GetInt("secondLevelLimit"), PlayerPrefs.GetInt("thirdLevelLimit")};
        CalculateLevelBoundaries(levels);
    }
    private void CalculateLevelBoundaries(ArrayList levels) {
        int startValue = Int32.MinValue;
        int endValue; 
        for (int i = 0; i < levels.Count; i++)
        {
            endValue = (int) levels[i];
            levelBoundaries.Add(new ArrayList{startValue, endValue});
            startValue = endValue += 1;
        }
         endValue = Int32.MaxValue;
        levelBoundaries.Add(new ArrayList{startValue, endValue});
    }

    public int GetRowNumber() {
       return propertiesArray[ROW_NUMBER_INDEX];
    }

    public int GetColumnNumber() {
        return propertiesArray[COLUMN_NUMBER_INDEX];
    }

    public int GetColorNumber() {
        return propertiesArray[COLOR_NUMBER_INDEX];
    }

    public ArrayList GetLevelBoundaries() {
        return levelBoundaries;
    }
}
