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

        //��������� ������� �� GridLayoutGroup
        if (table == null)
        {
            Debug.Log("table is null");
            return;
        }
        // ������� ������� ����� �����������
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }


        var resultTest = resultService.GetResultForIDStudent(MainScene.savedIdStudent); // �������� ������ ���������

        Debug.Log(MainScene.savedIdStudent);
        Debug.Log(resultTest);

        //��������� ��� �������� �����������
        if (resultTest == null)
        {
            Debug.Log("accounts list is empty");
            return;
        }

        fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

        // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
        GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
        panelHeader.transform.SetParent(parentObject.transform);
        RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        // ��������� ��������� Image �� ��������� ������
        Image panelImage = panelHeader.AddComponent<Image>();
        // ������������� �������� ����
        Color color;
        if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
        {
            panelImage.color = color;
        }
        //������� ������ ������� �� ������-������
        panelHeader.AddComponent<GridLayoutGroup>();
        panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(400, 100);



        // ������� ��������� ��� �������
        AddCellHeader(panelHeader, "�������� �����");
        AddCellHeader(panelHeader, "���� �����������");
        AddCellHeader(panelHeader, "����������");


        foreach (var account in resultTest)
        {
            //����� �� ��������� �����
            if (fontAsset == null)
            {
                Debug.Log("font not found");
                return;
            }

            // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
            GameObject panel = new GameObject("Panel", typeof(RectTransform));
            panel.transform.SetParent(parentObject.transform);
            RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
            rectTransformPanel.localScale = Vector2.one;

            // ��������� ��������� Image �� ��������� ������
            Image panelImagePanel = panel.AddComponent<Image>();

            // ������������� �������� ����
            Color colorPanel;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
            {
                panelImagePanel.color = colorPanel;
            }

            //������� ������ ������� �� ������-������
            panel.AddComponent<GridLayoutGroup>();
            panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(400, 100);

            //// ������� ����� ������� �������

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

        //��������� ������� �� GridLayoutGroup
        if (table == null)
        {
            Debug.Log("table is null");
            return;
        }
        // ������� ������� ����� �����������
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }


        var resultTest = resultService.GetResultPracticeForIDStudent(MainScene.savedIdStudent); // �������� ������ ���������

        Debug.Log(MainScene.savedIdStudent);
        Debug.Log(resultTest);

        //��������� ��� �������� �����������
        if (resultTest == null)
        {
            Debug.Log("accounts list is empty");
            return;
        }

        fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

        // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
        GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
        panelHeader.transform.SetParent(parentObject.transform);
        RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        // ��������� ��������� Image �� ��������� ������
        Image panelImage = panelHeader.AddComponent<Image>();
        // ������������� �������� ����
        Color color;
        if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
        {
            panelImage.color = color;
        }
        //������� ������ ������� �� ������-������
        panelHeader.AddComponent<GridLayoutGroup>();
        panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(400, 100);



        // ������� ��������� ��� �������
        AddCellHeader(panelHeader, "�������� ����������");
        AddCellHeader(panelHeader, "���� �����������");
        AddCellHeader(panelHeader, "������� �� ���������");


        foreach (var account in resultTest)
        {
            //����� �� ��������� �����
            if (fontAsset == null)
            {
                Debug.Log("font not found");
                return;
            }

            // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
            GameObject panel = new GameObject("Panel", typeof(RectTransform));
            panel.transform.SetParent(parentObject.transform);
            RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
            rectTransformPanel.localScale = Vector2.one;

            // ��������� ��������� Image �� ��������� ������
            Image panelImagePanel = panel.AddComponent<Image>();

            // ������������� �������� ����
            Color colorPanel;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
            {
                panelImagePanel.color = colorPanel;
            }

            //������� ������ ������� �� ������-������
            panel.AddComponent<GridLayoutGroup>();
            panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(400, 100);

            //// ������� ����� ������� �������

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

        //��������� ������� �� GridLayoutGroup
        if (table == null)
        {
            Debug.Log("table is null");
            return;
        }
        // ������� ������� ����� �����������
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }


        var resultTest = resultService.GetResult(); // �������� ������ ���������

        Debug.Log(MainScene.savedIdStudent);
        Debug.Log(resultTest);

        //��������� ��� �������� �����������
        if (resultTest == null)
        {
            Debug.Log("accounts list is empty");
            return;
        }

        fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

        // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
        GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
        panelHeader.transform.SetParent(parentObject.transform);
        RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        // ��������� ��������� Image �� ��������� ������
        Image panelImage = panelHeader.AddComponent<Image>();
        // ������������� �������� ����
        Color color;
        if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
        {
            panelImage.color = color;
        }
        //������� ������ ������� �� ������-������
        panelHeader.AddComponent<GridLayoutGroup>();
        panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 100);



        // ������� ��������� ��� �������
        AddCellHeader(panelHeader, "��� ��������");
        AddCellHeader(panelHeader, "������� ��������");
        AddCellHeader(panelHeader, "�������� �����");
        AddCellHeader(panelHeader, "���� �����������");
        AddCellHeader(panelHeader, "����������");


        foreach (var account in resultTest)
        {
            //����� �� ��������� �����
            if (fontAsset == null)
            {
                Debug.Log("font not found");
                return;
            }

            // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
            GameObject panel = new GameObject("Panel", typeof(RectTransform));
            panel.transform.SetParent(parentObject.transform);
            RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
            rectTransformPanel.localScale = Vector2.one;

            // ��������� ��������� Image �� ��������� ������
            Image panelImagePanel = panel.AddComponent<Image>();

            // ������������� �������� ����
            Color colorPanel;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
            {
                panelImagePanel.color = colorPanel;
            }

            //������� ������ ������� �� ������-������
            panel.AddComponent<GridLayoutGroup>();
            panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 100);

            //// ������� ����� ������� �������
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

        //��������� ������� �� GridLayoutGroup
        if (table == null)
        {
            Debug.Log("table is null");
            return;
        }
        // ������� ������� ����� �����������
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }


        var resultTest = resultService.GetResultPractice(); // �������� ������ ���������

        Debug.Log(MainScene.savedIdStudent);
        Debug.Log(resultTest);

        //��������� ��� �������� �����������
        if (resultTest == null)
        {
            Debug.Log("accounts list is empty");
            return;
        }

        fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

        // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
        GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
        panelHeader.transform.SetParent(parentObject.transform);
        RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        // ��������� ��������� Image �� ��������� ������
        Image panelImage = panelHeader.AddComponent<Image>();
        // ������������� �������� ����
        Color color;
        if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
        {
            panelImage.color = color;
        }
        //������� ������ ������� �� ������-������
        panelHeader.AddComponent<GridLayoutGroup>();
        panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 100);



        // ������� ��������� ��� �������
        AddCellHeader(panelHeader, "��� ��������");
        AddCellHeader(panelHeader, "������� ��������");
        AddCellHeader(panelHeader, "�������� ���������");
        AddCellHeader(panelHeader, "���� �����������");
        AddCellHeader(panelHeader, "������� ��");


        foreach (var account in resultTest)
        {
            //����� �� ��������� �����
            if (fontAsset == null)
            {
                Debug.Log("font not found");
                return;
            }

            // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
            GameObject panel = new GameObject("Panel", typeof(RectTransform));
            panel.transform.SetParent(parentObject.transform);
            RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
            rectTransformPanel.localScale = Vector2.one;

            // ��������� ��������� Image �� ��������� ������
            Image panelImagePanel = panel.AddComponent<Image>();

            // ������������� �������� ����
            Color colorPanel;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
            {
                panelImagePanel.color = colorPanel;
            }

            //������� ������ ������� �� ������-������
            panel.AddComponent<GridLayoutGroup>();
            panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 100);

            //// ������� ����� ������� �������
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
        // ������� ������� ����� �����������
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }
        try
        {
            int resultTest1 = accountService.GetAccountNameSurname(name, surname).Id; // �������� ������ ���������

            Debug.Log("id result" + resultTest1);

            var resultTest = resultService.GetResultForIDStudent(resultTest1); // �������� ������ ���������

            if (resultTest == null)
            {
                Debug.Log("accounts list is empty");
                return;
            }

            fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

            // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
            GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
            panelHeader.transform.SetParent(parentObject.transform);
            RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
            rectTransform.localScale = Vector2.one;
            // ��������� ��������� Image �� ��������� ������
            Image panelImage = panelHeader.AddComponent<Image>();
            // ������������� �������� ����
            Color color;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
            {
                panelImage.color = color;
            }
            //������� ������ ������� �� ������-������
            panelHeader.AddComponent<GridLayoutGroup>();
            panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 100);


            // ������� ��������� ��� �������
            AddCellHeader(panelHeader, "���� �����������");
            AddCellHeader(panelHeader, "�������� �����");
            AddCellHeader(panelHeader, "����������");


            foreach (var account in resultTest)
            {
                //����� �� ��������� �����
                if (fontAsset == null)
                {
                    Debug.Log("font not found");
                    return;
                }

                // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
                GameObject panel = new GameObject("Panel", typeof(RectTransform));
                panel.transform.SetParent(parentObject.transform);
                RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
                rectTransformPanel.localScale = Vector2.one;

                // ��������� ��������� Image �� ��������� ������
                Image panelImagePanel = panel.AddComponent<Image>();

                // ������������� �������� ����
                Color colorPanel;
                if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
                {
                    panelImagePanel.color = colorPanel;
                }

                //������� ������ ������� �� ������-������
                panel.AddComponent<GridLayoutGroup>();
                panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 100);


                //// ������� ����� ������� �������

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
        // ������� ������� ����� �����������
        foreach (Transform child in table.transform)
        {
            Destroy(child.gameObject);
        }
        try
        {
            int resultTest1 = accountService.GetAccountNameSurname(name, surname).Id; // �������� ������ ���������

            Debug.Log("id result" + resultTest1);

            var resultTest = resultService.GetResultPracticeForIDStudent(resultTest1); // �������� ������ ���������

            if (resultTest == null)
            {
                Debug.Log("accounts list is empty");
                return;
            }

            fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

            // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
            GameObject panelHeader = new GameObject("Panel", typeof(RectTransform));
            panelHeader.transform.SetParent(parentObject.transform);
            RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
            rectTransform.localScale = Vector2.one;
            // ��������� ��������� Image �� ��������� ������
            Image panelImage = panelHeader.AddComponent<Image>();
            // ������������� �������� ����
            Color color;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
            {
                panelImage.color = color;
            }
            //������� ������ ������� �� ������-������
            panelHeader.AddComponent<GridLayoutGroup>();
            panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 100);


            // ������� ��������� ��� �������
            AddCellHeader(panelHeader, "���� �����������");
            AddCellHeader(panelHeader, "�������� ���������");
            AddCellHeader(panelHeader, "������� �� ��������");


            foreach (var account in resultTest)
            {
                //����� �� ��������� �����
                if (fontAsset == null)
                {
                    Debug.Log("font not found");
                    return;
                }

                // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
                GameObject panel = new GameObject("Panel", typeof(RectTransform));
                panel.transform.SetParent(parentObject.transform);
                RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
                rectTransformPanel.localScale = Vector2.one;

                // ��������� ��������� Image �� ��������� ������
                Image panelImagePanel = panel.AddComponent<Image>();

                // ������������� �������� ����
                Color colorPanel;
                if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
                {
                    panelImagePanel.color = colorPanel;
                }

                //������� ������ ������� �� ������-������
                panel.AddComponent<GridLayoutGroup>();
                panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 100);


                //// ������� ����� ������� �������

                AddCell(panel, account.DatePassage);
                AddCell(panel, account.NameSimulator);
                AddCell(panel, account.Passed);
            }
            ResizeContentPractice(resultTest, parentObject, table);
        }
        catch { }

    }

    //�������� ����� ���������
    void AddCellHeader(GameObject panelHeader, string text)
    {
        TextMeshProUGUI header = Instantiate(new GameObject("Cell")).AddComponent<TextMeshProUGUI>();
        header.font = fontAsset;
        header.alignment = TextAlignmentOptions.Center;
        header.fontSize = 30;
        header.transform.SetParent(panelHeader.transform);
        header.text = text;
    }

    //�������� ����� �������
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
        Vector2 anchorPos = new Vector2(0f, 1f); // ������� ����� ����
        table.transform.SetParent(contentRectTransform);
        Vector2 anchorPosition = new Vector2(0f, 1f); // ������� ����� ����
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
        Vector2 anchorPos = new Vector2(0f, 1f); // ������� ����� ����
        table.transform.SetParent(contentRectTransform);
        Vector2 anchorPosition = new Vector2(0f, 1f); // ������� ����� ����
        parentObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        parentObject.GetComponent<RectTransform>().anchorMin = anchorPosition;
        parentObject.GetComponent<RectTransform>().anchorMax = anchorPosition;
        parentObject.GetComponent<RectTransform>().pivot = anchorPosition;
    }
}
