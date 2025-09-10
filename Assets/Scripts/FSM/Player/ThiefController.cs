using UnityEngine;

public class ThiefController : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 5f;
    
    [Header("Roubo")]
    public bool temItem = false; // Se está carregando item roubado
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.tag = "Player"; // Para os guardas detectarem
    }
    
    void Update()
    {
        // Movimento simples WASD
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(moveX, moveY);
        rb.linearVelocity = movement * speed;
        
        // Roubar item com E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TentarRoubar();
        }
    }
    
    void TentarRoubar()
    {
        if (temItem) return; // Já tem um item
        
        // Procura por itens próximos
        Collider2D item = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("Items"));
        
        if (item != null)
        {
            temItem = true;
            item.gameObject.SetActive(false); // Esconde o item
            Debug.Log("Item roubado!");
            
            // Muda cor para mostrar que tem item
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    
    // Chamado quando é pego pelo guarda
    public void ForPego()
    {
        if (temItem)
        {
            temItem = false;
            GetComponent<SpriteRenderer>().color = Color.white;
            Debug.Log("Ladrão foi pego com o item!");
        }
        
        // Volta para posição inicial ou reinicia
        transform.position = Vector3.zero;
    }
}