/*
 Navicat Premium Data Transfer

 Source Server         : Local
 Source Server Type    : PostgreSQL
 Source Server Version : 130011 (130011)
 Source Host           : localhost:5432
 Source Catalog        : library
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 130011 (130011)
 File Encoding         : 65001

 Date: 14/08/2023 09:23:37
*/


-- ----------------------------
-- Table structure for borrower
-- ----------------------------
DROP TABLE IF EXISTS "public"."borrower";
CREATE TABLE "public"."borrower" (
  "br_id" bigserial,
  "br_name" varchar(255) COLLATE "pg_catalog"."default",
  "br_nim" varchar(20) COLLATE "pg_catalog"."default",
  "br_phone_number" varchar(15) COLLATE "pg_catalog"."default",
  "br_address" text COLLATE "pg_catalog"."default",
  "br_faculty" varchar(50) COLLATE "pg_catalog"."default",
  "br_major" varchar(50) COLLATE "pg_catalog"."default",
  "br_borrow_date" date,
  "br_return_date" date,
  "br_penalty_amount" numeric(18,0),
  "br_created_date" timestamp(6),
  "br_updated_date" timestamp(6)
)
;

-- ----------------------------
-- Primary Key structure for table borrower
-- ----------------------------
ALTER TABLE "public"."borrower" ADD CONSTRAINT "borrower_pkey" PRIMARY KEY ("br_id");
