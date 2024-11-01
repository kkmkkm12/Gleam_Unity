using System.Collections.Generic;
using UnityEngine;

namespace AwesomeCharts {
    [System.Serializable]
    public class BarAxisLabelEntryProvider : AxisLabelEntryProvider {

        public BarCharPositioner barChartPositioner;
        public AxisLabelGravity labelsGravity = AxisLabelGravity.START;
        public AxisValueFormatter valueFormatter = new BarAxisValueFormatter ();
        public bool HVset = false;

        public List<AxisLabelRendererExtry> getLabelRendererEntries () {
            List<AxisLabelRendererExtry> entries = new List<AxisLabelRendererExtry> ();
            if (barChartPositioner == null)
                return entries;

            int entriesCount = barChartPositioner.GetVisibleEntriesRange ();
            int axisFirstVisibleIndex = (int) barChartPositioner.axisBounds.XMin;

            for (int i = axisFirstVisibleIndex; i < entriesCount + axisFirstVisibleIndex; i++) {
                AxisLabelRendererExtry entry = new AxisLabelRendererExtry ();
                if (!HVset)
                {
                    entry.PositionOnAxis = barChartPositioner.GetBarCenterPosition(i).x;
                    entry.Gravity = labelsGravity;
                    entry.Orientation = AxisOrientation.HORIZONTAL;
                }
                else
                {
                    entry.PositionOnAxis = barChartPositioner.GetBarCenterPosition(i).y;
                    entry.Gravity = labelsGravity;
                    entry.Orientation = AxisOrientation.VERTICAL;
                }
                entry.Text = GetLabelValueText(i, axisFirstVisibleIndex, entriesCount + axisFirstVisibleIndex);

                entries.Add (entry);
            }
            return entries;
        }

        private string GetLabelValueText (int index, int minIndex, int maxIndex) {
            return valueFormatter.FormatAxisValue (index, index, minIndex, maxIndex);
        }
    }
}