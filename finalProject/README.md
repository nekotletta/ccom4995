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

