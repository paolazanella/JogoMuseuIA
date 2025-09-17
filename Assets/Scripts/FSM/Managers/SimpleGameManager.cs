using UnityEngine;

public class SimpleGameManager : MonoBehaviour
{
    public static SimpleGameManager Instance;
    
    [Header("Referências")]
    public ThiefController ladrao;
    public GuardaFSM[] guardas;
    public DroneFSM[] drones; 
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        // Configurar referência do ladrão em todos os guardas
        foreach (var guarda in guardas)
        {
            // O guarda vai detectar automaticamente pelo GetNearestEnemy()
        }
    }
    
    // Método chamado quando câmera detecta o ladrão
    public void AlertarGuardas(Vector3 posicao)
    {
        Debug.Log("🚨 ALERTA! Ladrão detectado!");
        
        // Ativa todos os guardas próximos
        foreach (var guarda in guardas)
        {
            float distancia = Vector3.Distance(guarda.transform.position, posicao);
            if (distancia < 10f) // Só guardas próximos respondem
            {
                guarda.targetEnemy = ladrao.transform;
            }
        }
    }

    // Método chamado para ativar todos os drones
    public void AtivarDrones(Transform alvo)
    { 
        // Passa por cada drone na lista de referências
        foreach (var drone in drones)
        {
            // Define o alvo do drone. Isso fará com que o drone saia do estado 'Idle'.
            if (drone != null)
            {
                drone.targetEnemy = alvo;
                }
            }
        }
    }

}
