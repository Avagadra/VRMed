using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MainScene : MonoBehaviour
{

    //на добавление акк
    public TMP_InputField inputAccess;
    public TMP_InputField inputName;
    public TMP_InputField inputSurname;
    public TMP_InputField inputDate;
    public TMP_InputField inputLogin;
    public TMP_InputField inputPassword;

    //на изменение акк
    public TMP_InputField updateInputAccess;
    public TMP_InputField updateInputName;
    public TMP_InputField updateInputSurname;
    public TMP_InputField updateInputDate;
    public TMP_InputField updateInputLogin;
    public TMP_InputField updateInputPassword;

    public TMP_InputField inputDelName;
    public TMP_InputField inputDelSurname;

    public TMP_InputField inputAuthLogin;
    public TMP_InputField inputAuthPassword;

    public TMP_InputField inputNameSurch;
    public TMP_InputField inputSurnameSurch;

    public TMP_InputField inputNameStudent;
    public TMP_InputField inputSurnameStudent;
    public TMP_InputField inputNameStudentPractice;
    public TMP_InputField inputSurnameStudentPractice;

    public GameObject PanelADM;
    public GameObject PanelTeach;
    public GameObject PanelStude;
    public GameObject PanelAuth;

    public GameObject PanelError;

    public GameObject content;
    public GameObject contentDell;
    public GameObject contentStudent;
    public GameObject contentTeacher;
    public GameObject contentStudentPractice;
    public GameObject contentTeacherPractice;

    public GameObject PanelDellOrChange;

    public static int savedIdStudent;
    public static int savedAccessStudent;

    //Имена
    public TextMeshProUGUI NameStudent;
    public TextMeshProUGUI SurnameStudent;
    public TextMeshProUGUI NameTeacher;
    public TextMeshProUGUI SurnameTeacher;

    //public GridLayoutGroup table;

    AccountService accountService;
    DataController controller;
    DataControllerTestResult controllerStudent;
    // Start is called before the first frame update
    void Start()
    {
        accountService = new AccountService();
        controller = new DataController();
        controllerStudent = new DataControllerTestResult();
    }

    public void onCreateAccountTableButtonClick()
    {
        Debug.Log("Create");
        accountService.CreateAccountTable();
    }

    public void onAddAccountButtonClick()
    {
        Debug.Log("Add");

        int text2 = int.Parse(inputAccess.text);
        string text3 = inputName.text;
        string text4 = inputSurname.text;
        string text5 = inputDate.text;
        string text6 = inputLogin.text;
        string text7 = inputPassword.text;

        BDAccount account = new BDAccount
        {
            AccessLevel = text2,
            Name = text3,
            Surname = text4,
            DateOfBirth = text5,
            Login = text6,
            Password = text7
        };

        int pk = accountService.AddAccount(account);

        Debug.Log("PK = " + pk);

    }

    public void onUpdateAccountButtonClick()
    {
        Debug.Log("Update");

        int text2 = int.Parse(updateInputAccess.text);
        string text3 = updateInputName.text;
        string text4 = updateInputSurname.text;
        string text5 = updateInputDate.text;
        string text6 = updateInputLogin.text;
        string text7 = updateInputPassword.text;

        BDAccount account = new BDAccount
        {
            Id = DataController.savedId,
            AccessLevel = text2,
            Name = text3,
            Surname = text4,
            DateOfBirth = text5,
            Login = text6,
            Password = text7
        };

        int pk = accountService.UpdateAccount(account);

        Debug.Log("PK = " + pk);

    }

    public void onDeleteAccountButtonClick()
    {
        Debug.Log("Delete");

        string text1 = inputDelName.text;
        string text2 = inputDelSurname.text;

        BDAccount account = new BDAccount
        {
            Name = text1,
            Surname = text2
        };

        int val = accountService.DeleteAccount(account);

        Debug.Log("Return Val = " + val);

    }

    public void onGetAcountsButtonClick()
    {
        controller.GetTableAll(content.GetComponent<GridLayoutGroup>(), content, PanelDellOrChange, updateInputAccess, updateInputName, updateInputSurname, updateInputDate, updateInputLogin, updateInputPassword);
    }

    public void onGetTestResultButtonClick()
    {
        controllerStudent.GetTableByIDPractice(contentStudentPractice.GetComponent<GridLayoutGroup>(), contentStudentPractice);
    }

    public void onGetPracticeResultButtonClick()
    {
        controllerStudent.GetTableByID(contentStudent.GetComponent<GridLayoutGroup>(), contentStudent);
    }

    public void onGetTestResultAllButtonClick()
    {
        controllerStudent.GetTableAll(contentTeacher.GetComponent<GridLayoutGroup>(), contentTeacher);
    }
    public void onGetSimulatorResultAllButtonClick()
    {
        controllerStudent.GetTableAllPractice(contentTeacherPractice.GetComponent<GridLayoutGroup>(), contentTeacherPractice);
    }

    public void onTestResultByNameSurnameButtonClick()
    {
        controllerStudent.GetTableByNameSurname(contentTeacher.GetComponent<GridLayoutGroup>(), contentTeacher, inputNameStudent.text, inputSurnameStudent.text);
    }

    public void onSimulatorResultByNameSurnameButtonClick()
    {
        controllerStudent.GetTableByNameSurnamePractice(contentTeacherPractice.GetComponent<GridLayoutGroup>(), contentTeacherPractice, inputNameStudentPractice.text, inputSurnameStudentPractice.text);
    }

    public void onGetAcountsByNameSurnameButtonClick()
    {
        controller.GetTableByNameSurname(contentDell.GetComponent<GridLayoutGroup>(), contentDell, inputNameSurch.text, inputSurnameSurch.text, PanelDellOrChange);
    }

    public void onGetAcoountByLoginPasswordButtonClick()
    {
        string account = "";
        try
        {
            account = accountService.GetAccountAuth(inputAuthLogin.text, inputAuthPassword.text).ToString();
            savedIdStudent = accountService.GetAccountAuth(inputAuthLogin.text, inputAuthPassword.text).Id;
            savedAccessStudent = accountService.GetAccountAuth(inputAuthLogin.text, inputAuthPassword.text).AccessLevel;
            Debug.Log("студент под номером " + savedIdStudent);
        } catch(Exception ex)
        {
            PanelError.SetActive(true);
        }


        Debug.Log(account);

        char[] separators = { '=', ',' };
        string[] parts = account.Split(separators);

        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].Trim().Equals("AccessLevel"))
            {
                int accessLevel;
                if (int.TryParse(parts[i + 1], out accessLevel))
                {
                    // Значение AccessLevel успешно распознано
                    Debug.Log("AccessLevel: " + accessLevel);

                    PanelAuth.SetActive(false);

                    if (accessLevel == 3)
                        PanelADM.SetActive(true);
                    else if (accessLevel == 2) {
                        PanelTeach.SetActive(true);
                        NameTeacher.text = accountService.GetAccountAuth(inputAuthLogin.text, inputAuthPassword.text).Name;
                        SurnameTeacher.text = accountService.GetAccountAuth(inputAuthLogin.text, inputAuthPassword.text).Surname;
                    }
                    else
                    {
                        PanelStude.SetActive(true);
                        NameStudent.text = accountService.GetAccountAuth(inputAuthLogin.text, inputAuthPassword.text).Name;
                        SurnameStudent.text = accountService.GetAccountAuth(inputAuthLogin.text, inputAuthPassword.text).Surname;
                    }

                } else
                {
                    // Не удалось распознать значение AccessLevel
                    Debug.LogError("Failed to parse AccessLevel value");
                }
                break;
            }
        }
    }

    public void onDeleteAccountByIDButtonClick()
    {
        Debug.Log("Delete");

        BDAccount account = new BDAccount
        {
            Id = DataController.savedId,
        };

        int val = accountService.DeleteAccount(account);

        Debug.Log("Return Val = " + val);

    }

}
