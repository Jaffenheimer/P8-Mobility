﻿START TRANSACTION;

CREATE TABLE IF NOT EXISTS Route
(
    Id        VARCHAR(40) PRIMARY KEY,
    Password  VARCHAR(40) NOT NULL,
    UpdatedAt TIMESTAMP    NOT NULL
);

COMMIT;