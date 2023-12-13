# Final Project - Fast Cats

El último proyecto de este curso consiste en crear un videojuego utilizando los conceptos que hemos aprendido en la clase. En esta documentación se encontrará el aspecto técnico y lo que contiene el juego.


## Item System

El Item Box es un sistema similar a los que muchos conocemos, un jugador hace contacto con la caja y se ejecuta una ruleta para ver cuál poder le tocó al jugador y utilizarlo para tomar ventaja y ganar la carrera.

Cuando el jugador hace contacto con un bloque sorpresa, el bloque desaparece por un segundo y vuelve.

![image](img/ItemBox.gif)

En este tiempo, se invoca una función que escoge un item aleatorio de la lista de prefabs que contienen los items físicos. Esto funciona activando uno de los prefabs del los items que contiene el mismo jugador desactivados en el tope de la cabeza. OJO (Esto implica que los prefabs de los items que tiene el modelo del jugador, tienen que ser iguales a los de la lista)

![image](img/ItemSetup.png)

Cuando le das al click izquierdo al mouse, desactiva el item dezplegado, y el script de ItemSystem.cs invoca la función del effecto del power up basado en el tag del prefab que fue desactivado.

![image](img/ItemCabeza.png)


## Powerups

Los siguientes powerups le puede salir al jugador dentro del Item Box.

### Boost

Aumenta la velocidad del jugador por unos segundos para poder pasarle a sus oponentes satisfactoriamente.

```
public IEnumerator Boost(){
        CarController player = gameObject.GetComponent<CarController>();
        if(player != null){
            player.MaxRPM *= 3; // Triplicate the player's max and minimum speed
            player.MinRPM *= 3;
            cameraFov.fieldOfView += 30; // Adjust the camera so that it goes further back when boosting
            boostEffect.Play(); // Add the booster effect
            audioEffect.clip = useBoostSound; //Add sound effects assosciated with the effect
            audioEffect.Play();
            yield return new WaitForSeconds(3);
            boostEffect.Stop();
            // Reset modified parameters back to default after 3 seconds
            cameraFov.fieldOfView -= 30;
            player.MaxRPM /= 3;
            player.MinRPM /= 3;
        }
        yield return new WaitForSeconds(0);
    }
```

![image](img/Boost.gif)

### HoneyBun

Como todos sabemos, los honeybuns ponen lento a una persona. En este caso el jugador emite tres honeybuns en la parte de atrás para los oponentes correr sobre ellos y paralizarlos temporalmente. Aún hay que tener cuidado, ya que si el jugador corre sobre estos mismos va a perder la velocidad. 

```
public IEnumerator Bomb(){
        audioEffect.clip = dropBombSound;
        for(int i=0; i < 3; i++){
            audioEffect.Play();
            GameObject clone = Instantiate(drop);
            clone.transform.position = dropPoint.transform.position;
            clone.transform.rotation = dropPoint.transform.rotation;
            yield return new WaitForSeconds(1);
        }
    }
```

Jugador activando poder:

![image](img/Drop.gif)

Jugador recibiendo effectos del honeybun:

![image](img/SlowDownPlayer.gif)

### Bricks

El jugador dispara vareos ladrillos proyectiles, si un ladrillo le da a un enemigo, lo va a paralizar por un momento.

```
public IEnumerator Bullet(){
        audioEffect.clip = shootSound;
        for(int i=0; i < 6; i++){
            audioEffect.Play();
            // bulletEffect.Play
            // Shooting bullets
            GameObject clone = Instantiate(bullet);
            clone.transform.position = bulletPoint.transform.position;
            clone.transform.rotation = bulletPoint.transform.rotation;
            yield return new WaitForSeconds(1);
        }
    }
```

![image](img/Shoot.gif)

### Logica de Powerups

Todos los power ups siguen un patrón similar, corren unas acciones o efectos por un número fijo de segundos. Los power ups y los efectos de objetos emitidos por resultado del power up, fueron implementadas utilizando IEnumerators. Esto permitía que pudieramos manipular las variables de los componentes del jugador o computadora por un periodo de tiempo y reversar el efecto después de que el respectivo tiempo pase, como se demuestra en la siguiente ilustración:

```
	IEnumerator PowerOrEffect(arguments){
        // Get gameObject component if needed EJ: gameObject.GetComponent<ManipulatedComponent>();
        // Manipulate variables 
        yield return new WaitForSeconds(n);
        // Reverse back variables to normal state
    }
```

También para hacer algo como los power ups que emitían objetos entre segundos por un número de segundos, podíamos implementar el 'yield return'  dentro de un loop para dar el resultado deseado.

```
	IEnumerator DropOrShootPower(arguments){
	    // Get gameObject component if needed EJ: gameObject.GetComponent<ManipulatedComponent>();
        for(int i=0; i < numberOfSeconds; i++){
            // Drop or shoot object out player's drop or shoot point
            yield return new WaitForSeconds(1);
        }
    }
```

Si algún lector desea implementar algún nuevo power up,  se puede repetir el siguiente ciclo:

	1.  Crear un tag
 
	2.  Crear un prefab con el nombre del tag
 
	3.  Colocar el prefab en posición y tamaño que los otros prefabs encontrados en items dentro del prefab del jugador.
 
	4.  Añadir nuevo prefab a la lista items en el script ItemSystem.cs
 
	5.  Dentro de ItemSystem.cs, crear función del efecto e invocarla en la función de ActivatedItem(string itemTag).


## Implementacion NPCs

En un juego de carreras es necesario tener jugadores artificiales para hacerlo entretenido. 

Para implementar los NPC en el juego se utilizo un sistema de checkpoints (waypoints) posicionados a traves de la pista. 

```
public class NPCController : MonoBehaviour
{
    public List<Transform> waypoints; // Lista de objetos vacíos que representan el recorrido
    private int currentWaypointIndex = 0; // Índice del objeto vacío actual en el recorrido

    public float speed = 5f; // Velocidad de movimiento del NPC
    public float arrivalThreshold = 0.2f; // Distancia mínima para considerar que el NPC ha llegado al objeto vacío actual
    // Es decir, si un jugador se encuentra a 0.2 unidades del checkpoint, quiere decir que lego a el

     public float rotationSpeed = 2f; // Velocidad de rotación del NPC

    private void Update()
    {
        // Comprueba si aún hay objetos vacíos en el recorrido
        if (currentWaypointIndex < waypoints.Count)
        {
            // Obtiene la posición del objeto vacío actual
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;

            // Calcula la dirección hacia el objeto vacío actual
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();

            // Calcula la rotación hacia el objeto vacío actual
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Rota gradualmente hacia el objeto vacío actual
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Mueve el NPC hacia el objeto vacío actual
            transform.position += transform.forward * Time.deltaTime * speed;

            // Comprueba si el NPC ha llegado al objeto vacío actual
            if (Vector3.Distance(transform.position, targetPosition) < arrivalThreshold)
            {
                // Incrementa el índice del objeto vacío actual
		// Es decir, va hacia el punto siguiente en la pista
                currentWaypointIndex++;
            }
        }
    }
}
```

Adicional a eso, para hacer el juego más entretenido, se implementó una funcion para cambiar la velocidad dinámicamente cada 5 segundos

```
private IEnumerator ChangeSpeedPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Espera 5 segundos

            currentSpeed = GetRandomSpeed(); // Asigna una nueva velocidad aleatoria al NPC
        }
    }

    // Función para obtener una velocidad aleatoria dentro del rango especificado
    private float GetRandomSpeed()
    {
        return Random.Range(minSpeed, maxSpeed);
    }
```

## Personajes | Visuales del juego

### Modelo Maxwell 

Se utilizo un modelo de un gato, conocido como Maxwell online. Este modelo fue importado de sketchfab, hacho por bean(alwayshasbean)

![modelo maxwell](https://github.com/nekotletta/ccom4995/assets/99048617/ca75d013-8031-4b70-ad8b-5c42e7ec83ea)


### Modelo de autos y carreteras

Para el uso de las calles y el mapeado de la pista, se utilizó el siguiente paquete de texturas asset store

https://assetstore.unity.com/packages/3d/environments/urban/low-poly-street-pack-67475

![asset](https://github.com/nekotletta/ccom4995/assets/99048617/567e76c3-1d6e-49e7-87ec-4e8f3de1f45b)

  
Se creó un terreno para nuestra pista. Luego, usando las texturas y objetos provistas en el paquete se construyó una pista circular.

![image](https://github.com/nekotletta/ccom4995/assets/99048617/ea93124b-2cad-4a30-961f-895f65f3ffc9)


Con otros objetos se le añadieron algunos detalles para crear ambientación en la pista. 

![ambiente](https://github.com/nekotletta/ccom4995/assets/99048617/25274984-12f5-4514-80ce-e233764c4133)

Se añadió el agua y la cascada realizadas en clase para el entorno:

![awa](https://github.com/nekotletta/ccom4995/assets/99048617/13d8448d-8fa6-4dcf-8105-dcb056092088)


## Carro 

Para crear la fisica del carro, se utilizaron los scripts CarController y BodyTilt asociadas al unity asset siguiente:

https://assetstore.unity.com/packages/templates/systems/arcade-car-controller-lite-version-145489

![image](https://github.com/nekotletta/ccom4995/assets/99048617/7836a44d-ac8d-4a15-ba92-4982978c7eaf)

Se decidió añadirle un efecto de humo al carro

![image](https://github.com/nekotletta/ccom4995/assets/99048617/05f2bb3a-3ee3-444c-ab3e-8fea0bc65ec6)


Se añadieron diferentes cubos (box collider) alrededor de la pista para evitar que el jugador se saliera del mapa:

![image](https://github.com/nekotletta/ccom4995/assets/99048617/1d6eeb81-5bc0-4a25-bbc8-a6221ecffd93)


Resultado final del mapa:

![image](https://github.com/nekotletta/ccom4995/assets/99048617/c8ab4797-a77d-4a42-8d8c-af840edd654a)



Se importaron todos los efectos al proyecto y se añadieron al modelo final del corredor:


![image](https://github.com/nekotletta/ccom4995/assets/99048617/9434fdda-1546-4b99-9d0a-e4501b52e2f2)



Se añaden los otros carros:

![image](https://github.com/nekotletta/ccom4995/assets/99048617/bba211e1-fdbd-4e83-a48a-e7428a1d7e65)


Se le colocaron los efectos de humo al resto de los carros:

![image](https://github.com/nekotletta/ccom4995/assets/99048617/e52cc58c-5608-46d1-be1c-1226944d9147)


Se configuraron todos los "PowerUps":

![image](https://github.com/nekotletta/ccom4995/assets/99048617/e1d90895-4f56-45c7-a97d-cad0ae708f92)


Se colocaron las diferentes cajas de recompensa:

![image](https://github.com/nekotletta/ccom4995/assets/99048617/50a44f77-b5ac-439b-b6c4-e36b6ad9ec63)



