using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectPlant;
    public bool isPlanting = false;
    public int money = 100;
    public Text moneyTxt;

    public Color buyColor = Color.green;
    public Color cancelColor = Color.red;

    public bool isSelecting = false;
    public int selectedTool = 0;
    //1 - Water, 2- fertilzer 3- Buy plot

    public Image[] buttonsImg;
    public Sprite normalButton;
    public Sprite selectedButton;

    // Start is called before the first frame update
    void Start()
    {
        moneyTxt.text = "Rs"+ money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectPlant(PlantItem newPlant)
    {
        if (selectPlant == newPlant)
        {
            CheckSelection();
        }
        else
        {
           CheckSelection();
            selectPlant = newPlant;
            selectPlant.btnImage.color = cancelColor;
            selectPlant.btnTxt.text = "Cancel";
            isPlanting = true;
        }
    }

    public void selectTool(int toolNumber)
    {
        if (toolNumber == selectedTool)
        {
            CheckSelection();
        }
        else
        { 
            CheckSelection();
            isSelecting = true;
            selectedTool = toolNumber;
            buttonsImg[toolNumber - 1].sprite = selectedButton;
        }

    }
    void CheckSelection()
    {
        if (isPlanting)
        {
            isPlanting = false;

            if (selectPlant != null)
            {
                selectPlant.btnImage.color = buyColor;
                selectPlant.btnTxt.text = "Buy";
                selectPlant = null;
            }
        }
        if (isSelecting)
        {
            if (selectedTool > 0)
            {
                buttonsImg[selectedTool - 1].sprite = normalButton;
            }
            isSelecting = false;
            selectedTool = 0;
        }
    }
    public void Transaction (int value)
    {
        money += value;
        moneyTxt.text = "Rs:" + money;
    }
}