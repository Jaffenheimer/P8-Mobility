START TRANSACTION;

CREATE TABLE IF NOT EXISTS Bus
(
    Id        VARCHAR(40) PRIMARY KEY,
    RouteId   VARCHAR(40) NOT NULL,
    Latitude  DECIMAL(10, 8),
    Longitude DECIMAL(11, 8),
    Action    ENUM ('Default', 'Accelerate', 'Decelerate','MaintainSpeed') DEFAULT 'Default',
    UpdatedAt TIMESTAMP   NOT NULL
);

COMMIT;