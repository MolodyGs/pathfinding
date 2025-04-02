using UnityEngine;
using System.Threading.Tasks;

namespace Components
{
  public class TileNode
  {
    public int x;
    public int z;
    public int g = 0;
    public int h = 0;
    public int f = 0;
    public bool blocked = false;
    private bool closed = false;
    public TileNode parent = null;
    private GameObject plate;

    public TileNode(int x, int z)
    {
      this.x = x;
      this.z = z;
      plate = Object.Instantiate(Resources.Load<GameObject>("Plane"));
      plate.transform.position = new Vector3(x, 1.0f, z);
      plate.SetActive(false);
    }

    public void SetGCost(int gCost)
    {
      g = gCost;
      f = g + h;
    }

    public void SetHCost(int hCost)
    {
      h = hCost;
      f = g + h;
    }

    public async Task<int> SetPath()
    {
      try
      {
        // Activa la placa asociada al nodo
        plate.SetActive(true);

        // Añade el nodo a la lista de nodos para el camino
        Controllers.TilesController.path.Add(this);

        // Verifica que el padre exista
        if (parent == null) return 0;

        // Si el padre no es nulo, entonces llama a su método SetPath de forma recursiva
        return await parent.SetPath();
      }
      catch (System.Exception e)
      {
        Debug.LogError("Error al intentar recuperar al padre SetPath: " + e.Message);
        return -1; 
      }
    }

    public Vector2 GetPosition()
    {
      return new Vector2(x, z);
    }

    public void Reset()
    {
      closed = false;
      parent = null;
      g = 0;
      h = 0;
      f = 0;
      plate.SetActive(false);
    }

    public void SetClosed(bool closed)
    {
      this.closed = closed;
    }

    public bool GetClosed()
    {
      return closed;
    }

    public void SetPlate(bool state)
    {
        plate.SetActive(state);
    }
  }
}
