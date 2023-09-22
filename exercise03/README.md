# Ejercicio 3 - Viva México

Este proyecto consiste en:

a) Crear una pirámide representando el Templo Mayor
b) Crear un terreno para la pirámide
c) Importat assets de Asset Store para anadirlos a la escena
d) Crear una historia en base a las secciones previamente mencionadas

## a) Creación de la pirámide

Se utilizó la herramienta ProBuilder para la creación de este. ProBuilder es una herramienta que requiere ser descargada antes.

La base fue creada utilizando un cubo en probuilder. Luego se aplicó una selección de aristas. El propósito de esto fue empujar las aristas de la cara superior del cubo hacia adentro para hacer que la parte superior de este fuera mas pequeña. Esto nos da un trapecio como base.

![base-pir](./img/base-pir.png)

La pirámide tiene otras tres capas. Para lograr estas, duplicamos la base y le reducimos las dimensiones (en x y z)  en una escala de 0.25.

![bloques-pir](./img/bloques-pir.png)

Para la creación de las escaleras se uso un cubo simple. Se le dio algo de anchura para luego crear otro cubo más finito y colocarselo en el medio. Esto es para representar que hay dos escaleras(?).

![escaleras](./img/escaleras-pir.png)

Para la creación del tope de la pirámide se utilizo otro cubo probuilder.Se hizo lo mismo que se hizo para los bloques de la pirámide, solo que más notable. Es mas ancho y hay una mayor diferencia entre la cara de abajo y la de arriba.

![tope-pir](./img/tope-pir.png)

Ya al final se decidió colocar una textura de bloques/ladrillos arenosos para simular la antiguedad de la piramide (de acuerdo con lo visto en google). Para las escalaeras se utilizo una textura con muchas lineas arenosas para simular escalones.

![piramide](./img/piramide.png)

## b) Creación del terreno para la pirámide


Se empieza creando un terreno normal y se le da las dimensiones deseadas. Se pinta con la textura deseada para la base.

![base-terreno](./img/base-terreno.jpeg)

Añadimos los "Layers" con las texturas que vayamos a usar para pintar los detalles. En este caso se les dio un tono azulado para dar la ilusión de un clima más frío.

![layers-terreno](./img/layers-terreno.jpeg)

Seleccionamos la herramienta de "Raise or Lower terrain" para darle el relieve.

![relieve-terreno](./img/relieve-terreno.jpeg)

Pintamos el terreno y añadimos detalles.

![detalles-terreno](./img/detalles-terreno.jpeg)
