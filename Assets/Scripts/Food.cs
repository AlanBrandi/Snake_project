using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        //chama o método randomizePosition.
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        //obtem  bordas da arena.
        Bounds bounds = this.gridArea.bounds;

        // pega uma posição x e y aleatoria, a partir dos valores max e min.
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // retorna arredondado os valores.
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //quando existe colisão, ele inicia o processo novamente chamando o método randomizePosition.
        RandomizePosition();
    }

}
