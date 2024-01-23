/*
 Navicat Premium Data Transfer

 Source Server         : dokerMysql
 Source Server Type    : MySQL
 Source Server Version : 80033 (8.0.33)
 Source Host           : localhost:3307
 Source Schema         : learning

 Target Server Type    : MySQL
 Target Server Version : 80033 (8.0.33)
 File Encoding         : 65001

 Date: 28/04/2023 18:54:25
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for Course
-- ----------------------------
DROP TABLE IF EXISTS `Course`;
CREATE TABLE `Course`  (
  `CId` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Cname` varchar(10) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL,
  `TId` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Course
-- ----------------------------
INSERT INTO `Course` VALUES ('01', '语文', '02');
INSERT INTO `Course` VALUES ('02', '数学', '01');
INSERT INTO `Course` VALUES ('03', '英语', '03');

-- ----------------------------
-- Table structure for SC
-- ----------------------------
DROP TABLE IF EXISTS `SC`;
CREATE TABLE `SC`  (
  `SId` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CId` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `score` decimal(18, 1) NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of SC
-- ----------------------------
INSERT INTO `SC` VALUES ('01', '01', 80.0);
INSERT INTO `SC` VALUES ('01', '02', 90.0);
INSERT INTO `SC` VALUES ('01', '03', 99.0);
INSERT INTO `SC` VALUES ('02', '01', 70.0);
INSERT INTO `SC` VALUES ('02', '02', 60.0);
INSERT INTO `SC` VALUES ('02', '03', 80.0);
INSERT INTO `SC` VALUES ('03', '01', 80.0);
INSERT INTO `SC` VALUES ('03', '02', 80.0);
INSERT INTO `SC` VALUES ('03', '03', 80.0);
INSERT INTO `SC` VALUES ('04', '01', 50.0);
INSERT INTO `SC` VALUES ('04', '02', 30.0);
INSERT INTO `SC` VALUES ('04', '03', 20.0);
INSERT INTO `SC` VALUES ('05', '01', 76.0);
INSERT INTO `SC` VALUES ('05', '02', 87.0);
INSERT INTO `SC` VALUES ('06', '01', 31.0);
INSERT INTO `SC` VALUES ('06', '03', 34.0);
INSERT INTO `SC` VALUES ('07', '02', 89.0);
INSERT INTO `SC` VALUES ('07', '03', 98.0);

-- ----------------------------
-- Table structure for Student
-- ----------------------------
DROP TABLE IF EXISTS `Student`;
CREATE TABLE `Student`  (
  `SId` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Sname` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Sage` datetime NULL DEFAULT NULL,
  `Ssex` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Student
-- ----------------------------
INSERT INTO `Student` VALUES ('01', '赵雷', '1990-01-01 00:00:00', '男');
INSERT INTO `Student` VALUES ('02', '钱电', '1990-12-21 00:00:00', '男');
INSERT INTO `Student` VALUES ('03', '孙风', '1990-12-20 00:00:00', '男');
INSERT INTO `Student` VALUES ('04', '李云', '1990-12-06 00:00:00', '男');
INSERT INTO `Student` VALUES ('05', '周梅', '1991-12-01 00:00:00', '女');
INSERT INTO `Student` VALUES ('06', '吴兰', '1992-01-01 00:00:00', '女');
INSERT INTO `Student` VALUES ('07', '郑竹', '1989-01-01 00:00:00', '女');
INSERT INTO `Student` VALUES ('09', '张三', '2017-12-20 00:00:00', '女');
INSERT INTO `Student` VALUES ('10', '李四', '2017-12-25 00:00:00', '女');
INSERT INTO `Student` VALUES ('11', '李四', '2012-06-06 00:00:00', '女');
INSERT INTO `Student` VALUES ('12', '赵六', '2013-06-13 00:00:00', '女');
INSERT INTO `Student` VALUES ('13', '孙七', '2014-06-01 00:00:00', '女');

-- ----------------------------
-- Table structure for Teacher
-- ----------------------------
DROP TABLE IF EXISTS `Teacher`;
CREATE TABLE `Teacher`  (
  `TId` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Tname` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Teacher
-- ----------------------------
INSERT INTO `Teacher` VALUES ('01', '张三');
INSERT INTO `Teacher` VALUES ('02', '李四');
INSERT INTO `Teacher` VALUES ('03', '王五');

SET FOREIGN_KEY_CHECKS = 1;
