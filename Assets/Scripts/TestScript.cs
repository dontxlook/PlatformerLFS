using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private Coroutine _routine;
    private void Start()
    {
        _routine = StartCoroutine(SomeCoroutine());
    }

    [ContextMenu("KillCoroutine")]
    public void KillCoroutin()
    {
        StopCoroutine(_routine);
    }


    private IEnumerator SomeCoroutine()
    {
        Debug.Log("Start coroutine");
        while (enabled)
        {
            Debug.Log("wait for another coroutine");
            yield return AnotherCoroutine();
            Debug.Log("some coroutine done");
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator AnotherCoroutine()
    {
        Debug.Log("this is another coroutine");
        yield return new WaitForSeconds(2f);
        Debug.Log("another coroutine done");
    }
}
