CREATE TABLE [Categories] 
(
    [ID]       INT       IDENTITY (1, 1) NOT NULL,
    [Name]     CHAR (50) NOT NULL,
    [ParentID] INT       NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([ID] ASC)
);


