CREATE DATABASE NewTeach;

CREATE TABLE users (user_id INTEGER PRIMARY KEY AUTO_INCREMENT, user_name VARCHAR(30) NOT NULL, user_password VARCHAR(20) NOT NULL, type TINYINT);  -1 Student -2 Teacher

CREATE TABLE users_information (user_id INTEGER, user_sex TINYINT, user_birthday DATE, user_phone INTEGER, user_introduction VARCHER(200), FOREIGN KEY(user_id) REFERENCES users(user_id));
CREATE TABLE over_messages(sender_id INTEGER, receiver_id INTEGER, time DATETIME, message VARCHAR(1438));

CREATE TABLE following_list(user_id INTEGER, follow_id INTEGER, FOREIGN KEY(user_id) REFERENCES users(user_id), FOREIGN KEY(follow_id) REFERENCES users(user_id));
CREATE TABLE black_list(user_id INTEGER, black_id INTEGER, FOREIGN KEY(user_id) REFERENCES users(user_id), FOREIGN KEY(black_id) REFERENCES users(user_id));
CREATE TABLE user_images(user_id INTEGER, file_name VARCHAR(255))

;
CREATE TABLE files(user_id INTEGER, file_type TINYINT, file_name VARCHAR(255), file_key CHAR(16), file_length INTEGER);    --1 Private -2 Public
CREATE TABLE students(state TINYINT, teacher_id INTEGER, student_id INTEGER);    --1 Allowed -2 Not Allowed -3被拒绝
CREATE TABLE classes(teacher_id INTEGER, subject TINYINT, start_time DATETIME, end_time DATETIME, class_id INTEGER, class_state TINYINT, student_count INTEGER, class_intruduction VARCHER(200), FOREIGN KEY(teacher_id) REFERENCES users(user_id));   -- 1-未开始 2-正在授课 3-授课已结束
CREATE TABLE deal_list(class_id INTEGER, student_id INTEGER, deal_state TINYINT, deal_price INTEGER, review VARCHER(500), FOREIGN KEY(class_id) REFERENCES classes(class_id));
CREATE TABLE file_dynamic(user_id INTEGER, file_name VARCHER(255), FOREIGN KEY(file_name) REFERENCES files(file_name));
CREATE TABLE class_dynamic(student_id INTEGER, class_id INTEGER, FOREIGN KEY(class_id) REFERENCES classes(class_id));

--INSERT INTO users(user_name, user_password) VALUES ('大明', 123456);

--INSERT INTO users(user_name, user_password) VALUES ('小明', 123456);

--INSERT INTO users_information VALUES (1, '男', 946659661, 10086);

--INSERT INTO users_information VALUES (2, '男', 946659662, 10010);


--SELECT * FROM users INNER JOIN users_information ON users.user_id = users_information.user_id;

--on user.user_id like 'w%';,,,~~



--插入用户
--INSERT INTO users(user_name, user_password) VALUES (?, ?);


--搜索最大值
--SELECT LAST_INSERT_ID();


--插入用户资料
--INSERT INTO users_information VALUES (?, ?, ?, ?);


--修改全部信息
--UPDATE users_information SET user_sex = ?, user_birthday = ?, user_phone = ? WHERE user_id = ?;


--修改密码
--UPDATE users SET user_password = ？ WHERE user_id = ?;


--查找密码
--SELECT user_password FROM users WHERE user_id = ?;


--查找全部信息
--SELECT * FROM users_information WHERE user_id = ?;


--查找用户名
--SELECT user_name FROM users WHERE user_id = ?;