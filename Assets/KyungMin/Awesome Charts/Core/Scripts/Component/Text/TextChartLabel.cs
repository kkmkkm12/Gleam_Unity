using UnityEngine;
using UnityEngine.UI;

namespace AwesomeCharts {
    public class TextChartLabel : ChartLabel {

        public Text textLabel;

        public override void SetLabelColor (Color color) {
            textLabel.color = color;
            Debug.Log("³ª¿À³Ä? " + gameObject.transform.parent.name);
        }

        public override void SetLabelText (string text) {
            textLabel.text = text;
        }

        public override void SetLabelTextAlignment (TextAnchor anchor) {
            textLabel.alignment = anchor;
        }

        public override void SetLabelTextSize (int size) {
            textLabel.fontSize = size;
        }
    }
}