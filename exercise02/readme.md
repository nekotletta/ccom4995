Ejercicio 02 - Creacion de prefabs, variantes y terrenos

Este proyecto consiste en 
a) Creacion de un prefab junto a cuatro variantes del personaje Kirby unicamente usando las formas
   basicas provistas por Unity
b) Representar una configuracion geologica usando el objeto terreno en Unity


a)
I - Creacion del prefab de Kirby

Se utilizo una esfera para crear el cuerpo de Kirby y dos capsulas para crear los ojos. Todos los
componentes de este fueron unidos usando la funcion de jerarquia en Unity. Una vez la jerarquia fue
establecida, arrastramos el modelo hacia la seccion de "Assets" para convertirlo en un prefab

![prefab-kirby](./img/kirby-prefab.png)

II - Creacion de las variantes de Kirby

a) Variante Kirby Link

![kirby-link](./img/kirby-link.png)


Lo primero que se hizo fue posicionar las extremidades lo mas cercano a como se puede ver en la imagen.
Para los brazos se usaron esferas. Para las piernas se usaron capsulas.

![kirby-cuerpo](./img/kirby-cuerpo.png)
![kirby-showcase](./img/kirby-showcase.png)

Para poder posicionar las extremidades fue necesario tomar en consideracion los angulos y la posicion
con relacion al cuerpo (prefab) 

Luego de completar las extremidades se le annadieron los accesorios.

El sombrero fue creado utilizando dos capsulas (la base del sombrero y la "cola" (darle longitud)) y 
una esfera para la bolita al final.

![kirby-sombrero](./img/kirby-sombrero)

La espada fue creada con cuatro cubos. Dos fueron utilizados para crear el mango de la espada, 
uno para crear la hoja de la espada y el ultimo para crear el filo. 
El filo fue creado girando el cubo a 45 grados y posicionandolo en el borde, ya que Unity no tiene triangulos. 

![kirby-espada](./img/kirby-ready.png)

Como toque final se le anadio color al modelo

![kirby-final](./img/kirby-final.png)

b) Variante Kirby 2

c) Variante Kirby 3

d) Variante Kirby 4



III. Region geologica

IV. Conclusion

