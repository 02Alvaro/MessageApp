CREATE DATABASE MessageAppDB;

USE MessageAppDB;

CREATE TABLE
    Message (
        Id INT PRIMARY KEY AUTO_INCREMENT,
        Subject VARCHAR(255),
        Body TEXT,
        SentAt DATETIME
    );

CREATE TABLE
    RecipientsEmail (
        Id INT PRIMARY KEY AUTO_INCREMENT,
        EmailAddress VARCHAR(255),
        Name VARCHAR(255)
    );

CREATE TABLE
    EmailRecipients (
        MessageId INT,
        RecipientId INT,
        FOREIGN KEY (MessageId) REFERENCES Message (Id),
        FOREIGN KEY (RecipientId) REFERENCES RecipientsEmail (Id),
        PRIMARY KEY (MessageId, RecipientId)
    );