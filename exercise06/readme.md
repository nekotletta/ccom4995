# Exercise 6 - A Singleton manager to rule them all 

Este ejercicio consiste en crear managers en la forma singleton para manejar diferentes aspectos del juego.
En esta tarea vamos a manejar los enemigos y el piso.

## a) Enemy Manager

Enemy manager ya habia sido hacho durante el curso de varias clases. Es por esto que el EnemyManager, junto a Enemy simplemente fueron importados del proyecto que llevamos en clase.

## b.1) Floor

Tendremos un script llamado Floor que vamos a usar para nuestro FloorManager. Lo unico que necesitaos es una funcion que nos indique cual loseta remover de nuestro juego. 

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    void OnDestroy(){
        FloorManager.instance.removeTile(this);
    }
}
```

## b.2) FLoor Manager 

Para hacer el FloorManager, se copio el codigo ya hecho en Enemy y EnemyManager y se edito de acuerdo a lo necesario. 

Es necesario mantener una lista que tenga los objetos que queremos remover. Es por esto que hacemos una lista con objetos tipo Floor. 

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public List<Floor> Tiles; //lista con objetos Floor (losas del piso)
```

La lista tiene todas las losetas. Para inicializarla es necesario colocar todos los objetos que usamos para el piso en la lista. (hardcoded).  

![image](https://github.com/nekotletta/ccom4995/assets/99048617/73f00ee3-61ef-42b9-b69b-5a28f46c762a)


Usamos el formato singleton para crear nuestro manejador. Nos aseguramos que solo tengamos uno de la siguiente manera

```
    public static FloorManager instance;
    public List<Floor> Tiles; //lista con objetos Floor (losas del piso) 
    public UnityEvent onChanged;

    void Awake(){
        if(instance == null){
            instance = this; 
        }
        else{
            Debug.LogError("Duplicated Managers");
        }
```

Luego de asegurarnos de tener un solo manager, el proposito de este va a ser remover losas (tiles) de manera aleatoria. 

```
//remover la loseta previamente seleccionada 
    public void removeTile(Floor tile){
        Tiles.Remove(tile);
        Destroy(tile.gameObject); //para que desaparezca de la pantalla en unity
        onChanged.Invoke();
    }
    public void removeRandomTile(){
        if(Tiles.Count == 0){
            Debug.Log("No more tiles to remove.");
            return;
        }
        //seleccionar un indice aleatorio de la lista que contiene todas las losetas (en nuestro caso 16, matriz 4x4)
        int randomIndex = Random.Range(0, Tiles.Count);
        //loseta es del tipo Floor, como en la lista
        Floor tileToRemove = Tiles[randomIndex];
        removeTile(tileToRemove); //mandar loseta seleccionada a ser destruida
    }
```

## c) Remocion de losas

Las losas solo son removidas una vez una bala le pega a una pared. Las paredes funcionan como triggers.

Para eso utilizamos el siguiente script:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contacto : MonoBehaviour
{
    FloorManager floor; //llamar e inicializar objeto de otra clase en unity
    void Start(){
        floor = GameObject.Find("FloorManager").GetComponent<FloorManager>();
    }
     void OnTriggerEnter(Collider other){
        //cheuear que especificamente se le haya dado a la pared para no eliminar todo de una vez por accidente
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall")) {
            //llamar a la funcion para remover losas del piso en la clase Floor
           floor.removeRandomTile();
        }
    }
}
```

Este script le es colocado a nuestro prefab de la bala (bullet).


### Como crear layers necesarios

Para que el script funcione, es necesario tener los layers adecudados. Esos layers no vienen por defecto, por lo que hay que annadirlos

Para annadirlos podemos hacer click en el objeto al que le queremos colocar un layer e ir al inspector.

```
Hacer click en layer > Hacer click en add layer > Colocarle Wall como nombre > Seleccionar el layer Wall para tu objeto
```

![image](https://github.com/nekotletta/ccom4995/assets/99048617/e3bd09e3-cd50-4add-9819-c14357b13755)

Una vez tengamos el layer adecuado, podemos implementar nuestro script previamente mencionado.

### Colliders y fisica

Para que este script corra correctamente tambien es necesario tener los colliders y la fisica necesaria.

Para obtener el collider, simplemente seleccionamos nuestras paredes y les colocamos un box collider

![image](https://github.com/nekotletta/ccom4995/assets/99048617/01bf91b7-acf9-4e76-9c2f-a6d9c247b190)

La otra cosa necesaria es fisica en nuestra bala. Para colocarle fisica vamos al prefab de la bala y le colocamos un rigidbody. 

```
Es necesario seleccionar las opciones de Is Trigger y Freeze Position en Y
```

![image](https://github.com/nekotletta/ccom4995/assets/99048617/a441e18c-1f24-4986-8f85-6eef4b0ba862)

## d) Lluvia

Para la lluvia se creo un prefab con un objeto esfera

```
3D object > Object > Hacerlo un prefab
```

Luego se creo un BallManager y se le annadiendo el sguiente script:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bolita;
    void Start(){
        InvokeRepeating("spawn_bolita", 2f, 2f);
    }
    //funcion para spawnear bolas (objeto esfera) en posiciones random continuamente
    public void spawn_bolita(){
        //posiciones de cada eje, donde el rango son las dimensiones de la base
        int spawnX = Random.Range(400, 520); 
        int spawnY = 190; //procurar que sea posicionada mas alto que la camera
        int spawnZ = Random.Range(200, 320);
        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
        //para hacer que la bola aparezca / se cree
        Instantiate(bolita, spawnPos, Quaternion.identity);
    }
}
```
