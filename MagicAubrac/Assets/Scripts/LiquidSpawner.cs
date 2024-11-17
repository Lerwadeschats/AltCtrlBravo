using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _particle;
    [SerializeField] private float _amount;
    private List<SpriteRenderer> _particlesRend = new List<SpriteRenderer>();

    public void Serve(Color c)
    {
        StartCoroutine(ServingXLiquid(c));
    }
    IEnumerator ServingXLiquid(Color c)
    {
        float rand = Random.Range(-.5f, .5f);
        for(int i = 0; i < _amount; i++)
        {
            GameObject part = Instantiate(_particle,transform.position + new Vector3(rand, 0, 0),transform.rotation,transform) ;
            SpriteRenderer partRend = part.GetComponent<SpriteRenderer>();
            _particlesRend.Add(partRend);
            partRend.color = c;
            if (i % 5 == 0)
            {
                 rand = Random.Range(-.5f, .5f);
                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return null;
    }
    public void Mix(Color newC)
    {
        foreach (SpriteRenderer sr in _particlesRend)
        {
            StartCoroutine(MixingLiquid(newC, sr));
        }
    }
    IEnumerator MixingLiquid(Color newC,SpriteRenderer sr)
    {
        float timer = 0;
        Color baseColor = sr.color;
        while (timer < .5)
        {
            timer += Time.deltaTime;
            sr.color = Color.Lerp(baseColor, newC, timer * 2);
        }
        yield return null;
    }
}
