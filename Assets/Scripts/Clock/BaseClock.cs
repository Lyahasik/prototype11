using System;
using System.Collections;
using UnityEngine;

namespace Clock
{
    public abstract class BaseClock : MonoBehaviour
    {
        protected DateTime CurrentDateTime;

        private void Start()
        {
            GlobalTimeManager.OnCompareClocks += CompareClocks;
        }

        private void CompareClocks(DateTime datetime)
        {
            if (CurrentDateTime == datetime)
                return;
            
            UpdateTime(datetime);
        }
        
        private void UpdateTime(DateTime datetime)
        {
            CurrentDateTime = datetime;
            
            StopAllCoroutines();
            
            UpdateView();

            StartCoroutine(IncreaseNumberSeconds());
        }
    
        protected abstract void UpdateView();
        protected abstract IEnumerator IncreaseNumberSeconds();
        protected abstract void IncreaseNumberMinutes();
        protected abstract void IncreaseNumberHours();
    }
}
