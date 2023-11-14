USE [HCM-2023]
GO

SET IDENTITY_INSERT [Roles] ON
INSERT INTO [Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) 
VALUES 
(1, N'Administrator', 'ADMINISTRATOR', NULL),
(2, N'Employee', 'EMPLOYEE', NULL)
SET IDENTITY_INSERT [Roles] OFF
GO

SET IDENTITY_INSERT [Positions] ON
INSERT INTO [Positions] ([Id], [Name]) 
VALUES 
(1, N'Internal Developer'),
(2, N'Junior Developer'),
(3, N'Mid Developer'),
(4, N'Senior Developer'),
(5, N'Junior Designer'),
(6, N'Senior Designer'),
(7, N'Junior Tester'),
(8, N'Senior Tester')
SET IDENTITY_INSERT [Positions] OFF
GO

SET IDENTITY_INSERT [Departments] ON
INSERT INTO [Departments] ([Id], [Name]) 
VALUES 
(1, N'Architect'),
(2, N'Developer'),
(3, N'UI/UX Designer'),
(4, N'Tester')
SET IDENTITY_INSERT [Departments] OFF
GO

SET IDENTITY_INSERT [Employees] ON 
INSERT INTO [Employees] ([Id], [UserName], [HashedPassword], [FirstName], [LastName], [Email], [PhoneNumber], [IsHired], [HireDate], [PositionId], [Salary], [DepartmentId]) 
VALUES 
(1, N'admin', N'$2a$12$AQDe9jKSadLeCptcFmQrL.8OlWp73tRO3uTPR/mBiio/eFIS9VgYO', N'admin', N'admin', N'admin@admin.bg', N'6666666666', 1, CAST(N'2023-09-01T00:00:00.0000000' AS DateTime2), 4, CAST(5000.00 AS Decimal(18, 2)), 2),
(2, N'oleg_anokhin', N'$2a$12$XK93HuybEGRD8/VGGdrFVukQCndk.Y5xMf8heTtc3qndRoRIvsc5m', N'Oleg', N'Anokhin', N'oleg@abv.bg', N'0888888888', 1, CAST(N'2023-10-02T00:00:00.0000000' AS DateTime2), 1, CAST(2000.00 AS Decimal(18, 2)), 2)
SET IDENTITY_INSERT [Employees] OFF
GO

INSERT INTO [UserRole]([UserId], [RoleId])
VALUES
(1,1),
(2,2)
GO

SET IDENTITY_INSERT [QualificationsTraining] ON
INSERT INTO [QualificationsTraining] ([Id], [Name], [From], [To], [Description]) 
VALUES 
(2, N'DB Architecture', CAST(N'2023-11-27T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-01T00:00:00.0000000' AS DateTime2), N'In this tutorial we will look at the best base structure.'),
(3, N'Write cleare code.', CAST(N'2023-11-06T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-10T00:00:00.0000000' AS DateTime2), N'We look for the best practices in writen a cleare code.')
SET IDENTITY_INSERT [QualificationsTraining] OFF
GO

INSERT INTO [TrainingParticipants] ([ParticipantId], [TrainingId]) 
VALUES 
(1, 2),
(1, 3),
(2, 2),
(2, 3)
GO

SET IDENTITY_INSERT [PreviousPositions] ON
INSERT INTO [PreviousPositions] ([Id], [DepartmentId], [PositionId], [From], [To], [Salary], [EmployeeId]) 
VALUES 
(1, 3, 6, CAST(N'2022-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2), CAST(6666.66 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [PreviousPositions] OFF
GO

SET IDENTITY_INSERT [LeaveRequests] ON
INSERT INTO [LeaveRequests] ([LeaveRequestId], [EmployeeId], [StartDate], [EndDate], [VacationOrSickLeave], [Description], [Approved]) 
VALUES 
(1, 1, CAST(N'2023-12-04T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-08T00:00:00.0000000' AS DateTime2), N'Vacantion', N'I want to go ride my bike.', 1),
(2, 1, CAST(N'2023-11-23T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-24T00:00:00.0000000' AS DateTime2), N'Sick leave', N'Doctor say do that.', 1),
(3, 2, CAST(N'2023-11-20T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-24T00:00:00.0000000' AS DateTime2), N'Vacantion', N'I urgently need to rest.', 1)
SET IDENTITY_INSERT [LeaveRequests] OFF
GO
