use PhonebookDB

create table dbo.person(
	id int identity,
	last_name varchar(100),
	first_name varchar(100),
	address varchar(100),
	birthday datetime,
	email varchar(100),
	primary key (id)
);


create table dbo.contact_number(
	id int identity,
	phonenumber varchar(100),
	person_id int foreign key references person(id)
);



