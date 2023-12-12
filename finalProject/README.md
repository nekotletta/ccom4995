# Final Project - Fast Cats

El ultimo proyecto de este curso consiste en crear un videojuego utilizando los conceptos que hemos aprendido en la clase. En esta documentacion se encontrara el aspecto tecnico y lo que contiene el juego.


## Item System

El Item Box es un sistema similar a los que muchos conocemos, un jugador hace contacto con la caja y se corre una ruleta para ver cual poder le toco al jugador y utilizarlo para su ventaja y ganar la carrera.

Cuando jugador hace contacto con un bloque, el bloque desaparece por un segundo y vuelve.

![image](img/ItemBox.gif)

En este tiempo, se invoca una función que escoge un item aleatorio de la lista de prefabs que contienen los items fisicos. Esto funciona activando uno de los prefabs del los items que contiene el mismo jugador desactivados en el tope de la cabeza. OJO (Esto implica que los prefabs de los items que tiene el modelo del jugador, tienen que ser iguales a los de la lista)

![image](img/ItemSetup.png)

Cuando le das al mouse, desactiva el item dezplegado, y el script de ItemSystem.cs invoca la función del effecto del power up basado en el tag del prefab que fue seactivado.

![image](img/ItemCabeza.png)


## Powerups

Los siguientes powerups le puede salir la jugador dentro del Item Box.

### Boost

Aumenta la velocidad del jugadorpor unos segundos para poder pasarle a tus oponentes satisfactoriamente.

![image](img/Boost.gif)

### HoneyBun

Como todos sabemos, los honeybuns ponen lento a una persona. En este caso el jugador emite tres honeybuns en la parte de atras para los oponentes correr sobre ellos y paralizarlos temporeramente. Aún hay que tener cuidado, ya que si el jugador corre sobre estos mismos va a perder la velocidad temporeramente. 

Jugador activando poder

![image](img/Drop.gif)

Jugador recibiendo effectos del honeybun

![image](img/SlowDownPlayer.gif)

Enemigo recibiendo effectos del honeybun

`<Colocar aqui gif de enemigo comer honeybun>`

### Bricks

El jugador dispara vareos ladrillos proyectiles, si un ladrillo le da a un enemigo, lo va a paralizar por un momento.

`<Colocar aqui gif de jugador disparando a carro>`

### Logica de Powerups

Todos los power ups siguen un patron similar, corren unas acciones o effectos por un numero fijo de segundos. Los power ups y los effectos de objetos emitidos por resultado del powerup, fueron implementadas utilizando IEnumerators. Esto permitía que pudieramos manipular las variables de los componentes del jugador o computadora por un periodo de tiempo y reversar el effecto despues de que el respectivo tiempo pase como demonstrado en la siguiente ilustración.

```
	IEnumerator PowerOrEffect(arguments){
        // Get gameObject component if needed EJ: gameObject.GetComponent<ManipulatedComponent>();
        // Manipulate variables 
        yield return new WaitForSeconds(n);
        // Reverse back variables to normal state
    }
```

Tambien para hacer algo como los powerups que emitian objetos por entre segundos por un numero de segundos, podíamos implementar el 'yield return'  dentro de un loop para dar el resultado deseado.

```
	IEnumerator DropOrShootPower(arguments){
	    // Get gameObject component if needed EJ: gameObject.GetComponent<ManipulatedComponent>();
        for(int i=0; i < numberOfSeconds; i++){
            // Drop or shoot object out player's drop or shoot point
            yield return new WaitForSeconds(1);
        }
    }
```

Si algun lector desea implementar algun nuevo powerup,  se puede repetir el sigiente siglo:
	1.  Crear un tag
	2.  Crear un prefab con el nombre del tag
	3.  Colocar prefab en poscicion y tamaño que los otros prefabs encontrados en items dentro del prefab del jugador.
	4.  Añadir nuevo prefab a la lista items en el script ItemSystem.cs
	5.  Dentro de ItemSystem.cs, crear función del effecto y invocarla en la función de ActivatedItem(string itemTag).


## Implementacion IA

Cualquier juego de carreras requiere oponentes IA junto al jugador. Se le anadio IA al juego para simular otros jugadores en la pista usando el package de AI Navigation en Unity. A

Paquete obtenido mediante

```
Window > Package Manager > Unity Registry > AI Navigation
```

Una vez se obtuvo el paquete, se comenzo el desarrollo de el componente IA del juego. 

Empezamos haciendo algunas pruebas en un archivo de Unity diferente antes de implementrlo en el juego. 

### Pruebas de IA iniciales:

Las siguientes explicaciones sobre estas pruebas se aplican al proyecto. Fueron creadas a pequena escala para poder visualizarlas mejor. 

Se creo un proyecto nuevo en Uniy especificamente para crear y probar el IA. Para esto se siguieron los siguientes pasos:

Paso 1: Terreno basico para probar la IA:

Se utilizaron 5 cubos para el terreno. Uno fue utilizado como plataforma base y luego se le anadieron 4 cubos para hacer una "carretera" simple para que el IA guiara por ella. A

[IMAGEN DE CARRETERA PLAIN]

Paso 2: Navmesh Surfaces

Para seleccionar el espacio que el IA podia recorrer se utilizo el componente de NavMesh del paquete AI Navigation

```
Click derecho > AI > Navmesh Surface 
```

Una vez el objeto ha sido ceado, simplemente se le hace click al objeto y le damos click en "Bake". Bake colorea el path por donde nuestro agente IA va a poder moverse

[IMAGEN DEL NAVMESH]

Paso 2.5: Checkpoints

Para que el IA pueda correr mejor en la pista, se anadieron checkpoints a traves de la pista. El uso de estos sera explicado mas adelante.

[IMAGEN DE CHECKPOINTS]

### Sistema de checkpoints

El sistema de checkpoints consta de una serie de cubos ( 3D objects > Cube ) colocados a traves de la pista. Esto ayudara a dirigir la IA por la pista.

Para poder identifcar los checkpoints en la pista, se le coloco un tag con el nombre "Checkpoint" a cada uno de estos. El script encuentra todo objeto que tenga un tag con ese nombre y los coloca en una lista. El IA tiene que seguir los checkpoints en el orden en que aparecen en la lista. 

[SNIPET DEL CODIGO DE TAG Y LISTA]

Una vez el IA recorre todos los checkpoints, el indice se incrementa modularmente, por lo que el IA va a seguir dando vueltas.

