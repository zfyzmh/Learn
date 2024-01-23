/*
 Navicat Premium Data Transfer

 Source Server         : dokerMysql
 Source Server Type    : MySQL
 Source Server Version : 80033 (8.0.33)
 Source Host           : localhost:3307
 Source Schema         : learning2

 Target Server Type    : MySQL
 Target Server Version : 80033 (8.0.33)
 File Encoding         : 65001

 Date: 28/04/2023 18:54:42
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for Courses
-- ----------------------------
DROP TABLE IF EXISTS `Courses`;
CREATE TABLE `Courses`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Cname` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `TeacherId` int NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Courses_TeacherId`(`TeacherId` ASC) USING BTREE,
  CONSTRAINT `FK_Courses_Teachers_TeacherId` FOREIGN KEY (`TeacherId`) REFERENCES `Teachers` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Courses
-- ----------------------------
INSERT INTO `Courses` VALUES (1, '语文', 2);
INSERT INTO `Courses` VALUES (2, '数学', 1);
INSERT INTO `Courses` VALUES (3, '英语', 3);

-- ----------------------------
-- Table structure for Scs
-- ----------------------------
DROP TABLE IF EXISTS `Scs`;
CREATE TABLE `Scs`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CourseId` int NOT NULL,
  `StudentId` int NOT NULL,
  `Score` decimal(65, 30) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Scs_CourseId`(`CourseId` ASC) USING BTREE,
  INDEX `IX_Scs_StudentId`(`StudentId` ASC) USING BTREE,
  CONSTRAINT `FK_Scs_Courses_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `Courses` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Scs_Students_StudentId` FOREIGN KEY (`StudentId`) REFERENCES `Students` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 19 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Scs
-- ----------------------------
INSERT INTO `Scs` VALUES (1, 1, 1, 80.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (2, 2, 1, 90.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (3, 3, 1, 99.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (4, 1, 2, 70.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (5, 2, 2, 60.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (6, 3, 2, 80.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (7, 1, 3, 80.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (8, 2, 3, 80.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (9, 3, 3, 80.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (10, 1, 4, 50.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (11, 2, 4, 30.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (12, 3, 4, 20.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (13, 1, 5, 76.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (14, 2, 5, 87.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (15, 1, 6, 31.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (16, 3, 6, 34.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (17, 2, 7, 89.000000000000000000000000000000);
INSERT INTO `Scs` VALUES (18, 3, 7, 98.000000000000000000000000000000);

-- ----------------------------
-- Table structure for Students
-- ----------------------------
DROP TABLE IF EXISTS `Students`;
CREATE TABLE `Students`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Sage` datetime(6) NULL DEFAULT NULL,
  `Ssex` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 14 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Students
-- ----------------------------
INSERT INTO `Students` VALUES (1, '赵雷', '1990-01-01 00:00:00.000000', '男');
INSERT INTO `Students` VALUES (2, '钱电', '1990-12-21 00:00:00.000000', '男');
INSERT INTO `Students` VALUES (3, '孙风', '1990-12-20 00:00:00.000000', '男');
INSERT INTO `Students` VALUES (4, '李云', '1990-12-06 00:00:00.000000', '男');
INSERT INTO `Students` VALUES (5, '周梅', '1991-12-01 00:00:00.000000', '女');
INSERT INTO `Students` VALUES (6, '吴兰', '1992-01-01 00:00:00.000000', '女');
INSERT INTO `Students` VALUES (7, '郑竹', '1989-01-01 00:00:00.000000', '女');
INSERT INTO `Students` VALUES (9, '张三', '2017-12-20 00:00:00.000000', '女');
INSERT INTO `Students` VALUES (10, '李四', '2017-12-25 00:00:00.000000', '女');
INSERT INTO `Students` VALUES (11, '李四', '2012-06-06 00:00:00.000000', '女');
INSERT INTO `Students` VALUES (12, '赵六', '2013-06-13 00:00:00.000000', '女');
INSERT INTO `Students` VALUES (13, '孙七', '2014-06-01 00:00:00.000000', '女');

-- ----------------------------
-- Table structure for Teachers
-- ----------------------------
DROP TABLE IF EXISTS `Teachers`;
CREATE TABLE `Teachers`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Teachers
-- ----------------------------
INSERT INTO `Teachers` VALUES (1, '张三');
INSERT INTO `Teachers` VALUES (2, '李四');
INSERT INTO `Teachers` VALUES (3, '王五');

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE `__EFMigrationsHistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
INSERT INTO `__EFMigrationsHistory` VALUES ('20230428104151_migrationName', '7.0.5');
INSERT INTO `__EFMigrationsHistory` VALUES ('20230428105144_migrationName2', '7.0.5');

SET FOREIGN_KEY_CHECKS = 1;
