![image](https://github.com/nekotletta/ccom4995/assets/98557727/4fddd088-ff82-4c6d-8e11-9865afdea3e6)![image](https://github.com/nekotletta/ccom4995/assets/98557727/ceafadcf-0ec7-4144-8941-68becdd25eae)![image](https://github.com/nekotletta/ccom4995/assets/98557727/de31e5f6-5ad0-4b85-95e5-44465df2d568)# Exercise 6 - A Singleton manager to rule them all 

Este ejercicio consiste en crear managers en la forma singleton para manejar diferentes aspectos del juego.
En esta tarea vamos a manejar los enemigos y el piso.

## Preparacion del terreno

Se crearon los tags para diferenciar los colliders del terreno

![image](https://github.com/nekotletta/ccom4995/assets/99048617/7c49f3b4-1716-4034-8165-95866bcaaa1e)

Se importo el terreno ya hecho en clase, con el cambio que se le coloco una losa en el medio (previamente vacio). 
Ademas de esto, el fondo fue editado para simular el espacio. 

![image](https://github.com/nekotletta/ccom4995/assets/99048617/7ea841d2-8bf1-494b-a934-e83d1b25c9ec)

Tambien se decidio importar el script del movimiento y el disparo del personaje realizadas para la tarea anterior. 

Ademas de este script, se hizo un script con deteccion de colisiones mas preciso. Esto es para evitar que el jugador se salga del escenario por atravesar las paredes. 

```
bool IsWallInDirection(Vector3 direction){
    RaycastHit hit;
    if (Physics.Raycast(transform.position, direction, out hit, groundDistance)){
        if (hit.collider.CompareTag("Wall")){
            return true;
        } return false;
}
```

## Managers
### a) Enemy Manager

Enemy manager ya habia sido hacho durante el curso de varias clases. Es por esto que el EnemyManager, junto a Enemy simplemente fueron importados del proyecto que llevamos en clase.

### b.1) Floor

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

### b.2) FLoor Manager 

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

### c) Remocion de losas

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

## Lluvia

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

##Logica de Vida
La vida es bastante simple, tenemos un valor que representa la cantidad de vida que va a tener el prefab con este script atado y un objeto que deja saber si se invoco un evento de parte. El update mantiene seguimiento el valor de vida, cuando este llegue a 0, el objeto de evento deja saber que se invoco un elemento y se destruye el prefab el cual tenía el script atado (Sea el enemigo o el jugador).

Creamos dos scripst que utilizan "life", uno para quitarle vida a los enemigos y otro para quitarle puntos de vida al jugador.
Tenemos el script creado en la clase para el enemigo recibir damage, el cual la bala coilisiona con el y le quita una cantidad fija de vida, solo faltaba la del jugador.

Tenemos otro script para la plataforma que va a estar debajo del piso, el cual qualquier cosa que coilisione con ella y tenga atado el script de "life.cs", su valor de vida se reducira a 0. Cuando el jugador se caiga por las lozetas que se van desapareciendo, el jugador se elimina instantaneamente.
    <DeathPlatform.Gif>

##Coilisiones Implementadas para el juego
Utilizamos tags para determinar el tipo de objeto con el que este estaba coilisionando el objeto con el script. Los que van a causar cambios pricipales van a ser la gotas de la lluvia igual que la baja del jugador.

La bala contiene un script llamado contacto.cs, el cual basado en el tag del objeto que coilisiono hace se comporta differente mente. Se distribuyen entre 3 tags principales:
###"Enemy"
Si es un enemy, la bala se destruye y le quita vida del script Life.cs basado en la variable de damage.
	<ShootingEnemy.gif>
 
###"Wall"
Invoca una funcion de floor.cs el cual elimina una lozeta del piso al azar.
    <TileDestroy.gif>
    
###"Corner"
Destruye el piso completo.
    <FloorDestroy.gif>
    
Si es qualquier otra cosa se destruye la bala sin ningun otro efecto, esto permite que las gotas que caigan en el sielo no destruyan las balas.
			
Hablando de, las gotas tendran su propio script de contacto, en el cual reacciona differente basado al tag del game object con el que coalisiona:
###"Enemy"
se destruye la gota
    <GotaEnemy.gif>
###"ThePlayer" = si hace contacto con el jugador, la gota se destruye y le quita vida del script Life.cs basado en la variable de damage.
    <GotaPlayer.gif>


##Condición Ganar y Perder

1. Creamos un objeto vacío llamado GameMode, en el cual vamos a poner el cript que contiene nuestras condiciones para ganar.
2. Crea 2 nuevas escenas, una para representar que ganaste y otra que se invoque cuando pierdas, las llamaremos "WinScreen" y "LooseScreen" respectivamente.
3. Dentro del objeto Gamemode, creamos un script llamado wavesGameMode
4. Dentro del script creamos una objeto tipo "Life" el cual representara el prefab que estamos controlando.
5. Estre script mantendra dos funciones que monitorean cada evento que ocurre en el juego, en este caso si matan a un enemigo se       invoca una funcion que chequea si la criteria de ganar se a alcanzado (si eliminas todos los enemigos) y otra funcion si la         vida del objeto Life llega a 0.
6. En las funciones de ganar y perder vamos a utilizar el manejador de escenas para invocar la respectiva escena basada en si          ganamos o perdimos. En la función de perder invocamos a la escena "LoseScreen" y en la funcion de ganar invocamos la escena         "WinScreen".
7. Las Escenas son registradas en el build settings para que unity sepa donde buscarlas.

###Condicion de Ganar
    <Win.gif>

###Condición de Perder
    <Lose.gif>

El script de WavesGameMode.cs chequea que todos los waves hallan terminado de invocar enemigos igualmente que todos los enemigos hallan sido eliminados para demonstrar la escena de que ganastes. Esto se mantiene en track con en EnemyManager.cs WavesManager.cs los cuales contienen una lista de los respectivos Enemy y Waves que hay en el juego.











