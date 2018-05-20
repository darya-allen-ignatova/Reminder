CREATE TABLE [Prompts] (
    [ID]             INT        IDENTITY (1, 1) NOT NULL,
    [Name]           NCHAR (50) NOT NULL,
    [CategoryID]     INT        NOT NULL,
    [TimeOfPrompt]   TIME (7)   NULL,
    [DateOfCreating] DATE       NOT NULL,
    [Description]    NTEXT      NULL,
    [Image]          IMAGE      NULL,
    CONSTRAINT [PK_Prompts_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Prompts_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [Categories] ([ID])
);


