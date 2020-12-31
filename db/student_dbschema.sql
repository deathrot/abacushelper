Create table IF NOT EXISTS students(
	id VARCHAR(36) not null primary key,
	student_first_name VARCHAR(256) not null,
	student_last_name VARCHAR(256) not null,
	student_email VARCHAR(256) not null unique,
	starting_level_id double,
	starting_sub_level_id double,
	current_level_id double,
	current_sub_level_id double,
	login_password blob,
    login_secret blob,
    last_login_on datetime,
    last_log_out datetime, 
    is_locked_out boolean not null,
    deleted boolean not null,
	modified_on datetime not null
);

Create table IF NOT EXISTS users(
	id VARCHAR(36) not null primary key,
	user_email VARCHAR(256) not null,
	login_password blob,
    login_secret blob,
    last_login_on datetime,
    last_log_out datetime, 
    is_locked_out boolean not null,
	is_confirmed boolean not null,
	num_of_failed_password_attempt integer not null,
    deleted boolean not null,
	modified_on datetime not null
);

Create table IF NOT EXISTS sessions(
	id VARCHAR(36) not null primary key,
	userid VARCHAR(36) not null,
	login_time datetime not null,
	last_activity_time datetime not null,
	modified_on datetime not null
);

Create table IF NOT EXISTS session_archives(
	id VARCHAR(36) not null primary key,
	userid VARCHAR(36) not null,
	login_time datetime not null,
	last_activity_time datetime not null,
	log_out_time datetime not null,
	modified_on datetime not null
);

/*Create table if not exists student_activity_history(
    student_id VARCHAR(256) not null,
    question_id VARCHAR(256) not null,
    num_of_attempt bit not null,
    success boolean not null,
    delta double not null,
    time_taken_milliseconds integer not null,
	modified_on datetime not null,
    foreign key (student_id) references students(id)
);*/