using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivateBintBurn1 : MonoBehaviour
{

    public TextMeshProUGUI quest;
    public TextMeshProUGUI help;

    public GameObject Pants;
    public GameObject PantsCut;

    public GameObject Ice;
    public GameObject Obmotka;
    public GameObject Salfetka;

    public int AddResult = 0;
    public int Stage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (MainSceneTest.AddSimulator0 == 0)
        {
            MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
            mainScene.onAddSimulatorResultButtonClick("���� 2 �������");
            MainSceneTest.AddSimulator0 = 1;

        }
        Debug.Log("Via" + MainSceneTest.AddSimulator0);

        if (collision.gameObject.tag == "Noznici" && Stage == 1)
        {
            Pants.SetActive(false);
            PantsCut.SetActive(true);

            Destroy(collision.gameObject);

            quest.text = "2. �������� ����� ����� � ������� ��������� ��������� � ��������� 5 ������ (� ���������� ���� ����� ����� 15 �����)";
            help.text = "1. ��������� ����� ����� ��� ��������� ����, �������� ������ ��� ��� ����� ������ ����������� �������� (1-2 ������� �����).\r\n" +
                    "2. �������� ���������� �������, � ������ ��������� ��� (���� �� ������� �������� �������� 3 ������� �����).\r\n" +
                    "3. ����������� ����� ���������� � 10-20 ���. � ������ ����������� ����� ���������� ����� ��������� �� 40 ���. ��� ���� ���������� � ������� 2 ����� ����� ��������� ����� � �� ������ ��������� ����, �� � - ������� ���������.\r\n" +
                    "\r\n 4. ��� ����� ������ ������������ ���������� ��������, ���, �����, �������� ���� � ������ �������������� ��������.\r\n";

            Stage = 2;
            }

        if (collision.gameObject.tag == "Ice" && Stage == 2)
        {
            Ice.SetActive(true);
            Destroy(collision.gameObject);

            Invoke("TimerEnded", 5f);

        }

        if (collision.gameObject.tag == "Salfetka" && Stage == 3)
        {
            Salfetka.SetActive(true);
            Destroy(collision.gameObject);

            Stage = 4;
        }

        if (collision.gameObject.tag == "Bint" && Stage == 4)
        {
            Obmotka.SetActive(true);
            Destroy(collision.gameObject);

            quest.text = "4. ������ ������ �������";
            help.text = "1. ��� ���� �� ������� ���������� ����� �����. ���� �������� ����������, ����� ������������� ����� ������, ��������� ������� ����.\r\n" +
                "2. ��� ������ 3-4 ������� ��������� ���������� ����������� ������.\r\n" +
                "3. ���� ������������� ����� ���������� - �����������, �������� ��� �������������� ��������. ��� �������� ������������ �������� �������� ���� � ������ ������ �����������. ����� ���� ���� �������� ������� (3-4 �������) ��� �������� �������� ������� ������� ��������� �������� � ��������.\r\n";

            Stage = 5;

            Debug.Log("�� ����� � ����� � ��������");
            MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
            mainScene.onUpdateSimulatorResultButtonClick();
        }

    }
    void TimerEnded()
    {
        quest.text = "3. �������� �� ����� ����� ���������� ��������, � ����� ���������� ����� ��������� � ������� �����";
        help.text = "1. ������� ��� ����, ������ � ����� ��������� �� ���� ��� �������������� ��������� � �������� �������.\r\n";
        Destroy(Ice);

        Stage = 3;
    }
}
