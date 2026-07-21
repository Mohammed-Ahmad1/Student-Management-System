<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Student Management System API</title>

<style>
body{
    font-family: Arial, Helvetica, sans-serif;
    max-width:1100px;
    margin:auto;
    padding:40px;
    line-height:1.7;
    background:#f5f5f5;
    color:#333;
}

.container{
    background:#fff;
    padding:40px;
    border-radius:10px;
    box-shadow:0 0 10px rgba(0,0,0,.1);
}

h1,h2,h3{
    color:#0d6efd;
}

table{
    width:100%;
    border-collapse:collapse;
    margin:20px 0;
}

table th, table td{
    border:1px solid #ddd;
    padding:12px;
    text-align:left;
}

table th{
    background:#0d6efd;
    color:white;
}

code{
    background:#efefef;
    padding:3px 6px;
    border-radius:4px;
}

pre{
    background:#272822;
    color:white;
    padding:20px;
    overflow:auto;
    border-radius:6px;
}

hr{
    margin:40px 0;
}

ul{
    margin-top:5px;
}
</style>

</head>

<body>

<div class="container">

<h1>Student Management System API</h1>

<h2>Overview</h2>

<p>
Student Management System API is a RESTful Web API built using
<strong>ASP.NET Core</strong>. It provides a complete backend for managing
students, courses, instructors, departments, and enrollments.
The project follows a layered architecture that separates presentation,
business logic, and data access, making the application easy to maintain
and extend.
</p>

<hr>

<h2>Features</h2>

<h3>Student Management</h3>

<ul>
<li>Create new students</li>
<li>Update student information</li>
<li>Delete students</li>
<li>Retrieve all students</li>
<li>Retrieve student by ID</li>
<li>Get active students</li>
<li>Get inactive students</li>
<li>Get passed students</li>
<li>Calculate average grades</li>
<li>Calculate GPA by semester</li>
<li>Retrieve students enrolled in a course</li>
</ul>

<h3>Course Management</h3>

<ul>
<li>Create courses</li>
<li>Update courses</li>
<li>Delete courses</li>
<li>Retrieve all courses</li>
<li>Retrieve course by ID</li>
<li>Filter courses by credit hours</li>
</ul>

<h3>Department Management</h3>

<ul>
<li>Create departments</li>
<li>Update departments</li>
<li>Delete departments</li>
<li>Retrieve departments</li>
<li>Retrieve departments assigned to a course</li>
</ul>

<h3>Instructor Management</h3>

<ul>
<li>Create instructors</li>
<li>Update instructors</li>
<li>Delete instructors</li>
<li>Retrieve instructor details</li>
<li>Retrieve instructor courses</li>
</ul>

<h3>Enrollment Management</h3>

<ul>
<li>Retrieve student enrollments</li>
<li>Calculate completed credits</li>
<li>Calculate cumulative GPA</li>
<li>Generate student transcript</li>
<li>Enrollment reports</li>
</ul>

<hr>

<h2>Technologies Used</h2>

<ul>
<li>ASP.NET Core Web API</li>
<li>C#</li>
<li>Entity Framework Core</li>
<li>SQL Server</li>
<li>LINQ</li>
<li>Dependency Injection</li>
<li>RESTful API</li>
</ul>
