using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ActivateBint : MonoBehaviour
{
    public TextMeshProUGUI quest;
    public TextMeshProUGUI help;
    //public MeshRenderer PLSSSS;
    //public Outline outline;

    //public GameObject obj;

    public Animator animator;

    public GameObject BintLeg;
    public GameObject BintLeg1;
    public GameObject BintLeg2;

    public GameObject BintingLeg;
    public GameObject BintingLeg1;
    public GameObject BintingLeg2;

    public GameObject Podyshki;
    public GameObject Obmotka;


    private void OnCollisionEnter(Collision collision)
    {
        if (MainSceneTest.AddSimulator0 == 0)
        {
            MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
            mainScene.onAddSimulatorResultButtonClick("Перелом ноги");
            MainSceneTest.AddSimulator0 = 1;

        }
        Debug.Log("Via" + MainSceneTest.AddSimulator0);

        if (collision.gameObject.tag == "Bint")
        {

            if (gameObject.tag == "ShinaLeg")
            {
                Debug.Log("Лежать");
                animator.SetTrigger("IsLies");

                BintLeg.SetActive(true);
                gameObject.tag = "BintLeg";
                Destroy(collision.gameObject);
            }
            if (gameObject.tag == "ShinaLegMini1")
            {
                BintLeg1.SetActive(true);
                gameObject.tag = "BintLegMini1";
                Destroy(collision.gameObject);
            }
            if (gameObject.tag == "ShinaLegMini2")
            {
                BintLeg2.SetActive(true);
                gameObject.tag = "BintLegMini2";
                Destroy(collision.gameObject);
            }

            if ((BintLeg.activeSelf == true) & (BintLeg1.activeSelf == true) &  (BintLeg2.activeSelf == true))
            {
                Debug.Log("Все шины обмотаны");

                quest.text = "2. Наложите все 3 шины на ногу человека, лучше начинать с большой (120x11)";
                help.text = "1. Предотвратить дальнейшее повреждение кости, зафиксировать конечность на месте с помощью иммобилизационного материала.\r\n" +
                    "2. Шину накладывают поверх одежды, чтобы не тревожить место перелома, она должна захватывать два ближайших к перелому здоровых сустава. При переломе голени, ее нужно разместить так, чтобы закрыть стопу и часть бедра;" +
                    "\r\n3. Мерить и сгибать шину следует по здоровой ноге, а только затем накладывать на ногу с переломом.\r\n";
            }
        }

        if (collision.gameObject.tag == "Leg")
        {
            if (gameObject.tag == "BintLeg")
            {
                BintingLeg.SetActive(true);
                Destroy(gameObject);
            }

            if (gameObject.tag == "BintLegMini1")
            {
                BintingLeg1.SetActive(true);
                Destroy(gameObject);
            }

            if (gameObject.tag == "BintLegMini2")
            {
                BintingLeg2.SetActive(true);
                Destroy(gameObject);
            }

            if ((BintingLeg.activeSelf == true) & (BintingLeg1.activeSelf == true) & (BintingLeg2.activeSelf == true))
            {
                quest.text = "3. Подложите мягкие подушечки";
                help.text = "1. Вложить под костные выступы и в область паха при необходимости прокладку из ваты для предупреждения сдавления и развития некроза.\r\n";

            }
        }

        if (gameObject.tag == "Leg")
        {

            Debug.Log("Столкновение с ногой");
            if (collision.gameObject.tag == "Podyshka" && quest.text == "3. Подложите мягкие подушечки")
            {
                Podyshki.SetActive(true);
                Destroy(collision.gameObject);

                quest.text = "4. Закрепить шину на конечности эластичным бинтом";
                help.text = "1.  Зафиксировать шины на конечности спиральными турами бинта.\r\n";
            }

            if (collision.gameObject.tag == "ElastBint" && quest.text == "4. Закрепить шину на конечности эластичным бинтом")
            {
                Obmotka.SetActive(true);
                Destroy(collision.gameObject);

                quest.text = "5. Первая помощь оказана";
                help.text = "1. Транспортировать пострадавшего в медицинское учреждение для дальнейшего лечения и обследования.\r\n.\r\n";
                

                Debug.Log("Мы зашли в ноги и изменили");
                MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
                mainScene.onUpdateSimulatorResultButtonClick();
            }
        }
    }
}
