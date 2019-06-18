using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ParamsBinding : MonoBehaviour
{
    [SerializeField]
    private GridWithParams gridWithParams;

    [SerializeField]
    private Slider widthSlider;

    [SerializeField]
    private Slider heightSlider;

    [SerializeField]
    private Button toggleParamsButton;

    [SerializeField]
    private RectTransform toggleParamsArea;

    public bool IsParamsAreaVisible
    {
        get 
        {
            return toggleParamsArea.gameObject.activeSelf;
        }
    }
    
    void Awake()
    {
        widthSlider.onValueChanged.AddListener(WidthSliderChanged);
        heightSlider.onValueChanged.AddListener(HeightSliderChanged);
        toggleParamsButton.onClick.AddListener(Toggle);
    }

    void WidthSliderChanged(float newValue)
    {
        gridWithParams.parameters.width = (int)newValue;
        gridWithParams.BuildGrid();
    }

    void HeightSliderChanged(float newValue)
    {
        gridWithParams.parameters.height = (int)newValue;
        gridWithParams.BuildGrid();
    }

    void Toggle()
    {
        bool visibility = !toggleParamsArea.gameObject.activeSelf;
        toggleParamsArea.gameObject.SetActive(visibility);

        Text toggleText = toggleParamsButton.GetComponentInChildren<Text>();
        toggleText.text = visibility ? "Params off" : "Params on";
    }
}
