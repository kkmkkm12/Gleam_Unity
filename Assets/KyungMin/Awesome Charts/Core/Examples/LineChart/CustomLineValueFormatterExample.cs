using System.Collections;
using System.Collections.Generic;
using System.Data;
using AwesomeCharts;
using UnityEngine;

public class CustomLineValueFormatterExample : MonoBehaviour
{
    public LineChart lineChart;

    private BasicValueFormatterConfig basicValueFormatterConfig;

    private void Awake()
    {
        int[] a = new int[12];
        int[] b = new int[12];
        int[] c = new int[12];
        a[0] = 80;
        a[1] = 30;
        a[2] = 20;
        a[3] = 60;
        a[4] = 90;
        a[5] = 20;
        a[6] = 130;
        a[7] = 60;
        a[8] = 30;
        a[9] = 20;
        a[10] = 30;
        a[11] = 40;

        b[0] = 20;
        b[1] = 60;
        b[2] = 80;
        b[3] = 20;
        b[4] = 60;
        b[5] = 70;
        b[6] = 30;
        b[7] = 90;
        b[8] = 50;
        b[9] = 10;
        b[10] = 60;
        b[11] = 80;

        c[0] = 70;
        c[1] = 33;
        c[2] = 90;
        c[3] = 60;
        c[4] = 80;
        c[5] = 20;
        c[6] = 60;
        c[7] = 60;
        c[8] = 30;
        c[9] = 10;
        c[10] = 130;
        c[11] = 60;


        DrowGraph(a, b, c);
        Debug.Log("한번");

        DrowGraph(a, b, c);
        Debug.Log("두번");

    }
    void OnEnable()
    {
        
    }

    public void DrowGraph(int[] value1, int[] value2, int[] value3)
    {
        basicValueFormatterConfig = lineChart.AxisConfig.VerticalAxisConfig.ValueFormatterConfig;
        //barDataSet = barChart.data.DataSets[1];

        SetupCustomValueFormatter();

        List<LineEntry> lineEntries1 = new List<LineEntry>();
        List<LineEntry> lineEntries2 = new List<LineEntry>();
        List<LineEntry> lineEntries3 = new List<LineEntry>();

        List<LineDataSet> dataSet = new List<LineDataSet>();
        LineData barData = new LineData();
        lineEntries1.Add(new LineEntry(position: 0, value: value1[3]));
        lineEntries1.Add(new LineEntry(position: 1, value: value1[1]));
        lineEntries1.Add(new LineEntry(position: 2, value: value1[2]));
        lineEntries1.Add(new LineEntry(position: 3, value: value1[0]));
        lineEntries1.Add(new LineEntry(position: 4, value: value1[8]));
        lineEntries1.Add(new LineEntry(position: 5, value: value1[4]));
        lineEntries1.Add(new LineEntry(position: 6, value: value1[9]));
        lineEntries1.Add(new LineEntry(position: 7, value: value1[7]));
        lineEntries1.Add(new LineEntry(position: 8, value: value1[5]));
        lineEntries1.Add(new LineEntry(position: 9, value: value1[10]));
        lineEntries1.Add(new LineEntry(position: 10, value: value1[6]));
        lineEntries1.Add(new LineEntry(position: 11, value: value1[11]));

        lineEntries2.Add(new LineEntry(position: 0, value: value2[3]));
        lineEntries2.Add(new LineEntry(position: 1, value: value2[1]));
        lineEntries2.Add(new LineEntry(position: 2, value: value2[2]));
        lineEntries2.Add(new LineEntry(position: 3, value: value2[0]));
        lineEntries2.Add(new LineEntry(position: 4, value: value2[8]));
        lineEntries2.Add(new LineEntry(position: 5, value: value2[4]));
        lineEntries2.Add(new LineEntry(position: 6, value: value2[9]));
        lineEntries2.Add(new LineEntry(position: 7, value: value2[7]));
        lineEntries2.Add(new LineEntry(position: 8, value: value2[5]));
        lineEntries2.Add(new LineEntry(position: 9, value: value2[10]));
        lineEntries2.Add(new LineEntry(position: 10, value: value2[6]));
        lineEntries2.Add(new LineEntry(position: 11, value: value2[11]));

        lineEntries3.Add(new LineEntry(position: 0, value: value3[3]));
        lineEntries3.Add(new LineEntry(position: 1, value: value3[1]));
        lineEntries3.Add(new LineEntry(position: 2, value: value3[2]));
        lineEntries3.Add(new LineEntry(position: 3, value: value3[0]));
        lineEntries3.Add(new LineEntry(position: 4, value: value3[8]));
        lineEntries3.Add(new LineEntry(position: 5, value: value3[4]));
        lineEntries3.Add(new LineEntry(position: 6, value: value3[9]));
        lineEntries3.Add(new LineEntry(position: 7, value: value3[7]));
        lineEntries3.Add(new LineEntry(position: 8, value: value3[5]));
        lineEntries3.Add(new LineEntry(position: 9, value: value3[10]));
        lineEntries3.Add(new LineEntry(position: 10, value: value3[6]));
        lineEntries3.Add(new LineEntry(position: 11, value: value3[11]));

        /*barEntries.Add(new BarEntry(position: 1, value: 20));
        barEntries.Add(new BarEntry(position: 2, value: 30));*/

        // 2. BarDataSet 생성 및 BarEntry 추가
        //dataSet.AddEntry(llineEntries1[0]);

        dataSet.Add(new LineDataSet("당일", lineEntries1));
        dataSet.Add(new LineDataSet("기준일", lineEntries2));
        dataSet.Add(new LineDataSet("지난주요일", lineEntries3));

        dataSet[0].UseBezier = true;
        dataSet[1].UseBezier = true;
        dataSet[2].UseBezier = true;

        dataSet[0].LineColor = new Color(1f, 0.913718f, 0f, 1f);
        dataSet[0].FillColor = new Color(1f, 0.913718f, 0f, 0.2980392f);

        dataSet[1].LineColor = new Color(1f, 1f, 1f, 1f);
        dataSet[1].FillColor = new Color(1f, 1f, 1f, 0.2980392f);

        dataSet[2].LineColor = new Color(1f, 0.9580836f, 0.5f, 1f);
        dataSet[2].FillColor = new Color(1f, 0.9580836f, 0.5f, 0.2980392f);


        // 3. 데이터셋의 제목과 색상 설정
        //dataSet.Title = "";
        //dataSet.BarColors.Add(Color.white);

        /*barEntries.Add(new BarEntry(position: 0, value: 40));

        BarDataSet dataSet2 = new BarDataSet();
        dataSet2.AddEntry(barEntries[3]);

        dataSet2.Title = "Second Data";
        dataSet2.BarColors.Add(Color.black);*/

        // 4. BarData 객체 생성 및 데이터셋 추가
        barData.AddDataSet(dataSet[0]);
        barData.AddDataSet(dataSet[1]);
        barData.AddDataSet(dataSet[2]);
        //barData.AddDataSet(dataSet2);

        // 5. BarChart에 데이터 설정
        lineChart.data = barData;

        // 6. 차트 업데이트
        lineChart.SetDirty();

        if (basicValueFormatterConfig != null)
        {
            // customValues 리스트의 값을 가져오기
            List<string> values = basicValueFormatterConfig.CustomValues;
            values.Clear();
            //Axis Config - Horizontal Axis Config - Value Formatter Config - Custom Values에 추가하는 코드
            values.Add("~08시");
            values.Add("09시");
            values.Add("10시");
            values.Add("11시");
            values.Add("12시");
            values.Add("13시");
            values.Add("14시");
            values.Add("15시");
            values.Add("16시");
            values.Add("17시");
            values.Add("18시");
            values.Add("19시");
        }
        else
        {
            Debug.LogWarning("BarAxisValueFormatterConfig is not assigned.");
        }
        //Debug.Log(barChart.data.DataSets[0].Entries[0].Position + " " + barChart.data.DataSets[0].Entries[0].Value);

    }

    private void SetupCustomValueFormatter()
    {
        if (lineChart == null)
            return;

        lineChart.CustomVerticalAxisValueFormatter = new PriceValueFormatter();
    }

    private class PriceValueFormatter : AxisValueFormatter
    {
        public string FormatAxisValue(int index, float value, float minValue, float maxValue)
        {
            return value.ToString("F" + 0) + "";
        }
    }
}