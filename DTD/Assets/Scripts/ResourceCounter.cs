using UnityEngine;
using TMPro;

public class ResourceCounter : MonoBehaviour
{

    [SerializeField] private int _resources;
    [SerializeField] private int _startResources;
    [SerializeField] private TMP_Text _recourcesText;

    private static ResourceCounter _instance;

    public static ResourceCounter Instance {  get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else 
            _instance = this;

        _resources = _startResources;
        _recourcesText.text = _resources.ToString();
    }

    public void ReceiveResources (int resourceCount)
    {
        _resources += resourceCount;
        _recourcesText.text = _resources.ToString();

        GameEvents.Instance.ResourcesCountChage();
    }

    public void SpendResources(int resourceCount)
    {
        _resources -= resourceCount;
        _recourcesText.text = _resources.ToString();

        GameEvents.Instance.ResourcesCountChage();
    }
}
