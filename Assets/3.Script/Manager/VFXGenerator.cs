using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVFX
{
    PlayerAttack,
    PlayerStrongAttack,
    GainShield,
    Slime,
    EnemyAttack,
    Size,
}

public class VFXGenerator : MonoBehaviour, IRegisterable
{
    public bool isVFX = true;

    [SerializeField]
    private SpriteRenderer _vfxPrefab;
    [SerializeField]
    private List<Sprite> _vfxSprite;
    [SerializeField]
    private int initCount = 5;

    // 오브젝트 풀링
    private Dictionary<EVFX, Queue<GameObject>> _pool;

    public void Init()
    {
        isVFX = true;
        InitPool();
    }

    private void InitPool()
    {
        _pool = new Dictionary<EVFX, Queue<GameObject>>();

        for (int i = 0; i < _vfxSprite.Count; i++)
        {
            _pool[(EVFX)i] = new Queue<GameObject>();
            for(int j = 0; j < initCount; j++)
            {
                SpriteRenderer vfxSR = Instantiate(_vfxPrefab, transform);
                vfxSR.sprite = _vfxSprite[i];
                vfxSR.gameObject.name = ((EVFX)i).ToString();
                _pool[(EVFX)i].Enqueue(vfxSR.gameObject);
                vfxSR.gameObject.SetActive(false);
            }
        }
    }

    public GameObject CreateVFX(EVFX vfx, Vector3 pos)
    {
        if (!isVFX)
            return null;

        if(_pool[vfx].Count == 0)
        {
            SpriteRenderer vfxSR = Instantiate(_vfxPrefab, transform);
            vfxSR.sprite = _vfxSprite[(int)vfx];
            vfxSR.gameObject.SetActive(false);
            vfxSR.gameObject.name = vfx.ToString();
            _pool[vfx].Enqueue(vfxSR.gameObject);
        }

        GameObject result = _pool[vfx].Dequeue();
        result.SetActive(true);
        result.transform.position = pos;
        return result;
    }

    public void ReturnVFX(GameObject vfx)
    {
        for(int i = 0; i < (int)EVFX.Size; i++)
        {
            if(vfx.name == ((EVFX)i).ToString())
            {
                vfx.SetActive(false);
                _pool[(EVFX)i].Enqueue(vfx);
            }
        }
    }
}
