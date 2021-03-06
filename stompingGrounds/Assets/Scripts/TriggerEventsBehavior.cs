using UnityEngine;
using UnityEngine.Events;
public class TriggerEventsBehavior : MonoBehaviour {
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerAfterIgnored;
    public int ignoreLayer;
    private void OnTriggerEnter(Collider other) {
        triggerEnterEvent.Invoke();
        if (other.gameObject.layer != ignoreLayer) {
            triggerAfterIgnored.Invoke();
        }
    }
}
