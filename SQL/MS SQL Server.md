```sql
-- 选择特定行，行跳转
SELECT
    (SELECT DISTINCT
            Salary
        FROM
            Employee
        ORDER BY Salary DESC
        OFFSET 1 ROWS
        FETCH NEXT 1 ROWS ONLY ) AS SecondHighestSalary
;
```
