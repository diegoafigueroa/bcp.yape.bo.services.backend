Yape Client Registration Service
Descripción
Este proyecto implementa un servicio de registro de clientes para Yape Bolivia, utilizando una arquitectura hexagonal. La solución incluye una API web que permite registrar nuevos clientes y realizar validaciones de los datos ingresados, como la validación de números de teléfono y la verificación de datos en un servicio SOAP externo.

Arquitectura Hexagonal
Se ha utilizado la arquitectura hexagonal (también conocida como Ports and Adapters) para garantizar que el sistema sea flexible y fácilmente escalable. Esta arquitectura permite que las diferentes capas de la aplicación estén desacopladas, lo que facilita el mantenimiento y las pruebas. En esta arquitectura:

Puertos (Ports): Representan interfaces que definen las operaciones que el sistema debe realizar, como repositorios y servicios.
Adaptadores (Adapters): Implementan los puertos y permiten que el sistema interactúe con el mundo exterior, como bases de datos o servicios externos (por ejemplo, el servicio SOAP de personas).
Características del Proyecto
1. Exploración de la App Yape Bolivia
Para comprender el contexto funcional de los servicios, se realizó una exploración de la aplicación Yape Bolivia. Esto permitió obtener información valiosa sobre los flujos de registro de clientes y las validaciones asociadas a los datos de entrada. En base a esta exploración, se definieron las reglas de validación y los requisitos funcionales para el registro de clientes, tales como las restricciones en los números de celular, los tipos de documentos y otros parámetros requeridos.

2. Puertos y Adaptadores
ClientRepositoryPort: Puerto que define las operaciones para manejar los datos de los clientes (por ejemplo, agregar un cliente o verificar si un número de teléfono está registrado).
PeopleServicePort: Puerto que define las operaciones para interactuar con el servicio SOAP que obtiene información de personas basadas en su número de teléfono.
InMemoryRepository: Implementación en memoria del repositorio de clientes utilizando Entity Framework para facilitar el despliegue y las pruebas.
3. Validaciones del Modelo
Se implementaron varias validaciones para garantizar la integridad de los datos de los clientes al registrarse. Algunas de las validaciones más importantes son:

Validación del número de celular: Se verifica si el número de teléfono ya está registrado.
Validación en el servicio SOAP: Se consulta un servicio externo para obtener datos relacionados con el número de teléfono y se validan los datos del documento (tipo y número).
Validación de campos requeridos: Se implementaron validaciones para campos obligatorios como el nombre, apellido, número de celular, tipo de documento, etc.
Otras validaciones: Incluye validaciones de longitud de los campos y expresión regular para el teléfono y el número de documento.
4. Modelo de Registro de Cliente
El modelo ClientRegistrationRequest define los datos que se requieren para registrar un nuevo cliente. Este modelo incluye validaciones utilizando atributos como [Required], [StringLength] y [RegularExpression] para asegurar que los datos sean válidos.

5. Validación de Edad (No Implementada)
Aunque en la App de Yape Bolivia se requiere que los usuarios sean mayores de edad, esta validación no fue implementada en esta solución. Para agregarla, se podría verificar la fecha de nacimiento proporcionada por el cliente (si se recopila) y determinar si el usuario es mayor de edad.

6. Pruebas Unitarias
Se implementaron pruebas unitarias para la capa de la Web API, especialmente para verificar la validación de los datos de entrada. Estas pruebas se enfocan en verificar si los errores de validación son correctamente gestionados cuando los datos no cumplen con los requisitos del modelo.

7. Limitaciones y Áreas No Cubiertas
Autenticación y Autorización: El sistema no incluye ningún mecanismo de autenticación o autorización. No se implementaron medidas de seguridad como JWT o OAuth.
Rate Limiting: No se implementó limitación de velocidad para las solicitudes. Esta característica debería añadirse en una versión futura para evitar abusos.
Internacionalización y Globalización: El sistema no tiene en cuenta la internacionalización ni la globalización. Las validaciones y los mensajes de error están solo en español. En una implementación completa, se deberían considerar otros idiomas y la adaptación a diferentes regiones.
Multipais: La solución está explícitamente diseñada para Yape Bolivia y no se consideraron elementos de soporte para múltiples países (por ejemplo, diferentes formatos de teléfono o tipos de documento).
Conclusión
Este proyecto implementa una solución de registro de clientes con validaciones personalizadas y una arquitectura escalable basada en los principios de la arquitectura hexagonal. Las pruebas unitarias están orientadas a la validación de datos de la Web API, lo que facilita la futura expansión del sistema para cubrir otros casos y capas.
