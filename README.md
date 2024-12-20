# Документация по юнит-тестам для API

## 1. **AuthApiTests**

### **1.1 Test_Register_Success**
- **Описание:** Проверяет успешную регистрацию пользователя.
- **Метод:** POST `/register`
- **Входные данные:**
  ```json
  {
      "email": "test@example.com",
      "password": "password123",
      "first_name": "John",
      "last_name": "Doe",
      "phone": "1234567890",
      "role_id": 1
  }
  ```
- **Ожидаемый результат:** Статус код 201 (Created) и сообщение "Студент успешно зарегистрирован".

### **1.2 Test_Register_MissingFields**
- **Описание:** Проверяет ошибку при отсутствии обязательного поля.
- **Метод:** POST `/register`
- **Входные данные:**
  ```json
  {
      "email": "test@example.com"
  }
  ```
- **Ожидаемый результат:** Статус код 400 (Bad Request) и сообщение "password is required".

### **1.3 Test_Login_Success**
- **Описание:** Проверяет успешную авторизацию пользователя.
- **Метод:** POST `/login`
- **Входные данные:**
  ```json
  {
      "email": "test@example.com",
      "password": "password123"
  }
  ```
- **Ожидаемый результат:** Статус код 200 (OK) и наличие токена в ответе.

### **1.4 Test_Login_InvalidCredentials**
- **Описание:** Проверяет ошибку при неверных учётных данных.
- **Метод:** POST `/login`
- **Входные данные:**
  ```json
  {
      "email": "wrong@example.com",
      "password": "wrongpassword"
  }
  ```
- **Ожидаемый результат:** Статус код 401 (Unauthorized) и сообщение "Неверные учетные данные".

### **1.5 Test_Login_MissingFields**
- **Описание:** Проверяет ошибку при отсутствии обязательного поля.
- **Метод:** POST `/login`
- **Входные данные:**
  ```json
  {
      "email": "test@example.com"
  }
  ```
- **Ожидаемый результат:** Статус код 400 (Bad Request) и сообщение "password is required".

---

## 2. **GitHubApiTests**

### **2.1 Test_GetRepos_Success**
- **Описание:** Проверяет успешное получение списка репозиториев пользователя.
- **Метод:** GET `/repos`
- **Входные данные:**
  ```json
  {
      "github_url": "https://github.com/testuser"
  }
  ```
- **Ожидаемый результат:** Статус код 200 (OK) и наличие данных о репозиториях.

### **2.2 Test_GetRepos_MissingGitHubUrl**
- **Описание:** Проверяет ошибку при отсутствии параметра `github_url`.
- **Метод:** GET `/repos`
- **Входные данные:** Нет.
- **Ожидаемый результат:** Статус код 400 (Bad Request) и сообщение "GitHub URL обязателен".

### **2.3 Test_AddRepos_Success**
- **Описание:** Проверяет успешное добавление репозиториев в базу данных.
- **Метод:** POST `/add_repos`
- **Входные данные:**
  ```json
  {
      "github_url": "https://github.com/testuser",
      "id_resume": 1
  }
  ```
- **Ожидаемый результат:** Статус код 200 (OK) и сообщение "generated_description".

### **2.4 Test_AddRepos_MissingParameters**
- **Описание:** Проверяет ошибку при отсутствии обязательных параметров.
- **Метод:** POST `/add_repos`
- **Входные данные:** Нет.
- **Ожидаемый результат:** Статус код 400 (Bad Request) и сообщение "GitHub URL и resume_id обязательны".

### **2.5 Test_AddRepos_InvalidUrl**
- **Описание:** Проверяет ошибку при неверном формате `github_url`.
- **Метод:** POST `/add_repos`
- **Входные данные:**
  ```json
  {
      "github_url": "invalid_url",
      "id_resume": 1
  }
  ```
- **Ожидаемый результат:** Статус код 400 (Bad Request) и сообщение "Неверный формат URL".

### **2.6 Test_GetRepos_EmptyResult**
- **Описание:** Проверяет сценарий, когда у пользователя отсутствуют репозитории.
- **Метод:** GET `/repos`
- **Входные данные:**
  ```json
  {
      "github_url": "https://github.com/nonexistentuser"
  }
  ```
- **Ожидаемый результат:** Статус код 200 (OK) и пустой массив в ответе (`[]`).

### **2.7 Test_AddRepos_Unauthorized**
- **Описание:** Проверяет ошибку при добавлении репозиториев без авторизации.
- **Метод:** POST `/add_repos`
- **Входные данные:**
  ```json
  {
      "github_url": "https://github.com/testuser",
      "id_resume": 1
  }
  ```
- **Ожидаемый результат:** Статус код 401 (Unauthorized) и сообщение "Unauthorized".

---

## 3. **GenerateResumeApiTests**

### **3.1 Test_GenerateResume_Pattern1_Success**
- **Описание:** Проверяет успешную генерацию резюме по шаблону 1.
- **Метод:** GET `/pattern1/1`
- **Входные данные:**
  ```json
  {
      "login": "testuser",
      "password": "testpassword"
  }
  ```
- **Ожидаемый результат:** Статус код 200 (OK) и HTML-контент в ответе.

### **3.2 Test_GenerateResume_Pattern2_Success**
- **Описание:** Проверяет успешную генерацию резюме по шаблону 2.
- **Метод:** GET `/pattern2/1`
- **Входные данные:**
  ```json
  {
      "login": "testuser",
      "password": "testpassword"
  }
  ```
- **Ожидаемый результат:** Статус код 200 (OK) и HTML-контент в ответе.

### **3.3 Test_GenerateResume_Pattern3_Unauthorized**
- **Описание:** Проверяет ошибку при попытке генерации без авторизации.
- **Метод:** GET `/pattern3/1`
- **Входные данные:** Нет.
- **Ожидаемый результат:** Статус код 401 (Unauthorized) и сообщение "User not found".

### **3.4 Test_GenerateResumeImagePdf_Success**
- **Описание:** Проверяет успешную генерацию PDF-версии резюме.
- **Метод:** GET `/pattern_image_pdf/1/1`
- **Входные данные:** Нет.
- **Ожидаемый результат:** Статус код 200 (OK) и PDF-контент в ответе.

### **3.5 Test_GenerateResumeImagePdf_Failure**
- **Описание:** Проверяет ошибку при генерации PDF для несуществующего пользователя.
- **Метод:** GET `/pattern_image_pdf/999/1`
- **Входные данные:** Нет.
- **Ожидаемый результат:** Статус код 404 (Not Found) и сообщение "Failed to fetch PDF".
