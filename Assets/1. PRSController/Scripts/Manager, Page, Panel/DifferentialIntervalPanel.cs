using TangkaUI;
using UnityEngine;

namespace PRSController
{
    public class DifferentialIntervalPanel : Panel
    {
        PRSData data;

        [SerializeField] Dial dial;

        private void Awake()
        {
            dial.onValueChanged += UpdateData;
            //UpdateData(dial.DialValue);
        }

        private void UpdateData(float dialValue)
        {
            data.DifferentialInterval = dialValue;
        }

        public void SetData(ref PRSData data)
        {
            this.data = data;
        }
    }
}
