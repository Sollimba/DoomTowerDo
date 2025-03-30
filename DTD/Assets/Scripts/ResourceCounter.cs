using UnityEngine;
using TMPro;

public class ResourceCounter : MonoBehaviour
{

    [SerializeField] private int _resources;
    [SerializeField] private TMP_Text _recourcesText;

    private static ResourceCounter _instance;

    public static ResourceCounter Instance {  get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else 
            _instance = this;
    }

    public void ReceiveResources (int resourceCount)
    {
        _resources += resourceCount;
        _recourcesText.text = _resources.ToString();
    }
}
