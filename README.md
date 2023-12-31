# CodesOfTheRing API

## Introducción

El proyecto Codes of the Rings tiene como objetivo resolver problemas críticos en el mundo de
la programación. Los desarrolladores, independientemente de su nivel de experiencia, a menudo
enfrentan desafíos al buscar oportunidades para mejorar sus habilidades y mantenerse al día
con las cambiantes tendencias tecnológicas. Sin embargo, este viaje de mejora profesional
presenta desafíos significativos.

En primer lugar, muchos desarrolladores luchan por encontrar desafíos de programación que se
alineen con sus intereses y nivel de habilidad. La mayoría de las plataformas en línea ofrecen
ejercicios genéricos que pueden no ser relevantes o estimulantes para un individuo en particular.
Esto puede llevar a la falta de motivación y a un estancamiento en el aprendizaje.
Además, la programación es, en su mayoría, una tarea solitaria. La falta de interacción y
colaboración con otros desarrolladores puede resultar en una sensación de aislamiento. Esto
significa que la valiosa oportunidad de compartir conocimientos, explorar ideas y aprender de
otros se ve limitada.

La ausencia de un sistema efectivo para verificar las habilidades de otros desarrolladores es otro
desafío. Cuando los desarrolladores buscan competir o colaborar, no siempre tienen una manera
confiable de evaluar las habilidades de sus compañeros. Esto puede llevar a malentendidos y
subestimación del potencial de las personas.

Esta plataforma no solo ofrece un espacio para la resolución de problemas, sino que también
promueve la formación de una comunidad interactiva, donde los desarrolladores pueden
colaborar, compartir conocimientos y verificar sus habilidades. Codes of the Rings se destaca por
su apertura a diversos lenguajes de programación, la seguridad en la ejecución de ejercicios y la
capacidad de usar recursos de internet en sus ejercicios que previamente deberán aprobarse por
los administradores de la aplicación, comprometiéndose a brindar una experiencia que
enriquezca el crecimiento profesional de los desarrolladores en todo el mundo

## Clases

### Solicitudes

#### LoginRequest

Esta clase se utiliza para manejar las peticiones de login que tengan usuario y contraseña.

| *Atributo* | *Tipo* |
| ---------- | ------ |
| User | string |
| Password | string |

#### SignupRequest

Esta clase se utiliza para manejar una petición para registrar un nuevo usuario.

| *Atributo* | *Tipo* |
| ---------- | ------ |
| Nickname | string |
| Email | string |
| Name | string |
| Surname | string |
| SecondSurname | string? |
| Birthdate | string |
| Affiliation | string? |
| Password | string |

### Respuestas

#### ApiExceptionResponse

Esta clase será el objeto que se devuelve cuando se quiera indicar un error.

| *Atributo* | *Tipo* |
| ---------- | ------ |
| StatusCode | short |
| Message | string |

#### LoginResponse

Esta clase será el objeto que se devuelve como respuesta satisfactoriamente el login de usuarios.

| *Atributo* | *Tipo* |
| ---------- | ------ |
| AccessToken | string |
| RefreshToken | string |

#### TokenResponse

Esta clase será el objeto que se devuelve como respuesta satisfactoria para que otras aplicaciones NO web consuman la API.

| *Atributo* | *Tipo* |
| ---------- | ------ |
| Token | string |