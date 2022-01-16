using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using  UnityEngine.UI;
public class ErrorMessageScript : MonoBehaviour
{
    public RectTransform errorPanel;
    public Button errorPanelCloseButton;
    // Start is called before the first frame update
    void Start()
    {
        errorPanelCloseButton = errorPanel.GetComponentInChildren<Button>();
        errorPanelCloseButton.onClick.AddListener(Click);
    }

    // Update is called once per frame
     void Click() {
        errorPanel.gameObject.SetActive(false);
        //SetErrorMessage("deneme");
    }

    public void ShowErrorMessage(string errorMessage) {
        Text errorMessageText = errorPanel.GetComponentInChildren<Text>();
        errorMessageText.text = errorMessage;
        errorPanel.gameObject.SetActive(true);
    }
}
