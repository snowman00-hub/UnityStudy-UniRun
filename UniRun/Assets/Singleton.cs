using UnityEngine;

/// <summary>
/// 제네릭 지속성 싱글톤 베이스 클래스
/// 다양한 타입의 싱글톤을 쉽게 구현할 수 있게 합니다.
/// </summary>
/// <typeparam name="T">싱글톤으로 만들 MonoBehaviour 타입</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    // 선택적 설정: 데모 목적으로 중복 제거를 지연시킬 수 있음
    [Tooltip("데모용: 중복 인스턴스 제거를 명시적으로 호출될 때까지 지연")]
    [SerializeField]
    private bool m_DelayDuplicateRemoval;

    // 싱글톤 인스턴스에 대한 정적 참조
    private static T s_Instance;

    // 싱글톤 인스턴스에 대한 공용 접근자
    public static T Instance
    {
        get
        {
            // 인스턴스가 없으면 씬에서 찾거나 생성
            if (s_Instance == null)
            {
                s_Instance = FindFirstObjectByType<T>();

                if (s_Instance == null)
                {
                    // 인스턴스가 없으면 새로 생성
                    SetupInstance();
                }
                else
                {
                    // 인스턴스가 이미 존재하면 로그 출력
                    string typeName = typeof(T).Name;
                    Debug.Log("[Singleton] " + typeName + " instance already created: " +
                                s_Instance.gameObject.name);
                }
            }

            return s_Instance;
        }
    }

    // MonoBehaviour 생명주기 - 초기화
    public virtual void Awake()
    {
        // 설정에 따라 중복 제거 즉시 실행 또는 지연
        if (!m_DelayDuplicateRemoval)
            RemoveDuplicates();
    }

    // 싱글톤 인스턴스 생성 및 설정
    private static void SetupInstance()
    {
        // 씬에서 기존 인스턴스 검색
        s_Instance = FindFirstObjectByType<T>();

        // 없으면 새로 생성
        if (s_Instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;

            s_Instance = gameObj.AddComponent<T>();
            // 씬 전환시에도 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObj);
        }
    }

    // 중복 인스턴스 처리
    public void RemoveDuplicates()
    {
        if (s_Instance == null)
        {
            // 처음 생성된 인스턴스라면 등록하고 보존
            s_Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (s_Instance != this)
        {
            // 이미 인스턴스가 있으면 이 중복 객체를 파괴
            Destroy(gameObject);
        }
    }
}
