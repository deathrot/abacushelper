
Create table IF NOT EXISTS abacus_levels(
	Id NVARCHAR(256) not null primary key,
	RecordName VARCHAR(256) not null,
	RecordDescription VARCHAR(1024),
	SortOrder integer,
	ModifiedOn datetime not null
);

Create table IF NOT EXISTS abacus_sub_levels(
	Id NVARCHAR(256) not null primary key,
	RecordName VARCHAR(256) not null,
	RecordDescription VARCHAR(1024),
	SortOrder integer,
	ModifiedOn datetime not null
);

Create table if not exists abacus_questions(
	Id NVARCHAR(256) not null primary key,
	Severity INTEGER NOT NULL, 
	Question JSON NOT NULL,
	AbacusLevelId NVARCHAR(256),
	SortOrder integer,
	ModifiedOn datetime not NULL,
	foreign key (AbacusLevelId) references abacus_levels(Id) on delete CASCADE
);


INSERT IGNORE INTO abacus_levels(Id, RecordName, RecordDescription, SortOrder, ModifiedOn)
VALUES ('Lvl1', 'Spark 1', 'Spark 1', 1, CURRENT_TIMESTAMP),
('Lvl2', 'Spark 2', 'Spark 2', 2, CURRENT_TIMESTAMP),
('Lvl3', 'Spark 3', 'Spark 3', 3, CURRENT_TIMESTAMP),
('Lvl4', 'Spark 4', 'Spark 4', 4, CURRENT_TIMESTAMP),
('Lvl5', 'Spark 5', 'Spark 5', 5, CURRENT_TIMESTAMP),
('ML1', 'Mega Level 1', 'Mega Level 1', 6, CURRENT_TIMESTAMP),
('ML2', 'Mega Level 2', 'Mega Level 2', 7, CURRENT_TIMESTAMP),
 ('ML3', 'Mega Level 3', 'Mega Level 3', 8, CURRENT_TIMESTAMP),
('ML4', 'Mega Level 4', 'Mega Level 4', 9, CURRENT_TIMESTAMP),
('ML5', 'Mega Level 5', 'Mega Level 5', 10, CURRENT_TIMESTAMP);

INSERT IGNORE INTO abacus_sub_levels(Id, RecordName, RecordDescription, SortOrder, ModifiedOn)
VALUES ('SubLvl1', 'Level 1', 'Sub Level 1', 1, CURRENT_TIMESTAMP),
('SubLvl2', 'Sub Level 2', 'Sub Level 2', 2, CURRENT_TIMESTAMP),
('SubLvl3', 'Sub Level 3', 'Sub Level 3', 3, CURRENT_TIMESTAMP),
('SubLvl4', 'Sub Level 4', 'Sub Level 4', 4, CURRENT_TIMESTAMP),
('SubLvl5', 'Sub Level 5', 'Sub Level 5', 5, CURRENT_TIMESTAMP),
('SubLvl6', 'Sub Level 6', 'Sub Level 6', 6, CURRENT_TIMESTAMP),
('SubLvl7', 'Sub Level 7', 'Sub Level 7', 7, CURRENT_TIMESTAMP),
('SubLvl8', 'Sub Level 8', 'Sub Level 8', 8, CURRENT_TIMESTAMP),
('SubLvl9', 'Sub Level 9', 'Sub Level 9', 9, CURRENT_TIMESTAMP),
('SubLvl10', 'Sub Level 10', 'Sub Level 10', 10, CURRENT_TIMESTAMP);