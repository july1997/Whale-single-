using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Rendering;

public class Bootstrap : MonoBehaviour 
{
   public static Bootstrap _Instance;

    public static Bootstrap Instance 
    { 
        get 
        { 
            return _Instance ?? (_Instance = FindObjectOfType<Bootstrap>());
        }
    }

    public static bool IsValid
    {
        get { return Instance != null; }
    }

    [SerializeField]
    Param param;

    public static Param Param
    {
        get { return Instance.param; }
    }

    [System.Serializable]
    public struct BoidInfo
    {
        public int count;
        public Vector3 scale;
        public RenderMesh renderer;
    }

    [SerializeField]
    BoidInfo boidInfo = new BoidInfo 
    {
        count = 100,
        scale = new Vector3(0.1f, 0.1f, 0.3f),
    };

    public static BoidInfo Boid
    {
        get { return Instance.boidInfo; }
    }

    void OnDrawGizmos()
    {
        if (!param) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * param.wallScale);
    }
}
/*
    public GameObject Prefab;

    public static Bootstrap Instance { get; private set; }
    public static Param Param { get { return Instance.param; } }

    [SerializeField] int boidCount = 100;
    [SerializeField] float3 boidScale = new float3(0.1f, 0.1f, 0.3f);
    [SerializeField] Param param;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        // プレハブをエンティティに変換
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(Prefab, settings);
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // ランダムの初期化（Unity.Mathematics の利用）
        var random = new Unity.Mathematics.Random(853);

        // カウント分、エンティティの生成とコンポーネントの初期化
        for (int i = 0; i < boidCount; ++i)
        {
            // プレハブから実際にインスタンスを生成
            var instance = entityManager.Instantiate(prefab);
            var position = transform.TransformPoint(random.NextFloat3(1f));

            // 位置
            entityManager.SetComponentData(instance, new Translation {Value = position});
            // 回転値
            entityManager.SetComponentData(instance, new Rotation { Value = quaternion.identity });
            // 大きさ
            entityManager.SetComponentData(instance, new NonUniformScale { Value = new float3(boidScale.x, boidScale.y, boidScale.z) });
            
            // EntityManagerからComponentDataを追加する(Prefabに設定してもOK、その場合はSetComponentDataを使う)
            entityManager.AddComponentData(instance, new Velocity { Value = random.NextFloat3Direction() * param.initSpeed });
            entityManager.AddComponentData(instance, new Acceleration { Value = float3.zero });

            // Dynamic Buffer の追加
            entityManager.AddBuffer<NeighborsEntityBuffer>(instance);
        }
    }

    void OnDrawGizmos()
    {
        if (!param) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * param.wallScale);
    }
}
*/