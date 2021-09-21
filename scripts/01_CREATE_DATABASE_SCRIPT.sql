CREATE DATABASE IF NOT EXISTS ZeDeliveryBackendChallenge;
USE ZeDeliveryBackendChallenge;
CREATE TABLE `partner` (
  `Id` varchar(255) PRIMARY KEY,
  `TradingName` varchar(255),
  `OwnerName` varchar(255),
  `Document` varchar(18) UNIQUE,
  `CoverageAreaType` varchar(255),
  `CoverageAreaCoordinates` POLYGON,
  `AddressType` varchar(255),
  `AddressCoordinates` POINT
);
