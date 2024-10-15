using UnityEngine;

namespace AwesomeCharts {
    [System.Serializable]
    public class VerticalAxisLabelEntryProvider : LinearAxisLabelEntryProvider {

        protected override AxisOrientation GetEntryAxisOrientation (bool HVset) {
            if (!HVset)
                return AxisOrientation.VERTICAL;
            else
                return AxisOrientation.HORIZONTAL;
        }
    }
}