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
            mainScene.onAddSimulatorResultButtonClick("������� ����");
            MainSceneTest.AddSimulator0 = 1;

        }
        Debug.Log("Via" + MainSceneTest.AddSimulator0);

        if (collision.gameObject.tag == "Bint")
        {

            if (gameObject.tag == "ShinaLeg")
            {
                Debug.Log("������");
                animator.SetTrigger("IsRyka");

                BintHand.SetActive(true);
                gameObject.tag = "BintLeg";
                Destroy(collision.gameObject);

                quest.text = "2. �������� ���� �� ������ ����� ��������";
                help.text = "1. ������������� ���������� ����������� �����, ������������� ���������� �� ����� � ������� ������������������ ���������.\r\n" +
                    "2. ��������� ���� � �������� ���������� ���, ����� ����� ���������� � ����� ���� �������� �����, ����� ���� ������ ��������� �� ����� (��������� �����) �� ���������������� ��������� ������� (��������� ������������ ���������� ����).\r\n" +
                    "������� ������������ ���������� ��������������������� ��������� (�� ����������� ���� ������� � �������� �������), ����� ������� �� ���� � ��������� ����� ���������� � ���������." +
                    "\r\n3. ������ � ������� ���� ������� �� �������� ����, � ������ ����� ����������� �� ���� � ���������.\r\n";
            }
        }

        if (collision.gameObject.tag == "Leg")
        {
            if (gameObject.tag == "BintLeg")
            {
                BintingHand.SetActive(true);
                Destroy(gameObject);

                quest.text = "3. ��������� ������ ���������";
                help.text = "1. ������� ��� ����, ������ � ����� ��������� �� ���� ��� �������������� ��������� � �������� �������.\r\n";
            }
        }

        if (gameObject.tag == "Leg")
        {
            Debug.Log("������������ � �����");
            if (collision.gameObject.tag == "Podyshka" && quest.text == "3. ��������� ������ ���������")
            {
                Podyshki.SetActive(true);
                Destroy(collision.gameObject);

                quest.text = "4. ��������� ���� �� ���������� ���������� ������";
                help.text = "1.  ������������� ���� �� ���������� ����������� ������ �����.\r\n";
            }

            if (collision.gameObject.tag == "ElastBint" && quest.text == "4. ��������� ���� �� ���������� ���������� ������")
            {
                Obmotka.SetActive(true);
                Destroy(collision.gameObject);

                quest.text = "5. ��������� ���������� � ������� �������";
                help.text = "1. �������� ������� ����� ��� ������ �������� ���������� ��� ��������� ���������� � ������� ������� \r\n";
            }

            if (collision.gameObject.tag == "Kosinka" && quest.text == "5. ��������� ���������� � ������� �������")
            {
                Kosinka.SetActive(true);
                Destroy(collision.gameObject);

                quest.text = "6. ������ ������ �������";
                help.text = "1. ���������������� ������������� � ����������� ���������� ��� ����������� ������� � ������������.\r\n";


                Debug.Log("�� ����� � ���� � ��������");
                MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
                mainScene.onUpdateSimulatorResultButtonClick();
            }
        }
    }
}
