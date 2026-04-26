# 💊 Pharmacy Management System API
**Graduation Project - Backend Service**

This is a professional RESTful Web API built using **.NET Core 8**, designed to manage core pharmacy operations including inventory, sales, employees, and supplier relations.

---

## 🔗 Live Demo & Documentation
The API is fully deployed and accessible here:
👉 [**Live Swagger UI**](http://youssef5807-001-site1.ntempurl.com/)

---

## 🛠️ Key Features
The system architecture is based on a robust ERD and includes the following modules:

* **🔐 Authentication (JWT):** Secure identity management for Employees and Admins.
* **👥 Employee Management:** Full CRUD operations with Role-Based Access Control (RBAC).
* **📦 Medicine & Inventory:** Real-time tracking of stock levels, pricing, and expiration dates.
* **🤝 Supplier Management:** Centralized database for supplier contact and info.
* **🛒 Sales Orders:** Efficient handling of customer transactions.
* **🧾 Purchase Orders:** Managing stock replenishment and procurement from suppliers.
* **⚠️ Inventory Alerts:** Specialized endpoints for monitoring low-stock items.

---

## 🛡️ Security & Permissions
* **SSL Encryption:** All communications are secured via HTTPS.
* **RBAC (Role-Based Access Control):**
    * `Admin`: Full system access (Delete records, manage suppliers, financial reports).
    * `Pharmacist/User`: Limited access (View medicines, create customer orders).

---

## 🧪 How to Test (Swagger UI)
1.  Open the **[Host URL](http://youssef5807-001-site1.ntempurl.com/)**.
2.  Navigate to the `Auth` controller and use the **Login** endpoint.
3.  Copy the `token` from the JSON response.
4.  Click the **Authorize** button (top right of the page).
5.  In the value field, type `Bearer ` (with a space) and then paste your token.
    * *Example:* `Bearer eyJhbGciOiJIUzI1...`
6.  You can now test all protected endpoints.

---

## 📊 Database Configuration
* **Engine:** Microsoft SQL Server (MSSQL).
* **Environment Switching:** The system intelligently switches between:
    * `Development`: LocalDB for offline coding.
    * `Production`: Cloud-based SQL Server for the live site.

---
