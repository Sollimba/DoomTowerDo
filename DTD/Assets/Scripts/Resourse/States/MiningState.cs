using System.Collections;
using UnityEngine;

public class MiningState : MinerState
{
    private ResourceCounter _resourceCounter;

    private void OnEnable()
    {
        _resourceCounter = ResourceCounter.Instance;
        StartCoroutine(Mine());
    }

    private IEnumerator Mine ()
    {
        yield return new WaitForSeconds(2);
        _resourceCounter.ReceiveResources(1);
        StartCoroutine(Mine());
    }
}
