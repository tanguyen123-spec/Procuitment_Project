create database Produ_project
-- Tạo bảng Categories
CREATE TABLE Categories (
    CategoriesID varchar(50) PRIMARY KEY,
    NameCategories varchar(50)
);

-- Tạo bảng MainProduct
CREATE TABLE MainProduct (
    MainProductID varchar(50) PRIMARY KEY,
    NameMP varchar(50),
    CategoriesID varchar(50),
    FOREIGN KEY (CategoriesID) REFERENCES Categories(CategoriesID)
);

-- Tạo bảng ArtWork
CREATE TABLE ArtWork (
    AWID varchar(50) PRIMARY KEY,
    NameAW varchar(50),
    MainProductID varchar(50),
    ImgagesUrl varchar(100),
    FOREIGN KEY (MainProductID) REFERENCES MainProduct(MainProductID)
);

-- Tạo bảng Quality
CREATE TABLE Quality (
    AWID varchar(50),
    PCS int,
    color varchar(50),
    size Varchar(50),
    Note text,
    FOREIGN KEY (AWID) REFERENCES ArtWork(AWID)
);
CREATE TABLE SupplierInFo (
    SlID varchar(50),
    SupplierName varchar(50),
    CategoriesID varchar(50),
    Address_ varchar (100),
	City varchar(100),
    EstablishedYear int,
	Numberofworkers int,
	 MainProductID varchar(50),
	MOQ   varchar (100),
	Certificate_ varchar (100),
	Customized varchar (100),
	SampleProcess varchar(100),
	Leadtime varchar(100),
	ExportUS bit,
   FOREIGN KEY (CategoriesID) REFERENCES Categories(CategoriesID),
   FOREIGN KEY (MainProductID) REFERENCES MainProduct(MainProductID)
);
CREATE TABLE Staff (
    StaffID varchar(50),
	NameStaff varchar(20),
	Role_ bit,
	Password_ varchar(50),
);

