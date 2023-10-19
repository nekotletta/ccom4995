# Ejercicio 5 - Go ahead and jump

Este ejercicio consiste en usar el input actions system para mover nuestro personje y hacerlo bricar y moverlo rapido.

## a) Como implementar el Ipnut System 

El input system es un sistema que nos permite mapear teclas a las acciones que queramos en vez de tener que usar condiciones para chequear cada tecla. 

Para poder utilizar el input system es necesario descargarlo desde Unity Package Manager de la siguiente manera: 


Window > Package Manager > Unity Registry > Input System > Download


Una vez este se descargue, Unity se va a reiniciar. Ya luego de que se reinicie este podra ser utilizado. 

## b) Mapping de moviemientos

Una vez el objeto este en Unity, podemos mapear acciones a este. 

Para hacerlo es necesario crear un objeto Player Input y ponerle un nombre.

Los movimientos a mapear van a ser:

Move - Poder moverse hacia alfrente, atras, izquierda y derecha

FIre - Poder disparar balas cada vez que se hace click en la pantalla

Look - Poder girar al personaje 

Jump - Hacer el personaje salte

FastMov - Poder desplazar el personaje mas rapido si estamos presionando la tecla W junto a la tecla shift

## c) Showcase de mapping 

### Showcase Move 

[!image](./img/walk_run.gif)

Visual graph para Move

[!image](./img/move_graph.gif)


### Showcase Fire

[!image](./img/shoot.gif)

Visual graph para Fire

[!image](./img/fire_graph.gif)


### Showcase Look

[!image](./img/cursor.gif)

Visual graph para Look

[!image](./img/look_graph.gif)

### Showcase Jump

[!image](./img/doubleJump.gif)

Visual graph para Jump

[!image](./img/jump_graph.gif)


## d) Notas de las graficas

Move graph

[!image](./img/move1.png)

[!image](.img/move2.png)

Fire graph

[!image](./img/fire.png)

Look graph

[!image](./img/look.png)

Jump graph

[!image](./img/bribcar1.png)

[!image](./img/bribcar2.png)

[!image](./img/bribcar3.png)
