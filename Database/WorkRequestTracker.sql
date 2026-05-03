CREATE TABLE WorkRequests (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    ClientName NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Priority VARCHAR(20) NOT NULL,
    Status VARCHAR(20) NOT NULL,
    DueDate DATETIME NOT NULL,
    CreatedDate DATETIME NOT NULL,
    UpdatedDate DATETIME NOT NULL
);

CREATE TABLE WorkRequestNotes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    WorkRequestId INT NOT NULL,
    Note NVARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    FOREIGN KEY (WorkRequestId) REFERENCES WorkRequests(Id)
);

CREATE INDEX IX_WorkRequests_Status ON WorkRequests(Status);
CREATE INDEX IX_WorkRequests_Title_Client ON WorkRequests(Title, ClientName);