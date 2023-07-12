using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;


public class DataControlerForTeory : MonoBehaviour
{
    AccountService accountService;
    DB dB;
    private OrganSpawner organSpawner; // ������ �� ������ OrganSpawner

    public DataControlerForTeory()
    {
        dB = new DB();
    }

    public TMP_FontAsset fontAsset;

    public static string savedName;
    public string imagesPath; // ���� � ����� � �������������

    public static int currentPageIndex = 0;
    public static int saveCurrentPageIndex = 0;
    public static string[] buttonNamesList;
    int indexPanels = 0;
    public GameObject Organ = null;

    // Start is called before the first frame update
    void Start()
    {
        accountService = new AccountService();
        organSpawner = new OrganSpawner(); 
    }

    public void GetTableOrgans(GridLayoutGroup table, GameObject parentObject, int IdSystem, GridLayoutGroup tableTeory, GameObject parentObjectTeory,
        GameObject ImageTeory, GameObject contentTextTeory, TextMeshProUGUI TextTeory, GameObject PanelImageTeory, GameObject Humen)
    {
        accountService = new AccountService();
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


        var organs = accountService.GetOrgansByType(IdSystem); // �������� ������ �������

        //��������� ��� ������ �����������
        if (organs == null)
        {
            Debug.Log("organs list is empty");
            return;
        }
        string organListStr = string.Join(", ", organs.Select(x => x.Name).ToArray());

        //�����
        fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

        // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
        GameObject panelHeader = new GameObject("PanelHeader", typeof(RectTransform));
        panelHeader.transform.SetParent(parentObject.transform);
        RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        rectTransform.localPosition = Vector2.zero;
        rectTransform.localRotation = Quaternion.identity;
        // ��������� ��������� Image �� ��������� ������
        Image panelImage = panelHeader.AddComponent<Image>();
        // ������������� �������� ����
        Color color;
        if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
        {
            panelImage.color = color;
        }

        // ������� ��������� ��� �������
        AddCellHeader(panelHeader, "������");


        foreach (var organ in organs)
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
            rectTransformPanel.localPosition = Vector2.zero;
            rectTransformPanel.localRotation = Quaternion.identity;

            // ��������� ��������� Image �� ��������� ������
            Image panelImagePanel = panel.AddComponent<Image>();

            // ������������� �������� ����
            Color colorPanel;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
            {
                panelImagePanel.color = colorPanel;
            }


            // ��������� ��������� EventTrigger � ������
            EventTrigger eventTrigger = panel.AddComponent<EventTrigger>();

            // ������� ����� Entry ��� ��������� ������� PointerClick
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            // ������ ����� ������ OnPanelClicked � �������� �������-�����������
            entry.callback.AddListener((data) => { OnPanelClicked((PointerEventData)data); });
            // ��������� ����� Entry � ��������� EventTrigger
            eventTrigger.triggers.Add(entry);

            // ������� ����� Entry ��� ��������� ������� PointerEnter � PointerExit:
            EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
            entryPointerEnter.eventID = EventTriggerType.PointerEnter;
            entryPointerEnter.callback.AddListener((data) => { OnPointerEnter(panelImagePanel); });

            EventTrigger.Entry entryPointerDown = new EventTrigger.Entry();
            entryPointerDown.eventID = EventTriggerType.PointerDown;
            entryPointerDown.callback.AddListener((data) => { OnPointerDown(panelImagePanel); });

            EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
            entryPointerExit.eventID = EventTriggerType.PointerExit;
            entryPointerExit.callback.AddListener((data) => { OnPointerExit(panelImagePanel); });

            // ������� ����� Entry ��� ��������� ������� PointerClick:
            EventTrigger.Entry entryPointerClick = new EventTrigger.Entry();
            entryPointerClick.eventID = EventTriggerType.PointerClick;
            entryPointerClick.callback.AddListener((data) => { OnPanelClicked((PointerEventData)data); });

            eventTrigger.triggers.Add(entryPointerEnter);
            eventTrigger.triggers.Add(entryPointerDown);
            eventTrigger.triggers.Add(entryPointerExit);
            eventTrigger.triggers.Add(entryPointerClick);

            // ��������� ������ ��������� ������� PointerEnter � PointerExit, ������� ����� ������ ���� ������:
            void OnPointerDown(Image panelImage)
            {
                Color color;
                if (ColorUtility.TryParseHtmlString("#0093FF", out color)) // ���� ��� �������
                {
                    panelImage.color = color;
                }
            }

            void OnPointerEnter(Image panelImage)
            {
                Color color;
                if (ColorUtility.TryParseHtmlString("#21A9AB80", out color)) // ���� ��� ���������
                {
                    panelImage.color = color;
                }
            }

            void OnPointerExit(Image panelImage)
            {
                Color color;
                if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color)) // �������� ����
                {
                    panelImage.color = color;
                }
            }

            void OnPanelClicked(PointerEventData eventData)
            {
                // ���������� ������, �� ������� ��������
                var panel = eventData.pointerPress;

                // �������� ����� �� ������ ������ ������-������
                var textComponent = panel.GetComponentInChildren<TextMeshProUGUI>();
                var name = textComponent.text;
                int IdOrgan = accountService.GetOrganByName(name).ID;

                //���������� ����� �� id � ����


                if (Organ != null)
                {
                    Debug.Log("��������" + Organ.name);

                    Organ.SetActive(false);
                }

                Debug.Log("����� ������ �����");

                Transform parentTransform = Humen.transform;
                Organ = parentTransform.GetChild(IdOrgan-1).gameObject;
                Transform parent = Humen.transform.parent;
                // ��������� �������� ������� (������ ������� � ��)
                for (int j = 0; j < parent.childCount-1; j++)
                {
                    Transform childTransform = parent.GetChild(j);
                    childTransform.gameObject.SetActive(false);
                }
                // ������ ����� ��������
                Debug.Log("��������");
                Organ.SetActive(true);
               

                // �������� �������� name � ����� ���������
                HandleIdClicked(name);

                GetTableTeory(tableTeory, parentObjectTeory, IdOrgan, ImageTeory, contentTextTeory, TextTeory, PanelImageTeory);
            }

            void HandleIdClicked(string name)
            {
                savedName = name;
                Debug.Log($"Clicked on Organ: {name}");
            }

            //// ������� ����� ������� �������


            AddCell(panel, organ.Name);

        }

        ResizeContentOrgan(organs, parentObject, table);

    }

    public void GetTableTeory(GridLayoutGroup table, GameObject parentObject, int IdOrgan,
        GameObject ImageTeory, GameObject contentTextTeory, TextMeshProUGUI TextTeory, GameObject PanelImageTeory)
    {
        accountService = new AccountService();
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


        var teorys = accountService.GetTeoryByOrgan(IdOrgan); // �������� ������ �������

        //��������� ��� ������ �����������
        if (teorys == null)
        {
            Debug.Log("accounts list is empty");
            return;
        }

        //�����
        fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");

        // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
        GameObject panelHeader = new GameObject("PanelHeader", typeof(RectTransform));
        panelHeader.transform.SetParent(parentObject.transform);
        RectTransform rectTransform = panelHeader.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        rectTransform.localPosition = Vector2.zero;
        rectTransform.localRotation = Quaternion.identity;
        // ��������� ��������� Image �� ��������� ������
        Image panelImage = panelHeader.AddComponent<Image>();
        // ������������� �������� ����
        Color color;
        if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color))
        {
            panelImage.color = color;
        }

        // ������� ��������� ��� �������
        AddCellHeader(panelHeader, "������");


        foreach (var teory in teorys)
        {
            //����� �� ��������� �����
            if (fontAsset == null)
            {
                Debug.Log("font not found");
                return;
            }

            // ������� ����� ������� ������ � ��������� � ���� ��������� RectTransform, ������� �������� �� ������������ � ������� �������� �� ������
            GameObject panel = new GameObject("Panel"+ indexPanels, typeof(RectTransform));
            indexPanels++;
            panel.transform.SetParent(parentObject.transform);
            RectTransform rectTransformPanel = panel.GetComponent<RectTransform>();
            rectTransformPanel.localScale = Vector2.one;
            rectTransformPanel.localPosition = Vector2.zero;
            rectTransformPanel.localRotation = Quaternion.identity;

            // ��������� ��������� Image �� ��������� ������
            Image panelImagePanel = panel.AddComponent<Image>();

            // ������������� �������� ����
            Color colorPanel;
            if (ColorUtility.TryParseHtmlString("#A2EAFB80", out colorPanel))
            {
                panelImagePanel.color = colorPanel;
            }

            // ��������� ��������� EventTrigger � ������
             EventTrigger eventTrigger = panel.AddComponent<EventTrigger>();

            // ������� ����� Entry ��� ��������� ������� PointerClick
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            // ������ ����� ������ OnPanelClicked � �������� �������-�����������
            entry.callback.AddListener((data) => { OnPanelClicked((PointerEventData)data); });
            // ��������� ����� Entry � ��������� EventTrigger
            eventTrigger.triggers.Add(entry);


            // ������� ����� Entry ��� ��������� ������� PointerEnter � PointerExit:
            EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
            entryPointerEnter.eventID = EventTriggerType.PointerEnter;
            entryPointerEnter.callback.AddListener((data) => { OnPointerEnter(panelImagePanel); });

            EventTrigger.Entry entryPointerDown = new EventTrigger.Entry();
            entryPointerDown.eventID = EventTriggerType.PointerDown;
            entryPointerDown.callback.AddListener((data) => { OnPointerDown(panelImagePanel); });

            EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
            entryPointerExit.eventID = EventTriggerType.PointerExit;
            entryPointerExit.callback.AddListener((data) => { OnPointerExit(panelImagePanel); });

            // ������� ����� Entry ��� ��������� ������� PointerClick:
            EventTrigger.Entry entryPointerClick = new EventTrigger.Entry();
            entryPointerClick.eventID = EventTriggerType.PointerClick;
            entryPointerClick.callback.AddListener((data) => { OnPanelClicked((PointerEventData)data); });

            eventTrigger.triggers.Add(entryPointerEnter);
            eventTrigger.triggers.Add(entryPointerDown);
            eventTrigger.triggers.Add(entryPointerExit);
            eventTrigger.triggers.Add(entryPointerClick);

            // ��������� ������ ��������� ������� PointerEnter � PointerExit, ������� ����� ������ ���� ������:
            void OnPointerDown(Image panelImage)
            {
                Color color;
                if (ColorUtility.TryParseHtmlString("#0093FF", out color)) // ���� ��� �������
                {
                    panelImage.color = color;
                }
            }

            void OnPointerEnter(Image panelImage)
            {
                Color color;
                if (ColorUtility.TryParseHtmlString("#21A9AB80", out color)) // ���� ��� ���������
                {
                    panelImage.color = color;
                }
            }

            void OnPointerExit(Image panelImage)
            {
                Color color;
                if (ColorUtility.TryParseHtmlString("#A2EAFB80", out color)) // �������� ����
                {
                    panelImage.color = color;
                }
                currentPageIndex = 0;
            }

            void OnPanelClicked(PointerEventData eventData)
            {
                // ���������� ������, �� ������� ��������
                var panel = eventData.pointerPress;

                // �������� ����� �� ������ ������ ������-������
                var textComponent = panel.GetComponentInChildren<TextMeshProUGUI>();
                var name = textComponent.text;

                int panelIndex = panel.transform.GetSiblingIndex();

                if (currentPageIndex==0)
                {
                    currentPageIndex = panelIndex;
                    saveCurrentPageIndex = panelIndex;
                }
                

                Debug.Log("index page = " + currentPageIndex);

                Image image = ImageTeory.GetComponent<Image>();
                
                // �������� �������� id � ����� ���������
                HandleIdClicked(name);

                TextTeory.font = fontAsset;
                TextTeory.alignment = TextAlignmentOptions.TopLeft;
                TextTeory.fontSize = 35;
                TextTeory.transform.SetParent(contentTextTeory.transform);
                TextTeory.text = accountService.GetTeoryByName(name).Teory;
                RectTransform rectTransform = TextTeory.GetComponent<RectTransform>();
                float preferredWidth = rectTransform.sizeDelta.x;
                float preferredHeight = TextTeory.preferredHeight;
                rectTransform.sizeDelta = new Vector2(preferredWidth, preferredHeight);
                RectTransform contentRectTransform = contentTextTeory.transform.GetComponent<RectTransform>();
                contentRectTransform.sizeDelta = new Vector2(0, preferredHeight);


                ChangeImage(accountService.GetTeoryByName(name).Image, image, PanelImageTeory);

            }

            void HandleIdClicked(string name)
            {
                savedName = name;
                Debug.Log($"Clicked on Organ: {name}");
            }

            //// ������� ����� ������� �������


            AddCell(panel, teory.Name);

        }

        ResizeContentTeory(teorys, parentObject, table);

    }

    void AddCellHeader(GameObject panelHeader, string text)
    {
        TextMeshProUGUI header = Instantiate(new GameObject("Cell", typeof(RectTransform))).AddComponent<TextMeshProUGUI>();

        header.transform.SetParent(panelHeader.transform);
        RectTransform rectTransformPanel = header.GetComponent<RectTransform>();
        rectTransformPanel.localScale = Vector3.one;
        rectTransformPanel.localPosition = Vector2.zero;
        rectTransformPanel.localRotation = Quaternion.identity;
        float yRotation = 0.01f; // ���� �������� �� ��� Y

        Vector3 rotationVector = new Vector3(0f, yRotation, 0f);
        rectTransformPanel.localRotation *= Quaternion.Euler(rotationVector); // ��������� �������

        rectTransformPanel.anchorMin = Vector2.zero;
        rectTransformPanel.anchorMax = Vector2.one;
        rectTransformPanel.offsetMin = Vector2.zero;
        rectTransformPanel.offsetMax = Vector2.zero;

        header.font = fontAsset;
        header.alignment = TextAlignmentOptions.Center;
        header.fontSize = 30;
        header.transform.SetParent(panelHeader.transform);
        header.text = text;
    }

    void AddCell(GameObject panel, string text)
    {
        TextMeshProUGUI cellText = Instantiate(new GameObject("Cell", typeof(RectTransform))).AddComponent<TextMeshProUGUI>();

        cellText.transform.SetParent(panel.transform);
        RectTransform rectTransformPanel = cellText.GetComponent<RectTransform>();
        rectTransformPanel.localScale = Vector3.one;
        rectTransformPanel.localPosition = Vector2.zero;
        rectTransformPanel.localRotation = Quaternion.identity;
        float yRotation = 0.01f; // ���� �������� �� ��� Y

        Vector3 rotationVector = new Vector3(0f, yRotation, 0f);
        rectTransformPanel.localRotation *= Quaternion.Euler(rotationVector); // ��������� �������

        rectTransformPanel.anchorMin = Vector2.zero;
        rectTransformPanel.anchorMax = Vector2.one;
        rectTransformPanel.offsetMin = Vector2.zero;
        rectTransformPanel.offsetMax = Vector2.zero;
        cellText.font = fontAsset;
        cellText.alignment = TextAlignmentOptions.Center;
        cellText.fontSize = 20;
        cellText.transform.SetParent(panel.transform);
        cellText.text = text;
    }

    void ResizeContentOrgan(IEnumerable<BDOrgans> organ, GameObject parentObject, GridLayoutGroup table)
    {
        int rowCount = (organ.ToArray().Length) + 1;
        float height = table.cellSize.y * rowCount + (table.spacing.y * (rowCount - 1));
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
    void ResizeContentTeory(IEnumerable<BDTeoryForOrgans> organ, GameObject parentObject, GridLayoutGroup table)
    {
        int rowCount = (organ.ToArray().Length) + 1;
        float height = table.cellSize.y * rowCount + (table.spacing.y * (rowCount - 1));
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

    public void ChangeImage(string buttonName, Image image, GameObject PanelImageTeory)
    {
        if (buttonName != null)
        {
            try
            {
                PanelImageTeory.SetActive(true);
                buttonNamesList = buttonName.Split(';');
                Sprite sprite = Resources.Load<Sprite>("Image/" + buttonNamesList[0]);

                if (sprite != null)
                {
                    // ������������� ����������� ������ ��� ���������� Image
                    image.sprite = sprite;
                }
                else
                {
                    Debug.Log("Sprite not found: " + buttonName);
                }
            }
            catch { }   
        }
        else
        {
            Debug.Log("This teory material is not have image.");           
            PanelImageTeory.SetActive(false);   
        }
    }
}
