📚 **SGB - Sistema de Gestión de Biblioteca**

Sistema web para la gestión de bibliotecas que permite administrar libros, préstamos, usuarios y multas de manera eficiente.

El proyecto está desarrollado con **ASP.NET Core Web API** en el backend y **React + Vite** en el frontend, utilizando **Entity Framework Core** para el acceso a datos y una **arquitectura por capas** para mantener el código limpio y escalable.


🚀 **Tecnologías Utilizadas**
**Backend**

- C#

- ASP.NET Core Web API

- Entity Framework Core

- SQL Server


**Frontend**

- React

- Vite

- JavaScript

- CSS

**Arquitectura del Proyecto**

El backend sigue una **arquitectura por capas** para separar responsabilidades:

SGB
│

├── SGB.API

│   ├── Controllers

│   └── Configuración de la API

│

├── SGB.Application

│   ├── DTOS

│   ├── Repositorios

│   ├── Seguridad

│   └── servicios

│

├── SGB.Domain

│   └── Entidades

│

├── SGB.Infrastructure

│   └── DbContext



**Explicación de cada capa**

**Domain**

- Contiene las entidades del sistema que representa el modelo de negocio.

**Application**

- Contiene la lógica de negocio.

- Servicios.

- DTOs y Mapeos

- Seguridad

**Infrastructure**

- Acceso a la base de datos.

- Configuración de Entity Framework.

**API**

- Controladores.

- Endpoints REST.


⚙️ **Funcionalidades**

📖 **Gestión de Libros**

- Crear libros

- Editar libros

- Eliminar libros

- Listar libros disponibles

👥 **Gestión de Usuarios**

- Registro de usuarios(empleados)

- Autenticación con JWT

- Roles de usuario

- Regitro de clientes(lectores)


📦 **Préstamos de Libros**

- Registrar préstamo

- Registrar devolución

- Ver préstamos activos

- Ver préstamos retrasados

💰 **Multas**

- Cálculo automático de multas(proximamente)

- Configuración de tarifa diaria

- Cálculo en tiempo real sin devolver el libro(proximamente)

🗄 **Base de Datos**

- El sistema utiliza **SQL Server** con **Entity Framework Core.**

🔐 **Autenticación**

La API utiliza **JWT (JSON Web Tokens)** para la autenticación de empleados.

Flujo:

- El empleado inicia sesión.

- El servidor genera un token JWT.

- El cliente envía el token en cada petición.

📌 **Características del Proyecto**

✔ Arquitectura limpia

✔ API REST

✔ Entity Framework Core

✔ Inyección de dependencias

✔ Manejo de DTOs

✔ Mapeos

✔ patron repositorio

✔ Autenticación JWT

✔ Cálculo automático de multas(proximamente)


📚 **Objetivo del Proyecto**

Este proyecto fue desarrollado con fines de **aprendizaje y práctica de desarrollo fullstack**, aplicando buenas prácticas como:

- Arquitectura por capas

- Separación de responsabilidades

- Uso de DTOs
  
- Uso de AutoMapper
  
- Uso de patron repositorio

- Diseño de APIs REST

- Manejo de Entity Framework


📈 **Mejoras Futuras**
- Cálculo automático de multas
  
- Diseño responsive CSS

- Busqueda dinamica de clientes y de libros

- Validar email y telefono

- Notificciones y alertas

- Estadisticas libros mas prestados

- Otros


👨‍💻 **Autor**

Desarrollado por **[Jose Guillermo]**

📧 Email: darlinguillermo@gmail.com

