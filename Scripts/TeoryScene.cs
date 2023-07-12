using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class TeoryScene : MonoBehaviour
{

    public GameObject contentOrgan;
    public GameObject contentTeory;

    public GameObject contentTextTeory;
    public GameObject ImageTeory;
    public GameObject PanelImageTeory;
    public TextMeshProUGUI TextTeory;

    public GameObject Humen;

    AccountService accountService;
    DataControlerForTeory controllerTeory;
    int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        accountService = new AccountService();
        controllerTeory = new DataControlerForTeory();
    }

    public void onGetOrgansButtonClick(int IdSystem)
    {
        Debug.Log("id выбраной системы органов = "+ IdSystem);
        controllerTeory.GetTableOrgans(contentOrgan.GetComponent<GridLayoutGroup>(), contentOrgan, IdSystem, contentTeory.GetComponent<GridLayoutGroup>(),
            contentTeory, ImageTeory, contentTextTeory, TextTeory, PanelImageTeory, Humen);
    }

    public void NextImage()
    {
        Image image = ImageTeory.GetComponent<Image>();

        string buttonName = accountService.GetTeoryByName(DataControlerForTeory.savedName).Image;
        if (buttonName != null)
        {
            PanelImageTeory.SetActive(true);
            if (i+1 < DataControlerForTeory.buttonNamesList.Length)
            {
                i++;
                Sprite sprite = Resources.Load<Sprite>("Image/" + DataControlerForTeory.buttonNamesList[i]);

                if (sprite != null)
                {
                    // Устанавливаем загруженный спрайт для компонента Image
                    image.sprite = sprite;
                }
                else
                {
                    Debug.Log("Sprite not found: " + buttonName);
                }
                
            }
        }
        else
        {
            Debug.Log("This teory material is not have image.");
            PanelImageTeory.SetActive(false);
        }
    }

    public void BackImage()
    {
        Image image = ImageTeory.GetComponent<Image>();

        string buttonName = accountService.GetTeoryByName(DataControlerForTeory.savedName).Image;
        if (buttonName != null)
        {
            if (i > 0)
            {
                PanelImageTeory.SetActive(true);
                i--;
               
                Sprite sprite = Resources.Load<Sprite>("Image/" + DataControlerForTeory.buttonNamesList[i]);

                if (sprite != null)
                {
                    // Устанавливаем загруженный спрайт для компонента Image
                    image.sprite = sprite;
                }
                else
                {
                    Debug.Log("Sprite not found: " + buttonName);
                }

            }
        }
        else
        {
            Debug.Log("This teory material is not have image.");
            PanelImageTeory.SetActive(false);
        }
    }

}
