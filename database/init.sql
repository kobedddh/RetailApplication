create DATABASE retail;
use retail;

create table product(
    ProductId int not null PRIMARY KEY auto_increment,
    Name varchar(50) not null,
    Price decimal(19,4) not null default 0.00,
	CreatedOn datetime DEFAULT CURRENT_TIMESTAMP,
    ModifiedOn datetime ON UPDATE CURRENT_TIMESTAMP
);

create table productapproval(
    ProductApprovalId int not null PRIMARY KEY auto_increment,
    ProductId int null,
    ProductName varchar(50) not null,
    ProductPrice decimal(19,4) not null default 0.00,
    RequestReason varchar(200) not null,
    RequestDate datetime DEFAULT CURRENT_TIMESTAMP,
    RequestType int not null
);

alter table productapproval
add foreign key(ProductId) references product(ProductId);