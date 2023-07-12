using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateBurnTest : MonoBehaviour
{
    public TextMeshProUGUI quest;
    public TextMeshProUGUI textA;
    public TextMeshProUGUI textB;
    public TextMeshProUGUI textC;

    public int variant;
    public int score;
    public int questionNum;

    ColorBlock colors;

    public Button buttonA;
    public Button buttonB;
    public Button buttonC;

    public GameObject PanelTest;
    public GameObject PanelResult;
    public TextMeshProUGUI textResult;
    public TextMeshProUGUI textScore;

    void Start()
    {
        colors = buttonA.colors;
        colors = buttonB.colors;
        colors = buttonC.colors;
        score = 0;
        questionNum = 1;
    }

    // Update is called once per frame
    public void Click(int version)
    {
        variant = version;
        buttonA.enabled = false;
        buttonB.enabled = false;
        buttonC.enabled = false;

        Debug.Log("ТыКликнул");
        Debug.Log(version);
        Debug.Log(questionNum);

        if (questionNum == 5 || questionNum == 1)
        {
            if (variant == 1)
            {
                score++;
                colors.normalColor = Color.green;
                buttonA.colors = colors;
            }
            else if (variant == 2)
            {
                colors.normalColor = Color.red;
                buttonB.colors = colors;
            }
            else if (variant == 3)
            {
                colors.normalColor = Color.red;
                buttonC.colors = colors;
            }

            questionNum++;

        }
        else if (questionNum == 3 || questionNum == 4)
        {
            if (variant == 2)
            {
                score++;
                colors.normalColor = Color.green;
                buttonB.colors = colors;
            }
            else if (variant == 3)
            {
                colors.normalColor = Color.red;
                buttonC.colors = colors;
            }
            else if (variant == 1)
            {
                colors.normalColor = Color.red;
                buttonA.colors = colors;
            }
            questionNum++;

        }

        else if (questionNum == 6 || questionNum == 2)
        {
            if (variant == 3)
            {
                score++;
                colors.normalColor = Color.green;
                buttonC.colors = colors;
            }
            else if (variant == 1)
            {
                colors.normalColor = Color.red;
                buttonA.colors = colors;
            }
            else if (variant == 2)
            {
                colors.normalColor = Color.red;
                buttonB.colors = colors;
            }
            questionNum++;

        }

        Invoke("MyMethod", 2f);

    }
    void MyMethod()
    {
        buttonA.enabled = true;
        buttonB.enabled = true;
        buttonC.enabled = true;

        colors.normalColor = Color.white;
        buttonA.colors = colors;
        buttonB.colors = colors;
        buttonC.colors = colors;

        switch (questionNum)
        {
            case 2:
                textA.text = "Проточная вода и раствор медного купороса";
                textB.text = "Раствор лимонной кислоты или разбавленный водой столовый уксус";
                textC.text = "Холодная мыльная вода или содовый раствор";
                quest.text = "Чем следует промыть обоженную область, если на ожог попала кислота?";
                break;
            case 3:
                textA.text = "Верхний слой кожи полностью погибает и отслаивается, при этом образуются пузыри, заполненные прозрачной жидкостью";
                textB.text = "Некроз всех слоев тканей, образуются массивные пузыри с толстой оболочкой, заполненные кровянистым содержимым, очень болезненные";
                textC.text = "Покраснение кожи, ее отек, в пораженном месте отмечаются боли, чувство жжения";
                quest.text = "Каковы признаки ожога третьей степени?";
                break;
            case 4:
                textA.text = "5-10 минут";
                textB.text = "15-20 минут";
                textC.text = "25-30 минут";
                quest.text = "Как долго следует промывать поврежденное место холодной водой при ожоге?";
                break;
            case 5:
                textA.text = "Наложить стерильную повязку, а сверху приложить лед";
                textB.text = "Приложить лед, а затем накрыть поврежденную область стерильной повязкой";
                textC.text = "Промыть водой и накрыть поврежденную область";
                quest.text = "Как правильно нанести повязку на ожог, если из пузырей вытекает жидкость?";
                break;
            case 6:
                textA.text = "Площадь ожога относительно большая, по глубине преимущественно 2-я степень";
                textB.text = "Площадь ожога небольшая, но относительно глубокая, преимущественно 2—3-й степеней";
                textC.text = "Площадь ожога большая, но неглубокая";
                quest.text = "Каковы последствия получении ожога от пара?";
                break;
            case 7:
                quest.text = "Тест пройден";
                PanelTest.SetActive(false);
                PanelResult.SetActive(true);
                textResult.text = score.ToString();

                double perc = 0;
                perc = ((double)score / 6d) * 100d;

                Debug.Log("scoreee " + score);
                Debug.Log("perc   " + perc);
                Debug.Log("percscoreeeeee   " + ((double)score / 6d) * 100d);

                if (perc == 100)
                    score = 5;
                else if (perc >= 65)
                    score = 4;
                else if (perc >= 45)
                    score = 3;
                else
                    score = 2;

                textScore.text = score.ToString();
                MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
                mainScene.onAddResultButtonClick(score, "Ожоги");

                break;
        }
    }

}
