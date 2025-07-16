

# Dentizone API

[![Docker Build and Push](https://github.com/dentizone/api/actions/workflows/docker-build.yml/badge.svg)](https://github.com/dentizone/api/actions/workflows/docker-build.yml)

Welcome to the backend API for **Dentizone**, a comprehensive e-commerce platform tailored for dental students. This powerful and secure API is built with .NET 9 and follows Clean Architecture principles to facilitate the buying and selling of dental supplies and equipment within a verified student community.

## ✨ Key Features

This API is packed with features designed to create a robust and secure marketplace experience:

-   **🔐 Authentication & Security:**
    -   JWT-based authentication with access and refresh tokens.
    -   Role-based authorization (Admin, Verified User, etc.).
    -   Secure password handling and account lockout policies.
    -   Token blacklisting for enhanced security on logout and refresh.

-   **👤 User Management & Verification:**
    -   Complete user lifecycle management (register, login, profile).
    -   **KYC Verification:** Integration with an external service (`Didit.me`) for robust identity verification of students.
    -   University-based user affiliation.
    -   Detailed user activity logging.

-   **🛒 E-commerce Core:**
    -   **Product Listings (Posts):** Create, search, filter, and manage product listings.
    -   **Catalog Management:** Admin-controlled categories and sub-categories.
    -   **Shopping Cart:** Persistent shopping cart functionality.
    -   **Order Management:** Full order lifecycle from placement to completion, cancellation, and shipping status updates.
    -   **Favorites:** Users can save posts to a personal wishlist.
    -   **Reviews & Ratings:** Users can review completed orders.

-   **💰 Financials:**
    -   **Wallet System:** Each user has a personal wallet for managing funds from sales.
    -   **Secure Withdrawals:** A complete workflow for users to request withdrawals, with admin approval/rejection.
    -   **Transaction Tracking:** Records all sales and commissions.

-   **🤖 AI & Automation:**
    -   **Background Jobs (Hangfire):** Asynchronous job processing for tasks like email sending and content moderation.
    -   **AI-Powered Moderation:** Automated content scanning (posts, Q&A, reviews) for toxic language and contact information to ensure platform safety.
    -   **Sentiment Analysis:** AI-driven analysis of user reviews.

-   **📊 Analytics & Administration:**
    -   **Admin Dashboard:** Endpoints to provide comprehensive analytics on users, posts, and sales revenue.
    -   Full administrative control over users, listings, and platform settings.

## 🏛️ Project Architecture

The project is structured using **Clean Architecture** to ensure separation of concerns, maintainability, and testability.

-   `Dentizone.Domain`: Contains the core business logic, including entities, enums, domain-specific exceptions, and repository interfaces. This layer has no external dependencies.
-   `Dentizone.Application`: Orchestrates the business logic. It contains application services, DTOs, AutoMapper profiles, FluentValidation rules, and interfaces for infrastructure services.
-   `Dentizone.Infrastructure`: Handles all external concerns, such as data access, external API clients, and third-party integrations.
    -   **Persistence:** Entity Framework Core with a `DbContext`, repository implementations, and database configurations.
    -   **Identity:** ASP.NET Core Identity for user authentication.
    -   **External Services:** Integrations with Cloudinary (image storage), Refit (for KYC and email APIs), Hangfire (background jobs), and Redis (caching).
    -   **Secret Management:** Securely manages application secrets using [Infisical](https://infisical.com/).
-   `Dentizone.Presentaion`: The API layer, which exposes endpoints to the outside world. It includes controllers, middleware for error handling, and the application's entry point (`Program.cs`).

## 🛠️ Built With

-   **Framework:** [.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) & ASP.NET Core
-   **Database:** SQL Server
-   **ORM:** Entity Framework Core
-   **Caching:** Redis
-   **Background Jobs:** Hangfire
-   **Secret Management:** Infisical
-   **File Storage:** Cloudinary
-   **Containerization:** Docker & Docker Compose

## 🚀 Getting Started

To get a local copy up and running, follow these steps.

### Prerequisites

-   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
-   [Docker Desktop](https://www.docker.com/products/docker-desktop/)
-   A SQL Server instance (local, Docker, or cloud)
-   A Redis instance
-   An IDE like [Visual Studio 2022](https://visualstudio.microsoft.com/) or [JetBrains Rider](https://www.jetbrains.com/rider/)

### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/dentizone/api.git
    cd dentizone-api
    ```

2.  **Configure Secret Management:**
    This project uses [Infisical](https://infisical.com/) to manage secrets. Create a `.env` file in the root directory and add your Infisical credentials:
    ```.env
    ClientId=your_infisical_client_id
    ClientSecret=your_infisical_client_secret
    ProjectId=your_infisical_project_id
    ENV=dev # or your target environment
    ```
    Alternatively, you can modify `Dentizone.Infrastructure/Secret/SecretService.cs` and the DI registration to use .NET's User Secrets or `appsettings.Development.json` for local development.

3.  **Update Database Connection:**
    Ensure your SQL Server connection string in Infisical (or `appsettings.json`) is correctly pointing to your database instance.

4.  **Apply Database Migrations:**
    Open a terminal in the `Dentizone.Presentaion` directory and run:
    ```bash
    dotnet ef database update
    ```

5.  **Run the application:**
    ```bash
    dotnet run --project Dentizone.Presentaion
    ```
    The API will be available at `http://localhost:5028`.

## 🐳 Running with Docker

You can also run the entire application using Docker Compose.

1.  **Create an environment file:**
    Create a `.env` file in the root of the project with the necessary secrets for the container:
    ```.env
    # Your Docker Hub username (if different from secrets)
    DOCKERHUB_USERNAME=your_dockerhub_username
    # Infisical Secrets
    ClientId=your_infisical_client_id
    ClientSecret=your_infisical_client_secret
    ProjectId=your_infisical_project_id
    ```

2.  **Start the services:**
    Run the following command from the root of the project:
    ```bash
    docker-compose up -d
    ```
    This will build the `dentizone-api` image and start the container. The API will be accessible on `http://localhost:8080`.

## 📜 API Documentation

The API is documented using OpenAPI (Swagger) and is accessible via [Scalar](https://github.com/scalar/scalar). Once the application is running, navigate to:

-   `http://localhost:5028/scalar`

You will find a beautiful, interactive API reference with all available endpoints, models, and authorization requirements.

## 🔄 CI/CD

This repository is configured with GitHub Actions for continuous integration and deployment.

-   **`cleanup.yml`**: Automatically formats code and removes unused `using` directives on pull requests to the `dev` branch.
-   **`docker-build.yml`**: On every push to the `dev` branch, this workflow:
    1.  Builds the Docker image.
    2.  Pushes the image to Docker Hub.
    3.  Triggers a deployment on a Coolify instance to update the running service.

---
