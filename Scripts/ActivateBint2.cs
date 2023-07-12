using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateBint2 : MonoBehaviour
{
    public TextMeshProUGUI quest;
    public TextMeshProUGUI help;

    public Animator animator;

    public GameObject BintHand;

    public GameObject BintingHand;

    public GameObject Podyshki;
    public GameObject Obmotka;
    public GameObject Kosinka;

    public int AddResult = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (MainSceneTest.AddSimulator0 == 0)
        {
            MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
            mainScene.onAddSimulatorResultButtonClick("Перелом руки");
            MainSceneTest.AddSimulator0 = 1;

        }
        Debug.Log("Via" + MainSceneTest.AddSimulator0);

        if (collision.gameObject.tag == "Bint")
        {

            if (gameObject.tag == "ShinaLeg")
            {
                Debug.Log("Лежать");
                animator.SetTrigger("IsRyka");

                BintHand.SetActive(true);
                gameObject.tag = "BintLeg";
                Destroy(collision.gameObject);

                quest.text = "2. Наложите шины на правое плечо человека";
                help.text = "1. Предотвратить дальнейшее повреждение кости, зафиксировать конечность на месте с помощью иммобилизационного материала.\r\n" +
                    "2. Приложить шину к здоровой конечности так, чтобы кисть предплечье и плечо было охвачено шиной, конец шины должен проходить по спине (плечевому поясу) до противоположного плечевого сустава (проверить правильность подготовки шины).\r\n" +
                    "Придать поврежденной конечности среднефизиологическое положение (по возможности руку согнуть в локтевом суставе), кисть уложить на шине в положение между супинацией и пронацией." +
                    "\r\n3. Мерить и сгибать шину следует по здоровой руке, а только затем накладывать на руку с переломом.\r\n";
            }
        }

        if (collision.gameObject.tag == "Leg")
        {
            if (gameObject.tag == "BintLeg")
            {
                BintingHand.SetActive(true);
                Destroy(gameObject);

                quest.text = "3. Подложите мягкие подушечки";
                help.text = "1. Вложить под руку, локоть и плечо прокладку из ваты для предупреждения сдавления и развития некроза.\r\n";
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

                quest.text = "5. Закрепите конечность с помощью косынки";
                help.text = "1. Наложить повязку «Дезо» для лучшей фиксации конечности или закрепите конечность с помощью косынки \r\n";
            }

            if (collision.gameObject.tag == "Kosinka" && quest.text == "5. Закрепите конечность с помощью косынки")
            {
                Kosinka.SetActive(true);
                Destroy(collision.gameObject);

                quest.text = "6. Первая помощь оказана";
                help.text = "1. Транспортировать пострадавшего в медицинское учреждение для дальнейшего лечения и обследования.\r\n";


                Debug.Log("Мы зашли в руки и изменили");
                MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
                mainScene.onUpdateSimulatorResultButtonClick();
            }
        }
    }
}
