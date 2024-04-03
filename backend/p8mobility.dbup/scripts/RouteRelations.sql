﻿START TRANSACTION;

CREATE TABLE IF NOT EXISTS RouteRelations
(
    RouteId   VARCHAR(40) NOT NULL,
    BusStopId VARCHAR(40) NOT NULL,
    OrderNum  INT         NOT NULL,
    PRIMARY KEY (RouteId, BusStopId)
);

COMMIT;