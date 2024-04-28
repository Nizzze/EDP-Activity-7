CREATE DATABASE  IF NOT EXISTS `school_supplydb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `school_supplydb`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: school_supplydb
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `CategoryID` int NOT NULL,
  `CategoryName` varchar(50) NOT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'Pens'),(2,'Notebooks'),(3,'Pencils'),(4,'Erasers'),(5,'Markers'),(6,'Binders'),(7,'Rulers'),(8,'Backpacks'),(9,'Scissors'),(10,'Glue');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `highvalueorders`
--

DROP TABLE IF EXISTS `highvalueorders`;
/*!50001 DROP VIEW IF EXISTS `highvalueorders`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `highvalueorders` AS SELECT 
 1 AS `OrderID`,
 1 AS `OrderDate`,
 1 AS `CustomerName`,
 1 AS `TotalAmount`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `orderitems`
--

DROP TABLE IF EXISTS `orderitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderitems` (
  `OrderItemID` int NOT NULL,
  `OrderID` int DEFAULT NULL,
  `ProductID` int DEFAULT NULL,
  `Quantity` int DEFAULT NULL,
  `Total` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`OrderItemID`),
  KEY `fk_order` (`OrderID`),
  KEY `fk_product` (`ProductID`),
  CONSTRAINT `fk_order` FOREIGN KEY (`OrderID`) REFERENCES `orders` (`OrderID`),
  CONSTRAINT `fk_product` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderitems`
--

LOCK TABLES `orderitems` WRITE;
/*!40000 ALTER TABLE `orderitems` DISABLE KEYS */;
INSERT INTO `orderitems` VALUES (1,1,1,10,150.00),(2,1,2,10,150.00),(3,2,3,5,450.00),(4,2,4,1,399.99),(5,3,5,3,150.00),(6,3,6,5,2250.00),(7,4,7,4,400.00),(8,5,8,4,700.00),(9,6,9,2,1999.98),(10,7,10,6,360.00),(11,9,2,5,75.00),(12,11,3,1,90.00),(13,11,4,7,2799.93),(14,12,5,1,50.00),(15,12,6,5,2250.00),(16,13,7,9,900.00),(17,13,8,2,350.00),(18,14,9,10,9999.90),(19,14,10,6,360.00),(20,15,11,8,100.00),(21,15,12,1,149.75),(22,16,13,2,139.50),(23,16,14,4,158.00),(24,17,15,6,298.50),(25,17,16,5,98.75),(26,18,17,9,60.75),(27,18,18,7,8405.25),(28,19,19,7,87.50),(29,19,20,4,23.00),(30,20,21,9,265.50),(31,20,22,1,99.50),(32,21,23,7,418.25),(33,21,24,3,43.50),(34,22,25,5,63.75),(35,22,26,3,25.50),(36,23,27,10,15000.00),(37,23,28,1,8.75),(38,24,29,5,47.50),(39,24,30,9,177.75),(40,30,1,2,30.00),(41,30,2,3,45.00);
/*!40000 ALTER TABLE `orderitems` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `before_quantity_insert` BEFORE INSERT ON `orderitems` FOR EACH ROW SET NEW.Total = (NEW.Quantity * (SELECT Price FROM Products WHERE Products.ProductID = NEW.ProductID)) */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `update_orders_total` AFTER INSERT ON `orderitems` FOR EACH ROW BEGIN
    UPDATE orders
    SET TotalAmount = (
        SELECT SUM(Total)
        FROM orderitems
        WHERE OrderID = NEW.OrderID
    )
    WHERE OrderID = NEW.OrderID;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `before_quantity_update` BEFORE UPDATE ON `orderitems` FOR EACH ROW SET NEW.Total = NEW.Quantity * (SELECT Price FROM Products WHERE Products.ProductID = NEW.ProductID) */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `update_orders_total_after_update` AFTER UPDATE ON `orderitems` FOR EACH ROW BEGIN
    UPDATE orders
    SET TotalAmount = (
        SELECT SUM(Total)
        FROM orderitems
        WHERE OrderID = NEW.OrderID
    )
    WHERE OrderID = NEW.OrderID;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `update_orders_total_after_delete` AFTER DELETE ON `orderitems` FOR EACH ROW BEGIN
    UPDATE orders
    SET TotalAmount = COALESCE(
        (SELECT SUM(Total)
         FROM orderitems
         WHERE OrderID = OLD.OrderID
        ), 0.00)
    WHERE OrderID = OLD.OrderID;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `OrderID` int NOT NULL,
  `OrderDate` date DEFAULT NULL,
  `CustomerName` varchar(50) NOT NULL,
  `TotalAmount` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`OrderID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (1,'2024-02-01','Juan Sipag',300.00),(2,'2024-02-02','Emma Kulada',849.99),(3,'2024-02-03','Edi Malungkot',2400.00),(4,'2024-02-04','Ana Kala',400.00),(5,'2024-02-05','Pedro Penduko',700.00),(6,'2024-02-06','Ethan Sikat',1999.98),(7,'2024-02-07','Olivia Garcia',360.00),(8,'2024-02-08','Viky Tulog',0.00),(9,'2024-02-09','Edward Sipon',75.00),(10,'2024-02-10','Jului Malakas',0.00),(11,'2024-02-11','Michael Jordan',2889.93),(12,'2024-02-12','LeBron James',2300.00),(13,'2024-02-13','Kobe Bryant',1250.00),(14,'2024-02-14','Stephen Curry',10359.90),(15,'2024-02-15','Kevin Durant',249.75),(16,'2024-02-16','Tim Duncan',297.50),(17,'2024-02-17','Shaquille O\'Neal',397.25),(18,'2024-02-18','Magic Johnson',8466.00),(19,'2024-02-19','Larry Bird',110.50),(20,'2024-02-20','Kareem Abdul-Jabbar',365.00),(21,'2024-02-21','Bill Russell',461.75),(22,'2024-02-22','Wilt Chamberlain',89.25),(23,'2024-02-23','Jerry West',15008.75),(24,'2024-02-24','Hakeem Olajuwon',225.25),(25,'2024-02-25','Kevin Garnett',NULL),(26,'2024-02-26','Dirk Nowitzki',NULL),(27,'2024-02-27','Dwyane Wade',NULL),(28,'2024-02-28','Paul Pierce',NULL),(29,'2024-02-29','Ray Allen',NULL),(30,'2024-02-29','Chris Bosh',75.00);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `ordersummary`
--

DROP TABLE IF EXISTS `ordersummary`;
/*!50001 DROP VIEW IF EXISTS `ordersummary`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `ordersummary` AS SELECT 
 1 AS `OrderID`,
 1 AS `OrderDate`,
 1 AS `CustomerName`,
 1 AS `TotalAmount`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `ProductID` int NOT NULL,
  `ProductName` varchar(50) NOT NULL,
  `CategoryID` int DEFAULT NULL,
  `Price` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`ProductID`),
  KEY `fk_category` (`CategoryID`),
  CONSTRAINT `fk_category` FOREIGN KEY (`CategoryID`) REFERENCES `categories` (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (1,'Blue Pen',1,15.00),(2,'Black Pen',1,15.00),(3,'Spiral Notebook',2,90.00),(4,'No. 2 Pencils (Pack of 12)',3,399.99),(5,'Large Eraser',4,50.00),(6,'Highlighters (Pack of 4)',5,450.00),(7,'3-Ring Binder',6,100.00),(8,'12-Inch Ruler',7,175.00),(9,'School Backpack',8,999.99),(10,'Scissors',9,60.00),(11,'Red Pen',1,12.50),(12,'College-Ruled Notebook',2,149.75),(13,'Mechanical Pencils (Pack of 10)',3,69.75),(14,'Whiteboard Eraser',4,39.50),(15,'Dry Erase Markers (Pack of 6)',5,49.75),(16,'Expanding File Folder',6,19.75),(17,'Protractor',7,6.75),(18,'Messenger Bag',8,1200.75),(19,'Craft Scissors',9,12.50),(20,'School Glue (8 oz)',10,5.75),(21,'Blue Highlighter',5,29.50),(22,'Composition Notebook',2,99.50),(23,'Graphite Pencils (Pack of 12)',3,59.75),(24,'Correction Tape',4,14.50),(25,'Index Dividers (Set of 5)',6,12.75),(26,'Geometric Set',7,8.50),(27,'Rolling Backpack',8,1500.00),(28,'Safety Scissors',9,8.75),(29,'Glue Stick (Pack of 3)',10,9.50),(30,'Mechanical Eraser',4,19.75);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `productswithcategory`
--

DROP TABLE IF EXISTS `productswithcategory`;
/*!50001 DROP VIEW IF EXISTS `productswithcategory`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `productswithcategory` AS SELECT 
 1 AS `ProductID`,
 1 AS `ProductName`,
 1 AS `CategoryName`,
 1 AS `Price`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `suppliers`
--

DROP TABLE IF EXISTS `suppliers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suppliers` (
  `SupplierID` int NOT NULL,
  `SupplierName` varchar(50) NOT NULL,
  PRIMARY KEY (`SupplierID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `suppliers`
--

LOCK TABLES `suppliers` WRITE;
/*!40000 ALTER TABLE `suppliers` DISABLE KEYS */;
INSERT INTO `suppliers` VALUES (1,'Orions Stationery'),(2,'Faber Office Supplies'),(3,'123 School Mart'),(4,'Best Supplies Co.'),(5,'Quick Ship Supplies'),(6,'Global Office Solutions'),(7,'School Essentials Ltd.'),(8,'Office Depot'),(9,'Mega School Supplies'),(10,'Superior Office Products');
/*!40000 ALTER TABLE `suppliers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `Firstname` varchar(255) DEFAULT NULL,
  `Middlename` varchar(255) DEFAULT NULL,
  `Lastname` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `Usertype` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin','bu123','John Alvan','Blanco','Bernal','admin@example.com','Active','Admin'),(2,'teph','pass','Joseph','Borlagdatan','Riosa','joseph@gmail.com','Active','Admin'),(3,'vedo','143','Joshua','Encinares','Vergara','joshua@gmail.com','Inactive','Admin'),(4,'john','secret','John','Blanco','Bernal','john@gmail.com','Active','Admin'),(5,'aris','bu2024','Aris','Bale','Mortel','aris@gmail.com','Active','Admin'),(6,'paul','paul12','Paul','Kwan','Descarga','paul@gmail.com','Inactive','Admin');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'school_supplydb'
--

--
-- Dumping routines for database 'school_supplydb'
--
/*!50003 DROP FUNCTION IF EXISTS `priceHigh` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `priceHigh`(oldPrice double, amount double) RETURNS double(10,2)
    DETERMINISTIC
BEGIN
	declare newPrice double;
	set newPrice = oldPrice * (1 + amount);
	return newPrice;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddOrderWithItems` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddOrderWithItems`(
    IN orderDate DATE,
    IN customerName VARCHAR(50),
    IN itemsList JSON
)
BEGIN
    DECLARE newOrderID INT;

    -- Insert into Orders table
    INSERT INTO Orders (OrderDate, CustomerName, TotalAmount)
    VALUES (orderDate, customerName, 0);

    -- Get the newly inserted order ID
    SET newOrderID = LAST_INSERT_ID();

    -- Loop through each item in the JSON array and insert into OrderItems
    INSERT INTO OrderItems (OrderID, ProductID, Quantity)
    SELECT newOrderID, items->>'$.productID', items->>'$.quantity'
    FROM JSON_TABLE(itemsList, '$[*]' COLUMNS (
        productID INT PATH '$.productID',
        quantity INT PATH '$.quantity'
    )) AS items;

    -- Update the total amount in the Orders table
    UPDATE Orders
    SET TotalAmount = (SELECT SUM(oi.Quantity * p.Price)
                      FROM OrderItems oi
                      JOIN Products p ON oi.ProductID = p.ProductID
                      WHERE oi.OrderID = newOrderID)
    WHERE OrderID = newOrderID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `highvalueorders`
--

/*!50001 DROP VIEW IF EXISTS `highvalueorders`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `highvalueorders` AS select `ordersummary`.`OrderID` AS `OrderID`,`ordersummary`.`OrderDate` AS `OrderDate`,`ordersummary`.`CustomerName` AS `CustomerName`,`ordersummary`.`TotalAmount` AS `TotalAmount` from `ordersummary` where (`ordersummary`.`TotalAmount` > 1000.00) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `ordersummary`
--

/*!50001 DROP VIEW IF EXISTS `ordersummary`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `ordersummary` AS select `o`.`OrderID` AS `OrderID`,`o`.`OrderDate` AS `OrderDate`,`o`.`CustomerName` AS `CustomerName`,sum((`oi`.`Quantity` * `p`.`Price`)) AS `TotalAmount` from ((`orders` `o` join `orderitems` `oi` on((`o`.`OrderID` = `oi`.`OrderID`))) join `products` `p` on((`oi`.`ProductID` = `p`.`ProductID`))) group by `o`.`OrderID`,`o`.`OrderDate`,`o`.`CustomerName` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `productswithcategory`
--

/*!50001 DROP VIEW IF EXISTS `productswithcategory`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `productswithcategory` AS select `p`.`ProductID` AS `ProductID`,`p`.`ProductName` AS `ProductName`,`c`.`CategoryName` AS `CategoryName`,`p`.`Price` AS `Price` from (`products` `p` join `categories` `c` on((`p`.`CategoryID` = `c`.`CategoryID`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-28 16:05:07
