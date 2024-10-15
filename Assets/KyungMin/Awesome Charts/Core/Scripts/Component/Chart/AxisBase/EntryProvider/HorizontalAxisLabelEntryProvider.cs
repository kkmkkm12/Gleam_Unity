using UnityEngine;

namespace AwesomeCharts {
    [System.Serializable]
    public class HorizontalAxisLabelEntryProvider : LinearAxisLabelEntryProvider {

        protected override AxisOrientation GetEntryAxisOrientation (bool HVset) {
            if (!HVset)
                return AxisOrientation.HORIZONTAL;
            else
                return AxisOrientation.VERTICAL;
        }
    }
}