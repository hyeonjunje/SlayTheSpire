using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegisterable
{

}

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance;

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

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            Init();
        }
    }

    private void Init()
    {
        services = new Dictionary<object, IRegisterable>();

        services[typeof(BattleManager)] = battleManager;
        services[typeof(RewardManager)] = rewardManager;
        services[typeof(RoomManager)] = roomManager;
        services[typeof(CardGenerator)] = cardGenerator;
        services[typeof(MapGenerator)] = mapGenerator;
    }

    public T GetService<T>()
    {
        if (!services.ContainsKey(typeof(T)))
        {
            Debug.LogError("ServiceLocator::GetService ���� Ű�Դϴ�.");
            return default(T);
        }

        return (T)services[typeof(T)];
    }
}
