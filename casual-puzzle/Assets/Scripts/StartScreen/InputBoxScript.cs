using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using  UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InputBoxScript : MonoBehaviour
{
    public RectTransform errorPanel;
    public Button yourButton;
    private ErrorMessageScript errorMessageScript;

    int [] propertiesArray = new int[6];
    string [] componentNames = {"RowInput", "ColumnInput", "ColorInput", "FirstLevelInput", "SecondLevelInput", "ThirdLevelInput"};
    int [] lowerLimits = {2, 2, 1, 0, 0, 0};
    int [] higherLimits = {10, 10, 6, 100, 100, 100};
    int ROW_NUMBER_INDEX = 0, COLUMN_NUMBER_INDEX = 1, COLOR_NUMBER_INDEX = 2, FIRST_LEVEL_LIMIT_INDEX = 3, SECOND_LEVEL_LIMIT_INDEX = 4, THIRD_LEVEL_LIMIT_INDEX = 5; 
    

	void Start () {
        
        GameObject go = errorPanel.gameObject;
        errorMessageScript = errorPanel.GetComponentInChildren<ErrorMessageScript>();
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(Click);
	}

    void Click() {
        bool isInputCorrect = false;
        for(int valueIndex = 0; valueIndex < propertiesArray.Length; valueIndex++) {

            int lowerLimit = lowerLimits[valueIndex];
            int higherLimit = higherLimits[valueIndex]; 
            string name = componentNames[valueIndex];
            
            isInputCorrect = CheckInput(name, ref propertiesArray[valueIndex], lowerLimit, higherLimit, 
              "You should enter " + name + " number between " + lowerLimit + " - " + higherLimit);

            if(!isInputCorrect) {
                break;
            } 
            else if(valueIndex == FIRST_LEVEL_LIMIT_INDEX) {
                lowerLimits[SECOND_LEVEL_LIMIT_INDEX] = propertiesArray[FIRST_LEVEL_LIMIT_INDEX] + 1;
                Debug.Log(lowerLimits[SECOND_LEVEL_LIMIT_INDEX]);
            } 
            else if(valueIndex == SECOND_LEVEL_LIMIT_INDEX) {
                lowerLimits[THIRD_LEVEL_LIMIT_INDEX] = propertiesArray[SECOND_LEVEL_LIMIT_INDEX] + 1;
            }
        }
        if(isInputCorrect) {
            PlayerPrefs.SetInt("rowNumber", propertiesArray[ROW_NUMBER_INDEX]);
            PlayerPrefs.SetInt("columnNumber", propertiesArray[COLUMN_NUMBER_INDEX]);
            PlayerPrefs.SetInt("colorNumber", propertiesArray[COLOR_NUMBER_INDEX]);
            PlayerPrefs.SetInt("firstLevelLimit", propertiesArray[FIRST_LEVEL_LIMIT_INDEX]);
            PlayerPrefs.SetInt("secondLevelLimit", propertiesArray[SECOND_LEVEL_LIMIT_INDEX]);
            PlayerPrefs.SetInt("thirdLevelLimit", propertiesArray[THIRD_LEVEL_LIMIT_INDEX]);

            SceneManager.LoadScene("GameScene");
        }
    
    }

    bool CheckInput(string componentName, ref int value, int lowerLimit, int higherLimit, string errorMessage) {
        string inputValue = GetInputFieldText(componentName);
        bool result = false;
        if (Int32.TryParse(inputValue, out value))
        {
            if (lowerLimit <= value && value <= higherLimit) {
                result = true;
           } else {
               errorMessageScript.ShowErrorMessage(errorMessage);
           } 
        } else {
            errorMessageScript.ShowErrorMessage("You should enter " + componentName + " number as an integer");
        }
        return result;
    }

    string GetInputFieldText(string componentName) {
        GameObject inputFieldGo = GameObject.Find(componentName);
        InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
        return inputFieldCo.text;
    }

    


}
