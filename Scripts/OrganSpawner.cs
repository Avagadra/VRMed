using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;


public class OrganSpawner : MonoBehaviour
{

    public GameObject[] organPrefabs; // массив префабов органов
    public static GameObject[] organs; // массив созданных игровых объектов
    public GameObject HumenOrgans;

    void Start()
    {
        organs = new GameObject[organPrefabs.Length]; // инициализируем массив

        for (int i = 0; i < organPrefabs.Length; i++)
        {

            organs[i] = Instantiate(organPrefabs[i]); // создаем игровые объекты на сцене
            organs[i].transform.SetParent(HumenOrgans.transform);
            organs[i].AddComponent<RectTransform>();
            RectTransform rectTransformPanel = organs[i].GetComponent<RectTransform>();
            rectTransformPanel.localPosition = Vector3.zero;
            rectTransformPanel.localRotation = Quaternion.identity;
            organs[i].SetActive(false); // делаем объекты неактивными
        }
    }

}