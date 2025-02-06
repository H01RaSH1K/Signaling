using UnityEngine;

[RequireComponent(typeof(Signaling))]
[RequireComponent(typeof(SwiperWatcher))]
public class House : MonoBehaviour
{
    private Signaling _signaling;
    private SwiperWatcher _watcher;

    private void Awake()
    {
        _signaling = GetComponent<Signaling>();
        _watcher = GetComponent<SwiperWatcher>();
    }

    private void OnEnable()
    {
        _watcher.FirstSwiperEnter += OnFirstSwiperEnter;
        _watcher.LastSwiperLeft += OnLastSwiperLeft;
    }

    private void OnFirstSwiperEnter()
    {
        _signaling.StartSignalingGradually();
    }

    private void OnLastSwiperLeft()
    {
        _signaling.StopSignalingGradually();
    }
}
