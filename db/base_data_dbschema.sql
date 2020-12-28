DROP TABLE IF EXISTS settings;
DROP TABLE IF EXISTS question_tags;
DROP TABLE IF EXISTS tags;
DROP TABLE IF EXISTS questions;

Create table IF NOT EXISTS settings(
	id VARCHAR(256) not null primary key,
	setting_name VARCHAR(256) not null,
	setting_data_type VARCHAR(256) not null,
	setting_value VARCHAR(256) not null,
	sort_order integer,
	is_deleted boolean NOT NULL default(FALSE),
	modified_on datetime not null
);

Create table IF NOT EXISTS questions(
	id VARCHAR(256) not null primary key,
	severity INTEGER NOT NULL, 
	level INTEGER NOT NULL, 
	sub_level INTEGER NOT NULL, 
	question_type INTEGER NOT NULL,
	question_sub_type INTEGER NOT NULL,
	record_name VARCHAR(256),
	record_description VARCHAR(512),
	question JSON NOT NULL,
	sort_order integer,
	is_deleted boolean NOT NULL default(FALSE),
	modified_on datetime not NULL
);

Create table IF NOT EXISTS tags(
	id VARCHAR(256) not null primary key,	
	tag_name VARCHAR(256),
	is_deleted boolean NOT NULL default(FALSE),
	modified_on datetime not NULL
);

Create table IF NOT EXISTS question_tags(
	id VARCHAR(256) not null primary key,
	question_id VARCHAR(256) not null,
	tag_id VARCHAR(256) not null,
	is_deleted boolean NOT NULL default(FALSE),
	modified_on datetime not null,
	foreign key (question_id) references questions(id),
	foreign key (tag_id) references tags(id)
);
