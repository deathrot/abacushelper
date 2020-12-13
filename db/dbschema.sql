
SET FOREIGN_KEY_CHECKS = 0;

Drop table IF EXISTS abacus_levels;
Drop table IF EXISTS abacus_books;
Drop table if exists abacus_books_pages;
Drop table if exists abacus_books_pages_questions;
SET FOREIGN_KEY_CHECKS = 1;

Create table IF NOT EXISTS abacus_levels(
	Id NVARCHAR(256) not null primary key,
	RecordName VARCHAR(256) not null,
	RecordDescription VARCHAR(1024),
	SortOrder integer,
	ModifiedOn datetime not null
);

Create table IF NOT EXISTS abacus_books(
	Id NVARCHAR(256) not null primary key,
	AbacusLevelId NVARCHAR(256),
	RecordName VARCHAR(256) not null,
	RecordDescription VARCHAR(1024),
	SortOrder integer,
	ModifiedOn datetime not null,
	foreign key (AbacusLevelId) REFERENCES abacus_levels(Id) on delete cascade
);

Create table if not exists abacus_books_pages(
	Id NVARCHAR(256) not null primary key,
	AbacusBooksId NVARCHAR(256),
	RecordName VARCHAR(256) not null,
	RecordDescription VARCHAR(1024),
	SortOrder integer,
	ModifiedOn datetime not null,
	foreign key (AbacusBooksId) references abacus_books(Id) on delete cascade
);

Create table if not exists abacus_books_pages_questions(
	Id NVARCHAR(256) not null primary key,
	AbacusBooksPagesId NVARCHAR(256),
	Question JSON NOT NULL,
	SortOrder integer,
	ModifiedOn datetime not null,
	foreign key (AbacusBooksPagesId) references abacus_books_pages(Id) on delete cascade
);

Delete from abacus_levels;
s
INSERT INTO abacus_levels(Id, RecordName, RecordDescription, SortOrder, ModifiedOn)
VALUES ('ML3', 'Mega Level 3', 'Mega Level 3', 1, CURRENT_TIMESTAMP),
('ML4', 'Mega Level 4', 'Mega Level 4', 2, CURRENT_TIMESTAMP);

INSERT INTO abacus_books(Id, AbacusLevelId, RecordName, RecordDescription, SortOrder, ModifiedOn)
VALUES('ML3_A', 'ML3', 'Megal Level 3A', 'Megal Level 3A', 1, CURRENT_TIMESTAMP),
('ML3_B', 'ML3', 'Megal Level 3B', 'Megal Level 3B', 2, CURRENT_TIMESTAMP);

INSERT INTO abacus_books_pages(Id, AbacusBooksId, RecordName, RecordDescription, SortOrder, ModifiedOn)
VALUES('ML3_A_1', 'ML3_A', 'Page 1', 'Page 1', 1, CURRENT_TIMESTAMP);

INSERT INTO abacus_books_pages_questions(Id, AbacusBooksPagesId, Question, SortOrder, ModifiedOn)
VALUES('ML3_A_1_1', 'ML3_A_1', '{ "type": "test" }', 1, CURRENT_TIMESTAMP);