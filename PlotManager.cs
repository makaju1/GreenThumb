using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;
    public SpriteRenderer plant;
    BoxCollider2D plantCollider;

    int plantStage = 0;
    float timer;

    public Color availableColor = Color.green;
    public Color unavailableColor = Color.red;

    SpriteRenderer plot;

    PlantObject selectedPlant;

    FarmManager fm;

    bool isDry = true;
    public Sprite drySprite;
    public Sprite normalSprite;
    public Sprite unavaliablesprite;

    float speed = 1f;
    public bool isBought = true;

    [SerializeField] private AudioSource harverstSoundEffect, plantSoundEffect, buyPotSoundEffect, wateringcanSoundEffect, fertilizerSoundEffect;
    public ParticleSystem waterParticalEffect, furtilizerParticalEffect;
    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        fm = transform.parent.GetComponent<FarmManager>();
        plot = GetComponent<SpriteRenderer>();
        if (isBought)
        {
            plot.sprite = drySprite;
            
            
        }
        else
        {
            plot.sprite = unavaliablesprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlanted && !isDry)
        {
            timer -= speed * Time.deltaTime;
            if (timer < 0 && plantStage < selectedPlant.plantStages.Length - 1)
            {
                timer = selectedPlant.timeBtwStages;
                plantStage++;
                UpdatePlant();
                
            }
        }
    }
    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if (plantStage == selectedPlant.plantStages.Length - 1 && !fm.isPlanting && !fm.isSelecting)
            {
                Harvest();
            }
        }
        else if (fm.isPlanting && fm.selectPlant.plant.buyPrice <= fm.money && isBought)
        {
            Plant(fm.selectPlant.plant);

        }

        if (fm.isSelecting)
        {
            switch (fm.selectedTool)
            {
                case 1:
                    if (isBought) {
                        isDry = false;
                        waterParticalEffect.Play();
                        wateringcanSoundEffect.Play();
                        plot.sprite = normalSprite;
                    }
                    
                    break;
                case 2:
                    if (fm.money >= 40 && isBought) { 
                        fm.Transaction(-40);         
                        if (speed < 2) speed += 2f;
                        furtilizerParticalEffect.Play();
                        fertilizerSoundEffect.Play();

                    }
                   
                    break;
                case 3:
                    if (fm.money >= 250 && !isBought)
                    {
                        fm.Transaction(-250);
                        isBought = true;
                        plot.sprite = drySprite;
                        buyPotSoundEffect.Play();
                    }
                    break;
                default: 
                    break;
            }
        }
    }
    private void OnMouseOver()
    {
        if (fm.isPlanting)
        {
            if (isPlanted || fm.selectPlant.plant.buyPrice > fm.money || !isBought)
            {
                plot.color = unavailableColor;
            }
            else
            {
                plot.color = availableColor;
            }
        }
        if (fm.isSelecting)
        {
            switch(fm.selectedTool)
            {
                case 1:
                case 2:
                    if (isBought && fm.money>=(fm.selectedTool - 1 * 40))
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color= unavailableColor;
                    }
                    break;
                case 3:
                    if (!isBought && fm.money>=250)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                default:
                    plot.color = unavailableColor;
                    break;
            }
        }
    }

    private void OnMouseExit()
    {
        plot.color = Color.white;
    }
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        fm.Transaction(selectedPlant.sellPrice);
        isDry = true;
        plot.sprite = drySprite;
        speed = 1f;

        harverstSoundEffect.Play();
    }
    void Plant(PlantObject newPlant)
    {
        plantSoundEffect.Play();
        selectedPlant = newPlant;
        isPlanted = true;

        fm.Transaction(-selectedPlant.buyPrice);
        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }

    void UpdatePlant()
    {
        plant.sprite = selectedPlant.plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }

}
