SET FOREIGN_KEY_CHECKS=0;
DROP TABLE IF EXISTS levels;
DROP TABLE IF EXISTS sub_levels;
DROP TABLE IF EXISTS tags;
DROP TABLE IF EXISTS questions;
SET FOREIGN_KEY_CHECKS=1;

Create table IF NOT EXISTS levels(
	id NVARCHAR(256) not null primary key,
	record_name VARCHAR(256) not null,
	record_description VARCHAR(1024),
	sort_order integer,
	modified_on datetime not null
);

Create table IF NOT EXISTS sub_levels(
	id NVARCHAR(256) not null primary key,
	record_name VARCHAR(256) not null,
	record_description VARCHAR(1024),
	sort_order integer,
	modified_on datetime not null
);

Create table IF NOT EXISTS tags(
	id NVARCHAR(256) not null primary key,
	record_name VARCHAR(256) not null,
	record_description VARCHAR(1024),
	sort_order integer,
	modified_on datetime not null
);

Create table IF NOT EXISTS questions(
	id NVARCHAR(256) not null primary key,
	severity INTEGER NOT NULL, 
	question JSON NOT NULL,
	level_id NVARCHAR(256),
	sub_level_id NVARCHAR(256),
	sort_order integer,
	modified_on datetime not NULL,
	foreign key (level_id) references levels(id),
	foreign key (sub_level_id) references sub_levels(id)
);

INSERT IGNORE INTO levels(id, record_name, record_description, sort_order, modified_on)
VALUES ('Lvl1', 'Level 1', 'Level 1', 1, CURRENT_TIMESTAMP),
('Lvl2', 'Level 2', 'Level 2', 2, CURRENT_TIMESTAMP),
('Lvl3', 'Level 3', 'Level 3', 3, CURRENT_TIMESTAMP),
('Lvl4', 'Level 4', 'Level 4', 4, CURRENT_TIMESTAMP),
('Lvl5', 'Level 5', 'Level 5', 5, CURRENT_TIMESTAMP),
('Lvl6', 'Level 6', 'Level 6', 6, CURRENT_TIMESTAMP),
('Lvl7', 'Level 7', 'Level 7', 7, CURRENT_TIMESTAMP),
('Lvl8', 'Level 8', 'Level 8', 8, CURRENT_TIMESTAMP),
('Lvl9', 'Level 9', 'Level 9', 9, CURRENT_TIMESTAMP),
('Lvl10', 'Level 10', 'Level 10', 10, CURRENT_TIMESTAMP);

INSERT IGNORE INTO sub_levels(id, record_name, record_description, sort_order, modified_on)
VALUES ('SubLvl1', 'Sub-Level 1', 'Sub-Level 1', 1, CURRENT_TIMESTAMP),
('SubLvl2', 'Sub-Level 2', 'Sub-Level 2', 2, CURRENT_TIMESTAMP),
('SubLvl3', 'Sub-Level 3', 'Sub-Level 3', 3, CURRENT_TIMESTAMP),
('SubLvl4', 'Sub-Level 4', 'Sub-Level 4', 4, CURRENT_TIMESTAMP),
('SubLvl5', 'Sub-Level 5', 'Sub-Level 5', 5, CURRENT_TIMESTAMP),
('SubLvl6', 'Sub-Level 6', 'Sub-Level 6', 6, CURRENT_TIMESTAMP),
('SubLvl7', 'Sub-Level 7', 'Sub-Level 7', 7, CURRENT_TIMESTAMP),
('SubLvl8', 'Sub-Level 8', 'Sub-Level 8', 8, CURRENT_TIMESTAMP),
('SubLvl9', 'Sub-Level 9', 'Sub-Level 9', 9, CURRENT_TIMESTAMP),
('SubLvl10', 'Sub-Level 10', 'Sub-Level 10', 10, CURRENT_TIMESTAMP);