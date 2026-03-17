A full‑stack application built with **React** (frontend) and **ASP.NET Core Web API** (backend), containerized with **Docker** and orchestrated using **Docker Compose**. 
---

## 📐 Architecture

- **Frontend**:  
  - React app   
  - Provides UI for listing users (table or card layout), adding new users, and navigation.  
  - Styled with custom CSS for modern, responsive design.

- **Backend**:  
  - ASP.NET Core 8 Web API.  
  - Exposes endpoints for CRUD operations on users.  
  - Uses Entity Framework Core with SQLite for persistence.  
  
- **Deployment**:  
  - Docker multi‑stage builds for optimized images.  
  - Docker Compose orchestrates frontend and backend services.  
  - Ports mapped for easy local development.

---

## ⚙️ Prerequisites

- [Node.js](https://nodejs.org/) (for local frontend dev)
- [.NET 8 SDK](https://dotnet.microsoft.com/) (for backend dev)
- [Docker](https://www.docker.com/) & [Docker Compose](https://docs.docker.com/compose/)

---

#  Development Process

1. **Frontend (React)**  
   - Built with React hooks (`useState`, `useEffect`).  
   - Components: `UserComponent` (list users), `AddUser` (form with validation), `NavBar` (navigation).  
   - Styled with custom CSS for responsiveness.  
   - API calls handled via `service.js`.

2. **Backend (ASP.NET Core)**  
   - RESTful API with CRUD endpoints.  
   - SQLite database via EF Core.  
   - Validation rules enforced in controllers/models.  
   - Swagger UI enabled for API documentation.

3. **Testing**  Yet to cover 
   - Unit tests with xUnit and EF Core InMemory.  
   - Controller tests with Moq.  
   - Integration tests using `WebApplicationFactory<Program>`.

4. **Deployment**  Yet to cover
   - Multi‑stage Docker builds (SDK → runtime).  
   - Docker Compose orchestrates frontend + backend.  
   - Environment variables managed via `.env`.

---

## 🚀 Setup & Run

### 1. Clone the repository
## ```bash
git clone https://github.com/your-username/usersRepo.git
cd usersRepo

---

## 🚀 Setup & Run

Run Docker-compose up --build

Frontend (React UI) → http://localhost:3000

# Swagger only for Development phase
Backend API (Swagger) → http://localhost:5000/swagger
Backend API (Base) → http://localhost:5000/api/users

---

# AI Tool

Microsoft Copilot was used during development for:

Generating boilerplate code (React components, CSS).

