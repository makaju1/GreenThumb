using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlantItem : MonoBehaviour
{
    public PlantObject plant;
    public Text nameTxt;
    public Text priceTxt;
    public Image icon;

    public Image btnImage;
    public Text btnTxt;

    FarmManager fm;
    // Start is called before the first frame update
    void Start()
    {
        fm = FindObjectOfType<FarmManager>();
        InitializeUI();
        Tips();
    }
     
    public void BuyPlant()
    {
       fm.SelectPlant(this);
    }

    void InitializeUI()
    {
        nameTxt.text = plant.plantName;
        priceTxt.text = "Rs:" + plant.buyPrice;
        icon.sprite = plant.icon;

    }
    void Tips()
    {
        string Tip = "Plant Name: " + plant.plantName + "\nSelling Price: Rs "+ plant.sellPrice + "\nGrowing Time: " + plant.timeBtwStages +" Sec" + "\nFacts:\n"+plant.funFacts;

        HoverTip tip = GetComponent<HoverTip>();
        if (tip)
        {
            tip.SetMessage(Tip);
        }
    }
}
