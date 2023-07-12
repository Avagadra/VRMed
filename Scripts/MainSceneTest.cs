using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEngine.Rendering.DebugUI;


public class MainSceneTest : MonoBehaviour
{

    TestResultService testService;
    DataController controller;

    public static int savedIDSimulator;
    public static int savedIDStudentSimulator;
    public static string savedNameSimulator;
    public static string savedDateSimulator;

    public static int AddSimulator0 = 0;

    public TextMeshProUGUI result;

    // Start is called before the first frame update
    void Start()
    {
        testService = new TestResultService();
        controller = new DataController();
    }

    public void ChangeSimulator0()
    {
        AddSimulator0 = 0;
    }
    public void onCreate()
    {
        Debug.Log("Create");
        testService.CreateResultTable();
    }

    public void onCreateSimulatorResult()
    {
        Debug.Log("Create");
        testService.CreateSimulatorResultTable();
    }

    public void onAddResultButtonClick(int score, string nameTest)
    {

        if (MainScene.savedAccessStudent == 1)
        {
            Debug.Log("Add");

            testService = new TestResultService();

            BDTestResult account = new BDTestResult
            {
                IDStudent = MainScene.savedIdStudent,
                DatePassage = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                NameTest = nameTest,
                Score = score

            };

            Debug.Log(account.Id);
            Debug.Log(account.IDStudent);
            Debug.Log(account.DatePassage);
            Debug.Log(account.NameTest);
            Debug.Log(account.Score);

            int pk = testService.AddResult(account);

            Debug.Log("PK = " + pk);
        } else
        {
            Debug.Log("Это не студента");
        }
    }

    public void onAddSimulatorResultButtonClick(string nameSimulator)
    {

        if (MainScene.savedAccessStudent == 1)
        {
            Debug.Log("Add");
            testService = new TestResultService();
            savedDateSimulator = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            BDSimulatorResult account = new BDSimulatorResult
            {
                IDStudent = MainScene.savedIdStudent,
                DatePassage = savedDateSimulator,
                NameSimulator = nameSimulator,
                Passed = "Нет"

            };

            Debug.Log(account.Id);
            Debug.Log(account.IDStudent);
            Debug.Log(account.DatePassage);
            Debug.Log(account.NameSimulator);
            Debug.Log(account.Passed);

            int pk = testService.AddSimulatorResult(account);

            Debug.Log("PK = " + pk);

            savedIDSimulator = testService.GetSimulatorResultForDate(savedDateSimulator).Id;
            savedIDStudentSimulator = testService.GetSimulatorResultForDate(savedDateSimulator).IDStudent;
            savedNameSimulator = testService.GetSimulatorResultForDate(savedDateSimulator).NameSimulator;


            Debug.Log("savedd");
            Debug.Log(savedIDSimulator);
            Debug.Log(savedIDStudentSimulator);
            Debug.Log(savedNameSimulator);
        }
        else
        {
            Debug.Log("Это не студент");
        }
    }
    public void onUpdateSimulatorResultButtonClick()
    {
        if (MainScene.savedAccessStudent == 1)
        {
            Debug.Log("Update");

            testService = new TestResultService();

            BDSimulatorResult account = new BDSimulatorResult
            {
                Id = savedIDSimulator,
                IDStudent = savedIDStudentSimulator,
                DatePassage = savedDateSimulator,
                NameSimulator = savedNameSimulator,
                Passed = "Да"
            };
    

            Debug.Log(account.Id);
            Debug.Log(account.IDStudent);
            Debug.Log(account.DatePassage);
            Debug.Log(account.NameSimulator);
            Debug.Log(account.Passed);

            int pk = testService.UpdateSimulatorResult(account);

            Debug.Log("PK = " + pk);
        }
        else
        {
            Debug.Log("Это не студент");
        }

    }

}
