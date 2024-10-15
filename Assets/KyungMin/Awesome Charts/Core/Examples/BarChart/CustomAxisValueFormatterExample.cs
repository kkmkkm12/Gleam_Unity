using System.Collections;
using System.Collections.Generic;
using System.Data;
using AwesomeCharts;
using UnityEngine;

[ExecuteInEditMode]
public class CustomAxisValueFormatterExample : MonoBehaviour {

    private int SiblingIndex = 0;

    public BarChart barChart;
    public BarChartConfig config;
    private AxisBaseChart<BarData> axisBaseChart;

    private BarAxisValueFormatterConfig barAxisValueFormatterConfig;
    private BarData barDataSet;

    private string chartLabel = "";

    private bool barCompare = false;

    private void Awake()
    {
        axisBaseChart = barChart;

        
    }
    void OnEnable() {
        string parentParentName = transform.parent.parent.name;
        if (parentParentName.Contains("MainFunction1"))
        {
            SiblingIndex = transform.parent.GetSiblingIndex();
        }
        else if (parentParentName.Contains("MainFunction2"))
        {
            SiblingIndex = transform.parent.GetSiblingIndex() + 3;
        }

        string myParentName = gameObject.transform.parent.name;
        string[] mainArray = PlayerPrefs.GetString("MainSection1Setting").Split(",");
        if (myParentName.Contains("UnloadingVolume_Prefab"))
        {
            if (mainArray[SiblingIndex * 2].Equals("하차"))
            {
                if (mainArray[(SiblingIndex + 1) * 2 - 1].Equals("Bar(세로)"))
                {
                    barChart.HVset = false;
                    axisBaseChart.GridConfig.VerticalLinesCount = 0;
                    axisBaseChart.GridConfig.HorizontalLinesCount = 3;
                }
                else if (mainArray[(SiblingIndex + 1) * 2 - 1].Equals("Bar(가로)"))
                {
                    barChart.HVset = true;
                    axisBaseChart.GridConfig.VerticalLinesCount = 3;
                    axisBaseChart.GridConfig.HorizontalLinesCount = 0;
                }
                else if (mainArray[(SiblingIndex + 1) * 2 - 1].Equals("Bar(비교분석)"))
                {
                    barChart.HVset = false;
                    axisBaseChart.GridConfig.VerticalLinesCount = 0;
                    axisBaseChart.GridConfig.HorizontalLinesCount = 3;
                    barCompare = true;
                }
                chartLabel = "하차\n물류량";
            }
        }
        else if (myParentName.Contains("LoadingVolume_Prefab"))
        {
            if (mainArray[SiblingIndex * 2].Equals("상차"))
            {
                if (mainArray[(SiblingIndex + 1) * 2 - 1].Equals("Bar(세로)"))
                {
                    barChart.HVset = false;
                    axisBaseChart.GridConfig.VerticalLinesCount = 0;
                    axisBaseChart.GridConfig.HorizontalLinesCount = 3;
                }
                else if (mainArray[(SiblingIndex + 1) * 2 - 1].Equals("Bar(가로)"))
                {
                    barChart.HVset = true;
                    axisBaseChart.GridConfig.VerticalLinesCount = 3;
                    axisBaseChart.GridConfig.HorizontalLinesCount = 0;
                }
                else if (mainArray[(SiblingIndex + 1) * 2 - 1].Equals("Bar(비교분석)"))
                {
                    barChart.HVset = false;
                    axisBaseChart.GridConfig.VerticalLinesCount = 0;
                    axisBaseChart.GridConfig.HorizontalLinesCount = 3;
                    barCompare = true;
                }
                chartLabel = "상차\n물류량";
            }
        }
        else if (myParentName.Contains("TotalVolume_Prefab"))
        {
            if (mainArray[SiblingIndex * 2].Equals("물동량"))
            {
                if (mainArray[(SiblingIndex + 1) * 2 - 1].Equals("Bar(세로)"))
                {
                    barChart.HVset = false;
                    axisBaseChart.GridConfig.VerticalLinesCount = 0;
                    axisBaseChart.GridConfig.HorizontalLinesCount = 3;
                }
                else if (mainArray[(SiblingIndex + 1) * 2 - 1].Equals("Bar(가로)"))
                {
                    barChart.HVset = true;
                    axisBaseChart.GridConfig.VerticalLinesCount = 3;
                    axisBaseChart.GridConfig.HorizontalLinesCount = 0;
                }
                else if (mainArray[(SiblingIndex + 1) * 2 - 1].Equals("Bar(비교분석)"))
                {
                    barChart.HVset = false;
                    axisBaseChart.GridConfig.VerticalLinesCount = 0;
                    axisBaseChart.GridConfig.HorizontalLinesCount = 3;
                    barCompare = true;
                }
                chartLabel = "물동량";
            }
        }
    }

    public void DrowGraph(int[] value, Transform callObj)
    {
        barAxisValueFormatterConfig = barChart.AxisConfig.HorizontalAxisConfig.ValueFormatterConfig;
        //barDataSet = barChart.data.DataSets[1];

        SetupCustomValueFormatter();

        List<BarEntry> barEntries = new List<BarEntry>();
        BarDataSet dataSet = new BarDataSet();
        BarData barData = new BarData();
        if (!barCompare)
        {
            barEntries.Add(new BarEntry(position: 0, value: value[4]));
            /*barEntries.Add(new BarEntry(position: 1, value: 20));
            barEntries.Add(new BarEntry(position: 2, value: 30));*/

            // 2. BarDataSet 생성 및 BarEntry 추가
            dataSet.AddEntry(barEntries[0]);

            // 3. 데이터셋의 제목과 색상 설정
            dataSet.Title = "";
            dataSet.BarColors.Add(Color.white);

            /*barEntries.Add(new BarEntry(position: 0, value: 40));

            BarDataSet dataSet2 = new BarDataSet();
            dataSet2.AddEntry(barEntries[3]);

            dataSet2.Title = "Second Data";
            dataSet2.BarColors.Add(Color.black);*/

            // 4. BarData 객체 생성 및 데이터셋 추가
            barData.AddDataSet(dataSet);
            //barData.AddDataSet(dataSet2);

            // 5. BarChart에 데이터 설정
            barChart.data = barData;

            // 6. 차트 업데이트
            barChart.SetDirty();

            if (barAxisValueFormatterConfig != null)
            {
                // customValues 리스트의 값을 가져오기
                List<string> values = barAxisValueFormatterConfig.CustomValues;
                values.Clear();
                //Axis Config - Horizontal Axis Config - Value Formatter Config - Custom Values에 추가하는 코드
                values.Add(chartLabel);
            }
            else
            {
                Debug.LogWarning("BarAxisValueFormatterConfig is not assigned.");
            }
            //Debug.Log(barChart.data.DataSets[0].Entries[0].Position + " " + barChart.data.DataSets[0].Entries[0].Value);
        }
        else
        {
            for(int i = 0; i < value.Length; i++)
            {
                barEntries.Add(new BarEntry(position: i, value: value[i]));
                dataSet.AddEntry(barEntries[i]);
            }
            barData.AddDataSet(dataSet);
            barChart.data = barData;
            barChart.SetDirty();
            if (barAxisValueFormatterConfig != null)
            {
                // customValues 리스트의 값을 가져오기
                List<string> values = barAxisValueFormatterConfig.CustomValues;
                values.Clear();
                //Axis Config - Horizontal Axis Config - Value Formatter Config - Custom Values에 추가하는 코드
                values.Add("4일전");
                values.Add("3일전");
                values.Add("2일전");
                values.Add("1일전");
                values.Add("오늘");
            }
            else
            {
                Debug.LogWarning("BarAxisValueFormatterConfig is not assigned.");
            }
        }
    }

    private void SetupCustomValueFormatter () {
        if (barChart == null)
            return;

        barChart.CustomVerticalAxisValueFormatter = new PriceValueFormatter ();
    }

    private class PriceValueFormatter : AxisValueFormatter {
        public string FormatAxisValue (int index, float value, float minValue, float maxValue) {
            return value.ToString ("F" + 0) + "";
        }
    }
}