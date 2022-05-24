using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GlobalTimeManager : MonoBehaviour
{
    private const float UPDATE_TIME_DELAY = 3600.0f;
    
    public static Action<DateTime> OnCompareClocks;
    
    [SerializeField] private String[] webServicesUrls;

    private int _indexCurrentService;

    private void Start()
    {
        LoadGlobalTime();
    }

    public void LoadGlobalTime()
    {
        _indexCurrentService = 0;
        StartCoroutine(TryLoadServiceDateTime());
    }

    private IEnumerator TryLoadServiceDateTime()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(webServicesUrls[_indexCurrentService]);
    
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning(webServicesUrls[_indexCurrentService] + " service not responding.");
            
            NextService();
            yield break;
        }

        String stringDate = webRequest.GetResponseHeaders()["Date"];
        DateTime dateTime;
        
        if (!DateTime.TryParse(stringDate, out dateTime))
        {
            Debug.LogWarning(stringDate + " : failed to parse.");
            
            NextService();
            yield break;
        }
        
        OnCompareClocks?.Invoke(dateTime);
        StartCoroutine(NextCompareClocks());
    }

    private IEnumerator NextCompareClocks()
    {
        yield return new WaitForSeconds(UPDATE_TIME_DELAY);

        LoadGlobalTime();
    }

    private void NextService()
    {
        _indexCurrentService = (_indexCurrentService + 1) % webServicesUrls.Length;
        
        StartCoroutine(TryLoadServiceDateTime());
    }
}
