using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateTest : MonoBehaviour
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

        if (questionNum == 1 || questionNum == 2 || questionNum == 5)
        {
            if (variant == 3)
            {
                score++;
                colors.normalColor = Color.green;
                buttonC.colors = colors;
            }
            else if (variant == 2)
            {
                colors.normalColor = Color.red;
                buttonB.colors = colors;
            }
            else if (variant == 1)
            {
                colors.normalColor = Color.red;
                buttonA.colors = colors;
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
                textA.text = "Дать обезболивающее средство";
                textB.text = "Провести иммобилизацию конечности в том положении, в котором она находится в момент повреждения";
                textC.text = "Остановить кровотечение";
                quest.text = "При открытом переломе прежде всего необходимо";
                break;
            case 3:
                textA.text = "Сверху вниз";
                textB.text = "Снизу вверх";
                textC.text = "В любом порядке";
                quest.text = "Как правильно перевязать поврежденную конечность?";
                break;
            case 4:
                textA.text = "Отек, покраснение и повышение температуры кожи";
                textB.text = "Боль, онемение и ослабление пульса";
                textC.text = "Оба варианта верны";
                quest.text = "Какие признаки свидетельствуют о том, что поврежденная конечность начинает отмирать?";
                break;
            case 5:
                textA.text = "Дать воды или еды";
                textB.text = "Ощупать травму с целью уточнения места перелома";
                textC.text = "Дать обезболивающе";
                quest.text = "Что из перечисленного можно сделать при переломе?";
                break;
            case 6:
                quest.text = "Тест пройден";
                PanelTest.SetActive(false);
                PanelResult.SetActive(true);
                textResult.text = score.ToString();

                if (score < 2)
                    score = 2;

                textScore.text = score.ToString();
                MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
                mainScene.onAddResultButtonClick(score, "Переломы");

                break;
        }
    }

}
