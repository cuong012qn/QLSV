
create database QLHocSinh
go

use QLHocSinh
go

create table [UserRole]
(
	ID int identity(1,1) primary key,
	Name nvarchar(max) not null
)
go

create table [User]
(
	ID int identity(1,1) primary key,
	Name nvarchar(max) not null,
	UserName nvarchar(max) not null,
	Password nvarchar(max) not null,
	DateCreate datetime not null,
	DateUpdate datetime not null,

	--Foreign key
	IDRole int not null,
	foreign key (IDRole) references UserRole(ID)
)
go

create table ClassType
(
	ID int identity (1,1) primary key,
	Name nvarchar(max) not null
)
go

create table Class
(
	ID int identity primary key,
	Name nvarchar(max) not null,

	--Foreign key
	IDType int not null
	foreign key (IDType) references ClassType(ID)
)
go

create table [Student]
(
	ID nvarchar(15) primary key,
	Name nvarchar (max) not null,
	BirthDate date not null,
	Gender nvarchar(5) not null,
	Address nvarchar(max),
	Email nvarchar(max),
	Image image,
	
	DateCreate datetime not null,
	DateUpdate datetime not null,

	StartDate date not null,
	EndDate date not null,

	--Foreign key
	IDClass int not null,
	Foreign key (IDClass) references Class(ID)
)
go

create table Course
(
	ID int identity(1,1) primary key,
	Name nvarchar(max)
)
go

create table [Grade]
(
	ID int identity(1,1) primary key,
	Test float not null default 0,
	Midterm float not null default 0,
	Final float not null default 0,

	--Foreign key
	IDStudent nvarchar(15) not null,
	IDCourse int not null,
	Foreign key (IDCourse) references Course(ID),
	Foreign key (IDStudent) references Student(ID)
)
go


--Seed data
--UserRole
insert into UserRole (Name) values (N'Admin')
insert into UserRole (Name) values (N'Giáo viên')
insert into UserRole (Name) values (N'Học sinh')
go

--User
declare @date datetime
set @date = GETDATE()

insert into [User] (Name, IDRole, UserName, Password, DateCreate, DateUpdate) values (N'Admin',1,'Admin','123',@date, @date)
insert into [User] (Name, IDRole, UserName, Password, DateCreate, DateUpdate) values (N'Giáo viên',2,'gv01','123',@date, @date)
insert into [User] (Name, IDRole, UserName, Password, DateCreate, DateUpdate) values (N'Học sinh',2,'hs01','123',@date, @date)
go

--ClassType
insert into [ClassType] (Name) values (N'Khối 10')
insert into [ClassType] (Name) values (N'Khối 11')
insert into [ClassType] (Name) values (N'Khối 12')
go

--Class
insert into [Class] (IDType,Name) values (1,N'10A1')
insert into [Class] (IDType,Name) values (1,N'10A2')
insert into [Class] (IDType,Name) values (1,N'10A3')
insert into [Class] (IDType,Name) values (2,N'11A1')
insert into [Class] (IDType,Name) values (2,N'11A2')
insert into [Class] (IDType,Name) values (2,N'10A3')
insert into [Class] (IDType,Name) values (3,N'12A1')
insert into [Class] (IDType,Name) values (3,N'12A2')
insert into [Class] (IDType,Name) values (3,N'12A3')
go

--Course 
insert into [Course] (Name) values (N'Toán')
insert into [Course] (Name) values (N'Lý')
insert into [Course] (Name) values (N'Hóa')
insert into [Course] (Name) values (N'Sinh')
insert into [Course] (Name) values (N'Sử')
insert into [Course] (Name) values (N'Địa')
insert into [Course] (Name) values (N'Công dân')
insert into [Course] (Name) values (N'Tiếng anh')
go

--Student
declare @date datetime
set @date = GETDATE()

--10A1
insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1811544811', N'Kim Thúy Ái', '10/04/2000', N'Nữ', '', '', '', @date, @date, '2020', '2022', 1)

insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1811547380', N'Đinh Thị Tuyết Anh', '08/06/2000', N'Nữ', '', '', '', @date, @date, '2020', '2022', 1)

insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1811547164', N'Nguyễn Thị Trúc	Anh', '04/01/2001', N'Nữ', '', '', '', @date, @date, '2020', '2022', 1)

insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1811547412', N'Phạm Thị Minh Anh', CONVERT(datetime, '22/06/2000',103), N'Nữ', '', '', '', @date, @date, '2020', '2022', 1)

insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1800002524', N'Võ Thị Lan Anh', CONVERT(datetime, '17/09/2000',103), N'Nữ', '', '', '', @date, @date, '2020', '2022', 1)
go
--10A2
declare @date datetime
set @date = GETDATE()

insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1811545587', N'Hồ Nhựt	Băng', CONVERT(datetime, '07/09/2000',103), N'Nam', '', '', '', @date, @date, '2020', '2022', 2)
insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1800002068', N'Phạm Nguyễn Chi	Bảo', CONVERT(datetime, '03/04/2000',103), N'Nam', '', '', '', @date, @date, '2020', '2022', 2)
insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1811547430', N'Nguyễn Uyên	Bình', CONVERT(datetime, '30/10/2000',103), N'Nữ', '', '', '', @date, @date, '2020', '2022', 2)
insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1800001967', N'Nguyễn Minh	Châu', CONVERT(datetime, '19/06/2000',103), N'Nữ', '', '', '', @date, @date, '2020', '2022', 2)
insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1800003245', N'Nguyễn Song Bảo	Châu', CONVERT(datetime, '17/11/2000',103), N'Nữ', '', '', '', @date, @date, '2020', '2022', 2)
go

--10A3
declare @date datetime
set @date = GETDATE()

insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1800005746', N'Nguyễn Chiến Công', CONVERT(datetime, '13/01/2000',103), N'Nam', '', '', '', @date, @date, '2020', '2022', 3)
insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1800005142', N'Nguyễn Vũ Hoàng	Cường', CONVERT(datetime, '09/01/2000',103), N'Nam', '', '', '', @date, @date, '2020', '2022', 3)
insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1811546262', N'Nguyễn Thành Danh', CONVERT(datetime, '19/08/2000',103), N'Nam', '', '', '', @date, @date, '2020', '2022', 3)
insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1800002278', N'Hồ Thị Thu Đào', CONVERT(datetime, '25/06/2000',103), N'Nữ', '', '', '', @date, @date, '2020', '2022', 3)
insert into [Student] (ID,Name, BirthDate, Gender, Address, Email, Image, 
						DateCreate, DateUpdate, StartDate, EndDate, IDClass)
values ('1811549323', N'Nguyễn Minh	Đức', CONVERT(datetime, '22/07/1999',103), N'Nam', '', '', '', @date, @date, '2020', '2022', 3)
go

select * from Course
select * from Student where IDClass = 1
go
--Grade
--10A2
Insert into Grade (IDCourse, IDStudent, Test, Midterm, Final) values (1,'1800002524', 7.6, 8.0, 9.0)
Insert into Grade (IDCourse, IDStudent, Test, Midterm, Final) values (2,'1800002524', 2.1, 6.0, 7.0)
Insert into Grade (IDCourse, IDStudent, Test, Midterm, Final) values (3,'1800002524', 3.2, 5.6, 10.0)
Insert into Grade (IDCourse, IDStudent, Test, Midterm, Final) values (4,'1800002524', 7.6, 7.4, 5.4)
Insert into Grade (IDCourse, IDStudent, Test, Midterm, Final) values (5,'1800002524', 5.6, 8.2, 6.7)
Insert into Grade (IDCourse, IDStudent, Test, Midterm, Final) values (6,'1800002524', 4.0, 9.0, 7.6)
Insert into Grade (IDCourse, IDStudent, Test, Midterm, Final) values (7,'1800002524', 7.2, 8.4, 8.2)
Insert into Grade (IDCourse, IDStudent, Test, Midterm, Final) values (8,'1800002524', 7.6, 8.2, 7.0)
go

select * from Grade
go

select Student.ID, Student.Name, ClassType.Name, Class.Name from Student 
join Class on Class.ID = Student.IDClass
join ClassType on ClassType.ID = Class.IDType
go

select COUNT(Class.ID) as [Số lượng], Class.Name, ClassType.Name from Student 
join Class on Class.ID = Student.IDClass
join ClassType on ClassType.ID = Class.IDType
group by Class.ID, Class.Name, ClassType.Name
go

select COUNT(Student.ID), COUNT(Class.Name) from Student
join Class on Class.ID = Student.IDClass
go
select COUNT(Class.Name) from Class

select Student.ID, Student.Name, Student.Gender, Student.BirthDate, Student.Address, Student.Image, 
 Student.Email, Student.StartDate, Student.EndDate, Student.DateCreate, Student.DateUpdate , Class.Name, ClassType.Name from Student
join Class on Class.ID = Student.IDClass
join ClassType on ClassType.ID = Class.IDType

update Student
set Name = '', Gender = '', BirthDate = '', Address = '', Email = '', StartDate = '', EndDate = '', DateUpdate = '', IDClass =''
where Student.ID = N'1800001967'
go


update Grade 
set Test = 8 , Midterm = 8 , Final = 8 , AverageScore = 8  
where IDCourse =1 and IDStudent = '1800002524'

select * from Grade
