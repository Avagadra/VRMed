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
            mainScene.onAddSimulatorResultButtonClick("������� ����");
            MainSceneTest.AddSimulator0 = 1;

        }
        Debug.Log("Via" + MainSceneTest.AddSimulator0);

        if (collision.gameObject.tag == "Bint")
        {

            if (gameObject.tag == "ShinaLeg")
            {
                Debug.Log("������");
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
                Debug.Log("��� ���� ��������");

                quest.text = "2. �������� ��� 3 ���� �� ���� ��������, ����� �������� � ������� (120x11)";
                help.text = "1. ������������� ���������� ����������� �����, ������������� ���������� �� ����� � ������� ������������������ ���������.\r\n" +
                    "2. ���� ����������� ������ ������, ����� �� ��������� ����� ��������, ��� ������ ����������� ��� ��������� � �������� �������� �������. ��� �������� ������, �� ����� ���������� ���, ����� ������� ����� � ����� �����;" +
                    "\r\n3. ������ � ������� ���� ������� �� �������� ����, � ������ ����� ����������� �� ���� � ���������.\r\n";
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
                quest.text = "3. ��������� ������ ���������";
                help.text = "1. ������� ��� ������� ������� � � ������� ���� ��� ������������� ��������� �� ���� ��� �������������� ��������� � �������� �������.\r\n";

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

                quest.text = "5. ������ ������ �������";
                help.text = "1. ���������������� ������������� � ����������� ���������� ��� ����������� ������� � ������������.\r\n.\r\n";
                

                Debug.Log("�� ����� � ���� � ��������");
                MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
                mainScene.onUpdateSimulatorResultButtonClick();
            }
        }
    }
}
