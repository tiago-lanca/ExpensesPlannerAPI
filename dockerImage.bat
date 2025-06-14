docker build -t expensesplannerapi .
docker run -d -p 8080:8080 -p 8081:8081 expensesplannerapi