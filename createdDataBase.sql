if not exists(select top 1 1 from master.sys.databases where name = 'dbProjetoTeste')
begin
	create database dbProjetoTeste;
end
GO

use dbProjetoTeste;
GO

if not exists(select top 1 1 from sysobjects WHERE name = 'tbUsers')
begin
	create table dbo.tbUsers 
	(
		id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
		email VARCHAR(150) NOT NULL
	);
end
GO

if not exists(select top 1 1 from dbo.tbUsers where email = 'teste@teste.com')
begin
	insert into dbo.tbUsers ( email ) values ( 'teste@teste.com' );
end
GO

if not exists(select top 1 1 from sysobjects WHERE name = 'tbFiles')
begin
	create table dbo.tbFiles 
	(
		id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
		createdUserId UNIQUEIDENTIFIER NOT NULL,
		createdDate DATETIME NOT NULL DEFAULT GETDATE(),
		name VARCHAR(30) NOT NULL,
		lote VARCHAR(10) NOT NULL,
		dateFile DATETIME NOT NULL,
		totalRegisters INT NOT NULL,
		FOREIGN KEY (createdUserId) REFERENCES dbo.tbUsers(id)
	);
end
GO

if not exists(select top 1 1 from sysobjects WHERE name = 'tbCreditCards')
begin
	create table dbo.tbCreditCards 
	(
		id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
		createdType TINYINT NOT NULL, -- 1: createdByFile; 2: createdByUser
		createdUserId UNIQUEIDENTIFIER NULL,
		createdDate DATETIME NULL,
		createdFileId UNIQUEIDENTIFIER NULL,
		identify CHAR(1) NULL,
		number VARCHAR(6) NULL,
		creditCard VARCHAR(30) NOT NULL,
		FOREIGN KEY (createdUserId) REFERENCES dbo.tbUsers(id),
		FOREIGN KEY (createdFileId) REFERENCES dbo.tbFiles(id)
	);
end
GO

if not exists(select top 1 1 from sysobjects WHERE name = 'tbLog')
begin
	create table dbo.tbLog 
	(
		id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
		userId UNIQUEIDENTIFIER NOT NULL,
		date DATETIME NOT NULL DEFAULT GETDATE(),
		action VARCHAR(100) NOT NULL,
		result NVARCHAR(MAX) NOT NULL,
		FOREIGN KEY (userId) REFERENCES dbo.tbUsers(id)
	);
end
GO

if exists(select top 1 1 from sys.objects where name = 'spGetUser')
begin
	drop procedure dbo.spGetUser;
end
GO

create procedure dbo.spGetUser   
    @email varchar(150)
as   
begin
    SET NOCOUNT ON;  

    select id, email from dbo.tbUsers where email = @email;  
end
GO  

if exists(select top 1 1 from sys.objects where name = 'spInsertCreditCardByUser')
begin
	drop procedure dbo.spInsertCreditCardByUser;
end
GO

create procedure dbo.spInsertCreditCardByUser
	@createdUserId uniqueidentifier,
	@creditCard varchar(30)
as   
begin
	SET NOCOUNT ON;

	declare @id uniqueidentifier;
	declare @result varchar(10);

	select @result = 'exists', @id = id from dbo.tbCreditCards where creditCard = @creditCard;

	if @id is null
	begin
		insert into dbo.tbCreditCards
			(
				createdType,
				createdUserId,
				createdDate,
				creditCard
			)
		values
			(
				2,
				@createdUserId,
				getdate(),
				@creditCard
			);

		select @result = 'created', @id = id from dbo.tbCreditCards where creditCard = @creditCard;
	end

	select @result result, @id id;
end
GO

if exists(select top 1 1 from sys.objects where name = 'spInsertFile')
begin
	drop procedure dbo.spInsertFile;
end
GO

create procedure dbo.spInsertFile   
	@createdUserId uniqueidentifier,
	@name varchar(30),
	@lote varchar(10),
	@dateFile datetime,
	@totalRegisters int
as   
begin
    SET NOCOUNT ON;  

	insert into dbo.tbFiles 
		(
			createdUserId,
			name,
			lote,
			dateFile,
			totalRegisters
		)
	values
		(
			@createdUserId,
			@name,
			@lote,
			@dateFile,
			@totalRegisters
		);

	select top 1 id from dbo.tbFiles order by createdDate desc;

end
GO 

if exists(select top 1 1 from sys.objects where name = 'spInsertCreditCardByFile')
begin
	drop procedure dbo.spInsertCreditCardByFile;
end
GO

create procedure dbo.spInsertCreditCardByFile
	@createdFileId uniqueidentifier,
	@identify char(1),
	@number varchar(6),
	@creditCard varchar(30)
as   
begin
	SET NOCOUNT ON;

	declare @id uniqueidentifier;
	declare @result varchar(10);

	select @result = 'exists', @id = id from dbo.tbCreditCards where creditCard = @creditCard;

	if @id is null
	begin
		insert into dbo.tbCreditCards
			(
				createdType,
				createdFileId,
				identify,
				number,
				creditCard
			)
		values
			(
				1,
				@createdFileId,
				@identify,
				@number,
				@creditCard
			);

		select @result = 'created', @id = id from dbo.tbCreditCards where creditCard = @creditCard;
	end

	select @result result, @id id;
end
GO

if exists(select top 1 1 from sys.objects where name = 'spInsertLog')
begin
	drop procedure dbo.spInsertLog;
end
GO

create procedure dbo.spInsertLog
	@userId uniqueidentifier,
	@action varchar(100),
	@result nvarchar(max)
as   
begin
	SET NOCOUNT ON;

	insert into dbo.tbLog
		(
			userId,
			action,
			result
		)
	values
		(
			@userId,
			@action,
			@result
		);

	select top 1 id from dbo.tbLog order by date desc;

end
GO

if exists(select top 1 1 from sys.objects where name = 'spGetCreditCard')
begin
	drop procedure dbo.spGetCreditCard;
end
GO

create procedure dbo.spGetCreditCard   
    @creditCard varchar(30)
as   
begin
    SET NOCOUNT ON;  

	declare @result varchar(10) = 'not exists';
    if exists(select top 1 1 from dbo.tbCreditCards where creditCard = @creditCard)
	begin
		set @result = 'exists';
	end
	
	select @result result;
end
GO  