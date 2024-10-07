--TASK 1
--1.. Create the database named "SISDB

create database SISDB

--2..Define the schema for the Students, Courses, Enrollments, Teacher, and Payments tables based 
--on the provided schema.
select *from Student
--TABLE Students
create table Student(
student_id int identity(1,1) not null primary key,
first_name varchar(20) ,
last_name varchar(20),
date_of_birth date,
email varchar(20) unique,
phone_number varchar(15))

--TABLE Courses
create table Course(
course_id int identity(101,1) not null primary key,
course_name varchar(20),
course_code varchar(20),
instructor_name varchar(20),
)

select*from Course

--TABLE Teacher
create table Teacher(
teacher_id int identity(1,1) not null primary key,
first_name varchar(20),
last_name varchar(20),
email varchar(20) unique)

--TABLE Enrollments
 create table Enrollment(
 enrollment_id int identity(201,1) not null primary key,
 student_id int ,
 constraint FK_enrollment_student foreign key(student_id) references Student(student_id) on delete cascade,
 course_id int,
 constraint FK_enrollment_course foreign key(course_id) references Course(course_id) on delete cascade,
 enrollment_date date)


 --TABLE Payments
 create table Payment(
 payment_id int identity(301,1) not null primary key,
 student_id int,
 constraint FK_payment_student foreign key(student_id) references Student(student_id) on delete cascade,
 amount int ,
 payment_date date)

 --5..Insert at least 10 sample records into each of the following tables.
 insert into Student(first_name, last_name, date_of_birth, email, phone_number)
 values
  ('Amit', 'Sharma', '2000-01-15', 'amit.sharma@example.com', '9876543210'),
    ('Priya', 'Singh', '1999-05-22', 'priya.singh@example.com', '9123456789'),
    ('Rahul', 'Patel', '2001-07-10', 'rahul.patel@example.com', '9988776655'),
    ( 'Sneha', 'Reddy', '1998-09-30', 'sneha.reddy@example.com', '9876123450'),
    ( 'Vikram', 'Kumar', '2000-11-25', 'vikram.kumar@example.com', '9112345678')
   

	alter table Teacher
	alter column email varchar(30)

	select * from Students

insert into Teacher (first_name, last_name, email)
values
    ('Amit', 'Sharma', 'amit.sharma@example.com'),
    ( 'Priya', 'Singh', 'priya.singh@example.com'),
    ( 'Rahul', 'Patel', 'rahul.patel@example.com'),
    ( 'Sneha', 'Reddy', 'sneha.reddy@example.com'),
    ('Vikram', 'Kumar', 'vikram.kumar@example.com')

select * from Payment
select *from course
select *from Enrollment

INSERT INTO Course (course_name, course_code, instructor_name)
VALUES 
('Data Structures', 'CS101', 'Rajesh Kumar'),
('Operating Systems', 'CS102', 'Priya Sharma'),
('Database Systems', 'CS103', 'Anil Mehta'),
('Computer Networks', 'CS104', 'Sunita Patel'),
('AI', 'CS105', 'Manoj Desai')


  

INSERT INTO Enrollment (student_id, course_id, enrollment_date)
VALUES 
(6, 106, '2024-09-01'),
(2, 107, '2024-09-05'),
(3, 108, '2024-09-10'),
(4, 109, '2024-09-12'),
(5, 110, '2024-09-15');
select * from Enrollment

insert into Payment (student_id, amount, payment_date)
values
    ( 6, 5000, '2024-01-15'),
    ( 2, 4500, '2024-02-20'),
    ( 3, 5500, '2024-03-10'),
    (4, 6000, '2024-04-25'),
    ( 5, 5200, '2024-05-30')


	select * from Course

--TASK 2

/*1.Write an SQL query to insert a new student into the "Students" table with the following details:
a. First Name: John
© Hexaware Technologies Limited. All rights www.hexaware.com
b. Last Name: Doe
c. Date of Birth: 1995-08-15
d. Email: john.doe@example.com
e. Phone Number: 1234567890*/

insert into Students values('11','John','Doe','1995-08-15','john.doe@example.com','1234567890')

/*2..Write an SQL query to enroll a student in a course. Choose an existing student and course and 
insert a record into the "Enrollments" table with the enrollment date.*/

insert into Enrollments (enrollment_id, student_id, course_id, enrollment_date)
values(1011, 3, 103, '2024-09-15')

/*3..Update the email address of a specific teacher in the "Teacher" table. Choose any teacher and 
modify their email address.*/

Update Teacher set email='rahul.patel@newemail.com' where teacher_id=203


/*4..Write an SQL query to delete a specific enrollment record from the "Enrollments" table. Select 
an enrollment record based on the student and course.*/

delete from Enrollments
where student_id=6 and course_id=106

/*5..Update the "Courses" table to assign a specific teacher to a course. Choose any course and 
teacher from the respective tables.*/

UPDATE Courses
SET teacher_id = 205
WHERE course_id = 103

/*6..Delete a specific student from the "Students" table and remove all their enrollment records 
from the "Enrollments" table. Be sure to maintain referential integrity.*/alter table Enrollments add constraint FK_enrollments_studentforeign key (student_id)references Students(student_id)on delete cascadealter table Payments add constraint FK_payments_studentforeign key (student_id)references Students(student_id)on delete cascadedelete from Enrollmentswhere student_id=8delete from Studentswhere student_id=10/*--7.Update the payment amount for a specific payment record in the "Payments" table. Choose any 
payment record and modify the payment amount*/update Paymentsset amount=7500where payment_id=2007--TASK 3/*1..Write an SQL query to calculate the total payments made by a specific student. You will need to 
join the "Payments" table with the "Students" table based on the student's ID*/select s.student_id,s.first_name,s.last_name,sum(p.amount) as [Total Payments]from Students sjoin Payments pon s.student_id=p.student_idwhere s.student_id=6group by s.student_id,s.first_name,s.last_name/*2..Write an SQL query to retrieve a list of courses along with the count of students enrolled in each 
course. Use a JOIN operation between the "Courses" table and the "Enrollments" table.*/select c.course_id,c.course_name,count(e.student_id) as [Student Count]from courses cjoin enrollments eon c.course_id=e.course_idgroup by c.course_id,c.course_name/*3..Write an SQL query to find the names of students who have not enrolled in any course. Use a 
LEFT JOIN between the "Students" table and the "Enrollments" table to identify students 
without enrollments.*/select s.student_id,s.first_name,s.last_namefrom students sleft join enrollments eon s.student_id=e.student_idwhere e.student_id is null--ORselect s.student_id,s.first_name,s.last_namefrom students sleft join enrollments eon s.student_id=e.student_idgroup by s.student_id,s.first_name,s.last_namehaving count(e.student_id)=0/*4..Write an SQL query to retrieve the first name, last name of students, and the names of the 
courses they are enrolled in. Use JOIN operations between the "Students" table and the 
"Enrollments" and "Courses" tables.*/select s.first_name,s.last_name,c.course_namefrom Students sjoin enrollments eon s.student_id=e.student_idjoin Courses con e.course_id=c.course_id/*5..Create a query to list the names of teachers and the courses they are assigned to. Join the 
"Teacher" table with the "Courses" table.*/select t.teacher_id,t.first_name,t.last_name,c.course_namefrom Teacher tjoin Courses con t.teacher_id=c.teacher_id/*6..Retrieve a list of students and their enrollment dates for a specific course. You'll need to join the 
"Students" table with the "Enrollments" and "Courses" tables.*/select*from Enrollmentsselect s.student_id,s.first_name,s.last_name,c.course_name,e.enrollment_datefrom Students sjoin Enrollments eon s.student_id=e.student_idjoin Courses con  e.course_id=c.course_idwhere c.course_name='English'/*7..Find the names of students who have not made any payments. Use a LEFT JOIN between the 
"Students" table and the "Payments" table and filter for students with NULL payment records*/select s.first_name,s.last_namefrom Students sleft join payments pon s.student_id=p.student_idwhere p.payment_id is null/*8..Write a query to identify courses that have no enrollments. You'll need to use a LEFT JOIN 
between the "Courses" table and the "Enrollments" table and filter for courses with NULL 
enrollment records*/select c.course_id,c.course_namefrom Courses cleft join Enrollments eon c.course_id=e.course_idwhere e.course_id is null/*9..Identify students who are enrolled in more than one course. Use a self-join on the "Enrollments" 
table to find students with multiple enrollment records.*/select e1.student_id, count(e1.course_id) as course_count
from Enrollments e1
join Enrollments e2 
on e1.student_id = e2.student_id
where e1.course_id <> e2.course_id
group by e1.student_id
having count(e1.course_id) > 1
/*10..Find teachers who are not assigned to any courses. Use a LEFT JOIN between the "Teacher" 
table and the "Courses" table and filter for teachers with NULL course assignments.*/
select t.teacher_id,t.first_name,t.last_name
from Teacher t
left join Courses c
on t.teacher_id=c.teacher_id
where c.course_id is null


--TASK 4

/*1.. Write an SQL query to calculate the average number of students enrolled in each course. Use 
aggregate functions and subqueries to achieve this.*/

select avg(student_count) as avg_students_per_course
from (
select course_id, count(student_id) as student_count
from Enrollments
group by course_id
) as course_enrolls

/*2..Identify the student(s) who made the highest payment. Use a subquery to find the maximum 
payment amount and then retrieve the student(s) associated with that amount.*/


select student_id,first_name,last_name
from students 
where student_id in
(
select student_id
from Payments
where amount=(select max(amount) from Payments)
) 

/*3..Retrieve a list of courses with the highest number of enrollments. Use subqueries to find the 
course(s) with the maximum enrollment count.*/


select course_id, course_name
from courses
where course_id in (
    select course_id
    from enrollments
    group by course_id
    having count(student_id) = (
        select max(course_count)
        from (
            select count(student_id) as course_count
            from enrollments
            group by course_id
        ) as subquery
    )
)


/*4..Calculate the total payments made to courses taught by each teacher. Use subqueries to sum 
payments for each teacher's courses.*/


select t.teacher_id,t.first_name,t.last_name,
(
select sum(amount)
from Payments p
where p.student_id in(
  select e.student_id 
  from Enrollments e
  where e.course_id in(
    select c.course_id 
    from courses c
    where c.teacher_id=t.teacher_id
  )
))as total_payments
from Teacher t


/*5..Identify students who are enrolled in all available courses. Use subqueries to compare a 
student's enrollments with the total number of courses.*/



select s.student_id, s.first_name, s.last_name
from Students s
where (
    select count(*)
    from Enrollments e
    where e.student_id = s.student_id
) = (
    select count(*)
    from Courses
    )


/*6..Retrieve the names of teachers who have not been assigned to any courses. Use subqueries to 
find teachers with no course assignments.*/

select t.teacher_id, t.first_name, t.last_name
from teacher t
where t.teacher_id not in (
    select c.teacher_id
    from courses c
)


/*7.. Calculate the average age of all students. Use subqueries to calculate the age of each student 
based on their date of birth.*/

select avg(student_age) as average_age
from (
    select datediff(year, date_of_birth, getdate()) as student_age
    from students
) as age_subquery


/*8.. Identify courses with no enrollments. Use subqueries to find courses without enrollment 
records.*/

select course_id, course_name
from courses
where course_id not in 
(
    select distinct course_id
    from enrollments
)


/*9..Calculate the total payments made by each student for each course they are enrolled in. Use 
subqueries and aggregate functions to sum payments.*/

select student_id, course_id, 
    (select sum(amount) 
     from payments p 
     where p.student_id = e.student_id
	 ) as total_payments
from enrollments e

/*10.. Identify students who have made more than one payment. Use subqueries and aggregate 
functions to count payments per student and filter for those with counts greater than one.*/

select distinct student_id
from payments
where student_id in (
  select student_id
  from payments
  group by student_id
  having count(payment_id) > 1
)

/*11. Write an SQL query to calculate the total payments made by each student. Join the "Students" 
table with the "Payments" table and use GROUP BY to calculate the sum of payments for each 
student.*/


select s.student_id,s.first_name,s.last_name,sum(p.amount) as total_payment
from Students s
join Payments p
on s.student_id=p.student_id
group by p.student_id,s.student_id,s.first_name,s.last_name

/*12..Retrieve a list of course names along with the count of students enrolled in each course. Use 
JOIN operations between the "Courses" table and the "Enrollments" table and GROUP BY to 
count enrollments.*/

select c.course_name,count(e.student_id) as student_count
from Courses c
join Enrollments e
on c.course_id=e.course_id
group by c.course_name

/*13..Calculate the average payment amount made by students. Use JOIN operations between the 
"Students" table and the "Payments" table and GROUP BY to calculate the average.*/

select s.student_id, s.first_name, s.last_name, avg(p.amount) as average_payment
from students s
join payments p
on s.student_id = p.student_id
group by s.student_id, s.first_name, s.last_name



