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

 Date: 14/08/2023 09:23:25
*/


-- ----------------------------
-- Table structure for books
-- ----------------------------
DROP TABLE IF EXISTS "public"."books";
CREATE TABLE "public"."books" (
  "bk_id" bigserial,
  "bk_title" varchar(255) COLLATE "pg_catalog"."default",
  "bk_writer" varchar(255) COLLATE "pg_catalog"."default",
  "bk_isbn" varchar(255) COLLATE "pg_catalog"."default",
  "bk_publisher" varchar(255) COLLATE "pg_catalog"."default",
  "bk_page" int4,
  "bk_synopsis" text COLLATE "pg_catalog"."default",
  "bk_dimension" varchar(255) COLLATE "pg_catalog"."default",
  "bk_language" varchar(50) COLLATE "pg_catalog"."default",
  "bk_date_issue" date,
  "bk_quantity" int4,
  "bk_image_path" text COLLATE "pg_catalog"."default",
  "bk_created_date" timestamp(6)
)
;

-- ----------------------------
-- Primary Key structure for table books
-- ----------------------------
ALTER TABLE "public"."books" ADD CONSTRAINT "books_pkey" PRIMARY KEY ("bk_id");
