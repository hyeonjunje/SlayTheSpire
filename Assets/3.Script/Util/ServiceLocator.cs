using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegisterable
{
}

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator instance;
    public static ServiceLocator Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ServiceLocator>();
                instance.Init();
            }
            return instance;
        }
    }

    private IDictionary<object, IRegisterable> services;

    // manager�� �־���
    [SerializeField]
    private BattleManager battleManager;
    [SerializeField]
    private RewardManager rewardManager;
    [SerializeField]
    private RoomManager roomManager;
    [SerializeField]
    private CardGenerator cardGenerator;
    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private RelicGenerator relicGenerator;
    [SerializeField]
    private VFXGenerator vfxGenerator;


    private void Init()
    {
        services = new Dictionary<object, IRegisterable>();

        services[typeof(BattleManager)] = battleManager;
        services[typeof(RewardManager)] = rewardManager;
        services[typeof(RoomManager)] = roomManager;
        services[typeof(CardGenerator)] = cardGenerator;
        services[typeof(MapGenerator)] = mapGenerator;
        services[typeof(RelicGenerator)] = relicGenerator;
        services[typeof(VFXGenerator)] = vfxGenerator;

        battleManager.Init();
        relicGenerator.Init();
    }

    public T GetService<T>()
    {
        if (!services.ContainsKey(typeof(T)))
        {
            // Init();

            Debug.LogError("ServiceLocator::GetService ���� Ű�Դϴ�.");
            return default(T);
        }

        return (T)services[typeof(T)];
    }
}
