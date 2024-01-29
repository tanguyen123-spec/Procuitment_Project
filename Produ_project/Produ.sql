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
    AWID varchar(50)  PRIMARY KEY,
    PCScustomer int,
    color varchar(50),
    size Varchar(50),
    Note text,
    FOREIGN KEY (AWID) REFERENCES ArtWork(AWID)

);
CREATE TABLE Users (
    UserID varchar(30)  PRIMARY KEY,
	NameUser varchar(20),
	Role_ bit,
	Password_ varchar(50),
);


CREATE TABLE SupplierInFo (
    SlID varchar(50),
    SupplierName varchar(50),
    CategoriesID varchar(50),
    Address_ varchar (100),
	City varchar(100),
    EstablishedYear varchar(50),
	Numberofworkers int,
	 MainProductID varchar(50),
	MOQ   varchar (100),
	Certificate_ varchar (100),
	Customized varchar (100),
	SampleProcess varchar(100),
	Leadtime varchar(100),
	ExportUS bit,
	Websitelink varchar(100),
	Email varchar(30),
	Phone varchar(30),
	ContactPerson varchar(30),
	Note text,
	UserID varchar(30),
	ReviewQA bit,
	DateQA datetime,
   FOREIGN KEY (CategoriesID) REFERENCES Categories(CategoriesID),
   FOREIGN KEY (MainProductID) REFERENCES MainProduct(MainProductID),
   FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

