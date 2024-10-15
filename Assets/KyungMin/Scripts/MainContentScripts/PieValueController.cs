using AwesomeCharts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieValueController : MonoBehaviour
{
    public PieChart pieChart;
    private List<PieEntry> pieEntries = new List<PieEntry>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrowGraph(float value)
    {
        pieChart.GetChartData().DataSet.Entries.Clear();
        pieEntries.Clear();

        pieEntries.Add(new PieEntry(value: value, label: "", color: Color.yellow));

        for(int i = 0; i < pieEntries.Count; i++) 
            pieChart.GetChartData().DataSet.AddEntry(pieEntries[i]);

        pieChart.SetDirty();
    }
}
