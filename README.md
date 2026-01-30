📦 Catalog.Order.API (.NET 10 Microservice)

Este repositorio contiene el microservicio de Órdenes diseñado bajo principios de Arquitectura Limpia (Clean Architecture). Es el componente central encargado de gestionar las transacciones de compra y comunicarse de forma asíncrona con el ecosistema de microservicios.
🚀 Tecnologías y Herramientas

    Framework: .NET 10.
    Persistencia: Entity Framework Core con PostgreSQL.
    Mensajería: Productor de eventos usando Apache Kafka.
    Orquestación: Desplegado en Kubernetes.
    CI/CD: Pipeline automatizado en Jenkins.

🏗️ Arquitectura

El servicio sigue una estructura de capas para asegurar el desacoplamiento:
    
    Application: Casos de uso y lógica de orquestación.
    Domain: Entidades de negocio (PurchaseOrder, OrderItem).
    Infrastructure/Adapters/Driven: Implementaciones de Postgres (DB) y Kafka (Event Bus).
    Infrastructure/Adapters/Driving/API: Controladores REST para la creación de órdenes.

🛠️ Configuración en Kubernetes

El despliegue se gestiona mediante el archivo deployment.yaml, el cual configura dinámicamente:

    Service Discovery: Conexión al broker mediante kafka-service:9092.
    Base de Datos: Conexión resiliente a Postgres.

📡 Endpoints Principales
Crear Orden

POST /api/orders/create
JSON

{
  "customerId": "UUID",
  "items": [
    {
      "productId": "UUID",
      "quantity": 2,
      "unitPrice": 150.0
    }
  ]
}
