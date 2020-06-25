create database planer;
use planer;

create table Users (
	Userid int primary key auto_increment not null,
    FirstName nvarchar(50) not null,
    LastName nvarchar(50) not null,
    Passvord nvarchar(50) not null,
    Email nvarchar(50),
    Phone nvarchar(13)
    );
    
    
    
    