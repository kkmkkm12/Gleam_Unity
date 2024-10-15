using System.Collections.Generic;
using UnityEngine;

namespace AwesomeCharts
{
    [System.Serializable]
    public class BarCharPositioner
    {

        public AxisBounds axisBounds;
        public BarData data;
        public BarChartConfig barChartConfig;
        public Vector2 containerSize;

        public bool HVset = false;
        private float calculatedBarSize;

        public int GetVisibleEntriesRange()
        {
            if (axisBounds == null)
                return 0;
            else
                return (int)(axisBounds.XMax - axisBounds.XMin) + 1;
        }

        public void RecalculatePositioner()
        {
            if (!HVset)
                switch (barChartConfig.SizingMethod)
                {
                    case BarSizingMethod.STANDARD:
                        calculatedBarSize = GetBarWidthWithStandardMethod();
                        break;
                    case BarSizingMethod.SIZE_TO_FIT:
                        calculatedBarSize = GetBarWidthWithSizeToFitMethod();
                        break;
                }
            else
                switch (barChartConfig.SizingMethod)
                {
                    case BarSizingMethod.STANDARD:
                        calculatedBarSize = GetBarHeightWithStandardMethod();
                        break;
                    case BarSizingMethod.SIZE_TO_FIT:
                        calculatedBarSize = GetBarHeightWithSizeToFitMethod();
                        break;
                }
        }

        private float GetBarWidthWithStandardMethod()
        {
            float maxWidth = GetBarWidthWithSizeToFitMethod();
            return Mathf.Min(maxWidth, barChartConfig.BarWidth);
        }

        private float GetBarWidthWithSizeToFitMethod()
        {
            int visibleBars = GetVisibleEntriesRange() * data.DataSets.Count;
            float barsSpacings = (GetVisibleEntriesRange() + 1) * barChartConfig.BarSpacing;
            float innerBarsSpacings = (GetVisibleEntriesRange() * Mathf.Max(0, data.DataSets.Count - 1)) *
                barChartConfig.InnerBarSpacing;

            return (containerSize.x - barsSpacings - innerBarsSpacings) / visibleBars;
        }

        private float GetBarHeightWithStandardMethod()
        {
            float maxHeight = GetBarHeightWithSizeToFitMethod();
            return Mathf.Min(maxHeight, barChartConfig.BarWidth);
        }

        private float GetBarHeightWithSizeToFitMethod()
        {
            int visibleBars = GetVisibleEntriesRange() * data.DataSets.Count;
            float barsSpacings = (GetVisibleEntriesRange() + 1) * barChartConfig.BarSpacing;
            float innerBarsSpacings = (GetVisibleEntriesRange() * Mathf.Max(0, data.DataSets.Count - 1)) *
                barChartConfig.InnerBarSpacing;

            return (containerSize.y - barsSpacings - innerBarsSpacings) / visibleBars;
        }

        public Vector3 GetBarPosition(int position, int dataSetIndex)
        {
            if (axisBounds == null)
                return Vector3.zero;

            int visiblePosition = position - (int)axisBounds.XMin;
            float x = ((visiblePosition * data.DataSets.Count) * calculatedBarSize) +
                (dataSetIndex * calculatedBarSize) +
                (visiblePosition + 1) * barChartConfig.BarSpacing +
                (visiblePosition * Mathf.Max(0, data.DataSets.Count - 1) * barChartConfig.InnerBarSpacing) +
                (dataSetIndex * barChartConfig.InnerBarSpacing);

            if (!HVset)
                return new Vector3(x, 0, 0);
            else
                return new Vector3(0, x, 0);

        }

        public Vector3 GetBarCenterPosition(int position)
        {

            int dataSetsCount = Mathf.Max(1, data.DataSets.Count);
            float positionFullWidth = calculatedBarSize * dataSetsCount +
                barChartConfig.InnerBarSpacing * (dataSetsCount - 1);
            if (!HVset)
            {
                float x = GetBarPosition(position, 0).x + positionFullWidth / 2;
                return new Vector3(x, 0, 0);
            }
            else
            {
                float y = GetBarPosition(position, 0).y + positionFullWidth / 2;
                return new Vector3(0, y, 0);
            }

        }

        public float GetMaxBarHeight()
        {
            return containerSize.y;
        }

        public float GetMaxBarWidth()
        {
            return containerSize.x;
        }

        public Vector2 GetBarSize(float value)
        {
            if (!HVset)
                return new Vector2(calculatedBarSize, CalculateBarHeight(value));
            else
                return new Vector2(CalculateBarWidth(value), calculatedBarSize);
        }

        private float CalculateBarHeight(float value)
        {
            if (axisBounds.YMax - axisBounds.YMin == 0f || value == 0f)
                return 0;
            else
                return ((value - axisBounds.YMin) / axisBounds.YMax) * GetMaxBarHeight();
        }

        private float CalculateBarWidth(float value)
        {
            if (axisBounds.YMax - axisBounds.YMin == 0f || value == 0f)
                return 0;
            else
                return ((value - axisBounds.YMin) / axisBounds.YMax) * GetMaxBarWidth();
        }

        public Vector3 GetValuePopupPosition(BarEntry entry, int dataSetIndex)
        {
            Vector3 barPosition = GetBarPosition((int)entry.Position, dataSetIndex);
            Vector2 barSize = GetBarSize(entry.Value);

            if (!HVset)
                return new Vector3(barPosition.x + calculatedBarSize / 2, barPosition.y + barSize.y, 0);
            else
                return new Vector3(barPosition.x + barSize.x, barPosition.y + calculatedBarSize / 2, 0);
        }

        public int GetAllVisibleEntriesCount()
        {
            if (axisBounds == null || data == null || !data.HasAnyData())
                return 0;

            int result = 0;
            data.DataSets.ForEach(dataSet => {
                result += FilterVisibleEntries(dataSet).Count;
            });

            return result;
        }

        public List<BarEntry> GetVisibleEntries(int dataSetIndex)
        {
            if (axisBounds == null || data == null || !data.HasAnyData())
                return new List<BarEntry>();

            return FilterVisibleEntries(data.DataSets[dataSetIndex]);
        }

        private List<BarEntry> FilterVisibleEntries(BarDataSet dataSet)
        {
            return dataSet.Entries.FindAll((BarEntry entry) =>
                entry.Position >= axisBounds.XMin &&
                entry.Position <= axisBounds.XMax);
        }
    }
}