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

 Date: 14/08/2023 09:23:52
*/


-- ----------------------------
-- Table structure for borrower_book_transaction
-- ----------------------------
DROP TABLE IF EXISTS "public"."borrower_book_transaction";
CREATE TABLE "public"."borrower_book_transaction" (
  "brt_id" bigserial,
  "brt_br_id" int8,
  "brt_bk_id" int8,
  "brt_created_date" timestamp(6)
)
;

-- ----------------------------
-- Primary Key structure for table borrower_book_transaction
-- ----------------------------
ALTER TABLE "public"."borrower_book_transaction" ADD CONSTRAINT "borrower_book_transaction_pkey" PRIMARY KEY ("brt_id");
