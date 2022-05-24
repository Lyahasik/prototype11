using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Clock
{
    public class DigitalClock : BaseClock
    {
        [SerializeField] private InputField hourInput;
        [SerializeField] private InputField minuteInput;
        [SerializeField] private InputField secondInput;

        protected override void UpdateView()
        {
            hourInput.text = CurrentDateTime.Hour.ToString();
            minuteInput.text = CurrentDateTime.Minute.ToString();
            secondInput.text = CurrentDateTime.Second.ToString();
        }

        protected override IEnumerator IncreaseNumberSeconds()
        {
            while (true)
            {
                CurrentDateTime = CurrentDateTime.AddSeconds(1.0f);
                secondInput.text = CurrentDateTime.Second.ToString();
                
                if (CurrentDateTime.Second == 0)
                    IncreaseNumberMinutes();
                
                yield return new WaitForSeconds(1.0f);
            }
        }

        protected override void IncreaseNumberMinutes()
        {
            minuteInput.text = CurrentDateTime.Minute.ToString();
            
            if (CurrentDateTime.Minute == 0)
                IncreaseNumberHours();
        }

        protected override void IncreaseNumberHours()
        {
            hourInput.text = CurrentDateTime.Hour.ToString();
        }
    }
}
