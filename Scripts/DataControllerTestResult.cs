using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.Data;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine.UI;
using System.IO;
using SQLiteConnection = Unity.VisualScripting.Dependencies.Sqlite.SQLiteConnection;
using System;
using TMPro;
using static UnityEditor.Progress;
using UnityEngine.TextCore.Text;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.EventSystems;
using System.Linq;

public class DataControllerTestResult : MonoBehaviour
{

    public GridLayoutGroup table;
    public TextMeshProUGUI textMeshProUGUI;
    public TMP_FontAsset fontAsset;
    TestResultService resultService;
    AccountService accountService;

    public static int savedId;

    DB dB;
    public DataControllerTestResult()
    {
        dB = new DB();
    }

    void Start()
    {
        resultService = new TestResultService();
        accountService = new AccountService();
    }

    public void GetTableByID(GridLayoutGroup table, GameObject parentObject)
    {
        resultService = new TestResultService();
        dB.GetConnection();

        //проверяем получен ли GridLayoutGroup
        if (table == null)
        {
            Debug.Log("table is null");
            return;
        }
        // Очищаем таблицу перед заполнением
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }


        var resultTest = resultService.GetResultForIDStudent(MainScene.savedIdStudent); // получаем список аккаунтов

        Debug.Log(MainScene.savedIdStudent);
        Debug.Log(resultTest);

        //проверяем что аккаунты считываются
        if (resultTest == null)
        {
            Debug.Log("accounts list is empty");
            return;
        }

        fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

        // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
        GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
        panelHeader.transform.SetParent(parentObject.transform);
        RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        // Добавляем компонент Image на созданный объект
        Image panelImage = panelHeader.AddComponent<Image>();
        // Устанавливаем желаемый цвет
        Color color;
        if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
        {
            panelImage.color = color;
        }
        //создаем ячейки таблицы на панеле-строке
        panelHeader.AddComponent<GridLayoutGroup>();
        panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(400, 100);



        // Создаем заголовки для таблицы
        AddCellHeader(panelHeader, "Название теста");
        AddCellHeader(panelHeader, "Дата прохождения");
        AddCellHeader(panelHeader, "Результаты");


        foreach (var account in resultTest)
        {
            //верно ли подключен шрифт
            if (fontAsset == null)
            {
                Debug.Log("font not found");
                return;
            }

            // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
            GameObject panel = new GameObject("Panel", typeof(RectTransform));
            panel.transform.SetParent(parentObject.transform);
            RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
            rectTransformPanel.localScale = Vector2.one;

            // Добавляем компонент Image на созданный объект
            Image panelImagePanel = panel.AddComponent<Image>();

            // Устанавливаем желаемый цвет
            Color colorPanel;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
            {
                panelImagePanel.color = colorPanel;
            }

            //создаем ячейки таблицы на панеле-строке
            panel.AddComponent<GridLayoutGroup>();
            panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(400, 100);

            //// Создаем новый элемент таблицы

            AddCell(panel, account.NameTest);
            AddCell(panel, account.DatePassage);
            AddCell(panel, account.Score.ToString());
        }

        ResizeContent(resultTest, parentObject, table);
    }

    public void GetTableByIDPractice(GridLayoutGroup table, GameObject parentObject)
    {
        resultService = new TestResultService();
        dB.GetConnection();

        //проверяем получен ли GridLayoutGroup
        if (table == null)
        {
            Debug.Log("table is null");
            return;
        }
        // Очищаем таблицу перед заполнением
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }


        var resultTest = resultService.GetResultPracticeForIDStudent(MainScene.savedIdStudent); // получаем список аккаунтов

        Debug.Log(MainScene.savedIdStudent);
        Debug.Log(resultTest);

        //проверяем что аккаунты считываются
        if (resultTest == null)
        {
            Debug.Log("accounts list is empty");
            return;
        }

        fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

        // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
        GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
        panelHeader.transform.SetParent(parentObject.transform);
        RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        // Добавляем компонент Image на созданный объект
        Image panelImage = panelHeader.AddComponent<Image>();
        // Устанавливаем желаемый цвет
        Color color;
        if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
        {
            panelImage.color = color;
        }
        //создаем ячейки таблицы на панеле-строке
        panelHeader.AddComponent<GridLayoutGroup>();
        panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(400, 100);



        // Создаем заголовки для таблицы
        AddCellHeader(panelHeader, "Название симулятора");
        AddCellHeader(panelHeader, "Дата прохождения");
        AddCellHeader(panelHeader, "Пройден ли симулятор");


        foreach (var account in resultTest)
        {
            //верно ли подключен шрифт
            if (fontAsset == null)
            {
                Debug.Log("font not found");
                return;
            }

            // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
            GameObject panel = new GameObject("Panel", typeof(RectTransform));
            panel.transform.SetParent(parentObject.transform);
            RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
            rectTransformPanel.localScale = Vector2.one;

            // Добавляем компонент Image на созданный объект
            Image panelImagePanel = panel.AddComponent<Image>();

            // Устанавливаем желаемый цвет
            Color colorPanel;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
            {
                panelImagePanel.color = colorPanel;
            }

            //создаем ячейки таблицы на панеле-строке
            panel.AddComponent<GridLayoutGroup>();
            panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(400, 100);

            //// Создаем новый элемент таблицы

            AddCell(panel, account.NameSimulator);
            AddCell(panel, account.DatePassage);
            AddCell(panel, account.Passed);
        }

        ResizeContentPractice(resultTest, parentObject, table);
    }

    public void GetTableAll(GridLayoutGroup table, GameObject parentObject)
    {
        accountService = new AccountService();
        resultService = new TestResultService();
        dB.GetConnection();

        //проверяем получен ли GridLayoutGroup
        if (table == null)
        {
            Debug.Log("table is null");
            return;
        }
        // Очищаем таблицу перед заполнением
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }


        var resultTest = resultService.GetResult(); // получаем список аккаунтов

        Debug.Log(MainScene.savedIdStudent);
        Debug.Log(resultTest);

        //проверяем что аккаунты считываются
        if (resultTest == null)
        {
            Debug.Log("accounts list is empty");
            return;
        }

        fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

        // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
        GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
        panelHeader.transform.SetParent(parentObject.transform);
        RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        // Добавляем компонент Image на созданный объект
        Image panelImage = panelHeader.AddComponent<Image>();
        // Устанавливаем желаемый цвет
        Color color;
        if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
        {
            panelImage.color = color;
        }
        //создаем ячейки таблицы на панеле-строке
        panelHeader.AddComponent<GridLayoutGroup>();
        panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 100);



        // Создаем заголовки для таблицы
        AddCellHeader(panelHeader, "Имя студента");
        AddCellHeader(panelHeader, "Фамилия студента");
        AddCellHeader(panelHeader, "Название теста");
        AddCellHeader(panelHeader, "Дата прохождения");
        AddCellHeader(panelHeader, "Результаты");


        foreach (var account in resultTest)
        {
            //верно ли подключен шрифт
            if (fontAsset == null)
            {
                Debug.Log("font not found");
                return;
            }

            // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
            GameObject panel = new GameObject("Panel", typeof(RectTransform));
            panel.transform.SetParent(parentObject.transform);
            RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
            rectTransformPanel.localScale = Vector2.one;

            // Добавляем компонент Image на созданный объект
            Image panelImagePanel = panel.AddComponent<Image>();

            // Устанавливаем желаемый цвет
            Color colorPanel;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
            {
                panelImagePanel.color = colorPanel;
            }

            //создаем ячейки таблицы на панеле-строке
            panel.AddComponent<GridLayoutGroup>();
            panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 100);

            //// Создаем новый элемент таблицы
            ///

            try
            {
                string name = accountService.GetAccountId(account.IDStudent).Name;
                string surname = accountService.GetAccountId(account.IDStudent).Surname;

                AddCell(panel, name);
                AddCell(panel, surname);
                AddCell(panel, account.NameTest);
                AddCell(panel, account.DatePassage);
                AddCell(panel, account.Score.ToString());
            } catch { }
        }

        ResizeContent(resultTest, parentObject, table);
    }

    public void GetTableAllPractice(GridLayoutGroup table, GameObject parentObject)
    {
        accountService = new AccountService();
        resultService = new TestResultService();
        dB.GetConnection();

        //проверяем получен ли GridLayoutGroup
        if (table == null)
        {
            Debug.Log("table is null");
            return;
        }
        // Очищаем таблицу перед заполнением
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }


        var resultTest = resultService.GetResultPractice(); // получаем список аккаунтов

        Debug.Log(MainScene.savedIdStudent);
        Debug.Log(resultTest);

        //проверяем что аккаунты считываются
        if (resultTest == null)
        {
            Debug.Log("accounts list is empty");
            return;
        }

        fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

        // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
        GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
        panelHeader.transform.SetParent(parentObject.transform);
        RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        // Добавляем компонент Image на созданный объект
        Image panelImage = panelHeader.AddComponent<Image>();
        // Устанавливаем желаемый цвет
        Color color;
        if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
        {
            panelImage.color = color;
        }
        //создаем ячейки таблицы на панеле-строке
        panelHeader.AddComponent<GridLayoutGroup>();
        panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 100);



        // Создаем заголовки для таблицы
        AddCellHeader(panelHeader, "Имя студента");
        AddCellHeader(panelHeader, "Фамилия студента");
        AddCellHeader(panelHeader, "Название тренажера");
        AddCellHeader(panelHeader, "Дата прохождения");
        AddCellHeader(panelHeader, "Пройден ли");


        foreach (var account in resultTest)
        {
            //верно ли подключен шрифт
            if (fontAsset == null)
            {
                Debug.Log("font not found");
                return;
            }

            // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
            GameObject panel = new GameObject("Panel", typeof(RectTransform));
            panel.transform.SetParent(parentObject.transform);
            RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
            rectTransformPanel.localScale = Vector2.one;

            // Добавляем компонент Image на созданный объект
            Image panelImagePanel = panel.AddComponent<Image>();

            // Устанавливаем желаемый цвет
            Color colorPanel;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
            {
                panelImagePanel.color = colorPanel;
            }

            //создаем ячейки таблицы на панеле-строке
            panel.AddComponent<GridLayoutGroup>();
            panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 100);

            //// Создаем новый элемент таблицы
            ///

            try
            {
                string name = accountService.GetAccountId(account.IDStudent).Name;
                string surname = accountService.GetAccountId(account.IDStudent).Surname;

                AddCell(panel, name);
                AddCell(panel, surname);
                AddCell(panel, account.NameSimulator);
                AddCell(panel, account.DatePassage);
                AddCell(panel, account.Passed);
            }
            catch { }
        }

        ResizeContentPractice(resultTest, parentObject, table);
    }
    public void GetTableByNameSurname(GridLayoutGroup table, GameObject parentObject, string name, string surname)
    {
        accountService = new AccountService();


        resultService = new TestResultService();
        dB.GetConnection();

        if (table == null)
        {
            Debug.Log("table is null");
            return;
        }
        // Очищаем таблицу перед заполнением
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }
        try
        {
            int resultTest1 = accountService.GetAccountNameSurname(name, surname).Id; // получаем список аккаунтов

            Debug.Log("id result" + resultTest1);

            var resultTest = resultService.GetResultForIDStudent(resultTest1); // получаем список аккаунтов

            if (resultTest == null)
            {
                Debug.Log("accounts list is empty");
                return;
            }

            fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

            // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
            GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
            panelHeader.transform.SetParent(parentObject.transform);
            RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
            rectTransform.localScale = Vector2.one;
            // Добавляем компонент Image на созданный объект
            Image panelImage = panelHeader.AddComponent<Image>();
            // Устанавливаем желаемый цвет
            Color color;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
            {
                panelImage.color = color;
            }
            //создаем ячейки таблицы на панеле-строке
            panelHeader.AddComponent<GridLayoutGroup>();
            panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 100);


            // Создаем заголовки для таблицы
            AddCellHeader(panelHeader, "Дата прохождения");
            AddCellHeader(panelHeader, "Название теста");
            AddCellHeader(panelHeader, "Результаты");


            foreach (var account in resultTest)
            {
                //верно ли подключен шрифт
                if (fontAsset == null)
                {
                    Debug.Log("font not found");
                    return;
                }

                // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
                GameObject panel = new GameObject("Panel", typeof(RectTransform));
                panel.transform.SetParent(parentObject.transform);
                RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
                rectTransformPanel.localScale = Vector2.one;

                // Добавляем компонент Image на созданный объект
                Image panelImagePanel = panel.AddComponent<Image>();

                // Устанавливаем желаемый цвет
                Color colorPanel;
                if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
                {
                    panelImagePanel.color = colorPanel;
                }

                //создаем ячейки таблицы на панеле-строке
                panel.AddComponent<GridLayoutGroup>();
                panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 100);


                //// Создаем новый элемент таблицы

                AddCell(panel, account.DatePassage);
                AddCell(panel, account.NameTest);
                AddCell(panel, account.Score.ToString());
            }
            ResizeContent(resultTest, parentObject, table);
        } catch { }

    }

    public void GetTableByNameSurnamePractice(GridLayoutGroup table, GameObject parentObject, string name, string surname)
    {
        accountService = new AccountService();


        resultService = new TestResultService();
        dB.GetConnection();

        if (table == null)
        {
            Debug.Log("table is null");
            return;
        }
        // Очищаем таблицу перед заполнением
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }
        try
        {
            int resultTest1 = accountService.GetAccountNameSurname(name, surname).Id; // получаем список аккаунтов

            Debug.Log("id result" + resultTest1);

            var resultTest = resultService.GetResultPracticeForIDStudent(resultTest1); // получаем список аккаунтов

            if (resultTest == null)
            {
                Debug.Log("accounts list is empty");
                return;
            }

            fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

            // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
            GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
            panelHeader.transform.SetParent(parentObject.transform);
            RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
            rectTransform.localScale = Vector2.one;
            // Добавляем компонент Image на созданный объект
            Image panelImage = panelHeader.AddComponent<Image>();
            // Устанавливаем желаемый цвет
            Color color;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
            {
                panelImage.color = color;
            }
            //создаем ячейки таблицы на панеле-строке
            panelHeader.AddComponent<GridLayoutGroup>();
            panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 100);


            // Создаем заголовки для таблицы
            AddCellHeader(panelHeader, "Дата прохождения");
            AddCellHeader(panelHeader, "Название тренажера");
            AddCellHeader(panelHeader, "Пройден ли тренажер");


            foreach (var account in resultTest)
            {
                //верно ли подключен шрифт
                if (fontAsset == null)
                {
                    Debug.Log("font not found");
                    return;
                }

                // Создаем новый игровой объект и добавляем к нему компонент RectTransform, который отвечает за расположение и размеры элемента на экране
                GameObject panel = new GameObject("Panel", typeof(RectTransform));
                panel.transform.SetParent(parentObject.transform);
                RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
                rectTransformPanel.localScale = Vector2.one;

                // Добавляем компонент Image на созданный объект
                Image panelImagePanel = panel.AddComponent<Image>();

                // Устанавливаем желаемый цвет
                Color colorPanel;
                if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
                {
                    panelImagePanel.color = colorPanel;
                }

                //создаем ячейки таблицы на панеле-строке
                panel.AddComponent<GridLayoutGroup>();
                panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 100);


                //// Создаем новый элемент таблицы

                AddCell(panel, account.DatePassage);
                AddCell(panel, account.NameSimulator);
                AddCell(panel, account.Passed);
            }
            ResizeContentPractice(resultTest, parentObject, table);
        }
        catch { }

    }

    //создание ячеек заголовка
    void AddCellHeader(GameObject panelHeader, string text)
    {
        TextMeshProUGUI header = Instantiate(new GameObject("Cell")).AddComponent<TextMeshProUGUI>();
        header.font = fontAsset;
        header.alignment = TextAlignmentOptions.Center;
        header.fontSize = 30;
        header.transform.SetParent(panelHeader.transform);
        header.text = text;
    }

    //создание ячеек таблицы
    void AddCell(GameObject panel, string text)
    {
        TextMeshProUGUI cellText = Instantiate(new GameObject("Cell")).AddComponent<TextMeshProUGUI>();
        cellText.font = fontAsset;
        cellText.alignment = TextAlignmentOptions.Center;
        cellText.fontSize = 30;
        cellText.transform.SetParent(panel.transform);
        cellText.text = text;
    }

    void ResizeContent(IEnumerable<BDTestResult> account, GameObject parentObject, GridLayoutGroup table)
    {
        int rowCount = (account.ToArray().Length) + 1;
        Debug.Log("rowCount " + rowCount);
        float height = table.cellSize.y * rowCount + (table.spacing.y * (rowCount - 1));
        Debug.Log("height " + height);
        RectTransform contentRectTransform = parentObject.transform.GetComponent<RectTransform>();
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.rect.width, height);
        Vector2 anchorPos = new Vector2(0f, 1f); // верхний левый угол
        table.transform.SetParent(contentRectTransform);
        Vector2 anchorPosition = new Vector2(0f, 1f); // верхний левый угол
        parentObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        parentObject.GetComponent<RectTransform>().anchorMin = anchorPosition;
        parentObject.GetComponent<RectTransform>().anchorMax = anchorPosition;
        parentObject.GetComponent<RectTransform>().pivot = anchorPosition;
    }

    void ResizeContentPractice(IEnumerable<BDSimulatorResult> account, GameObject parentObject, GridLayoutGroup table)
    {
        int rowCount = (account.ToArray().Length) + 1;
        Debug.Log("rowCount " + rowCount);
        float height = table.cellSize.y * rowCount + (table.spacing.y * (rowCount - 1));
        Debug.Log("height " + height);
        RectTransform contentRectTransform = parentObject.transform.GetComponent<RectTransform>();
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.rect.width, height);
        Vector2 anchorPos = new Vector2(0f, 1f); // верхний левый угол
        table.transform.SetParent(contentRectTransform);
        Vector2 anchorPosition = new Vector2(0f, 1f); // верхний левый угол
        parentObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        parentObject.GetComponent<RectTransform>().anchorMin = anchorPosition;
        parentObject.GetComponent<RectTransform>().anchorMax = anchorPosition;
        parentObject.GetComponent<RectTransform>().pivot = anchorPosition;
    }
}
