Create table IF NOT EXISTS settings(
	id VARCHAR(256) not null primary key,
	setting_name VARCHAR(256) not null,
	setting_data_type VARCHAR(256) not null,
	setting_value VARCHAR(256) not null,
	sort_order integer,
	modified_on datetime not null,
)

Create table IF NOT EXISTS questions(
	id VARCHAR(256) not null primary key,
	severity INTEGER NOT NULL, 
	level_id INTEGER NOT NULL, 
	sub_level_id INTEGER NOT NULL, 
	record_name VARCHAR(256),
	record_description VARCHAR(512),
	question JSON NOT NULL,
	sort_order integer,
	modified_on datetime not NULL
);

Create table IF NOT EXISTS question_tags(
	id VARCHAR(256) not null primary key,
	question_id VARCHAR(256) not null,
	record_name VARCHAR(256) not null,
	record_description VARCHAR(1024),
	sort_order integer,
	modified_on datetime not null,
	foreign key (question_id) references questions(id)
);