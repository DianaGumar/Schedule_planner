create database planner;
use planner;

-- drop database planner;

create table users (
	id int primary key auto_increment,
    name nvarchar(50) not null,
    password nvarchar(50) not null,
    email nvarchar(50),
    phone nvarchar(20),
    role int not null
    );

create table tasks (
	id int primary key auto_increment,
    name nvarchar(100) not null,
    description nvarchar(400),
    label nvarchar(100),
    priority int default 3,
    deadline datetime,
    time_volume int,
    progress int
);

create table schedules (
	id int primary key auto_increment not null,
    task_id int not null,
    schedule_date datetime not null,
    make int 
);

alter table schedule add foreign key (task_id)
	references tasks (id) on delete cascade;


insert into users (name, password, role, email)
value ("root", 1111, 1, "lantan.mp4@gmail.com")

