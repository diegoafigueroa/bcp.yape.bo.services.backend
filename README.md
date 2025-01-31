# Yape Client Registration Service

## Descripción

Este proyecto implementa un servicio de registro de clientes específicamente diseñado para **Yape Bolivia**. Utilizando una **arquitectura hexagonal**, se logró crear un sistema modular, flexible y fácil de escalar. La arquitectura implementada proporciona una separación clara entre la lógica de negocio central y los detalles de infraestructura, permitiendo que los diferentes componentes del sistema puedan evolucionar de forma independiente. La solución ofrece una API Web RESTful que gestiona el proceso de registro de clientes, con validaciones de datos y consumo de un servicio SOAP externo para verificar la existencia de personas mediante el número de teléfono.

El servicio también está documentado mediante **Swagger**, lo que facilita la comprensión de los endpoints y su interacción con la API. Además, se incluye un ejemplo de prueba unitaria para la capa Web API, que puede ser extendido con más pruebas en el futuro.

## Arquitectura

El sistema fue construido siguiendo los principios de la **arquitectura hexagonal** (también conocida como **Ports and Adapters**). Este enfoque garantiza que las distintas capas de la aplicación estén desacopladas entre sí, promoviendo una estructura flexible que facilita la implementación de cambios sin afectar otras partes del sistema. La arquitectura permite que la lógica de negocio central sea independiente de la infraestructura y las tecnologías externas como bases de datos, servicios web, etc.

### Componentes Principales

- **Puertos (Ports)**: Definen las interfaces para las operaciones que deben implementarse, como la gestión de los datos de clientes y la integración con servicios externos (como el servicio SOAP para consultar información de personas).
- **Adaptadores (Adapters)**: Implementan los puertos y actúan como el punto de interacción entre el sistema interno y el mundo exterior. Los adaptadores manejan las interacciones con la base de datos (repositorios), los servicios externos, la API Web, etc.
- **Servicio de Registro de Cliente**: El servicio principal que valida y registra a los clientes en el sistema. Este servicio realiza varias validaciones, como verificar que el número de teléfono no esté registrado previamente, que el tipo y número de documento coincidan con los registros de la persona, y que los datos sean correctos antes de proceder con el registro.

### Swagger para Documentación

Se utilizó **Swagger** para generar la documentación interactiva de la API. Swagger permite a los desarrolladores explorar todos los endpoints disponibles de la API, así como probarlos directamente desde la interfaz web. Este enfoque facilita la integración con otros sistemas y ayuda a mantener una documentación siempre actualizada y accesible. La documentación está completamente en español, adaptándose a las necesidades del equipo de desarrollo y los usuarios finales en Bolivia.

### Repositorio y Servicios Externos

- **ClientRepositoryPort**: Un puerto que define las operaciones de acceso a datos de los clientes, como agregar un nuevo cliente y verificar si un número de teléfono ya está registrado.
- **PeopleServicePort**: Un puerto que define las operaciones necesarias para interactuar con un servicio SOAP externo que obtiene información sobre las personas a partir del número de teléfono.
- **InMemoryRepository**: Para facilitar el desarrollo y las pruebas, se implementó un repositorio en memoria utilizando **Entity Framework**, lo que permite el despliegue rápido sin la necesidad de configurar una base de datos externa.

### Validaciones del Modelo

El servicio incluye un conjunto de **validaciones** robustas para garantizar que los datos ingresados por los usuarios sean válidos y cumplan con los requisitos establecidos:

- **Validación de número de celular**: El servicio verifica si el número de teléfono ya está registrado en el sistema antes de proceder con el registro.
- **Consumo de servicio SOAP**: Se consulta el servicio SOAP externo para obtener información sobre el número de teléfono y se realiza la validación de los documentos asociados a esa persona, verificando que el tipo y número de documento coincidan con los registros existentes.
- **Validación de campos obligatorios**: Se valida que los campos esenciales (nombre, apellido, número de celular, tipo de documento, número de documento) estén presentes y sean correctos.
- **Validación de formato**: Los campos como el número de celular y el número de documento se validan utilizando expresiones regulares para asegurar que sigan el formato esperado.

### Modelo de Registro de Cliente

El modelo `ClientRegistrationRequest` define los datos necesarios para registrar un cliente en el sistema. Este modelo es validado a través de anotaciones como `[Required]`, `[StringLength]`, `[RegularExpression]`, entre otras, para garantizar la validez de los datos antes de ser procesados por la lógica del negocio.

#### Campos del Modelo:

- **Nombre y Apellido**: Los campos de nombre y apellido tienen un mínimo de 2 caracteres y un máximo de 100.
- **Número de Celular**: El número de celular debe tener una longitud exacta de 8 caracteres y debe comenzar con un 6 o un 7.
- **Tipo y Número de Documento**: Se validan mediante enumeraciones y expresiones regulares para asegurar que el formato sea el correcto.

### Validación de Edad (No Implementada)

Aunque **Yape Bolivia** requiere que los usuarios sean mayores de edad para completar el registro, esta validación no fue implementada en la versión actual del sistema. Esta validación podría añadirse fácilmente en una futura actualización, donde se verifique que la persona registrada sea mayor de 18 años a partir de su fecha de nacimiento.

### Consideraciones Regionales

El sistema está construido explícitamente para **Yape Bolivia** y no tiene en cuenta elementos de **multipaís**. Esto implica que las reglas de validación, como el formato de número de celular y el tipo de documentos, están diseñadas para cumplir con los requisitos específicos de Bolivia. La solución no incluye funcionalidades de internacionalización ni de localización, ya que está enfocada en un único país y sus normativas locales.

### Repositorio In-Memory

Para facilitar el desarrollo y despliegue rápido del sistema, se implementó un **repositorio en memoria utilizando Entity Framework**. Esto permite trabajar sin una base de datos real y simplifica el proceso de prueba y despliegue en entornos de desarrollo. Sin embargo, se recomienda implementar un repositorio de base de datos real para entornos de producción.

## Requisitos

- **.NET 8.0**
- **Entity Framework Core**
- **Swagger UI**

## Ejecución

Para ejecutar el servicio de registro, simplemente se debe iniciar el proyecto y acceder a la interfaz Swagger en `http://localhost:5000/swagger` para explorar los diferentes endpoints disponibles.

## Pruebas Unitarias

El proyecto incluye 1 ejemplo de **pruebas unitarias** para validar el funcionamiento de la capa de Web API. Estas pruebas aseguran que los endpoints respondan correctamente a diferentes escenarios de entrada. No fue posible construir mayores pruebas unitarias, debido al corto tiempo de desarrollo. Queda como futura mejora un mayor cobertura en TDD.

## Limitaciones y Áreas No Cubiertas

El sistema presenta ciertas limitaciones que deben tenerse en cuenta antes de su implementación en un entorno de producción:

- **Validación de Edad No Implementada**: Aunque **Yape Bolivia** requiere que los usuarios sean mayores de edad para completar el registro, esta validación no fue implementada en la versión actual del sistema. Se recomienda agregar esta validación en una futura versión.
- **Falta de Autenticación y Autorización**: El sistema no implementa ningún mecanismo de autenticación o autorización, lo que es un requisito fundamental en aplicaciones reales. Se recomienda implementar estas funciones para garantizar la seguridad de los datos de los usuarios.
- **No Se Consideraron Escenarios de Multipaís**: El sistema está diseñado explícitamente para **Yape Bolivia**, sin considerar casos de uso para otros países. No se implementaron funcionalidades de internacionalización ni localización. En un futuro, se podría considerar la extensión del sistema para ser utilizado en otros países de América Latina.
- **Limitaciones en la Base de Datos**: Se utiliza un repositorio en memoria para facilitar las pruebas y el desarrollo rápido. Esto no es adecuado para un entorno de producción, y se recomienda reemplazarlo por una base de datos real.
