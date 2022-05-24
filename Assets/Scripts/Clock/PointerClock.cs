using System.Collections;
using UnityEngine;

namespace Clock
{
    public class PointerClock : BaseClock
    {
        private const float HOUR_HAND_STEP = 15.0f;
        private const float MINUTE_HAND_STEP = 6.0f;
        private const float SECOND_HAND_STEP = 6.0f;
        
        [SerializeField] private GameObject _hourHand;
        [SerializeField] private GameObject _minuteHand;
        [SerializeField] private GameObject _secondHand;

        protected override void UpdateView()
        {
            _hourHand.transform.rotation = Quaternion.Euler(0.0f, 0.0f, CurrentDateTime.Hour * -HOUR_HAND_STEP);
            _minuteHand.transform.rotation = Quaternion.Euler(0.0f, 0.0f, CurrentDateTime.Minute * -MINUTE_HAND_STEP);
            _secondHand.transform.rotation = Quaternion.Euler(0.0f, 0.0f, CurrentDateTime.Second * -SECOND_HAND_STEP);
        }

        protected override IEnumerator IncreaseNumberSeconds()
        {
            while (true)
            {
                CurrentDateTime = CurrentDateTime.AddSeconds(1.0f);
                _secondHand.transform.RotateAround(_secondHand.transform.position, -Vector3.forward, SECOND_HAND_STEP);
                
                if (CurrentDateTime.Second == 0)
                    IncreaseNumberMinutes();
                
                yield return new WaitForSeconds(1.0f);
            }
        }

        protected override void IncreaseNumberMinutes()
        {
            _minuteHand.transform.RotateAround(_minuteHand.transform.position, -Vector3.forward, MINUTE_HAND_STEP);
            
            if (CurrentDateTime.Minute == 0)
                IncreaseNumberHours();
        }

        protected override void IncreaseNumberHours()
        {
            _hourHand.transform.RotateAround(_hourHand.transform.position, -Vector3.forward, HOUR_HAND_STEP);
        }
    }
}
