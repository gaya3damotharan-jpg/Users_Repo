## backend project

# Prerequisites
.NET 8 DSK
Docker

# Local run

cd Backend/usersAPI
dotnet restore
dotnet build
dotnet run

Swagger UI → http://localhost:5000/swagger

Base API → http://localhost:5000/api/users

# docker

cd Backend/usersAPI
docker build -t usersapi .
docker run -p 5000:8080 usersapi