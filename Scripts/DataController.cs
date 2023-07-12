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

public class DataController : MonoBehaviour
{

    public GridLayoutGroup table;
    public TextMeshProUGUI textMeshProUGUI;
    public TMP_FontAsset fontAsset;
    AccountService accountService;

    public static int savedId;

    DB dB;
    public DataController()
    {
        dB = new DB();
    }

    void Start()
    {
        accountService = new AccountService();        
    }

    public void GetTableAll(GridLayoutGroup table, GameObject parentObject, GameObject PanelDellOrChange, TMP_InputField updateInputAccess,
        TMP_InputField updateInputName, TMP_InputField updateInputSurname, TMP_InputField updateInputDate, TMP_InputField updateInputLogin, TMP_InputField updateInputPassword)
    {
        accountService = new AccountService();
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

        
        var accounts = accountService.GetAccounts(); // получаем список аккаунтов

        //проверяем что аккаунты считываются
        if (accounts == null )
        {
            Debug.Log("accounts list is empty");
            return;
        }

        //шрифт
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
        panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(200, 100);

        

        // Создаем заголовки для таблицы
        AddCellHeader(panelHeader, "ID");
        AddCellHeader(panelHeader, "Уровень доступа");
        AddCellHeader(panelHeader, "Имя");
        AddCellHeader(panelHeader, "Фамилия");
        AddCellHeader(panelHeader, "Дата рождения");
        AddCellHeader(panelHeader, "Логин");
        AddCellHeader(panelHeader, "Пароль");
        

        foreach (var account in accounts)
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
            panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(200, 100);

            // Добавляем компонент EventTrigger к панели
            EventTrigger eventTrigger = panel.AddComponent<EventTrigger>();

            // Создаем новый Entry для обработки событий PointerClick
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            // Задаем вызов метода OnPanelClicked в качестве функции-обработчика
            entry.callback.AddListener((data) => { OnPanelClicked((PointerEventData)data); });
            // Добавляем новый Entry в компонент EventTrigger
            eventTrigger.triggers.Add(entry);

            void OnPanelClicked(PointerEventData eventData)
            {
                //отобразить панель изменить/удалить
                PanelDellOrChange.SetActive(true);
                //перемещение панели в место клика
                PanelDellOrChange.transform.position = eventData.position;

                // Определяем панель, на которую кликнули
                var panel = eventData.pointerPress;

                // Получаем текст из первой ячейки панели-строки
                var textComponent = panel.GetComponentInChildren<TextMeshProUGUI>();
                var id = textComponent.text;                

                var textAccessLevel = panel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
                var AccessLevel = textAccessLevel.text;
                updateInputAccess.text = AccessLevel;

                var textName = panel.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
                var Name = textName.text;
                updateInputName.text = Name;

                var textSurname = panel.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>();
                var Surname = textSurname.text;
                updateInputSurname.text = Surname;

                var textDateOfBirth = panel.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>();
                var DateOfBirth = textDateOfBirth.text;
                updateInputDate.text = DateOfBirth;

                var textLogin = panel.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>();
                var Login = textLogin.text;
                updateInputLogin.text = Login;

                var textPassword = panel.transform.GetChild(6).GetComponentInChildren<TextMeshProUGUI>();
                var Password = textPassword.text;
                updateInputPassword.text = Password;



                // Передаем значение id в метод обработки
                HandleIdClicked(id);
            }

            void HandleIdClicked(string id)
            {
                savedId = int.Parse(id);
                Debug.Log($"Clicked on ID: {id}");
            }

            //// Создаем новый элемент таблицы

            AddCell(panel, account.Id.ToString());
            AddCell(panel, account.AccessLevel.ToString());
            AddCell(panel, account.Name);
            AddCell(panel, account.Surname);
            AddCell(panel, account.DateOfBirth);
            AddCell(panel, account.Login);
            AddCell(panel, account.Password);           
        }

        ResizeContent(accounts, parentObject, table);

    }
    public void GetTableByNameSurname(GridLayoutGroup table, GameObject parentObject, string name, string surname, GameObject PanelDellOrChange)
    {
        accountService = new AccountService();
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


        var accounts = accountService.GetAccountForNameSurname(name, surname); // получаем список аккаунтов

        if (accounts == null)
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
        panelHeader.GetComponent<GridLayoutGroup>().cellSize = new Vector2(200, 100);


        // Создаем заголовки для таблицы
        AddCellHeader(panelHeader, "ID");
        AddCellHeader(panelHeader, "Уровень доступа");
        AddCellHeader(panelHeader, "Имя");
        AddCellHeader(panelHeader, "Фамилия");
        AddCellHeader(panelHeader, "Дата рождения");
        AddCellHeader(panelHeader, "Логин");
        AddCellHeader(panelHeader, "Пароль");


        foreach (var account in accounts)
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
            panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(200, 100);

            // Добавляем компонент EventTrigger к панели
            EventTrigger eventTrigger = panel.AddComponent<EventTrigger>();

            // Создаем новый Entry для обработки событий PointerClick
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            // Задаем вызов метода OnPanelClicked в качестве функции-обработчика
            entry.callback.AddListener((data) => { OnPanelClicked((PointerEventData)data); });
            // Добавляем новый Entry в компонент EventTrigger
            eventTrigger.triggers.Add(entry);

            void OnPanelClicked(PointerEventData eventData)
            {
                //отобразить панель изменить/удалить
                PanelDellOrChange.SetActive(true);
                //перемещение панели в место клика
                PanelDellOrChange.transform.position = eventData.position;

                // Определяем панель, на которую кликнули
                var panel = eventData.pointerPress;

                // Получаем текст из первой ячейки панели-строки
                var textComponent = panel.GetComponentInChildren<TextMeshProUGUI>();
                var id = textComponent.text;

                // Передаем значение id в метод обработки
                HandleIdClicked(id);
            }

            void HandleIdClicked(string id)
            {
                savedId = int.Parse(id);
                Debug.Log($"Clicked on ID: {id}");
                // Дальнейшие действия с полученным значением id
            }


            //// Создаем новый элемент таблицы

            AddCell(panel, account.Id.ToString());
            AddCell(panel, account.AccessLevel.ToString());
            AddCell(panel, account.Name);
            AddCell(panel, account.Surname);
            AddCell(panel, account.DateOfBirth);
            AddCell(panel, account.Login);
            AddCell(panel, account.Password);
        }

        ResizeContent(accounts, parentObject, table);

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

    //изменение длины content под количество строк в таблице, для нормальной работы скролера
    void ResizeContent(IEnumerable<BDAccount> account, GameObject parentObject, GridLayoutGroup table)
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
