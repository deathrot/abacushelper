CREATE DATABASE IF NOT EXISTS STUDENTS;

USE STUDENTS;

DROP TABLE IF EXISTS students;
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS sessions;
DROP TABLE IF EXISTS session_archives;
DROP VIEW IF EXISTS sessions_view;

Create table IF NOT EXISTS students(
	id varchar(36) not null primary key,
	student_name VARCHAR(256) not null,
	student_display_name VARCHAR(16) not null,
	student_email VARCHAR(256) not null unique,
	starting_level_id double,
	starting_sub_level_id double,
	current_level_id double,
	current_sub_level_id double,
	last_login_on datetime,
    last_log_out datetime, 
    is_locked_out boolean not null,
	is_deleted boolean NOT NULL default(FALSE),
	modified_on datetime not null
);

Create table IF NOT EXISTS users(
	id varchar(36) not null primary key,
	user_email VARCHAR(256) not null unique,
	login_password blob,
   last_login_on datetime,
   last_log_out datetime, 
   is_locked_out boolean not null,
	is_confirmed boolean not null,
	num_of_failed_password_attempt integer not null,
	is_deleted boolean NOT NULL default(FALSE),
	modified_on datetime not null
);

Create table IF NOT EXISTS sessions(
	id varchar(36) not null primary key,
	user_id VARCHAR(36) not null,
	session_token  VARCHAR(512) not null,
	login_time datetime not null,
	last_activity_time datetime not null,
	login_method INTEGER NOT NULL,
	next_login_timeout datetime NOT NULL,
	additional_session_data VARCHAR(512),
	is_deleted boolean NOT NULL default(FALSE),
	modified_on datetime not null
);

Create Index IX_Sessions_SessionToken on sessions(session_token);

Create table IF NOT EXISTS session_archives(
	id varchar(36) NOT NULL, 
	user_id VARCHAR(36) not null,
	session_token  VARCHAR(512) not null,
	login_time datetime not null,
	last_activity_time datetime not null,
	login_method INTEGER NOT NULL,
	log_out_time datetime not null,
	is_deleted boolean NOT NULL default(FALSE),
	modified_on datetime not null
);

/*Create table if not exists student_activity_history(
    student_id varchar(36) not null,
    question_id varchar(36) not null,
    num_of_attempt bit not null,
    success boolean not null,
    delta double not null,
    time_taken_milliseconds integer not null,
	is_deleted boolean NOT NULL default(FALSE),
	modified_on datetime not null,
    foreign key (student_id) references students(id)
);*/

CREATE VIEW sessions_view
AS 
SELECT s.id, 
	s.user_id,
	s.session_token,
	s.login_time,
	s.last_activity_time,
	s.login_method,
	s.next_login_timeout,
	s.additional_session_data,
	s.is_deleted,
	s.modified_on,
	st.student_display_name,
	st.student_email
FROM sessions s 
INNER JOIN users u ON u.id = s.user_id 
INNER JOIN students st ON st.id = u.id;