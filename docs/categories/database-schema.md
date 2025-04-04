# Database Schema

## Tables
### 1. APP_CATEGORY_TYPES
| Column | Type | Nullable | Default | Description |
|--------|------|----------|---------|-------------|
| ID | RAW(16) | No | - | Primary key |
| CODE | NVARCHAR2(50) | No | - | Mã loại danh mục |
| NAME | NVARCHAR2(100) | No | - | Tên loại danh mục |
| DESCRIPTION | NVARCHAR2(2000) | Yes | NULL | Mô tả |
| IS_ACTIVE | NUMBER(1) | No | 1 | Trạng thái hoạt động |
| ALLOW_MULTIPLE_SELECT | NUMBER(1) | No | 0 | Cho phép chọn nhiều |
| EXTRA_PROPERTIES | NCLOB | Yes | NULL | Thuộc tính mở rộng |
| CONCURRENCY_STAMP | NVARCHAR2(40) | Yes | NULL | Concurrency stamp |
| CREATION_TIME | TIMESTAMP(6) | No | CURRENT_TIMESTAMP | Thời điểm tạo |
| CREATOR_ID | RAW(16) | Yes | NULL | ID người tạo |
| LAST_MODIFICATION_TIME | TIMESTAMP(6) | Yes | NULL | Thời điểm cập nhật |
| LAST_MODIFIER_ID | RAW(16) | Yes | NULL | ID người cập nhật |
| IS_DELETED | NUMBER(1) | No | 0 | Đánh dấu xóa |
| DELETER_ID | RAW(16) | Yes | NULL | ID người xóa |
| DELETION_TIME | TIMESTAMP(6) | Yes | NULL | Thời điểm xóa |

### 2. APP_CATEGORY_ITEMS
| Column | Type | Nullable | Default | Description |
|--------|------|----------|---------|-------------|
| ID | RAW(16) | No | - | Primary key |
| CATEGORY_TYPE_ID | RAW(16) | No | - | FK to APP_CATEGORY_TYPES |
| CODE | NVARCHAR2(50) | No | - | Mã danh mục |
| NAME | NVARCHAR2(100) | No | - | Tên danh mục |
| PARENT_ID | RAW(16) | Yes | NULL | FK to APP_CATEGORY_ITEMS |
| DISPLAY_ORDER | NUMBER(10) | No | 0 | Thứ tự hiển thị |
| IS_ACTIVE | NUMBER(1) | No | 1 | Trạng thái hoạt động |
| VALUE | NVARCHAR2(500) | Yes | NULL | Giá trị |
| ICON | NVARCHAR2(100) | Yes | NULL | Icon |
| EXTRA_PROPERTIES | NCLOB | Yes | NULL | Thuộc tính mở rộng |
| CONCURRENCY_STAMP | NVARCHAR2(40) | Yes | NULL | Concurrency stamp |
| CREATION_TIME | TIMESTAMP(6) | No | CURRENT_TIMESTAMP | Thời điểm tạo |
| CREATOR_ID | RAW(16) | Yes | NULL | ID người tạo |
| LAST_MODIFICATION_TIME | TIMESTAMP(6) | Yes | NULL | Thời điểm cập nhật |
| LAST_MODIFIER_ID | RAW(16) | Yes | NULL | ID người cập nhật |
| IS_DELETED | NUMBER(1) | No | 0 | Đánh dấu xóa |
| DELETER_ID | RAW(16) | Yes | NULL | ID người xóa |
| DELETION_TIME | TIMESTAMP(6) | Yes | NULL | Thời điểm xóa |

## Indexes
### APP_CATEGORY_TYPES
1. PK_APP_CATEGORY_TYPES
   - Table: APP_CATEGORY_TYPES
   - Columns: ID
   - Type: Primary Key
   - Using: B-tree

2. UK_APP_CATEGORY_TYPES_CODE
   - Table: APP_CATEGORY_TYPES
   - Columns: CODE
   - Type: Unique
   - Filter: IS_DELETED = 0
   - Using: B-tree
   - Include: (NAME, IS_ACTIVE)

3. IX_APP_CATEGORY_TYPES_IS_ACTIVE
   - Table: APP_CATEGORY_TYPES
   - Columns: IS_ACTIVE
   - Type: Non-unique
   - Filter: IS_DELETED = 0
   - Using: Bitmap
   - Include: (ID, CODE, NAME)

4. IX_APP_CATEGORY_TYPES_AUDIT
   - Table: APP_CATEGORY_TYPES
   - Columns: (CREATION_TIME, CREATOR_ID)
   - Type: Non-unique
   - Using: B-tree

### APP_CATEGORY_ITEMS
1. PK_APP_CATEGORY_ITEMS
   - Table: APP_CATEGORY_ITEMS
   - Columns: ID
   - Type: Primary Key
   - Using: B-tree

2. UK_APP_CATEGORY_ITEMS_TYPE_CODE
   - Table: APP_CATEGORY_ITEMS
   - Columns: (CATEGORY_TYPE_ID, CODE)
   - Type: Unique
   - Filter: IS_DELETED = 0
   - Using: B-tree
   - Include: (NAME, IS_ACTIVE)

3. IX_APP_CATEGORY_ITEMS_PARENT
   - Table: APP_CATEGORY_ITEMS
   - Columns: PARENT_ID
   - Type: Non-unique
   - Filter: IS_DELETED = 0
   - Using: B-tree
   - Include: (CATEGORY_TYPE_ID, CODE, NAME)

4. IX_APP_CATEGORY_ITEMS_TYPE_ACTIVE
   - Table: APP_CATEGORY_ITEMS
   - Columns: (CATEGORY_TYPE_ID, IS_ACTIVE)
   - Type: Non-unique
   - Filter: IS_DELETED = 0
   - Using: Bitmap
   - Include: (ID, CODE, NAME)

5. IX_APP_CATEGORY_ITEMS_DISPLAY_ORDER
   - Table: APP_CATEGORY_ITEMS
   - Columns: (CATEGORY_TYPE_ID, DISPLAY_ORDER)
   - Type: Non-unique
   - Filter: IS_DELETED = 0
   - Using: B-tree
   - Include: (ID, CODE, NAME)

6. IX_APP_CATEGORY_ITEMS_AUDIT
   - Table: APP_CATEGORY_ITEMS
   - Columns: (CREATION_TIME, CREATOR_ID)
   - Type: Non-unique
   - Using: B-tree

## Foreign Keys
1. FK_APP_CATEGORY_ITEMS_TYPE
   - From: APP_CATEGORY_ITEMS.CATEGORY_TYPE_ID
   - To: APP_CATEGORY_TYPES.ID
   - On Delete: RESTRICT
   - Deferrable: INITIALLY IMMEDIATE

2. FK_APP_CATEGORY_ITEMS_PARENT
   - From: APP_CATEGORY_ITEMS.PARENT_ID
   - To: APP_CATEGORY_ITEMS.ID
   - On Delete: RESTRICT
   - Deferrable: INITIALLY IMMEDIATE

## Migrations
### Initial Migration
```sql
-- Create APP_CATEGORY_TYPES table
CREATE TABLE APP_CATEGORY_TYPES (
    ID RAW(16) NOT NULL,
    CODE NVARCHAR2(50) NOT NULL,
    NAME NVARCHAR2(100) NOT NULL,
    DESCRIPTION NVARCHAR2(2000),
    IS_ACTIVE NUMBER(1) DEFAULT 1 NOT NULL,
    ALLOW_MULTIPLE_SELECT NUMBER(1) DEFAULT 0 NOT NULL,
    EXTRA_PROPERTIES NCLOB,
    CONCURRENCY_STAMP NVARCHAR2(40),
    CREATION_TIME TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    CREATOR_ID RAW(16),
    LAST_MODIFICATION_TIME TIMESTAMP,
    LAST_MODIFIER_ID RAW(16),
    IS_DELETED NUMBER(1) DEFAULT 0 NOT NULL,
    DELETER_ID RAW(16),
    DELETION_TIME TIMESTAMP,
    CONSTRAINT PK_APP_CATEGORY_TYPES PRIMARY KEY (ID)
);

-- Create APP_CATEGORY_ITEMS table
CREATE TABLE APP_CATEGORY_ITEMS (
    ID RAW(16) NOT NULL,
    CATEGORY_TYPE_ID RAW(16) NOT NULL,
    CODE NVARCHAR2(50) NOT NULL,
    NAME NVARCHAR2(100) NOT NULL,
    PARENT_ID RAW(16),
    DISPLAY_ORDER NUMBER(10) DEFAULT 0 NOT NULL,
    IS_ACTIVE NUMBER(1) DEFAULT 1 NOT NULL,
    VALUE NVARCHAR2(500),
    ICON NVARCHAR2(100),
    EXTRA_PROPERTIES NCLOB,
    CONCURRENCY_STAMP NVARCHAR2(40),
    CREATION_TIME TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    CREATOR_ID RAW(16),
    LAST_MODIFICATION_TIME TIMESTAMP,
    LAST_MODIFIER_ID RAW(16),
    IS_DELETED NUMBER(1) DEFAULT 0 NOT NULL,
    DELETER_ID RAW(16),
    DELETION_TIME TIMESTAMP,
    CONSTRAINT PK_APP_CATEGORY_ITEMS PRIMARY KEY (ID),
    CONSTRAINT FK_APP_CATEGORY_ITEMS_TYPE FOREIGN KEY (CATEGORY_TYPE_ID) REFERENCES APP_CATEGORY_TYPES(ID),
    CONSTRAINT FK_APP_CATEGORY_ITEMS_PARENT FOREIGN KEY (PARENT_ID) REFERENCES APP_CATEGORY_ITEMS(ID)
);

-- Create indexes
CREATE UNIQUE INDEX UK_APP_CATEGORY_TYPES_CODE ON APP_CATEGORY_TYPES(CODE) WHERE IS_DELETED = 0;
CREATE INDEX IX_APP_CATEGORY_TYPES_IS_ACTIVE ON APP_CATEGORY_TYPES(IS_ACTIVE) WHERE IS_DELETED = 0;

CREATE UNIQUE INDEX UK_APP_CATEGORY_ITEMS_TYPE_CODE ON APP_CATEGORY_ITEMS(CATEGORY_TYPE_ID, CODE) WHERE IS_DELETED = 0;
CREATE INDEX IX_APP_CATEGORY_ITEMS_PARENT ON APP_CATEGORY_ITEMS(PARENT_ID) WHERE IS_DELETED = 0;
CREATE INDEX IX_APP_CATEGORY_ITEMS_TYPE_ACTIVE ON APP_CATEGORY_ITEMS(CATEGORY_TYPE_ID, IS_ACTIVE) WHERE IS_DELETED = 0;
CREATE INDEX IX_APP_CATEGORY_ITEMS_DISPLAY_ORDER ON APP_CATEGORY_ITEMS(CATEGORY_TYPE_ID, DISPLAY_ORDER) WHERE IS_DELETED = 0;
``` 

## Performance Optimization

### 1. Partitioning
CategoryItems có thể được partition theo CATEGORY_TYPE_ID để cải thiện performance khi query theo loại danh mục:

```sql
ALTER TABLE APP_CATEGORY_ITEMS 
PARTITION BY HASH (CATEGORY_TYPE_ID) 
PARTITIONS 8;
```

### 2. Materialized Views
Tạo materialized view cho các query thường xuyên sử dụng:

```sql
CREATE MATERIALIZED VIEW MV_CATEGORY_HIERARCHY
BUILD IMMEDIATE
REFRESH FAST ON COMMIT
AS
SELECT 
    i.ID,
    i.CODE,
    i.NAME,
    i.PARENT_ID,
    t.CODE AS TYPE_CODE,
    t.NAME AS TYPE_NAME,
    LEVEL as DEPTH,
    SYS_CONNECT_BY_PATH(i.NAME, '/') as FULL_PATH
FROM 
    APP_CATEGORY_ITEMS i
    JOIN APP_CATEGORY_TYPES t ON i.CATEGORY_TYPE_ID = t.ID
WHERE
    i.IS_DELETED = 0
    AND t.IS_DELETED = 0
START WITH 
    i.PARENT_ID IS NULL
CONNECT BY 
    PRIOR i.ID = i.PARENT_ID;
```

### 3. Result Cache
Sử dụng Result Cache cho các query thường xuyên:

```sql
SELECT /*+ RESULT_CACHE */
    i.ID,
    i.CODE,
    i.NAME,
    t.NAME as TYPE_NAME
FROM 
    APP_CATEGORY_ITEMS i
    JOIN APP_CATEGORY_TYPES t ON i.CATEGORY_TYPE_ID = t.ID
WHERE
    i.IS_DELETED = 0
    AND t.IS_DELETED = 0;
```

### 4. Index Organized Tables
Sử dụng Index Organized Table cho các bảng lookup:

```sql
CREATE TABLE CATEGORY_LOOKUP
(
    CODE VARCHAR2(50) PRIMARY KEY,
    VALUE VARCHAR2(500)
)
ORGANIZATION INDEX;
```

## Monitoring và Maintenance
1. Gather statistics định kỳ:
```sql
BEGIN
    DBMS_STATS.GATHER_TABLE_STATS(
        ownname => 'SCHEMA_NAME',
        tabname => 'APP_CATEGORY_ITEMS',
        estimate_percent => DBMS_STATS.AUTO_SAMPLE_SIZE,
        method_opt => 'FOR ALL COLUMNS SIZE AUTO'
    );
END;
```

2. Monitor index usage:
```sql
SELECT * FROM V$OBJECT_USAGE 
WHERE TABLE_NAME IN ('APP_CATEGORY_TYPES', 'APP_CATEGORY_ITEMS');
```

3. Analyze execution plans:
```sql
EXPLAIN PLAN FOR
SELECT /*+ GATHER_PLAN_STATISTICS */
    i.ID, i.CODE, i.NAME
FROM APP_CATEGORY_ITEMS i
WHERE i.CATEGORY_TYPE_ID = :1
AND i.IS_DELETED = 0;

SELECT * FROM TABLE(DBMS_XPLAN.DISPLAY);
``` 