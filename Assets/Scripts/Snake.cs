using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    //Cração da lista transform(os segmentos) para formar o player, ou seja, a "cobrinha".
    private List<Transform> _segments = new List<Transform>();
    //Criação da variável transform, para a localicação dos segmentos.
    public Transform segmentPrefab;
    //Criação do vector2 com o valor de vector2 para a direita, ou seja, direção é igual a direita(do mundo).
    public Vector2 direction = Vector2.right;
    //Variável int para o tamanho inicial de segmentos padrões.
    public int initialSize = 4;
        
    //Start unity, quando iniciar o programa, inicia o que está dentro.
    private void Start()
    {
        //Inicia o método ResetState.
        ResetState();
    }
    
    //Update Unity, a unity entra neste método varias vezes durante um segundo.
    private void Update()
    {
        //Se a direção, no eixo x estiver diferente de 0 entre nisso. + se mover para frente e para trás, utilizando o W,S, arrow up e arrow down.
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
        //Caso não for e a direção, no eixo y, for diferente de 0, entre nisso. + movimentação para direita, esquerda, utilizando D,A, arrow left e arrow right.
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        // Andando os segmentos para que eles acompanhem o próximo.
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        // adicionando novas variáveis, float x e y, para armazenar as posições em um novo vector, com as mesmas. (utilizando para acompanhar e adicionar os novos locais que os segmentos precisam estar.)
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        this.transform.position = new Vector2(x, y);
    }
    
    //Método Grow, ele instancia um novo segmento, eu sua devida posição.
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    
    //Método ResetState, volta para o estato inicial do jogo, com apenas 4 segmentos e em sua posição inicial.
    public void ResetState()
    {
        //ele inicia indo para a direita, no ponto zero do mundo.
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        // destruir os ultimos segmentos para dar a sensação de movimento.
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        // limpar o a lista dos segmentos e adicionar um novo, junto de sua devida posição.
        _segments.Clear();
        _segments.Add(this.transform);

        // se acaso ele estiver menor do que deveria ele adiciona um novo segmento.
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    //Sistema de colissões da unity, collider trigger, sem colisão física.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //caso a colisão foi contra um objeto com a tag comida, ele chama o método Grow.
        if (other.tag == "Food") {
            Grow();
        } 
        //Caso for um obstaculo ele chama o método ResetState.
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
