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

        Debug.Log("���������");
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
                textA.text = "��������� ���� � ������� ������� ��������";
                textB.text = "������� �������� ������� ��� ������������ ����� �������� �����";
                textC.text = "�������� ������� ���� ��� ������� �������";
                quest.text = "��� ������� ������� ��������� �������, ���� �� ���� ������ �������?";
                break;
            case 3:
                textA.text = "������� ���� ���� ��������� �������� � ������������, ��� ���� ���������� ������, ����������� ���������� ���������";
                textB.text = "������ ���� ����� ������, ���������� ��������� ������ � ������� ���������, ����������� ����������� ����������, ����� �����������";
                textC.text = "����������� ����, �� ����, � ���������� ����� ���������� ����, ������� ������";
                quest.text = "������ �������� ����� ������� �������?";
                break;
            case 4:
                textA.text = "5-10 �����";
                textB.text = "15-20 �����";
                textC.text = "25-30 �����";
                quest.text = "��� ����� ������� ��������� ������������ ����� �������� ����� ��� �����?";
                break;
            case 5:
                textA.text = "�������� ���������� �������, � ������ ��������� ���";
                textB.text = "��������� ���, � ����� ������� ������������ ������� ���������� ��������";
                textC.text = "������� ����� � ������� ������������ �������";
                quest.text = "��� ��������� ������� ������� �� ����, ���� �� ������� �������� ��������?";
                break;
            case 6:
                textA.text = "������� ����� ������������ �������, �� ������� ��������������� 2-� �������";
                textB.text = "������� ����� ���������, �� ������������ ��������, ��������������� 2�3-� ��������";
                textC.text = "������� ����� �������, �� ����������";
                quest.text = "������ ����������� ��������� ����� �� ����?";
                break;
            case 7:
                quest.text = "���� �������";
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
                mainScene.onAddResultButtonClick(score, "�����");

                break;
        }
    }

}
