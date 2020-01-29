/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_Graph : MonoBehaviour
{

    public List<AgeDistribution> ageDistributions;
    [SerializeField] private Sprite dotSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    //private RectTransform dashTemplateX;
    //private RectTransform dashTemplateY;
    // private Dropdown dropdown;
    private List<GameObject> gameObjectList;
    public List<int> valueList = new List<int>() { };
    [SerializeField]
    public DataLoader dataLoader;
    private bool LinzData = false;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        //dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();
        //dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();


        //dropdown = transform.Find("Dropdown").GetComponent<Dropdown>();
        //Debug.Log(dropdown); 


        gameObjectList = new List<GameObject>();





    }

    public void outputGraph()
    {
        //List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
        //ShowGraph(valueList, -1, (int _i) => "Day " + (_i + 1), (float _f) => "$" + Mathf.RoundToInt(_f));
        dataLoader.LoadAllData();
        List<AgeDistribution> ageDistributions = dataLoader.ageDistributions;
        Debug.Log("#district: " + dataLoader.getDistricts().Count);
        foreach (var value in ageDistributions)
        {
            // Age Distribiution Graph for data for Linz 
            foreach (Districts district in dataLoader.getDistricts())
            {
                if(district.ToString() == "Linz")
                {
                    LinzData = true; 
                    Debug.Log("Linz "+LinzData); 
                }
                else
                {
                    LinzData = false;
                    graphContainer.sizeDelta = new Vector2(100, 100);
                }
                if (district == value.district)
                {
                    ShowGraph(value.ages, -1, (int _i) => " " + (_i + 1), (float _f) => " " + Mathf.RoundToInt(_f));
                }
            }
        }
        dataLoader.removeDistrict();
    }

    private void ShowGraph(List<int> valueList, int maxVisibleValueAmount = -1, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {

        // Debug.Log("Dropdown: " + buttonA.GetComponent<Dropdown>().value);
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        if (maxVisibleValueAmount <= 0)
        {
            maxVisibleValueAmount = valueList.Count;
        }

        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();

        float graphWidth = (graphContainer.sizeDelta.x) * 2;
        float graphHeight = graphContainer.sizeDelta.y;
        if (LinzData == true)
        {
            graphHeight = graphContainer.sizeDelta.y * 2;
            graphContainer.sizeDelta = new Vector2(100, 150);
        }

        float yMaximum = valueList[0];
        float yMinimum = valueList[0];

        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
        {
            int value = valueList[i];
            if (value > yMaximum)
            {
                yMaximum = value;
            }
            if (value < yMinimum)
            {
                yMinimum = value;
            }
        }
        Debug.Log("Max: "+yMaximum);
        Debug.Log("Min: " + yMinimum);

        float yDifference = yMaximum - yMinimum;
        if (yDifference <= 0)
        {
            yDifference = 5f;
        }
        yMaximum = yMaximum + (yDifference * 0.2f);
        yMinimum = yMinimum - (yDifference * 0.2f);

        yMinimum = 0f; // Start the graph at zero

        float xSize = graphWidth / (maxVisibleValueAmount + 1);

        int xIndex = 0;
        int separatorCount = 20;

        //GameObject lastDotGameObject = null;
        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
        {
            float xPosition = xSize + xIndex * xSize;
            float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            GameObject barGameObject = CreateBar(new Vector2(xPosition, yPosition), xSize * .8f);
            gameObjectList.Add(barGameObject);
            /*
            GameObject dotGameObject = CreateDot(new Vector2(xPosition, yPosition));
            gameObjectList.Add(dotGameObject);
            if (lastDotGameObject != null) {
                GameObject dotConnectionGameObject = CreateDotConnection(lastDotGameObject.GetComponent<RectTransform>().anchoredPosition, dotGameObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnectionGameObject);
            }
            lastDotGameObject = dotGameObject;
            */

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);

            if (i % 3 == 0)
            {
                labelX.anchoredPosition = new Vector2(xPosition, -7f);
                labelX.GetComponent<Text>().text = getAxisLabelX(i);
                gameObjectList.Add(labelX.gameObject);
            }



            /*RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, -3f);
            gameObjectList.Add(dashX.gameObject);*/

            xIndex++;
        }

        RectTransform labelY = Instantiate(labelTemplateY);
        float normalizedValue = 0;
        for (int i = 0; i <= separatorCount; i++)
        {
            labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            normalizedValue = i * 1f / separatorCount;
            //if (i % 2 == 0){
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
                labelY.GetComponent<Text>().text = getAxisLabelY((yMinimum + (normalizedValue * (yMaximum - yMinimum))));
                gameObjectList.Add(labelY.gameObject);
            //}

            /* RectTransform dashY = Instantiate(dashTemplateY);
             dashY.SetParent(graphContainer, false);
             dashY.gameObject.SetActive(true);
             dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
             gameObjectList.Add(dashY.gameObject);*/
        }

        labelY = Instantiate(labelTemplateY);
        labelY.SetParent(graphContainer, false);
        labelY.gameObject.SetActive(true);
        normalizedValue = 21 * 1f / separatorCount;
        //if (i % 2 == 0){
        labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
        foreach (Districts district in dataLoader.getDistricts())
        {
            labelY.GetComponent<Text>().text = district.ToString();
            labelY.GetComponent<Text>().color = new Color32(128, 12, 232, 91);
        }
        gameObjectList.Add(labelY.gameObject);
    }

    private GameObject CreateDot(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("dot", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = dotSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
        return gameObject;
    }

    private GameObject CreateBar(Vector2 graphPosition, float barWidth)
    {
        GameObject gameObject = new GameObject("bar", typeof(Image));
        //gameObject.GetComponent<Image>
        gameObject.transform.SetParent(graphContainer, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
        rectTransform.sizeDelta = new Vector2(barWidth, graphPosition.y);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(.5f, 0f);
        Image image = gameObject.GetComponent<Image>();
        image.color = new Color32(8, 91, 235, 92);
        return gameObject;
    }


    /* ---- Flats ----
    public void LoadHousingInformation()
    {
        string[] dataFloorSpace = DataLoadingHelpers.GetDataRows("HousingInformation/floorSpace");
        string[] dataFlatRooms = DataLoadingHelpers.GetDataRows("HousingInformation/flatRooms");
        string[] dataFlatsPerBuilding = DataLoadingHelpers.GetDataRows("HousingInformation/flatsPerBuilding");

        var districts = Enum.GetValues(typeof(Districts));
        housingInformation = new HousingInformation[districts.Length];
        housingInformation = DataLoadingHelpers.ParseFlatInformation<RoomsPerFlat>(housingInformation, dataFlatRooms, "RoomsPerFlat");
        housingInformation = DataLoadingHelpers.ParseFlatInformation<RoomsPerFlat>(housingInformation, dataFloorSpace, "FloorSpace");
        housingInformation = DataLoadingHelpers.ParseFlatInformation<RoomsPerFlat>(housingInformation, dataFlatsPerBuilding, "FlatsPerBuilding");
    }*/

}
