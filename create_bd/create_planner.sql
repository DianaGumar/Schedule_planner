create database planner;
use planner;

-- drop database planner;

create table users (
	id int primary key auto_increment not null,
    name nvarchar(50) not null,
    password nvarchar(50) not null,
    email nvarchar(50),
    phone nvarchar(13),
    role int not null
    );
    
create table tasks_areas (
	id int primary key auto_increment not null,
    name nvarchar(50) not null
);

create table tasks_types (
	id int primary key auto_increment not null,
    name nvarchar(50) not null
);

create table tasks (
	id int primary key auto_increment not null,
    name nvarchar(100) not null,
    description nvarchar(200),
    priority int not null,
    area_id int,
    type_id int
);

create table schedule (
	id int primary key auto_increment not null,
    task_id int not null,
    schedule_date datetime not null,
    checked int 
);

alter table schedule add foreign key (task_id)
	references tasks (id) on delete cascade;
    
    
alter table tasks add foreign key (area_id)
	references tasks_areas (id);
    
alter table tasks add foreign key (type_id)
	references tasks_types (id);



insert into tasks_types (name)
values
("task"),
("habit");


insert into tasks_areas (name)
values
("work"),
("self-improvement"),
("home"),
("university");


insert into users (name, password, role, email)
value ("admin", 1111, 1, "lantan.mp4@gmail.com")

