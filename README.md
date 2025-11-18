# OpenTriviaApp

OpenTriviaApp is a full-stack application that allows users to answer trivia questions fetched from the Open Trivia Database. It consists of:

- **Backend**: ASP.NET Core Web API using `IHttpClientFactory` for efficient HTTP calls.
- **Frontend**: React + Vite with Bootstrap for styling.
- **Deployment**: Dockerized with `docker-compose` for running both services together.

---

## ✅ Features
- Fetches trivia questions from Open Trivia DB.
- Displays questions and multiple-choice answers.
- Prevents exposing correct answers in the frontend.
- Calculates and displays the number of correct answers.
- Handles HTML entities properly (e.g., `&quot;` → `"`).
- Fully Dockerized for easy deployment.

---

## 🛠 Project Structure
```
OpenTriviaApp/
├── backend/                # ASP.NET Core Web API
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Program.cs
│   └── Dockerfile
├── frontend/               # React + Vite app
│   ├── src/
│   ├── index.html
│   └── Dockerfile
└── docker-compose.yml      # Runs both services together
```

---

## 🚀 How to Run Locally

### Backend
```bash
cd backend
dotnet run
```
API will be available at `http://localhost:5000`.

### Frontend
```bash
cd frontend
npm install
npm run dev
```
Frontend will be available at `http://localhost:5173`.

---

## 🐳 Run with Docker Compose
```bash
docker-compose up --build
```
- Frontend: `http://localhost:3000`
- Backend: `http://localhost:5000`

---

## ✅ Fixes in Latest Version
- Correct result display: `You got X out of Y correct`.
- HTML entities decoded using `dangerouslySetInnerHTML`.
- Axios used for API calls.
- Added Dockerfile for both services and `docker-compose.yml`.

---

## ☁️ Deployment
You can deploy this app for free on **Render**:
1. Push the project to GitHub.
2. Create a new Blueprint on Render.
3. Render will detect `docker-compose.yml` and deploy both services.
